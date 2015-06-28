using ghostSCSIM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM
{
    class DataContainerResult
    {

        private static DataContainerResult instance = new DataContainerResult();

        //Dictionary für Fertigungsaufträge (Teilenummer und zu produzierende Menge)?
        //List<Arbeitsplatz> für die Kapa-Planung (Arbeitsplatz, Anzahl Schichten, Überstunden)?
        private int vertriebswunschP1;
        private int vertriebswunschP2;
        private int vertriebswunschP3;
        private List<Bestellung> bestellung;
        private List<Direktverkauf> direktverkauf;
        private List<Arbeitsplatz> arbeitsplaetze;
        private LinkedList<Reihenfolgenplanung> fertigung;

        private DataContainerResult()
        {
            this.vertriebswunschP1 = 0;
            this.vertriebswunschP2 = 0;
            this.vertriebswunschP3 = 0;
            this.bestellung = new List<Bestellung>();
            this.direktverkauf = new List<Direktverkauf>();
            this.arbeitsplaetze = new List<Arbeitsplatz>();
            this.fertigung = new LinkedList<Reihenfolgenplanung>();
        }


        public static DataContainerResult Instance
        {
            get
            {
                return instance;
            }
        }


        public int getVertriebswunschP1()
        {
            return vertriebswunschP1;
        }

        public int getVertriebswunschP2()
        {
            return vertriebswunschP2;
        }

        public int getVertriebswunschP3()
        {
            return vertriebswunschP3;
        }

        public void setVertriebswuensche(int vertriebswunschP1, int vertriebswunschP2, int vertriebswunschP3)
        {
            this.vertriebswunschP1 = vertriebswunschP1;
            this.vertriebswunschP2 = vertriebswunschP2;
            this.vertriebswunschP3 = vertriebswunschP3;
        }


        public List<Bestellung> getBestellung()
        {
            return bestellung;
        }

        public void setBestellung(List<Bestellung> bestellung)
        {
            this.bestellung.Clear();
            foreach (Bestellung bp in bestellung)
            {
                int bestelltyp = (bp.getBestellTyp() == Bestelltyp.F) ? 4 : 5;
                this.bestellung.Add(new Bestellung(bp.getTeil(), bp.getMenge(), bestelltyp));
            }
        }

        public void addBestellung(Teil teil, int menge, int bestelltyp)
        {
            this.bestellung.Add(new Bestellung(teil, menge, bestelltyp));
        }

        public void clearBestellung()
        {
            this.bestellung.Clear();
        }


        public List<Direktverkauf> getDirektverkauf()
        {
            return direktverkauf;
        }

        public void setDirektverkauf(List<Direktverkauf> direktverkauf)
        {
            this.direktverkauf.Clear();
            foreach (Direktverkauf dv in direktverkauf)
            {
                this.direktverkauf.Add(new Direktverkauf(dv.getTeil(), dv.getMenge(), dv.getPreis(), dv.getStrafe()));
            }
        }

        public void addDirektverkauf(Teil teil, int menge, double preis, double strafe)
        {
            this.direktverkauf.Add(new Direktverkauf(teil, menge, preis, strafe));
        }
        
        public void removeDirektverkaufAtIndex(int index)
        {
            this.direktverkauf.RemoveAt(index);
        }

        public void clearDirektverkauf()
        {
            this.direktverkauf.Clear();
        }


        public List<Arbeitsplatz> getArbeitsplaetze()
        {
            return arbeitsplaetze;
        }

        public void setArbeitsplaetze(List<Arbeitsplatz> arbeitsplaetze)
        {
            this.arbeitsplaetze.Clear();
            foreach (Arbeitsplatz ap in arbeitsplaetze)
            {
                this.arbeitsplaetze.Add(new Arbeitsplatz(ap.getApNummer(), ap.getUeberstunden(), ap.getSchichten()));
            }
        }

        public void addArbeitsplatz(int arbeitsplatznummer, int ueberstunden, int schichten)
        {
            this.arbeitsplaetze.Add(new Arbeitsplatz(arbeitsplatznummer, ueberstunden, schichten));
        }

        public void clearArbeitsplaetze()
        {
            this.arbeitsplaetze.Clear();
        }


        public LinkedList<Reihenfolgenplanung> getFertigung()
        {
            return fertigung;
        }

        public void setFertigung(LinkedList<Reihenfolgenplanung> fertigung)
        {
            this.fertigung.Clear();
            this.fertigung = new LinkedList<Reihenfolgenplanung>(fertigung);
        }

        public void clearFertigung()
        {
            this.fertigung.Clear();
        }

    }
}
