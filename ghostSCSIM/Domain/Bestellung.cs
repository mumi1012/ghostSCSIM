using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class Bestellung
    {

        private int periode;
        private Bestelltyp bestellTyp;
        private int menge;
        private Teil teil;
        private bool eingetroffen;

        public Bestellung()
        {

        }

        public Bestellung(int periode, Bestelltyp bestellTyp, int menge,
                Teil teil, bool eingetroffen)
        {
            this.periode = periode;
            this.bestellTyp = bestellTyp;
            this.menge = menge;
            this.teil = teil;
            this.eingetroffen = eingetroffen;
        }

        public bool isEingetroffen()
        {
            return eingetroffen;
        }

        public void setEingetroffen(bool eingetroffen)
        {
            this.eingetroffen = eingetroffen;
        }

        public int getPeriode()
        {
            return periode;
        }

        public void setPeriode(int periode)
        {
            this.periode = periode;
        }

        public Bestelltyp getBestellTyp()
        {
            return bestellTyp;
        }

        public void setBestellTyp(Bestelltyp bestellTyp)
        {
            this.bestellTyp = bestellTyp;
        }

        public int getMenge()
        {
            return menge;
        }

        public void setMenge(int menge)
        {
            this.menge = menge;
        }

        public Teil getTeil()
        {
            return teil;
        }

        public void setTeil(Teil teil)
        {
            this.teil = teil;
        }

    }
}
