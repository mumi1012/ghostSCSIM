using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
    public class OrdersInWork
    {
        [XmlElement]
        public List<Workplace> workplace = new List<Workplace>();

        public int getInBearbeitungMengeByItem(int item)
        {
            int returnValue = 0;
            if (workplace != null) { 
                foreach (Workplace wp in workplace)
                {
                    if (wp.item.Equals(item)) {
                        returnValue = wp.amount;
                    }
                }

            }
            
            return returnValue;
        }

        public int getInBearbeitungMengeByKHDItem(int item)
        {
            int returnValue = 0;
            if (workplace != null)
            {
                foreach (Workplace wp in workplace)
                {
                    if (wp.item.Equals(item))
                    {
                        returnValue = wp.amount/3;
                    }
                }

            }

            return returnValue;
        }

        public int getInBearbeitungMengeByArbeitsplatz(int arbeitsplatzId)
        {
            int returnValue = 0;
            if (workplace != null)
            {
                foreach (Workplace wp in workplace)
                {
                    if (wp.id.Equals(arbeitsplatzId))
                    {
                        returnValue = wp.timeneed;
                    }
                }

            }

            return returnValue;
        }
    }
    
}
