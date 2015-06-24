using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.Domain;

namespace ghostSCSIM.Service.Disposition
{
    class DispositionErgebnis
    {
        private Teil teil { get; set; }
        private TeilLieferdaten lieferDaten { get; set; }

        private StuecklisteItem stuecklisteItemP1 { get; set; }
        private StuecklisteItem stuecklisteItemP2 { get; set; }
        private StuecklisteItem stuecklisteItemP3 { get; set; }

        private List<StuecklisteItem> teileVerwendungsNachweis;

        private int bruttoBedarfPeriode1 = 0;  
        private int bruttoBedarfPeriode2 = 0;
        private int bruttoBedarfPeriode3 = 0;    
        private int bruttoBedarfPeriode4 = 0;

        //ToDO restliche Felder setzen
        private double maxLieferdauerPeriode = -1;
        private int maxLieferdauerTage = -1;

        private Bestelltyp vorgeschlagenerBestellTyp;
        private double materialGehtAusAmTag = -999.0;
        private int materialGehtAusInPeriode = -999;
        private int bestellPeriode;

       

        public DispositionErgebnis(Teil teil, StuecklisteItem stuecklisteItemP1,
               StuecklisteItem stuecklisteItemP2,
               StuecklisteItem stuecklisteItemP3,
               TeilLieferdaten lieferdaten, int bruttoBedarfPeriode1,
               int bruttoBedarfPeriode2, int bruttoBedarfPeriode3,
               int bruttoBedarfPeriode4, List<StuecklisteItem> teileVerwendungsnachweis)
        {
            this.teil = teil;
            this.stuecklisteItemP1 = stuecklisteItemP1;
            this.stuecklisteItemP2 = stuecklisteItemP2;
            this.stuecklisteItemP3 = stuecklisteItemP3;

            this.bruttoBedarfPeriode1 = bruttoBedarfPeriode1;
            this.bruttoBedarfPeriode2 = bruttoBedarfPeriode2;
            this.bruttoBedarfPeriode3 = bruttoBedarfPeriode3;
            this.bruttoBedarfPeriode4 = bruttoBedarfPeriode4;

            this.teileVerwendungsNachweis = teileVerwendungsnachweis;

            this.lieferDaten = lieferdaten;

            MaterialGehtAusBerechnen();
            bestellPeriodeBerechnen();
            
        }

        private void addTeileVerwendungsnachweis(List<StuecklisteItem> verwendeteTeil)
        {
            this.teileVerwendungsNachweis = verwendeteTeil;
        }

        private void bestellPeriodeBerechnen()
        {
            int diskontMenge = lieferDaten.getDiskontMenge();
            double gesamtWiederbeschaffungszeit = lieferDaten
                                      .getWiederbeschaffungszeitPeriode()
                                      + lieferDaten.getAbweichungPeriode();

            //Ermitteln wie lange die Reichweite ist = materialGehtAusAmTag
            
        }

        private void MaterialGehtAusBerechnen()
        {
            //Zum besseren iterieren 
            int[] bruttoBedarfe = new int[4];
            bruttoBedarfe[0] = bruttoBedarfPeriode1;
            bruttoBedarfe[1] = bruttoBedarfPeriode2;
            bruttoBedarfe[2] = bruttoBedarfPeriode3;
            bruttoBedarfe[3] = bruttoBedarfPeriode4;

            /**
             * Der gesamtBedarf in der akutellen periode (i in der schleife. i = 0
             * ist Periode 1)
             */
            int gesamtBedarf = 0;
            for (int i = 0; i < 4; i++)
            {
                gesamtBedarf += bruttoBedarfe[i];

                if (gesamtBedarf >= lieferDaten.getLagerMenge())
                {
                    int differenzGesamtBedarf__LagerMenge = gesamtBedarf
                            - lieferDaten.getLagerMenge();

                    double prozent = 1
                            - Convert.ToDouble(differenzGesamtBedarf__LagerMenge / bruttoBedarfe[i]);

                    double materialGehtAusAmTag = prozent * 5;

                    if (i > 0)
                        materialGehtAusAmTag += i * 5;
                    setMaterialGehtAusAmTag(materialGehtAusAmTag);
                    break;
                }

            }
        }

        //private List<Bestellung> erstelleBestellListe()
        //{

        //}

        //Getter & Setter
        public Teil getTeil()
        {
            return teil;
        }

        public void setTeil(Teil teil)
        {
            this.teil = teil;
        }

        public List<StuecklisteItem> getTeileVerwendungsnachweis()
        {
            return this.teileVerwendungsNachweis;
        }

        public TeilLieferdaten getLieferDaten()
        {
            return lieferDaten;
        }

        public void setLieferdaten(TeilLieferdaten lieferdaten)
        {
            this.lieferDaten = lieferdaten;
        }

        public int getBruttoBedarfPeriode1()
        {
            return bruttoBedarfPeriode1;
        }

        public void setBruttoBedarfPeriode1(int bruttoBedarfPeriode1)
        {
            this.bruttoBedarfPeriode1 = bruttoBedarfPeriode1;
        }

