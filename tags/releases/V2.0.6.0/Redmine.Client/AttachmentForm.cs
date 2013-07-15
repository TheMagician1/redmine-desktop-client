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
using System.IO;
using Microsoft.Win32;

namespace Redmine.Client
{
    public partial class AttachmentForm : Form
    {
        private DialogType type;
        private Issue issue;
        public Attachment NewAttachment { get; private set; }

        public AttachmentForm(Issue issue, DialogType type, string path)
        {
            InitializeComponent();
            this.type = type;
            this.issue = issue;
            textBoxAttachmentFilePath.Text = path;
            LangTools.UpdateControlsForLanguage(this.Controls);
            this.Text = String.Format(Lang.DlgAttachmentTitle, issue.Id, issue.Subject);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = textBoxAttachmentFilePath.Text;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                textBoxAttachmentFilePath.Text = openFileDialog.FileName;
        }

        private void BtnOKButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (type == DialogType.Edit)
                {
                    byte[] file = File.ReadAllBytes(textBoxAttachmentFilePath.Text);
                    Upload uploadedFile = RedmineClientForm.redmine.UploadData(file);
                    uploadedFile.FileName = Path.GetFileName(textBoxAttachmentFilePath.Text);
                    uploadedFile.Description = textBoxDescription.Text;
                    uploadedFile.ContentType = GetMimeType(Path.GetExtension(textBoxAttachmentFilePath.Text));
                    issue.Uploads = new List<Upload>();
                    issue.Uploads.Add(uploadedFile);
                    RedmineClientForm.redmine.UpdateObject<Issue>(issue.Id.ToString(), issue);
                }
                else
                {
                    NewAttachment = new Attachment
                    {
                        ContentUrl = textBoxAttachmentFilePath.Text,
                        Description = textBoxDescription.Text,
                        FileName = Path.GetFileName(textBoxAttachmentFilePath.Text),
                        ContentType = GetMimeType(Path.GetExtension(textBoxAttachmentFilePath.Text)),
                        FileSize = (int)new FileInfo(textBoxAttachmentFilePath.Text).Length,
                        Author = new IdentifiableName { Id = RedmineClientForm.Instance.CurrentUser.Id, Name = RedmineClientForm.Instance.CurrentUser.CompleteName() }
                    };
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Lang.Error_Exception, ex.Message), Lang.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static string GetMimeType(string extension)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(extension.ToLower());

            if(regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if(contentType != null)
                    mimeType = contentType.ToString();
            }

            return mimeType;
        }

    }
}
