namespace Redmine.Client
{
    partial class EditEnumForm
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
            this.labelEnumId = new System.Windows.Forms.Label();
            this.EnumNameTextBox = new System.Windows.Forms.TextBox();
            this.EnumIdTextBox = new System.Windows.Forms.MaskedTextBox();
            this.labelEnumName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnOKButton
            // 
            this.BtnOKButton.Location = new System.Drawing.Point(141, 54);
            this.BtnOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOKButton.Name = "BtnOKButton";
            this.BtnOKButton.Size = new System.Drawing.Size(68, 24);
            this.BtnOKButton.TabIndex = 4;
            this.BtnOKButton.Text = "OK";
            this.BtnOKButton.UseVisualStyleBackColor = true;
            this.BtnOKButton.Click += new System.EventHandler(this.BtnOKButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(213, 54);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 5;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            // 
            // labelEnumId
            // 
            this.labelEnumId.AutoSize = true;
            this.labelEnumId.Location = new System.Drawing.Point(9, 13);
            this.labelEnumId.Name = "labelEnumId";
            this.labelEnumId.Size = new System.Drawing.Size(16, 13);
            this.labelEnumId.TabIndex = 0;
            this.labelEnumId.Text = "Id";
            // 
            // EnumNameTextBox
            // 
            this.EnumNameTextBox.Location = new System.Drawing.Point(45, 29);
            this.EnumNameTextBox.Name = "EnumNameTextBox";
            this.EnumNameTextBox.Size = new System.Drawing.Size(235, 20);
            this.EnumNameTextBox.TabIndex = 3;
            // 
            // EnumIdTextBox
            // 
            this.EnumIdTextBox.Location = new System.Drawing.Point(12, 29);
            this.EnumIdTextBox.Mask = "99";
            this.EnumIdTextBox.Name = "EnumIdTextBox";
            this.EnumIdTextBox.Size = new System.Drawing.Size(27, 20);
            this.EnumIdTextBox.TabIndex = 1;
            // 
            // labelEnumName
            // 
            this.labelEnumName.AutoSize = true;
            this.labelEnumName.Location = new System.Drawing.Point(42, 13);
            this.labelEnumName.Name = "labelEnumName";
            this.labelEnumName.Size = new System.Drawing.Size(35, 13);
            this.labelEnumName.TabIndex = 2;
            this.labelEnumName.Text = "Name";
            // 
            // EditEnumForm
            // 
            this.AcceptButton = this.BtnOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(292, 90);
            this.Controls.Add(this.EnumIdTextBox);
            this.Controls.Add(this.EnumNameTextBox);
            this.Controls.Add(this.labelEnumName);
            this.Controls.Add(this.labelEnumId);
            this.Controls.Add(this.BtnOKButton);
            this.Controls.Add(this.BtnCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditEnumForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "EditEnumForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOKButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Label labelEnumId;
        private System.Windows.Forms.TextBox EnumNameTextBox;
        private System.Windows.Forms.MaskedTextBox EnumIdTextBox;
        private System.Windows.Forms.Label labelEnumName;
    }
}