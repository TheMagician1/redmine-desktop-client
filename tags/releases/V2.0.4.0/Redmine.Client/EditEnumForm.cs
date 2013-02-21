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
    public partial class EditEnumForm : Form
    {
        public enum eFormType
        {
            New,
            Edit,
        };
        public IdentifiableName enumValue;
        private eFormType type;
        private string enumName; // the name of the enumeration set.

        public EditEnumForm(string enumName)
        {
            InitializeComponent();
            this.type = eFormType.New;
            this.enumName = enumName;
            this.enumValue = new IdentifiableName();
            LoadLanguage();
        }
        public EditEnumForm(string enumName, IdentifiableName enumValue)
        {
            InitializeComponent();
            this.type = eFormType.Edit;
            this.enumName = enumName;
            this.enumValue = enumValue;

            EnumIdTextBox.Text = enumValue.Id.ToString();
            EnumNameTextBox.Text = enumValue.Name;
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            LangTools.UpdateControlsForLanguage(this.Controls);
            if (type == eFormType.New)
                this.Text = String.Format(Lang.DlgEditEnumFormTitle_New, enumName);
            else
                this.Text = String.Format(Lang.DlgEditEnumFormTitle_Edit, enumValue.Name, enumName);
        }

        private void BtnOKButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(EnumIdTextBox.Text))
            {
                MessageBox.Show(String.Format(Lang.Error_EnumFieldIsMandatory, labelEnumId.Text), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (String.IsNullOrEmpty(EnumNameTextBox.Text))
            {
                MessageBox.Show(String.Format(Lang.Error_EnumFieldIsMandatory, labelEnumName.Text), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            enumValue.Id = Convert.ToInt32(EnumIdTextBox.Text);
            enumValue.Name = EnumNameTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
