using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.DAO;
using ghostSCSIM.Domain;
using ghostSCSIM.Service;

namespace ghostSCSIM.Service.Disposition
{
    class Disposition
    {
        private DaoHelper daoHelper = new DaoHelper();
        private DataContainer dc = Start.xmlData;
        private Produktionsprogramm produktionsProgramm = new Produktionsprogramm();
        private List<DispositionErgebnis> ergebnisse = new List<DispositionErgebnis>();
        


        public Produktionsprogramm getProduktionsProgramm()
        {
            return produktionsProgramm;
        }

        public void setProduktionsProgramm(Produktionsprogramm programm)
        {
            this.produktionsProgramm = programm;
        }

        public void einkaufProgrammBerechnen()
        {
            Dictionary<int, TeilLieferdaten> teilLieferdaten = daoHelper.getTeilLieferdaten();
            //Offene Bestellungen aus XML, Key = Order_ID
            List<Bestellung> offeneBestellungen = new List<Bestellung>();
            //Lagerbestände aus der Vorperiode setzen
            foreach(int teileNummer in teilLieferdaten.Keys) {
                var teil_lagerbestand = dc.warehouseStock.article.SingleOrDefault(article => article.id.Equals(teileNummer));
                if (teil_lagerbestand != null)
                {
                    teilLieferdaten[teileNummer].setLagerMenge(teil_lagerbestand.amount);
                }
                                    
            }

            //TODO hier weitermachen
            foreach (var order in dc.futureInwardStockMovement.orders.AsEnumerable())
            {
                Teil teil = new Teil();
                teil.setNummer(order.article);
                Bestellung bestellung = new Bestellung(order.orderperiod, order.mode, order.amount, teil, false);

                offeneBestellungen.Add(bestellung);
                


            }                     
            

            //Stücklisten holen
            Stueckliste stueckListeP1 = daoHelper.getStueckListeByFahrradnummer(1);
            Stueckliste stueckListeP2 = daoHelper.getStueckListeByFahrradnummer(2);
            Stueckliste stueckListeP3 = daoHelper.getStueckListeByFahrradnummer(3);
            Dictionary<int, int> teileProduktion = new Dictionary<int, int>(Start.teile_Produktion);
            berechnen(teilLieferdaten, stueckListeP1, stueckListeP2, stueckListeP3, teileProduktion);

        }

