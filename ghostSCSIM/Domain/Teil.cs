using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    public class Teil
    {

        private int nummer;
        private String bezeichnung;
        private Verwendung verwendung;
        private String buchstabe;
        private double wert;
        /// <summary>
        /// Position in der Stückliste, Hilfsvariable für die Sortierung der Teile in der Stückliste
        /// </summary>
        private int stuecklistenPos;

        public Teil()
        {

        }

        public Teil(int nummer, String bezeichnung, Verwendung verwendung,
                String buchstabe, double wert)
        {
            this.nummer = nummer;
            this.bezeichnung = bezeichnung;
            this.buchstabe = buchstabe;
            this.wert = wert;
            this.verwendung = verwendung;
        }
        public Teil(int nummer, String bezeichnung, Verwendung verwendung,
             String buchstabe, double wert, int stuecklistenPos)
        {
            this.nummer = nummer;
            this.bezeichnung = bezeichnung;
            this.buchstabe = buchstabe;
            this.wert = wert;
            this.verwendung = verwendung;
            this.stuecklistenPos = stuecklistenPos;
        }

        public Teil(int nummer)
        {
            this.nummer = nummer;
        }

        public Teil(int nummer, String bezeichnung, String buchstabe, double wert)
        {
            this.nummer = nummer;
            this.bezeichnung = bezeichnung;
            this.buchstabe = buchstabe;
            this.wert = wert;
        }

        public int getNummer()
        {
            return nummer;
        }

        public void setNummer(int nummer)
        {
            this.nummer = nummer;
        }

        public String getBezeichnung()
        {
            return bezeichnung;
        }

        public void setBezeichnung(String bezeichnung)
        {
            this.bezeichnung = bezeichnung;
        }

        public Verwendung getVerwendung()
        {
            return verwendung;
        }

        public void setVerwendung(Verwendung verwendung)
        {
            this.verwendung = verwendung;
        }

        public String getBuchstabe()
        {
            return buchstabe;
        }

        public void setBuchstabe(String buchstabe)
        {
            if (buchstabe.Count() > 1)
                throw new Exception(
                        "Die Laenge der Variable Buchstabe ist groesser als 1.");
            this.buchstabe = buchstabe;
        }

        public double getWert()
        {
            return wert;
        }

        public void setWert(double wert)
        {
            this.wert = wert;
        }

        public int getStuecklistenPos()
        {
            return stuecklistenPos;
        }

        public override string ToString()
        {
            return "Teil: [" + buchstabe + nummer + "]"
                + ", Bezeichnung= " + bezeichnung
                        + ", Verwendung= " + verwendung
                            + ", Wert= " + wert;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Teil t = (Teil)obj;
            return this.getNummer() == t.getNummer();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

         
    }
}
