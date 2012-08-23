using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Client
{
    public static class ClientExtensionMethods
    {
        public static void WriteCollectionAsElement<T>(this XmlWriter writer, IList<T> list, string listname) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            writer.WriteStartElement(listname);
            foreach (T element in list)
            {
                serializer.Serialize(writer, element);
            }
            writer.WriteEndElement();
        }
    }
}
