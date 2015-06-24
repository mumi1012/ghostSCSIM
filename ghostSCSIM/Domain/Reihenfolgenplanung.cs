using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class Reihenfolgenplanung
    {
        private string teil;
        private int menge;
        private int splittmenge;

        public Reihenfolgenplanung(string teil, int menge, int splittmenge)
        {
            this.teil = teil;
            this.menge = menge;
            this.splittmenge = splittmenge;
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
