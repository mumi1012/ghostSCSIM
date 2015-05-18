using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
    [XmlRoot("waitinglistworkstations")]
    public class WaitingListWorkstations
    {
        [XmlElement("workplace")]
        public List<WlWorkplace> workplaces = new List<WlWorkplace>();

        public int getWarteschlangeMengeByItem(int item)
        {
            int returnValue = 0;
            foreach (WlWorkplace wp in workplaces) {
                if (wp.waitinglist != null)
                {
                    if (wp.waitinglist.item.Equals(item))
                    {
                        returnValue = wp.waitinglist.amount;
                    }
                }
                
            }
            return returnValue;
        }
    }
   
    public class WlWorkplace
    {
        
        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public int timeneed { get; set; }
        
        [XmlElement]
        public Waitinglist waitinglist { get ; set; }
    }
    [XmlRoot("waitinglist")]
    public class Waitinglist {

        [XmlAttribute("period")]
        public int period {get;set;}

        [XmlAttribute]
        public int order {get;set;}

        [XmlAttribute]
        public int firstbatch {get;set;}

        [XmlAttribute]
        public int lastbatch {get;set;}

        [XmlAttribute]
        public int item {get;set;}

        [XmlAttribute]
        public int amount {get;set;}

        [XmlAttribute]
        public int timeneed {get;set;}
    }
}
