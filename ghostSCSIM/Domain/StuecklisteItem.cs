using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
   class StuecklisteItem

    {
       private Teil teil;
       /// <summary>
       /// Übergeordnetes Teil
       /// </summary>
       private Teil parent;
       private int anzahl;
       private int nettoBedarf;
              
       
       public StuecklisteItem(Teil teil, Teil parent, int anzahl)
       {
           this.teil = teil;
           this.anzahl = anzahl;
           this.parent = parent;
       }

       public StuecklisteItem(Teil teil, Teil parent, int anzahl, int nettoBedarf)
       {
           this.teil = teil;
           this.anzahl = anzahl;
           this.parent = parent;
           this.nettoBedarf = nettoBedarf;
          
       }

       public StuecklisteItem() { }

       public Teil getTeil()
       {
           return teil;
       }

       public void setTeil(Teil teil)
       {
           this.teil = teil;
       }

       public int getAnzahl()
       {
           return anzahl;
       }

       public void setAnzahl(int anzahl)
       {
           this.anzahl = anzahl;
       }

       public void setParent(Teil parent)
       {
           this.parent = parent;
       }

       public Teil getParent()
       {
           return parent;
       }

       public void setNettobedarf(int nettoBedarf)
       {
           this.nettoBedarf = nettoBedarf;
       }

       public int getNettobedarf()
       {
           return nettoBedarf;
       }

       

       public override string ToString()
       {
           return teil.ToString()
                        + ", Stuecklisten Menge= " + anzahl
                            + " ,Verwendet in TeileNummer: " + parent.getNummer().ToString();
       }
    }
}
