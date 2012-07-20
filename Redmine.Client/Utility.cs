using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        private const string currentVersionXmlUrl = "http://redmineclient.sourceforge.net/currentversion.xml";

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
                System.Version latestVersion = new System.Version(doc.SelectSingleNode("//redmineclient/version").InnerText);
                string latestVersionUrl = doc.SelectSingleNode("//redmineclient/url").InnerText;

                if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version < latestVersion)
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
