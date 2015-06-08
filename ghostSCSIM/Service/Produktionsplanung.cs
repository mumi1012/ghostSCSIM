using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.Domain;
using ghostSCSIM.DAO;
using ghostSCSIM.XML;

namespace ghostSCSIM.service
{
    class Produktionsplanung
    {

        private DataContainer dc = Start.xmlData;
        
        private List<TeilDisposition> ergebnisse = new List<TeilDisposition>();

        //TeileDispo Objekte erstellen
        public void generate()
        {
            DaoHelper daoHelper = new DaoHelper();
            List<Teil> teile_stammdaten = new List<Teil>();


            
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

                ergebnisse.Add(teil_dispo);
            }

        }
    }
}
