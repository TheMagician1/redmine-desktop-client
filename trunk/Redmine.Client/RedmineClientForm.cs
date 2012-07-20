using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    public partial class RedmineClientForm : Form
    {
        internal static IssueFormData DataCache = null;
        private int ticks = 0;
        private bool ticking = false;
        private int issueId = 0;
        internal int projectId = 0;
        private int activityId = 0;
        internal static RedmineManager redmine;
        private bool updating = false;
        private User currentUser = null;

        private string RedmineURL;
        private bool RedmineAuthentication;
        private string RedmineUser;
        private string RedminePassword;
        private bool MinimizeToSystemTray;

        private bool CheckForUpdates;
        private int CacheLifetime;

        private Rectangle NormalSize;

        public RedmineClientForm()
        {
            InitializeComponent();
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
            LoadConfig();
            if (this.MinimizeToSystemTray)
                this.RestoreToolStripMenuItem.Text = "&Hide";
            else
                this.RestoreToolStripMenuItem.Text = "Mi&nimize";

            if (RedmineAuthentication)
                redmine = new RedmineManager(RedmineURL, RedmineUser, RedminePassword);
            else
                redmine = new RedmineManager(RedmineURL);
            this.Cursor = Cursors.AppStarting;
            this.BtnCommitButton.Enabled = false;
            this.BtnRefreshButton.Enabled = false;
            this.BtnNewIssueButton.Enabled = false;
            try
            {
                currentUser = redmine.GetCurrentUser();
                if (this.CheckForUpdates)
                {
                    backgroundWorker2.RunWorkerAsync();
                }
                backgroundWorker1.RunWorkerAsync(0);
            }
            catch (System.Net.WebException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
       }

        private MainFormData PrepareFormData(int projectId)
        {
            NameValueCollection parameters = new NameValueCollection();
            IList<Project> projects = redmine.GetObjectList<Project>(parameters);
            if (projects.Count > 0)
            {
                if (projectId == 0)
                {
                    projectId = projects[0].Id;
                }
                this.projectId = projectId;
                NameValueCollection curProject = new NameValueCollection { { "project_id", projectId.ToString() } };
                return new MainFormData() { Issues = redmine.GetObjectList<Issue>(curProject), Members = redmine.GetObjectList<ProjectMembership>(curProject), Projects = projects };
            }
            throw new Exception("No projects found in Redmine.");
        }

        private static IIssue IssueToIIssue(Issue issue)
        {
            return new CIssue(issue);
        }

        private void FillForm(MainFormData data, Enumerations enums)
        {
            updating = true;
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
            
            ComboBoxActivity.DataSource = enums.Activities;
            ComboBoxActivity.DisplayMember = "Name";
            ComboBoxActivity.ValueMember = "Id";

            DataGridViewIssues.DataSource = data.Issues;//.ConvertAll(new Converter<Issue, IIssue>(IssueToIIssue));
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

            if (ComboBoxProject.Items.Count > 0)
            {
                if (projectId != 0)
                {
                    ComboBoxProject.SelectedValue = projectId;
                }
                else
                {
                    ComboBoxProject.SelectedIndex = 0;   
                }
                if ((int)ComboBoxProject.SelectedValue == 0)
                {
                    projectId = 0;
                }   
            }
            if (ComboBoxActivity.Items.Count > 0)
            {
                ComboBoxActivity.SelectedIndex = 0;
                if (!Int32.TryParse(ComboBoxActivity.SelectedValue.ToString(), out activityId))
                {
                    activityId = 0;
                } 
            }
            if (DataGridViewIssues.Rows.Count > 0)
            {
                DataGridViewIssues.Rows[0].Selected = true;
                DataGridViewIssues_SelectionChanged(null, null);
            }
            updating = false;
            this.Cursor = Cursors.Default;
        }

        private void LoadConfig()
        {
            Configuration roamingConf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = roamingConf.FilePath;
            Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            if (conf.AppSettings.Settings.Count == 0)
            {
                conf.AppSettings.Settings.Add("RedmineURL", ConfigurationManager.AppSettings["RedmineURL"]);
                conf.AppSettings.Settings.Add("RedmineAuthentication", ConfigurationManager.AppSettings["RedmineAuthentication"]);
                conf.AppSettings.Settings.Add("RedmineUser", ConfigurationManager.AppSettings["RedmineUser"]);
                conf.AppSettings.Settings.Add("RedminePassword", ConfigurationManager.AppSettings["RedminePassword"]);
                conf.AppSettings.Settings.Add("CheckForUpdates", ConfigurationManager.AppSettings["CheckForUpdates"]);
                conf.AppSettings.Settings.Add("MinimizeToSystemTray", ConfigurationManager.AppSettings["MinimizeToSystemTray"]);
                conf.AppSettings.Settings.Add("CacheLifetime", ConfigurationManager.AppSettings["CacheLifetime"]);
                conf.Save(ConfigurationSaveMode.Modified);
            }
            RedmineURL = conf.AppSettings.Settings["RedmineURL"].Value;
            try
            {
                RedmineAuthentication = Convert.ToBoolean(conf.AppSettings.Settings["RedmineAuthentication"].Value);
            }
            catch (Exception)
            {
                RedmineAuthentication = true;
            }
            try
            {
                MinimizeToSystemTray = Convert.ToBoolean(conf.AppSettings.Settings["MinimizeToSystemTray"].Value);
            }
            catch (Exception)
            {
                MinimizeToSystemTray = true;
            }

            RedmineUser = conf.AppSettings.Settings["RedmineUser"].Value;
            RedminePassword = conf.AppSettings.Settings["RedminePassword"].Value;
            try
            {
                CheckForUpdates = Convert.ToBoolean(conf.AppSettings.Settings["CheckForUpdates"].Value);
            }
            catch (Exception)
            {
                CheckForUpdates = true;
            }
            try
            {
                CacheLifetime = Convert.ToInt32(conf.AppSettings.Settings["CacheLifetime"].Value);
            }
            catch (Exception)
            {
                CacheLifetime = 0;
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
                if (MinimizeToSystemTray)
                    Show();
                Bounds = NormalSize;
                WindowState = FormWindowState.Normal;
                if (MinimizeToSystemTray)
                    RestoreToolStripMenuItem.Text = "&Hide";
                else
                    RestoreToolStripMenuItem.Text = "Mi&nimize";
            }
        }

        private void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            NormalSize = this.RestoreBounds;
            if (MinimizeToSystemTray)
                Hide();
            RestoreToolStripMenuItem.Text = "&Restore";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Minimize();
            }
            else
            {
                if (MinimizeToSystemTray)
                    RestoreToolStripMenuItem.Text = "&Hide";
                else
                    RestoreToolStripMenuItem.Text = "Mi&nimize";
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

        private void BtnPauseButton_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (ticking)
            {
                timer1.Stop();
                BtnPauseButton.Text = "Start";
            }
            else
            {
                timer1.Start();
                BtnPauseButton.Text = "Pause";
            }
            ticking = !ticking;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ticks++;
            this.UpdateTime();
        }

        private void ResetForm()
        {
            this.ticks = 0;
            this.UpdateTime();
            this.dateTimePicker1.Value = DateTime.Now;
            this.TextBoxComment.Text = String.Empty;
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
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void BtnCommitButton_Click(object sender, EventArgs e)
        {
            bool shouldIRestart = ticking;
            if (issueId == 0)
            {
                if (DataGridViewIssues.SelectedRows.Count == 1)
                    Int32.TryParse(DataGridViewIssues.SelectedRows[0].Cells["Id"].Value.ToString(), out issueId);
            }
            if (projectId != 0 && issueId != 0 && activityId != 0 && ticks != 0 )
            {
                ticking = false;
                timer1.Stop();
                BtnPauseButton.Text = "Start";
                if (MessageBox.Show(String.Format("Do you really want to commit the following entry: {6} Project: {0}, Activity: {1}, Issue: {2}, Date: {3}, Comment: {4}, Time: {5}", 
                    projectId, activityId, issueId, dateTimePicker1.Value.ToString("yyyy-MM-dd"), TextBoxComment.Text, String.Format("{0:0.##}", (double)ticks / 3600), Environment.NewLine), 
                    "Ready to commit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    TimeEntry entry = new TimeEntry();
                    entry.Activity = new IdentifiableName { Id = activityId };
                    entry.Comments = TextBoxComment.Text;
                    entry.Hours = (decimal)ticks/3600;
                    entry.Issue = new IdentifiableName { Id = issueId };
                    entry.Project = new IdentifiableName { Id = projectId };
                    entry.SpentOn = dateTimePicker1.Value;
                    entry.User = new IdentifiableName { Id = currentUser.Id };

                    redmine.CreateObject(entry);

                    ResetForm();
                    MessageBox.Show("Work logged successfully ", "Work logged", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else if (shouldIRestart)
                {
                    ticking = true;
                    timer1.Start();
                    BtnPauseButton.Text = "Pause";
                }
            }
            else
            {
                if (ticks == 0)
                {
                    MessageBox.Show("There is no time to log...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                }
                else
				{
                    MessageBox.Show("Some mandatory information is missing...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
            }
        }

        private void ComboBoxActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(ComboBoxActivity.SelectedValue.ToString(), out activityId))
            {
                activityId = 0;
            }
        }

        private void ComboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                DataCache = null;
                int reselect = ComboBoxProject.SelectedIndex;
                this.Cursor = Cursors.AppStarting;
                if ((int)ComboBoxProject.SelectedValue == 0)
                    projectId = 0;
                else
                    projectId = (int)ComboBoxProject.SelectedValue;

                FillForm(PrepareFormData(projectId), new Enumerations());
                updating = true;
                ComboBoxProject.SelectedIndex = reselect;
                updating = false;
            }
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private void BtnRefreshButton_Click(object sender, EventArgs e)
        {
            //redmine.InvalidateCache();
            this.Cursor = Cursors.AppStarting;
            int reselect = ComboBoxProject.SelectedIndex;
            if (ComboBoxProject.SelectedValue != null)
            {
                if ((int)ComboBoxProject.SelectedValue == 0)
                    projectId = 0;
                else
                    projectId = (int)ComboBoxProject.SelectedValue;
            }
            else
            {
                projectId = 0;
            }
            try
            {
                FillForm(PrepareFormData(projectId), new Enumerations());
                ComboBoxProject.SelectedIndex = reselect;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnSettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm dlg = new SettingsForm();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                LoadConfig();
                this.Cursor = Cursors.AppStarting;
                if (RedmineAuthentication)
                    redmine = new RedmineManager(RedmineURL, RedmineUser, RedminePassword);
                else
                    redmine = new RedmineManager(RedmineURL);
                try
                {
                    currentUser = redmine.GetCurrentUser();
                    FillForm(PrepareFormData(0), new Enumerations());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            MainFormData data;
            try
            {
                data = PrepareFormData((int)e.Argument);
                e.Result = data;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                MessageBox.Show(((Exception) e.Result).Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
				FillForm((MainFormData)e.Result, new Enumerations());
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnNewIssueButton_Click(object sender, EventArgs e)
        {
            NewIssueForm dlg = new NewIssueForm();
            dlg.ProjectId = projectId;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BtnRefreshButton_Click(null, null);
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            string latestVersionUrl = Utility.CheckForUpdate();
            if (latestVersionUrl != String.Empty)
            {
                if (MessageBox.Show("New version available. Do you want me to take you to the download location?",
                                    "New version", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(latestVersionUrl);
                }
            }
        }
    }
}
