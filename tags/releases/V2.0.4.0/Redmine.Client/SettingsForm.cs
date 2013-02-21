using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using Redmine.Client.Languages;
using Redmine.Net.Api.Types;
using System.Collections.Specialized;

namespace Redmine.Client
{
    public partial class SettingsForm : Form
    {
        private List<System.Globalization.CultureInfo> supportedLang = new List<System.Globalization.CultureInfo> {
            new System.Globalization.CultureInfo("nl"),
            new System.Globalization.CultureInfo("en"),
            new System.Globalization.CultureInfo("de"),
            new System.Globalization.CultureInfo("cs-CZ"),
            new System.Globalization.CultureInfo("pt-BR"),
            new System.Globalization.CultureInfo("fr")
        };
        /* api version lower then 1.1 does not support time-entry, so is not supported. */
        private List<IdentifiableName> apiVersions = new List<IdentifiableName> {
            /*new IdentifiableName { Id = (int)ApiVersion.V10x, Name = LangTools.GetTextForApiVersion(ApiVersion.V10x) },*/
            new IdentifiableName { Id = (int)ApiVersion.V11x, Name = LangTools.GetTextForApiVersion(ApiVersion.V11x) },
            new IdentifiableName { Id = (int)ApiVersion.V12x, Name = LangTools.GetTextForApiVersion(ApiVersion.V12x) },
            new IdentifiableName { Id = (int)ApiVersion.V13x, Name = LangTools.GetTextForApiVersion(ApiVersion.V13x) },
            new IdentifiableName { Id = (int)ApiVersion.V14x, Name = LangTools.GetTextForApiVersion(ApiVersion.V14x) },
            new IdentifiableName { Id = (int)ApiVersion.V20x, Name = LangTools.GetTextForApiVersion(ApiVersion.V20x) },
            new IdentifiableName { Id = (int)ApiVersion.V21x, Name = LangTools.GetTextForApiVersion(ApiVersion.V21x) },
            new IdentifiableName { Id = (int)ApiVersion.V22x, Name = LangTools.GetTextForApiVersion(ApiVersion.V22x) }
        };

        public SettingsForm()
        {
            InitializeComponent();
            LoadLanguage();

            supportedLang.Sort((x, y) => string.Compare(x.DisplayName, y.DisplayName));

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
            if (Languages.Lang.Culture == null)
                Languages.Lang.Culture = new System.Globalization.CultureInfo("en");
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
                Properties.Settings.Default.PropertyValues["PauseTickingOnLock"].PropertyValue = PauseTimerOnLockCheckBox.Checked;
                if (ComboBoxCloseStatus.Enabled)
                    Properties.Settings.Default.PropertyValues["ClosedStatus"].PropertyValue = (int)ComboBoxCloseStatus.SelectedValue;
                if (UpdateIssueIfStateCheckBox.Enabled)
                {
                    Properties.Settings.Default.PropertyValues["UpdateIssueIfNew"].PropertyValue = UpdateIssueIfStateCheckBox.Checked;
                    if (UpdateIssueIfStateCheckBox.Checked)
                    {
                        Properties.Settings.Default.PropertyValues["NewStatus"].PropertyValue = (int)UpdateIssueNewStateComboBox.SelectedValue;
                        Properties.Settings.Default.PropertyValues["InProgressStatus"].PropertyValue = (int)UpdateIssueInProgressComboBox.SelectedValue;
                    }
                }
                Properties.Settings.Default.Save();
                String Name = Properties.Settings.Default.LanguageCode;
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
            PauseTimerOnLockCheckBox.Checked = Properties.Settings.Default.PauseTickingOnLock;
            RedmineVersionComboBox.SelectedIndex = RedmineVersionComboBox.FindStringExact(Languages.LangTools.GetTextForApiVersion((ApiVersion)Properties.Settings.Default.ApiVersion));
            UpdateIssueIfStateCheckBox.Checked = Properties.Settings.Default.UpdateIssueIfNew;
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

        private void BtnEditActivitiesButton_Click(object sender, EventArgs e)
        {
            EditEnumListForm dlg = new EditEnumListForm(Enumerations.Activities, Lang.EnumName_Activities);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Enumerations.Activities = dlg.enumeration;
                Enumerations.SaveActivities();
            }
        }

        private void BtnEditDocumentCategories_Click(object sender, EventArgs e)
        {
            EditEnumListForm dlg = new EditEnumListForm(Enumerations.DocumentCategories, Lang.EnumName_DocumentCategories);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Enumerations.DocumentCategories = dlg.enumeration;
                Enumerations.SaveDocumentCategories();
            }
        }

