using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Xml;

using System.Xml.Serialization;

using System.IO;

namespace UBLGenratorMy
{
    public class SerializeDeserialize<T>

    {

        StringBuilder sbData;

        StringWriter swWriter;

        XmlDocument xDoc;

        XmlNodeReader xNodeReader;

        XmlSerializer xmlSerializer;

        public SerializeDeserialize()

        {

            sbData = new StringBuilder();

        }

        public string SerializeData(T data)
        {

            XmlSerializer employeeSerializer = new XmlSerializer(typeof(T));

            StringWriterUtf8 swWriter = new StringWriterUtf8(sbData);

            //  swWriter = new StringWriter(sbData);

            employeeSerializer.Serialize(swWriter, data);

            return sbData.ToString();

        }

        public class StringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }

            public StringWriterUtf8(StringBuilder sbData): base(sbData)
            {
                
            }
        }

        public T DeserializeData(string dataXML)

        {

            xDoc = new XmlDocument();

            xDoc.LoadXml(dataXML);

            xNodeReader = new XmlNodeReader(xDoc.DocumentElement);

            xmlSerializer = new XmlSerializer(typeof(T));

            var employeeData = xmlSerializer.Deserialize(xNodeReader);

            T deserializedEmployee = (T)employeeData;

            return deserializedEmployee;

        }

    }

}

