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
            foreach (WlWorkplace wp in workplaces)
            {
                if (wp.waitinglist != null)
                {
                    foreach (Waitinglist wl in wp.waitinglist)
                        if (wl.item.Equals(item))
                        {
                            returnValue = wl.amount;
                        }
                }

            }
            return returnValue;
        }

        public int getWarteschlangeMengeByKHDItem(int item)
        {
            int returnValue = 0;
            foreach (WlWorkplace wp in workplaces)
            {
                if (wp.waitinglist != null)
                {
                    foreach (Waitinglist wl in wp.waitinglist)
                        if (wl.item.Equals(item))
                        {
                            returnValue = wl.amount/3;
                        }
                }

            }
            return returnValue;
        }

        public List<Waitinglist> getListWarteschlange()
        {
            List<Waitinglist> listWarteschlange = new List<Waitinglist>();
            foreach(WlWorkplace wp in workplaces)
            {
                if (wp.waitinglist != null)
                {
                    foreach(Waitinglist wl in wp.waitinglist)
                    {
                        listWarteschlange.Add(wl);
                    }
                }
            }
            return listWarteschlange;
        }


        public List<Waitinglist> getWaitinglistByArbeitsplatzId(int arbeitsplatzId)
        {

            foreach (WlWorkplace wp in workplaces)
            {
                if (wp.id == arbeitsplatzId)
                {
                    return wp.waitinglist;
                }
            }
            return new List<Waitinglist>();
        }
    }

    public class WlWorkplace
    {

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public int timeneed { get; set; }

        [XmlElement("waitinglist")]
        public List<Waitinglist> waitinglist { get; set; }
    }

    public class Waitinglist
    {

        [XmlAttribute("period")]
        public int period { get; set; }

        [XmlAttribute]
        public int order { get; set; }

        [XmlAttribute]
        public int firstbatch { get; set; }

        [XmlAttribute]
        public int lastbatch { get; set; }

        [XmlAttribute]
        public int item { get; set; }

        [XmlAttribute]
        public int amount { get; set; }

        [XmlAttribute]
        public int timeneed { get; set; }
    }
}