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
        public List<Teil> getTeilStammdaten() {

            DatenbankDataSet datenbankDataSet = new DatenbankDataSet();
            DatenbankDataSetTableAdapters.TeilTableAdapter teilTableAdapter = new DatenbankDataSetTableAdapters.TeilTableAdapter();

            DataTable resultTable = teilTableAdapter.GetData();
            List<Teil> teileListe = new List<Teil>();
             
            foreach (DataRow dr in resultTable.Rows)
            {
                
                Teil einTeil = new Teil((int)dr["ID"], (string)dr["Bezeichnung"],(Verwendung)Enum.Parse(typeof(Verwendung),dr["Verwendung"].ToString(),true),(string)dr["Buchstabe"], 5.0);
                teileListe.Add(einTeil);
            }

            return teileListe;

          
            
           
         

            

            
        }

       


     }
}
