using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    [Obsolete("Nicht mehr benutzen!", true)]
    class MengenStuecklisteItem
    {

        private Teil ETeil;
        //verwendetes Teil = Kaufteil 
        private Teil verwendetesTeil;
        private int menge;

        public MengenStuecklisteItem()
        {
        }

        public MengenStuecklisteItem(Teil eTeil, Teil verwendetesTeil, int menge)
        {
            this.menge = menge;
            this.verwendetesTeil = verwendetesTeil;
            this.ETeil = eTeil;
        }

        public Teil getETeil()
        {
            return ETeil;
        }

        public void setETeil(Teil eTeil)
        {
            this.ETeil = eTeil;
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

      
        public override string ToString()
        {
            return "MengenstueckListe Item: [ETeil= " + ETeil.getNummer()
            + ", Verwendetes Teil Nr= " + verwendetesTeil.getNummer()
                    + ", Menge= " + menge + "]";
        }

    }
}
