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
    public partial class EditEnumListForm : Form
    {

        public List<IdentifiableName> enumeration { get; private set; }
        private string enumName;

        public EditEnumListForm(List<IdentifiableName> enumeration, string enumName)
        {
            InitializeComponent();
            this.enumName = enumName;
            this.enumeration = enumeration;

            EnumerationListView.Columns.Add("ColumnId", Lang.labelEnumId, 25);
            EnumerationListView.Columns.Add("ColumnName", Lang.labelEnumName, 150);
            EnumerationListView.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(EnumerationListView_RetrieveVirtualItem);
            EnumerationListView.SelectedIndexChanged += new EventHandler(EnumerationListView_SelectedIndexChanged);
            EnumerationListView.MouseDoubleClick += new MouseEventHandler(EnumerationListView_MouseDoubleClick);
            EnumerationListView.VirtualListSize = enumeration.Count;
        
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            LangTools.UpdateControlsForLanguage(this.Controls);
            this.Text = String.Format(Lang.DlgEditEnumListFormTitle, enumName);
        }

        void EnumerationListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BtnModifyButton_Click(null, null);
        }

        void EnumerationListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDeleteButton.Enabled = EnumerationListView.SelectedIndices.Count != 0;
            BtnModifyButton.Enabled = EnumerationListView.SelectedIndices.Count != 0;
        }

        public void EnumerationListView_RetrieveVirtualItem(object Sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < 0 || e.ItemIndex >= enumeration.Count)
                return;
            IdentifiableName item = enumeration[e.ItemIndex];
            e.Item = new ListViewItem();
            e.Item.Text = item.Id.ToString();
            e.Item.SubItems.Add(item.Name);
        }

        private void BtnAddButton_Click(object sender, EventArgs e)
        {
            EditEnumForm dlg = new EditEnumForm(enumName);
            do
            {
                DialogResult result = dlg.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;
                if (IsUnique(dlg.enumValue))
                {
                    AddItem(dlg.enumValue);
                    return;
                }
                if (MessageBox.Show(Lang.Error_EnumMustBeUnique, Lang.Error, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;
            }
            while (true);
        }

        private void AddItem(IdentifiableName identifiableName)
        {
            enumeration.Add(identifiableName);
            EnumerationListView.VirtualListSize = enumeration.Count;
        }

        private void DeleteItem(IdentifiableName identifiableName)
        {
            enumeration.Remove(identifiableName);
            EnumerationListView.VirtualListSize = enumeration.Count;
        }

        private bool IsUnique(IdentifiableName identifiableName)
        {
            return IsUnique(identifiableName, null);
        }

        private bool IsUnique(IdentifiableName identifiableName, IdentifiableName identifiableNameOri)
        {
            foreach (IdentifiableName item in enumeration)
            {
                if (identifiableNameOri != null)
                    if (identifiableNameOri == item)
                        continue;
                if (item.Id == identifiableName.Id ||
                    String.Compare(item.Name, identifiableName.Name, true) == 0)
                    return false;
            }
            return true;
        }

        private IdentifiableName GetCurrentSelectedItem()
        {
            if (EnumerationListView.SelectedIndices.Count != 1)
                return null;
            return enumeration[EnumerationListView.SelectedIndices[0]];
        }

        private void BtnDeleteButton_Click(object sender, EventArgs e)
        {
            IdentifiableName item = GetCurrentSelectedItem();
            if (item == null)
                return;
            DeleteItem(item);
        }

        private void BtnModifyButton_Click(object sender, EventArgs e)
        {
            IdentifiableName item = GetCurrentSelectedItem();
            if (item == null)
                return;
            EditEnumForm dlg = new EditEnumForm(enumName, item);
            do
            {
                DialogResult result = dlg.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;
                if (IsUnique(dlg.enumValue, item))
                {
                    UpdateItem(dlg.enumValue, item);
                    return;
                }
                if (MessageBox.Show(Lang.Error_EnumMustBeUnique, Lang.Error, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;
            }
            while (true);
        }

        private void UpdateItem(IdentifiableName identifiableName, IdentifiableName original)
        {
            foreach (IdentifiableName item in enumeration)
            {
                if (original == item)
                {
                    item.Id = identifiableName.Id;
                    item.Name = identifiableName.Name;
                    EnumerationListView.Invalidate();
                    return;
                }
            }
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
