using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Redmine.Client.Languages;
using Redmine.Client.Properties;

namespace Redmine.Client
{
    public partial class IssueGridSelectColumns : Form
    {
        public IssueGridSelectColumns()
        {
            InitializeComponent();
            LangTools.UpdateControlsForLanguage(this.Controls);

            radioButtonHideAssignedTo.Checked = !Settings.Default.IssueGridHeader_ShowAssignedTo;
            radioButtonShowAssignedTo.Checked = Settings.Default.IssueGridHeader_ShowAssignedTo;
            radioButtonHideCategory.Checked = !Settings.Default.IssueGridHeader_ShowCategory;
            radioButtonShowCategory.Checked = Settings.Default.IssueGridHeader_ShowCategory;
            radioButtonHideParent.Checked = !Settings.Default.IssueGridHeader_ShowParentIssue;
            radioButtonShowParent.Checked = Settings.Default.IssueGridHeader_ShowParentIssue;
            radioButtonHidePriority.Checked = !Settings.Default.IssueGridHeader_ShowPriority;
            radioButtonShowPriority.Checked = Settings.Default.IssueGridHeader_ShowPriority;
            radioButtonHideProject.Checked = !Settings.Default.IssueGridHeader_ShowProject;
            radioButtonShowProject.Checked = Settings.Default.IssueGridHeader_ShowProject;
            radioButtonHideStatus.Checked = !Settings.Default.IssueGridHeader_ShowStatus;
            radioButtonShowStatus.Checked = Settings.Default.IssueGridHeader_ShowStatus;
            radioButtonHideFixedVersion.Checked = !Settings.Default.IssueGridHeader_ShowFixedVersion;
            radioButtonShowFixedVersion.Checked = Settings.Default.IssueGridHeader_ShowFixedVersion;
        }

        private void BtnOKButton_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.UpdateSetting("IssueGridHeader_ShowAssignedTo", radioButtonShowAssignedTo.Checked);
                Settings.Default.UpdateSetting("IssueGridHeader_ShowCategory", radioButtonShowCategory.Checked);
                Settings.Default.UpdateSetting("IssueGridHeader_ShowParentIssue", radioButtonShowParent.Checked);
                Settings.Default.UpdateSetting("IssueGridHeader_ShowPriority", radioButtonShowPriority.Checked);
                Settings.Default.UpdateSetting("IssueGridHeader_ShowProject", radioButtonShowProject.Checked);
                Settings.Default.UpdateSetting("IssueGridHeader_ShowStatus", radioButtonShowStatus.Checked);
                Settings.Default.UpdateSetting("IssueGridHeader_ShowFixedVersion", radioButtonShowFixedVersion.Checked);
                Settings.Default.Save();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
