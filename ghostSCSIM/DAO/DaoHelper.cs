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

                Teil einTeil = new Teil((int)dr["ID"], (string)dr["Bezeichnung"], (Verwendung)Enum.Parse(typeof(Verwendung), dr["Verwendung"].ToString(), true), (string)dr["Buchstabe"], 5.0);
                teileListe.Add(einTeil);
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

    }
}
