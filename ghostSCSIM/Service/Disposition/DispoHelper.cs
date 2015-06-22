using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.Domain;
using ghostSCSIM.DAO;
using ghostSCSIM.XML;

namespace ghostSCSIM.Service.Disposition
{
    /// <summary>
    /// DispoHelper Klasse für die Anzeige der Kaufteile
    /// </summary>
    class DispoHelper
    {

        private DataContainer dc = Start.xmlData;

        private List<TeilDisposition> ergebnisse = new List<TeilDisposition>();

        internal List<TeilDisposition> Ergebnisse
        {
            get { return ergebnisse; }
            set { ergebnisse = value; }
        }

        

       /// <summary>
       /// TeilDisposition-Objekte bauen anhand Programmplan und Ergebnissen der Vorperiode
       /// </summary>
        public List<TeilDisposition> generate()
        {
            DaoHelper daoHelper = new DaoHelper();
            List<Teil> teile_stammdaten = new List<Teil>();
            Dictionary<int, int> produktionsMengen = Start.teile_Produktion;
            teile_stammdaten = daoHelper.getErzeugnisseStammdaten();
            
            //TeilDispoHelper Objekte bauen
            foreach (Teil teil in teile_stammdaten)
            {

                int teile_nummer = teil.getNummer();
                TeilDisposition teil_dispo = new TeilDisposition(teil);

                //Aktuelle Lagermenge auslesen aus WarehouseStock
                var teil_lagerbestand = dc.warehouseStock.article.SingleOrDefault(article => article.id.Equals(teile_nummer));
                if(teil_lagerbestand != null)
                    teil_dispo.setLagerbestand_ende_vorperiode(teil_lagerbestand.amount);
             
                //Aufträge in Bearbeitung
                int auftrag_in_bearbeitung = dc.ordersInWork.getInBearbeitungMengeByItem(teile_nummer);
                teil_dispo.setAuftrage_bearbeitung(auftrag_in_bearbeitung);
                               
                //Aufträge in Warteschlange
                int auftraege_warteschlange = dc.waitingListWorkstations.getWarteschlangeMengeByItem(teile_nummer);
                teil_dispo.setAuftraege_warteschlange(auftraege_warteschlange);

                //Produktionsmengen aus dem Programmplan hinzufügen
                int produktionsMenge = produktionsMengen[teil.getNummer()];
                //Wenn Produktionsmenge aus dem Programmplan negativ ist, auf null setzen
                if (produktionsMenge < 0) { produktionsMenge = 0; }
                teil_dispo.setProduktionsauftrag_naechste_periode(produktionsMenge);


                ergebnisse.Add(teil_dispo);
            }

            return ergebnisse;
           
            

        }
    }
}
