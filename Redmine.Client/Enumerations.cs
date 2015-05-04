
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Redmine.Client
{
    /// <summary>
    /// The enumerations we need that are not available through the Redmine API
    /// </summary>
    public static class Enumerations
    {
        [XmlRoot("enumeration_item")]
        public class EnumerationItem : IXmlSerializable, IEquatable<EnumerationItem>
        {
            [XmlElement("id")]
            public int Id { get; set; }
            [XmlElement("name")]
            public string Name { get; set; }
            [XmlElement("is_default")]
            public bool IsDefault { get; set; }

            public IdentifiableName ToIdentifiableName()
            {
                return new IdentifiableName() { Id = this.Id, Name = this.Name };
            }

            #region Implementation of IXmlSerializable

            public XmlSchema GetSchema() { return null; }

            /// <summary>
            /// Generates an object from its XML representation.
            /// </summary>
            /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
            public void ReadXml(XmlReader reader)
            {
                reader.Read();
                while (!reader.EOF)
                {
                    if (reader.IsEmptyElement && !reader.HasAttributes)
                    {
                        reader.Read();
                        continue;
                    }

                    switch (reader.Name)
                    {
                        case "id": Id = reader.ReadElementContentAsInt(); break;

                        case "name": Name = reader.ReadElementContentAsString(); break;

                        case "is_default": IsDefault = reader.ReadElementContentAsBoolean(); break;

                        default: reader.Read(); break;
                    }
                }
            }

            public void WriteXml(XmlWriter writer)
            {
                writer.WriteAttributeString("id", Id.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("name", Name);
                writer.WriteAttributeString("is_default", IsDefault.ToString(CultureInfo.InvariantCulture));
            }

            #endregion

            #region Implementation of IEquatable<EnumerationItem>

            public bool Equals(EnumerationItem other)
            {
                if (other == null) return false;

                return Id == other.Id && Name == other.Name && IsDefault == other.IsDefault;
            }

            #endregion
        };

        /// <summary>
        /// Priorities that can be used in issues
        /// </summary>
        public static List<EnumerationItem> IssuePriorities { get; set; }
        /// <summary>
        /// Activities that can be used to commit time
        /// </summary>
        public static List<EnumerationItem> Activities { get; set; }

        /// <summary>
        /// Load all enumerations from file
        /// </summary>
        public static void LoadAll()
        {
            LoadIssuePriorities();
            LoadActivities();
        }

        public static void LoadIssuePriorities()
        {
            bool loadDefault = false;
            try
            {
                IssuePriorities = Load("IssuePriorities");
            }
            catch (Exception) { loadDefault = true; }
            if (loadDefault)
            {
                IssuePriorities = new List<EnumerationItem> {
                    new EnumerationItem { Id = 3, Name = "Low", IsDefault = false },
                    new EnumerationItem { Id = 4, Name = "Normal", IsDefault = true },
                    new EnumerationItem { Id = 5, Name = "High", IsDefault = false },
                    new EnumerationItem { Id = 6, Name = "Urgent", IsDefault = false },
                    new EnumerationItem { Id = 7, Name = "Immediate", IsDefault = false }
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
            catch (Exception) { loadDefault = true; }
            if (loadDefault)
            {
                Activities = new List<EnumerationItem> {
                    new EnumerationItem { Id = 8, Name = "Design", IsDefault = true },
                    new EnumerationItem { Id = 9, Name = "Development", IsDefault = false }
                                       };
            }
        }

        public static List<EnumerationItem> Load(string listName)
        {
            string fileName = Application.CommonAppDataPath + "\\" + listName + ".xml";
            FileStream f = File.OpenRead(fileName);
            List<EnumerationItem> list = new List<EnumerationItem>();
            using (var xmlReader = new XmlTextReader(f))
            {
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                xmlReader.Read();
                list = xmlReader.ReadElementContentAsCollection<EnumerationItem>();
            }
            return list;
        }

        public static void SaveAll()
        {
            SaveIssuePriorities();
            SaveActivities();
        }

        public static void SaveIssuePriorities()
        {
            Save(IssuePriorities, "IssuePriorities");
        }
        public static void SaveActivities()
        {
            Save(Activities, "Activities");
        }

        public static void Save(IList<EnumerationItem> list, string listName)
        {
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true };
            string fileName = Application.CommonAppDataPath + "\\" + listName + ".xml";

            File.Delete(fileName);
            FileStream f = File.OpenWrite(fileName);
            using (var xmlWriter = XmlWriter.Create(f, xws))
            {
                xmlWriter.WriteCollectionAsElement(list, listName);
            }
            f.Close();
        }

        public static void UpdateActivities(IList<TimeEntryActivity> timeEntryActivities)
        {
            Activities.Clear();
            foreach (TimeEntryActivity ta in timeEntryActivities)
                Activities.Add(new EnumerationItem { Id = ta.Id, Name = ta.Name, IsDefault = ta.IsDefault } );
        }

        public static void UpdateIssuePriorities(IList<IssuePriority> issuePriorities)
        {
            IssuePriorities.Clear();
            foreach (IssuePriority ip in issuePriorities)
                IssuePriorities.Add(new EnumerationItem { Id = ip.Id, Name = ip.Name, IsDefault = ip.IsDefault });
        }

    }

}
