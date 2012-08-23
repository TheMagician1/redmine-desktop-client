using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Redmine.Net.Api.Types;
using Redmine.Client.Languages;

namespace Redmine.Client
{
    public partial class IssueForm : Form
    {
        public enum DialogType {
            New,
            Edit,
        };
        private Project project;
        private Issue issue;
        private int ProjectId { get { if (this.type == DialogType.New) return project.Id; else return issue.Project.Id; } }
        private DialogType type;

        public IssueForm(Project project)
        {
            this.project = project;
            this.type = DialogType.New;
            InitializeComponent();
            this.Text = String.Format(Lang.DlgIssueTitleNew, project.Name);
            BtnCloseButton.Visible = false;
            linkEditInRedmine.Visible = false;
            LangTools.UpdateControlsForLanguage(this.Controls);
        }

        public IssueForm(Issue issue)
        {
            this.issue = issue;
            this.type = DialogType.Edit;
            InitializeComponent();

            this.Text = String.Format(Lang.DlgIssueTitleEdit, issue.Id, issue.Project.Name);
            LangTools.UpdateControlsForLanguage(this.Controls);
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Issue issue = new Issue();
            if (type == DialogType.Edit)
                issue.Id = this.issue.Id;
            issue.Project = new IdentifiableName { Id = ProjectId };
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
                    if (type == DialogType.New)
                        RedmineClientForm.redmine.CreateObject<Issue>(issue);
                    else
                        RedmineClientForm.redmine.UpdateObject<Issue>(issue.Id.ToString(), issue);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Lang.Error_IssueSubjectMandatory,
                                Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TextBoxSubject.Focus();
                }
            }
            catch (Exception ex)
            {
                if (type == DialogType.New)
                    MessageBox.Show(String.Format(Lang.Error_CreateIssueFailed, ex.Message),
                                Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(String.Format(Lang.Error_UpdateIssueFailed, ex.Message),
                                Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //this.ListBoxWatchers.DataSource = RedmineClientForm.DataCache.Watchers;
            //this.ListBoxWatchers.DisplayMember = "Name";
            //this.ListBoxWatchers.ClearSelected();

            if (this.type == DialogType.Edit)
            {
                if (issue.AssignedTo != null)
                    ComboBoxAssignedTo.SelectedIndex = ComboBoxAssignedTo.FindStringExact(issue.AssignedTo.Name);
                if (issue.Description != null)
                    TextBoxDescription.Text = issue.Description;
                TextBoxEstimatedTime.Text = issue.EstimatedHours.ToString();
                numericUpDown1.Value = Convert.ToDecimal(issue.DoneRatio);
                ComboBoxPriority.SelectedIndex = ComboBoxPriority.FindStringExact(issue.Priority.Name);
                if (issue.StartDate.HasValue)
                    DateStart.Value = issue.StartDate.Value;
                if (issue.DueDate.HasValue)
                    DateDue.Value = issue.DueDate.Value;
                ComboBoxStatus.SelectedIndex = ComboBoxStatus.FindStringExact(issue.Status.Name);
                TextBoxSubject.Text = issue.Subject;
                if (issue.FixedVersion != null)
                    ComboBoxTargetVersion.SelectedIndex = ComboBoxTargetVersion.FindStringExact(issue.FixedVersion.Name);
                ComboBoxTracker.SelectedIndex = ComboBoxTracker.FindStringExact(issue.Tracker.Name);
            }
        }

        private static Assignee MemberToAssignee(ProjectMembership projectMember)
        {
            return new Assignee(projectMember);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            RedmineClientForm.DataCache = new IssueFormData();
            int projectId = (int)e.Argument;
            NameValueCollection parameters = new NameValueCollection { { "project_id", projectId.ToString() } };

            RedmineClientForm.DataCache.Statuses = RedmineClientForm.redmine.GetObjectList<IssueStatus>(parameters);
            RedmineClientForm.DataCache.Trackers = RedmineClientForm.redmine.GetObjectList<Tracker>(parameters);
            RedmineClientForm.DataCache.Versions = (List<Redmine.Net.Api.Types.Version>)RedmineClientForm.redmine.GetObjectList<Redmine.Net.Api.Types.Version>(parameters);
            RedmineClientForm.DataCache.Versions.Insert(0, new Redmine.Net.Api.Types.Version { Id = 0, Name = "" });
            List<ProjectMembership> projectMembers = (List<ProjectMembership>)RedmineClientForm.redmine.GetObjectList<ProjectMembership>(parameters);
            //RedmineClientForm.DataCache.Watchers = projectMembers.ConvertAll(new Converter<ProjectMembership, Assignee>(MemberToAssignee));
            RedmineClientForm.DataCache.Assignees = projectMembers.ConvertAll(new Converter<ProjectMembership, Assignee>(MemberToAssignee));
            RedmineClientForm.DataCache.Assignees.Insert(0, new Assignee(new ProjectMembership { Id = 0, User = new IdentifiableName { Id = 0, Name = "" } }));
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

        private void BtnCloseButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(Lang.CloseIssueText, issue.Id), Lang.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ComboBoxStatus.SelectedIndex = ComboBoxStatus.FindStringExact("Closed");
                BtnSaveButton_Click(null, null);
            }
        }

        private void linkEditInRedmine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkEditInRedmine.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(RedmineClientForm.RedmineURL + "/issues/" + issue.Id.ToString());
        }


    }
}
