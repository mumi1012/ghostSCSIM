using ghostSCSIM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
    class XmlOutput
    {
        private static DataContainerResult outputData = DataContainerResult.Instance;

        //Methode um die Input XML zu erzeugen
        public void createXml(string filepath)
        {
            //TODO: Vertriebswünsche, Produktionsprogramm, Bestellungen, ... als liste !!!
            XmlWriter writer = XmlWriter.Create(filepath);
            writer.WriteStartDocument();
            writer.WriteStartElement("input");
            writer.WriteStartElement("qualitycontrol");
            writer.WriteAttributeString("type", "no");
            writer.WriteAttributeString("losequantity", "0");
            writer.WriteAttributeString("delay", "0");
            writer.WriteEndElement();

            writer.WriteStartElement("sellwish");

            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "1");
            writer.WriteAttributeString("quantity", outputData.getVertriebswunschP1().ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "2");
            writer.WriteAttributeString("quantity", outputData.getVertriebswunschP2().ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "3");
            writer.WriteAttributeString("quantity", outputData.getVertriebswunschP3().ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.WriteStartElement("selldirect");

            foreach (Direktverkauf dv in outputData.getDirektverkauf())
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("article", dv.getTeil().getNummer().ToString());
                writer.WriteAttributeString("quantity", dv.getMenge().ToString());
                writer.WriteAttributeString("price", dv.getPreis().ToString());
                writer.WriteAttributeString("penalty", dv.getStrafe().ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement("orderlist");

            foreach (Bestellung best in outputData.getBestellung())
            {
                int bestelltyp = (best.getBestellTyp() == Bestelltyp.F) ? 4 : 5;

                writer.WriteStartElement("order");
                writer.WriteAttributeString("article", best.getTeil().getNummer().ToString());
                writer.WriteAttributeString("quantity", best.getMenge().ToString());
                writer.WriteAttributeString("modus", bestelltyp.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("productionlist");

            foreach (Reihenfolgenplanung rfp in outputData.getFertigung())
            {
                writer.WriteStartElement("production");
                writer.WriteAttributeString("article", rfp.getTeil());
                writer.WriteAttributeString("quantity", rfp.getMenge().ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement("workingtimelist");

            foreach (Arbeitsplatz ap in outputData.getArbeitsplaetze())
            {
                writer.WriteStartElement("workingtime");
                writer.WriteAttributeString("station", ap.getApNummer().ToString());
                writer.WriteAttributeString("shift", ap.getSchichten().ToString());
                writer.WriteAttributeString("overtime", ap.getUeberstunden().ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

    }
}
