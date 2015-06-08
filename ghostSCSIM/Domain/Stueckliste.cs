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
    }
}
