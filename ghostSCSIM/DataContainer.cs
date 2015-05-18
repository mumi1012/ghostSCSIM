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
    public class DataContainer
    {
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

        public DataContainer()
        {
            xmlImported = true;
        }
    }
}
