using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using Redmine.Client.Languages;
using Redmine.Client.Properties;

namespace Redmine.Client
{
    public delegate void AsyncCloseForm(DialogResult result, Size currentWindowSize);

    public partial class RedmineClientForm : BgWorker
    {
        private string Title = Lang.RedmineClientTitle_NoUser;

        private int ticks = 0;
        private bool ticking = false;
        private int issueId = 0;
        private int projectId = 0;
        private int activityId = 0;
        internal static RedmineManager redmine;
        private bool updating = false;
        private User currentUser = null;
        private string currentWorkName;

        public static string RedmineURL;
        private bool RedmineAuthentication;
        private string RedmineUser;
        private string RedminePassword;
        private bool MinimizeToSystemTray;
        private bool MinimizeOnStartTimer;
        private int PopupInterval;

        private bool CheckForUpdates;
        private int CacheLifetime;
        private DataGridViewColumn currentSortedColumn;

        private Rectangle NormalSize;
        private DateTime MinimizeTime;

        private Dictionary<int, Project> Projects;
        public static ApiVersion RedmineVersion { get; private set; }

        private Filter currentFilter = new Filter();
        public User CurrentUser { get { return currentUser; } }

        private IList<Issue> currentIssues;

        /* ugly hack to create a singleton */
        private static readonly RedmineClientForm instance = new RedmineClientForm();
        public static RedmineClientForm Instance { get { return instance; } }
        private RedmineClientForm()
        {
            InitializeComponent();
            Settings.Default.Upgrade();
            Settings.Default.Reload();

            timer1.Interval = 1000;

            if (!IsRunningOnMono())
            {
                this.Icon = (Icon)Resources.ResourceManager.GetObject("clock");
                this.notifyIcon1.Icon = (Icon)Resources.ResourceManager.GetObject("clock");
                this.notifyIcon1.Visible = true;
            }
			else 
			{
				this.DataGridViewIssues.Click += new System.EventHandler(this.DataGridViewIssues_SelectionChanged);
			}
            this.FormClosing += new FormClosingEventHandler(RedmineClientForm_FormClosing);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            Reinit(false);
 
            //At last add check-for-updates work...
            if (this.CheckForUpdates)
            {
                AddBgWork(Lang.BgWork_CheckUpdates, () =>
                {
                    AsyncCheckForUpdates();
                    return null;
                });
            }
        }

        bool OnInitFailed(Exception e)
        {
            this.Cursor = Cursors.Default;
            if (MessageBox.Show(String.Format(Lang.Error_Exception, e.Message), Lang.Error_Startup, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) != DialogResult.OK)
                return false;
            if (!ShowSettingsForm())
                return false;
            return true;
        }

        void RedmineClientForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (ticks != 0 && !Settings.Default.PauseTickingOnLock)
            {
                switch (MessageBox.Show(String.Format(Lang.Warning_ClosingSaveTimes, Environment.NewLine), Lang.Warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                    case DialogResult.Yes:
                        BtnCommitButton_Click(null, null);
                        break;
                }
            }
        }

