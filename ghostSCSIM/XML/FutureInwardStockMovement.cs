using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
   public class FutureInwardStockMovement
    {
       [XmlElement("order")]
       public List<Order> orders = new List<Order>();
    }

    public class Order {

        [XmlAttribute]
        public int orderperiod { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public int mode { get; set; }

        [XmlAttribute]
        public int article { get; set; }

        [XmlAttribute]
        public int amount { get; set; }
        


    }
}
