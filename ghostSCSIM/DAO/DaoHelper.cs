using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using ghostSCSIM.DatenbankDataSetTableAdapters;
using ghostSCSIM.Domain;
using System.Reflection;

namespace ghostSCSIM.DAO

{
    class DaoHelper
    {

        /// <summary>
        /// Methode um die Stammdaten der Teile auszulesen und als Objekt Teil zu speichern
        /// </summary>
        /// <returns></returns>
        public List<Teil> getTeilStammdaten()
        {

            DatenbankDataSet datenbankDataSet = new DatenbankDataSet();
            DatenbankDataSetTableAdapters.TeilTableAdapter teilTableAdapter = new DatenbankDataSetTableAdapters.TeilTableAdapter();

            DataTable resultTable = teilTableAdapter.GetData();
            List<Teil> teileListe = new List<Teil>();

            foreach (DataRow dr in resultTable.Rows)
            {

                Teil einTeil = new Teil((int)dr["ID"], (string)dr["Bezeichnung"], (Verwendung)Enum.Parse(typeof(Verwendung), dr["Verwendung"].ToString(), true), (string)dr["Buchstabe"], Convert.ToDouble(dr["Wert"]));
                teileListe.Add(einTeil);
            }

            return teileListe;

        }
        /// <summary>
        /// Die Stammdaten zu den Halb- und FE auslesen
        /// </summary>
        /// <returns></returns>
        public List<Teil> getErzeugnisseStammdaten()
        {
            DatenbankDataSetTableAdapters.TeilTableAdapter teilTableAdapter = new DatenbankDataSetTableAdapters.TeilTableAdapter();
            DataTable resultTable = teilTableAdapter.getErzeugnisseStammdaten();
            List<Teil> teileListe = new List<Teil>();

            List<TeileHelper> teileHelperListe = new List<TeileHelper>();
            teileHelperListe = (from teil in resultTable.AsEnumerable()
                                select new TeileHelper
                                {
                                    partId = teil.Field<int>("ID"),
                                    bezeichnung = teil.Field<string>("Bezeichnung"),
                                    verwendung = teil.Field<string>("Verwendung"),
                                    buchstabe = teil.Field<string>("Buchstabe"),
                                    wert = teil.Field<double>("Wert")
                                    
 

                                }).ToList();

            foreach (TeileHelper teileHelper in teileHelperListe)
            {
                Teil einzelTeil = new Teil(teileHelper.partId, teileHelper.bezeichnung, (Verwendung)Enum.Parse(typeof(Verwendung), teileHelper.verwendung), teileHelper.buchstabe, teileHelper.wert);
                teileListe.Add(einzelTeil);
            }
            return teileListe;
        }

        public TeilLieferdaten getTeilLieferdatenByTeilenummer(int teileNummer)
        {
            //TODO: eventuell eine Abfrage einbauen, dass nur sinnvolle Teilenummern abgefragt werden können
            DatenbankDataSet datenbankDataSet = new DatenbankDataSet();
            DatenbankDataSetTableAdapters.BestelldatenTableAdapter bestellDatenAdapter = new DatenbankDataSetTableAdapters.BestelldatenTableAdapter();

            DataTable resultTable = bestellDatenAdapter.GetDataByTeilenummer(teileNummer);
            TeilLieferdaten lieferDaten = null;
            if (!(resultTable.Rows.Count == 0))
            {
                foreach (DataRow dr in resultTable.Rows)
                    {
                        lieferDaten = new TeilLieferdaten();
                        //TODO: Konstruktor bauen für Teillieferdaten
                        lieferDaten.setTeileNummer(Convert.ToInt32(dr["Teil_FK"]));
                        lieferDaten.setWiederbeschaffungszeitPeriode(Convert.ToDouble(dr["Lieferfrist"]));
                        lieferDaten.setDiskontMenge(Convert.ToInt32(dr["Diskontmenge"]));
                        lieferDaten.setAbweichungPeriode(Convert.ToDouble(dr["Abweichung"]));
                        lieferDaten.setBestellkosten(Convert.ToDouble(dr["Bestellkosten"]));
                        lieferDaten.convertPeriodeZuTage(lieferDaten.getWiederbeschaffungszeitPeriode(), lieferDaten.getAbweichungPeriode());
                    }
               
            }

            return lieferDaten;


        }
        public Dictionary<int, TeilLieferdaten> getTeilLieferdaten()
        {
            DatenbankDataSetTableAdapters.BestelldatenTableAdapter bestellDatenTableAdapter = new BestelldatenTableAdapter();
            DataTable resultTable = bestellDatenTableAdapter.getTeilLieferdaten();

            Dictionary<int, TeilLieferdaten> lieferDatenMap = new Dictionary<int, TeilLieferdaten>();
            TeilLieferdaten lieferDaten = null;

            foreach (DataRow dr in resultTable.Rows)
            {
                lieferDaten = new TeilLieferdaten();
                int teileNummer = Convert.ToInt32(dr["Teil_FK"]);
                lieferDaten.setTeileNummer(teileNummer);
                lieferDaten.setWiederbeschaffungszeitPeriode(Convert.ToDouble(dr["Lieferfrist"]));
                lieferDaten.setDiskontMenge(Convert.ToInt32(dr["Diskontmenge"]));
                lieferDaten.setAbweichungPeriode(Convert.ToDouble(dr["Abweichung"]));
                lieferDaten.setBestellkosten(Convert.ToDouble(dr["Bestellkosten"]));
                lieferDaten.convertPeriodeZuTage(lieferDaten.getWiederbeschaffungszeitPeriode(), lieferDaten.getAbweichungPeriode());

                lieferDatenMap.Add(teileNummer, lieferDaten);
            }
            return lieferDatenMap;
        }

