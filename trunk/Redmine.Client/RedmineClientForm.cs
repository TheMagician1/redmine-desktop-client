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

        private Rectangle NormalSize;
        private DateTime MinimizeTime;

        private Dictionary<int, Project> Projects;
        public static ApiVersion RedmineVersion { get; private set; }

        public User CurrentUser { get { return currentUser; } }

        /* ugly hack to create a singleton */
        private static readonly RedmineClientForm instance = new RedmineClientForm();
        public static RedmineClientForm Instance { get { return instance; } }
        private RedmineClientForm()
        {
            InitializeComponent();
            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.Reload();

            timer1.Interval = 1000;

            if (!IsRunningOnMono())
            {
                this.Icon = (Icon)Properties.Resources.ResourceManager.GetObject("clock");
                this.notifyIcon1.Icon = (Icon)Properties.Resources.ResourceManager.GetObject("clock");
                this.notifyIcon1.Visible = true;
            }
			else 
			{
				this.DataGridViewIssues.Click += new System.EventHandler(this.DataGridViewIssues_SelectionChanged);
			}
            this.FormClosing += new FormClosingEventHandler(RedmineClientForm_FormClosing);
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
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
            if (ticks != 0 && !Properties.Settings.Default.PauseTickingOnLock)
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
                        this.ticks = Properties.Settings.Default.TickingTicks;
                        this.UpdateTime();
                        if (Properties.Settings.Default.IsTicking)
                        {
                            if (MessageBox.Show(Lang.Timer_WasTickingWhenClosed, Lang.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                this.StartTimer();
                        }
                    }

                    if (RedmineAuthentication)
                        redmine = new RedmineManager(RedmineURL, RedmineUser, RedminePassword);
                    else
                        redmine = new RedmineManager(RedmineURL);
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

        private MainFormData PrepareFormData(int projectId, bool onlyMe)
        {
            NameValueCollection parameters = new NameValueCollection();
            IList<Project> projects = OnlyProjectsForMember(currentUser, redmine.GetTotalObjectList<Project>(parameters));
            if (projects.Count > 0)
            {
                Projects = MainFormData.ToDictionaryName(projects);

                projectId = GetProjectIdCheckExists(Projects, projectId);
                return new MainFormData(projects, projectId, onlyMe);
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

        private void FillForm(MainFormData data, int issueId, int activityId)
        {
            updating = true;
            this.projectId = GetProjectIdCheckExists(Projects, this.projectId);

            this.BtnSettingsButton.Enabled = true;

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
                
                BtnRefreshButton.Enabled = true;
            }
            else
            {
                BtnCommitButton.Enabled = true;
                BtnRefreshButton.Enabled = true;
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

            DataGridViewIssues.DataSource = data.Issues;
            foreach (DataGridViewColumn column in DataGridViewIssues.Columns)
            {
                if (column.Name != "Id" && column.Name != "Subject")
                    column.Visible = false;
                if (projectId == -1 && column.Name == "Project")
                    column.Visible = true;
            }
            try // Very ugly trick to fix the mono crash reported in the SF.net forum
            {
                DataGridViewIssues.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception) { }
            DataGridViewIssues.RowHeadersWidth = 20;
            DataGridViewIssues.Columns["Id"].DisplayIndex = 0;
            DataGridViewIssues.Columns["Subject"].DisplayIndex = 1;
            DataGridViewIssues.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (projectId == -1)
                DataGridViewIssues.Columns["Project"].DisplayIndex = 2;

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
            updating = false;
            this.Cursor = Cursors.Default;
        }


        String GetSetting(KeyValueConfigurationCollection coll, String name, String defaultVal, bool bEmptyIsDefault = false)
        {
            KeyValueConfigurationElement val = coll[name];
            if (val == null)
                return defaultVal;
            if (bEmptyIsDefault && String.IsNullOrEmpty(val.Value))
                return defaultVal;
            return val.Value;
        }
        Boolean GetSetting(KeyValueConfigurationCollection coll, String name, Boolean defaultVal)
        {
            return Convert.ToBoolean(GetSetting(coll, name, Convert.ToString(defaultVal), true));
        }
        Int32 GetSetting(KeyValueConfigurationCollection coll, String name, Int32 defaultVal)
        {
            return Convert.ToInt32(GetSetting(coll, name, Convert.ToString(defaultVal), true));
        }

        private void SaveRuntimeConfig()
        {
            if (Size != null && WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.PropertyValues["MainWindowSizeX"].PropertyValue = Size.Width;
                Properties.Settings.Default.PropertyValues["MainWindowSizeY"].PropertyValue = Size.Height;
            }
            Properties.Settings.Default.PropertyValues["LastProjectId"].PropertyValue = projectId;
            Properties.Settings.Default.PropertyValues["LastIssueId"].PropertyValue = issueId;
            Properties.Settings.Default.PropertyValues["LastActivityId"].PropertyValue = activityId;
            Properties.Settings.Default.PropertyValues["OnlyAssignedToMe"].PropertyValue = CheckBoxOnlyMe.Checked;
            Properties.Settings.Default.Save();
        }

        private void LoadConfig()
        {
            Enumerations.LoadAll();

            if (Lang.Culture == null)
                Lang.Culture = System.Globalization.CultureInfo.CurrentUICulture;
            Properties.Settings.Default.Reload();
            RedmineURL = Properties.Settings.Default.RedmineURL;
            RedmineAuthentication = Properties.Settings.Default.RedmineAuthentication;
            RedmineUser = Properties.Settings.Default.RedmineUser;
            RedminePassword = Properties.Settings.Default.RedminePassword;
            MinimizeToSystemTray = Properties.Settings.Default.MinimizeToSystemTray;
            MinimizeOnStartTimer = Properties.Settings.Default.MinimizeOnStartTimer;
            CheckForUpdates = Properties.Settings.Default.CheckForUpdates;
            CacheLifetime = Properties.Settings.Default.CacheLifetime;
            PopupInterval = Properties.Settings.Default.PopupInterval;
            RedmineVersion = (ApiVersion)Properties.Settings.Default.ApiVersion;

            int sizeX = Properties.Settings.Default.MainWindowSizeX;
            int sizeY = Properties.Settings.Default.MainWindowSizeY;
            Size FormSize = new Size(
                                      Properties.Settings.Default.MainWindowSizeX,
                                      Properties.Settings.Default.MainWindowSizeY);
            if (FormSize.Height > 0 && FormSize.Width > 0)
                Size = FormSize;

            try
            {
                Languages.Lang.Culture = new System.Globalization.CultureInfo(Properties.Settings.Default.LanguageCode);
            }
            catch (Exception)
            {
                Languages.Lang.Culture = System.Globalization.CultureInfo.CurrentUICulture;
            }

            Languages.LangTools.UpdateControlsForLanguage(this.Controls);
            Languages.LangTools.UpdateControlsForLanguage(NotifyIconMenuStrip.Items);
            SetRestoreToolStripMenuItem();
            UpdateToolStripMenuItemsStartPause();

            projectId = Properties.Settings.Default.LastProjectId;
            issueId = Properties.Settings.Default.LastIssueId;
            activityId = Properties.Settings.Default.LastActivityId;
            CheckBoxOnlyMe.Checked = Properties.Settings.Default.OnlyAssignedToMe;

            BtnNewIssueButton.Visible = RedmineVersion >= ApiVersion.V13x;
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HideRestore();
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

        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideRestore();
        }

        private void BtnExitButton_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
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
            Properties.Settings.Default.SetTickingTick(this.ticking, this.ticks);
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
            Properties.Settings.Default.SetTickingTick(this.ticking, this.ticks);
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

        private void BtnResetButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void UpdateTime()
        {
            this.updating = true;
            this.TextBoxHours.Text = (ticks / 3600 % 60).ToString();
            this.TextBoxMinutes.Text = (ticks / 60 % 60).ToString();
            this.TextBoxSeconds.Text = (ticks % 60).ToString();
            this.updating = false;
        }

        private void UpdateTicks()
        {
            ticks = Convert.ToInt32(TextBoxHours.Text)*3600 + Convert.ToInt32(TextBoxMinutes.Text)*60 +
                    Convert.ToInt32(TextBoxSeconds.Text);
            UpdateTime();
        }

        private void BtnAboutButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void TextBoxSeconds_TextChanged(object sender, EventArgs e)
        {
            if (!CheckNumericValue(TextBoxSeconds.Text, 0, 60))
            {
                MessageBox.Show(Lang.Error_ValueOutOfRange, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();   
                }
            }
        }

        private void TextBoxMinutes_TextChanged(object sender, EventArgs e)
        {
            if (!CheckNumericValue(TextBoxMinutes.Text, 0, 60))
            {
                MessageBox.Show(Lang.Error_ValueOutOfRange, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();
                }
            }
        }

        private void TextBoxHours_TextChanged(object sender, EventArgs e)
        {
            if (!CheckNumericValue(TextBoxHours.Text, 0, 999))
            {
                MessageBox.Show(Lang.Error_ValueOutOfRange, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();
                }
            }
        }

        /// <summary>
        /// Check if the value is numeric and between the provided minimum and maximum
        /// </summary>
        /// <param name="val">string value with numbers</param>
        /// <param name="min">minumum value</param>
        /// <param name="max">maximum value</param>
        /// <returns></returns>
        private static bool CheckNumericValue(string val, int min, int max)
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
                            if (Properties.Settings.Default.ClosedStatus == 0)
                            {
                                MessageBox.Show(Lang.Error_ClosedStatusUnknown, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                try
                                {
                                    UpdateIssueState(selectedIssue, Properties.Settings.Default.ClosedStatus);
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
                FillForm(PrepareFormData(projectId, CheckBoxOnlyMe.Checked), issueId, activityId);
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Cursor = Cursors.Default;
            SetCurrentWorkName("");
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
        /// Get Projects, Issues and Activities and select the current/last selected
        /// </summary>
        /// <param name="projectId">The current/last selected project</param>
        /// <param name="issueId">The current/last selected issue</param>
        /// <param name="activityId">The current/last selected activity</param>
        /// <param name="onlyMe">Retrieve only issues assigned to me</param>
        private void AsyncGetRestOfFormData(int projectId, int issueId, int activityId, bool onlyMe)
        {
            AddBgWork(Lang.BgWork_GetFormData, () =>
            {
                try
                {
                    MainFormData data = PrepareFormData(projectId, onlyMe);
                    
                    //Let main thread fill form data...
                    return () =>
                    {
                        FillForm(data, issueId, activityId);
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
                        AsyncGetRestOfFormData(projectId, issueId, activityId, onlyMe);
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
        /// Create a new issue through the issue dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewIssueButton_Click(object sender, EventArgs e)
        {
            IssueForm dlg = new IssueForm(Projects[projectId]);
            dlg.Size = new Size(Properties.Settings.Default.IssueWindowSizeX,
                                Properties.Settings.Default.IssueWindowSizeY);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BtnRefreshButton_Click(null, null);
            }
            Properties.Settings.Default.PropertyValues["IssueWindowSizeX"].PropertyValue = dlg.Size.Width;
            Properties.Settings.Default.PropertyValues["IssueWindowSizeY"].PropertyValue = dlg.Size.Height;
            Properties.Settings.Default.Save();
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

        /// <summary>
        /// Allow users to double click on the issues and open the issue dialog to display the double clicked issue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewIssues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Issue issue = (Issue)DataGridViewIssues.Rows[e.RowIndex].DataBoundItem;
            try
            {
                IssueForm dlg = new IssueForm(issue);
                dlg.Size = new Size(Properties.Settings.Default.IssueWindowSizeX,
                                    Properties.Settings.Default.IssueWindowSizeY);
                dlg.Show(this);
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
            Properties.Settings.Default.PropertyValues["IssueWindowSizeX"].PropertyValue = currentWindowSize.Width;
            Properties.Settings.Default.PropertyValues["IssueWindowSizeY"].PropertyValue = currentWindowSize.Height;
            Properties.Settings.Default.Save();
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
        /// if the 'Show Issues only assigned to me' checkbox is clicked, refresh the issues.
        /// this way it will respect the setting of the checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxOnlyMe_Click(object sender, EventArgs e)
        {
            BtnRefreshButton_Click(sender, e);
        }

        /// <summary>
        /// Not used at the moment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// When the system is locked, check the setting and if requested, stop the timer.
        /// Also start the timer again on unlock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (Properties.Settings.Default.PauseTickingOnLock)
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
            if (!Properties.Settings.Default.UpdateIssueIfNew)
                return;

            if (DataGridViewIssues.SelectedRows.Count != 1)
                return;

            if (Properties.Settings.Default.NewStatus == 0 || Properties.Settings.Default.InProgressStatus == 0)
            {
                MessageBox.Show(Lang.Error_NewOrInProgressStatusUnknown, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            try
            {
                Issue selectedIssue = (Issue)DataGridViewIssues.SelectedRows[0].DataBoundItem;
                if (selectedIssue.Status.Id == Properties.Settings.Default.NewStatus)
                {
                    if (UpdateIssueState(selectedIssue, Properties.Settings.Default.InProgressStatus))
                        MessageBox.Show(String.Format(Lang.IssueUpdatedToInProgress, selectedIssue.Subject, Properties.Settings.Default.InProgressStatus), Lang.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (issue.Status.Id == idState)
                return false;

            Dictionary<int, IssueStatus> statusDict = MainFormData.ToDictionaryName<IssueStatus>(redmine.GetObjectList<IssueStatus>(null));
            IssueStatus newStatus;
            if (!statusDict.TryGetValue(Properties.Settings.Default.InProgressStatus, out newStatus))
                throw new Exception(Lang.Error_ClosedStatusUnknown);

            issue.Status = new IdentifiableName { Id = newStatus.Id, Name = newStatus.Name };
            if (Properties.Settings.Default.AddNoteOnChangeStatus)
            {
                UpdateIssueNoteForm dlg = new UpdateIssueNoteForm(originalIssue, issue);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    issue.Notes = dlg.Note;
                    RedmineClientForm.redmine.UpdateObject<Issue>(issue.Id.ToString(), issue);
                }
                else
                    return false;
            }
            else
                RedmineClientForm.redmine.UpdateObject<Issue>(issue.Id.ToString(), issue);
            return true;
        }

        private void DataGridViewIssues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == DataGridViewIssues.Columns["Id"].Index) // Id column
            {
                Issue currentIssue = (Issue)DataGridViewIssues.Rows[e.RowIndex].DataBoundItem;
                e.Value = currentIssue.Tracker.Name + " " + currentIssue.Id.ToString();
            }
            if (e.ColumnIndex == DataGridViewIssues.Columns["Project"].Index)
            {
                Issue currentIssue = (Issue)DataGridViewIssues.Rows[e.RowIndex].DataBoundItem;
                e.Value = currentIssue.Project.Name;
            }
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnStartButton_Click(sender, e);
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnStartButton_Click(sender, e);
        }

    }
}
