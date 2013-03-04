namespace Redmine.Client
{
    partial class AttachmentForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelAttachmentDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.BtnOKButton = new System.Windows.Forms.Button();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.textBoxAttachmentFilePath = new System.Windows.Forms.TextBox();
            this.labelSelectAttachment = new System.Windows.Forms.Label();
            this.BtnBrowseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "*.*";
            // 
            // labelAttachmentDescription
            // 
            this.labelAttachmentDescription.AutoSize = true;
            this.labelAttachmentDescription.Location = new System.Drawing.Point(9, 58);
            this.labelAttachmentDescription.Name = "labelAttachmentDescription";
            this.labelAttachmentDescription.Size = new System.Drawing.Size(60, 13);
            this.labelAttachmentDescription.TabIndex = 17;
            this.labelAttachmentDescription.Text = "Description";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(12, 74);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(366, 20);
            this.textBoxDescription.TabIndex = 16;
            // 
            // BtnOKButton
            // 
            this.BtnOKButton.Location = new System.Drawing.Point(125, 101);
            this.BtnOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOKButton.Name = "BtnOKButton";
            this.BtnOKButton.Size = new System.Drawing.Size(68, 24);
            this.BtnOKButton.TabIndex = 14;
            this.BtnOKButton.Text = "OK";
            this.BtnOKButton.UseVisualStyleBackColor = true;
            this.BtnOKButton.Click += new System.EventHandler(this.BtnOKButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(197, 101);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 15;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            // 
            // textBoxAttachmentFilePath
            // 
            this.textBoxAttachmentFilePath.Location = new System.Drawing.Point(12, 35);
            this.textBoxAttachmentFilePath.Name = "textBoxAttachmentFilePath";
            this.textBoxAttachmentFilePath.Size = new System.Drawing.Size(293, 20);
            this.textBoxAttachmentFilePath.TabIndex = 16;
            // 
            // labelSelectAttachment
            // 
            this.labelSelectAttachment.AutoSize = true;
            this.labelSelectAttachment.Location = new System.Drawing.Point(12, 19);
            this.labelSelectAttachment.Name = "labelSelectAttachment";
            this.labelSelectAttachment.Size = new System.Drawing.Size(80, 13);
            this.labelSelectAttachment.TabIndex = 17;
            this.labelSelectAttachment.Text = "Attachment File";
            // 
            // BtnBrowseButton
            // 
            this.BtnBrowseButton.Location = new System.Drawing.Point(310, 32);
            this.BtnBrowseButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnBrowseButton.Name = "BtnBrowseButton";
            this.BtnBrowseButton.Size = new System.Drawing.Size(68, 24);
            this.BtnBrowseButton.TabIndex = 15;
            this.BtnBrowseButton.Text = "Browse";
            this.BtnBrowseButton.UseVisualStyleBackColor = true;
            this.BtnBrowseButton.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // AttachmentForm
            // 
            this.AcceptButton = this.BtnOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(390, 136);
            this.Controls.Add(this.labelSelectAttachment);
            this.Controls.Add(this.labelAttachmentDescription);
            this.Controls.Add(this.textBoxAttachmentFilePath);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.BtnOKButton);
            this.Controls.Add(this.BtnBrowseButton);
            this.Controls.Add(this.BtnCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AttachmentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AttachmentForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelAttachmentDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button BtnOKButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.TextBox textBoxAttachmentFilePath;
        private System.Windows.Forms.Label labelSelectAttachment;
        private System.Windows.Forms.Button BtnBrowseButton;
    }
}