        private void BtnEditIssuePriorities_Click(object sender, EventArgs e)
        {
            EditEnumListForm dlg = new EditEnumListForm(Enumerations.IssuePriorities, Lang.EnumName_IssuePriorities);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Enumerations.IssuePriorities = dlg.enumeration;
                Enumerations.SaveIssuePriorities();
            }
        }

        private void BtnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                Redmine.Net.Api.RedmineManager manager;
                if (AuthenticationCheckBox.Checked)
                    manager = new Redmine.Net.Api.RedmineManager(RedmineBaseUrlTextBox.Text, RedmineUsernameTextBox.Text, RedminePasswordTextBox.Text);
                else
                    manager = new Redmine.Net.Api.RedmineManager(RedmineBaseUrlTextBox.Text);
                User newCurrentUser = manager.GetCurrentUser();
                MessageBox.Show(Lang.ConnectionTestOK_Text, Lang.ConnectionTestOK_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.ConnectionTestFailed_Text, ex.Message), Lang.ConnectionTestFailed_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            LoadAndEnableCloseStatus();
            LoadAndEnableSetToInProgressStatus();
        }

        private List<IssueStatus> CloseStatuses;
        private void LoadAndEnableCloseStatus()
        {
            labelSelectCloseStatus.Enabled = false;
            ComboBoxCloseStatus.Enabled = false;
            if ((ApiVersion)RedmineVersionComboBox.SelectedValue < ApiVersion.V13x)
                return;
            try
            {
                Redmine.Net.Api.RedmineManager manager;
                CloseStatuses = new List<IssueStatus>();
                if (AuthenticationCheckBox.Checked)
                    manager = new Redmine.Net.Api.RedmineManager(RedmineBaseUrlTextBox.Text, RedmineUsernameTextBox.Text, RedminePasswordTextBox.Text);
                else
                    manager = new Redmine.Net.Api.RedmineManager(RedmineBaseUrlTextBox.Text);

                NameValueCollection parameters = new NameValueCollection { { "is_closed", "true" } };
                foreach (IssueStatus status in manager.GetTotalObjectList<IssueStatus>(parameters))
                {
                    if (status.IsClosed)
                        CloseStatuses.Add(status);
                }
                ComboBoxCloseStatus.DataSource = CloseStatuses;
                ComboBoxCloseStatus.ValueMember = "Id";
                ComboBoxCloseStatus.DisplayMember = "Name";
                labelSelectCloseStatus.Enabled = true;
                ComboBoxCloseStatus.Enabled = true;

                if (Properties.Settings.Default.ClosedStatus != 0)
                    ComboBoxCloseStatus.SelectedValue = Properties.Settings.Default.ClosedStatus;
                else
                    ComboBoxCloseStatus.SelectedIndex = ComboBoxCloseStatus.FindStringExact("Closed");


            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ComboBoxCloseStatus.Enabled = false;
                labelSelectCloseStatus.Enabled = false;
            }
        }

        private List<IssueStatus> NewStatuses;
        private List<IssueStatus> InProgressStatuses;
        private void LoadAndEnableSetToInProgressStatus()
        {
            UpdateIssueNewStateComboBox.Enabled = false;
            UpdateIssueInProgressComboBox.Enabled = false;
            UpdateIssueIfStateCheckBox.Enabled = false;
            UpdateIssueIfStateLabel.Enabled = false;
            BtnEditActivitiesButton.Enabled = true;
            BtnEditIssuePriorities.Enabled = true;
            if ((ApiVersion)RedmineVersionComboBox.SelectedValue < ApiVersion.V13x)
                return;
            try
            {
                Redmine.Net.Api.RedmineManager manager;
                NewStatuses = new List<IssueStatus>();
                InProgressStatuses = new List<IssueStatus>();
                if (AuthenticationCheckBox.Checked)
                    manager = new Redmine.Net.Api.RedmineManager(RedmineBaseUrlTextBox.Text, RedmineUsernameTextBox.Text, RedminePasswordTextBox.Text);
                else
                    manager = new Redmine.Net.Api.RedmineManager(RedmineBaseUrlTextBox.Text);

                NameValueCollection parameters = new NameValueCollection { { "is_closed", "false" } };
                foreach (IssueStatus status in manager.GetTotalObjectList<IssueStatus>(parameters))
                {
                    if (!status.IsClosed)
                    {
                        NewStatuses.Add(status);
                        InProgressStatuses.Add(status);
                    }
                }
                UpdateIssueNewStateComboBox.DataSource = NewStatuses;
                UpdateIssueNewStateComboBox.ValueMember = "Id";
                UpdateIssueNewStateComboBox.DisplayMember = "Name";

                if (Properties.Settings.Default.NewStatus!= 0)
                    UpdateIssueNewStateComboBox.SelectedValue = Properties.Settings.Default.NewStatus;
                else
                    UpdateIssueNewStateComboBox.SelectedIndex = UpdateIssueNewStateComboBox.FindStringExact("New");

                UpdateIssueInProgressComboBox.DataSource = InProgressStatuses;
                UpdateIssueInProgressComboBox.ValueMember = "Id";
                UpdateIssueInProgressComboBox.DisplayMember = "Name";

                if (Properties.Settings.Default.InProgressStatus != 0)
                    UpdateIssueInProgressComboBox.SelectedValue = Properties.Settings.Default.InProgressStatus;
                else
                    UpdateIssueInProgressComboBox.SelectedIndex = UpdateIssueInProgressComboBox.FindStringExact("In Progress");

                UpdateIssueIfStateCheckBox.Enabled = true;
                UpdateIssueIfStateCheckBox.Checked = Properties.Settings.Default.UpdateIssueIfNew;
                UpdateIssueIfStateLabel.Enabled = true;

                EnableDisableUpdateIssueIfNewFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateIssueNewStateComboBox.Enabled = false;
                UpdateIssueInProgressComboBox.Enabled = false;
            }
            if ((ApiVersion)RedmineVersionComboBox.SelectedValue < ApiVersion.V22x)
                return;

            BtnEditActivitiesButton.Enabled = false;
            BtnEditIssuePriorities.Enabled = false;
        }

        private void EnableDisableUpdateIssueIfNewFields()
        {
            UpdateIssueNewStateComboBox.Enabled = UpdateIssueIfStateCheckBox.Checked && UpdateIssueIfStateCheckBox.Enabled;
            UpdateIssueInProgressComboBox.Enabled = UpdateIssueIfStateCheckBox.Checked && UpdateIssueIfStateCheckBox.Enabled;
        }

        private void UpdateIssueIfStateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableUpdateIssueIfNewFields();
        }

    }
}
