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
    public partial class UpdateIssueNoteForm : Form
    {
        private Issue fromIssue;
        private Issue toIssue;
        public String Note { get; private set; }

        public UpdateIssueNoteForm(Issue fromIssue, Issue toIssue)
        {
            this.fromIssue = fromIssue;
            this.toIssue = toIssue;
            InitializeComponent();

            this.Text = String.Format(Lang.DlgUpdateIssueNoteTitle, toIssue.Id, toIssue.Subject);
            LangTools.UpdateControlsForLanguage(this.Controls);
        }

        private void AddUpdatedLabel(string fieldName, string labelCaption)
        {
            if (String.IsNullOrEmpty(labelCaption))
                return;
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Location = new System.Drawing.Point(labelUpdateIssueNote.Location.X, labelUpdateIssueNote.Location.Y);
            newLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            newLabel.Name = "labelUpdateField_" + fieldName;
            newLabel.Size = new System.Drawing.Size(44, 13);
            newLabel.TabIndex = 4;
            newLabel.Text = labelCaption;
            newLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            Controls.Add(newLabel);
            if (newLabel.Size.Width > Size.Width - newLabel.Location.X - 10)
                this.Size = new Size(newLabel.Location.X + newLabel.Size.Width + 10, this.Size.Height);

            SuspendLayout();
            int newLabelHeight = newLabel.Height + 5;
            Size = new System.Drawing.Size(Size.Width, Size.Height + newLabelHeight);
            MinimumSize = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height + newLabelHeight);
            labelUpdateIssueNote.MoveControl(0, newLabelHeight);
            TextBoxDescription.MoveControl(0, newLabelHeight);
            BtnSaveButton.MoveControl(0, newLabelHeight);
            BtnCancelButton.MoveControl(0, newLabelHeight);
            ResumeLayout(false);
        }

        private void UpdateIssueNoteForm_Load(object sender, EventArgs e)
        {
            AddUpdatedLabel("project", LangTools.CreateUpdatedText("project", fromIssue.Project, toIssue.Project));
            AddUpdatedLabel("tracker", LangTools.CreateUpdatedText("tracker", fromIssue.Tracker, toIssue.Tracker));
            AddUpdatedLabel("subject", LangTools.CreateUpdatedText("subject", fromIssue.Subject, toIssue.Subject));
            AddUpdatedLabel("priority", LangTools.CreateUpdatedText("priority", fromIssue.Priority, toIssue.Priority));
            AddUpdatedLabel("status", LangTools.CreateUpdatedText("status", fromIssue.Status, toIssue.Status));
            AddUpdatedLabel("author", LangTools.CreateUpdatedText("author", fromIssue.Author, toIssue.Author));
            AddUpdatedLabel("assignedto", LangTools.CreateUpdatedText("assignedto", fromIssue.AssignedTo, toIssue.AssignedTo));
            AddUpdatedLabel("category", LangTools.CreateUpdatedText("category", fromIssue.Category, toIssue.Category));
            AddUpdatedLabel("parentissue", LangTools.CreateUpdatedText("parentissue", fromIssue.ParentIssue, toIssue.ParentIssue));
            AddUpdatedLabel("fixedversion", LangTools.CreateUpdatedText("fixedversion", fromIssue.FixedVersion, toIssue.FixedVersion));
            AddUpdatedLabel("start_date", LangTools.CreateUpdatedText("startdate", fromIssue.StartDate, toIssue.StartDate, "d"));
            AddUpdatedLabel("duedate", LangTools.CreateUpdatedText("duedate", fromIssue.DueDate, toIssue.DueDate, "d"));
            AddUpdatedLabel("doneratio", LangTools.CreateUpdatedText("doneratio", fromIssue.DoneRatio, toIssue.DoneRatio, "0.0"));
            AddUpdatedLabel("estimatedhours", LangTools.CreateUpdatedText("estimatedhours", fromIssue.EstimatedHours, toIssue.EstimatedHours, "0.0"));
            //AddUpdatedLabel("createdon", LangTools.CreateUpdatedText("createdon", fromIssue.CreatedOn, toIssue.CreatedOn, "d"));
            //AddUpdatedLabel("updatedon", LangTools.CreateUpdatedText("updatedon", fromIssue.UpdatedOn, toIssue.UpdatedOn, "d"));
            if (fromIssue.Description != toIssue.Description)
                AddUpdatedLabel("description", Lang.UpdatedField_Description);
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Note = TextBoxDescription.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
