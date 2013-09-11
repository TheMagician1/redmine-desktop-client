using System.Windows.Forms;

namespace Redmine.Client
{
    partial class RedmineClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedmineClientForm));
            this.TextBoxHours = new System.Windows.Forms.TextBox();
            this.TextBoxMinutes = new System.Windows.Forms.TextBox();
            this.TextBoxSeconds = new System.Windows.Forms.TextBox();
            this.BtnStartButton = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.TextBoxComment = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.ComboBoxActivity = new System.Windows.Forms.ComboBox();
            this.labelActivity = new System.Windows.Forms.Label();
            this.labelProject = new System.Windows.Forms.Label();
            this.ComboBoxProject = new System.Windows.Forms.ComboBox();
            this.BtnCommitButton = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataGridViewIssues = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BtnRefreshButton = new System.Windows.Forms.Button();
            this.BtnExitButton = new System.Windows.Forms.Button();
            this.BtnResetButton = new System.Windows.Forms.Button();
            this.BtnAboutButton = new System.Windows.Forms.Button();
            this.BtnSettingsButton = new System.Windows.Forms.Button();
            this.BtnNewIssueButton = new System.Windows.Forms.Button();
            this.CheckBoxOnlyMe = new System.Windows.Forms.CheckBox();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.ComboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelTargetVersion = new System.Windows.Forms.Label();
            this.ComboBoxTargetVersion = new System.Windows.Forms.ComboBox();
            this.labelAssignedTo = new System.Windows.Forms.Label();
            this.ComboBoxAssignedTo = new System.Windows.Forms.ComboBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.ComboBoxPriority = new System.Windows.Forms.ComboBox();
            this.BtnClearButton = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.ComboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelTracker = new System.Windows.Forms.Label();
            this.ComboBoxTracker = new System.Windows.Forms.ComboBox();
            this.IssueGridHeaderMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editVisibleColumnsHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueGridMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openIssueInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editVisibleColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnOpenIssueButton = new System.Windows.Forms.Button();
            this.NotifyIconMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewIssues)).BeginInit();
            this.groupBoxFilter.SuspendLayout();
            this.IssueGridHeaderMenuStrip.SuspendLayout();
            this.IssueGridMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxHours
            // 
            this.TextBoxHours.Location = new System.Drawing.Point(9, 10);
            this.TextBoxHours.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxHours.MaxLength = 3;
            this.TextBoxHours.Name = "TextBoxHours";
            this.TextBoxHours.Size = new System.Drawing.Size(26, 20);
            this.TextBoxHours.TabIndex = 15;
            this.TextBoxHours.Text = "0";
            this.TextBoxHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHours_KeyPress);
            this.TextBoxHours.Leave += new System.EventHandler(this.TextBoxHours_Leave);
            // 
            // TextBoxMinutes
            // 
            this.TextBoxMinutes.Location = new System.Drawing.Point(39, 10);
            this.TextBoxMinutes.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxMinutes.MaxLength = 2;
            this.TextBoxMinutes.Name = "TextBoxMinutes";
            this.TextBoxMinutes.Size = new System.Drawing.Size(26, 20);
            this.TextBoxMinutes.TabIndex = 16;
            this.TextBoxMinutes.Text = "0";
            this.TextBoxMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxMinutes_KeyPress);
            this.TextBoxMinutes.Leave += new System.EventHandler(this.TextBoxMinutes_Leave);
            // 
            // TextBoxSeconds
            // 
            this.TextBoxSeconds.Location = new System.Drawing.Point(69, 10);
            this.TextBoxSeconds.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxSeconds.MaxLength = 2;
            this.TextBoxSeconds.Name = "TextBoxSeconds";
            this.TextBoxSeconds.Size = new System.Drawing.Size(26, 20);
            this.TextBoxSeconds.TabIndex = 17;
            this.TextBoxSeconds.Text = "0";
            this.TextBoxSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxSeconds_KeyPress);
            this.TextBoxSeconds.Leave += new System.EventHandler(this.TextBoxSeconds_Leave);
            // 
            // BtnStartButton
            // 
            this.BtnStartButton.Location = new System.Drawing.Point(99, 7);
            this.BtnStartButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnStartButton.Name = "BtnStartButton";
            this.BtnStartButton.Size = new System.Drawing.Size(82, 24);
            this.BtnStartButton.TabIndex = 6;
            this.BtnStartButton.Text = "Start";
            this.BtnStartButton.UseVisualStyleBackColor = true;
            this.BtnStartButton.Click += new System.EventHandler(this.BtnStartButton_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(9, 37);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(86, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // TextBoxComment
            // 
            this.TextBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxComment.Location = new System.Drawing.Point(185, 38);
            this.TextBoxComment.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxComment.Name = "TextBoxComment";
            this.TextBoxComment.Size = new System.Drawing.Size(318, 20);
            this.TextBoxComment.TabIndex = 8;
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(185, 17);
            this.labelComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(51, 13);
            this.labelComment.TabIndex = 7;
            this.labelComment.Text = "Comment";
            // 
            // ComboBoxActivity
            // 
            this.ComboBoxActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxActivity.FormattingEnabled = true;
            this.ComboBoxActivity.Location = new System.Drawing.Point(9, 78);
            this.ComboBoxActivity.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxActivity.Name = "ComboBoxActivity";
            this.ComboBoxActivity.Size = new System.Drawing.Size(172, 21);
            this.ComboBoxActivity.TabIndex = 4;
            this.ComboBoxActivity.SelectedIndexChanged += new System.EventHandler(this.ComboBoxActivity_SelectedIndexChanged);
            // 
            // labelActivity
            // 
            this.labelActivity.AutoSize = true;
            this.labelActivity.Location = new System.Drawing.Point(8, 60);
            this.labelActivity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelActivity.Name = "labelActivity";
            this.labelActivity.Size = new System.Drawing.Size(41, 13);
            this.labelActivity.TabIndex = 3;
            this.labelActivity.Text = "Activity";
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(185, 60);
            this.labelProject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(40, 13);
            this.labelProject.TabIndex = 1;
            this.labelProject.Text = "Project";
            // 
            // ComboBoxProject
            // 
            this.ComboBoxProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProject.FormattingEnabled = true;
            this.ComboBoxProject.Location = new System.Drawing.Point(185, 78);
            this.ComboBoxProject.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxProject.Name = "ComboBoxProject";
            this.ComboBoxProject.Size = new System.Drawing.Size(232, 21);
            this.ComboBoxProject.TabIndex = 2;
            this.ComboBoxProject.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProject_SelectedIndexChanged);
            // 
            // BtnCommitButton
            // 
            this.BtnCommitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCommitButton.Location = new System.Drawing.Point(521, 10);
            this.BtnCommitButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCommitButton.Name = "BtnCommitButton";
            this.BtnCommitButton.Size = new System.Drawing.Size(82, 24);
            this.BtnCommitButton.TabIndex = 9;
            this.BtnCommitButton.Text = "Commit";
            this.BtnCommitButton.UseVisualStyleBackColor = true;
            this.BtnCommitButton.Click += new System.EventHandler(this.BtnCommitButton_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.NotifyIconMenuStrip;
            this.notifyIcon1.Text = "Redmine Client";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // NotifyIconMenuStrip
            // 
            this.NotifyIconMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestoreToolStripMenuItem,
            this.StartToolStripMenuItem,
            this.PauseToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.NotifyIconMenuStrip.Name = "NotifyIconMenuStrip";
            this.NotifyIconMenuStrip.Size = new System.Drawing.Size(106, 92);
            // 
            // RestoreToolStripMenuItem
            // 
            this.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem";
            this.RestoreToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.RestoreToolStripMenuItem.Text = "&Hide";
            this.RestoreToolStripMenuItem.Click += new System.EventHandler(this.RestoreToolStripMenuItem_Click);
            // 
            // StartToolStripMenuItem
            // 
            this.StartToolStripMenuItem.Name = "StartToolStripMenuItem";
            this.StartToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.StartToolStripMenuItem.Text = "&Start";
            this.StartToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
            // 
            // PauseToolStripMenuItem
            // 
            this.PauseToolStripMenuItem.Name = "PauseToolStripMenuItem";
            this.PauseToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.PauseToolStripMenuItem.Text = "&Pause";
            this.PauseToolStripMenuItem.Click += new System.EventHandler(this.PauseToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.ExitToolStripMenuItem.Text = "&Close";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // DataGridViewIssues
            // 
            this.DataGridViewIssues.AllowUserToAddRows = false;
            this.DataGridViewIssues.AllowUserToDeleteRows = false;
            this.DataGridViewIssues.AllowUserToResizeRows = false;
            this.DataGridViewIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewIssues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewIssues.Location = new System.Drawing.Point(9, 103);
            this.DataGridViewIssues.Margin = new System.Windows.Forms.Padding(2);
            this.DataGridViewIssues.MultiSelect = false;
            this.DataGridViewIssues.Name = "DataGridViewIssues";
            this.DataGridViewIssues.ReadOnly = true;
            this.DataGridViewIssues.RowTemplate.Height = 24;
            this.DataGridViewIssues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewIssues.Size = new System.Drawing.Size(408, 385);
            this.DataGridViewIssues.TabIndex = 0;
            this.DataGridViewIssues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewIssues_CellDoubleClick);
            this.DataGridViewIssues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewIssues_CellFormatting);
            this.DataGridViewIssues.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewIssues_CellMouseDown);
            this.DataGridViewIssues.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewIssues_ColumnHeaderMouseClick);
            this.DataGridViewIssues.SelectionChanged += new System.EventHandler(this.DataGridViewIssues_SelectionChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BtnRefreshButton
            // 
            this.BtnRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRefreshButton.Location = new System.Drawing.Point(4, 327);
            this.BtnRefreshButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnRefreshButton.Name = "BtnRefreshButton";
            this.BtnRefreshButton.Size = new System.Drawing.Size(82, 24);
            this.BtnRefreshButton.TabIndex = 10;
            this.BtnRefreshButton.Text = "Refresh";
            this.BtnRefreshButton.UseVisualStyleBackColor = true;
            this.BtnRefreshButton.Click += new System.EventHandler(this.BtnRefreshButton_Click);
            // 
            // BtnExitButton
            // 
            this.BtnExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExitButton.Location = new System.Drawing.Point(521, 464);
            this.BtnExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnExitButton.Name = "BtnExitButton";
            this.BtnExitButton.Size = new System.Drawing.Size(82, 24);
            this.BtnExitButton.TabIndex = 18;
            this.BtnExitButton.Text = "Exit";
            this.BtnExitButton.UseVisualStyleBackColor = true;
            this.BtnExitButton.Click += new System.EventHandler(this.BtnExitButton_Click);
            // 
            // BtnResetButton
            // 
            this.BtnResetButton.Location = new System.Drawing.Point(99, 35);
            this.BtnResetButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnResetButton.Name = "BtnResetButton";
            this.BtnResetButton.Size = new System.Drawing.Size(82, 24);
            this.BtnResetButton.TabIndex = 14;
            this.BtnResetButton.Text = "Reset";
            this.BtnResetButton.UseVisualStyleBackColor = true;
            this.BtnResetButton.Click += new System.EventHandler(this.BtnResetButton_Click);
            // 
            // BtnAboutButton
            // 
            this.BtnAboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAboutButton.Location = new System.Drawing.Point(421, 464);
            this.BtnAboutButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAboutButton.Name = "BtnAboutButton";
            this.BtnAboutButton.Size = new System.Drawing.Size(82, 24);
            this.BtnAboutButton.TabIndex = 13;
            this.BtnAboutButton.Text = "About";
            this.BtnAboutButton.UseVisualStyleBackColor = true;
            this.BtnAboutButton.Click += new System.EventHandler(this.BtnAboutButton_Click);
            // 
            // BtnSettingsButton
            // 
            this.BtnSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSettingsButton.Location = new System.Drawing.Point(521, 47);
            this.BtnSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSettingsButton.Name = "BtnSettingsButton";
            this.BtnSettingsButton.Size = new System.Drawing.Size(82, 24);
            this.BtnSettingsButton.TabIndex = 12;
            this.BtnSettingsButton.Text = "Settings";
            this.BtnSettingsButton.UseVisualStyleBackColor = true;
            this.BtnSettingsButton.Click += new System.EventHandler(this.BtnSettingsButton_Click);
            // 
            // BtnNewIssueButton
            // 
            this.BtnNewIssueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNewIssueButton.Location = new System.Drawing.Point(424, 75);
            this.BtnNewIssueButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnNewIssueButton.Name = "BtnNewIssueButton";
            this.BtnNewIssueButton.Size = new System.Drawing.Size(82, 24);
            this.BtnNewIssueButton.TabIndex = 11;
            this.BtnNewIssueButton.Text = "New issue";
            this.BtnNewIssueButton.UseVisualStyleBackColor = true;
            this.BtnNewIssueButton.Click += new System.EventHandler(this.BtnNewIssueButton_Click);
            // 
            // CheckBoxOnlyMe
            // 
            this.CheckBoxOnlyMe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxOnlyMe.AutoSize = true;
            this.CheckBoxOnlyMe.Location = new System.Drawing.Point(348, 15);
            this.CheckBoxOnlyMe.Name = "CheckBoxOnlyMe";
            this.CheckBoxOnlyMe.Size = new System.Drawing.Size(155, 17);
            this.CheckBoxOnlyMe.TabIndex = 19;
            this.CheckBoxOnlyMe.Text = "Only Issues assigned to Me";
            this.CheckBoxOnlyMe.UseVisualStyleBackColor = true;
            this.CheckBoxOnlyMe.Click += new System.EventHandler(this.CheckBoxOnlyMe_Click);
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilter.Controls.Add(this.labelCategory);
            this.groupBoxFilter.Controls.Add(this.ComboBoxCategory);
            this.groupBoxFilter.Controls.Add(this.labelTargetVersion);
            this.groupBoxFilter.Controls.Add(this.ComboBoxTargetVersion);
            this.groupBoxFilter.Controls.Add(this.labelAssignedTo);
            this.groupBoxFilter.Controls.Add(this.ComboBoxAssignedTo);
            this.groupBoxFilter.Controls.Add(this.labelSubject);
            this.groupBoxFilter.Controls.Add(this.TextBoxSubject);
            this.groupBoxFilter.Controls.Add(this.labelPriority);
            this.groupBoxFilter.Controls.Add(this.ComboBoxPriority);
            this.groupBoxFilter.Controls.Add(this.BtnClearButton);
            this.groupBoxFilter.Controls.Add(this.BtnRefreshButton);
            this.groupBoxFilter.Controls.Add(this.labelStatus);
            this.groupBoxFilter.Controls.Add(this.ComboBoxStatus);
            this.groupBoxFilter.Controls.Add(this.labelTracker);
            this.groupBoxFilter.Controls.Add(this.ComboBoxTracker);
            this.groupBoxFilter.Location = new System.Drawing.Point(422, 103);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(180, 356);
            this.groupBoxFilter.TabIndex = 20;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Filter";
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(2, 280);
            this.labelCategory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(49, 13);
            this.labelCategory.TabIndex = 24;
            this.labelCategory.Text = "Category";
            // 
            // ComboBoxCategory
            // 
            this.ComboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCategory.FormattingEnabled = true;
            this.ComboBoxCategory.Location = new System.Drawing.Point(5, 298);
            this.ComboBoxCategory.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxCategory.Name = "ComboBoxCategory";
            this.ComboBoxCategory.Size = new System.Drawing.Size(170, 21);
            this.ComboBoxCategory.TabIndex = 25;
            this.ComboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCategory_SelectedIndexChanged);
            // 
            // labelTargetVersion
            // 
            this.labelTargetVersion.AutoSize = true;
            this.labelTargetVersion.Location = new System.Drawing.Point(2, 236);
            this.labelTargetVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTargetVersion.Name = "labelTargetVersion";
            this.labelTargetVersion.Size = new System.Drawing.Size(75, 13);
            this.labelTargetVersion.TabIndex = 22;
            this.labelTargetVersion.Text = "Target version";
            // 
            // ComboBoxTargetVersion
            // 
            this.ComboBoxTargetVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTargetVersion.FormattingEnabled = true;
            this.ComboBoxTargetVersion.Location = new System.Drawing.Point(5, 254);
            this.ComboBoxTargetVersion.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxTargetVersion.Name = "ComboBoxTargetVersion";
            this.ComboBoxTargetVersion.Size = new System.Drawing.Size(170, 21);
            this.ComboBoxTargetVersion.TabIndex = 23;
            this.ComboBoxTargetVersion.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTargetVersion_SelectedIndexChanged);
            // 
            // labelAssignedTo
            // 
            this.labelAssignedTo.AutoSize = true;
            this.labelAssignedTo.Location = new System.Drawing.Point(2, 192);
            this.labelAssignedTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAssignedTo.Name = "labelAssignedTo";
            this.labelAssignedTo.Size = new System.Drawing.Size(62, 13);
            this.labelAssignedTo.TabIndex = 18;
            this.labelAssignedTo.Text = "Assigned to";
            // 
            // ComboBoxAssignedTo
            // 
            this.ComboBoxAssignedTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAssignedTo.FormattingEnabled = true;
            this.ComboBoxAssignedTo.Location = new System.Drawing.Point(5, 210);
            this.ComboBoxAssignedTo.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxAssignedTo.Name = "ComboBoxAssignedTo";
            this.ComboBoxAssignedTo.Size = new System.Drawing.Size(170, 21);
            this.ComboBoxAssignedTo.TabIndex = 19;
            this.ComboBoxAssignedTo.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAssignedTo_SelectedIndexChanged);
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(2, 149);
            this.labelSubject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(43, 13);
            this.labelSubject.TabIndex = 12;
            this.labelSubject.Text = "Subject";
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSubject.Location = new System.Drawing.Point(5, 167);
            this.TextBoxSubject.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(170, 20);
            this.TextBoxSubject.TabIndex = 13;
            this.TextBoxSubject.TextChanged += new System.EventHandler(this.TextBoxSubject_TextChanged);
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(2, 105);
            this.labelPriority.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(38, 13);
            this.labelPriority.TabIndex = 10;
            this.labelPriority.Text = "Priority";
            // 
            // ComboBoxPriority
            // 
            this.ComboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPriority.FormattingEnabled = true;
            this.ComboBoxPriority.Location = new System.Drawing.Point(5, 123);
            this.ComboBoxPriority.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxPriority.Name = "ComboBoxPriority";
            this.ComboBoxPriority.Size = new System.Drawing.Size(170, 21);
            this.ComboBoxPriority.TabIndex = 11;
            this.ComboBoxPriority.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPriority_SelectedIndexChanged);
            // 
            // BtnClearButton
            // 
            this.BtnClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearButton.Location = new System.Drawing.Point(93, 327);
            this.BtnClearButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnClearButton.Name = "BtnClearButton";
            this.BtnClearButton.Size = new System.Drawing.Size(82, 24);
            this.BtnClearButton.TabIndex = 10;
            this.BtnClearButton.Text = "Clear";
            this.BtnClearButton.UseVisualStyleBackColor = true;
            this.BtnClearButton.Click += new System.EventHandler(this.BtnClearButton_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(2, 61);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "Status";
            // 
            // ComboBoxStatus
            // 
            this.ComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStatus.FormattingEnabled = true;
            this.ComboBoxStatus.Location = new System.Drawing.Point(5, 79);
            this.ComboBoxStatus.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxStatus.Name = "ComboBoxStatus";
            this.ComboBoxStatus.Size = new System.Drawing.Size(170, 21);
            this.ComboBoxStatus.TabIndex = 9;
            this.ComboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStatus_SelectedIndexChanged);
            // 
            // labelTracker
            // 
            this.labelTracker.AutoSize = true;
            this.labelTracker.Location = new System.Drawing.Point(2, 17);
            this.labelTracker.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTracker.Name = "labelTracker";
            this.labelTracker.Size = new System.Drawing.Size(44, 13);
            this.labelTracker.TabIndex = 6;
            this.labelTracker.Text = "Tracker";
            // 
            // ComboBoxTracker
            // 
            this.ComboBoxTracker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTracker.FormattingEnabled = true;
            this.ComboBoxTracker.Location = new System.Drawing.Point(5, 35);
            this.ComboBoxTracker.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxTracker.Name = "ComboBoxTracker";
            this.ComboBoxTracker.Size = new System.Drawing.Size(170, 21);
            this.ComboBoxTracker.TabIndex = 7;
            this.ComboBoxTracker.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTracker_SelectedIndexChanged);
            // 
            // IssueGridHeaderMenuStrip
            // 
            this.IssueGridHeaderMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editVisibleColumnsHeaderToolStripMenuItem});
            this.IssueGridHeaderMenuStrip.Name = "IssueGridHeaderMenuStrip";
            this.IssueGridHeaderMenuStrip.Size = new System.Drawing.Size(183, 26);
            // 
            // editVisibleColumnsHeaderToolStripMenuItem
            // 
            this.editVisibleColumnsHeaderToolStripMenuItem.Name = "editVisibleColumnsHeaderToolStripMenuItem";
            this.editVisibleColumnsHeaderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.editVisibleColumnsHeaderToolStripMenuItem.Text = "Edit Visible Columns";
            this.editVisibleColumnsHeaderToolStripMenuItem.Click += new System.EventHandler(this.editVisibleColumnsToolStripMenuItem_Click);
            // 
            // IssueGridMenuStrip
            // 
            this.IssueGridMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openIssueToolStripMenuItem,
            this.openIssueInBrowserToolStripMenuItem,
            this.editVisibleColumnsToolStripMenuItem});
            this.IssueGridMenuStrip.Name = "IssueGridMenuStrip";
            this.IssueGridMenuStrip.Size = new System.Drawing.Size(191, 70);
            // 
            // openIssueToolStripMenuItem
            // 
            this.openIssueToolStripMenuItem.Name = "openIssueToolStripMenuItem";
            this.openIssueToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openIssueToolStripMenuItem.Text = "Open Issue";
            this.openIssueToolStripMenuItem.Click += new System.EventHandler(this.openIssueToolStripMenuItem_Click);
            // 
            // openIssueInBrowserToolStripMenuItem
            // 
            this.openIssueInBrowserToolStripMenuItem.Name = "openIssueInBrowserToolStripMenuItem";
            this.openIssueInBrowserToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openIssueInBrowserToolStripMenuItem.Text = "Open Issue In Browser";
            this.openIssueInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openIssueInBrowserToolStripMenuItem_Click);
            // 
            // editVisibleColumnsToolStripMenuItem
            // 
            this.editVisibleColumnsToolStripMenuItem.Name = "editVisibleColumnsToolStripMenuItem";
            this.editVisibleColumnsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.editVisibleColumnsToolStripMenuItem.Text = "Edit Visible Columns";
            this.editVisibleColumnsToolStripMenuItem.Click += new System.EventHandler(this.editVisibleColumnsToolStripMenuItem_Click);
            // 
            // BtnOpenIssueButton
            // 
            this.BtnOpenIssueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOpenIssueButton.Location = new System.Drawing.Point(521, 75);
            this.BtnOpenIssueButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOpenIssueButton.Name = "BtnOpenIssueButton";
            this.BtnOpenIssueButton.Size = new System.Drawing.Size(82, 24);
            this.BtnOpenIssueButton.TabIndex = 11;
            this.BtnOpenIssueButton.Text = "Open Issue";
            this.BtnOpenIssueButton.UseVisualStyleBackColor = true;
            this.BtnOpenIssueButton.Click += new System.EventHandler(this.BtnOpenIssueButton_Click);
            // 
            // RedmineClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 498);
            this.Controls.Add(this.groupBoxFilter);
            this.Controls.Add(this.BtnSettingsButton);
            this.Controls.Add(this.CheckBoxOnlyMe);
            this.Controls.Add(this.BtnOpenIssueButton);
            this.Controls.Add(this.BtnNewIssueButton);
            this.Controls.Add(this.BtnResetButton);
            this.Controls.Add(this.BtnAboutButton);
            this.Controls.Add(this.DataGridViewIssues);
            this.Controls.Add(this.BtnExitButton);
            this.Controls.Add(this.labelActivity);
            this.Controls.Add(this.ComboBoxProject);
            this.Controls.Add(this.BtnCommitButton);
            this.Controls.Add(this.ComboBoxActivity);
            this.Controls.Add(this.labelProject);
            this.Controls.Add(this.TextBoxComment);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.TextBoxSeconds);
            this.Controls.Add(this.BtnStartButton);
            this.Controls.Add(this.TextBoxMinutes);
            this.Controls.Add(this.TextBoxHours);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(625, 536);
            this.Name = "RedmineClientForm";
            this.Text = "Redmine Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.NotifyIconMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewIssues)).EndInit();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.IssueGridHeaderMenuStrip.ResumeLayout(false);
            this.IssueGridMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxHours;
        private System.Windows.Forms.TextBox TextBoxMinutes;
        private System.Windows.Forms.TextBox TextBoxSeconds;
        private System.Windows.Forms.Button BtnStartButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox TextBoxComment;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.ComboBox ComboBoxActivity;
        private System.Windows.Forms.Label labelActivity;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.ComboBox ComboBoxProject;
        private System.Windows.Forms.Button BtnCommitButton;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip NotifyIconMenuStrip;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem RestoreToolStripMenuItem;
        private DataGridView DataGridViewIssues;
        private Timer timer1;
        private Button BtnRefreshButton;
        private Button BtnExitButton;
        private Button BtnResetButton;
        private Button BtnAboutButton;
        private Button BtnSettingsButton;
        private Button BtnNewIssueButton;
        private CheckBox CheckBoxOnlyMe;
        private ToolStripMenuItem PauseToolStripMenuItem;
        private ToolStripMenuItem StartToolStripMenuItem;
        private GroupBox groupBoxFilter;
        private Label labelTracker;
        private ComboBox ComboBoxTracker;
        private Label labelStatus;
        private ComboBox ComboBoxStatus;
        private Label labelPriority;
        private ComboBox ComboBoxPriority;
        private Label labelSubject;
        private TextBox TextBoxSubject;
        private Label labelAssignedTo;
        private ComboBox ComboBoxAssignedTo;
        private Label labelTargetVersion;
        private ComboBox ComboBoxTargetVersion;
        private Label labelCategory;
        private ComboBox ComboBoxCategory;
        private ContextMenuStrip IssueGridHeaderMenuStrip;
        private ToolStripMenuItem editVisibleColumnsHeaderToolStripMenuItem;
        private ContextMenuStrip IssueGridMenuStrip;
        private ToolStripMenuItem openIssueToolStripMenuItem;
        private ToolStripMenuItem openIssueInBrowserToolStripMenuItem;
        private ToolStripMenuItem editVisibleColumnsToolStripMenuItem;
        private Button BtnClearButton;
        private Button BtnOpenIssueButton;
    }
}

