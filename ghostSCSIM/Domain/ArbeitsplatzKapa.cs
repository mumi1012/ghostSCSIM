using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class ArbeitsplatzKapa
    {

        private int arbeitsplatz;
        private int teilfk;
        private int ruestzeit;
        private int fertigungszeit;

        public ArbeitsplatzKapa(int arbeitsplatz, int teilfk, int ruestzeit, int fertigungszeit)
        {
            this.arbeitsplatz = arbeitsplatz;
            this.teilfk = teilfk;
            this.ruestzeit = ruestzeit;
            this.fertigungszeit = fertigungszeit;
        }

        public int getArbeitsplatz()
        {
            return arbeitsplatz;
        }

        public void setArbeitsplatz(int arbeitsplatz)
        {
            this.arbeitsplatz = arbeitsplatz;
        }

        public int getTeilfk()
        {
            return teilfk;
        }

        public void setTeilfk(int teilfk)
        {
            this.teilfk = teilfk;
        }

        public int getRuestzeit()
        {
            return ruestzeit;
        }

        public void setRuestzeit(int ruestzeit)
        {
            this.ruestzeit = ruestzeit;
        }

        public int getFertigungszeit()
        {
            return fertigungszeit;
        }

        public void setFertigungszeit(int fertigungszeit)
        {
            this.fertigungszeit = fertigungszeit;
        }

    }
}