        void LoadLastIds()
        {
            try
            {
                projectId = ((Project)ComboBoxProject.SelectedItem).Id;
                activityId = ((IdentifiableName)ComboBoxActivity.SelectedItem).Id;
                issueId = ((Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem).Id;
            }
            catch (Exception) { }
        }

        void Reinit(bool clientIsRunning = true)
        {
            if (clientIsRunning)
                SaveRuntimeConfig();
            bool bRetry = false;
            do
            {
                try
                {
                    LoadConfig();
                    if (!clientIsRunning)
                    {
                        this.ticks = Settings.Default.TickingTicks;
                        this.UpdateTime();
                        if (Settings.Default.IsTicking)
                        {
                            if (MessageBox.Show(Lang.Timer_WasTickingWhenClosed, Lang.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                this.StartTimer();
                        }
                    }

                    if (RedmineAuthentication)
                        redmine = new RedmineManager(RedmineURL, RedmineUser, RedminePassword, Settings.Default.CommunicationType);
                    else
                        redmine = new RedmineManager(RedmineURL, Settings.Default.CommunicationType);
                    this.Cursor = Cursors.AppStarting;

                    AsyncGetFormData(projectId, issueId, activityId, CheckBoxOnlyMe.Checked);
                }
                catch (Exception e)
                {
                    if (OnInitFailed(e))
                        bRetry = true;
                }
            } while (bRetry);
        }

        int GetProjectIdCheckExists(Dictionary<int, Project> projects, int projectId)
        {
            Project CurProject;
            if (projectId != -1 && (!projects.TryGetValue(projectId, out CurProject)
                || projectId == 0))
            {
                try
                {
                    IEnumerator<int> enumerator = projects.Keys.GetEnumerator();
                    enumerator.MoveNext();
                    projectId = (int)enumerator.Current;
                }
                catch (Exception)
                {
                    projectId = -1;
                }
            }
            return projectId;
        }

        private MainFormData PrepareFormData(int projectId, bool onlyMe, Filter filter)
        {
            NameValueCollection parameters = new NameValueCollection();
            IList<Project> projects = OnlyProjectsForMember(currentUser, redmine.GetTotalObjectList<Project>(parameters));
            if (projects.Count > 0)
            {
                Projects = MainFormData.ToDictionaryName(projects);

                projectId = GetProjectIdCheckExists(Projects, projectId);
                return new MainFormData(projects, projectId, onlyMe, filter);
            }
            throw new Exception(Lang.Error_NoProjectsFound);
        }

        private IList<Project> OnlyProjectsForMember(User member, IList<Project> projects)
        {
            List<Project> memberProjects = new List<Project>();
            foreach (Project p in projects)
            {
                foreach (Membership m in member.Memberships)
                {
                    if (p.Id == m.Project.Id)
                    {
                        memberProjects.Add(p);
                        break;
                    }
                }
            }
            return memberProjects;
        }

        private void FillForm(MainFormData data, int issueId, int activityId, Filter filter)
        {
            updating = true;
            this.projectId = GetProjectIdCheckExists(Projects, this.projectId);

            BtnSettingsButton.Enabled = true;
            BtnRefreshButton.Enabled = true;
            BtnOpenIssueButton.Enabled = true;

            if (data.Projects.Count == 0 || data.Issues.Count == 0)
            {
                BtnCommitButton.Enabled = false;
                if (data.Projects.Count > 0 && projectId != -1)
                {
                    BtnNewIssueButton.Enabled = true;   
                }
                else
                {
                    BtnNewIssueButton.Enabled = false;
                }
                
            }
            else
            {
                BtnCommitButton.Enabled = true;
                BtnNewIssueButton.Enabled = projectId != -1;
            }

            ComboBoxProject.DataSource = data.Projects;
            ComboBoxProject.ValueMember = "Id";
            ComboBoxProject.DisplayMember = "DisplayName";

            if (RedmineVersion >= ApiVersion.V22x)
            {
                Enumerations.UpdateActivities(data.Activities);
                Enumerations.SaveActivities();
            }
            
            ComboBoxActivity.DataSource = Enumerations.Activities;
            ComboBoxActivity.DisplayMember = "Name";
            ComboBoxActivity.ValueMember = "Id";

            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x && data.ProjectMembers != null)
                {
                    labelAssignedTo.Enabled = true;
                    ComboBoxAssignedTo.Enabled = true;
                    ComboBoxAssignedTo.DataSource = data.ProjectMembers;
                    ComboBoxAssignedTo.DisplayMember = "Name";
                    ComboBoxAssignedTo.ValueMember = "Id";
                    ComboBoxAssignedTo.SelectedValue = filter.AssignedToId;
                }
                else
                {
                    ComboBoxAssignedTo.Enabled = false;
                    labelAssignedTo.Enabled = false;
                    ComboBoxAssignedTo.DataSource = null;
                }

                ComboBoxStatus.DataSource = data.Statuses;
                ComboBoxStatus.DisplayMember = "Name";
                ComboBoxStatus.ValueMember = "Id";
                ComboBoxStatus.SelectedValue = filter.StatusId;

                if (data.Versions != null)
                {
                    labelTargetVersion.Enabled = true;
                    ComboBoxTargetVersion.Enabled = true;
                    ComboBoxTargetVersion.DataSource = data.Versions;
                    ComboBoxTargetVersion.DisplayMember = "Name";
                    ComboBoxTargetVersion.ValueMember = "Id";
                    ComboBoxTargetVersion.SelectedValue = filter.VersionId;
                }
                else
                {
                    labelTargetVersion.Enabled = false;
                    ComboBoxTargetVersion.Enabled = false;
                    ComboBoxTargetVersion.DataSource = null;
                }

                ComboBoxTracker.DataSource = data.Trackers;
                ComboBoxTracker.DisplayMember = "Name";
                ComboBoxTracker.ValueMember = "Id";
                ComboBoxTracker.SelectedValue = filter.TrackerId;

                if (data.Categories != null)
                {
                    labelCategory.Enabled = true;
                    ComboBoxCategory.Enabled = true;
                    ComboBoxCategory.DataSource = data.Categories;
                    ComboBoxCategory.DisplayMember = "Name";
                    ComboBoxCategory.ValueMember = "Id";
                    ComboBoxCategory.SelectedValue = filter.CategoryId;
                }
                else
                {
                    labelCategory.Enabled = false;
                    ComboBoxCategory.Enabled = false;
                    ComboBoxCategory.DataSource = null;
                }
                UpdateFilterControls();
            }
            else
            {
                ComboBoxAssignedTo.Enabled = false;
                ComboBoxStatus.Enabled = false;
                ComboBoxTargetVersion.Enabled = false;
                ComboBoxTracker.Enabled = false;
                ComboBoxCategory.Enabled = false;
            }
            ComboBoxPriority.DataSource = data.IssuePriorities;
            ComboBoxPriority.DisplayMember = "Name";
            ComboBoxPriority.ValueMember = "Id";
            ComboBoxPriority.SelectedValue = filter.PriorityId;

            currentIssues = data.Issues;

            FilterAndFillCurrentIssues();
        }

        bool IssueHasKeyword(Issue issue, String keyword)
        {
            if (issue.Subject.ToLower().Contains(keyword))
                return true;
            if (issue.Status.ToString().ToLower().Contains(keyword))
                return true;
            if (issue.Id.ToString().Contains(keyword))
                return true;
            if (projectId == -1 && // Only search project when there is no project pre-filter
                issue.Project.Name.ToLower().Contains(keyword))
                return true;
            return false;
        }

        void FilterAndFillCurrentIssues()
        {
            if (currentIssues == null)
                return; // No issues yet
            String[] keywords = textBoxSearch.Text.ToLower().Split(' ');
            IList<Issue> filteredIssues = new List<Issue>();
            foreach (Issue i in currentIssues)
            {
                bool found = true;
                foreach (String keyword in keywords)
                {
                    if (!IssueHasKeyword(i, keyword))
                    {
                        found = false;
                        break;
                    }
                }
                if (!found)
                    continue;
                filteredIssues.Add(i);
            }
            FillIssues(filteredIssues);
        }

        void FillIssues(IList<Issue> Issues)
        {
            DataGridViewIssues.DataSource = Issues;
            UpdateIssueDataColumns();
            try // Very ugly trick to fix the mono crash reported in the SF.net forum
            {
                DataGridViewIssues.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception) { }
            DataGridViewIssues.RowHeadersWidth = 20;
            DataGridViewIssues.Columns["Id"].DisplayIndex = 0;
            DataGridViewIssues.Columns["Subject"].DisplayIndex = 1;
            DataGridViewIssues.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewIssues.Columns["Subject"].MinimumWidth = 200;
            if (projectId == -1)
                DataGridViewIssues.Columns["Project"].DisplayIndex = 2;
            if (currentSortedColumn == null)
            {
                currentSortedColumn = DataGridViewIssues.Columns[Settings.Default.IssueGridSortColumn];
                SortOrder order = Settings.Default.IssueGridSortOrder;
                InvertSort(ref order);
                currentSortedColumn.HeaderCell.SortGlyphDirection = order;
            }
            
            if (!DataGridViewIssues.Columns[currentSortedColumn.Name].Visible)
                DataGridViewIssues_SortByColumn(DataGridViewIssues.Columns["Id"], SortOrder.Ascending);
            else
                DataGridViewIssues_SortByColumn(DataGridViewIssues.Columns[currentSortedColumn.Name], null);

            if (ComboBoxProject.Items.Count > 0)
            {
                if (projectId != 0)
                    ComboBoxProject.SelectedValue = projectId;
                else
                    ComboBoxProject.SelectedIndex = 0;
                 projectId = (int)ComboBoxProject.SelectedValue;
            }
            if (ComboBoxActivity.Items.Count > 0)
            {
                if (activityId != 0)
                    ComboBoxActivity.SelectedValue = activityId;
                else
                    ComboBoxActivity.SelectedIndex = 0;
                activityId = (int)ComboBoxActivity.SelectedValue;
            }
            SetIssueSelectionTo(issueId);
            updating = false;
            this.Cursor = Cursors.Default;
        }

        private void SaveRuntimeConfig()
        {
            if (Size != null && WindowState == FormWindowState.Normal)
            {
                Settings.Default.UpdateSetting("MainWindowSizeX", Size.Width);
                Settings.Default.UpdateSetting("MainWindowSizeY", Size.Height);
            }
            Settings.Default.UpdateSetting("LastProjectId", projectId);
            Settings.Default.UpdateSetting("LastIssueId", issueId);
            Settings.Default.UpdateSetting("LastActivityId", activityId);
            Settings.Default.UpdateSetting("OnlyAssignedToMe", CheckBoxOnlyMe.Checked);
            Settings.Default.Save();
        }

        private void LoadConfig()
        {
            Enumerations.LoadAll();

            if (Lang.Culture == null)
                Lang.Culture = System.Globalization.CultureInfo.CurrentUICulture;
            Settings.Default.Reload();
            RedmineURL = Settings.Default.RedmineURL;
            RedmineAuthentication = Settings.Default.RedmineAuthentication;
            RedmineUser = Settings.Default.RedmineUser;
            RedminePassword = Settings.Default.RedminePassword;
            MinimizeToSystemTray = Settings.Default.MinimizeToSystemTray;
            MinimizeOnStartTimer = Settings.Default.MinimizeOnStartTimer;
            CheckForUpdates = Settings.Default.CheckForUpdates;
            CacheLifetime = Settings.Default.CacheLifetime;
            PopupInterval = Settings.Default.PopupInterval;
            RedmineVersion = (ApiVersion)Settings.Default.ApiVersion;

            int sizeX = Settings.Default.MainWindowSizeX;
            int sizeY = Settings.Default.MainWindowSizeY;
            Size FormSize = new Size(
                                      Settings.Default.MainWindowSizeX,
                                      Settings.Default.MainWindowSizeY);
            if (FormSize.Height > 0 && FormSize.Width > 0)
                Size = FormSize;

            try
            {
                Lang.Culture = new System.Globalization.CultureInfo(Settings.Default.LanguageCode);
            }
            catch (Exception)
            {
                Lang.Culture = System.Globalization.CultureInfo.CurrentUICulture;
            }

            LangTools.UpdateControlsForLanguage(this.Controls);
            LangTools.UpdateControlsForLanguage(NotifyIconMenuStrip.Items);
            LangTools.UpdateControlsForLanguage(IssueGridHeaderMenuStrip.Items);
            LangTools.UpdateControlsForLanguage(IssueGridMenuStrip.Items);
            SetRestoreToolStripMenuItem();
            UpdateToolStripMenuItemsStartPause();

            projectId = Settings.Default.LastProjectId;
            issueId = Settings.Default.LastIssueId;
            activityId = Settings.Default.LastActivityId;
            CheckBoxOnlyMe.Checked = Settings.Default.OnlyAssignedToMe;
            UpdateFilterControls();

            BtnNewIssueButton.Visible = RedmineVersion >= ApiVersion.V13x;
        }

        private void UpdateFilterControls()
        {
            if (ComboBoxAssignedTo.DataSource != null)
            {
                labelAssignedTo.Enabled = !CheckBoxOnlyMe.Checked;
                ComboBoxAssignedTo.Enabled = !CheckBoxOnlyMe.Checked;
            }
        }

        private void UpdateToolStripMenuItemsStartPause()
        {
            StartToolStripMenuItem.Enabled = !ticking;
            PauseToolStripMenuItem.Enabled = ticking;
        }

        private void SetRestoreToolStripMenuItem()
        {
            if (WindowState == FormWindowState.Minimized)
                RestoreToolStripMenuItem.Text = Languages.Lang.RestoreToolStrip_Restore;
            else
            {
                if (MinimizeToSystemTray)
                    RestoreToolStripMenuItem.Text = Languages.Lang.RestoreToolStrip_Hide;
                else
                    RestoreToolStripMenuItem.Text = Languages.Lang.RestoreToolStrip_Minimize;
            }
        }

        private void HideRestore()
        {
            if (WindowState == FormWindowState.Normal)
            {
                Minimize();
            }
            else
            {
                Restore();
            }
        }

        private void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            NormalSize = this.RestoreBounds;
            if (MinimizeToSystemTray)
                Hide();
            SetRestoreToolStripMenuItem();
            MinimizeTime = DateTime.Now;
        }

