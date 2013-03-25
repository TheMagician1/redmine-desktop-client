using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using Redmine.Net.Api.Types;

namespace Redmine.Client.Languages
{
    internal static class LangTools
    {
//        public static CultureInfo language = System.Globalization.CultureInfo.CurrentUICulture;

        public static void UpdateControlsForLanguage(Control.ControlCollection formControls)
        {
            foreach (Control c in formControls)
            {
                if (!String.IsNullOrEmpty(Lang.ResourceManager.GetString(c.Name, Lang.Culture)))
                    c.Text = Lang.ResourceManager.GetString(c.Name, Lang.Culture);
                if (c.Controls.Count != 0)
                    UpdateControlsForLanguage(c.Controls);
            }
        }

        internal static void UpdateControlsForLanguage(ToolStripItemCollection toolStripItems)
        {
            foreach (ToolStripItem i in toolStripItems)
            {
                if (!String.IsNullOrEmpty(Lang.ResourceManager.GetString(i.Name, Lang.Culture)))
                    i.Text = Lang.ResourceManager.GetString(i.Name, Lang.Culture);
            }
        }
        public static string GetTextForApiVersion(ApiVersion apiVersion)
        {
            return Lang.ResourceManager.GetString("ApiVersion_" + apiVersion.ToString(), Lang.Culture);
        }

        public static string CreateUpdatedText(string fieldName, string from, string to)
        {
            string basicFormatString;
            if (from != null && to != null && from != to)
                basicFormatString = Lang.UpdatedField_UpdatedTo;
            else if (from == null && to != null)
                basicFormatString = Lang.UpdatedField_SetTo;
            else if (from != null && to == null)
                basicFormatString = Lang.UpdatedField_Deleted;
            else
                return ""; // nothing changed...
            return String.Format(basicFormatString, Lang.ResourceManager.GetString("IssueField_" + fieldName, Lang.Culture), from, to);
        }
        public static string CreateUpdatedText(string fieldName, IdentifiableName from, IdentifiableName to)
        {
            return CreateUpdatedText(fieldName, from != null ? from.Name : null, to != null ? to.Name : null);
        }
        public static string CreateUpdatedText(string fieldName, DateTime? from, DateTime? to, string fmt)
        {
            return CreateUpdatedText(fieldName, from.HasValue ? from.Value.ToString(fmt, Lang.Culture) : null, to.HasValue ? to.Value.ToString(fmt, Lang.Culture) : null);
        }
        public static string CreateUpdatedText(string fieldName, float? from, float? to, string fmt)
        {
            return CreateUpdatedText(fieldName, from.HasValue ? from.Value.ToString(fmt, Lang.Culture) : null, to.HasValue ? to.Value.ToString(fmt, Lang.Culture) : null);
        }
    }
}
