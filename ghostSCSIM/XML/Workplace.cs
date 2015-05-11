using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
    
    public class Workplace
    {
        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public int period { get; set; }

        [XmlAttribute]
        public int order { get; set; }

        [XmlAttribute]
        public int batch { get; set; }

        [XmlAttribute]
        public int item { get; set; }

        [XmlAttribute]
        public int amount { get ; set; }

        [XmlAttribute]
        public int timeneed { get; set; }

        
      
    }
}