        private void berechnen(Dictionary<int, TeilLieferdaten> teilLieferdaten, Stueckliste stueckListeP1, Stueckliste stueckListeP2, Stueckliste stueckListeP3, Dictionary<int, int> teileProduktion) {
            foreach (var kvp in teilLieferdaten)
            {
                //Ergebnisse
                int nettoBedarfPeriode1 = 0;
                int bruttoBedarfPeriode2 = 0;
                int bruttoBedarfPeriode3 = 0;
                int bruttoBedarfPeriode4 = 0;

                //Teilenummer
                int teilenummer = kvp.Key;

                //Teillieferdaten
                TeilLieferdaten currentLieferDaten = kvp.Value;


                StuecklisteItem stueckListeItemP1 = stueckListeP1.getMengenBedarfKaufteilByTeilenummer(teilenummer, stueckListeP1);
                StuecklisteItem stueckListeItemP2 = stueckListeP2.getMengenBedarfKaufteilByTeilenummer(teilenummer, stueckListeP2);
                StuecklisteItem stueckListeItemP3 = stueckListeP3.getMengenBedarfKaufteilByTeilenummer(teilenummer, stueckListeP3);

                Stueckliste[] alleStuecklisten = new Stueckliste[3];
                alleStuecklisten[0] = stueckListeP1;
                alleStuecklisten[1] = stueckListeP2;
                alleStuecklisten[2] = stueckListeP3;

                List<StuecklisteItem> teileVerwendungsNachweis = new List<StuecklisteItem>();
                //TODO Nochmal überprüfen zwecks doppelten Einträgen E16,17,26
                for (int i = 0; i < alleStuecklisten.Count(); ++i)
                {
                    teileVerwendungsNachweis.AddRange(alleStuecklisten[i].getTeileVerwendungsNachweis(teilenummer, teileProduktion));
                }

                                

                //Periode 1 anhand der Werte aus dem Produktionsprogramm -> mit Sicherheitsbeständen
                if (stueckListeItemP1 != null)
                {
                    nettoBedarfPeriode1 += stueckListeP1.getNettoBedarfToFertigteilByTeilenummer(teilenummer, teileProduktion);
                    
                }
                if (stueckListeItemP2 != null)
                {
                    nettoBedarfPeriode1 += stueckListeP2.getNettoBedarfToFertigteilByTeilenummer(teilenummer, teileProduktion);
                }
                if (stueckListeItemP3 != null)
                {
                    nettoBedarfPeriode1 += stueckListeP3.getNettoBedarfToFertigteilByTeilenummer(teilenummer, teileProduktion);
                }

                //Periode 2
                if (stueckListeItemP1 != null)
                {
                    bruttoBedarfPeriode2 += stueckListeItemP1.getAnzahl() * produktionsProgramm.P1_2;
                }
                if (stueckListeItemP2 != null)
                {
                    bruttoBedarfPeriode2 += stueckListeItemP2.getAnzahl() * produktionsProgramm.P2_2;
                }
                if (stueckListeItemP3 != null)
                {
                    bruttoBedarfPeriode2 += stueckListeItemP3.getAnzahl() * produktionsProgramm.P3_2;
                }

                //Periode 3
                if (stueckListeItemP1 != null)
                {
                    bruttoBedarfPeriode3 += stueckListeItemP1.getAnzahl() * produktionsProgramm.P1_3;
                }
                if (stueckListeItemP2 != null)
                {
                    bruttoBedarfPeriode3 += stueckListeItemP2.getAnzahl() * produktionsProgramm.P2_3;
                }
                if (stueckListeItemP3 != null)
                {
                    bruttoBedarfPeriode3 += stueckListeItemP3.getAnzahl() * produktionsProgramm.P3_3;
                }

                //Periode 4
                if (stueckListeItemP1 != null)
                {
                    bruttoBedarfPeriode4 += stueckListeItemP1.getAnzahl() * produktionsProgramm.P1_4;
                }
                if (stueckListeItemP2 != null)
                {
                    bruttoBedarfPeriode4 += stueckListeItemP2.getAnzahl() * produktionsProgramm.P2_4;
                }
                if (stueckListeItemP3 != null)
                {
                    bruttoBedarfPeriode4 += stueckListeItemP3.getAnzahl() * produktionsProgramm.P3_4;
                }

                Teil verwendetesTeil = null;

                if (stueckListeItemP1 != null)
                {
                    verwendetesTeil = stueckListeItemP1.getTeil();
                }

                if (stueckListeItemP2 != null)
                {
                    verwendetesTeil = stueckListeItemP2.getTeil();
                }

                if (stueckListeItemP3 != null)
                {
                    verwendetesTeil = stueckListeItemP3.getTeil();
                }

                DispositionErgebnis kaufTeilDispoErgebnis = new DispositionErgebnis(
                        verwendetesTeil, stueckListeItemP1,
                        stueckListeItemP2, stueckListeItemP3,
                        currentLieferDaten, nettoBedarfPeriode1,
                        bruttoBedarfPeriode2, bruttoBedarfPeriode3,
                        bruttoBedarfPeriode4, teileVerwendungsNachweis);

                //Ergebnis zur Liste zufügen
                ergebnisse.Add(kaufTeilDispoErgebnis);
            }
            
        }

        public List<DispositionErgebnis> getDispositionsErgebnisse()
        {
            return this.ergebnisse;
        }

       

          
           
        

    }
}
