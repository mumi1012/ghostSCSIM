using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ghostSCSIM.XML;

namespace ghostSCSIM.XML
{
    public class XmlIO
    {
        public XmlSerializer deserializer;
        public TextReader reader;
        public object xml;

        /// <summary>
        /// Achtung der Serializer kann keine Kommata "," im XML-Dokument lesen, -> Exception
        /// Workaround: Alle Kommata mit Punkt "." ersetzen
        /// </summary>
        /// <param name="filepath"></param>
        public XmlIO(String filepath)
        {
            deserializer = new XmlSerializer(typeof(DataContainer));
            reader = new StreamReader(filepath);
            xml = deserializer.Deserialize(reader);
            reader.Close();

        }
       
        
        
    }
}
