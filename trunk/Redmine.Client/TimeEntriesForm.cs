using System;
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

        private void AsyncLoadTimeEntries()
        {
            this.Cursor = Cursors.AppStarting;        
            AddBgWork(Lang.BgWork_GetTimeEntries, () =>
                {
                    try
                    {
                        NameValueCollection parameters = new NameValueCollection { { "issue_id", issue.Id.ToString() } };
                        IList<TimeEntry> entries = RedmineClientForm.redmine.GetTotalObjectList<TimeEntry>(parameters);

                        //Let main thread fill form data...
                        return () =>
                        {
                            FillForm(entries);
                            this.Cursor = Cursors.Default;
                        };
                    }
                    catch (Exception e)
                    {
                        //Show the exception in the main thread
                        return () =>
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(String.Format(Lang.Error_Exception, e.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        };
                    }
                });
        }

        private void FillForm(IList<TimeEntry> entries)
        {
            timeEntries = entries;

            DataGridViewTimeEntries.DataSource = timeEntries;
            foreach (DataGridViewColumn column in DataGridViewTimeEntries.Columns)
            {
                if (column.Name != "SpentOn"
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
            DataGridViewTimeEntries.Columns["SpentOn"].DisplayIndex = 0;
            DataGridViewTimeEntries.Columns["Activity"].DisplayIndex = 1;
            DataGridViewTimeEntries.Columns["User"].DisplayIndex = 2;
            DataGridViewTimeEntries.Columns["Hours"].DisplayIndex = 3;
            DataGridViewTimeEntries.Columns["Comments"].DisplayIndex = 4;
            DataGridViewTimeEntries.Columns["UpdatedOn"].DisplayIndex = 5;

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
                    //BtnRefreshButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}
