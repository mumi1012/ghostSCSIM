using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ghostSCSIM.Domain
{
    class Stueckliste
    {

        private SortedList<Teil, LinkedList<StuecklisteItem>> stueckliste;


        public Stueckliste()
        {

            this.stueckliste = new SortedList<Teil, LinkedList<StuecklisteItem>>(new TeilComparer());
        }

        public SortedList<Teil, LinkedList<StuecklisteItem>> getStueckliste()
        {
            return stueckliste;
        }

        public void addItemToStueckliste(Teil teil, LinkedList<StuecklisteItem> verwendeteTeile)
        {
            stueckliste.Add(teil, verwendeteTeile);
        }
        
        /// <summary>
        /// Verwendete Teile anhand einer Teilenummer auslesen
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public LinkedList<StuecklisteItem> getVerwendeteTeileByPartNumber(int number)
        {
            List<Teil> keys = getKeys();
            if (keys == null) { return null; }
            
            Teil teil = keys.First<Teil>(Teil => Teil.getNummer().Equals(number));
            if (teil == null) { return null; }

            return getVerwendeteTeileListe(teil);
        }
        /// <summary>
        /// Hilfsmethode um die verwendeten Teile auszulesen
        /// </summary>
        /// <param name="teil"></param>
        /// <returns></returns>
        private LinkedList<StuecklisteItem> getVerwendeteTeileListe(Teil teil)
        {

            return stueckliste[teil];
        }

        /// <summary>
        /// Die Teil-Stueckliste anhand der gesuchten Teilenummer auslesen = ETeile Nummber
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Stueckliste</returns>
        public Stueckliste getTeilStuecklisteByPartNumber(int number)
        {
            List<Teil> keys = getKeys();
            if (keys == null)
            {
                return null;
            }
            Teil teil = keys.First<Teil>(Teil => Teil.getNummer() == number);
            return getTeilStueckliste(teil);
          
        }
        //Hilfsmethode um die Teilstückliste aufzubauen
        private Stueckliste getTeilStueckliste(Teil parent)
        {
         
            Stueckliste teilStueckListe = new Stueckliste();
            LinkedList<StuecklisteItem> verwendeteTeile = getVerwendeteTeileListe(parent);
            teilStueckListe.addItemToStueckliste(parent, verwendeteTeile);

            return teilStueckListe;
        }

        //Hilfsmethode um die Keys aus der Strukturstückliste auszulesen
        private List<Teil> getKeys()
        {
            IList<Teil> keys = stueckliste.Keys;
            
            return keys.ToList();

        }

        public StuecklisteItem getMengenBedarfKaufteilByTeilenummer(int teilenummer, Stueckliste stueckListe)
        {
            StuecklisteItem stueckListeItem = null;
            int anzahl = 0;
            foreach (Teil teil in stueckListe.getKeys())
            {
                LinkedList<StuecklisteItem> verwendeteTeile =  stueckListe.getVerwendeteTeileListe(teil);

                foreach (StuecklisteItem item in verwendeteTeile.Where(item => item.getTeil().getNummer().Equals(teilenummer)))
                {
                    stueckListeItem = new StuecklisteItem();
                    stueckListeItem.setTeil(item.getTeil());
                    anzahl += item.getAnzahl();
                }
            }
            if (stueckListeItem != null)
            {
                stueckListeItem.setAnzahl(anzahl);
            }
            
            return stueckListeItem;
        }

        /// <summary>
        /// Nettbedarf an Kaufteilen für das aktuelle Produktionsprogramm ermitteln, inklusive Sicherheitsbständen
        /// </summary>
        /// <param name="teilenummer"></param>
        /// <returns></returns>
        public int getNettoBedarfToFertigteilByTeilenummer(int teilenummer, Dictionary<int,int> produktionsProgramm)
        {

           //Keys der Stückliste ermitteln
            IList<Teil> keys = this.getKeys();
              
            //Über Keys iterieren und nach Teilenummer suchen
            int nettoBedarf = 0;

            foreach (Teil teil in keys)
            {
                //Bruttobedarf zu dem Teil aus dem Produktionsprogramm
                int bruttoBedarf = produktionsProgramm[teil.getNummer()];
                
                //Bei Teilenummer E26, E17 oder E16 werden die summierten Bedarfe verwendet, daher durch drei teilen
                if (teil.getNummer().Equals(16) || teil.getNummer().Equals(17) || teil.getNummer().Equals(27)) {
                    bruttoBedarf = bruttoBedarf / 3;
                }
                  
                LinkedList<StuecklisteItem> verwendeteTeile = this.getVerwendeteTeileListe(teil);
                foreach (StuecklisteItem item in verwendeteTeile.Where(item => item.getTeil().getNummer().Equals(teilenummer)))
                {
                    nettoBedarf += item.getAnzahl() * bruttoBedarf;
                }
            }
            return nettoBedarf;
        }
    }
}