        private void Restore()
        {
            if (MinimizeToSystemTray)
                Show();
            Bounds = NormalSize;
            WindowState = FormWindowState.Normal;
            SetRestoreToolStripMenuItem();
            Activate();
         }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Minimize();
            }
            else
            {
                SetRestoreToolStripMenuItem();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HideRestore();
        }

        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideRestore();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnStartButton_Click(sender, e);
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnStartButton_Click(sender, e);
        }

        private void BtnExitButton_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void BtnStartButton_Click(object sender, EventArgs e)
        {
            if (ticking)
            {
                timer1.Stop();
                BtnStartButton.Text = Lang.BtnStartButton;
                ticking = false;
            }
            else
            {
                StartTimer();
            }
            Settings.Default.SetTickingTick(this.ticking, this.ticks);
            UpdateNotifyIconText();
            UpdateToolStripMenuItemsStartPause();
        }

        private void StartTimer()
        {
            timer1.Start();
            BtnStartButton.Text = Lang.BtnStartButton_Pause;
            ticking = true;
            UpdateIssueIfNeeded();
            if (MinimizeOnStartTimer)
                Minimize();
        }

 

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ticks++;
            this.UpdateTime();
            Settings.Default.SetTickingTick(this.ticking, this.ticks);
            AlertIfMinimized();
        }

        private void AlertIfMinimized()
        {
            if (WindowState == FormWindowState.Minimized && PopupInterval > 0)
                if ((DateTime.Now-MinimizeTime).TotalMinutes >= PopupInterval)
                    Restore();
        }

        private void ResetForm()
        {
            this.ticks = 0;
            this.UpdateTime();
            Settings.Default.SetTickingTick(this.ticking, this.ticks);
            this.dateTimePicker1.Value = DateTime.Now;
            this.TextBoxComment.Text = String.Empty;
        }

        private void UpdateNotifyIconText()
        {
            if (ticking)
            {
                string issueText = "";
                string activityText = "";
                if (DataGridViewIssues.SelectedRows.Count == 1)
                {
                    Issue selectedIssue = (Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem;
                    issueText = String.Format("({0}) {1}", selectedIssue.Id, selectedIssue.Subject);
                }
                if (ComboBoxActivity.SelectedItem != null)
                {
                    IdentifiableName selectedActivity = (IdentifiableName)ComboBoxActivity.SelectedItem;
                    activityText = selectedActivity.Name;
                }
                string finalText = String.Format("{3} - {2}{0}{1}", Environment.NewLine, issueText, activityText, Lang.RedmineClientTitle_NoUser);
                if (finalText.Length>63)
                    finalText = String.Format("{0}...", finalText.Substring(0,60));
                this.notifyIcon1.Text = finalText;
            }
            else
                this.notifyIcon1.Text = Lang.RedmineClientTitle_NoUser;
        }

        private void UpdateTime()
        {
            this.updating = true;
            this.TextBoxHours.Text   = (ticks / 3600)   .ToString("D2");
            this.TextBoxMinutes.Text = (ticks / 60 % 60).ToString("D2");
            this.TextBoxSeconds.Text = (ticks % 60)     .ToString("D2");
            this.updating = false;
        }

        private void UpdateTicks()
        {
            ticks = Convert.ToInt32(TextBoxHours.Text)*3600 + Convert.ToInt32(TextBoxMinutes.Text)*60 +
                    Convert.ToInt32(TextBoxSeconds.Text);
            Properties.Settings.Default.SetTickingTick(ticking, ticks);
            UpdateTime();
        }

        private void TextBoxHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!RedmineClientForm.CheckNumericValue(new string(e.KeyChar, 1), 0, 9) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void TextBoxMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!RedmineClientForm.CheckNumericValue(new string(e.KeyChar, 1), 0, 9) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void TextBoxSeconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!RedmineClientForm.CheckNumericValue(new string(e.KeyChar, 1), 0, 9) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void TimeControl_Leave(TextBox control, int maxValue)
        {
            if (!CheckNumericValue(control.Text, 0, maxValue))
            {
                MessageBox.Show(Lang.Error_ValueOutOfRange, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
                control.Focus();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();
                }
            }
        }

        private void TextBoxSeconds_Leave(object sender, EventArgs e)
        {
            TimeControl_Leave(TextBoxSeconds, 59);
        }

        private void TextBoxMinutes_Leave(object sender, EventArgs e)
        {
            TimeControl_Leave(TextBoxMinutes, 59);
        }

        private void TextBoxHours_Leave(object sender, EventArgs e)
        {
            TimeControl_Leave(TextBoxHours, 999);
        }

        /// <summary>
        /// Check if the value is numeric and between the provided minimum and maximum
        /// </summary>
        /// <param name="val">string value with numbers</param>
        /// <param name="min">minumum value</param>
        /// <param name="max">maximum value</param>
        /// <returns></returns>
        public static bool CheckNumericValue(string val, int min, int max)
        {
            int myval;
            bool succ = Int32.TryParse(val, out myval);
            if (!succ || myval > max || myval < min)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Commit the current time to Redmine and if applicable, close the current issue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCommitButton_Click(object sender, EventArgs e)
        {
            bool shouldIRestart = ticking;

            if (DataGridViewIssues.SelectedRows.Count == 1 && ComboBoxActivity.SelectedItem != null && ticks != 0)
            {
                Issue selectedIssue = (Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem;
                IdentifiableName selectedActivity = (IdentifiableName)ComboBoxActivity.SelectedItem;

                ticking = false;
                timer1.Stop();
                BtnStartButton.Text = Lang.BtnStartButton;
                CommitForm commitDlg = new CommitForm(selectedIssue, ticks, TextBoxComment.Text, selectedActivity.Id, dateTimePicker1.Value);
                if (commitDlg.ShowDialog(this) == DialogResult.OK)
                {
                    TimeEntry entry = new TimeEntry();
                    entry.Activity = new IdentifiableName { Id = commitDlg.activityId };
                    entry.Comments = commitDlg.Comment;
                    entry.Hours = (decimal)ticks / 3600;
                    entry.Issue = new IdentifiableName { Id = selectedIssue.Id };
                    entry.Project = new IdentifiableName { Id = selectedIssue.Project.Id };
                    entry.SpentOn = dateTimePicker1.Value;
                    entry.User = new IdentifiableName { Id = currentUser.Id };
                    try
                    {
                        redmine.CreateObject(entry);
                        ResetForm();
                        MessageBox.Show(Lang.CommitSuccessfullText, Lang.CommitSuccessfullTitle, MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        if (commitDlg.closeIssue)
                        {
                            if (Settings.Default.ClosedStatus == 0)
                            {
                                MessageBox.Show(Lang.Error_ClosedStatusUnknown, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                try
                                {
                                    UpdateIssueState(selectedIssue, Settings.Default.ClosedStatus);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(String.Format(Lang.Error_UpdateIssueFailed, ex.Message),
                                                Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                BtnRefreshButton_Click(null, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (shouldIRestart)
                {
                    TextBoxComment.Text = commitDlg.Comment;
                    ticking = true;
                    timer1.Start();
                    BtnStartButton.Text = Lang.BtnStartButton_Pause;
                }
                else
                    TextBoxComment.Text = commitDlg.Comment;
            }
            else
            {
                if (ticks == 0)
                {
                    MessageBox.Show(Lang.CommitNoTime, Lang.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                }
                else if (DataGridViewIssues.SelectedRows.Count != 1)
				{
                    MessageBox.Show(Lang.CommitNoIssueSelected, Lang.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
                else if (ComboBoxActivity.SelectedItem == null)
                {
                    MessageBox.Show(Lang.CommitNoActivitySelected, Lang.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// A new activity has been selected; update the systemtray
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(ComboBoxActivity.SelectedValue.ToString(), out activityId))
            {
                activityId = 0;
            }
            UpdateNotifyIconText();
        }

        /// <summary>
        /// A new project is selected; update the form data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
                RefreshFormData();
        }

        /// <summary>
        /// Is executable running on Mono?
        /// </summary>
        /// <returns>true if running on Mono</returns>
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// Refresh de form data at onclick refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshButton_Click(object sender, EventArgs e)
        {
            RefreshFormData();
        }

        /// <summary>
        /// Refresh de form data synchronous
        /// </summary>
        private void RefreshFormData()
        {
            LoadLastIds();

            this.Cursor = Cursors.AppStarting;
            SetCurrentWorkName(Lang.BgWork_GetFormData);
            try
            {
                Filter newFilter = (Filter)currentFilter.Clone();
                FillForm(PrepareFormData(projectId, CheckBoxOnlyMe.Checked, newFilter), issueId, activityId, newFilter);
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Cursor = Cursors.Default;
            SetCurrentWorkName("");
            BtnRefreshButton.Text = Lang.BtnRefreshButton;
        }

        /// <summary>
        /// Show the settings form
        /// </summary>
        /// <returns>True if window was closed with OK</returns>
        private bool ShowSettingsForm()
        {
            SettingsForm dlg = new SettingsForm();
            return dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Open the settings dialog and reload the data after successful closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettingsButton_Click(object sender, EventArgs e)
        {
            SaveRuntimeConfig();
            if (!ShowSettingsForm())
                return; //User canceled.

            Reinit();
        }

        /// <summary>
        /// Create a new issue through the issue dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewIssueButton_Click(object sender, EventArgs e)
        {
            IssueForm dlg = new IssueForm(Projects[projectId]);
            dlg.Size = new Size(Settings.Default.IssueWindowSizeX,
                                Settings.Default.IssueWindowSizeY);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BtnRefreshButton_Click(null, null);
            }
            Settings.Default.UpdateSetting("IssueWindowSizeX", dlg.Size.Width);
            Settings.Default.UpdateSetting("IssueWindowSizeY", dlg.Size.Height);
            Settings.Default.Save();
        }

        private void BtnAboutButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void BtnResetButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        /// <summary>
        /// Get Projects, Issues and Activities and select the current/last selected
        /// </summary>
        /// <param name="projectId">The current/last selected project</param>
        /// <param name="issueId">The current/last selected issue</param>
        /// <param name="activityId">The current/last selected activity</param>
        /// <param name="onlyMe">Retrieve only issues assigned to me</param>
        /// <param name="filter">Retrieve only issues matchig the filter</param>
        private void AsyncGetRestOfFormData(int projectId, int issueId, int activityId, bool onlyMe, Filter filter)
        {
            AddBgWork(Lang.BgWork_GetFormData, () =>
            {
                try
                {
                    MainFormData data = PrepareFormData(projectId, onlyMe, filter);
                    
                    //Let main thread fill form data...
                    return () =>
                    {
                        FillForm(data, issueId, activityId, (Filter)currentFilter.Clone());
                        BtnRefreshButton.Text = Lang.BtnRefreshButton;
                        this.Cursor = Cursors.Default;
                    };
                }
                catch (Exception e)
                {
                    //Show the exception in the main thread
                    return () =>
                    {
                        //this.Cursor = Cursors.Default;
                        //MessageBox.Show(String.Format(Lang.Error_Exception, e.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        if (OnInitFailed(e))
                            Reinit();
                    };
                }
            });
        }

        /// <summary>
        /// Retrieve the form data asynchronous (first only the username)
        /// </summary>
        /// <param name="projectId">The current/last selected project</param>
        /// <param name="issueId">The current/last selected issue</param>
        /// <param name="activityId">The current/last selected activity</param>
        /// <param name="onlyMe">Retrieve only issues assigned to me</param>
        private void AsyncGetFormData(int projectId, int issueId, int activityId, bool onlyMe)
        {
            this.Cursor = Cursors.WaitCursor;
            //Retrieve current user asynchroneous...

            this.BtnCommitButton.Enabled = false;
            this.BtnRefreshButton.Enabled = false;
            this.BtnOpenIssueButton.Enabled = false;
            this.BtnNewIssueButton.Enabled = false;
            this.BtnSettingsButton.Enabled = false;

            AddBgWork(Lang.BgWork_GetUsername, () =>
            {
                try
                {
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("include", "memberships");
                    User newCurrentUser = redmine.GetCurrentUser(parameters);
                    return () =>
                    {
                        currentUser = newCurrentUser;
                        if (currentUser != null)
                            SetTitle(String.Format(Lang.RedmineClientTitle_User, currentUser.FirstName, currentUser.LastName));
                        else
                            SetTitle(Lang.RedmineClientTitle_NoUser);
                        //When done, get the rest of the form data...
                        AsyncGetRestOfFormData(projectId, issueId, activityId, onlyMe, currentFilter);
                    };
                }
                catch (Exception e)
                {
                    return () =>
                    {
                        currentUser = null;
                        SetTitle(Lang.RedmineClientTitle_NoUser);
                        if (OnInitFailed(e))
                            Reinit();
                        else
                            this.BtnSettingsButton.Enabled = true;
                    };
                }
            });
        }

        /// <summary>
        /// Set the (new) title of this dialog
        /// </summary>
        /// <param name="title">The (new) title</param>
        void SetTitle(String title)
        {
            Title = title;
            UpdateTitle();
        }

        /// <summary>
        /// Update the window title with the current background job (if one)
        /// </summary>
        void UpdateTitle()
        {
            String title = Title;
            if (!String.IsNullOrEmpty(currentWorkName))
                title += " - " + currentWorkName + "...";
            this.Text = title;
        }

        override protected void WorkTriggered(BgWork CurrentWork) 
        {
            if (CurrentWork != null)
                SetCurrentWorkName(CurrentWork.m_name);
            else
                SetCurrentWorkName("");
        }

        private void SetCurrentWorkName(string currentWorkName)
        {
            this.currentWorkName = currentWorkName;
            UpdateTitle();
        }

        /// <summary>
        /// Check for updates and if there is an update available, send the user to the download URL.
        /// </summary>
        void AsyncCheckForUpdates()
        {
            string latestVersionUrl = Utility.CheckForUpdate();
            if (latestVersionUrl != String.Empty)
            {
                if (MessageBox.Show(Lang.NewVersionText,
                                    Lang.NewVersionTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(latestVersionUrl);
                }
            }
        }

        public static void ShowIssue(Issue issue)
        {
            try
            {
                foreach (Form f in Application.OpenForms)
                {
                    if (f.GetType() == typeof(IssueForm))
                    {
                        if (((IssueForm)f).ShowingIssue(issue.Id))
                        {
                            f.Focus();
                            return;
                        }
                    }
                }
                IssueForm dlg = new IssueForm(issue);
                dlg.Size = new Size(Settings.Default.IssueWindowSizeX,
                                    Settings.Default.IssueWindowSizeY);
                dlg.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void IssueFormClosed(DialogResult result, Size currentWindowSize)
        {
            if (result == DialogResult.OK)
            {
                BtnRefreshButton_Click(null, null);
            }
            Settings.Default.UpdateSetting("IssueWindowSizeX", currentWindowSize.Width);
            Settings.Default.UpdateSetting("IssueWindowSizeY", currentWindowSize.Height);
            Settings.Default.Save();
        }

        /// <summary>
        /// When closing the form, save the running settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            LoadLastIds();
            SaveRuntimeConfig();
        }

        /// <summary>
        /// When the system is locked, check the setting and if requested, stop the timer.
        /// Also start the timer again on unlock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (Settings.Default.PauseTickingOnLock)
            {
                switch (e.Reason)
                {
                    case SessionSwitchReason.SessionLock:
                        timer1.Stop();
                        break;
                    case SessionSwitchReason.SessionUnlock:
                        if (this.ticking)
                        {
                            timer1.Start();
                        }
                        break;
                }
                
            }
        }

        /// <summary>
        /// If the setting for updating the issue is true, then check the state of the current selected issue and if necessary update it in Redmine.
        /// </summary>
        private void UpdateIssueIfNeeded()
        {
            if (!Settings.Default.UpdateIssueIfNew)
                return;

            if (DataGridViewIssues.SelectedRows.Count != 1)
                return;

            if (Settings.Default.NewStatus == 0 || Settings.Default.InProgressStatus == 0)
            {
                MessageBox.Show(Lang.Error_NewOrInProgressStatusUnknown, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            try
            {
                Issue selectedIssue = (Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem;
                if (selectedIssue.Status.Id == Settings.Default.NewStatus)
                {
                    if (UpdateIssueState(selectedIssue, Settings.Default.InProgressStatus))
                        MessageBox.Show(String.Format(Lang.IssueUpdatedToInProgress, selectedIssue.Subject, Settings.Default.InProgressStatus), Lang.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_UpdateIssueFailed, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private bool UpdateIssueState(Issue issue, int idState)
        {
            Issue originalIssue = redmine.GetObject<Issue>(issue.Id.ToString(), null);
            if (originalIssue.Status.Id == idState)
                return false;

            Issue newIssue = (Issue)originalIssue.Clone();

            Dictionary<int, IssueStatus> statusDict = MainFormData.ToDictionaryName<IssueStatus>(redmine.GetObjectList<IssueStatus>(null));
            IssueStatus newStatus;
            if (!statusDict.TryGetValue(idState, out newStatus))
                throw new Exception(Lang.Error_ClosedStatusUnknown);

            newIssue.Status = new IdentifiableName { Id = newStatus.Id, Name = newStatus.Name };
            if (Settings.Default.AddNoteOnChangeStatus)
            {
                UpdateIssueNoteForm dlg = new UpdateIssueNoteForm(originalIssue, newIssue);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    newIssue.Notes = dlg.Note;
                    RedmineClientForm.redmine.UpdateObject<Issue>(originalIssue.Id.ToString(), newIssue);
                }
                else
                    return false;
            }
            else
                RedmineClientForm.redmine.UpdateObject<Issue>(originalIssue.Id.ToString(), newIssue);
            return true;
        }


        #region DataGridViewIssues Functions
        /// <summary>
        /// A new Issue has been selected; update the systemtray
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewIssues_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewIssues.SelectedRows.Count == 0 || !Int32.TryParse(DataGridViewIssues.SelectedRows[0].Cells["Id"].Value.ToString(), out issueId))
            {
                issueId = 0;
            }
            UpdateNotifyIconText();
        }

        /// <summary>
        /// Allow users to double click on the issues and open the issue dialog to display the double clicked issue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewIssues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            Issue issue = (Issue)DataGridViewIssues.Rows[e.RowIndex].DataBoundItem;
            ShowIssue(issue);
        }

        private void UpdateIssueDataColumns()
        {
            foreach (DataGridViewColumn column in DataGridViewIssues.Columns)
            {
                if (column.Name != "Id" && column.Name != "Subject")
                    column.Visible = Settings.Default.ShowIssueGridColumn(column.Name);
                if (projectId == -1 && column.Name == "Project")
                    column.Visible = true;
                if (column.Visible)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }
                column.HeaderCell.ContextMenuStrip = IssueGridHeaderMenuStrip;
                column.HeaderText = LangTools.GetIssueField(column.Name); // translate the headers
                column.ContextMenuStrip = IssueGridMenuStrip;
            }
        }

        private void SetIssueSelectionTo(int issueId)
        {
            if (DataGridViewIssues.Rows.Count > 0)
            {
                DataGridViewIssues.ClearSelection();
                foreach (DataGridViewRow row in DataGridViewIssues.Rows)
                {
                    if (((Issue)row.DataBoundItem).Id == issueId)
                    {
                        row.Selected = true;
                        DataGridViewIssues_SelectionChanged(null, null);
                        break;
                    }
                }
            }
        }

        private void DataGridViewIssues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            Issue currentIssue = (Issue)DataGridViewIssues.Rows[e.RowIndex].DataBoundItem;

            if (e.ColumnIndex == DataGridViewIssues.Columns["Id"].Index) // Id column
                e.Value = currentIssue.Tracker.Name + " " + currentIssue.Id.ToString();
            else if (e.ColumnIndex == DataGridViewIssues.Columns["Project"].Index)
                e.Value = currentIssue.Project.Name;
            else if (e.ColumnIndex == DataGridViewIssues.Columns["ParentIssue"].Index)
                e.Value = currentIssue.ParentIssue!=null?currentIssue.ParentIssue.Id.ToString() + (currentIssue.ParentIssue.Name!=null?" " + currentIssue.ParentIssue.Name:"") :"";
            else if (e.ColumnIndex == DataGridViewIssues.Columns["AssignedTo"].Index)
                e.Value = currentIssue.AssignedTo!=null?currentIssue.AssignedTo.Name:"";
            else if (e.ColumnIndex == DataGridViewIssues.Columns["Status"].Index)
                e.Value = currentIssue.Status.Name;
            else if (e.ColumnIndex == DataGridViewIssues.Columns["Priority"].Index)
                e.Value = currentIssue.Priority.Name;
            else if (e.ColumnIndex == DataGridViewIssues.Columns["Category"].Index)
                e.Value = currentIssue.Category!=null?currentIssue.Category.Name:"";
            else if (e.ColumnIndex == DataGridViewIssues.Columns["FixedVersion"].Index)
                e.Value = currentIssue.FixedVersion != null ? currentIssue.FixedVersion.Name : "";
        }

        class CompareIssue : IComparer<Issue>
        {
            public CompareIssue(string column, SortOrder sortOrder)
            {
                this.column = column;
                this.sortOrder = sortOrder;
            }
            private string column;
            private SortOrder sortOrder;

            #region IComparer<Issue> Members

            public int Compare(Issue left, Issue right)
            {
                Issue x, y;
                if (sortOrder == SortOrder.Ascending)
                {
                    x = left;
                    y = right;
                }
                else
                {
                    x = right;
                    y = left;
                }
                if (column == "Id")
                {
                    return x.Id.CompareTo(y.Id);
                }
                else
                {
                    Type type = GetPropertyType(x, column);
                    if (type == typeof(IdentifiableName))
                    {
                        var valx = GetPropertyValue<IdentifiableName>(x, column);
                        var valy = GetPropertyValue<IdentifiableName>(y, column);
                        if (valx == null || valy == null)
                        {
                            if (valx == null && valy != null)
                                return -1;
                            else if (valx != null && valy == null)
                                return 1;
                            return 0;
                        }
                        return valx.Name.CompareTo(valy.Name);
                    }
                    else if (type == typeof(string))
                    {
                        var valx = GetPropertyValue<string>(x, column);
                        var valy = GetPropertyValue<string>(y, column);
                        return valx.CompareTo(valy);
                    }
                }
                return 0;
            }

            #endregion
            #region Get Property Values
            private T GetPropertyValue<T>(object o, string p) where T : class
            {
                return (T)o.GetType().GetProperty(p).GetValue(o, null);
            }
            private Type GetPropertyType(object o, string p)
            {
                return o.GetType().GetProperty(p).GetValue(o, null).GetType();
            }
            #endregion
        }

        private void DataGridViewIssues_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            if (e.Button != MouseButtons.Left)
                return;

            DataGridViewIssues_SortByColumn(DataGridViewIssues.Columns[e.ColumnIndex], null);
        }

        private void DataGridViewIssues_SortByColumn(DataGridViewColumn sortColumn, SortOrder? newOrder)
        {
            SortOrder sortOrder = sortColumn.HeaderCell.SortGlyphDirection;
            // reset current sortcolumn after retrieving the current sortorder.
            if (currentSortedColumn != null && currentSortedColumn.DataGridView == DataGridViewIssues)
                currentSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;

            int currentSelectedIssue = issueId;
            if (sortOrder == SortOrder.None)
                sortOrder = SortOrder.Ascending;
            else
                InvertSort(ref sortOrder);

            if (newOrder.HasValue)
                sortOrder = newOrder.Value;

            List<Issue> issueList = (List<Issue>)DataGridViewIssues.DataSource;
            issueList.Sort(new CompareIssue(sortColumn.Name, sortOrder));
            sortColumn.HeaderCell.SortGlyphDirection = sortOrder;
            currentSortedColumn = sortColumn;
            Settings.Default.SetIssueGridSort(sortColumn.Name, sortOrder);
            SetIssueSelectionTo(currentSelectedIssue);
            DataGridViewIssues.Refresh();
        }

        public static void InvertSort(ref System.Windows.Forms.SortOrder order)
        {
            if (order == System.Windows.Forms.SortOrder.None)
                return;
            if (order == System.Windows.Forms.SortOrder.Ascending)
                order = System.Windows.Forms.SortOrder.Descending;
            else
                order = System.Windows.Forms.SortOrder.Ascending;
        }

        private void DataGridViewIssues_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewIssues.ClearSelection();
            DataGridViewIssues.Rows[e.RowIndex].Selected = true;
            DataGridViewIssues_SelectionChanged(null, null);
        }

        #endregion //DataGridViewIssues Functions

        #region DataGridViewIssues Context Menu's
        private void editVisibleColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueGridSelectColumns dlg = new IssueGridSelectColumns();
            if (dlg.ShowDialog(this) == DialogResult.OK)
                UpdateIssueDataColumns();
        }

        private void openIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Issue issue = (Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem;
                ShowIssue(issue);
            }
            catch (Exception)
            {
            }
        }

        private void openIssueInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Issue issue = (Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem;
                System.Diagnostics.Process.Start(RedmineClientForm.RedmineURL + "/issues/" + issue.Id.ToString());
            }
            catch (Exception)
            {
            }
        }
        #endregion //DataGridViewIssues Context Menu's

        #region FilterFunctions
        /// <summary>
        /// if the 'Show Issues only assigned to me' checkbox is clicked, refresh the issues.
        /// this way it will respect the setting of the checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxOnlyMe_Click(object sender, EventArgs e)
        {
            BtnRefreshButton_Click(sender, e);
            UpdateFilterControls();
        }

        private void ComboBoxTracker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.TrackerId = (int)ComboBoxTracker.SelectedValue;
            }
            catch (Exception)
            {
                currentFilter.TrackerId = 0;
            }
            FilterChanged();
        }

        private void ComboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.StatusId = (int)ComboBoxStatus.SelectedValue;
            }
            catch (Exception)
            {
                currentFilter.StatusId = 0;
            }
            FilterChanged();
        }

        private void ComboBoxPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.PriorityId = (int)ComboBoxPriority.SelectedValue;
            }
            catch (Exception)
            {
                currentFilter.PriorityId = 0;
            }
            FilterChanged();
        }

        private void ComboBoxAssignedTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.AssignedToId = (int)ComboBoxAssignedTo.SelectedValue;
            }
            catch (Exception)
            {
                currentFilter.AssignedToId = 0;
            }
            FilterChanged();
        }

        private void ComboBoxTargetVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.VersionId = (int)ComboBoxTargetVersion.SelectedValue;
            }
            catch (Exception)
            {
                currentFilter.VersionId = 0;
            }
            FilterChanged();
        }

        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.CategoryId = (int)ComboBoxCategory.SelectedValue;
            }
            catch (Exception)
            {
                currentFilter.CategoryId = 0;
            }
            FilterChanged();
        }
        private void TextBoxSubject_TextChanged(object sender, EventArgs e)
        {
            try
            {
                currentFilter.Subject = TextBoxSubject.Text;
            }
            catch (Exception)
            {
                currentFilter.Subject = "";
            }
            FilterChanged();
        }

        private void FilterChanged()
        {
            BtnRefreshButton.Text = Lang.BtnRefreshButton_Search;
        }

        private void BtnClearButton_Click(object sender, EventArgs e)
        {
            ComboBoxTracker.SelectedValue = 0;
            ComboBoxStatus.SelectedValue = 0;
            ComboBoxPriority.SelectedValue = 0;
            TextBoxSubject.Text = "";
            ComboBoxAssignedTo.SelectedValue = 0;
            ComboBoxTargetVersion.SelectedValue = 0;
            ComboBoxCategory.SelectedValue = 0;
        }
        #endregion //FilterFunctions

        private void BtnOpenIssueButton_Click(object sender, EventArgs e)
        {
            OpenSpecificIssueForm dlg = new OpenSpecificIssueForm();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Issue issue = new Issue { Id = dlg.IssueNumber };
                ShowIssue(issue);
            }
        }


        Timer refreshIssuesTimer;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (refreshIssuesTimer != null)
                refreshIssuesTimer.Dispose();
            refreshIssuesTimer = new Timer();
            refreshIssuesTimer.Interval = 400;
            refreshIssuesTimer.Enabled = true;
            refreshIssuesTimer.Tick += (object s2, EventArgs e2) =>
            {
                FilterAndFillCurrentIssues();
                refreshIssuesTimer.Stop();
            };
            refreshIssuesTimer.Start();
        }

    }
}
