using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
[Obsolete("Nicht mehr benutzen!", true)]
    class MengenStueckliste
    {

        private int fahrrad_fk;
        private Dictionary<Int32, MengenStuecklisteItem> teileP1 = new Dictionary<Int32, MengenStuecklisteItem>();
        private Dictionary<Int32, MengenStuecklisteItem> teileP2 = new Dictionary<Int32, MengenStuecklisteItem>();
        private Dictionary<Int32, MengenStuecklisteItem> teileP3 = new Dictionary<Int32, MengenStuecklisteItem>();

        public int getFahrrad_fk()
        {
            return fahrrad_fk;
        }

        public void setFahrrad_fk(int fahrrad_fk)
        {
            this.fahrrad_fk = fahrrad_fk;
        }

        public Dictionary<Int32, MengenStuecklisteItem> getTeileP1()
        {
            return teileP1;
        }

        public void setTeileP1(Dictionary<Int32, MengenStuecklisteItem> teile)
        {
            this.teileP1 = teile;
        }

        public Dictionary<Int32, MengenStuecklisteItem> getTeileP2()
        {
            return teileP2;
        }

        public void setTeileP2(Dictionary<Int32, MengenStuecklisteItem> teileP2)
        {
            this.teileP2 = teileP2;
        }

        public Dictionary<Int32, MengenStuecklisteItem> getTeileP3()
        {
            return teileP3;
        }

        public void setTeileP3(Dictionary<Int32, MengenStuecklisteItem> teileP3)
        {
            this.teileP3 = teileP3;
        }

    }
}
