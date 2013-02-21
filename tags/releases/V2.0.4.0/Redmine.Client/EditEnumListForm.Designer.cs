namespace Redmine.Client
{
    partial class EditEnumListForm
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
            this.EnumerationListView = new System.Windows.Forms.ListView();
            this.BtnAddButton = new System.Windows.Forms.Button();
            this.BtnSaveButton = new System.Windows.Forms.Button();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.BtnModifyButton = new System.Windows.Forms.Button();
            this.BtnDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EnumerationListView
            // 
            this.EnumerationListView.FullRowSelect = true;
            this.EnumerationListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.EnumerationListView.HideSelection = false;
            this.EnumerationListView.Location = new System.Drawing.Point(12, 12);
            this.EnumerationListView.MultiSelect = false;
            this.EnumerationListView.Name = "EnumerationListView";
            this.EnumerationListView.Size = new System.Drawing.Size(296, 84);
            this.EnumerationListView.TabIndex = 0;
            this.EnumerationListView.UseCompatibleStateImageBehavior = false;
            this.EnumerationListView.View = System.Windows.Forms.View.Details;
            this.EnumerationListView.VirtualMode = true;
            // 
            // BtnAddButton
            // 
            this.BtnAddButton.Location = new System.Drawing.Point(314, 12);
            this.BtnAddButton.Name = "BtnAddButton";
            this.BtnAddButton.Size = new System.Drawing.Size(68, 24);
            this.BtnAddButton.TabIndex = 1;
            this.BtnAddButton.Text = "Add";
            this.BtnAddButton.UseVisualStyleBackColor = true;
            this.BtnAddButton.Click += new System.EventHandler(this.BtnAddButton_Click);
            // 
            // BtnSaveButton
            // 
            this.BtnSaveButton.Location = new System.Drawing.Point(242, 101);
            this.BtnSaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSaveButton.Name = "BtnSaveButton";
            this.BtnSaveButton.Size = new System.Drawing.Size(68, 24);
            this.BtnSaveButton.TabIndex = 4;
            this.BtnSaveButton.Text = "Save";
            this.BtnSaveButton.UseVisualStyleBackColor = true;
            this.BtnSaveButton.Click += new System.EventHandler(this.BtnSaveButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(314, 101);
            this.BtnCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(68, 24);
            this.BtnCancelButton.TabIndex = 5;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            // 
            // BtnModifyButton
            // 
            this.BtnModifyButton.Location = new System.Drawing.Point(314, 42);
            this.BtnModifyButton.Name = "BtnModifyButton";
            this.BtnModifyButton.Size = new System.Drawing.Size(68, 24);
            this.BtnModifyButton.TabIndex = 2;
            this.BtnModifyButton.Text = "Modify";
            this.BtnModifyButton.UseVisualStyleBackColor = true;
            this.BtnModifyButton.Click += new System.EventHandler(this.BtnModifyButton_Click);
            // 
            // BtnDeleteButton
            // 
            this.BtnDeleteButton.Location = new System.Drawing.Point(314, 72);
            this.BtnDeleteButton.Name = "BtnDeleteButton";
            this.BtnDeleteButton.Size = new System.Drawing.Size(68, 24);
            this.BtnDeleteButton.TabIndex = 3;
            this.BtnDeleteButton.Text = "Delete";
            this.BtnDeleteButton.UseVisualStyleBackColor = true;
            this.BtnDeleteButton.Click += new System.EventHandler(this.BtnDeleteButton_Click);
            // 
            // EditEnumListForm
            // 
            this.AcceptButton = this.BtnSaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(393, 136);
            this.Controls.Add(this.BtnSaveButton);
            this.Controls.Add(this.BtnCancelButton);
            this.Controls.Add(this.BtnDeleteButton);
            this.Controls.Add(this.BtnModifyButton);
            this.Controls.Add(this.BtnAddButton);
            this.Controls.Add(this.EnumerationListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditEnumListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit Enumerations";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView EnumerationListView;
        private System.Windows.Forms.Button BtnAddButton;
        private System.Windows.Forms.Button BtnSaveButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Button BtnModifyButton;
        private System.Windows.Forms.Button BtnDeleteButton;
    }
}