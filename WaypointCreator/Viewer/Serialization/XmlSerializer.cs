#region

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

namespace WaypointCreator.Viewer.Serialization
{
    public class XMLSerializer
    {
        #region Public Properties

        public static bool PrettyPrint { get; set; } = true;

        public static string TargetNamespace => "http://www.w3.org/2001/XMLSchema";

        #endregion

        #region Public Methods and Operators

        public static object FromXml(string xml, Type type)
        {
            var xsr = new XmlSerializer(type);

            using (var sr = new StringReader(xml))
            {
                using (var xtr = new XmlTextReader(sr))
                {
                    if (xsr.CanDeserialize(xtr))
                    {
                        var obj = xsr.Deserialize(xtr);
                        xtr.Close();
                        sr.Close();
                        return obj;
                    }
                }
            }
            return null;
        }

        public static XmlSerializerNamespaces GetNamespaces(bool supportNamespaces)
        {
            var ns = new XmlSerializerNamespaces();
            if (!supportNamespaces)
            {
                ns.Add(String.Empty, String.Empty);
            }
            return ns;
        }

        public static T ReadObjectFromXmlFile<T>(string filename, Type type, Encoding encoding)
        {
            using (var sr = new StreamReader(filename, encoding))
            {
                using (var xtr = new XmlTextReader(sr))
                {
                    var xsr = new XmlSerializer(type);
                    var obj = xsr.Deserialize(xtr);
                    xtr.Close();
                    sr.Close();
                    return (T)obj;
                }
            }
        }

        public static T ReadObjectFromXmlFileAscii<T>(string filename, Type type)
        {
            return ReadObjectFromXmlFile<T>(filename, type, Encoding.ASCII);
        }

        public static T ReadObjectFromXmlFileUnicode<T>(string filename, Type type)
        {
            return ReadObjectFromXmlFile<T>(filename, type, Encoding.Unicode);
        }

        public static string ToXml(object obj)
        {
            var type = obj.GetType();
            var supressnamespaces = DoesSuportNamespaces(type);

            var xsr = new XmlSerializer(type);

            using (var ms = new MemoryStream())
            {
                using (var xtw = new XmlTextWriter(ms, Encoding.Unicode))
                {
                    if (PrettyPrint)
                    {
                        xtw.Formatting = Formatting.Indented;
                        xtw.Indentation = 1;
                        xtw.IndentChar = Convert.ToChar(9);
                    }

                    xtw.Namespaces = true;

                    xsr.Serialize(xtw, obj, GetNamespaces(supressnamespaces));

                    xtw.Close();
                    ms.Close();

                    var xml = Encoding.Unicode.GetString(ms.GetBuffer());
                    xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                    return xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                }
            }
        }

        public static void WriteObjectToXmlFile(object obj, string filename, Encoding encoding)
        {
            using (var xtw = new XmlTextWriter(filename, encoding))
            {
                var xsr = new XmlSerializer(obj.GetType());
                xsr.Serialize(xtw, obj);
                xtw.Close();
            }
        }

        public static void WriteObjectToXmlFileAscii(object obj, string filename)
        {
            WriteObjectToXmlFile(obj, filename, Encoding.ASCII);
        }

        public static void WriteObjectToXmlFileUnicode(object obj, string filename)
        {
            WriteObjectToXmlFile(obj, filename, Encoding.Unicode);
        }

        #endregion

        #region Methods

        private static bool DoesSuportNamespaces(Type type)
        {
            var attr = (XmlTypeAttribute)Attribute.GetCustomAttribute(type, typeof(XmlTypeAttribute));
            return attr != null;
        }

        #endregion
    }
}