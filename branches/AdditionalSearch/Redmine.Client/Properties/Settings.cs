namespace Redmine.Client.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }

        public void SetTickingTick(bool ticking, int ticks)
        {
            Settings.Default.PropertyValues["IsTicking"].PropertyValue = ticking;
            Settings.Default.PropertyValues["TickingTicks"].PropertyValue = ticks;
            Settings.Default.Save();
        }

        public void SetIssueGridSort(string columnName, System.Windows.Forms.SortOrder order)
        {
            Settings.Default.PropertyValues["IssueGridSortColumn"].PropertyValue = columnName;
            Settings.Default.PropertyValues["IssueGridSortOrder"].PropertyValue = order;
            Settings.Default.Save();
        }
        public bool ShowIssueGridColumn(string columnName)
        {
            if (Settings.Default.PropertyValues["IssueGridHeader_Show" + columnName] != null)
                return (bool)Settings.Default.PropertyValues["IssueGridHeader_Show" + columnName].PropertyValue;
            return false;
        }

        public void UpdateSetting<T>(string settingName, T setting)
        {
            Settings.Default.PropertyValues[settingName].PropertyValue = setting;
        }
    }
}