       public Stueckliste getStueckListeByFahrradnummer(int fahrradnummer)
       {
            DatenbankDataSetTableAdapters.PartsListPosTableAdapter partsListPostTbAdapter = new PartsListPosTableAdapter();

            DataTable resultTable = partsListPostTbAdapter.GetStuecklisteByFahrradnummer(fahrradnummer);
           
            Stueckliste stueckListe = new Stueckliste();
            StuecklisteItem item = new StuecklisteItem();
            LinkedList<StuecklisteItem> verwendeteTeile = null;
            
            Teil parent = null;

            //IDs der Teile sammeln, die selbst ein FE sind, also E-Teile
            List<int> halbFeIdListe = new List<int>();
            halbFeIdListe = getFETableIds(resultTable);

            foreach (int posId in halbFeIdListe)
            {
                TeileHelper parentItem = getParentItem(resultTable, posId);
                parent = new Teil(parentItem.partId, parentItem.bezeichnung, (Verwendung)Enum.Parse(typeof(Verwendung), parentItem.verwendung), parentItem.buchstabe, parentItem.wert, parentItem.posId);

                List<TeileHelper> unterStueckliste = getUnterStueckliste(resultTable, posId);

                verwendeteTeile = new LinkedList<StuecklisteItem>();
                //Über teilStueckListe iterieren und Teile zur Stueckliste hinzufügen
                foreach (TeileHelper einzelTeil in unterStueckliste)
                {
                  
                    Teil teil = new Teil(einzelTeil.partId, einzelTeil.bezeichnung, (Verwendung)Enum.Parse(typeof(Verwendung), einzelTeil.verwendung), einzelTeil.buchstabe, einzelTeil.wert);
                    //Verwendete Teile Liste aufbauen
                    verwendeteTeile.AddLast(new StuecklisteItem(teil, parent, einzelTeil.anzahl));
                }
                //Verwendete Teile der Stückliste zuweisen, Key = aktuelles Teil, bzw. parent
                stueckListe.addItemToStueckliste(parent, verwendeteTeile);
    

            }
          


            return stueckListe;
        }
        /// <summary>
        /// Hilfsmethode um die Unterstückliste zurückzugeben
        /// </summary>
        /// <param name="resultTable"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<TeileHelper> getUnterStueckliste(DataTable resultTable, int id)
        {

            List<TeileHelper> unterStueckliste =  (from items in resultTable.AsEnumerable()
                                    where items.Field<int>("OTPosId") == id
                                    select new TeileHelper
                                    {
                                        posId = items.Field<int>("ID"),
                                        partId = items.Field<int>("PartId"),
                                        otPosId = items.Field<int>("OTPosId"),
                                        anzahl = items.Field<int>("Anzahl"),
                                        bezeichnung = items.Field<string>("Bezeichnung"),
                                        verwendung = items.Field<string>("Verwendung"),
                                        wert = items.Field<double>("Wert"),
                                        buchstabe = items.Field<string>("Buchstabe")
                                                                               
                                    }).ToList();
            
            return unterStueckliste;

            
        }
        /// <summary>
        /// Table IDs zu allen FE Teilen aus der Tabelle ermitteln
        /// </summary>
        /// <param name="resultTable"></param>
        /// <returns></returns>
        private List<int> getFETableIds(DataTable resultTable)
        {
            List<int> resultList = (from items in resultTable.AsEnumerable()
                                    where items.Field<int>("Flag").Equals(1)
                                    select items.Field<int>("ID")).ToList();


            return resultList;
        }

        private TeileHelper getParentItem(DataTable resultTable, int posId)
        {
            TeileHelper result = (from items in resultTable.AsEnumerable()
                                  where items.Field<int>("ID") == posId
                                  select new TeileHelper
                                  {
                                      posId = items.Field<int>("ID"),
                                      partId = items.Field<int>("PartId"),
                                      otPosId = items.Field<int>("OTPosId"),
                                      bezeichnung = items.Field<string>("Bezeichnung"),
                                      verwendung = items.Field<string>("Verwendung"),
                                      wert = items.Field<double>("Wert"),
                                      buchstabe = items.Field<string>("Buchstabe")
                                  }).Single();

            return result;
        }

        public List<ArbeitsplatzKapa> getArbeitsplaetzeKapa()
        {
            DatenbankDataSetTableAdapters.Arbeitsplatz_TeilTableAdapter arbeitsplatzTeil = new Arbeitsplatz_TeilTableAdapter();
            DataTable result = arbeitsplatzTeil.GetData();
            List<ArbeitsplatzKapa> listeArbeitsplatzKapa = new List<ArbeitsplatzKapa>();

            foreach (DataRow dr in result.Rows)
            {
                ArbeitsplatzKapa eiDing = new ArbeitsplatzKapa((int)dr["Arbeitsplatz"], (int)dr["Teil_FK"], (int)dr["Rüstzeit"], (int)dr["Fertigungszeit"]);
                listeArbeitsplatzKapa.Add(eiDing);
            }

            return listeArbeitsplatzKapa;

        }
           
    }
}
