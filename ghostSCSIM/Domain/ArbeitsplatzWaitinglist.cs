using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class ArbeitsplatzWaitinglist
    {

        private int arbeitsplatz;
        private int teil;
        private int menge;

        public ArbeitsplatzWaitinglist(int arbeitsplatz, int teil, int menge)
        {
            this.arbeitsplatz = arbeitsplatz;
            this.teil = teil;
            this.menge = menge;
        }

        public int getArbeitsplatz()
        {
            return arbeitsplatz;
        }

        public void setArbeitsplatz(int arbeitsplatz)
        {
            this.arbeitsplatz = arbeitsplatz;
        }

        public int getTeil()
        {
            return teil;
        }

        public void setTeil(int teil)
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


    }
}