namespace Redmine.Client
{
    partial class TimeEntryForm
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
            this.BtnOKButton = new System.Windows.Forms.Button();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.comboBoxActivity = new System.Windows.Forms.ComboBox();
            this.datePickerSpentOn = new System.Windows.Forms.DateTimePicker();
            this.comboBoxByUser = new System.Windows.Forms.ComboBox();
            this.textBoxSpentHours = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelSpentOn = new System.Windows.Forms.Label();
            this.labelByUser = new System.Windows.Forms.Label();
            this.labelActivity = new System.Windows.Forms.Label();
            this.labelSpentHours = new System.Windows.Forms.Label();
            this.labelComment = new System.Windows.Forms.Label();
            this.labelTimeEntryTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnOKButton
            // 
            this.BtnOKButton.Location = new System.Drawing.Point(82, 202);
            this.BtnOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOKButton.Name = "BtnOKButton";
            this.BtnOKButton.Size = new System.Drawing.Size(68, 24);
            this.BtnOKButton.TabIndex = 6;
            this.BtnOKButton.Text = "OK";
            this.BtnOKButton.UseVisualStyleBackColor = true;
            this.BtnOKButton.Click += new System.EventHandler(this.BtnOKButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(154, 202);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 7;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            // 
            // comboBoxActivity
            // 
            this.comboBoxActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxActivity.FormattingEnabled = true;
            this.comboBoxActivity.Location = new System.Drawing.Point(153, 105);
            this.comboBoxActivity.Name = "comboBoxActivity";
            this.comboBoxActivity.Size = new System.Drawing.Size(121, 21);
            this.comboBoxActivity.TabIndex = 8;
            // 
            // datePickerSpentOn
            // 
            this.datePickerSpentOn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerSpentOn.Location = new System.Drawing.Point(153, 52);
            this.datePickerSpentOn.Name = "datePickerSpentOn";
            this.datePickerSpentOn.Size = new System.Drawing.Size(95, 20);
            this.datePickerSpentOn.TabIndex = 9;
            // 
            // comboBoxByUser
            // 
            this.comboBoxByUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxByUser.Enabled = false;
            this.comboBoxByUser.FormattingEnabled = true;
            this.comboBoxByUser.Location = new System.Drawing.Point(153, 78);
            this.comboBoxByUser.Name = "comboBoxByUser";
            this.comboBoxByUser.Size = new System.Drawing.Size(121, 21);
            this.comboBoxByUser.TabIndex = 10;
            // 
            // textBoxSpentHours
            // 
            this.textBoxSpentHours.Location = new System.Drawing.Point(153, 132);
            this.textBoxSpentHours.Name = "textBoxSpentHours";
            this.textBoxSpentHours.Size = new System.Drawing.Size(121, 20);
            this.textBoxSpentHours.TabIndex = 11;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(34, 177);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(240, 20);
            this.textBoxComment.TabIndex = 12;
            // 
            // labelSpentOn
            // 
            this.labelSpentOn.AutoSize = true;
            this.labelSpentOn.Location = new System.Drawing.Point(31, 58);
            this.labelSpentOn.Name = "labelSpentOn";
            this.labelSpentOn.Size = new System.Drawing.Size(52, 13);
            this.labelSpentOn.TabIndex = 13;
            this.labelSpentOn.Text = "Spent On";
            // 
            // labelByUser
            // 
            this.labelByUser.AutoSize = true;
            this.labelByUser.Location = new System.Drawing.Point(31, 81);
            this.labelByUser.Name = "labelByUser";
            this.labelByUser.Size = new System.Drawing.Size(44, 13);
            this.labelByUser.TabIndex = 13;
            this.labelByUser.Text = "By User";
            // 
            // labelActivity
            // 
            this.labelActivity.AutoSize = true;
            this.labelActivity.Location = new System.Drawing.Point(31, 108);
            this.labelActivity.Name = "labelActivity";
            this.labelActivity.Size = new System.Drawing.Size(41, 13);
            this.labelActivity.TabIndex = 13;
            this.labelActivity.Text = "Activity";
            // 
            // labelSpentHours
            // 
            this.labelSpentHours.AutoSize = true;
            this.labelSpentHours.Location = new System.Drawing.Point(31, 135);
            this.labelSpentHours.Name = "labelSpentHours";
            this.labelSpentHours.Size = new System.Drawing.Size(66, 13);
            this.labelSpentHours.TabIndex = 13;
            this.labelSpentHours.Text = "Spent Hours";
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(31, 161);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(51, 13);
            this.labelComment.TabIndex = 13;
            this.labelComment.Text = "Comment";
            // 
            // labelTimeEntryTitle
            // 
            this.labelTimeEntryTitle.AutoSize = true;
            this.labelTimeEntryTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTimeEntryTitle.Name = "labelTimeEntryTitle";
            this.labelTimeEntryTitle.Size = new System.Drawing.Size(35, 13);
            this.labelTimeEntryTitle.TabIndex = 14;
            this.labelTimeEntryTitle.Text = "label1";
            // 
            // TimeEntryForm
            // 
            this.AcceptButton = this.BtnOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(305, 237);
            this.Controls.Add(this.labelTimeEntryTitle);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.labelSpentHours);
            this.Controls.Add(this.labelActivity);
            this.Controls.Add(this.labelByUser);
            this.Controls.Add(this.labelSpentOn);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.textBoxSpentHours);
            this.Controls.Add(this.comboBoxByUser);
            this.Controls.Add(this.datePickerSpentOn);
            this.Controls.Add(this.comboBoxActivity);
            this.Controls.Add(this.BtnOKButton);
            this.Controls.Add(this.BtnCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TimeEntryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TimeEntryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOKButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.ComboBox comboBoxActivity;
        private System.Windows.Forms.DateTimePicker datePickerSpentOn;
        private System.Windows.Forms.ComboBox comboBoxByUser;
        private System.Windows.Forms.TextBox textBoxSpentHours;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelSpentOn;
        private System.Windows.Forms.Label labelByUser;
        private System.Windows.Forms.Label labelActivity;
        private System.Windows.Forms.Label labelSpentHours;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.Label labelTimeEntryTitle;
    }
}