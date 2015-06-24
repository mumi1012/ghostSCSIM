using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class Bestellposition
    {

        private Teil teil;
        private int menge;
        private bool eilbestellung;

        public Bestellposition(Teil teil, int menge, bool eilbestellung)
        {
            this.teil = teil;
            this.menge = menge;
            this.eilbestellung = eilbestellung;
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

        public bool isEilbestellung()
        {
            return eilbestellung;
        }

        public void setEilbestellung(bool eilbestellung)
        {
            this.eilbestellung = eilbestellung;
        }

        public int getBestelltyp()
        {
            if (eilbestellung)
            {
                return 4;
            }
            else
            {
                return 5;
            }

        }
    }
}