using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class Direktverkauf
    {

        private Teil teil;
        private int menge;
        private double preis;
        private double strafe;

        public Direktverkauf(Teil teil, int menge, double preis)
        {
            this.teil = teil;
            this.menge = menge;
            this.preis = preis;
            this.strafe = 0.0;
        }


        public Teil getTeil()
        {
            return teil;
        }

        public void setTeil(Teil teil)
        {
            this.teil = teil;
        }

        public int getMenge()
        {
            return menge;
        }

        public void setMenge(int menge)
        {
            this.menge = menge;
        }

        public double getPreis()
        {
            return preis;
        }

        public void setPreis(double preis)
        {
            this.preis = preis;
        }

        public double getStrafe()
        {
            return strafe;
        }

        public void setStrafe(double strafe)
        {
            this.strafe = strafe;
        }

    }
}