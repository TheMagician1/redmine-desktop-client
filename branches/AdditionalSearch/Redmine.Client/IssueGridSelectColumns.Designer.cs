namespace Redmine.Client
{
    partial class IssueGridSelectColumns
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
            this.SuspendLayout();
            // 
            // BtnOKButton
            // 
            this.BtnOKButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnOKButton.Location = new System.Drawing.Point(83, 210);
            this.BtnOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOKButton.Name = "BtnOKButton";
            this.BtnOKButton.Size = new System.Drawing.Size(68, 24);
            this.BtnOKButton.TabIndex = 16;
            this.BtnOKButton.Text = "Commit";
            this.BtnOKButton.UseVisualStyleBackColor = true;
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(155, 210);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 17;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            // 
            // IssueGridSelectColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 245);
            this.Controls.Add(this.BtnOKButton);
            this.Controls.Add(this.BtnCancelButton);
            this.Name = "IssueGridSelectColumns";
            this.Text = "Select Columns";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOKButton;
        private System.Windows.Forms.Button BtnCancelButton;
    }
}