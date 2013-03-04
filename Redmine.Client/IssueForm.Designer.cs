namespace Redmine.Client
{
    partial class IssueForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BtnSaveButton = new System.Windows.Forms.Button();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.labelTracker = new System.Windows.Forms.Label();
            this.ComboBoxTracker = new System.Windows.Forms.ComboBox();
            this.DateStart = new System.Windows.Forms.DateTimePicker();
            this.labelSubject = new System.Windows.Forms.Label();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.ComboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.ComboBoxPriority = new System.Windows.Forms.ComboBox();
            this.DateDue = new System.Windows.Forms.DateTimePicker();
            this.labelEstimatedTime = new System.Windows.Forms.Label();
            this.TextBoxEstimatedTime = new System.Windows.Forms.TextBox();
            this.labelAssignedTo = new System.Windows.Forms.Label();
            this.ComboBoxAssignedTo = new System.Windows.Forms.ComboBox();
            this.labelTargetVersion = new System.Windows.Forms.Label();
            this.ComboBoxTargetVersion = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelPercentDone = new System.Windows.Forms.Label();
            this.cbStartDate = new System.Windows.Forms.CheckBox();
            this.cbDueDate = new System.Windows.Forms.CheckBox();
            this.BtnCloseButton = new System.Windows.Forms.Button();
            this.linkEditInRedmine = new System.Windows.Forms.LinkLabel();
            this.DataGridViewCustomFields = new System.Windows.Forms.DataGridView();
            this.BtnViewTimeButton = new System.Windows.Forms.Button();
            this.dataGridViewAttachments = new System.Windows.Forms.DataGridView();
            this.contextMenuStripAttachments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxAttachments = new System.Windows.Forms.GroupBox();
            this.BtnDeleteButton = new System.Windows.Forms.Button();
            this.BtnAddButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCustomFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttachments)).BeginInit();
            this.contextMenuStripAttachments.SuspendLayout();
            this.groupBoxAttachments.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSaveButton
            // 
            this.BtnSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaveButton.Location = new System.Drawing.Point(327, 401);
            this.BtnSaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSaveButton.Name = "BtnSaveButton";
            this.BtnSaveButton.Size = new System.Drawing.Size(68, 24);
            this.BtnSaveButton.TabIndex = 24;
            this.BtnSaveButton.Text = "Save";
            this.BtnSaveButton.UseVisualStyleBackColor = true;
            this.BtnSaveButton.Click += new System.EventHandler(this.BtnSaveButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(399, 401);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 25;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            this.BtnCancelButton.Click += new System.EventHandler(this.BtnCancelButton_Click);
            // 
            // labelTracker
            // 
            this.labelTracker.AutoSize = true;
            this.labelTracker.Location = new System.Drawing.Point(6, 6);
            this.labelTracker.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTracker.Name = "labelTracker";
            this.labelTracker.Size = new System.Drawing.Size(44, 13);
            this.labelTracker.TabIndex = 4;
            this.labelTracker.Text = "Tracker";
            // 
            // ComboBoxTracker
            // 
            this.ComboBoxTracker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTracker.FormattingEnabled = true;
            this.ComboBoxTracker.Location = new System.Drawing.Point(9, 23);
            this.ComboBoxTracker.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxTracker.Name = "ComboBoxTracker";
            this.ComboBoxTracker.Size = new System.Drawing.Size(149, 21);
            this.ComboBoxTracker.TabIndex = 5;
            // 
            // DateStart
            // 
            this.DateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateStart.Location = new System.Drawing.Point(9, 207);
            this.DateStart.Margin = new System.Windows.Forms.Padding(2);
            this.DateStart.Name = "DateStart";
            this.DateStart.Size = new System.Drawing.Size(95, 20);
            this.DateStart.TabIndex = 11;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(6, 46);
            this.labelSubject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(43, 13);
            this.labelSubject.TabIndex = 0;
            this.labelSubject.Text = "Subject";
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSubject.Location = new System.Drawing.Point(9, 61);
            this.TextBoxSubject.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(458, 20);
            this.TextBoxSubject.TabIndex = 1;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(6, 83);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Description";
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.AcceptsReturn = true;
            this.TextBoxDescription.AcceptsTab = true;
            this.TextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDescription.Location = new System.Drawing.Point(9, 96);
            this.TextBoxDescription.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxDescription.Multiline = true;
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxDescription.Size = new System.Drawing.Size(458, 73);
            this.TextBoxDescription.TabIndex = 3;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(160, 6);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Status";
            // 
            // ComboBoxStatus
            // 
            this.ComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStatus.FormattingEnabled = true;
            this.ComboBoxStatus.Location = new System.Drawing.Point(163, 23);
            this.ComboBoxStatus.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxStatus.Name = "ComboBoxStatus";
            this.ComboBoxStatus.Size = new System.Drawing.Size(149, 21);
            this.ComboBoxStatus.TabIndex = 7;
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(315, 6);
            this.labelPriority.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(38, 13);
            this.labelPriority.TabIndex = 8;
            this.labelPriority.Text = "Priority";
            // 
            // ComboBoxPriority
            // 
            this.ComboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPriority.FormattingEnabled = true;
            this.ComboBoxPriority.Location = new System.Drawing.Point(318, 23);
            this.ComboBoxPriority.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxPriority.Name = "ComboBoxPriority";
            this.ComboBoxPriority.Size = new System.Drawing.Size(149, 21);
            this.ComboBoxPriority.TabIndex = 9;
            // 
            // DateDue
            // 
            this.DateDue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DateDue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateDue.Location = new System.Drawing.Point(108, 207);
            this.DateDue.Margin = new System.Windows.Forms.Padding(2);
            this.DateDue.Name = "DateDue";
            this.DateDue.Size = new System.Drawing.Size(93, 20);
            this.DateDue.TabIndex = 13;
            // 
            // labelEstimatedTime
            // 
            this.labelEstimatedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelEstimatedTime.Location = new System.Drawing.Point(202, 174);
            this.labelEstimatedTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEstimatedTime.Name = "labelEstimatedTime";
            this.labelEstimatedTime.Size = new System.Drawing.Size(86, 29);
            this.labelEstimatedTime.TabIndex = 14;
            this.labelEstimatedTime.Text = "Estimated time";
            this.labelEstimatedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxEstimatedTime
            // 
            this.TextBoxEstimatedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TextBoxEstimatedTime.Location = new System.Drawing.Point(205, 206);
            this.TextBoxEstimatedTime.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxEstimatedTime.Name = "TextBoxEstimatedTime";
            this.TextBoxEstimatedTime.Size = new System.Drawing.Size(83, 20);
            this.TextBoxEstimatedTime.TabIndex = 15;
            // 
            // labelAssignedTo
            // 
            this.labelAssignedTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAssignedTo.AutoSize = true;
            this.labelAssignedTo.Location = new System.Drawing.Point(202, 232);
            this.labelAssignedTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAssignedTo.Name = "labelAssignedTo";
            this.labelAssignedTo.Size = new System.Drawing.Size(62, 13);
            this.labelAssignedTo.TabIndex = 16;
            this.labelAssignedTo.Text = "Assigned to";
            // 
            // ComboBoxAssignedTo
            // 
            this.ComboBoxAssignedTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxAssignedTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAssignedTo.FormattingEnabled = true;
            this.ComboBoxAssignedTo.Location = new System.Drawing.Point(205, 251);
            this.ComboBoxAssignedTo.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxAssignedTo.Name = "ComboBoxAssignedTo";
            this.ComboBoxAssignedTo.Size = new System.Drawing.Size(262, 21);
            this.ComboBoxAssignedTo.TabIndex = 17;
            // 
            // labelTargetVersion
            // 
            this.labelTargetVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetVersion.AutoSize = true;
            this.labelTargetVersion.Location = new System.Drawing.Point(202, 275);
            this.labelTargetVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTargetVersion.Name = "labelTargetVersion";
            this.labelTargetVersion.Size = new System.Drawing.Size(75, 13);
            this.labelTargetVersion.TabIndex = 20;
            this.labelTargetVersion.Text = "Target version";
            // 
            // ComboBoxTargetVersion
            // 
            this.ComboBoxTargetVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxTargetVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTargetVersion.FormattingEnabled = true;
            this.ComboBoxTargetVersion.Location = new System.Drawing.Point(205, 290);
            this.ComboBoxTargetVersion.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxTargetVersion.Name = "ComboBoxTargetVersion";
            this.ComboBoxTargetVersion.Size = new System.Drawing.Size(262, 21);
            this.ComboBoxTargetVersion.TabIndex = 21;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(293, 206);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(81, 20);
            this.numericUpDown1.TabIndex = 19;
            // 
            // labelPercentDone
            // 
            this.labelPercentDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPercentDone.Location = new System.Drawing.Point(293, 173);
            this.labelPercentDone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPercentDone.Name = "labelPercentDone";
            this.labelPercentDone.Size = new System.Drawing.Size(81, 30);
            this.labelPercentDone.TabIndex = 18;
            this.labelPercentDone.Text = "% done";
            this.labelPercentDone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbStartDate
            // 
            this.cbStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbStartDate.Location = new System.Drawing.Point(9, 173);
            this.cbStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.cbStartDate.Name = "cbStartDate";
            this.cbStartDate.Size = new System.Drawing.Size(95, 30);
            this.cbStartDate.TabIndex = 10;
            this.cbStartDate.Text = "Start date";
            this.cbStartDate.UseVisualStyleBackColor = true;
            this.cbStartDate.CheckedChanged += new System.EventHandler(this.cbStartDate_CheckedChanged);
            // 
            // cbDueDate
            // 
            this.cbDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDueDate.Location = new System.Drawing.Point(108, 173);
            this.cbDueDate.Margin = new System.Windows.Forms.Padding(2);
            this.cbDueDate.Name = "cbDueDate";
            this.cbDueDate.Size = new System.Drawing.Size(93, 30);
            this.cbDueDate.TabIndex = 12;
            this.cbDueDate.Text = "Due date";
            this.cbDueDate.UseVisualStyleBackColor = true;
            this.cbDueDate.CheckedChanged += new System.EventHandler(this.cbDueDate_CheckedChanged);
            // 
            // BtnCloseButton
            // 
            this.BtnCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCloseButton.Location = new System.Drawing.Point(204, 401);
            this.BtnCloseButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCloseButton.Name = "BtnCloseButton";
            this.BtnCloseButton.Size = new System.Drawing.Size(118, 24);
            this.BtnCloseButton.TabIndex = 26;
            this.BtnCloseButton.Text = "Close Issue";
            this.BtnCloseButton.UseVisualStyleBackColor = true;
            this.BtnCloseButton.Click += new System.EventHandler(this.BtnCloseButton_Click);
            // 
            // linkEditInRedmine
            // 
            this.linkEditInRedmine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkEditInRedmine.AutoSize = true;
            this.linkEditInRedmine.Location = new System.Drawing.Point(6, 407);
            this.linkEditInRedmine.Name = "linkEditInRedmine";
            this.linkEditInRedmine.Size = new System.Drawing.Size(127, 13);
            this.linkEditInRedmine.TabIndex = 23;
            this.linkEditInRedmine.TabStop = true;
            this.linkEditInRedmine.Text = "Edit this issue in Redmine";
            this.linkEditInRedmine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkEditInRedmine_LinkClicked);
            // 
            // DataGridViewCustomFields
            // 
            this.DataGridViewCustomFields.AllowUserToAddRows = false;
            this.DataGridViewCustomFields.AllowUserToDeleteRows = false;
            this.DataGridViewCustomFields.AllowUserToResizeRows = false;
            this.DataGridViewCustomFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewCustomFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewCustomFields.Location = new System.Drawing.Point(9, 232);
            this.DataGridViewCustomFields.MultiSelect = false;
            this.DataGridViewCustomFields.Name = "DataGridViewCustomFields";
            this.DataGridViewCustomFields.ReadOnly = true;
            this.DataGridViewCustomFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCustomFields.Size = new System.Drawing.Size(191, 79);
            this.DataGridViewCustomFields.TabIndex = 22;
            // 
            // BtnViewTimeButton
            // 
            this.BtnViewTimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnViewTimeButton.Location = new System.Drawing.Point(379, 204);
            this.BtnViewTimeButton.Name = "BtnViewTimeButton";
            this.BtnViewTimeButton.Size = new System.Drawing.Size(88, 23);
            this.BtnViewTimeButton.TabIndex = 27;
            this.BtnViewTimeButton.Text = "View Time";
            this.BtnViewTimeButton.UseVisualStyleBackColor = true;
            this.BtnViewTimeButton.Click += new System.EventHandler(this.BtnViewTimeButton_Click);
            // 
            // dataGridViewAttachments
            // 
            this.dataGridViewAttachments.AllowDrop = true;
            this.dataGridViewAttachments.AllowUserToAddRows = false;
            this.dataGridViewAttachments.AllowUserToDeleteRows = false;
            this.dataGridViewAttachments.AllowUserToResizeRows = false;
            this.dataGridViewAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAttachments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttachments.ContextMenuStrip = this.contextMenuStripAttachments;
            this.dataGridViewAttachments.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewAttachments.MultiSelect = false;
            this.dataGridViewAttachments.Name = "dataGridViewAttachments";
            this.dataGridViewAttachments.ReadOnly = true;
            this.dataGridViewAttachments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAttachments.Size = new System.Drawing.Size(371, 54);
            this.dataGridViewAttachments.TabIndex = 22;
            this.dataGridViewAttachments.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAttachments_CellContentDoubleClick);
            this.dataGridViewAttachments.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewAttachments_CellFormatting);
            this.dataGridViewAttachments.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAttachments_CellMouseDown);
            this.dataGridViewAttachments.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewAttachments_DragDrop);
            this.dataGridViewAttachments.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewAttachments_DragEnter);
            // 
            // contextMenuStripAttachments
            // 
            this.contextMenuStripAttachments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadOpenToolStripMenuItem,
            this.addNewToolStripMenuItem});
            this.contextMenuStripAttachments.Name = "contextMenuStripAttachments";
            this.contextMenuStripAttachments.Size = new System.Drawing.Size(169, 48);
            // 
            // downloadOpenToolStripMenuItem
            // 
            this.downloadOpenToolStripMenuItem.Name = "downloadOpenToolStripMenuItem";
            this.downloadOpenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.downloadOpenToolStripMenuItem.Text = "Download / Open";
            this.downloadOpenToolStripMenuItem.Click += new System.EventHandler(this.downloadOpenToolStripMenuItem_Click);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // groupBoxAttachments
            // 
            this.groupBoxAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAttachments.Controls.Add(this.BtnDeleteButton);
            this.groupBoxAttachments.Controls.Add(this.BtnAddButton);
            this.groupBoxAttachments.Controls.Add(this.dataGridViewAttachments);
            this.groupBoxAttachments.Location = new System.Drawing.Point(9, 317);
            this.groupBoxAttachments.Name = "groupBoxAttachments";
            this.groupBoxAttachments.Size = new System.Drawing.Size(457, 79);
            this.groupBoxAttachments.TabIndex = 28;
            this.groupBoxAttachments.TabStop = false;
            this.groupBoxAttachments.Text = "Attachments";
            // 
            // BtnDeleteButton
            // 
            this.BtnDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDeleteButton.Location = new System.Drawing.Point(383, 49);
            this.BtnDeleteButton.Name = "BtnDeleteButton";
            this.BtnDeleteButton.Size = new System.Drawing.Size(68, 24);
            this.BtnDeleteButton.TabIndex = 24;
            this.BtnDeleteButton.Text = "Delete";
            this.BtnDeleteButton.UseVisualStyleBackColor = true;
            this.BtnDeleteButton.Click += new System.EventHandler(this.BtnDeleteButton_Click);
            // 
            // BtnAddButton
            // 
            this.BtnAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddButton.Location = new System.Drawing.Point(383, 19);
            this.BtnAddButton.Name = "BtnAddButton";
            this.BtnAddButton.Size = new System.Drawing.Size(68, 24);
            this.BtnAddButton.TabIndex = 23;
            this.BtnAddButton.Text = "Add";
            this.BtnAddButton.UseVisualStyleBackColor = true;
            this.BtnAddButton.Click += new System.EventHandler(this.BtnAddButton_Click);
            // 
            // IssueForm
            // 
            this.AcceptButton = this.BtnSaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(476, 432);
            this.Controls.Add(this.groupBoxAttachments);
            this.Controls.Add(this.BtnViewTimeButton);
            this.Controls.Add(this.DataGridViewCustomFields);
            this.Controls.Add(this.linkEditInRedmine);
            this.Controls.Add(this.DateDue);
            this.Controls.Add(this.DateStart);
            this.Controls.Add(this.cbDueDate);
            this.Controls.Add(this.cbStartDate);
            this.Controls.Add(this.labelPercentDone);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.labelTargetVersion);
            this.Controls.Add(this.ComboBoxTargetVersion);
            this.Controls.Add(this.labelAssignedTo);
            this.Controls.Add(this.ComboBoxAssignedTo);
            this.Controls.Add(this.labelEstimatedTime);
            this.Controls.Add(this.TextBoxEstimatedTime);
            this.Controls.Add(this.labelPriority);
            this.Controls.Add(this.ComboBoxPriority);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.ComboBoxStatus);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.TextBoxDescription);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.TextBoxSubject);
            this.Controls.Add(this.labelTracker);
            this.Controls.Add(this.ComboBoxTracker);
            this.Controls.Add(this.BtnCloseButton);
            this.Controls.Add(this.BtnSaveButton);
            this.Controls.Add(this.BtnCancelButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(492, 470);
            this.Name = "IssueForm";
            this.ShowIcon = false;
            this.Text = "Create new issue";
            this.Load += new System.EventHandler(this.NewIssueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCustomFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttachments)).EndInit();
            this.contextMenuStripAttachments.ResumeLayout(false);
            this.groupBoxAttachments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSaveButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Label labelTracker;
        private System.Windows.Forms.ComboBox ComboBoxTracker;
        private System.Windows.Forms.DateTimePicker DateStart;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.TextBox TextBoxSubject;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox TextBoxDescription;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox ComboBoxStatus;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.ComboBox ComboBoxPriority;
        private System.Windows.Forms.DateTimePicker DateDue;
        private System.Windows.Forms.Label labelEstimatedTime;
        private System.Windows.Forms.TextBox TextBoxEstimatedTime;
        private System.Windows.Forms.Label labelAssignedTo;
        private System.Windows.Forms.ComboBox ComboBoxAssignedTo;
        private System.Windows.Forms.Label labelTargetVersion;
        private System.Windows.Forms.ComboBox ComboBoxTargetVersion;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label labelPercentDone;
        private System.Windows.Forms.CheckBox cbStartDate;
        private System.Windows.Forms.CheckBox cbDueDate;
        private System.Windows.Forms.Button BtnCloseButton;
        private System.Windows.Forms.LinkLabel linkEditInRedmine;
        private System.Windows.Forms.DataGridView DataGridViewCustomFields;
        private System.Windows.Forms.Button BtnViewTimeButton;
        private System.Windows.Forms.DataGridView dataGridViewAttachments;
        private System.Windows.Forms.GroupBox groupBoxAttachments;
        private System.Windows.Forms.Button BtnAddButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAttachments;
        private System.Windows.Forms.ToolStripMenuItem downloadOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.Button BtnDeleteButton;
    }
}