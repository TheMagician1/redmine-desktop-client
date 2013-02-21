using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Redmine.Net.Api.Types;
using Redmine.Client.Languages;
using System.Text.RegularExpressions;

namespace Redmine.Client
{
    public partial class IssueForm : BgWorker
    {
        class ClientCustomField
        {
            public String Name { get; set; }
            public String Value { get; set; }
        };
        public enum DialogType
        {
            New,
            Edit,
        };
        private Project project;
        private int issueId = 0;
        private Issue issue;
        private int projectId;
        private DialogType type;
        private IssueFormData DataCache = null;

        private Label LabelChildren;
        private DataGridView DataGridViewChildren;
        private Label LabelParent;

        private const int ChildrenHeight = 119;
        private const int ParentHeight = 24;

        public IssueForm(Project project)
        {
            this.project = project;
            this.projectId = project.Id;
            this.type = DialogType.New;
            InitializeComponent();
            this.Text = String.Format(Lang.DlgIssueTitleNew, project.Name);
            BtnCloseButton.Visible = false;
            linkEditInRedmine.Visible = false;
            DataGridViewCustomFields.Visible = false;
            LangTools.UpdateControlsForLanguage(this.Controls);
        }

        public IssueForm(Issue issue)
        {
            this.issueId = issue.Id;
            this.projectId = issue.Project.Id;
            this.type = DialogType.Edit;
            InitializeComponent();

            this.Text = String.Format(Lang.DlgIssueTitleEdit, issue.Id, issue.Project.Name);
            LangTools.UpdateControlsForLanguage(this.Controls);

            EnableDisableAllControls(false);
        }

