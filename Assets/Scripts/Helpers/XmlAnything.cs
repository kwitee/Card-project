using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CardProject.Helpers
{
    public sealed class XmlAnything<T> : IXmlSerializable
    {
        public XmlAnything() { }
        public XmlAnything(T t) { Value = t; }
        public T Value { get; set; }

        public void WriteXml(XmlWriter writer)
        {
            if (Value == null)
            {
                writer.WriteAttributeString("type", "null");
                return;
            }

            Type type = Value.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            writer.WriteAttributeString("type", type.AssemblyQualifiedName);
            serializer.Serialize(writer, Value);
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes)
                throw new FormatException("Expected a type attribute!");

            string typeText = reader.GetAttribute("type");
            reader.Read(); // consume the value

            if (typeText == "null")
                return; // leave T at default value

            var type = Type.GetType(typeText);

            if (type == null)
                throw new FormatException(string.Format("Type {0} is not recognized!", typeText));

            var serializer = new XmlSerializer(type);
            Value = (T)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public XmlSchema GetSchema() { return (null); }
    }
}