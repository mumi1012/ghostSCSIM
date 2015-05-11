using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class MengenStuecklisteItem
    {

        private Teil fahrrad;
        private Teil verwendetesTeil;
        private int menge;

        public MengenStuecklisteItem()
        {
        }

        public MengenStuecklisteItem(Teil fahrrad, Teil verwendetesTeil, int menge)
        {
            this.menge = menge;
            this.verwendetesTeil = verwendetesTeil;
            this.fahrrad = fahrrad;
        }

        public Teil getFahrrad()
        {
            return fahrrad;
        }

        public void setFahrrad(Teil fahrrad)
        {
            this.fahrrad = fahrrad;
        }

        public Teil getVerwendetesTeil()
        {
            return verwendetesTeil;
        }

        public void setVerwendetesTeil(Teil verwendetesTeil)
        {
            this.verwendetesTeil = verwendetesTeil;
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