        private void EnableDisableAllControls(bool enable)
        {
            //BtnCancelButton.Enabled = enable;
            BtnSaveButton.Enabled = enable;
            labelTracker.Enabled = enable;
            ComboBoxTracker.Enabled = enable;
            DateStart.Enabled = enable;
            labelSubject.Enabled = enable;
            TextBoxSubject.Enabled = enable;
            labelDescription.Enabled = enable;
            TextBoxDescription.Enabled = enable;
            labelStatus.Enabled = enable;
            ComboBoxStatus.Enabled = enable;
            labelPriority.Enabled = enable;
            ComboBoxPriority.Enabled = enable;
            DateDue.Enabled = enable;
            labelEstimatedTime.Enabled = enable;
            TextBoxEstimatedTime.Enabled = enable;
            labelAssignedTo.Enabled = enable;
            ComboBoxAssignedTo.Enabled = enable;
            labelTargetVersion.Enabled = enable;
            ComboBoxTargetVersion.Enabled = enable;
            numericUpDown1.Enabled = enable;
            labelPercentDone.Enabled = enable;
            cbStartDate.Enabled = enable;
            cbDueDate.Enabled = enable;
            BtnCloseButton.Enabled = enable;
            //linkEditInRedmine.Enabled = enable;
            DataGridViewCustomFields.Enabled = enable;
            BtnViewTimeButton.Enabled = enable;
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Issue issue = new Issue();
            if (type == DialogType.Edit)
                issue.Id = this.issue.Id;
            issue.Project = new IdentifiableName { Id = projectId };
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

                    // resize to screen without children and parents...
                    if (issue.Children != null && issue.Children.Count > 0)
                    {
                        MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - ChildrenHeight);
                        Size = new System.Drawing.Size(Size.Width, Size.Height - ChildrenHeight);
                    }
                    if (issue.ParentIssue != null && issue.ParentIssue.Id != 0)
                    {
                        MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - ParentHeight);
                        Size = new System.Drawing.Size(Size.Width, Size.Height - ParentHeight);
                    }

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
            // resize to screen without children and parents...
            if (issue.Children != null && issue.Children.Count > 0)
            {
                MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - ChildrenHeight);
                Size = new System.Drawing.Size(Size.Width, Size.Height - ChildrenHeight);
            }
            if (issue.ParentIssue != null && issue.ParentIssue.Id != 0)
            {
                MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - ParentHeight);
                Size = new System.Drawing.Size(Size.Width, Size.Height - ParentHeight);
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NewIssueForm_Load(object sender, EventArgs e)
        {
            cbDueDate.Checked = false;
            cbStartDate.Checked = false;
            DateStart.Enabled = false;
            DateDue.Enabled = false;
            if (this.DataCache == null)
            {
                this.Cursor = Cursors.AppStarting;
                RunWorkerAsync(projectId);
                this.BtnSaveButton.Enabled = false;
            }
            else
            {
                FillForm();
            }
        }

        private void FillForm()
        {
            EnableDisableAllControls(true);
            // update title again
            this.Text = String.Format(Lang.DlgIssueTitleEdit, issue.Id, issue.Project.Name);
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x)
                {
                    this.ComboBoxAssignedTo.DataSource = this.DataCache.ProjectMembers;
                    this.ComboBoxAssignedTo.DisplayMember = "Name";
                    this.ComboBoxAssignedTo.ValueMember = "Id";
                }
                else
                    ComboBoxAssignedTo.Enabled = false;
                this.ComboBoxStatus.DataSource = this.DataCache.Statuses;
                this.ComboBoxStatus.DisplayMember = "Name";
                this.ComboBoxStatus.ValueMember = "Id";
                this.ComboBoxTargetVersion.DataSource = this.DataCache.Versions;
                this.ComboBoxTargetVersion.DisplayMember = "Name";
                this.ComboBoxTargetVersion.ValueMember = "Id";
                this.ComboBoxTracker.DataSource = this.DataCache.Trackers;
                this.ComboBoxTracker.DisplayMember = "Name";
                this.ComboBoxTracker.ValueMember = "Id";
                //this.ListBoxWatchers.DataSource = RedmineClientForm.DataCache.Watchers;
                //this.ListBoxWatchers.DisplayMember = "Name";
                //this.ListBoxWatchers.ClearSelected();
            }
            else
            {
                ComboBoxAssignedTo.Enabled = false;
                ComboBoxStatus.Enabled = false;
                ComboBoxTargetVersion.Enabled = false;
                ComboBoxTracker.Enabled = false;
                BtnCloseButton.Visible = false;
            }
            this.ComboBoxPriority.DataSource = Enumerations.IssuePriorities;
            this.ComboBoxPriority.DisplayMember = "Name";
            this.ComboBoxPriority.ValueMember = "Id";

            if (this.type == DialogType.Edit)
            {
                if (issue.AssignedTo != null)
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x)
                    {
                        ComboBoxAssignedTo.SelectedIndex = ComboBoxAssignedTo.FindStringExact(issue.AssignedTo.Name);
                    }
                    else
                    {
                        ComboBoxAssignedTo.Items.Add(issue.AssignedTo);
                        ComboBoxAssignedTo.DisplayMember = "Name";
                        ComboBoxAssignedTo.ValueMember = "Id";
                        ComboBoxAssignedTo.SelectedItem = issue.AssignedTo;
                    }
                }
                if (issue.Description != null)
                    TextBoxDescription.Text = Regex.Replace(issue.Description, "(?<!\r)\n", "\r\n");
                TextBoxEstimatedTime.Text = issue.EstimatedHours.ToString();
                numericUpDown1.Value = Convert.ToDecimal(issue.DoneRatio);
                ComboBoxPriority.SelectedIndex = ComboBoxPriority.FindStringExact(issue.Priority.Name);
                if (issue.StartDate.HasValue)
                    DateStart.Value = issue.StartDate.Value;
                if (issue.DueDate.HasValue)
                    DateDue.Value = issue.DueDate.Value;
                if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                {
                    ComboBoxStatus.SelectedIndex = ComboBoxStatus.FindStringExact(issue.Status.Name);
                    ComboBoxTracker.SelectedIndex = ComboBoxTracker.FindStringExact(issue.Tracker.Name);
                }
                else
                {
                    ComboBoxStatus.Items.Add(issue.Status);
                    ComboBoxStatus.DisplayMember = "Name";
                    ComboBoxStatus.ValueMember = "Id";
                    ComboBoxStatus.SelectedItem = issue.Status;
                    ComboBoxTracker.Items.Add(issue.Tracker);
                    ComboBoxTracker.DisplayMember = "Name";
                    ComboBoxTracker.ValueMember = "Id";
                    ComboBoxTracker.SelectedItem = issue.Tracker;
                }
                TextBoxSubject.Text = issue.Subject;
                if (issue.FixedVersion != null)
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                    {
                        ComboBoxTargetVersion.SelectedIndex = ComboBoxTargetVersion.FindStringExact(issue.FixedVersion.Name);
                    }
                    else
                    {
                        ComboBoxTargetVersion.Items.Add(issue.FixedVersion);
                        ComboBoxTargetVersion.DisplayMember = "Name";
                        ComboBoxTargetVersion.ValueMember = "Id";
                        ComboBoxTargetVersion.SelectedItem = issue.FixedVersion;
                    }
                }
                if (issue.CustomFields != null && issue.CustomFields.Count != 0)
                {
                    List<ClientCustomField> customFields = new List<ClientCustomField>();
                    foreach (CustomField cf in issue.CustomFields)
                    {
                        ClientCustomField field = new ClientCustomField();
                        field.Name = cf.Name;
                        foreach (CustomFieldValue cfv in cf.Values)
                        {
                            if (field.Value == null)
                                field.Value = cfv.Info;
                            else
                                field.Value += ", " + cfv.Info;
                        }
                        customFields.Add(field);
                    }
                    DataGridViewCustomFields.DataSource = customFields;
                    DataGridViewCustomFields.RowHeadersVisible = false;
                    DataGridViewCustomFields.ColumnHeadersVisible = false;
                }
                else
                    DataGridViewCustomFields.Visible = false;

                // if the issue has children, show them.
                if (issue.Children != null && issue.Children.Count > 0)
                {
                    LabelChildren = new Label();
                    LabelChildren.AutoSize = true;
                    LabelChildren.Location = new System.Drawing.Point(TextBoxDescription.Location.X, linkEditInRedmine.Location.Y);
                    LabelChildren.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                    LabelChildren.Name = "LabelChildren";
                    LabelChildren.Size = new System.Drawing.Size(44, 13);
                    LabelChildren.TabIndex = 4;
                    LabelChildren.Text = Lang.LabelChildren;
                    LabelChildren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    Controls.Add(LabelChildren);

                    DataGridViewChildren = new DataGridView();
                    DataGridViewChildren.AllowUserToAddRows = false;
                    DataGridViewChildren.AllowUserToDeleteRows = false;
                    DataGridViewChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DataGridViewChildren.Location = new System.Drawing.Point(TextBoxDescription.Location.X, linkEditInRedmine.Location.Y+19);
                    DataGridViewChildren.MultiSelect = false;
                    DataGridViewChildren.Name = "DataGridViewChildren";
                    DataGridViewChildren.ReadOnly = true;
                    DataGridViewChildren.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                    DataGridViewChildren.Size = new System.Drawing.Size(TextBoxDescription.Width, 88);
                    DataGridViewChildren.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewChildren_CellFormatting);
                    DataGridViewChildren.TabIndex = 26;
                    DataGridViewChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DataGridViewChildren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    Controls.Add(DataGridViewChildren);
                    DataGridViewChildren.DataSource = issue.Children;
                    try // Very ugly trick to fix the mono crash reported in the SF.net forum
                    {
                        DataGridViewChildren.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    catch (Exception) { }
                    if (DataGridViewChildren.Columns.Count > 0)
                    {
                        DataGridViewChildren.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    DataGridViewChildren.RowHeadersWidth = 20;
                    foreach (DataGridViewColumn column in DataGridViewChildren.Columns)
                    {
                        if (column.Name != "Id" && column.Name != "Subject")
                        {
                            column.Visible = false;
                        }
                    }
                    DataGridViewChildren.Columns["Id"].DisplayIndex = 0;
                    DataGridViewChildren.Columns["Subject"].DisplayIndex = 1;
                    SuspendLayout();
                    // first set size, then alter minimum size; otherwise dialog is expanded twice.
                    Size = new System.Drawing.Size(Size.Width, Size.Height + ChildrenHeight);
                    MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height + ChildrenHeight);
                    MoveControl(linkEditInRedmine, 0, ChildrenHeight);
                    MoveControl(BtnCancelButton, 0, ChildrenHeight);
                    MoveControl(BtnCloseButton, 0, ChildrenHeight);
                    MoveControl(BtnSaveButton, 0, ChildrenHeight);
                    ResumeLayout(false);
                }
                if (issue.ParentIssue != null && issue.ParentIssue.Id != 0)
                {
                    LabelParent = new Label();
                    LabelParent.AutoSize = true;
                    System.Drawing.Font defaultFont = (System.Drawing.Font)labelDescription.Font.Clone();
                    LabelParent.Font = new System.Drawing.Font(defaultFont.FontFamily, defaultFont.Size, System.Drawing.FontStyle.Italic, defaultFont.Unit, defaultFont.GdiCharSet);
                    LabelParent.Location = new System.Drawing.Point(TextBoxDescription.Location.X, linkEditInRedmine.Location.Y);
                    LabelParent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                    LabelParent.Name = "LabelParent";
                    LabelParent.Size = new System.Drawing.Size(44, 13);
                    LabelParent.TabIndex = 4;
                    LabelParent.Text = String.Format(Lang.LabelParent, issue.ParentIssue.Id, issue.ParentIssue.Name);
                    LabelParent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    Controls.Add(LabelParent);
                    SuspendLayout();
                    // first set size, then alter minimum size; otherwise dialog is expanded twice.
                    Size = new System.Drawing.Size(Size.Width, Size.Height + ParentHeight);
                    MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height + ParentHeight);
                    MoveControl(linkEditInRedmine, 0, ParentHeight);
                    MoveControl(BtnCancelButton, 0, ParentHeight);
                    MoveControl(BtnCloseButton, 0, ParentHeight);
                    MoveControl(BtnSaveButton, 0, ParentHeight);
                    ResumeLayout(false);
                    if (Size.Width < LabelParent.Width + 30)
                        Size = new System.Drawing.Size(LabelParent.Width + 30, Size.Height);
                    if (MinimumSize.Width < LabelParent.Width + 30)
                        MinimumSize = new System.Drawing.Size(LabelParent.Width + 30, MinimumSize.Height);
                }
            }
        }

        private void MoveControl(Control control, int diffx, int diffy)
        {
            System.Drawing.Point loc = control.Location;
            loc.X += diffx;
            loc.Y += diffy;
            control.Location = loc;
        }

        private static ProjectMember MembershipToMember(ProjectMembership projectMember)
        {
            return new ProjectMember(projectMember);
        }

        private void RunWorkerAsync(int projectId)
        {
            NameValueCollection parameters = new NameValueCollection { { "project_id", projectId.ToString() } };

            AddBgWork(Lang.BgWork_GetIssue, () =>
                {
                    try
                    {
                        IssueFormData dataCache = new IssueFormData();
                        NameValueCollection issueParameters = new NameValueCollection { { "include", "journals,relations,children" } };
                        Issue currentIssue = RedmineClientForm.redmine.GetObject<Issue>(issueId.ToString(), issueParameters);
                        if (currentIssue.ParentIssue != null && currentIssue.ParentIssue.Id != 0)
                        {
                            Issue parentIssue = RedmineClientForm.redmine.GetObject<Issue>(currentIssue.ParentIssue.Id.ToString(), null);
                            currentIssue.ParentIssue.Name = parentIssue.Subject;
                        }
                        if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                        {
                            dataCache.Statuses = RedmineClientForm.redmine.GetTotalObjectList<IssueStatus>(parameters);
                            dataCache.Trackers = RedmineClientForm.redmine.GetTotalObjectList<Tracker>(parameters);
                            dataCache.Versions = (List<Redmine.Net.Api.Types.Version>)RedmineClientForm.redmine.GetTotalObjectList<Redmine.Net.Api.Types.Version>(parameters);
                            dataCache.Versions.Insert(0, new Redmine.Net.Api.Types.Version { Id = 0, Name = "" });
                            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                            {
                                List<ProjectMembership> projectMembers = (List<ProjectMembership>)RedmineClientForm.redmine.GetTotalObjectList<ProjectMembership>(parameters);
                                //RedmineClientForm.DataCache.Watchers = projectMembers.ConvertAll(new Converter<ProjectMembership, Assignee>(MemberToAssignee));
                                dataCache.ProjectMembers = projectMembers.ConvertAll(new Converter<ProjectMembership, ProjectMember>(MembershipToMember));
                                dataCache.ProjectMembers.Insert(0, new ProjectMember(new ProjectMembership { Id = 0, User = new IdentifiableName { Id = 0, Name = "" } }));
                                if (RedmineClientForm.RedmineVersion >= ApiVersion.V22x)
                                {
                                    Enumerations.UpdateIssuePriorities(RedmineClientForm.redmine.GetTotalObjectList<IssuePriority>(null));
                                    Enumerations.SaveIssuePriorities();
                                }
                            }
                        }
                        return () =>
                            {
                                this.issue = currentIssue;
                                this.DataCache = dataCache;
                                FillForm();
                                this.BtnSaveButton.Enabled = true;
                                this.Cursor = Cursors.Default;
                            };
                    }
                    catch (Exception ex)
                    {
                        return () =>
                            {
                                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                                this.Cursor = Cursors.Default;
                            };
                    }
                });
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
            if (Properties.Settings.Default.ClosedStatus == 0)
            {
                MessageBox.Show(Lang.Error_ClosedStatusUnknown, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show(String.Format(Lang.CloseIssueText, issue.Id), Lang.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ComboBoxStatus.SelectedValue = Properties.Settings.Default.ClosedStatus; // ComboBoxStatus.FindStringExact("Closed");
                BtnSaveButton_Click(null, null);
            }
        }

        private void linkEditInRedmine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkEditInRedmine.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(RedmineClientForm.RedmineURL + "/issues/" + issueId.ToString());
        }

        private void BtnViewTimeButton_Click(object sender, EventArgs e)
        {
            try
            {
                TimeEntriesForm dlg = new TimeEntriesForm(issue, DataCache.ProjectMembers);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    //BtnRefreshButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void DataGridViewChildren_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == DataGridViewChildren.Columns["Id"].Index) // Id column
            {
                IssueChild currentIssueChild = (IssueChild)DataGridViewChildren.Rows[e.RowIndex].DataBoundItem;
                e.Value = currentIssueChild.Tracker.Name + " " + currentIssueChild.Id.ToString();
            }
        }

    }
}
