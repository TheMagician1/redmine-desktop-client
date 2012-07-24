using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Redmine.Client
{

    /// <summary>
    /// Utility class providing helper functions for the application
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// URL of the current version XML file
        /// </summary>
        private const string currentVersionXmlUrl = "https://redmine-desktop-client.googlecode.com/svn/tags/releases/latestversion.xml";

        /// <summary>
        /// checks for updates
        /// </summary>
        /// <returns>url for downloading the latest version or empty string</returns>
        internal static string CheckForUpdate()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new XmlTextReader(currentVersionXmlUrl));
                Version latestVersion = new Version(doc.SelectSingleNode("//redmineclient/version").InnerText);
                string latestVersionUrl = doc.SelectSingleNode("//redmineclient/url").InnerText;
                Version myVersion = new Version(FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath).FileVersion);
                if (myVersion < latestVersion)
                {
                    return latestVersionUrl;
                }
            }
            catch (Exception) // we do not care about the errors, we will simply try it the next time around
            {
            }

            return String.Empty;   
        }
    }
}
