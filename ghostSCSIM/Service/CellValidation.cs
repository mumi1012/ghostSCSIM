using ghostSCSIM.DAO;
using ghostSCSIM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ghostSCSIM.service
{
    class CellValidation
    {
        private DaoHelper dao = new DaoHelper();

        /// <summary>
        /// Validiert die Teilenummer in der Bestelllistenzelle
        /// </summary>
        /// <returns></returns>
        public bool bestellungNummerIsValid(DataGridViewCell teilenummerCell)
        {
            int intValue;

            //Artikelnummer ist null oder ""
            if (string.IsNullOrEmpty(teilenummerCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Artikelnummer ist keine (positive) Ganzzahl
            else if (!int.TryParse(teilenummerCell.FormattedValue.ToString(), out intValue) || intValue < 0)
            {
                return false;
            }
            //Artikelnummer ist keine gültige Kaufteilnummer
            else
            {
                List<Teil> kaufteilliste = new List<Teil>(dao.getKaufteileStammdaten());
                //Liste aller K-Teilenummern
                List<int> kTeilenummern = new List<int>();

                foreach (Teil teil in kaufteilliste)
                {
                    kTeilenummern.Add(teil.getNummer());
                }

                if (!kTeilenummern.Contains(int.Parse(teilenummerCell.FormattedValue.ToString())))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Validiert die Mengenangaben in der Bestelllistenzelle
        /// </summary>
        /// <returns></returns>
        public bool bestellungMengeIsValid(DataGridViewCell mengeCell)
        {
            int intValue;

            //Menge ist null oder ""
            if (string.IsNullOrEmpty(mengeCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Menge ist keine (positive) Ganzzahl
            else if (!int.TryParse(mengeCell.FormattedValue.ToString(), out intValue) || intValue <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Validiert die Teilenummer der Direktverkauf DatagridviewCell
        /// </summary>
        /// <returns></returns>
        public bool direktverkaufNummerIsValid(DataGridViewCell teilenummerCell)
        {
            int intValue;

            //Artikelnummer ist null oder ""
            if (string.IsNullOrEmpty(teilenummerCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Artikelnummer ist keine (positive) Ganzzahl
            else if (!int.TryParse(teilenummerCell.FormattedValue.ToString(), out intValue) || intValue < 0)
            {
                return false;
            }
            //Artikelnummer ist keine gültige Kaufteilnummer
            else
            {
                List<Teil> teileListe = new List<Teil>(dao.getFertigerzeugnisseStammdaten());
                //Liste aller Teilenummern
                List<int> teilenummern = new List<int>();

                foreach (Teil teil in teileListe)
                {
                    teilenummern.Add(teil.getNummer());
                }

                if (!teilenummern.Contains(int.Parse(teilenummerCell.FormattedValue.ToString())))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Validiert die Menge der Direktverkauf DatagridviewCell
        /// </summary>
        /// <returns></returns>
        public bool direktverkaufMengeIsValid(DataGridViewCell mengeCell)
        {
            int intValue;

            //Menge: null oder ""
            if (string.IsNullOrEmpty(mengeCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Menge: Keine (positive) Ganzzahl
            else if (!int.TryParse(mengeCell.FormattedValue.ToString(), out intValue) || intValue < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validiert den Preis der Direktverkauf DatagridviewCell
        /// </summary>
        /// <returns></returns>
        public bool direktverkaufPreisIsValid(DataGridViewCell preisCell)
        {
            double doubleValue;

            //Preis ist null oder ""
            if (string.IsNullOrEmpty(preisCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Preis ist keine (positive) Dezimalzahl
            else if (!double.TryParse(preisCell.FormattedValue.ToString(), out doubleValue) || doubleValue < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validiert die Strafe der Direktverkauf DatagridviewCell
        /// </summary>
        /// <returns></returns>
        public bool direktverkaufStrafeIsValid(DataGridViewCell strafeCell)
        {
            double doubleValue;

            //Strafe ist null oder ""
            if (string.IsNullOrEmpty(strafeCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Strafe ist keine positive Dezimalzahl
            else if (!double.TryParse(strafeCell.FormattedValue.ToString(), out doubleValue) || doubleValue < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Validiert die Überstundenangabe im Kapazitätsplan
        /// </summary>
        /// <returns></returns>
        public bool kapaPlanUeberstundenIsValid(DataGridViewCell ueberstundenCell)
        {
            int intValue;

            //Überstunden sind null oder ""
            if (string.IsNullOrEmpty(ueberstundenCell.FormattedValue.ToString()))
            {
                return false;
            }
            //Überstunden sin nich in positiver Ganzzahl angegeben
            else if (!int.TryParse(ueberstundenCell.FormattedValue.ToString(), out intValue) || intValue < 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Validiert die Überstunden im Kapazitätsplan nach maximaler Überstundenanzahl
        /// </summary>
        /// <returns></returns>
        public bool kapaPlanUeberstundenMaxIsValid(DataGridViewCell ueberstundenCell)
        {
            int intValue;

            //Maximale Überstunden-Anzahl überschritten
            if (int.TryParse(ueberstundenCell.FormattedValue.ToString(), out intValue) && intValue > 240)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validiert die Überstundenangabe im Kapazitätsplan in Zusammenhang mit 3. Schicht
        /// </summary>
        /// <returns></returns>
        public bool kapaPlanUeberstundenSchichtIsValid(DataGridViewCell ueberstundenCell, bool dreiSchicht)
        {
            int intValue;

            //Überstunden trotz 3. Schicht angeordnet
            if (dreiSchicht && int.TryParse(ueberstundenCell.FormattedValue.ToString(), out intValue) && intValue > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Validiert die Teilenummern für Fertigungsaufträge
        /// </summary>
        /// <returns></returns>
        public bool fertigungNummerIsValid(DataGridViewCell nummerCell)
        {
            //Teilenummer ist null oder ""
            //Teilenummer ist keine (positive) Ganzzahl
            //Angegebene Teilenummer ist keine gültige Teilenummer
            return false;
        }

        /// <summary>
        /// Validiert die Menge für Fertigungsaufträge
        /// </summary>
        /// <returns></returns>
        public bool fertigungMengeIsValid(DataGridViewCell nummerCell)
        {
            //Menge ist null oder ""
            //Menge ist keine (positive) Ganzzahl
            // (Split-) Menge überschreitet verfügbares Material
            return false;
        }

    }
}
