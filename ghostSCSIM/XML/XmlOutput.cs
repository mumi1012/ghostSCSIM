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

            //Nur falls kein P1 zum Direktverkauf ausgewählt wurde
            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "1");
            writer.WriteAttributeString("quantity", "0");
            writer.WriteAttributeString("price", "0.0");
            writer.WriteAttributeString("penalty", "0.0");
            writer.WriteEndElement();

            //Nur falls kein P2 zum Direktverkauf ausgewählt wurde
            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "2");
            writer.WriteAttributeString("quantity", "0");
            writer.WriteAttributeString("price", "0.0");
            writer.WriteAttributeString("penalty", "0.0");
            writer.WriteEndElement();

            //Nur falls kein P3 zum Direktverkauf ausgewählt wurde
            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "3");
            writer.WriteAttributeString("quantity", "0");
            writer.WriteAttributeString("price", "0.0");
            writer.WriteAttributeString("penalty", "0.0");
            writer.WriteEndElement();

            //TODO: Daten für Direktverkauf
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

            //TODO: Daten für Bestellungen
            foreach (Bestellposition bp in outputData.getBestellung())
            {
                writer.WriteStartElement("order");
                writer.WriteAttributeString("article", bp.getTeil().getNummer().ToString());
                writer.WriteAttributeString("quantity", bp.getMenge().ToString());
                writer.WriteAttributeString("modus", bp.getBestelltyp().ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("productionlist");
            //TODO: Daten für Produktionsplan
            /*
            foreach (Teil t in produktionsplan)
            {
                writer.WriteStartElement("production");
                writer.WriteAttributeString("article", t.getTeilenummer());
                writer.WriteAttributeString("quantity", t.getMenge());
                writer.WriteEndElement();
            }
            */
            writer.WriteEndElement();

            writer.WriteStartElement("workingtimelist");
            //TODO: Daten für Schichten/Überstunden
            /*
            foreach (Arbeitsplatz ap in arbeitsplaetze)
            {
                writer.WriteStartElement("workingtime");
                writer.WriteAttributeString("station", ap.getArbeitsplatznummer());
                writer.WriteAttributeString("shift", ap.getSchicht());
                writer.WriteAttributeString("overtime", ap.getUeberstunden());
                writer.WriteEndElement();
            }
            */
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

    }
}
