using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
    
    public class WarehouseStock
    {
        [XmlElement("article")]
        public List<Article> article = new List<Article>();
    }

    public class Article
    {
        [XmlAttribute]
        public int id { get ; set; }

        [XmlAttribute]
        public int amount { get; set; }

        [XmlAttribute]
        public int startamount { get; set; }

        [XmlAttribute]
        public double pct { get; set; }

        [XmlAttribute]
        public double price { get; set; }

        [XmlAttribute]
        public double stockvalue { get; set; }

        
    }
}
