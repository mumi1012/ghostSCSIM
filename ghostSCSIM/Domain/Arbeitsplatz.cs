using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class Arbeitsplatz
    {
        private int apNummer;
        private int ueberstunden;
        private int schichten;

        public Arbeitsplatz(int arbeitsplatznummer, int ueberstunden, int schichten)
        {
            this.apNummer = arbeitsplatznummer;
            this.ueberstunden = ueberstunden;
            this.schichten = schichten;
        }

        public int getApNummer()
        {
            return apNummer;
        }

        public void setApNummer(int arbeitsplatznummer)
        {
            this.apNummer = arbeitsplatznummer;
        }

        public int getUeberstunden()
        {
            return ueberstunden;
        }

        public void setUeberstunden(int ueberstunden)
        {
            this.ueberstunden = ueberstunden;
        }

        public int getSchichten()
        {
            return schichten;
        }

        public void setSchichten(int schichten)
        {
            this.schichten = schichten;
        }
    }
}
