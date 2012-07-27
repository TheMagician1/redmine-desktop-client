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
            ((System.ComponentModel.ISupportInitialize)(this.CacheLifetime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupTimout)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRedmineURL
            // 
            this.labelRedmineURL.AutoSize = true;
            this.labelRedmineURL.Location = new System.Drawing.Point(9, 7);
            this.labelRedmineURL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedmineURL.Name = "labelRedmineURL";
            this.labelRedmineURL.Size = new System.Drawing.Size(74, 13);
            this.labelRedmineURL.TabIndex = 0;
            this.labelRedmineURL.Text = "Redmine URL";
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(370, 254);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(56, 19);
            this.BtnCancelButton.TabIndex = 1;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            this.BtnCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // BtnSaveButton
            // 
            this.BtnSaveButton.Location = new System.Drawing.Point(309, 254);
            this.BtnSaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSaveButton.Name = "BtnSaveButton";
            this.BtnSaveButton.Size = new System.Drawing.Size(56, 19);
            this.BtnSaveButton.TabIndex = 2;
            this.BtnSaveButton.Text = "Save";
            this.BtnSaveButton.UseVisualStyleBackColor = true;
            this.BtnSaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AuthenticationCheckBox
            // 
            this.AuthenticationCheckBox.AutoSize = true;
            this.AuthenticationCheckBox.Location = new System.Drawing.Point(12, 48);
            this.AuthenticationCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.AuthenticationCheckBox.Name = "AuthenticationCheckBox";
            this.AuthenticationCheckBox.Size = new System.Drawing.Size(138, 17);
            this.AuthenticationCheckBox.TabIndex = 3;
            this.AuthenticationCheckBox.Text = "Requires authentication";
            this.AuthenticationCheckBox.UseVisualStyleBackColor = true;
            this.AuthenticationCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // RedmineBaseUrlTextBox
            // 
            this.RedmineBaseUrlTextBox.Location = new System.Drawing.Point(11, 24);
            this.RedmineBaseUrlTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RedmineBaseUrlTextBox.Name = "RedmineBaseUrlTextBox";
            this.RedmineBaseUrlTextBox.Size = new System.Drawing.Size(416, 20);
            this.RedmineBaseUrlTextBox.TabIndex = 4;
            // 
            // RedmineUsernameTextBox
            // 
            this.RedmineUsernameTextBox.Location = new System.Drawing.Point(11, 82);
            this.RedmineUsernameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RedmineUsernameTextBox.Name = "RedmineUsernameTextBox";
            this.RedmineUsernameTextBox.Size = new System.Drawing.Size(204, 20);
            this.RedmineUsernameTextBox.TabIndex = 6;
            // 
            // labelRedmineUsername
            // 
            this.labelRedmineUsername.AutoSize = true;
            this.labelRedmineUsername.Location = new System.Drawing.Point(9, 66);
            this.labelRedmineUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedmineUsername.Name = "labelRedmineUsername";
            this.labelRedmineUsername.Size = new System.Drawing.Size(98, 13);
            this.labelRedmineUsername.TabIndex = 5;
            this.labelRedmineUsername.Text = "Redmine username";
            // 
            // RedminePasswordTextBox
            // 
            this.RedminePasswordTextBox.Location = new System.Drawing.Point(224, 82);
            this.RedminePasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RedminePasswordTextBox.Name = "RedminePasswordTextBox";
            this.RedminePasswordTextBox.PasswordChar = '*';
            this.RedminePasswordTextBox.Size = new System.Drawing.Size(204, 20);
            this.RedminePasswordTextBox.TabIndex = 8;
            // 
            // labelRedminePassword
            // 
            this.labelRedminePassword.AutoSize = true;
            this.labelRedminePassword.Location = new System.Drawing.Point(221, 66);
            this.labelRedminePassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRedminePassword.Name = "labelRedminePassword";
            this.labelRedminePassword.Size = new System.Drawing.Size(97, 13);
            this.labelRedminePassword.TabIndex = 7;
            this.labelRedminePassword.Text = "Redmine password";
            // 
            // CheckForUpdatesCheckBox
            // 
            this.CheckForUpdatesCheckBox.AutoSize = true;
            this.CheckForUpdatesCheckBox.Location = new System.Drawing.Point(12, 141);
            this.CheckForUpdatesCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.CheckForUpdatesCheckBox.Name = "CheckForUpdatesCheckBox";
            this.CheckForUpdatesCheckBox.Size = new System.Drawing.Size(163, 17);
            this.CheckForUpdatesCheckBox.TabIndex = 9;
            this.CheckForUpdatesCheckBox.Text = "Check for updates on startup";
            this.CheckForUpdatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // labelCacheLifetime
            // 
            this.labelCacheLifetime.AutoSize = true;
            this.labelCacheLifetime.Location = new System.Drawing.Point(9, 102);
            this.labelCacheLifetime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCacheLifetime.Name = "labelCacheLifetime";
            this.labelCacheLifetime.Size = new System.Drawing.Size(172, 13);
            this.labelCacheLifetime.TabIndex = 11;
            this.labelCacheLifetime.Text = "Cache lifetime (minutes, 0 = infinite)";
            // 
            // CacheLifetime
            // 
            this.CacheLifetime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CacheLifetime.Location = new System.Drawing.Point(11, 119);
            this.CacheLifetime.Margin = new System.Windows.Forms.Padding(2);
            this.CacheLifetime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CacheLifetime.Name = "CacheLifetime";
            this.CacheLifetime.Size = new System.Drawing.Size(70, 20);
            this.CacheLifetime.TabIndex = 12;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(9, 213);
            this.labelLanguage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(55, 13);
            this.labelLanguage.TabIndex = 0;
            this.labelLanguage.Text = "Language";
            // 
            // MinimizeToSystemTrayCheckBox
            // 
            this.MinimizeToSystemTrayCheckBox.AutoSize = true;
            this.MinimizeToSystemTrayCheckBox.Location = new System.Drawing.Point(12, 162);
            this.MinimizeToSystemTrayCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeToSystemTrayCheckBox.Name = "MinimizeToSystemTrayCheckBox";
            this.MinimizeToSystemTrayCheckBox.Size = new System.Drawing.Size(132, 17);
            this.MinimizeToSystemTrayCheckBox.TabIndex = 3;
            this.MinimizeToSystemTrayCheckBox.Text = "Minimize to Systemtray";
            this.MinimizeToSystemTrayCheckBox.UseVisualStyleBackColor = true;
            this.MinimizeToSystemTrayCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // MinimizeOnStartTimerCheckBox
            // 
            this.MinimizeOnStartTimerCheckBox.AutoSize = true;
            this.MinimizeOnStartTimerCheckBox.Location = new System.Drawing.Point(12, 183);
            this.MinimizeOnStartTimerCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeOnStartTimerCheckBox.Name = "MinimizeOnStartTimerCheckBox";
            this.MinimizeOnStartTimerCheckBox.Size = new System.Drawing.Size(129, 17);
            this.MinimizeOnStartTimerCheckBox.TabIndex = 3;
            this.MinimizeOnStartTimerCheckBox.Text = "Minimize on start timer";
            this.MinimizeOnStartTimerCheckBox.UseVisualStyleBackColor = true;
            this.MinimizeOnStartTimerCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // labelPopupTimout
            // 
            this.labelPopupTimout.AutoSize = true;
            this.labelPopupTimout.Location = new System.Drawing.Point(221, 140);
            this.labelPopupTimout.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPopupTimout.Name = "labelPopupTimout";
            this.labelPopupTimout.Size = new System.Drawing.Size(154, 39);
            this.labelPopupTimout.TabIndex = 11;
            this.labelPopupTimout.Text = "Popup window when minimized\r\nor Request attention every\r\n(minutes, 0 = never)";
            // 
            // PopupTimout
            // 
            this.PopupTimout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PopupTimout.Location = new System.Drawing.Point(224, 182);
            this.PopupTimout.Margin = new System.Windows.Forms.Padding(2);
            this.PopupTimout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.PopupTimout.Name = "PopupTimout";
            this.PopupTimout.Size = new System.Drawing.Size(70, 20);
            this.PopupTimout.TabIndex = 12;
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Location = new System.Drawing.Point(69, 210);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(146, 21);
            this.LanguageComboBox.TabIndex = 13;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.BtnSaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.LanguageComboBox);
            this.Controls.Add(this.PopupTimout);
            this.Controls.Add(this.labelPopupTimout);
            this.Controls.Add(this.CacheLifetime);
            this.Controls.Add(this.labelCacheLifetime);
            this.Controls.Add(this.CheckForUpdatesCheckBox);
            this.Controls.Add(this.RedminePasswordTextBox);
            this.Controls.Add(this.labelRedminePassword);
            this.Controls.Add(this.RedmineUsernameTextBox);
            this.Controls.Add(this.labelRedmineUsername);
            this.Controls.Add(this.RedmineBaseUrlTextBox);
            this.Controls.Add(this.MinimizeOnStartTimerCheckBox);
            this.Controls.Add(this.MinimizeToSystemTrayCheckBox);
            this.Controls.Add(this.AuthenticationCheckBox);
            this.Controls.Add(this.BtnSaveButton);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.BtnCancelButton);
            this.Controls.Add(this.labelRedmineURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.CacheLifetime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupTimout)).EndInit();
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
    }
}