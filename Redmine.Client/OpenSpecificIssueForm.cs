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
    public partial class OpenSpecificIssueForm : Form
    {
        public OpenSpecificIssueForm()
        {
            InitializeComponent();
            this.Text = Lang.DlgOpenSpecificIssueTitle;
            LangTools.UpdateControlsForLanguage(this.Controls);
            if (labelIssueNumberToOpen.Size.Width > Size.Width - textBoxIssueNumber.Location.X - labelIssueNumberToOpen.Location.X - 15)
            {
                if (labelIssueNumberToOpen.Size.Width + textBoxIssueNumber.Size.Width + 15 > Size.Width - labelIssueNumberToOpen.Location.X - 15)
                    this.Size = new Size(labelIssueNumberToOpen.Location.X + labelIssueNumberToOpen.Size.Width + textBoxIssueNumber.Size.Width + 30, this.Size.Height);
                textBoxIssueNumber.Location = new Point(labelIssueNumberToOpen.Location.X + labelIssueNumberToOpen.Size.Width + 10, textBoxIssueNumber.Location.Y);
            }
        }
        private int issueNumber = 0;
        public int IssueNumber { get { return issueNumber; } }

        private void textBoxIssueNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!RedmineClientForm.CheckNumericValue(new string(e.KeyChar, 1), 0, 9) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void BtnOKButton_Click(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(textBoxIssueNumber.Text, out issueNumber);
            if (!success || issueNumber == 0)
            {
                MessageBox.Show(Lang.Error_ValueOutOfRange, Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
