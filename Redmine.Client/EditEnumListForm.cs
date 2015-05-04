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

        public List<Enumerations.EnumerationItem> enumeration { get; private set; }
        private string enumName;

        public EditEnumListForm(List<Enumerations.EnumerationItem> enumeration, string enumName)
        {
            InitializeComponent();
            this.enumName = enumName;
            this.enumeration = enumeration;

            EnumerationListView.Columns.Add("ColumnId", Lang.labelEnumId, 25);
            EnumerationListView.Columns.Add("ColumnName", Lang.labelEnumName, 150);
            EnumerationListView.Columns.Add("ColumnIsDefault", Lang.labelEnumIsDefault, 150);
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
            Enumerations.EnumerationItem item = enumeration[e.ItemIndex];
            e.Item = new ListViewItem();
            e.Item.Text = item.Id.ToString();
            e.Item.SubItems.Add(item.Name);
            e.Item.SubItems.Add(item.IsDefault.ToString());
        }

        private void BtnAddButton_Click(object sender, EventArgs e)
        {
            EditEnumForm dlg = new EditEnumForm(enumName, EditEnumForm.eFormType.New, new Enumerations.EnumerationItem { Id = 0, Name = "", IsDefault = false });
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

        private void AddItem(Enumerations.EnumerationItem item)
        {
            enumeration.Add(item);
            EnumerationListView.VirtualListSize = enumeration.Count;
        }

        private void DeleteItem(Enumerations.EnumerationItem item)
        {
            enumeration.Remove(item);
            EnumerationListView.VirtualListSize = enumeration.Count;
        }

        private bool IsUnique(Enumerations.EnumerationItem item)
        {
            return IsUnique(item, null);
        }

        private bool IsUnique(Enumerations.EnumerationItem item, Enumerations.EnumerationItem itemOri)
        {
            foreach (Enumerations.EnumerationItem real in enumeration)
            {
                if (itemOri != null)
                    if (itemOri == real)
                        continue;
                if (real.Id == item.Id ||
                    String.Compare(item.Name, item.Name, true) == 0 ||
                    real.IsDefault == item.IsDefault)
                    return false;
            }
            return true;
        }

        private Enumerations.EnumerationItem GetCurrentSelectedItem()
        {
            if (EnumerationListView.SelectedIndices.Count != 1)
                return null;
            return enumeration[EnumerationListView.SelectedIndices[0]];
        }

        private void BtnDeleteButton_Click(object sender, EventArgs e)
        {
            Enumerations.EnumerationItem item = GetCurrentSelectedItem();
            if (item == null)
                return;
            DeleteItem(item);
        }

        private void BtnModifyButton_Click(object sender, EventArgs e)
        {
            Enumerations.EnumerationItem item = GetCurrentSelectedItem();
            if (item == null)
                return;
            EditEnumForm dlg = new EditEnumForm(enumName, EditEnumForm.eFormType.Edit, item);
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

        private void UpdateItem(Enumerations.EnumerationItem newItem, Enumerations.EnumerationItem original)
        {
            foreach (Enumerations.EnumerationItem item in enumeration)
            {
                if (original == item)
                {
                    item.Id = newItem.Id;
                    item.Name = newItem.Name;
                    item.IsDefault = newItem.IsDefault;
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
