
using System;
using System.Collections.Generic;
using System.Text;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Redmine.Client
{
    internal static class Enumerations
    {
        public static List<IdentifiableName> IssuePriorities { get; set; }
        public static List<IdentifiableName> DocumentCategories { get; set; }
        public static List<IdentifiableName> Activities { get; set; }

        public static void LoadAll()
        {
            LoadDocumentCategories();
            LoadIssuePriorities();
            LoadActivities();
        }

        public static void LoadDocumentCategories()
        {
            bool loadDefault = false;
            try
            {
                DocumentCategories = Load("DocumentCategories");
            }
            catch (FileNotFoundException) { loadDefault = true; }
            catch (XmlException) { loadDefault = true; }
            if (loadDefault)
            {
                DocumentCategories = new List<IdentifiableName> {
                    new IdentifiableName { Id = 1, Name = "User Documentation" },
                    new IdentifiableName { Id = 2, Name = "Technical Documentation" }
                                       };
            }
        }
        public static void LoadIssuePriorities()
        {
            bool loadDefault = false;
            try
            {
                IssuePriorities = Load("IssuePriorities");
            }
            catch (FileNotFoundException) { loadDefault = true; }
            catch (XmlException) { loadDefault = true; }
            if (loadDefault)
            {
                IssuePriorities = new List<IdentifiableName> {
                    new IdentifiableName { Id = 3, Name = "Low" },
                    new IdentifiableName { Id = 4, Name = "Normal" },
                    new IdentifiableName { Id = 5, Name = "High" },
                    new IdentifiableName { Id = 6, Name = "Urgent" },
                    new IdentifiableName { Id = 7, Name = "Immediate" }
                                        };
            }
        }
        public static void LoadActivities()
        {
            bool loadDefault = false;
            try
            {
                Activities = Load("Activities");
            }
            catch (FileNotFoundException) { loadDefault = true; }
            catch (XmlException) { loadDefault = true; }
            if (loadDefault)
            {
                Activities = new List<IdentifiableName> {
                    new IdentifiableName { Id = 8, Name = "Design" },
                    new IdentifiableName { Id = 9, Name = "Development" }
                                       };
            }
        }

        public static List<IdentifiableName> Load(string listName)
        {
            string fileName = Application.StartupPath + "\\" + listName + ".xml";
            FileStream f = File.OpenRead(fileName);
            using (var xmlReader = new XmlTextReader(f))
            {
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                xmlReader.Read();
                return xmlReader.ReadElementContentAsCollection<IdentifiableName>();
            }
        }

        public static void SaveAll()
        {
            SaveDocumentCategories();
            SaveIssuePriorities();
            SaveActivities();
        }

        public static void SaveDocumentCategories()
        {
            Save(DocumentCategories, "DocumentCategories");
        }
        public static void SaveIssuePriorities()
        {
            Save(IssuePriorities, "IssuePriorities");
        }
        public static void SaveActivities()
        {
            Save(Activities, "Activities");
        }

        public static void Save(IList<IdentifiableName> list, string listName)
        {
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true };
            string fileName = Application.StartupPath + "\\" + listName + ".xml";

            File.Delete(fileName);
            FileStream f = File.OpenWrite(fileName);
            using (var xmlWriter = XmlWriter.Create(f, xws))
            {
                xmlWriter.WriteCollectionAsElement(list, listName);
            }
            f.Close();
        }
    }

}
