using System;
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
    delegate void OnDone();
    delegate OnDone RunAsync();

    public partial class RedmineClientForm : Form
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

        public RedmineClientForm()
        {
            InitializeComponent();
            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.Reload();

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
            if (ticks != 0)
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

        void Reinit(bool saveRuntime = true)
        {
            if (saveRuntime)
                SaveRuntimeConfig();
            bool bRetry = false;
            do
            {
                try
                {
                    LoadConfig();

                    if (RedmineAuthentication)
                        redmine = new RedmineManager(RedmineURL, RedmineUser, RedminePassword);
                    else
                        redmine = new RedmineManager(RedmineURL);
                    this.Cursor = Cursors.AppStarting;

                    AsyncGetFormData(projectId, issueId, activityId);
                }
                catch (Exception e)
                {
                    if (OnInitFailed(e))
                        bRetry = true;
                }
            } while (bRetry);
        }

        private MainFormData PrepareFormData(int projectId)
        {
            NameValueCollection parameters = new NameValueCollection();
            IList<Project> projects = redmine.GetTotalObjectList<Project>(parameters);
            if (projects.Count > 0)
            {
                if (projectId == 0)
                {
                    projectId = projects[0].Id;
                }
                Projects = MainFormData.ToDictionaryName(projects);
                return new MainFormData(projectId) { Projects = projects };
            }
            throw new Exception(Lang.Error_NoProjectsFound);
        }

        private void FillForm(MainFormData data, int issueId, int activityId)
        {
            updating = true;
            this.BtnSettingsButton.Enabled = true;

            if (data.Projects.Count == 0 || data.Issues.Count == 0)
            {
                BtnCommitButton.Enabled = false;
                if (data.Projects.Count > 0)
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
                BtnNewIssueButton.Enabled = true;
                BtnRefreshButton.Enabled = true;
            }
            ComboBoxProject.DataSource = data.Projects;
            ComboBoxProject.ValueMember = "Id";
            ComboBoxProject.DisplayMember = "Name";
            
            ComboBoxActivity.DataSource = Enumerations.Activities;
            ComboBoxActivity.DisplayMember = "Name";
            ComboBoxActivity.ValueMember = "Id";

            DataGridViewIssues.DataSource = data.Issues;
            foreach (DataGridViewColumn column in DataGridViewIssues.Columns)
            {
                if (column.Name != "Id" && column.Name != "Subject")
                {
                    column.Visible = false;
                }
            }
            try // Very ugly trick to fix the mono crash reported in the SF.net forum
            {
                DataGridViewIssues.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception) { }
            if (DataGridViewIssues.Columns.Count > 0)
            {
                DataGridViewIssues.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;    
            }
            DataGridViewIssues.RowHeadersWidth = 20;
            DataGridViewIssues.Columns["Id"].DisplayIndex = 0;
            DataGridViewIssues.Columns["Subject"].DisplayIndex = 1;

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

            projectId = Properties.Settings.Default.LastProjectId;
            issueId = Properties.Settings.Default.LastIssueId;
            activityId = Properties.Settings.Default.LastActivityId;

            BtnNewIssueButton.Visible = RedmineVersion >= ApiVersion.V13x;
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
            timer1.Interval = 1000;
            if (ticking)
            {
                timer1.Stop();
                BtnStartButton.Text = Lang.BtnStartButton;
            }
            else
            {
                timer1.Start();
                BtnStartButton.Text = Lang.BtnStartButton_Pause;
                if (MinimizeOnStartTimer)
                    Minimize();
            }
            ticking = !ticking;
            UpdateNotifyIconText();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ticks++;
            this.UpdateTime();
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

        private void DataGridViewIssues_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewIssues.SelectedRows.Count == 0 || !Int32.TryParse(DataGridViewIssues.SelectedRows[0].Cells["Id"].Value.ToString(), out issueId))
            {
                issueId = 0;
            }
            UpdateNotifyIconText();
        }

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
                            try
                            {
                                Issue issue = selectedIssue;
                                issue.Status = new IdentifiableName { Name = "Closed" };
                                RedmineClientForm.redmine.UpdateObject<Issue>(issue.Id.ToString(), issue);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(String.Format(Lang.Error_UpdateIssueFailed, ex.Message),
                                            Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            BtnRefreshButton_Click(null, null);
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

        private void ComboBoxActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(ComboBoxActivity.SelectedValue.ToString(), out activityId))
            {
                activityId = 0;
            }
            UpdateNotifyIconText();
        }

        private void ComboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                LoadLastIds();
                this.Cursor = Cursors.AppStarting;

                FillForm(PrepareFormData(projectId), issueId, activityId);
            }
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private void BtnRefreshButton_Click(object sender, EventArgs e)
        {
            LoadLastIds();
            this.Cursor = Cursors.AppStarting;
            try
            {
                FillForm(PrepareFormData(projectId), issueId, activityId);
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Cursor = Cursors.Default;
        }

        private bool ShowSettingsForm()
        {
            SettingsForm dlg = new SettingsForm();
            return dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK;
        }

        private void BtnSettingsButton_Click(object sender, EventArgs e)
        {
            SaveRuntimeConfig();
            if (!ShowSettingsForm())
                return; //User canceled.

            Reinit();
        }

        private void AsyncGetRestOfFormData(int projectId, int issueId, int activityId)
        {
            AddBgWork(Lang.BgWork_GetFormData, () =>
            {
                try
                {
                    MainFormData data = PrepareFormData(projectId);
                    
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


        private void AsyncGetFormData(int projectId, int issueId, int activityId)
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
                    User newCurrentUser = redmine.GetCurrentUser();
                    return () =>
                    {
                        currentUser = newCurrentUser;
                        if (currentUser != null)
                            SetTitle(String.Format(Lang.RedmineClientTitle_User, currentUser.FirstName, currentUser.LastName));
                        else
                            SetTitle(Lang.RedmineClientTitle_NoUser);
                        //When done, get the rest of the form data...
                        AsyncGetRestOfFormData(projectId, issueId, activityId);
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


        private void BtnNewIssueButton_Click(object sender, EventArgs e)
        {
            IssueForm dlg = new IssueForm(Projects[projectId]);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BtnRefreshButton_Click(null, null);
            }
        }

        void SetTitle(String title)
        {
            Title = title;
            UpdateTitle();
        }

        void UpdateTitle()
        {
            String title = Title;
            if (m_WorkQueue.Count > 0)
                title += " - " + m_WorkQueue.Peek().m_name + "...";
            this.Text = title;
        }

        Queue<BgWork> m_WorkQueue = new Queue<BgWork>();

        private void AddBgWork(String name, RunAsync work)
        {
            m_WorkQueue.Enqueue(new BgWork(name, work));
            TriggerWork();
        }

        void TriggerWork(bool bForce = false)
        {
            if(m_WorkQueue.Count == 0)
                return; //No work
            if(!bForce && m_WorkQueue.Count != 1)
                return; //Already busy...

            backgroundWorker2.RunWorkerAsync(m_WorkQueue.Peek().m_work);
            UpdateTitle();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null)
                e.Result = ((RunAsync)e.Argument)();
        }

        private void backgroundWorker2_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                ((OnDone)e.Result)();
            m_WorkQueue.Dequeue();
            UpdateTitle();
            TriggerWork(true);
        }

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

        private void DataGridViewIssues_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Issue issue = (Issue)DataGridViewIssues.Rows[e.RowIndex].DataBoundItem;
            try
            {
                IssueForm dlg = new IssueForm(issue);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    BtnRefreshButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            LoadLastIds();
            SaveRuntimeConfig();
        }

    }
}
