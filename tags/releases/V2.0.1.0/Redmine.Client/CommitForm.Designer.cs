namespace Redmine.Client
{
    partial class CommitForm
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
            this.BtnCommitButton = new System.Windows.Forms.Button();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.TextBoxComment = new System.Windows.Forms.TextBox();
            this.labelCommitComment = new System.Windows.Forms.Label();
            this.labelCommitProject = new System.Windows.Forms.Label();
            this.labelProjectContent = new System.Windows.Forms.Label();
            this.labelCommitIssue = new System.Windows.Forms.Label();
            this.labelIssueContent = new System.Windows.Forms.Label();
            this.labelCommitActivity = new System.Windows.Forms.Label();
            this.ComboBoxActivity = new System.Windows.Forms.ComboBox();
            this.labelCommitQuestion = new System.Windows.Forms.Label();
            this.labelCommitTime = new System.Windows.Forms.Label();
            this.labelTimeContent = new System.Windows.Forms.Label();
            this.labelCommitDateSpent = new System.Windows.Forms.Label();
            this.labelDateSpentContent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnCommitButton
            // 
            this.BtnCommitButton.Location = new System.Drawing.Point(317, 175);
            this.BtnCommitButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCommitButton.Name = "BtnCommitButton";
            this.BtnCommitButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCommitButton.TabIndex = 6;
            this.BtnCommitButton.Text = "Commit";
            this.BtnCommitButton.UseVisualStyleBackColor = true;
            this.BtnCommitButton.Click += new System.EventHandler(this.BtnCommitButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(389, 175);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 5;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            this.BtnCancelButton.Click += new System.EventHandler(this.BtnCancelButton_Click);
            // 
            // TextBoxComment
            // 
            this.TextBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxComment.Location = new System.Drawing.Point(10, 149);
            this.TextBoxComment.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxComment.Name = "TextBoxComment";
            this.TextBoxComment.Size = new System.Drawing.Size(447, 20);
            this.TextBoxComment.TabIndex = 9;
            // 
            // labelCommitComment
            // 
            this.labelCommitComment.AutoSize = true;
            this.labelCommitComment.Location = new System.Drawing.Point(7, 134);
            this.labelCommitComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCommitComment.Name = "labelCommitComment";
            this.labelCommitComment.Size = new System.Drawing.Size(51, 13);
            this.labelCommitComment.TabIndex = 10;
            this.labelCommitComment.Text = "Comment";
            // 
            // labelCommitProject
            // 
            this.labelCommitProject.AutoSize = true;
            this.labelCommitProject.Location = new System.Drawing.Point(7, 38);
            this.labelCommitProject.Name = "labelCommitProject";
            this.labelCommitProject.Size = new System.Drawing.Size(43, 13);
            this.labelCommitProject.TabIndex = 11;
            this.labelCommitProject.Text = "Project:";
            // 
            // labelProjectContent
            // 
            this.labelProjectContent.AutoSize = true;
            this.labelProjectContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProjectContent.Location = new System.Drawing.Point(78, 38);
            this.labelProjectContent.Name = "labelProjectContent";
            this.labelProjectContent.Size = new System.Drawing.Size(79, 13);
            this.labelProjectContent.TabIndex = 12;
            this.labelProjectContent.Text = "ProjectName";
            // 
            // labelCommitIssue
            // 
            this.labelCommitIssue.AutoSize = true;
            this.labelCommitIssue.Location = new System.Drawing.Point(7, 57);
            this.labelCommitIssue.Name = "labelCommitIssue";
            this.labelCommitIssue.Size = new System.Drawing.Size(35, 13);
            this.labelCommitIssue.TabIndex = 11;
            this.labelCommitIssue.Text = "Issue:";
            // 
            // labelIssueContent
            // 
            this.labelIssueContent.AutoSize = true;
            this.labelIssueContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIssueContent.Location = new System.Drawing.Point(78, 57);
            this.labelIssueContent.Name = "labelIssueContent";
            this.labelIssueContent.Size = new System.Drawing.Size(69, 13);
            this.labelIssueContent.TabIndex = 12;
            this.labelIssueContent.Text = "IssueName";
            // 
            // labelCommitActivity
            // 
            this.labelCommitActivity.AutoSize = true;
            this.labelCommitActivity.Location = new System.Drawing.Point(7, 76);
            this.labelCommitActivity.Name = "labelCommitActivity";
            this.labelCommitActivity.Size = new System.Drawing.Size(44, 13);
            this.labelCommitActivity.TabIndex = 11;
            this.labelCommitActivity.Text = "Activity:";
            // 
            // ComboBoxActivity
            // 
            this.ComboBoxActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxActivity.FormattingEnabled = true;
            this.ComboBoxActivity.Location = new System.Drawing.Point(81, 73);
            this.ComboBoxActivity.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBoxActivity.Name = "ComboBoxActivity";
            this.ComboBoxActivity.Size = new System.Drawing.Size(158, 21);
            this.ComboBoxActivity.TabIndex = 13;
            // 
            // labelCommitQuestion
            // 
            this.labelCommitQuestion.AutoSize = true;
            this.labelCommitQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCommitQuestion.Location = new System.Drawing.Point(6, 9);
            this.labelCommitQuestion.Name = "labelCommitQuestion";
            this.labelCommitQuestion.Size = new System.Drawing.Size(310, 20);
            this.labelCommitQuestion.TabIndex = 14;
            this.labelCommitQuestion.Text = "Do you want to commit the following entry?";
            // 
            // labelCommitTime
            // 
            this.labelCommitTime.AutoSize = true;
            this.labelCommitTime.Location = new System.Drawing.Point(7, 95);
            this.labelCommitTime.Name = "labelCommitTime";
            this.labelCommitTime.Size = new System.Drawing.Size(50, 13);
            this.labelCommitTime.TabIndex = 11;
            this.labelCommitTime.Text = "Time (H):";
            // 
            // labelTimeContent
            // 
            this.labelTimeContent.AutoSize = true;
            this.labelTimeContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeContent.Location = new System.Drawing.Point(78, 95);
            this.labelTimeContent.Name = "labelTimeContent";
            this.labelTimeContent.Size = new System.Drawing.Size(34, 13);
            this.labelTimeContent.TabIndex = 12;
            this.labelTimeContent.Text = "Time";
            // 
            // labelCommitDateSpent
            // 
            this.labelCommitDateSpent.AutoSize = true;
            this.labelCommitDateSpent.Location = new System.Drawing.Point(7, 113);
            this.labelCommitDateSpent.Name = "labelCommitDateSpent";
            this.labelCommitDateSpent.Size = new System.Drawing.Size(55, 13);
            this.labelCommitDateSpent.TabIndex = 11;
            this.labelCommitDateSpent.Text = "Spent On:";
            // 
            // labelDateSpentContent
            // 
            this.labelDateSpentContent.AutoSize = true;
            this.labelDateSpentContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateSpentContent.Location = new System.Drawing.Point(78, 113);
            this.labelDateSpentContent.Name = "labelDateSpentContent";
            this.labelDateSpentContent.Size = new System.Drawing.Size(34, 13);
            this.labelDateSpentContent.TabIndex = 12;
            this.labelDateSpentContent.Text = "Date";
            // 
            // CommitForm
            // 
            this.AcceptButton = this.BtnCommitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(464, 205);
            this.Controls.Add(this.labelCommitQuestion);
            this.Controls.Add(this.ComboBoxActivity);
            this.Controls.Add(this.labelCommitActivity);
            this.Controls.Add(this.labelDateSpentContent);
            this.Controls.Add(this.labelCommitDateSpent);
            this.Controls.Add(this.labelTimeContent);
            this.Controls.Add(this.labelCommitTime);
            this.Controls.Add(this.labelIssueContent);
            this.Controls.Add(this.labelCommitIssue);
            this.Controls.Add(this.labelProjectContent);
            this.Controls.Add(this.labelCommitProject);
            this.Controls.Add(this.TextBoxComment);
            this.Controls.Add(this.labelCommitComment);
            this.Controls.Add(this.BtnCommitButton);
            this.Controls.Add(this.BtnCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CommitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "CommitForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCommitButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.TextBox TextBoxComment;
        private System.Windows.Forms.Label labelCommitComment;
        private System.Windows.Forms.Label labelCommitProject;
        private System.Windows.Forms.Label labelProjectContent;
        private System.Windows.Forms.Label labelCommitIssue;
        private System.Windows.Forms.Label labelIssueContent;
        private System.Windows.Forms.Label labelCommitActivity;
        private System.Windows.Forms.ComboBox ComboBoxActivity;
        private System.Windows.Forms.Label labelCommitQuestion;
        private System.Windows.Forms.Label labelCommitTime;
        private System.Windows.Forms.Label labelTimeContent;
        private System.Windows.Forms.Label labelCommitDateSpent;
        private System.Windows.Forms.Label labelDateSpentContent;
    }
}