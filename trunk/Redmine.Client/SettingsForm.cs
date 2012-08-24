using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using Redmine.Client.Languages;

namespace Redmine.Client
{
    public partial class SettingsForm : Form
    {
        private List<System.Globalization.CultureInfo> supportedLang = new List<System.Globalization.CultureInfo> {
            new System.Globalization.CultureInfo("nl-NL"),
            new System.Globalization.CultureInfo("en-US")
        };
        /* api version lower then 1.1 does not support time-entry, so is not supported. */
        private List<Redmine.Net.Api.Types.IdentifiableName> apiVersions = new List<Redmine.Net.Api.Types.IdentifiableName> {
            /*new Redmine.Net.Api.Types.IdentifiableName { Id = (int)ApiVersion.V10x, Name = LangTools.GetTextForApiVersion(ApiVersion.V10x) },*/
            new Redmine.Net.Api.Types.IdentifiableName { Id = (int)ApiVersion.V11x, Name = LangTools.GetTextForApiVersion(ApiVersion.V11x) },
            new Redmine.Net.Api.Types.IdentifiableName { Id = (int)ApiVersion.V13x, Name = LangTools.GetTextForApiVersion(ApiVersion.V13x) },
            new Redmine.Net.Api.Types.IdentifiableName { Id = (int)ApiVersion.V14x, Name = LangTools.GetTextForApiVersion(ApiVersion.V14x) },
            new Redmine.Net.Api.Types.IdentifiableName { Id = (int)ApiVersion.V21x, Name = LangTools.GetTextForApiVersion(ApiVersion.V21x) }
        };

        public SettingsForm()
        {
            InitializeComponent();
            LoadLanguage();
            LanguageComboBox.DataSource = supportedLang;
            LanguageComboBox.ValueMember = "Name";
            LanguageComboBox.DisplayMember = "DisplayName";

            RedmineVersionComboBox.DataSource = apiVersions;
            RedmineVersionComboBox.ValueMember = "Id";
            RedmineVersionComboBox.DisplayMember = "Name";

            LoadConfig();
            EnableDisableAuthenticationFields();
        }

        private void LoadLanguage()
        {
            LangTools.UpdateControlsForLanguage(this.Controls);
            this.Text = Lang.DlgSettingsTitle;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Uri uri;
            if (!Uri.TryCreate(RedmineBaseUrlTextBox.Text, UriKind.Absolute, out uri))
            {
                MessageBox.Show(Lang.Error_InvalidUrl, Lang.Error, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.RedmineBaseUrlTextBox.Focus();
                return;
            }
            SaveConfig();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveConfig()
        {
            try
            {
                Languages.Lang.Culture = (System.Globalization.CultureInfo)LanguageComboBox.SelectedItem;
            }
            catch (Exception) { }
            try
            {
                Properties.Settings.Default.PropertyValues["RedmineURL"].PropertyValue = RedmineBaseUrlTextBox.Text;
                Properties.Settings.Default.PropertyValues["RedmineUser"].PropertyValue = RedmineUsernameTextBox.Text;
                Properties.Settings.Default.PropertyValues["RedminePassword"].PropertyValue = RedminePasswordTextBox.Text;
                Properties.Settings.Default.PropertyValues["RedmineAuthentication"].PropertyValue = AuthenticationCheckBox.Checked;
                Properties.Settings.Default.PropertyValues["CheckForUpdates"].PropertyValue = CheckForUpdatesCheckBox.Checked;
                Properties.Settings.Default.PropertyValues["MinimizeToSystemTray"].PropertyValue = MinimizeToSystemTrayCheckBox.Checked;
                Properties.Settings.Default.PropertyValues["MinimizeOnStartTimer"].PropertyValue = MinimizeOnStartTimerCheckBox.Checked;
                Properties.Settings.Default.PropertyValues["PopupInterval"].PropertyValue = PopupTimout.Value;
                Properties.Settings.Default.PropertyValues["CacheLifetime"].PropertyValue = CacheLifetime.Value;
                Properties.Settings.Default.PropertyValues["LanguageCode"].PropertyValue = Languages.Lang.Culture.Name;
                Properties.Settings.Default.PropertyValues["ApiVersion"].PropertyValue = (int)RedmineVersionComboBox.SelectedValue;
                Properties.Settings.Default.Save();
                Enumerations.SaveAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void LoadConfig()
        {
            RedmineBaseUrlTextBox.Text = Properties.Settings.Default.RedmineURL;
            AuthenticationCheckBox.Checked = Properties.Settings.Default.RedmineAuthentication;
            MinimizeToSystemTrayCheckBox.Checked = Properties.Settings.Default.MinimizeToSystemTray;
            MinimizeOnStartTimerCheckBox.Checked = Properties.Settings.Default.MinimizeOnStartTimer;
            RedmineUsernameTextBox.Text = Properties.Settings.Default.RedmineUser;
            RedminePasswordTextBox.Text = Properties.Settings.Default.RedminePassword;
            CheckForUpdatesCheckBox.Checked = Properties.Settings.Default.CheckForUpdates;
            CacheLifetime.Value = Properties.Settings.Default.CacheLifetime;
            PopupTimout.Value = Properties.Settings.Default.PopupInterval;
            RedmineVersionComboBox.SelectedIndex = RedmineVersionComboBox.FindStringExact(Languages.LangTools.GetTextForApiVersion((ApiVersion)Properties.Settings.Default.ApiVersion));
            try {
                Languages.Lang.Culture = new System.Globalization.CultureInfo(Properties.Settings.Default.LanguageCode);
            }
            catch (Exception)
            {
                Languages.Lang.Culture = System.Globalization.CultureInfo.CurrentUICulture;
            }
            LanguageComboBox.SelectedIndex = LanguageComboBox.FindStringExact(Languages.Lang.Culture.DisplayName);
        }

        private void AuthenticationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableAuthenticationFields();
        }

        private void EnableDisableAuthenticationFields()
        {
            if (AuthenticationCheckBox.Checked)
            {
                RedmineUsernameTextBox.Enabled = true;
                RedminePasswordTextBox.Enabled = true;
            }
            else
            {
                RedmineUsernameTextBox.Enabled = false;
                RedminePasswordTextBox.Enabled = false;
            }
        }
    }
}
