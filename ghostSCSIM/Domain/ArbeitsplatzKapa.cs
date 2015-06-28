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
        private int ueberstunden;
        private int rueckstaendekapa;
        private int rueckstaendez;
        private int gesamt;
        private bool schicht2;
        private bool schicht3;


        public ArbeitsplatzKapa(int arbeitsplatz, int teilfk, int ruestzeit, int fertigungszeit, int rueckstaendekapa, int rueckstaendez, int gesamt, int ueberstunden, bool schicht2, bool schicht3)
        {
            this.arbeitsplatz = arbeitsplatz;
            this.teilfk = teilfk;
            this.ruestzeit = ruestzeit;
            this.fertigungszeit = fertigungszeit;
            this.rueckstaendekapa = rueckstaendekapa;
            this.rueckstaendez = rueckstaendez;
            this.gesamt = gesamt;
            this.ueberstunden = ueberstunden;
            this.schicht2 = schicht2;
            this.schicht3 = schicht3;
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

        public int getRueckstaendekapa()
        {
            return rueckstaendekapa;
        }

        public void setRueckstaendekapa(int rueckstaendekapa)
        {
            this.rueckstaendekapa = rueckstaendekapa;
        }

        public int getRueckstaendez()
        {
            return rueckstaendez;
        }

        public void setRueckstaendez(int rueckstaendez)
        {
            this.rueckstaendez = rueckstaendez;
        }

        public int getGesamt()
        {
            return gesamt;
        }

        public void setGesamt(int gesamt)
        {
            this.gesamt = gesamt;
        }

        public int getUeberstunden()
        {
            return ueberstunden;
        }

        public void setUeberstunden(int ueberstunden)
        {
            this.ueberstunden = ueberstunden;
        }

        public bool getSchicht2()
        {
            return schicht2;
        }

        public void setSchicht2(bool schicht2)
        {
            this.schicht2 = schicht2;
        }

        public bool getSchicht3()
        {
            return schicht3;
        }

        public void setSchicht3(bool schicht3)
        {
            this.schicht3 = schicht3;
        }

    }
}
