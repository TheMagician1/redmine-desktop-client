using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Redmine.Client.Languages;

namespace Redmine.Client
{
    public partial class IssueGridSelectColumns : Form
    {
        public IssueGridSelectColumns()
        {
            InitializeComponent();
            LangTools.UpdateControlsForLanguage(this.Controls);

            radioButtonHideAssignedTo.Checked = !Properties.Settings.Default.IssueGridHeader_ShowAssignedTo;
            radioButtonShowAssignedTo.Checked = Properties.Settings.Default.IssueGridHeader_ShowAssignedTo;
            radioButtonHideCategory.Checked = !Properties.Settings.Default.IssueGridHeader_ShowCategory;
            radioButtonShowCategory.Checked = Properties.Settings.Default.IssueGridHeader_ShowCategory;
            radioButtonHideParent.Checked = !Properties.Settings.Default.IssueGridHeader_ShowParentIssue;
            radioButtonShowParent.Checked = Properties.Settings.Default.IssueGridHeader_ShowParentIssue;
            radioButtonHidePriority.Checked = !Properties.Settings.Default.IssueGridHeader_ShowPriority;
            radioButtonShowPriority.Checked = Properties.Settings.Default.IssueGridHeader_ShowPriority;
            radioButtonHideProject.Checked = !Properties.Settings.Default.IssueGridHeader_ShowProject;
            radioButtonShowProject.Checked = Properties.Settings.Default.IssueGridHeader_ShowProject;
            radioButtonHideStatus.Checked = !Properties.Settings.Default.IssueGridHeader_ShowStatus;
            radioButtonShowStatus.Checked = Properties.Settings.Default.IssueGridHeader_ShowStatus;
            radioButtonHideFixedVersion.Checked = !Properties.Settings.Default.IssueGridHeader_ShowFixedVersion;
            radioButtonShowFixedVersion.Checked = Properties.Settings.Default.IssueGridHeader_ShowFixedVersion;
        }

        private void BtnOKButton_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowAssignedTo"].PropertyValue = radioButtonShowAssignedTo.Checked;
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowCategory"].PropertyValue = radioButtonShowCategory.Checked;
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowParentIssue"].PropertyValue = radioButtonShowParent.Checked;
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowPriority"].PropertyValue = radioButtonShowPriority.Checked;
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowProject"].PropertyValue = radioButtonShowProject.Checked;
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowStatus"].PropertyValue = radioButtonShowStatus.Checked;
                Properties.Settings.Default.PropertyValues["IssueGridHeader_ShowFixedVersion"].PropertyValue = radioButtonShowFixedVersion.Checked;
                Properties.Settings.Default.Save();

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
