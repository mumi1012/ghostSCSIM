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

        private List<Bestellung> offeneBestellungen;

        private int aktuellePeriode = Start.xmlData.period;
        private int lagerzugang;

        private int bruttoBedarfPeriode0 = 0;  
        private int bruttoBedarfPeriode1 = 0;
        private int bruttoBedarfPeriode2 = 0;    
        private int bruttoBedarfPeriode3 = 0;

        //Bestaende fuer die einzelnen n+x Perioden
        private int[] bestandPeriode = new int[4];
        
        /// <summary>
        /// Flag zum identifizieren möglicher Knappheiten der Kaufteile während der aktuellen Produktionsperiode
        /// 0 = alles OK
        /// 1 = kritisch, durch Reihenfolgenplanung möglich
        /// 2 = Teile langen nicht, Sicherheitsbestände reduzieren
        /// </summary>
        private int criticalFlag = 0;

        private int lieferungAnTag;


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
               int bruttoBedarfPeriode4, List<StuecklisteItem> teileVerwendungsnachweis,
               List<Bestellung> offeneBestellungen)
        {
            this.teil = teil;
            this.stuecklisteItemP1 = stuecklisteItemP1;
            this.stuecklisteItemP2 = stuecklisteItemP2;
            this.stuecklisteItemP3 = stuecklisteItemP3;

            this.bruttoBedarfPeriode0 = bruttoBedarfPeriode1;
            this.bruttoBedarfPeriode1 = bruttoBedarfPeriode2;
            this.bruttoBedarfPeriode2 = bruttoBedarfPeriode3;
            this.bruttoBedarfPeriode3 = bruttoBedarfPeriode4;

            this.offeneBestellungen = offeneBestellungen;
            this.teileVerwendungsNachweis = teileVerwendungsnachweis;

            this.lieferDaten = lieferdaten;

            bestaendeBerechnen();
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


        /// <summary>
        /// Berechnet an welchem Tag das Material vermutlich ausgeht
        /// </summary>
        private void MaterialGehtAusBerechnen()
        {

            int differenzBetrag = 0;
            
            //Langen die Teile ohne Bestellung?
            if (lieferDaten.getLagerMenge() < bruttoBedarfPeriode0)
            {
               //Bestellungen prüfen
                differenzBetrag = lieferDaten.getLagerMenge() - bruttoBedarfPeriode0;
                    

                //Offene Bestellungen anschauen ob in dieser Periode noch Teile kommen
                if (offeneBestellungen != null)
                {
                    lieferungAnTag = 0;

                    foreach (Bestellung bestellung in offeneBestellungen)
                    {
                        if (differenzBetrag > 0)
                        {
                            criticalFlag = 0;
                            break;
                        }
                        //Prüfen ob Bestellung rechtzeitig ankommt
                        lieferungAnTag = berechneLieferungInTagen(bestellung);

                        //Bestellung kommt noch in dieser Periode an
                        //Lieferung an Tag 1 in der Periode, zum Bestand hinzufügen
                        if (lieferungAnTag == 1)
                        {
                            //Differenzbetrag wird durch die Bestellung aufgebraucht, Teile langen
                            if (differenzBetrag + bestellung.getMenge() > 0)
                            {
                                criticalFlag = 0;
                            }
                            else
                            {
                                differenzBetrag = differenzBetrag + bestellung.getMenge();
                            }
                        }
                        //Bestellung kommt Mitte der Periode an, criticalFlag = 1 setzen für Reihenfolgenplanung, wenn die Menge langt
                        if (lieferungAnTag <= 4 && lieferungAnTag > 1)
                        {
                            if (differenzBetrag + bestellung.getMenge() > 0)
                            {
                                criticalFlag = 1;
                            }
                            else
                            {
                                differenzBetrag = differenzBetrag + bestellung.getMenge();
                            }
                        }

                        if (lieferungAnTag >= 5)
                        {
                            criticalFlag = 2;
                        }


                    }
                    //Wenn nach Prüfung der Bestelluhngen immer noch ein Differenzbetrag übrig ist, Produktion nicht durchführbar
                    if (differenzBetrag < 0)
                    {
                        criticalFlag = 2;
                    }
                }
                //Keine Bestellungen, Teile langen nicht
                else
                {
                    criticalFlag = 2;
                }
                

            }
            
                       
                //Wann geht das Material aus?   
                
                //Zum besseren iterieren 
                int[] bruttoBedarfe = new int[3];
                bruttoBedarfe[0] = bruttoBedarfPeriode1;
                bruttoBedarfe[1] = bruttoBedarfPeriode2;
                bruttoBedarfe[2] = bruttoBedarfPeriode3;


                /**
                 * Der gesamtBedarf in der akutellen periode (i in der schleife. i = 0
                 * ist Periode 1)
                 */

                //Lagermenge anpassen
                if (differenzBetrag > 0)
                {
                    lagerzugang = differenzBetrag;
                                
                int gesamtBedarf = 0;
                for (int i = 0; i < 3; i++)
                {
                    gesamtBedarf += bruttoBedarfe[i];


                    if (gesamtBedarf >= lieferDaten.getLagerMenge()+lagerzugang)
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
                else { setMaterialGehtAusAmTag(1); }
        }

        public void bestaendeBerechnen()
        {
            int aktuellerLagerbestand = lieferDaten.getLagerMenge();
            
            int[] bruttoBedarfe = new int[4];
            bruttoBedarfe[0] = bruttoBedarfPeriode0;
            bruttoBedarfe[1] = bruttoBedarfPeriode1;
            bruttoBedarfe[2] = bruttoBedarfPeriode2;
            bruttoBedarfe[3] = bruttoBedarfPeriode3;


            //Bestand n+1
            //Zunächst prüfen ob der Lagerbestand für die Produktion ausreichend ist
            if ((aktuellerLagerbestand - bruttoBedarfPeriode0) > 0)
            {
                criticalFlag = 0;
            }
           
            bestandPeriode[0] = aktuellerLagerbestand + berechneLagerZugang(aktuellePeriode) - bruttoBedarfPeriode0;

            //Wenn der Bestand in der Folgeperiode > 0 ist, Anzeige als Lagerzugang
            if (berechneLagerZugang(aktuellePeriode) > 0)
            {
                int menge = bestandPeriode[0] - aktuellerLagerbestand;
                if (menge > 0)
                {
                    lagerzugang = menge;
                }
                else
                {
                    lagerzugang = 0;
                }
                    
               
            }

            //Bestand n+2 - n+4
            for (int i = 1; i < 4; ++i)
            {
                bestandPeriode[i] = bestandPeriode[i - 1] + berechneLagerZugang(aktuellePeriode + i) - bruttoBedarfe[i];
            }
            //Prüfen in welcher Periode der Bestand ausgeht
            for (int i = bestandPeriode.Count() - 1 ; i >= 0; --i)
            {
                if (bestandPeriode[i] <= 0)
                {
                    setMaterialGehtAusInPeriode(aktuellePeriode + i);
                }
            }
            //Material geht in der ersten Periode schon aus
            if (materialGehtAusInPeriode == aktuellePeriode)
            {
                criticalFlag = 2;
            }


        }
        //Funktion um den aktuellen Lagerzugang zu berechnen
        private int berechneLagerZugang(int periode)
        {
            int menge = 0;
            if (offeneBestellungen != null)
            {
                foreach (Bestellung bestellung in offeneBestellungen)
                {
                    //Laut Handbuch immer +2 Tage, damit Teil in Produktion verfügbar ist
                    double verfuegbarInProduktion = 0.4;

                    //Eilbestellung
                    if (bestellung.getBestellTyp().Equals(Bestelltyp.N))
                    {
                        verfuegbarInProduktion += bestellung.getPeriode() + lieferDaten.getWiederbeschaffungszeitPeriode() + lieferDaten.getAbweichungPeriode() * Start.lieferRisiko / 100;

                    }
                    else if (bestellung.getBestellTyp().Equals(Bestelltyp.F))
                    {
                        verfuegbarInProduktion += bestellung.getPeriode() + lieferDaten.getWiederbeschaffungszeitPeriode() / 2;
                    }
                    //Prüfen ob Teil direkt zu Beginn der Periode verfügbar ist = Tag 1 (0.0), direkt zur Menge hinzurechnen
                    if (verfuegbarInProduktion == periode)
                    {
                        if (periode == aktuellePeriode)
                        {
                            criticalFlag = 0;
                        }
                        menge += bestellung.getMenge();
                    }
                    //Teil kommt in der Periode zwischen Tag 2 = 0.2 und Tag 4 = 0.8
                    else if (verfuegbarInProduktion > periode && verfuegbarInProduktion <= (periode + 0.9))
                    {
                        menge += bestellung.getMenge();
                        //Wenn die Periode = aktuelle Planperiode, dann criticalFlag = 1 setzen
                        if (periode == aktuellePeriode)
                        {
                            //Hier nochmal prüfen ob mit den Lagerzugang der Bedarf gedeckt ist
                            int fehlMenge = lieferDaten.getLagerMenge() - bruttoBedarfPeriode0;
                            if(fehlMenge < 0) 
                            {
                                //Bestellmenge ausreichend
                                if (bestellung.getMenge() >= fehlMenge)
                                {
                                    criticalFlag = 1;
                                }
                            }
                           

                                                            
                        }
                       
                    }
                                        
                }
                
            }
            return menge;
        }

        
        private int berechneLieferungInTagen(Bestellung bestellung)
        {
            double lieferRisiko = Start.lieferRisiko;
            int orderPeriod = bestellung.getPeriode();
            
            int beschaffungsZeitInTagen = 0;

            //Lieferdaten zu dem Teil aus der Bestellung

            //Eilbestellung
            if(bestellung.getBestellTyp().Equals(Bestelltyp.F)) {
                double beschaffungsZeitInTagenAsDec = Convert.ToDouble(lieferDaten.getWiederbeschaffungszeitTage()) / 2;
                beschaffungsZeitInTagen = Convert.ToInt32(Math.Ceiling(beschaffungsZeitInTagenAsDec));
            }
            //Normalbestellung
            else {
                //Ohne Abweichung - Risikoneutral
                if(lieferRisiko == 0) {
                    beschaffungsZeitInTagen = lieferDaten.getWiederbeschaffungszeitTage();
                }
                //Mit maximaler positiver Abweichung - Risikoavers
                if(lieferRisiko == 1) {
                    beschaffungsZeitInTagen = lieferDaten.getWiederbeschaffungszeitTage() + lieferDaten.getAbweichungTage();
                }
                
                //Mit maximal negativer Abweichung - Riskikoaffing
                if (lieferRisiko == -1) {
                    beschaffungsZeitInTagen = lieferDaten.getWiederbeschaffungszeitTage() - lieferDaten.getAbweichungTage();
                }
            }
            return beschaffungsZeitInTagen;
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

        public List<Bestellung> getOffeneBestellungen()
        {
            return this.offeneBestellungen;
        }

        
        public TeilLieferdaten getLieferDaten()
        {
            return lieferDaten;
        }

        public int getBestandInPeriode(int index)
        {
            if (index < 0 || index > 3)
            {
                throw new IndexOutOfRangeException("Index out of Range");
            }

            return bestandPeriode[index];
        }
        public int[] getBestaende()
        {
            return bestandPeriode;
        }

        public void setLieferdaten(TeilLieferdaten lieferdaten)
        {
            this.lieferDaten = lieferdaten;
        }

        public int getBruttoBedarfPeriode1()
        {
            return bruttoBedarfPeriode0;
        }

        public void setBruttoBedarfPeriode1(int bruttoBedarfPeriode1)
        {
            this.bruttoBedarfPeriode0 = bruttoBedarfPeriode1;
        }

        public int getBruttoBedarfPeriode2()
        {
            return bruttoBedarfPeriode1;
        }

        public void setBruttoBedarfPeriode2(int bruttoBedarfPeriode2)
        {
            this.bruttoBedarfPeriode1 = bruttoBedarfPeriode2;
        }

        public int getBruttoBedarfPeriode3()
        {
            return bruttoBedarfPeriode2;
        }

        public void setBruttoBedarfPeriode3(int bruttoBedarfPeriode3)
        {
            this.bruttoBedarfPeriode2 = bruttoBedarfPeriode3;
        }

        public int getBruttoBedarfPeriode4()
        {
            return bruttoBedarfPeriode3;
        }

        public void setBruttoBedarfPeriode4(int bruttoBedarfPeriode4)
        {
            this.bruttoBedarfPeriode3 = bruttoBedarfPeriode4;
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

        public int getLagerzugang()
        {
            return lagerzugang;
        }
        public void setLagerzugang(int zugang)
        {
            this.lagerzugang = zugang;
        }

        public int getCriticalFlag()
        {
            return criticalFlag;
        }

        public void setCriticalFlag(int flag) 
        {
            criticalFlag = flag;
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
