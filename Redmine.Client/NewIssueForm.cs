using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Redmine.Net.Api.Types;

namespace Redmine.Client
{
    public partial class NewIssueForm : Form
    {
        internal int ProjectId;

        public NewIssueForm()
        {
            InitializeComponent();
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Issue issue = new Issue();
            issue.Project = new IdentifiableName { Id = this.ProjectId };
            issue.AssignedTo = new IdentifiableName { Id = Convert.ToInt32(ComboBoxAssignedTo.SelectedValue) };
            issue.Description = TextBoxDescription.Text;
            
            int time;
            issue.EstimatedHours = Int32.TryParse(TextBoxEstimatedTime.Text, out time) ? time : 0;
            issue.DoneRatio = Convert.ToInt32(numericUpDown1.Value);
            issue.Priority = new IdentifiableName { Id = Convert.ToInt32(ComboBoxPriority.SelectedValue) };
            if (DateStart.Enabled)
            {
                issue.StartDate = DateStart.Value;
            }
            if (DateDue.Enabled)
            {
                issue.DueDate = DateDue.Value;   
            }
            issue.Status = new IdentifiableName { Id = Convert.ToInt32(ComboBoxStatus.SelectedValue) };
            issue.Subject = TextBoxSubject.Text;
            issue.FixedVersion = new IdentifiableName { Id = Convert.ToInt32(ComboBoxTargetVersion.SelectedValue) };
            issue.Tracker = new IdentifiableName { Id = Convert.ToInt32(ComboBoxTracker.SelectedValue) };
            try
            {
                if (issue.Subject != String.Empty)
                {
                    RedmineClientForm.redmine.CreateObject<Issue>(issue);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The issue subject is mandatory.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Creating the issue failed, the server responded: {0}", ex.Message),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NewIssueForm_Load(object sender, EventArgs e)
        {
            cbDueDate.Checked = false;
            cbStartDate.Checked = false;
            DateStart.Enabled = false;
            DateDue.Enabled = false;
            if (RedmineClientForm.DataCache == null)
            {
                this.Cursor = Cursors.AppStarting;
                backgroundWorker2.RunWorkerAsync(ProjectId);
                this.BtnSaveButton.Enabled = false;
            }
            else
            {
                FillForm();   
            }
        }

        private void FillForm()
        {
            this.ComboBoxAssignedTo.DataSource = RedmineClientForm.DataCache.Assignees;
            this.ComboBoxAssignedTo.DisplayMember = "Name";
            this.ComboBoxAssignedTo.ValueMember = "Id";
            this.ComboBoxPriority.DataSource = Enumerations.IssuePriorities;
            this.ComboBoxPriority.DisplayMember = "Name";
            this.ComboBoxPriority.ValueMember = "Id";
            this.ComboBoxStatus.DataSource = RedmineClientForm.DataCache.Statuses;
            this.ComboBoxStatus.DisplayMember = "Name";
            this.ComboBoxStatus.ValueMember = "Id";
            this.ComboBoxTargetVersion.DataSource = RedmineClientForm.DataCache.Versions;
            this.ComboBoxTargetVersion.DisplayMember = "Name";
            this.ComboBoxTargetVersion.ValueMember = "Id";
            this.ComboBoxTracker.DataSource = RedmineClientForm.DataCache.Trackers;
            this.ComboBoxTracker.DisplayMember = "Name";
            this.ComboBoxTracker.ValueMember = "Id";
            this.ListBoxWatchers.DataSource = RedmineClientForm.DataCache.Assignees;
            this.ListBoxWatchers.DisplayMember = "Name";
            this.ListBoxWatchers.ClearSelected();
        }

        private static Assignee MemberToAssignee(ProjectMembership projectMember)
        {
            return new Assignee(projectMember);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            RedmineClientForm.DataCache = new IssueFormData();
            int projectId = (int)e.Argument;
//            RedmineClientForm.DataCache.Priorities = RedmineClientForm.redmine.GetObjectList<Priorities(projectId);
            NameValueCollection parameters = new NameValueCollection { { "project_id", projectId.ToString() } };

            RedmineClientForm.DataCache.Statuses = RedmineClientForm.redmine.GetObjectList<IssueStatus>(parameters);
            RedmineClientForm.DataCache.Trackers = RedmineClientForm.redmine.GetObjectList<Tracker>(parameters);
            RedmineClientForm.DataCache.Versions = (List<Redmine.Net.Api.Types.Version>)RedmineClientForm.redmine.GetObjectList<Redmine.Net.Api.Types.Version>(parameters);
            RedmineClientForm.DataCache.Versions.Insert(0, new Redmine.Net.Api.Types.Version { Id = 0, Name = "" });
            List<ProjectMembership> projectMembers = (List<ProjectMembership>)RedmineClientForm.redmine.GetObjectList<ProjectMembership>(parameters);
            RedmineClientForm.DataCache.Assignees = projectMembers.ConvertAll(new Converter<ProjectMembership, Assignee>(MemberToAssignee));
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            FillForm();
            this.BtnSaveButton.Enabled = true;
        }

        private void cbStartDate_CheckedChanged(object sender, EventArgs e)
        {
            DateStart.Enabled = cbStartDate.Checked;
        }

        private void cbDueDate_CheckedChanged(object sender, EventArgs e)
        {
            DateDue.Enabled = cbDueDate.Checked;
        }


    }
}
