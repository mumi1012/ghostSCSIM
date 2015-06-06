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
       
       
       public StuecklisteItem(Teil teil, Teil parent, int anzahl)
       {
           this.teil = teil;
           this.anzahl = anzahl;
           this.parent = parent;
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

       public override string ToString()
       {
           return teil.ToString()
                        + ", Stuecklisten Menge= " + anzahl
                            + " ,Verwendet in TeileNummer: " + parent.getNummer().ToString();
       }
    }
}
