using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ghostSCSIM.XML;

namespace ghostSCSIM
{
    /**
     * 
     * Abbildung der XML-Struktur aus den Ergebnisdateien
     * 
     * */
    [XmlRoot("results")]
    public sealed class DataContainer
    {
        private static readonly DataContainer _instance = new DataContainer();

        static DataContainer() { }


        public static DataContainer Instance {
            get {
                return _instance;
            }
        }
        [XmlAttribute("game")]
        public int game { get; set; }

        [XmlAttribute("group")]
        public int group { get; set; }

        [XmlAttribute("period")]
        public int period { get; set; }

        [XmlElement("warehousestock")]
        public WarehouseStock warehouseStock { get; set; }

        [XmlElement("inwardstockmovement")]
        public InwardStockMovement inwardStockMovement { get; set; }

        [XmlElement("futureinwardstockmovement")]
        public  FutureInwardStockMovement futureInwardStockMovement { get; set; }

        [XmlElement("idletimecosts")]
        public IdleTimeCosts idleTimeCosts { get; set; }

        [XmlElement("waitinglistworkstations")]
        public WaitingListWorkstations waitingListWorkstations { get; set; }

        [XmlElement("ordersinwork")]
        public OrdersInWork ordersInWork { get; set; }

        //Boolean Flag
        private bool xmlImported;

        public void setXmlImported(bool flag)
        {
            this.xmlImported = flag;
        }

        public bool getXmlImported()
        {
            return xmlImported;
        }
        private DataContainer()
        {
            this.xmlImported = false;
        }

    }
}
