namespace Redmine.Client
{
    partial class OpenSpecificIssueForm
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
            this.labelIssueNumberToOpen = new System.Windows.Forms.Label();
            this.textBoxIssueNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnOKButton
            // 
            this.BtnOKButton.Location = new System.Drawing.Point(153, 40);
            this.BtnOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOKButton.Name = "BtnOKButton";
            this.BtnOKButton.Size = new System.Drawing.Size(68, 24);
            this.BtnOKButton.TabIndex = 2;
            this.BtnOKButton.Text = "OK";
            this.BtnOKButton.UseVisualStyleBackColor = true;
            this.BtnOKButton.Click += new System.EventHandler(this.BtnOKButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(225, 40);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 3;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            // 
            // labelIssueNumberToOpen
            // 
            this.labelIssueNumberToOpen.AutoSize = true;
            this.labelIssueNumberToOpen.Location = new System.Drawing.Point(12, 15);
            this.labelIssueNumberToOpen.Name = "labelIssueNumberToOpen";
            this.labelIssueNumberToOpen.Size = new System.Drawing.Size(106, 13);
            this.labelIssueNumberToOpen.TabIndex = 0;
            this.labelIssueNumberToOpen.Text = "Issuenumber to open";
            // 
            // textBoxIssueNumber
            // 
            this.textBoxIssueNumber.Location = new System.Drawing.Point(153, 12);
            this.textBoxIssueNumber.MaxLength = 20;
            this.textBoxIssueNumber.Name = "textBoxIssueNumber";
            this.textBoxIssueNumber.Size = new System.Drawing.Size(95, 20);
            this.textBoxIssueNumber.TabIndex = 1;
            this.textBoxIssueNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIssueNumber_KeyPress);
            // 
            // OpenSpecificIssueForm
            // 
            this.AcceptButton = this.BtnOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(304, 75);
            this.Controls.Add(this.textBoxIssueNumber);
            this.Controls.Add(this.labelIssueNumberToOpen);
            this.Controls.Add(this.BtnOKButton);
            this.Controls.Add(this.BtnCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenSpecificIssueForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "OpenSpecificIssueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOKButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Label labelIssueNumberToOpen;
        private System.Windows.Forms.TextBox textBoxIssueNumber;
    }
}