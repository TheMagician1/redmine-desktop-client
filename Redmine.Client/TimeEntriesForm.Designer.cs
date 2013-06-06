namespace Redmine.Client
{
    partial class TimeEntriesForm
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
            this.DataGridViewTimeEntries = new System.Windows.Forms.DataGridView();
            this.BtnDeleteButton = new System.Windows.Forms.Button();
            this.BtnModifyButton = new System.Windows.Forms.Button();
            this.BtnAddButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewTimeEntries)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnOKButton
            // 
            this.BtnOKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnOKButton.Location = new System.Drawing.Point(192, 235);
            this.BtnOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOKButton.Name = "BtnOKButton";
            this.BtnOKButton.Size = new System.Drawing.Size(68, 24);
            this.BtnOKButton.TabIndex = 26;
            this.BtnOKButton.Text = "OK";
            this.BtnOKButton.UseVisualStyleBackColor = true;
            this.BtnOKButton.Click += new System.EventHandler(this.BtnOKButton_Click);
            // 
            // DataGridViewTimeEntries
            // 
            this.DataGridViewTimeEntries.AllowUserToAddRows = false;
            this.DataGridViewTimeEntries.AllowUserToDeleteRows = false;
            this.DataGridViewTimeEntries.AllowUserToResizeRows = false;
            this.DataGridViewTimeEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewTimeEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewTimeEntries.Location = new System.Drawing.Point(12, 12);
            this.DataGridViewTimeEntries.MultiSelect = false;
            this.DataGridViewTimeEntries.Name = "DataGridViewTimeEntries";
            this.DataGridViewTimeEntries.ReadOnly = true;
            this.DataGridViewTimeEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewTimeEntries.Size = new System.Drawing.Size(354, 218);
            this.DataGridViewTimeEntries.TabIndex = 28;
            this.DataGridViewTimeEntries.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewTimeEntries_CellFormatting);
            this.DataGridViewTimeEntries.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewTimeEntries_CellMouseDoubleClick);
            // 
            // BtnDeleteButton
            // 
            this.BtnDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDeleteButton.Location = new System.Drawing.Point(372, 72);
            this.BtnDeleteButton.Name = "BtnDeleteButton";
            this.BtnDeleteButton.Size = new System.Drawing.Size(68, 24);
            this.BtnDeleteButton.TabIndex = 31;
            this.BtnDeleteButton.Text = "Delete";
            this.BtnDeleteButton.UseVisualStyleBackColor = true;
            this.BtnDeleteButton.Click += new System.EventHandler(this.BtnDeleteButton_Click);
            // 
            // BtnModifyButton
            // 
            this.BtnModifyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnModifyButton.Location = new System.Drawing.Point(372, 42);
            this.BtnModifyButton.Name = "BtnModifyButton";
            this.BtnModifyButton.Size = new System.Drawing.Size(68, 24);
            this.BtnModifyButton.TabIndex = 30;
            this.BtnModifyButton.Text = "Modify";
            this.BtnModifyButton.UseVisualStyleBackColor = true;
            this.BtnModifyButton.Click += new System.EventHandler(this.BtnModifyButton_Click);
            // 
            // BtnAddButton
            // 
            this.BtnAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddButton.Location = new System.Drawing.Point(372, 12);
            this.BtnAddButton.Name = "BtnAddButton";
            this.BtnAddButton.Size = new System.Drawing.Size(68, 24);
            this.BtnAddButton.TabIndex = 29;
            this.BtnAddButton.Text = "Add";
            this.BtnAddButton.UseVisualStyleBackColor = true;
            this.BtnAddButton.Click += new System.EventHandler(this.BtnAddButton_Click);
            // 
            // TimeEntriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 270);
            this.Controls.Add(this.BtnDeleteButton);
            this.Controls.Add(this.BtnModifyButton);
            this.Controls.Add(this.BtnAddButton);
            this.Controls.Add(this.DataGridViewTimeEntries);
            this.Controls.Add(this.BtnOKButton);
            this.Name = "TimeEntriesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TimeEntriesForm";
            this.Load += new System.EventHandler(this.TimeEntriesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewTimeEntries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOKButton;
        private System.Windows.Forms.DataGridView DataGridViewTimeEntries;
        private System.Windows.Forms.Button BtnDeleteButton;
        private System.Windows.Forms.Button BtnModifyButton;
        private System.Windows.Forms.Button BtnAddButton;
    }
}