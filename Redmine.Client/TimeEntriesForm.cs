﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class TimeEntriesForm : BgWorker
    {
        private Issue issue;
        private IList<TimeEntry> timeEntries;
        private IList<ProjectMember> projectMembers;

        public TimeEntriesForm(Issue issue, IList<ProjectMember> projectMembers)
        {
            this.issue = issue;
            this.projectMembers = projectMembers;
            InitializeComponent();
            this.Text = String.Format(Lang.DlgTimeEntriesTitle, issue.Id, issue.Subject);
            LangTools.UpdateControlsForLanguage(this.Controls);
        }

        private void TimeEntriesForm_Load(object sender, EventArgs e)
        {
            AsyncLoadTimeEntries();
        }

        private async void AsyncLoadTimeEntries()
        {
            this.Cursor = Cursors.AppStarting;
            NameValueCollection parameters = new NameValueCollection { { "issue_id", issue.Id.ToString() } };
            try
            {
                FillForm(await AddBgWork(Lang.BgWork_GetTimeEntries, () => RedmineClientForm.redmine.GetObjects<TimeEntry>(parameters)));
            }
            catch (Exception e)
            {
                //Show the exception in the main thread
                this.Cursor = Cursors.Default;
                MessageBox.Show(String.Format(Lang.Error_Exception, e.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Cursor = Cursors.Default;
        }

        private void FillForm(IList<TimeEntry> entries)
        {
            timeEntries = entries;

            DataGridViewTimeEntries.DataSource = timeEntries;
            foreach (DataGridViewColumn column in DataGridViewTimeEntries.Columns)
            {
                if (column.Name != "Id"
                    && column.Name != "SpentOn"
                    && column.Name != "Activity"
                    && column.Name != "Hours"
                    && column.Name != "User"
                    && column.Name != "Comments"
                    && column.Name != "UpdatedOn")
                {
                    column.Visible = false;
                }
            }
            try // Very ugly trick to fix the mono crash reported in the SF.net forum
            {
                DataGridViewTimeEntries.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception) { }
            if (DataGridViewTimeEntries.Columns.Count > 0)
            {
                DataGridViewTimeEntries.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            DataGridViewTimeEntries.RowHeadersWidth = 20;
            DataGridViewTimeEntries.Columns["Id"].DisplayIndex = 0;
            DataGridViewTimeEntries.Columns["SpentOn"].DisplayIndex = 1;
            DataGridViewTimeEntries.Columns["Activity"].DisplayIndex = 2;
            DataGridViewTimeEntries.Columns["User"].DisplayIndex = 3;
            DataGridViewTimeEntries.Columns["Hours"].DisplayIndex = 4;
            DataGridViewTimeEntries.Columns["Comments"].DisplayIndex = 5;
            DataGridViewTimeEntries.Columns["UpdatedOn"].DisplayIndex = 6;

        }

        private void DataGridViewTimeEntries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is IdentifiableName)
                e.Value = ((IdentifiableName)e.Value).Name;
        }

        private void BtnOKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DataGridViewTimeEntries_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TimeEntry timeEntry = (TimeEntry)DataGridViewTimeEntries.Rows[e.RowIndex].DataBoundItem;
            try
            {
                TimeEntryForm dlg = new TimeEntryForm(issue, projectMembers, timeEntry);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    AsyncLoadTimeEntries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                TimeEntryForm dlg = new TimeEntryForm(issue, projectMembers);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    AsyncLoadTimeEntries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnModifyButton_Click(object sender, EventArgs e)
        {
            if (DataGridViewTimeEntries.SelectedRows.Count <= 0)
                return;

            try
            {
                TimeEntry timeEntry = (TimeEntry)DataGridViewTimeEntries.SelectedRows[0].DataBoundItem;
                TimeEntryForm dlg = new TimeEntryForm(issue, projectMembers, timeEntry);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    AsyncLoadTimeEntries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private async void BtnDeleteButton_Click(object sender, EventArgs e)
        {
            if (DataGridViewTimeEntries.SelectedRows.Count <= 0)
                return;

            try
            {
                TimeEntry timeEntry = (TimeEntry)DataGridViewTimeEntries.SelectedRows[0].DataBoundItem;
                if (MessageBox.Show(String.Format(Lang.Warning_AreYouSureDeleteTimeEntry, timeEntry.Id), Lang.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                    return;

                await AddBgWork("Delete TimeEntry", () => RedmineClientForm.redmine.DeleteObject<TimeEntry>(timeEntry.Id.ToString(), null));
                AsyncLoadTimeEntries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}
