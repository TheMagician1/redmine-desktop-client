using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Redmine.Net.Api.Types;
using Redmine.Client.Languages;

namespace Redmine.Client
{
    public partial class CommitForm : Form
    {
        public Issue issue;
        public int ticks = 0;
        public string Comment;
        public int activityId = 0;
        public DateTime spentOn;
        public bool closeIssue = false;

        public CommitForm(Issue issue, int ticks, string Comment, int activityId, DateTime spentOn)
        {
            InitializeComponent();
            LangTools.UpdateControlsForLanguage(this.Controls);
            Text = Lang.CommitConfirmQuestion;

            this.issue = issue;
            this.ticks = ticks;
            this.Comment = Comment;
            this.activityId = activityId;
            this.spentOn = spentOn;

            labelProjectContent.Text = issue.Project.Name;
            labelIssueContent.Text = String.Format("({0}) {1}", issue.Id, issue.Subject);
            if (labelIssueContent.Size.Width > Size.Width - labelIssueContent.Location.X - 10)
                this.Size = new Size(labelIssueContent.Location.X + labelIssueContent.Size.Width + 10, this.Size.Height);

            ComboBoxActivity.DataSource = Enumerations.Activities;
            ComboBoxActivity.DisplayMember = "Name";
            ComboBoxActivity.ValueMember = "Id";
            ComboBoxActivity.SelectedValue = activityId;

            labelTimeContent.Text = String.Format("{0:0.##}", (double)ticks / 3600);
            labelDateSpentContent.Text = spentOn.ToString(Lang.Culture.DateTimeFormat.ShortDatePattern);

            TextBoxComment.Text = Comment;

            if (RedmineClientForm.RedmineVersion < ApiVersion.V13x)
            {
                CheckBoxClosesIssue.Enabled = false;
                CheckBoxClosesIssue.Visible = false;
            }
        }

        private void BtnCommitButton_Click(object sender, EventArgs e)
        {
            Comment = TextBoxComment.Text;
            activityId = (int)ComboBoxActivity.SelectedValue;
            closeIssue = CheckBoxClosesIssue.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelButton_Click(object sender, EventArgs e)
        {
            if (TextBoxComment.Text != Comment)
            {
                switch (MessageBox.Show(String.Format(Lang.CommitCancelSaveCommentText, Environment.NewLine), Lang.CommitCancelSaveCommentQuestion, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        this.DialogResult = DialogResult.None;
                        return;
                    case DialogResult.Yes:
                        Comment = TextBoxComment.Text;
                        break;
                }
            }
        }
    }
}
