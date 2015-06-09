using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ghostSCSIM.XML;

namespace ghostSCSIM.XML
{
    public class XmlIO
    {
        public XmlSerializer deserializer;
        public TextReader reader;
        public object xml;

        /// <summary>
        /// Achtung der Serializer kann keine Kommata "," im XML-Dokument lesen, -> Exception
        /// Workaround: Alle Kommata mit Punkt "." ersetzen
        /// </summary>
        /// <param name="filepath"></param>
        public XmlIO(String filepath)
        {
            deserializer = new XmlSerializer(typeof(DataContainer));
            reader = new StreamReader(filepath);
            xml = deserializer.Deserialize(reader);
            reader.Close();

        }

        //Standardkonstruktor
        public XmlIO(): base() {}
        
        //Methode um die Input XML zu erzeugen
        public void createInputXml(string filepath)
        {
            var pp_p1_p1_vw = 100;
            var pp_p2_p2_vw = 100;
            var pp_p3_p3_vw = 100;
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
            writer.WriteAttributeString("quantity", pp_p1_p1_vw.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "2");
            writer.WriteAttributeString("quantity", pp_p2_p2_vw.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("item");
            writer.WriteAttributeString("article", "3");
            writer.WriteAttributeString("quantity", pp_p3_p3_vw.ToString());
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
            /*
            foreach (Direktverkauf d in direktverkaeufe)
            {
                writer.WriteStartElement("order");
                writer.WriteAttributeString("article", d.getTeil());
                writer.WriteAttributeString("quantity", d.getMenge());
                writer.WriteAttributeString("price", d.getPreis());
                writer.WriteAttributeString("penalty", d.getStrafe());
                writer.WriteEndElement();
            }
            */
            writer.WriteEndElement();

            writer.WriteStartElement("orderlist");
            //TODO: Daten für Bestellungen
            /*
            foreach (Bestellung b in bestellungen)
            {
                writer.WriteStartElement("order");
                writer.WriteAttributeString("article", b.getTeil());
                writer.WriteAttributeString("quantity", b.getMenge());
                writer.WriteAttributeString("modus", b.getBestellTyp());
                writer.WriteEndElement();
            }
            */
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
