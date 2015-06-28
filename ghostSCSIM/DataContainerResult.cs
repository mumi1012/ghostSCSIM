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
        private List<Bestellposition> bestellung;
        private List<Direktverkauf> direktverkauf;

        private DataContainerResult()
        {
            this.vertriebswunschP1 = 0;
            this.vertriebswunschP2 = 0;
            this.vertriebswunschP3 = 0;
            this.bestellung = new List<Bestellposition>();
            this.direktverkauf = new List<Direktverkauf>();
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


        public List<Bestellposition> getBestellung()
        {
            return bestellung;
        }

        public void setBestellung(List<Bestellposition> bestellung)
        {
            this.bestellung.Clear();
            foreach (Bestellposition bp in bestellung)
            {
                this.bestellung.Add(new Bestellposition(bp.getTeil(), bp.getMenge(), bp.isEilbestellung()));
            }
        }

        public void addBestellposition(Teil teil, int menge, bool eilbestellung)
        {
            this.bestellung.Add(new Bestellposition(teil, menge, eilbestellung));
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

        public void addDirektverakuf(Direktverkauf dv)
        {
            this.direktverkauf.Add(dv);
        }

    }
}