        public int getBruttoBedarfPeriode2()
        {
            return bruttoBedarfPeriode2;
        }

        public void setBruttoBedarfPeriode2(int bruttoBedarfPeriode2)
        {
            this.bruttoBedarfPeriode2 = bruttoBedarfPeriode2;
        }

        public int getBruttoBedarfPeriode3()
        {
            return bruttoBedarfPeriode3;
        }

        public void setBruttoBedarfPeriode3(int bruttoBedarfPeriode3)
        {
            this.bruttoBedarfPeriode3 = bruttoBedarfPeriode3;
        }

        public int getBruttoBedarfPeriode4()
        {
            return bruttoBedarfPeriode4;
        }

        public void setBruttoBedarfPeriode4(int bruttoBedarfPeriode4)
        {
            this.bruttoBedarfPeriode4 = bruttoBedarfPeriode4;
        }

        public StuecklisteItem getStuecklisteItemP1()
        {
            return stuecklisteItemP1;
        }

        public void setStuecklisteItemP1(StuecklisteItem stuecklisteItemP1)
        {
            this.stuecklisteItemP1 = stuecklisteItemP1;
        }

        public StuecklisteItem getStuecklisteItemP2()
        {
            return stuecklisteItemP1;
        }

        public void setStuecklisteItemP2(StuecklisteItem stuecklisteItemP2)
        {
            this.stuecklisteItemP2 = stuecklisteItemP2;
        }

        public StuecklisteItem getStuecklisteItemP3()
        {
            return stuecklisteItemP3;
        }

        public void setStuecklisteItemP3(StuecklisteItem stuecklisteItemP3)
        {
            this.stuecklisteItemP3 = stuecklisteItemP3;
        }

        public double getMaxLieferdauerPeriode()
        {
            if (maxLieferdauerPeriode < 0 && lieferDaten == null)
            {
                throw new Exception(
                    "Die Lieferdauer darf nicht null sein!");
            }
            if (maxLieferdauerPeriode < 0)
            {
                maxLieferdauerPeriode = lieferDaten
                                        .getWiederbeschaffungszeitPeriode()
                                        + lieferDaten.getAbweichungPeriode();
                maxLieferdauerPeriode = DispositionErgebnis.round(maxLieferdauerPeriode, 1);
            }
            return maxLieferdauerPeriode;
        }

        public int getMaxLieferdauerTage()
        {
            if (maxLieferdauerTage < 0 && lieferDaten == null)
            {
                throw new Exception(
                        "<<<<<<<<<<<<<< Setz die Lieferdauer. Die darf nicht null sein!>>>>>>>>>>>>>>>>>>>");
            }
            if (maxLieferdauerTage < 0)
            {
                maxLieferdauerTage = lieferDaten.getWiederbeschaffungszeitTage()
                        + lieferDaten.getAbweichungTage();
            }
            return maxLieferdauerTage;
        }

        public void setMaxLieferdauerTage(int tage)
        {
            maxLieferdauerTage = tage;
        }

        public void setMaxLieferdauerPeriode(int maxLieferdauerPeriode)
        {
            this.maxLieferdauerPeriode = maxLieferdauerPeriode;
        }

        public Bestelltyp getVorgeschlagenerBestellTye()
        {
            return vorgeschlagenerBestellTyp;
        }

        public void setVorgeschlagenerBestellTye(
                Bestelltyp vorgeschlagenerBestellTye)
        {
            this.vorgeschlagenerBestellTyp = vorgeschlagenerBestellTye;
        }

        public double getMaterialGehtAusAmTag()
        {
            return materialGehtAusAmTag;
        }

        /**
         * Setzt implizit materialGetAusInPeriode!!!!!!!!!!!
         * 
         * @param materialGehtAus
         */
        public void setMaterialGehtAusAmTag(double materialGehtAus)
        {
            this.materialGehtAusAmTag = materialGehtAus;
            setMaterialGehtAusInPeriode((int)Math.Ceiling(materialGehtAusAmTag / 5));
           
        }

        /**
         * Die Periode in der Bestellt werden soll
         * 
         * @return
         */
        public int getBestellPeriode()
        {
            return bestellPeriode;
        }

        /**
         * Die periode in der bestellt werden soll
         * 
         * @param bestellPeriode
         */
        public void setBestellPeriode(int bestellPeriode)
        {
            this.bestellPeriode = bestellPeriode;
        }

        public Bestelltyp getVorgeschlagenerBestellTyp()
        {
            return vorgeschlagenerBestellTyp;
        }

        public void setVorgeschlagenerBestellTyp(
                Bestelltyp vorgeschlagenerBestellTyp)
        {
            this.vorgeschlagenerBestellTyp = vorgeschlagenerBestellTyp;
        }

        public int getMaterialGehtAusInPeriode()
        {
            return materialGehtAusInPeriode;
        }

        public void setMaterialGehtAusInPeriode(int materialGehtAusInPeriode)
        {
            this.materialGehtAusInPeriode = materialGehtAusInPeriode;
        }


        public static double round(double d, int decimalPlace)
        {
            double result = Math.Round(d, decimalPlace);
            return result;

        }
    }
}
