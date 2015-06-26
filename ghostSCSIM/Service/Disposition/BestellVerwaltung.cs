using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.Domain;

namespace ghostSCSIM.Service.Disposition
{
    class BestellVerwaltung
    {
        List<Bestellung> bestellungListe;
        private double lieferRisiko = Start.lieferRisiko;
        private int aktuellePeriode = Start.xmlData.period;

        public BestellVerwaltung()
        {
            bestellungListe = new List<Bestellung>();
        }

        public List<Bestellung> getBestellungListe()
        {
            return bestellungListe;
        }

        public void setBestellungListe(List<Bestellung> newBestellungListe)
        {
            bestellungListe.Clear();
            bestellungListe = newBestellungListe;
        }

        public void generiereBestellListe(List<DispositionErgebnis> dispoErgebnisse) 
        {
            if (dispoErgebnisse == null || dispoErgebnisse.Count == 0)
            {
                return;
            }
            

            double faktorVerfuegbarInProduktion = 0.4;

            foreach (DispositionErgebnis singleDispo in dispoErgebnisse)
            {
                double periodeBeginn = 1.0;

                int mengeNorm = 0;
                int mengeEil = 0;

                int[] bestaende = singleDispo.getBestaende();


                TeilLieferdaten lieferDaten = singleDispo.getLieferDaten();
                int diskontMenge = lieferDaten.getDiskontMenge();
                double teilePreis = lieferDaten.getTeilePreis();
                double bestellKosten = lieferDaten.getBestellkosten();

                double lieferungNorm = Math.Ceiling(lieferDaten.getWiederbeschaffungszeitPeriode()
                    + (lieferDaten.getAbweichungPeriode() * (Start.lieferRisiko / 100))) + faktorVerfuegbarInProduktion;
                double lieferungEil = (lieferDaten.getWiederbeschaffungszeitPeriode() / 2) + faktorVerfuegbarInProduktion;

                int index = 0;
                while (lieferungNorm >= periodeBeginn)
                {
                    if (bestaende[index] <= 0)
                    {
                        if (periodeBeginn == lieferungNorm)
                        {
                            if (bestaende[index] < 0)
                            {
                                mengeNorm += berechneMenge(bestaende[index] * (-1), diskontMenge, teilePreis, bestellKosten);
                            }
                            else
                            {
                                if (bestaende[index + 1] < 0)
                                {
                                    mengeNorm += berechneMenge(bestaende[index] * (-1), diskontMenge, teilePreis, bestellKosten);
                                }
                            }
                        }
                        else
                        {
                            if (bestaende[index] < 0)
                            {
                                mengeEil += berechneMenge(bestaende[index] * (-1), diskontMenge, teilePreis, bestellKosten);

                            }
                        }
                    }
                    ++index;
                    ++periodeBeginn;
                }
                //Erzeuge Bestellungen
                if (mengeNorm > 0)
                {
                    bestellungListe.Add(new Bestellung(singleDispo.getTeil(), mengeNorm, 5));
                }
                if (mengeEil > 0)
                {
                    bestellungListe.Add(new Bestellung(singleDispo.getTeil(), mengeEil, 4));
                }
            }
            optimiereBestellPositionen(dispoErgebnisse);
        }

        private int berechneMenge(int bestellmenge, int diskontmenge, double preis, double fixKosten)
        {
            int outputMenge;

            //Berechne den Preisunterschied zwischen Diskontmenge und der Fehlmenge
            if (bestellmenge < diskontmenge)
            {
                double preisFehlmenge = bestellmenge * preis + fixKosten;
                double preisMitDiskont = (diskontmenge * (preis * 0.9)) + fixKosten;

                //Diskont ist billiger
                if (preisFehlmenge > preisMitDiskont)
                {
                    outputMenge = diskontmenge;
                }
                else
                {
                    outputMenge = bestellmenge;
                    
                }
            }
            else
            {
                outputMenge = bestellmenge;
            }
            return outputMenge;
        }

        private void optimiereBestellPositionen(List<DispositionErgebnis> dispoErgebnisse)
        {
            //Wenn mehrere Eilbestellungen für das selbe Kaufteil gefunden werden:
            //Bestellungen als eine einzelne summiert aufführen
            foreach (DispositionErgebnis dispoErgebnis in dispoErgebnisse)
            {
                var eilBestellungen = 
                    bestellungListe.Where(bp => bp.getTeil().Equals(dispoErgebnis.getTeil()) && bp.getBestellTyp().Equals(Bestelltyp.F)).ToList();
                if (eilBestellungen.Any())
                {
                    int bestellMenge = 0;
                    foreach (Bestellung bestellung in eilBestellungen)
                    {
                        //Normalbestellungen filtern und zur Eilbestellung zufügen
                        var normalBestellungen =
                            bestellungListe.Where(nb => nb.getTeil().Equals(bestellung.getTeil()) && !nb.getBestellTyp().Equals(Bestelltyp.F)).ToList();
                        foreach (Bestellung normBestellung in normalBestellungen)
                        {
                            bestellMenge += normBestellung.getMenge();
                            bestellungListe.Remove(normBestellung);
                        }
                        bestellMenge += bestellung.getMenge();
                        bestellungListe.Remove(bestellung);
                    }
                    bestellungListe.Add(new Bestellung(dispoErgebnis.getTeil(), bestellMenge, 4));
                }
            }

        }
    }
}
