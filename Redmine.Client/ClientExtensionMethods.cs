using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using System.Collections.Specialized;

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

        public static string CompleteName(this User user)
        {
            string completeName = "";
            if (!String.IsNullOrEmpty(user.FirstName))
                completeName += user.FirstName;
            if (!String.IsNullOrEmpty(user.LastName))
            {
                if (!String.IsNullOrEmpty(completeName))
                    completeName += " ";
                completeName += user.LastName;
            }
            return completeName;
        }

        public static String ToByteString(this long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public static String ToByteString(this int byteCount)
        {
            long bytes = byteCount;
            return bytes.ToByteString();
        }

        public static void MoveControl(this System.Windows.Forms.Control control, int diffx, int diffy)
        {
            System.Drawing.Point loc = control.Location;
            loc.X += diffx;
            loc.Y += diffy;
            control.Location = loc;
        }

    }
}
