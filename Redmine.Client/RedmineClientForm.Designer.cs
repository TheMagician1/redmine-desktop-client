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
            this.PauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataGridViewIssues = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BtnRefreshButton = new System.Windows.Forms.Button();
            this.BtnExitButton = new System.Windows.Forms.Button();
            this.BtnResetButton = new System.Windows.Forms.Button();
            this.BtnAboutButton = new System.Windows.Forms.Button();
            this.BtnSettingsButton = new System.Windows.Forms.Button();
            this.BtnNewIssueButton = new System.Windows.Forms.Button();
            this.CheckBoxOnlyMe = new System.Windows.Forms.CheckBox();
            this.NotifyIconMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewIssues)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBoxHours
            // 
            this.TextBoxHours.Location = new System.Drawing.Point(9, 10);
            this.TextBoxHours.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxHours.Name = "TextBoxHours";
            this.TextBoxHours.Size = new System.Drawing.Size(26, 20);
            this.TextBoxHours.TabIndex = 15;
            this.TextBoxHours.Text = "0";
            this.TextBoxHours.TextChanged += new System.EventHandler(this.TextBoxHours_TextChanged);
            // 
            // TextBoxMinutes
            // 
            this.TextBoxMinutes.Location = new System.Drawing.Point(39, 10);
            this.TextBoxMinutes.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxMinutes.Name = "TextBoxMinutes";
            this.TextBoxMinutes.Size = new System.Drawing.Size(26, 20);
            this.TextBoxMinutes.TabIndex = 16;
            this.TextBoxMinutes.Text = "0";
            this.TextBoxMinutes.TextChanged += new System.EventHandler(this.TextBoxMinutes_TextChanged);
            // 
            // TextBoxSeconds
            // 
            this.TextBoxSeconds.Location = new System.Drawing.Point(69, 10);
            this.TextBoxSeconds.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxSeconds.Name = "TextBoxSeconds";
            this.TextBoxSeconds.Size = new System.Drawing.Size(26, 20);
            this.TextBoxSeconds.TabIndex = 17;
            this.TextBoxSeconds.Text = "0";
            this.TextBoxSeconds.TextChanged += new System.EventHandler(this.TextBoxSeconds_TextChanged);
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
            this.TextBoxComment.Size = new System.Drawing.Size(240, 20);
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
            this.ComboBoxActivity.Location = new System.Drawing.Point(9, 74);
            this.ComboBoxActivity.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxActivity.Name = "ComboBoxActivity";
            this.ComboBoxActivity.Size = new System.Drawing.Size(172, 21);
            this.ComboBoxActivity.TabIndex = 4;
            this.ComboBoxActivity.SelectedIndexChanged += new System.EventHandler(this.ComboBoxActivity_SelectedIndexChanged);
            // 
            // labelActivity
            // 
            this.labelActivity.AutoSize = true;
            this.labelActivity.Location = new System.Drawing.Point(8, 59);
            this.labelActivity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelActivity.Name = "labelActivity";
            this.labelActivity.Size = new System.Drawing.Size(41, 13);
            this.labelActivity.TabIndex = 3;
            this.labelActivity.Text = "Activity";
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(185, 59);
            this.labelProject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(40, 13);
            this.labelProject.TabIndex = 0;
            this.labelProject.Text = "Project";
            // 
            // ComboBoxProject
            // 
            this.ComboBoxProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProject.FormattingEnabled = true;
            this.ComboBoxProject.Location = new System.Drawing.Point(185, 74);
            this.ComboBoxProject.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxProject.Name = "ComboBoxProject";
            this.ComboBoxProject.Size = new System.Drawing.Size(240, 21);
            this.ComboBoxProject.TabIndex = 1;
            this.ComboBoxProject.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProject_SelectedIndexChanged);
            // 
            // BtnCommitButton
            // 
            this.BtnCommitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCommitButton.Location = new System.Drawing.Point(429, 10);
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
            this.NotifyIconMenuStrip.Size = new System.Drawing.Size(153, 114);
            // 
            // RestoreToolStripMenuItem
            // 
            this.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem";
            this.RestoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.RestoreToolStripMenuItem.Text = "&Hide";
            this.RestoreToolStripMenuItem.Click += new System.EventHandler(this.RestoreToolStripMenuItem_Click);
            // 
            // PauseToolStripMenuItem
            // 
            this.PauseToolStripMenuItem.Name = "PauseToolStripMenuItem";
            this.PauseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PauseToolStripMenuItem.Text = "&Pause";
            this.PauseToolStripMenuItem.Click += new System.EventHandler(this.PauseToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitToolStripMenuItem.Text = "&Close";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // StartToolStripMenuItem
            // 
            this.StartToolStripMenuItem.Name = "StartToolStripMenuItem";
            this.StartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.StartToolStripMenuItem.Text = "&Start";
            this.StartToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
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
            this.DataGridViewIssues.Location = new System.Drawing.Point(9, 99);
            this.DataGridViewIssues.Margin = new System.Windows.Forms.Padding(2);
            this.DataGridViewIssues.MultiSelect = false;
            this.DataGridViewIssues.Name = "DataGridViewIssues";
            this.DataGridViewIssues.ReadOnly = true;
            this.DataGridViewIssues.RowTemplate.Height = 24;
            this.DataGridViewIssues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewIssues.Size = new System.Drawing.Size(416, 312);
            this.DataGridViewIssues.TabIndex = 2;
            this.DataGridViewIssues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewIssues_CellDoubleClick);
            this.DataGridViewIssues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewIssues_CellFormatting);
            this.DataGridViewIssues.SelectionChanged += new System.EventHandler(this.DataGridViewIssues_SelectionChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BtnRefreshButton
            // 
            this.BtnRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRefreshButton.Location = new System.Drawing.Point(429, 71);
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
            this.BtnExitButton.Location = new System.Drawing.Point(429, 387);
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
            this.BtnAboutButton.Location = new System.Drawing.Point(429, 359);
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
            this.BtnSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSettingsButton.Location = new System.Drawing.Point(429, 331);
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
            this.BtnNewIssueButton.Location = new System.Drawing.Point(429, 99);
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
            this.CheckBoxOnlyMe.Location = new System.Drawing.Point(270, 16);
            this.CheckBoxOnlyMe.Name = "CheckBoxOnlyMe";
            this.CheckBoxOnlyMe.Size = new System.Drawing.Size(155, 17);
            this.CheckBoxOnlyMe.TabIndex = 19;
            this.CheckBoxOnlyMe.Text = "Only Issues assigned to Me";
            this.CheckBoxOnlyMe.UseVisualStyleBackColor = true;
            this.CheckBoxOnlyMe.Click += new System.EventHandler(this.CheckBoxOnlyMe_Click);
            // 
            // RedmineClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 421);
            this.Controls.Add(this.BtnSettingsButton);
            this.Controls.Add(this.CheckBoxOnlyMe);
            this.Controls.Add(this.BtnNewIssueButton);
            this.Controls.Add(this.BtnResetButton);
            this.Controls.Add(this.BtnAboutButton);
            this.Controls.Add(this.DataGridViewIssues);
            this.Controls.Add(this.BtnExitButton);
            this.Controls.Add(this.BtnRefreshButton);
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
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(525, 250);
            this.Name = "RedmineClientForm";
            this.Text = "Redmine Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.NotifyIconMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewIssues)).EndInit();
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
    }
}

