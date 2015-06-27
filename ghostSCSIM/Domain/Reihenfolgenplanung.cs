using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ghostSCSIM.Service.Disposition;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class Reihenfolgenplanung
    {
        private int index;
        private string teil;
        private int menge;
        private int splittmenge;

        public Reihenfolgenplanung(int index, string teil, int menge, int splittmenge)
        {
            this.index = index;
            this.teil = teil;
            this.menge = menge;
            this.splittmenge = splittmenge;
        }

        public int getIndex()
        {
            return index;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public string getTeil()
        {
            return teil;
        }

        public void setTeil(string teil)
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

        public int getSplittmenge()
        {
            return splittmenge;
        }

        public void setSplittmenge(int splittmenge)
        {
            this.splittmenge = splittmenge;
        }

        

    }
}
