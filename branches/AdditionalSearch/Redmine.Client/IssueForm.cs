using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Redmine.Net.Api.Types;
using Redmine.Client.Languages;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Redmine.Client
{
    public partial class IssueForm : BgWorker
    {
        class ClientCustomField
        {
            public String Name { get; set; }
            public String Value { get; set; }
        };
        class ClientIssueRelation : IssueRelation
        {
            private Issue issueTo;
            public String IssueToSubject { get { return issueTo.Subject; } }
            public IdentifiableName IssueToTracker { get { return issueTo.Tracker; } }
            public IdentifiableName IssueToStatus { get { return issueTo.Status; } }
            public IdentifiableName IssueToProject { get { return issueTo.Project; } }
            public ClientIssueRelation(IssueRelation relation, Issue issueTo)
            {
                this.Id = relation.Id;
                this.IssueId = relation.IssueId;
                this.IssueToId = relation.IssueToId;
                this.Type = relation.Type;
                this.issueTo = issueTo;
            }
        };
        private Project project;
        private int issueId = 0;
        private Issue issue;
        private List<ClientIssueRelation> issueRelations;
        private IdentifiableName projectId;
        private DialogType type;
        private IssueFormData DataCache = null;

        private Label LabelChildren;
        private DataGridView DataGridViewChildren;
        private Label LabelParent;
        private Label LabelRelations;
        private DataGridView DataGridViewRelations;

        private const int ChildrenHeight = 100;
        private const int RelationsHeight = 100;
        private const int ParentHeight = 24;

        public IssueForm(Project project)
        {
            this.project = project;
            this.projectId = new IdentifiableName { Id = project.Id, Name = project.Name } ;
            this.type = DialogType.New;
            InitializeComponent();
            UpdateTitle(null);
            BtnCloseButton.Visible = false;
            linkEditInRedmine.Visible = false;
            DataGridViewCustomFields.Visible = false;
            downloadOpenToolStripMenuItem.Enabled = false;
            LangTools.UpdateControlsForLanguage(this.Controls);
            LangTools.UpdateControlsForLanguage(contextMenuStripAttachments.Items);

            // initialize new objects
            Issue issue = new Issue();
            issue.Attachments = new List<Attachment>();
        }

        public IssueForm(Issue issue)
        {
            this.issueId = issue.Id;
            this.projectId = issue.Project;
            this.type = DialogType.Edit;
            InitializeComponent();

            LangTools.UpdateControlsForLanguage(this.Controls);
            LangTools.UpdateControlsForLanguage(contextMenuStripAttachments.Items);
            UpdateTitle(issue);

            BtnDeleteButton.Visible = false;

            EnableDisableAllControls(false);
        }

        private void UpdateTitle(Issue issue)
        {
            if (type == DialogType.New)
                this.Text = String.Format(Lang.DlgIssueTitleNew, project.Name);
            else
                this.Text = String.Format(Lang.DlgIssueTitleEdit, issue.Id, issue.Project!=null?issue.Project.Name:"");
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
            labelCategory.Enabled = enable;
            ComboBoxCategory.Enabled = enable;
            groupBoxAttachments.Enabled = enable;
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Issue newIssue = (Issue)issue.Clone();
            if (type == DialogType.Edit)
                newIssue.Id = this.issue.Id;
            // first check subject as it is mandatory
            newIssue.Subject = TextBoxSubject.Text;
            if (String.IsNullOrEmpty(newIssue.Subject))
            {
                MessageBox.Show(Lang.Error_IssueSubjectMandatory,
                            Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextBoxSubject.Focus();
                return;
            }

            // set project identification.
            newIssue.Project = projectId;

            // set assigned to
            try {
                if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x)
                {
                    ProjectMember selectedMember = (ProjectMember)ComboBoxAssignedTo.SelectedItem;
                    if (selectedMember.Id != 0)
                        newIssue.AssignedTo = new IdentifiableName { Id = selectedMember.Id, Name = selectedMember.Name };
                    else
                        newIssue.AssignedTo = null;
                }
                else if (type == DialogType.Edit)
                {
                    newIssue.AssignedTo = issue.AssignedTo;
                }
            } catch (Exception) {}

            // set description
            newIssue.Description = TextBoxDescription.Text;

            // set estimated hours
            float time;
            if (float.TryParse(TextBoxEstimatedTime.Text, out time))
                newIssue.EstimatedHours = time;
            else
                newIssue.EstimatedHours = null;

            // set done ratio
            int doneRatio = Convert.ToInt32(numericUpDown1.Value);
            if (doneRatio >= 0)
                newIssue.DoneRatio = doneRatio;
            else
                newIssue.DoneRatio = null;

            // set priority
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                newIssue.Priority = (IdentifiableName)ComboBoxPriority.SelectedItem;
            else
                newIssue.Priority = issue.Priority;

            // set start date
            if (DateStart.Enabled && cbStartDate.Checked)
                newIssue.StartDate = DateStart.Value;
            else
                newIssue.StartDate = null;

            // set due date
            if (DateDue.Enabled && cbDueDate.Checked)
                newIssue.DueDate = DateDue.Value;
            else
                newIssue.DueDate = null;

            // set status
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                IssueStatus status = (IssueStatus)ComboBoxStatus.SelectedItem;
                newIssue.Status = new IdentifiableName { Id = status.Id, Name = status.Name };
            }
            else
                newIssue.Status = issue.Status;

            // set version
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                Redmine.Net.Api.Types.Version version = (Redmine.Net.Api.Types.Version)ComboBoxTargetVersion.SelectedItem;
                if (version.Id != 0)
                    newIssue.FixedVersion = new IdentifiableName { Id = version.Id, Name = version.Name };
                else
                    newIssue.FixedVersion = null;
            }
            else
                newIssue.FixedVersion = issue.FixedVersion;

            // set tracker
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                newIssue.Tracker = (ProjectTracker)ComboBoxTracker.SelectedItem;
            else
                newIssue.Tracker = issue.Tracker;

            // set category
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                IssueCategory category = (IssueCategory)ComboBoxCategory.SelectedItem;
                if (category.Id != 0)
                    newIssue.Category = new IdentifiableName { Id = category.Id, Name = category.Name };
                else
                    newIssue.Category = null;
            }
            else
                newIssue.Category = issue.Category;

            try
            {
                if (type == DialogType.New)
                {
                    if (issue.Attachments.Count >= 0)
                    {
                        // first upload all attachment
                        newIssue.Uploads = new List<Upload>();
                        foreach (var a in issue.Attachments)
                        {
                            byte[] file = File.ReadAllBytes(a.ContentUrl);
                            Upload uploadedFile = RedmineClientForm.redmine.UploadData(file);
                            uploadedFile.FileName = a.FileName;
                            uploadedFile.Description = a.Description;
                            uploadedFile.ContentType = a.ContentType;
                            newIssue.Uploads.Add(uploadedFile);
                        }
                    }
                    RedmineClientForm.redmine.CreateObject<Issue>(newIssue);
                }
                else
                {
                    // ask for additional note...
                    UpdateIssueNoteForm dlg = new UpdateIssueNoteForm(issue, newIssue);
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!String.IsNullOrEmpty(dlg.Note))
                            newIssue.Notes = dlg.Note;
                        RedmineClientForm.redmine.UpdateObject<Issue>(newIssue.Id.ToString(), newIssue);
                    }
                    else
                        return;
                }

                // resize to screen without children and parents...
                SetOriginalSize();

                this.DialogResult = DialogResult.OK;
                if (type == DialogType.Edit)
                    RedmineClientForm.Instance.Invoke(new AsyncCloseForm(RedmineClientForm.Instance.IssueFormClosed), new Object[] { this.DialogResult, Size });
                this.Close();
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
            if (issue != null)
            {
                SetOriginalSize();
            }
            this.DialogResult = DialogResult.Cancel;
            if (type == DialogType.Edit)
                RedmineClientForm.Instance.Invoke(new AsyncCloseForm(RedmineClientForm.Instance.IssueFormClosed), new Object[] { this.DialogResult, Size });
            this.Close();
        }

        private void SetOriginalSize()
        {
            if (DataGridViewChildren != null)
            {
                MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - ChildrenHeight);
                Size = new System.Drawing.Size(Size.Width, Size.Height - ChildrenHeight);
            }
            if (LabelParent != null)
            {
                MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - ParentHeight);
                Size = new System.Drawing.Size(Size.Width, Size.Height - ParentHeight);
            }
            if (DataGridViewRelations != null)
            {
                MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height - RelationsHeight);
                Size = new System.Drawing.Size(Size.Width, Size.Height - RelationsHeight);
            }
        }

        private void NewIssueForm_Load(object sender, EventArgs e)
        {
            cbDueDate.Checked = false;
            cbStartDate.Checked = false;
            DateStart.Enabled = false;
            DateDue.Enabled = false;
            if (this.DataCache == null)
            {
                UpdateDataFromRedmine();
            }
            else
            {
                FillForm();
            }
        }

        private void UpdateDataFromRedmine()
        {
            EnableDisableAllControls(false);
            this.Cursor = Cursors.AppStarting;
            RunWorkerAsync(projectId);
            this.BtnSaveButton.Enabled = false;
        }

        private void FillForm()
        {
            EnableDisableAllControls(true);
            // update title again
            UpdateTitle(issue);
            if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
            {
                if (RedmineClientForm.RedmineVersion >= ApiVersion.V14x)
                {
                    ComboBoxAssignedTo.DataSource = DataCache.ProjectMembers;
                    ComboBoxAssignedTo.DisplayMember = "Name";
                    ComboBoxAssignedTo.ValueMember = "Id";
                }
                else
                    ComboBoxAssignedTo.Enabled = false;
                ComboBoxStatus.DataSource = DataCache.Statuses;
                ComboBoxStatus.DisplayMember = "Name";
                ComboBoxStatus.ValueMember = "Id";

                ComboBoxTargetVersion.DataSource = DataCache.Versions;
                ComboBoxTargetVersion.DisplayMember = "Name";
                ComboBoxTargetVersion.ValueMember = "Id";

                ComboBoxTracker.DataSource = DataCache.Trackers;
                ComboBoxTracker.DisplayMember = "Name";
                ComboBoxTracker.ValueMember = "Id";

                ComboBoxCategory.DataSource = DataCache.Categories;
                ComboBoxCategory.DisplayMember = "Name";
                ComboBoxCategory.ValueMember = "Id";

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
                ComboBoxCategory.Enabled = false;
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
                        ComboBoxAssignedTo.SelectedValue = issue.AssignedTo.Id;
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
                ComboBoxPriority.SelectedValue = issue.Priority.Id;

                cbStartDate.Checked = issue.StartDate.HasValue;
                DateStart.Enabled = cbStartDate.Checked;
                if (issue.StartDate.HasValue)
                    DateStart.Value = issue.StartDate.Value;

                cbDueDate.Checked = issue.DueDate.HasValue;
                DateDue.Enabled = cbDueDate.Checked;
                if (issue.DueDate.HasValue)
                    DateDue.Value = issue.DueDate.Value;

                if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                {
                    ComboBoxStatus.SelectedValue = issue.Status.Id;
                    ComboBoxTracker.SelectedValue = issue.Tracker.Id;
                    if (issue.Category != null)
                        ComboBoxCategory.SelectedValue = issue.Category.Id;
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
                    if (issue.Category != null)
                    {
                        ComboBoxCategory.Items.Add(issue.Category);
                        ComboBoxCategory.DisplayMember = "Name";
                        ComboBoxCategory.ValueMember = "Id";
                        ComboBoxCategory.SelectedItem = issue.Category;
                    }
                }
                TextBoxSubject.Text = issue.Subject;
                if (issue.FixedVersion != null)
                {
                    if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                    {
                        ComboBoxTargetVersion.SelectedValue = issue.FixedVersion.Id;
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
                    try // Very ugly trick to fix the mono crash reported in the SF.net forum
                    {
                        DataGridViewCustomFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    catch (Exception) { }
                    DataGridViewCustomFields.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                    DataGridViewCustomFields.Visible = false;

                if (issue.Attachments != null)
                {
                    dataGridViewAttachments.RowHeadersVisible = false;
                    dataGridViewAttachments.ColumnHeadersVisible = false;
                    AttachAttachements(issue.Attachments);
                }
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
                    DataGridViewChildren.Location = new System.Drawing.Point(TextBoxDescription.Location.X, linkEditInRedmine.Location.Y + 19);
                    DataGridViewChildren.MultiSelect = false;
                    DataGridViewChildren.Name = "DataGridViewChildren";
                    DataGridViewChildren.ReadOnly = true;
                    DataGridViewChildren.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                    DataGridViewChildren.Size = new System.Drawing.Size(TextBoxDescription.Width, 69);
                    DataGridViewChildren.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewChildren_CellFormatting);
                    DataGridViewChildren.TabIndex = 26;
                    DataGridViewChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DataGridViewChildren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    DataGridViewChildren.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewChildren_CellDoubleClick);
                    Controls.Add(DataGridViewChildren);
                    DataGridViewChildren.DataSource = issue.Children;
                    try // Very ugly trick to fix the mono crash reported in the SF.net forum
                    {
                        DataGridViewChildren.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    catch (Exception) { }

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
                    DataGridViewChildren.Columns["Subject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    SuspendLayout();
                    // first set size, then alter minimum size; otherwise dialog is expanded twice.
                    Size = new System.Drawing.Size(Size.Width, Size.Height + ChildrenHeight);
                    MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height + ChildrenHeight);
                    linkEditInRedmine.MoveControl(0, ChildrenHeight);
                    BtnCancelButton.MoveControl(0, ChildrenHeight);
                    BtnCloseButton.MoveControl(0, ChildrenHeight);
                    BtnSaveButton.MoveControl(0, ChildrenHeight);
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
                    linkEditInRedmine.MoveControl(0, ParentHeight);
                    BtnCancelButton.MoveControl(0, ParentHeight);
                    BtnCloseButton.MoveControl(0, ParentHeight);
                    BtnSaveButton.MoveControl(0, ParentHeight);
                    ResumeLayout(false);
                    if (Size.Width < LabelParent.Width + 30)
                        Size = new System.Drawing.Size(LabelParent.Width + 30, Size.Height);
                    if (MinimumSize.Width < LabelParent.Width + 30)
                        MinimumSize = new System.Drawing.Size(LabelParent.Width + 30, MinimumSize.Height);
                }
                // if the issue has relations, show them.
                if (issue.Relations != null && issue.Relations.Count > 0)
                {
                    LabelRelations = new Label();
                    LabelRelations.AutoSize = true;
                    LabelRelations.Location = new System.Drawing.Point(TextBoxDescription.Location.X, linkEditInRedmine.Location.Y);
                    LabelRelations.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                    LabelRelations.Name = "LabelRelations";
                    LabelRelations.Size = new System.Drawing.Size(44, 13);
                    LabelRelations.TabIndex = 4;
                    LabelRelations.Text = Lang.LabelRelations;
                    LabelRelations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    Controls.Add(LabelRelations);

                    DataGridViewRelations = new DataGridView();
                    DataGridViewRelations.AllowUserToAddRows = false;
                    DataGridViewRelations.AllowUserToDeleteRows = false;
                    DataGridViewRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DataGridViewRelations.Location = new System.Drawing.Point(TextBoxDescription.Location.X, linkEditInRedmine.Location.Y + 19);
                    DataGridViewRelations.MultiSelect = false;
                    DataGridViewRelations.Name = "DataGridViewRelations";
                    DataGridViewRelations.ReadOnly = true;
                    DataGridViewRelations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                    DataGridViewRelations.Size = new System.Drawing.Size(TextBoxDescription.Width, 69);
                    DataGridViewRelations.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewRelations_CellFormatting);
                    DataGridViewRelations.TabIndex = 26;
                    DataGridViewRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DataGridViewRelations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    DataGridViewRelations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewRelations_CellDoubleClick);

                    Controls.Add(DataGridViewRelations);
                    DataGridViewRelations.DataSource = issueRelations;
                    try // Very ugly trick to fix the mono crash reported in the SF.net forum
                    {
                        DataGridViewRelations.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    catch (Exception) { }
                    DataGridViewRelations.RowHeadersWidth = 20;
                    foreach (DataGridViewColumn column in DataGridViewRelations.Columns)
                    {
                        if (column.Name != "Id"
                            && column.Name != "IssueToSubject"
                            && column.Name != "Type"
                            && column.Name != "IssueToStatus")
                        {
                            column.Visible = false;
                        }
                    }
                    DataGridViewRelations.Columns["Type"].DisplayIndex = 0;
                    DataGridViewRelations.Columns["Type"].HeaderText = "Relation";
                    DataGridViewRelations.Columns["Id"].DisplayIndex = 1;
                    DataGridViewRelations.Columns["IssueToSubject"].DisplayIndex = 2;
                    DataGridViewRelations.Columns["IssueToSubject"].HeaderText = "Subject";
                    DataGridViewRelations.Columns["IssueToSubject"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    DataGridViewRelations.Columns["IssueToStatus"].DisplayIndex = 3;
                    DataGridViewRelations.Columns["IssueToStatus"].HeaderText = "Status";

                    SuspendLayout();
                    // first set size, then alter minimum size; otherwise dialog is expanded twice.
                    Size = new System.Drawing.Size(Size.Width, Size.Height + RelationsHeight);
                    MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height + RelationsHeight);
                    linkEditInRedmine.MoveControl(0, RelationsHeight);
                    BtnCancelButton.MoveControl(0, RelationsHeight);
                    BtnCloseButton.MoveControl(0, RelationsHeight);
                    BtnSaveButton.MoveControl(0, RelationsHeight);
                    ResumeLayout(false);
                }
            }
            else // type new
            {
                cbStartDate.Checked = true;
                DateStart.Enabled = cbStartDate.Checked;
                DateDue.Enabled = cbDueDate.Checked;
            }
        }

        private void AttachAttachements(IList<Attachment> attachments)
        {
            dataGridViewAttachments.DataSource = null;
            dataGridViewAttachments.DataSource = attachments;
            foreach (DataGridViewColumn column in dataGridViewAttachments.Columns)
            {
                if (column.Name != "FileName"
                    && column.Name != "Description"
                    && column.Name != "Author")
                {
                    column.Visible = false;
                }
            }
            try // Very ugly trick to fix the mono crash reported in the SF.net forum
            {
                dataGridViewAttachments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception) { }
            dataGridViewAttachments.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAttachments.Columns["FileName"].DisplayIndex = 0;
            dataGridViewAttachments.Columns["Description"].DisplayIndex = 1;
            dataGridViewAttachments.Columns["Author"].DisplayIndex = 2;
        }

        private static ProjectMember MembershipToMember(ProjectMembership projectMember)
        {
            return new ProjectMember(projectMember);
        }

        private void RunWorkerAsync(IdentifiableName projectId)
        {
            AddBgWork(Lang.BgWork_GetIssue, () =>
                {
                    try
                    {
                        IssueFormData dataCache = new IssueFormData();
                        List<ClientIssueRelation> currentIssueRelations = new List<ClientIssueRelation>();
                        Issue currentIssue = null;
                        if (type == DialogType.Edit)
                        {
                            NameValueCollection issueParameters = new NameValueCollection { { "include", "journals,relations,children,attachments" } };
                            currentIssue = RedmineClientForm.redmine.GetObject<Issue>(issueId.ToString(), issueParameters);
                            if (currentIssue.ParentIssue != null && currentIssue.ParentIssue.Id != 0)
                            {
                                Issue parentIssue = RedmineClientForm.redmine.GetObject<Issue>(currentIssue.ParentIssue.Id.ToString(CultureInfo.InvariantCulture), null);
                                currentIssue.ParentIssue.Name = parentIssue.Subject;
                            }
                            this.projectId = projectId = currentIssue.Project;
                        }
                        else
                        {
                            // initialize new objects
                            currentIssue = new Issue();
                            currentIssue.Id = 0;
                            currentIssue.Subject = Lang.NewIssue;
                            currentIssue.Attachments = new List<Attachment>();
                        }
                        if (RedmineClientForm.RedmineVersion >= ApiVersion.V13x)
                        {
                            NameValueCollection parameters = new NameValueCollection { { "project_id", projectId.Id.ToString() } };
                            NameValueCollection projectParameters = new NameValueCollection { { "include", "trackers" } };
                            Project project = RedmineClientForm.redmine.GetObject<Project>(projectId.Id.ToString(), projectParameters);
                            dataCache.Trackers = project.Trackers;
                            dataCache.Categories = new List<IssueCategory>(RedmineClientForm.redmine.GetTotalObjectList<IssueCategory>(parameters));
                            dataCache.Categories.Insert(0, new IssueCategory { Id = 0, Name = "" });
                            dataCache.Statuses = RedmineClientForm.redmine.GetTotalObjectList<IssueStatus>(parameters);
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
                            if (currentIssue.Relations != null)
                            {
                                foreach (var r in currentIssue.Relations)
                                {
                                    // swap id's if neccesary
                                    if (r.IssueId != issueId)
                                    {
                                        r.IssueToId = r.IssueId;
                                        r.IssueId = issueId;
                                    }
                                    Issue relatedIssue = RedmineClientForm.redmine.GetObject<Issue>(r.IssueToId.ToString(), null);
                                    currentIssueRelations.Add(new ClientIssueRelation(r, relatedIssue));
                                }
                            }
                        }
                        return () =>
                            {
                                this.issue = currentIssue;
                                this.issueRelations = currentIssueRelations;
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

        private void DataGridViewChildren_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            IssueChild currentIssueChild = (IssueChild)DataGridViewChildren.Rows[e.RowIndex].DataBoundItem;
            RedmineClientForm.ShowIssue(new Issue { Id = currentIssueChild.Id, Subject = currentIssueChild.Subject });
        }

        private void DataGridViewRelations_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == DataGridViewRelations.Columns["Id"].Index) // Id column
            {
                ClientIssueRelation currentIssueRelation = (ClientIssueRelation)DataGridViewRelations.Rows[e.RowIndex].DataBoundItem;
                e.Value = currentIssueRelation.IssueToTracker.Name + " " + currentIssueRelation.IssueToId.ToString();
            }
            if (e.ColumnIndex == DataGridViewRelations.Columns["IssueToStatus"].Index) // Id column
            {
                ClientIssueRelation currentIssueRelation = (ClientIssueRelation)DataGridViewRelations.Rows[e.RowIndex].DataBoundItem;
                e.Value = currentIssueRelation.IssueToStatus.Name;
            }
        }

        private void DataGridViewRelations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClientIssueRelation currentIssueRelation = (ClientIssueRelation)DataGridViewRelations.Rows[e.RowIndex].DataBoundItem;
            RedmineClientForm.ShowIssue(new Issue { Id = currentIssueRelation.IssueToId, Subject = currentIssueRelation.IssueToSubject, Project = currentIssueRelation.IssueToProject } );
        }

        private void dataGridViewAttachments_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (type == DialogType.New)
                return;
            Attachment attachment = (Attachment)dataGridViewAttachments.Rows[e.RowIndex].DataBoundItem;
            System.Diagnostics.Process.Start(attachment.ContentUrl);
        }

        private void dataGridViewAttachments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewAttachments.Columns["FileName"].Index) // Filename
            {
                Attachment attachment = (Attachment)dataGridViewAttachments.Rows[e.RowIndex].DataBoundItem;
                e.Value = attachment.FileName + " (" + attachment.FileSize.ToByteString() + ")";
            }
            if (e.ColumnIndex == dataGridViewAttachments.Columns["Author"].Index) // Author
            {
                Attachment attachment = (Attachment)dataGridViewAttachments.Rows[e.RowIndex].DataBoundItem;
                e.Value = attachment.Author.Name;
            }
        }

        private void BtnAddButton_Click(object sender, EventArgs e)
        {
            AttachmentForm dlg = new AttachmentForm(issue, type, "");
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (type == DialogType.Edit)
                    UpdateDataFromRedmine();
                else
                {
                    issue.Attachments.Add(dlg.NewAttachment);
                    AttachAttachements(issue.Attachments);
                }
            }
        }

        private void dataGridViewAttachments_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void dataGridViewAttachments_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                bool addedAttachment = false;
                foreach (var file in files)
                {
                    AttachmentForm dlg = new AttachmentForm(issue, type, file);
                    if (dlg.ShowDialog(this) == DialogResult.Cancel)
                        break;
                    else
                    {
                        if (type == DialogType.New)
                            issue.Attachments.Add(dlg.NewAttachment);
                    }
                    addedAttachment = true;
                }
                if (addedAttachment)
                {
                    if (type == DialogType.Edit)
                        UpdateDataFromRedmine();
                    else
                        AttachAttachements(issue.Attachments);
                }
            }
        }

        private void dataGridViewAttachments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridViewAttachments.ClearSelection();
                dataGridViewAttachments.Rows[e.RowIndex].Selected = true;
            }
        }

        private void downloadOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (type == DialogType.New)
                return;

            if (dataGridViewAttachments.SelectedRows.Count <= 0)
                return;

            Attachment attachment = (Attachment)dataGridViewAttachments.SelectedRows[0].DataBoundItem;
            System.Diagnostics.Process.Start(attachment.ContentUrl);
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnAddButton_Click(sender, e);
        }

        private void BtnDeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewAttachments.SelectedRows.Count <= 0)
                return;

            Attachment attachment = (Attachment)dataGridViewAttachments.SelectedRows[0].DataBoundItem;
            issue.Attachments.Remove(attachment);
            AttachAttachements(issue.Attachments);
        }

        internal bool ShowingIssue(int issueId)
        {
            return this.issueId == issueId;
        }
    }
}
