namespace Redmine.Client
{
    partial class SettingsForm
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
            this.labelRedmineURL = new System.Windows.Forms.Label();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.BtnSaveButton = new System.Windows.Forms.Button();
            this.AuthenticationCheckBox = new System.Windows.Forms.CheckBox();
            this.RedmineBaseUrlTextBox = new System.Windows.Forms.TextBox();
            this.RedmineUsernameTextBox = new System.Windows.Forms.TextBox();
            this.labelRedmineUsername = new System.Windows.Forms.Label();
            this.RedminePasswordTextBox = new System.Windows.Forms.TextBox();
            this.labelRedminePassword = new System.Windows.Forms.Label();
            this.CheckForUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.labelCacheLifetime = new System.Windows.Forms.Label();
            this.CacheLifetime = new System.Windows.Forms.NumericUpDown();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.MinimizeToSystemTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.MinimizeOnStartTimerCheckBox = new System.Windows.Forms.CheckBox();
            this.labelPopupTimout = new System.Windows.Forms.Label();
            this.PopupTimout = new System.Windows.Forms.NumericUpDown();
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.labelRedmineVersion = new System.Windows.Forms.Label();
            this.RedmineVersionComboBox = new System.Windows.Forms.ComboBox();
            this.BtnEditActivitiesButton = new System.Windows.Forms.Button();
            this.BtnEditDocumentCategories = new System.Windows.Forms.Button();
            this.BtnEditIssuePriorities = new System.Windows.Forms.Button();
            this.GrpRedmineServerSettings = new System.Windows.Forms.GroupBox();
            this.BtnTestConnection = new System.Windows.Forms.Button();
            this.GrpApplicationSettings = new System.Windows.Forms.GroupBox();
            this.UpdateIssueIfStateLabel = new System.Windows.Forms.Label();
            this.labelExplClosingIssueStatus = new System.Windows.Forms.Label();
            this.UpdateIssueInProgressComboBox = new System.Windows.Forms.ComboBox();
            this.UpdateIssueNewStateComboBox = new System.Windows.Forms.ComboBox();
            this.ComboBoxCloseStatus = new System.Windows.Forms.ComboBox();
            this.labelSelectCloseStatus = new System.Windows.Forms.Label();
            this.UpdateIssueIfStateCheckBox = new System.Windows.Forms.CheckBox();
            this.PauseTimerOnLockCheckBox = new System.Windows.Forms.CheckBox();
            this.GrpEditEnumerations = new System.Windows.Forms.GroupBox();
            this.AddNoteOnChangeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.CacheLifetime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupTimout)).BeginInit();
            this.GrpRedmineServerSettings.SuspendLayout();
            this.GrpApplicationSettings.SuspendLayout();
            this.GrpEditEnumerations.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRedmineURL
            // 
            this.labelRedmineURL.AutoSize = true;
            this.labelRedmineURL.Location = new System.Drawing.Point(5, 16);
            this.labelRedmineURL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedmineURL.Name = "labelRedmineURL";
            this.labelRedmineURL.Size = new System.Drawing.Size(74, 13);
            this.labelRedmineURL.TabIndex = 0;
            this.labelRedmineURL.Text = "Redmine URL";
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(441, 510);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 6;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            this.BtnCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // BtnSaveButton
            // 
            this.BtnSaveButton.Location = new System.Drawing.Point(369, 510);
            this.BtnSaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSaveButton.Name = "BtnSaveButton";
            this.BtnSaveButton.Size = new System.Drawing.Size(68, 24);
            this.BtnSaveButton.TabIndex = 5;
            this.BtnSaveButton.Text = "Save";
            this.BtnSaveButton.UseVisualStyleBackColor = true;
            this.BtnSaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AuthenticationCheckBox
            // 
            this.AuthenticationCheckBox.AutoSize = true;
            this.AuthenticationCheckBox.Location = new System.Drawing.Point(8, 57);
            this.AuthenticationCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.AuthenticationCheckBox.Name = "AuthenticationCheckBox";
            this.AuthenticationCheckBox.Size = new System.Drawing.Size(138, 17);
            this.AuthenticationCheckBox.TabIndex = 2;
            this.AuthenticationCheckBox.Text = "Requires authentication";
            this.AuthenticationCheckBox.UseVisualStyleBackColor = true;
            this.AuthenticationCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // RedmineBaseUrlTextBox
            // 
            this.RedmineBaseUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RedmineBaseUrlTextBox.Location = new System.Drawing.Point(7, 33);
            this.RedmineBaseUrlTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RedmineBaseUrlTextBox.Name = "RedmineBaseUrlTextBox";
            this.RedmineBaseUrlTextBox.Size = new System.Drawing.Size(485, 20);
            this.RedmineBaseUrlTextBox.TabIndex = 1;
            // 
            // RedmineUsernameTextBox
            // 
            this.RedmineUsernameTextBox.Location = new System.Drawing.Point(8, 91);
            this.RedmineUsernameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RedmineUsernameTextBox.Name = "RedmineUsernameTextBox";
            this.RedmineUsernameTextBox.Size = new System.Drawing.Size(240, 20);
            this.RedmineUsernameTextBox.TabIndex = 4;
            // 
            // labelRedmineUsername
            // 
            this.labelRedmineUsername.AutoSize = true;
            this.labelRedmineUsername.Location = new System.Drawing.Point(5, 75);
            this.labelRedmineUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedmineUsername.Name = "labelRedmineUsername";
            this.labelRedmineUsername.Size = new System.Drawing.Size(98, 13);
            this.labelRedmineUsername.TabIndex = 3;
            this.labelRedmineUsername.Text = "Redmine username";
            // 
            // RedminePasswordTextBox
            // 
            this.RedminePasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RedminePasswordTextBox.Location = new System.Drawing.Point(252, 91);
            this.RedminePasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RedminePasswordTextBox.Name = "RedminePasswordTextBox";
            this.RedminePasswordTextBox.PasswordChar = '*';
            this.RedminePasswordTextBox.Size = new System.Drawing.Size(240, 20);
            this.RedminePasswordTextBox.TabIndex = 6;
            // 
            // labelRedminePassword
            // 
            this.labelRedminePassword.AutoSize = true;
            this.labelRedminePassword.Location = new System.Drawing.Point(249, 75);
            this.labelRedminePassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedminePassword.Name = "labelRedminePassword";
            this.labelRedminePassword.Size = new System.Drawing.Size(97, 13);
            this.labelRedminePassword.TabIndex = 5;
            this.labelRedminePassword.Text = "Redmine password";
            // 
            // CheckForUpdatesCheckBox
            // 
            this.CheckForUpdatesCheckBox.AutoSize = true;
            this.CheckForUpdatesCheckBox.Location = new System.Drawing.Point(8, 18);
            this.CheckForUpdatesCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.CheckForUpdatesCheckBox.Name = "CheckForUpdatesCheckBox";
            this.CheckForUpdatesCheckBox.Size = new System.Drawing.Size(163, 17);
            this.CheckForUpdatesCheckBox.TabIndex = 0;
            this.CheckForUpdatesCheckBox.Text = "Check for updates on startup";
            this.CheckForUpdatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // labelCacheLifetime
            // 
            this.labelCacheLifetime.AutoSize = true;
            this.labelCacheLifetime.Enabled = false;
            this.labelCacheLifetime.Location = new System.Drawing.Point(10, 516);
            this.labelCacheLifetime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCacheLifetime.Name = "labelCacheLifetime";
            this.labelCacheLifetime.Size = new System.Drawing.Size(172, 13);
            this.labelCacheLifetime.TabIndex = 3;
            this.labelCacheLifetime.Text = "Cache lifetime (minutes, 0 = infinite)";
            this.labelCacheLifetime.Visible = false;
            // 
            // CacheLifetime
            // 
            this.CacheLifetime.Enabled = false;
            this.CacheLifetime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CacheLifetime.Location = new System.Drawing.Point(186, 514);
            this.CacheLifetime.Margin = new System.Windows.Forms.Padding(2);
            this.CacheLifetime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CacheLifetime.Name = "CacheLifetime";
            this.CacheLifetime.Size = new System.Drawing.Size(70, 20);
            this.CacheLifetime.TabIndex = 4;
            this.CacheLifetime.Visible = false;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(5, 104);
            this.labelLanguage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(55, 13);
            this.labelLanguage.TabIndex = 6;
            this.labelLanguage.Text = "Language";
            // 
            // MinimizeToSystemTrayCheckBox
            // 
            this.MinimizeToSystemTrayCheckBox.AutoSize = true;
            this.MinimizeToSystemTrayCheckBox.Location = new System.Drawing.Point(8, 39);
            this.MinimizeToSystemTrayCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeToSystemTrayCheckBox.Name = "MinimizeToSystemTrayCheckBox";
            this.MinimizeToSystemTrayCheckBox.Size = new System.Drawing.Size(132, 17);
            this.MinimizeToSystemTrayCheckBox.TabIndex = 1;
            this.MinimizeToSystemTrayCheckBox.Text = "Minimize to Systemtray";
            this.MinimizeToSystemTrayCheckBox.UseVisualStyleBackColor = true;
            this.MinimizeToSystemTrayCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // MinimizeOnStartTimerCheckBox
            // 
            this.MinimizeOnStartTimerCheckBox.AutoSize = true;
            this.MinimizeOnStartTimerCheckBox.Location = new System.Drawing.Point(8, 60);
            this.MinimizeOnStartTimerCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeOnStartTimerCheckBox.Name = "MinimizeOnStartTimerCheckBox";
            this.MinimizeOnStartTimerCheckBox.Size = new System.Drawing.Size(129, 17);
            this.MinimizeOnStartTimerCheckBox.TabIndex = 2;
            this.MinimizeOnStartTimerCheckBox.Text = "Minimize on start timer";
            this.MinimizeOnStartTimerCheckBox.UseVisualStyleBackColor = true;
            this.MinimizeOnStartTimerCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // labelPopupTimout
            // 
            this.labelPopupTimout.AutoSize = true;
            this.labelPopupTimout.Location = new System.Drawing.Point(249, 19);
            this.labelPopupTimout.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPopupTimout.Name = "labelPopupTimout";
            this.labelPopupTimout.Size = new System.Drawing.Size(154, 39);
            this.labelPopupTimout.TabIndex = 4;
            this.labelPopupTimout.Text = "Popup window when minimized\r\nor Request attention every\r\n(minutes, 0 = never)";
            // 
            // PopupTimout
            // 
            this.PopupTimout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PopupTimout.Location = new System.Drawing.Point(252, 60);
            this.PopupTimout.Margin = new System.Windows.Forms.Padding(2);
            this.PopupTimout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.PopupTimout.Name = "PopupTimout";
            this.PopupTimout.Size = new System.Drawing.Size(70, 20);
            this.PopupTimout.TabIndex = 5;
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Location = new System.Drawing.Point(252, 101);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(239, 21);
            this.LanguageComboBox.TabIndex = 7;
            // 
            // labelRedmineVersion
            // 
            this.labelRedmineVersion.AutoSize = true;
            this.labelRedmineVersion.Location = new System.Drawing.Point(5, 119);
            this.labelRedmineVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedmineVersion.Name = "labelRedmineVersion";
            this.labelRedmineVersion.Size = new System.Drawing.Size(87, 13);
            this.labelRedmineVersion.TabIndex = 7;
            this.labelRedmineVersion.Text = "Redmine Version";
            // 
            // RedmineVersionComboBox
            // 
            this.RedmineVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RedmineVersionComboBox.FormattingEnabled = true;
            this.RedmineVersionComboBox.Location = new System.Drawing.Point(157, 116);
            this.RedmineVersionComboBox.Name = "RedmineVersionComboBox";
            this.RedmineVersionComboBox.Size = new System.Drawing.Size(91, 21);
            this.RedmineVersionComboBox.TabIndex = 8;
            // 
            // BtnEditActivitiesButton
            // 
            this.BtnEditActivitiesButton.Location = new System.Drawing.Point(7, 19);
            this.BtnEditActivitiesButton.Name = "BtnEditActivitiesButton";
            this.BtnEditActivitiesButton.Size = new System.Drawing.Size(82, 24);
            this.BtnEditActivitiesButton.TabIndex = 0;
            this.BtnEditActivitiesButton.Text = "Activities";
            this.BtnEditActivitiesButton.UseVisualStyleBackColor = true;
            this.BtnEditActivitiesButton.Click += new System.EventHandler(this.BtnEditActivitiesButton_Click);
            // 
            // BtnEditDocumentCategories
            // 
            this.BtnEditDocumentCategories.Location = new System.Drawing.Point(95, 19);
            this.BtnEditDocumentCategories.Name = "BtnEditDocumentCategories";
            this.BtnEditDocumentCategories.Size = new System.Drawing.Size(82, 24);
            this.BtnEditDocumentCategories.TabIndex = 1;
            this.BtnEditDocumentCategories.Text = "Doc. Cat.";
            this.BtnEditDocumentCategories.UseVisualStyleBackColor = true;
            this.BtnEditDocumentCategories.Click += new System.EventHandler(this.BtnEditDocumentCategories_Click);
            // 
            // BtnEditIssuePriorities
            // 
            this.BtnEditIssuePriorities.Location = new System.Drawing.Point(183, 19);
            this.BtnEditIssuePriorities.Name = "BtnEditIssuePriorities";
            this.BtnEditIssuePriorities.Size = new System.Drawing.Size(82, 24);
            this.BtnEditIssuePriorities.TabIndex = 2;
            this.BtnEditIssuePriorities.Text = "Priorities";
            this.BtnEditIssuePriorities.UseVisualStyleBackColor = true;
            this.BtnEditIssuePriorities.Click += new System.EventHandler(this.BtnEditIssuePriorities_Click);
            // 
            // GrpRedmineServerSettings
            // 
            this.GrpRedmineServerSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpRedmineServerSettings.Controls.Add(this.RedminePasswordTextBox);
            this.GrpRedmineServerSettings.Controls.Add(this.BtnTestConnection);
            this.GrpRedmineServerSettings.Controls.Add(this.labelRedminePassword);
            this.GrpRedmineServerSettings.Controls.Add(this.RedmineUsernameTextBox);
            this.GrpRedmineServerSettings.Controls.Add(this.RedmineVersionComboBox);
            this.GrpRedmineServerSettings.Controls.Add(this.labelRedmineUsername);
            this.GrpRedmineServerSettings.Controls.Add(this.RedmineBaseUrlTextBox);
            this.GrpRedmineServerSettings.Controls.Add(this.AuthenticationCheckBox);
            this.GrpRedmineServerSettings.Controls.Add(this.labelRedmineURL);
            this.GrpRedmineServerSettings.Controls.Add(this.labelRedmineVersion);
            this.GrpRedmineServerSettings.Location = new System.Drawing.Point(12, 12);
            this.GrpRedmineServerSettings.Name = "GrpRedmineServerSettings";
            this.GrpRedmineServerSettings.Size = new System.Drawing.Size(497, 173);
            this.GrpRedmineServerSettings.TabIndex = 0;
            this.GrpRedmineServerSettings.TabStop = false;
            this.GrpRedmineServerSettings.Text = "Redmine Server Settings";
            // 
            // BtnTestConnection
            // 
            this.BtnTestConnection.Location = new System.Drawing.Point(8, 143);
            this.BtnTestConnection.Name = "BtnTestConnection";
            this.BtnTestConnection.Size = new System.Drawing.Size(149, 24);
            this.BtnTestConnection.TabIndex = 9;
            this.BtnTestConnection.Text = "Test Connection";
            this.BtnTestConnection.UseVisualStyleBackColor = true;
            this.BtnTestConnection.Click += new System.EventHandler(this.BtnTestConnection_Click);
            // 
            // GrpApplicationSettings
            // 
            this.GrpApplicationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpApplicationSettings.Controls.Add(this.UpdateIssueIfStateLabel);
            this.GrpApplicationSettings.Controls.Add(this.labelExplClosingIssueStatus);
            this.GrpApplicationSettings.Controls.Add(this.UpdateIssueInProgressComboBox);
            this.GrpApplicationSettings.Controls.Add(this.UpdateIssueNewStateComboBox);
            this.GrpApplicationSettings.Controls.Add(this.ComboBoxCloseStatus);
            this.GrpApplicationSettings.Controls.Add(this.LanguageComboBox);
            this.GrpApplicationSettings.Controls.Add(this.PopupTimout);
            this.GrpApplicationSettings.Controls.Add(this.labelPopupTimout);
            this.GrpApplicationSettings.Controls.Add(this.labelSelectCloseStatus);
            this.GrpApplicationSettings.Controls.Add(this.MinimizeToSystemTrayCheckBox);
            this.GrpApplicationSettings.Controls.Add(this.labelLanguage);
            this.GrpApplicationSettings.Controls.Add(this.AddNoteOnChangeCheckBox);
            this.GrpApplicationSettings.Controls.Add(this.UpdateIssueIfStateCheckBox);
            this.GrpApplicationSettings.Controls.Add(this.PauseTimerOnLockCheckBox);
            this.GrpApplicationSettings.Controls.Add(this.MinimizeOnStartTimerCheckBox);
            this.GrpApplicationSettings.Controls.Add(this.CheckForUpdatesCheckBox);
            this.GrpApplicationSettings.Location = new System.Drawing.Point(12, 196);
            this.GrpApplicationSettings.Name = "GrpApplicationSettings";
            this.GrpApplicationSettings.Size = new System.Drawing.Size(497, 254);
            this.GrpApplicationSettings.TabIndex = 1;
            this.GrpApplicationSettings.TabStop = false;
            this.GrpApplicationSettings.Text = "Application Settings";
            // 
            // UpdateIssueIfStateLabel
            // 
            this.UpdateIssueIfStateLabel.AutoSize = true;
            this.UpdateIssueIfStateLabel.Enabled = false;
            this.UpdateIssueIfStateLabel.Location = new System.Drawing.Point(24, 203);
            this.UpdateIssueIfStateLabel.Name = "UpdateIssueIfStateLabel";
            this.UpdateIssueIfStateLabel.Size = new System.Drawing.Size(101, 13);
            this.UpdateIssueIfStateLabel.TabIndex = 13;
            this.UpdateIssueIfStateLabel.Text = "then set the state to";
            // 
            // labelExplClosingIssueStatus
            // 
            this.labelExplClosingIssueStatus.AutoSize = true;
            this.labelExplClosingIssueStatus.Location = new System.Drawing.Point(5, 133);
            this.labelExplClosingIssueStatus.Name = "labelExplClosingIssueStatus";
            this.labelExplClosingIssueStatus.Size = new System.Drawing.Size(309, 13);
            this.labelExplClosingIssueStatus.TabIndex = 8;
            this.labelExplClosingIssueStatus.Text = "Enable fields with \'Test Connection\' and version V1.3.x or higher";
            // 
            // UpdateIssueInProgressComboBox
            // 
            this.UpdateIssueInProgressComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateIssueInProgressComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UpdateIssueInProgressComboBox.Enabled = false;
            this.UpdateIssueInProgressComboBox.FormattingEnabled = true;
            this.UpdateIssueInProgressComboBox.Location = new System.Drawing.Point(342, 200);
            this.UpdateIssueInProgressComboBox.Name = "UpdateIssueInProgressComboBox";
            this.UpdateIssueInProgressComboBox.Size = new System.Drawing.Size(149, 21);
            this.UpdateIssueInProgressComboBox.TabIndex = 14;
            // 
            // UpdateIssueNewStateComboBox
            // 
            this.UpdateIssueNewStateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateIssueNewStateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UpdateIssueNewStateComboBox.Enabled = false;
            this.UpdateIssueNewStateComboBox.FormattingEnabled = true;
            this.UpdateIssueNewStateComboBox.Location = new System.Drawing.Point(342, 176);
            this.UpdateIssueNewStateComboBox.Name = "UpdateIssueNewStateComboBox";
            this.UpdateIssueNewStateComboBox.Size = new System.Drawing.Size(149, 21);
            this.UpdateIssueNewStateComboBox.TabIndex = 12;
            // 
            // ComboBoxCloseStatus
            // 
            this.ComboBoxCloseStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxCloseStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCloseStatus.Enabled = false;
            this.ComboBoxCloseStatus.FormattingEnabled = true;
            this.ComboBoxCloseStatus.Location = new System.Drawing.Point(342, 149);
            this.ComboBoxCloseStatus.Name = "ComboBoxCloseStatus";
            this.ComboBoxCloseStatus.Size = new System.Drawing.Size(149, 21);
            this.ComboBoxCloseStatus.TabIndex = 10;
            // 
            // labelSelectCloseStatus
            // 
            this.labelSelectCloseStatus.AutoSize = true;
            this.labelSelectCloseStatus.Enabled = false;
            this.labelSelectCloseStatus.Location = new System.Drawing.Point(5, 152);
            this.labelSelectCloseStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSelectCloseStatus.Name = "labelSelectCloseStatus";
            this.labelSelectCloseStatus.Size = new System.Drawing.Size(152, 13);
            this.labelSelectCloseStatus.TabIndex = 9;
            this.labelSelectCloseStatus.Text = "When closing an Issue set it to";
            // 
            // UpdateIssueIfStateCheckBox
            // 
            this.UpdateIssueIfStateCheckBox.AutoSize = true;
            this.UpdateIssueIfStateCheckBox.Enabled = false;
            this.UpdateIssueIfStateCheckBox.Location = new System.Drawing.Point(8, 178);
            this.UpdateIssueIfStateCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.UpdateIssueIfStateCheckBox.Name = "UpdateIssueIfStateCheckBox";
            this.UpdateIssueIfStateCheckBox.Size = new System.Drawing.Size(179, 17);
            this.UpdateIssueIfStateCheckBox.TabIndex = 11;
            this.UpdateIssueIfStateCheckBox.Text = "If timer starts and issue has state";
            this.UpdateIssueIfStateCheckBox.UseVisualStyleBackColor = true;
            this.UpdateIssueIfStateCheckBox.CheckedChanged += new System.EventHandler(this.UpdateIssueIfStateCheckBox_CheckedChanged);
            // 
            // PauseTimerOnLockCheckBox
            // 
            this.PauseTimerOnLockCheckBox.AutoSize = true;
            this.PauseTimerOnLockCheckBox.Location = new System.Drawing.Point(8, 81);
            this.PauseTimerOnLockCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.PauseTimerOnLockCheckBox.Name = "PauseTimerOnLockCheckBox";
            this.PauseTimerOnLockCheckBox.Size = new System.Drawing.Size(204, 17);
            this.PauseTimerOnLockCheckBox.TabIndex = 3;
            this.PauseTimerOnLockCheckBox.Text = "Pause timer on screen lock and logoff";
            this.PauseTimerOnLockCheckBox.UseVisualStyleBackColor = true;
            this.PauseTimerOnLockCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // GrpEditEnumerations
            // 
            this.GrpEditEnumerations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpEditEnumerations.Controls.Add(this.BtnEditIssuePriorities);
            this.GrpEditEnumerations.Controls.Add(this.BtnEditDocumentCategories);
            this.GrpEditEnumerations.Controls.Add(this.BtnEditActivitiesButton);
            this.GrpEditEnumerations.Location = new System.Drawing.Point(12, 456);
            this.GrpEditEnumerations.Name = "GrpEditEnumerations";
            this.GrpEditEnumerations.Size = new System.Drawing.Size(497, 49);
            this.GrpEditEnumerations.TabIndex = 2;
            this.GrpEditEnumerations.TabStop = false;
            this.GrpEditEnumerations.Text = "Edit Enumerations";
            // 
            // AddNoteOnChangeCheckBox
            // 
            this.AddNoteOnChangeCheckBox.AutoSize = true;
            this.AddNoteOnChangeCheckBox.Enabled = false;
            this.AddNoteOnChangeCheckBox.Location = new System.Drawing.Point(8, 229);
            this.AddNoteOnChangeCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.AddNoteOnChangeCheckBox.Name = "AddNoteOnChangeCheckBox";
            this.AddNoteOnChangeCheckBox.Size = new System.Drawing.Size(295, 17);
            this.AddNoteOnChangeCheckBox.TabIndex = 11;
            this.AddNoteOnChangeCheckBox.Text = "Add a note when changing the status with above options";
            this.AddNoteOnChangeCheckBox.UseVisualStyleBackColor = true;
            this.AddNoteOnChangeCheckBox.CheckedChanged += new System.EventHandler(this.UpdateIssueIfStateCheckBox_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.BtnSaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(521, 550);
            this.Controls.Add(this.GrpRedmineServerSettings);
            this.Controls.Add(this.CacheLifetime);
            this.Controls.Add(this.labelCacheLifetime);
            this.Controls.Add(this.BtnSaveButton);
            this.Controls.Add(this.BtnCancelButton);
            this.Controls.Add(this.GrpApplicationSettings);
            this.Controls.Add(this.GrpEditEnumerations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.CacheLifetime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupTimout)).EndInit();
            this.GrpRedmineServerSettings.ResumeLayout(false);
            this.GrpRedmineServerSettings.PerformLayout();
            this.GrpApplicationSettings.ResumeLayout(false);
            this.GrpApplicationSettings.PerformLayout();
            this.GrpEditEnumerations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRedmineURL;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Button BtnSaveButton;
        private System.Windows.Forms.CheckBox AuthenticationCheckBox;
        private System.Windows.Forms.TextBox RedmineBaseUrlTextBox;
        private System.Windows.Forms.TextBox RedmineUsernameTextBox;
        private System.Windows.Forms.Label labelRedmineUsername;
        private System.Windows.Forms.TextBox RedminePasswordTextBox;
        private System.Windows.Forms.Label labelRedminePassword;
        private System.Windows.Forms.CheckBox CheckForUpdatesCheckBox;
        private System.Windows.Forms.Label labelCacheLifetime;
        private System.Windows.Forms.NumericUpDown CacheLifetime;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.CheckBox MinimizeToSystemTrayCheckBox;
        private System.Windows.Forms.CheckBox MinimizeOnStartTimerCheckBox;
        private System.Windows.Forms.Label labelPopupTimout;
        private System.Windows.Forms.NumericUpDown PopupTimout;
        private System.Windows.Forms.ComboBox LanguageComboBox;
        private System.Windows.Forms.Label labelRedmineVersion;
        private System.Windows.Forms.ComboBox RedmineVersionComboBox;
        private System.Windows.Forms.Button BtnEditActivitiesButton;
        private System.Windows.Forms.Button BtnEditDocumentCategories;
        private System.Windows.Forms.Button BtnEditIssuePriorities;
        private System.Windows.Forms.GroupBox GrpRedmineServerSettings;
        private System.Windows.Forms.Button BtnTestConnection;
        private System.Windows.Forms.GroupBox GrpApplicationSettings;
        private System.Windows.Forms.GroupBox GrpEditEnumerations;
        private System.Windows.Forms.ComboBox ComboBoxCloseStatus;
        private System.Windows.Forms.Label labelSelectCloseStatus;
        private System.Windows.Forms.Label labelExplClosingIssueStatus;
        private System.Windows.Forms.CheckBox PauseTimerOnLockCheckBox;
        private System.Windows.Forms.Label UpdateIssueIfStateLabel;
        private System.Windows.Forms.ComboBox UpdateIssueInProgressComboBox;
        private System.Windows.Forms.ComboBox UpdateIssueNewStateComboBox;
        private System.Windows.Forms.CheckBox UpdateIssueIfStateCheckBox;
        private System.Windows.Forms.CheckBox AddNoteOnChangeCheckBox;
    }
}