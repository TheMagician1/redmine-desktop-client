using System;
using System.Configuration;
using System.Windows.Forms;

namespace Redmine.Client
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadConfig();
            EnableDisableAuthenticationFields();
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
                MessageBox.Show("Invalid URL of Redmine installation.", "Error", MessageBoxButtons.OK,
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
                System.Configuration.Configuration roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = roamingConfig.FilePath;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                config.AppSettings.Settings.Clear();
                config.AppSettings.Settings.Add("RedmineURL", RedmineBaseUrlTextBox.Text);
                config.AppSettings.Settings.Add("RedmineUser", RedmineUsernameTextBox.Text);
                config.AppSettings.Settings.Add("RedminePassword", RedminePasswordTextBox.Text);
                config.AppSettings.Settings.Add("RedmineAuthentication", AuthenticationCheckBox.Checked.ToString());
                config.AppSettings.Settings.Add("CheckForUpdates", CheckForUpdatesCheckBox.Checked.ToString());
                config.AppSettings.Settings.Add("CacheLifetime", CacheLifetime.Value.ToString());
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void LoadConfig()
        {
            Configuration roamingConf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = roamingConf.FilePath;
            Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            if (conf.AppSettings.Settings.Count == 0)
            {
                conf.AppSettings.Settings.Add("RedmineURL", ConfigurationManager.AppSettings["RedmineURL"]);
                conf.AppSettings.Settings.Add("RedmineAuthentication", ConfigurationManager.AppSettings["RedmineAuthentication"]);
                conf.AppSettings.Settings.Add("RedmineUser", ConfigurationManager.AppSettings["RedmineUser"]);
                conf.AppSettings.Settings.Add("RedminePassword", ConfigurationManager.AppSettings["RedminePassword"]);
                conf.AppSettings.Settings.Add("CheckForUpdates", ConfigurationManager.AppSettings["CheckForUpdates"]);
                conf.AppSettings.Settings.Add("CacheLifetime", ConfigurationManager.AppSettings["CacheLifetime"]);
                conf.Save(ConfigurationSaveMode.Modified);
            }
            RedmineBaseUrlTextBox.Text = conf.AppSettings.Settings["RedmineURL"].Value;
            AuthenticationCheckBox.Checked = Convert.ToBoolean(conf.AppSettings.Settings["RedmineAuthentication"].Value);
            RedmineUsernameTextBox.Text = conf.AppSettings.Settings["RedmineUser"].Value;
            RedminePasswordTextBox.Text = conf.AppSettings.Settings["RedminePassword"].Value;
            CheckForUpdatesCheckBox.Checked = Convert.ToBoolean(conf.AppSettings.Settings["CheckForUpdates"].Value);
            try
            {
                CacheLifetime.Value = Convert.ToInt32(conf.AppSettings.Settings["CacheLifetime"].Value);
            }
            catch (Exception)
            {
                CacheLifetime.Value = 0;
            }
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
