﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    [Serializable]
    class TeilLieferdaten
    {

    // Serializable in C#?
	private int lagerMenge;
	private int diskontMenge;
    private double teilePreis;
	private double bestellkosten;
	private double wiederbeschaffungszeitPeriode;
	private int wiederbeschaffungszeitTage;
	private double abweichungPeriode;
	private int abweichungtage;
	private int teileNummer;
    

	public int getLagerMenge() {
		return lagerMenge;
	}

	public void setLagerMenge(int lagerMenge) {
		this.lagerMenge = lagerMenge;
	}

	public int getDiskontMenge() {
		return diskontMenge;
	}

	public void setDiskontMenge(int diskontMenge) {
		this.diskontMenge = diskontMenge;
	}

	public double getBestellkosten() {
		return bestellkosten;
	}

	public void setBestellkosten(double bestellkosten) {
		this.bestellkosten = bestellkosten;
	}

	public double getWiederbeschaffungszeitPeriode() {
		return wiederbeschaffungszeitPeriode;
	}

	public void setWiederbeschaffungszeitPeriode(
			double wiederbeschaffungszeitPeriode) {
		this.wiederbeschaffungszeitPeriode = wiederbeschaffungszeitPeriode;
	}

	public int getWiederbeschaffungszeitTage() {
		return wiederbeschaffungszeitTage;
	}

	public void setWiederbeschaffungszeitTage(int wiederbeschaffungszeitTage) {
		this.wiederbeschaffungszeitTage = wiederbeschaffungszeitTage;
	}

	public double getAbweichungPeriode() {
		return abweichungPeriode;
	}

	public void setAbweichungPeriode(double abweichungPeriode) {
		this.abweichungPeriode = abweichungPeriode;
	}

	public int getAbweichungTage() {
		return abweichungtage;
	}

	public void setAbweichungTage(int abweichungtage) {
		this.abweichungtage = abweichungtage;
	}

	public int getTeileNummer() {
		return teileNummer;
	}

	public void setTeileNummer(int teileNummer) {
		this.teileNummer = teileNummer;
	}

    public double getTeilePreis()
    {
        return teilePreis;
    }

    public void setTeilePreis(double preis)
    {
        this.teilePreis = preis;
    }
    //Periode in Tage konvertieren
    public void convertPeriodeZuTage(double wiederBeschaffungszeitPeriode, double abweichungPeriode) {
        setAbweichungTage(Convert.ToInt32(abweichungPeriode*5));
        setWiederbeschaffungszeitTage(Convert.ToInt32(wiederbeschaffungszeitPeriode*5));
    }

    public override string ToString()
    {
        return "Teil_Lieferdaten: [teilenummer= " + teileNummer
            + ", Wiederbeschaffungszeit/Periode= " + wiederbeschaffungszeitPeriode
                    + ", Wiederbeschaffungszeit/Tage= " + wiederbeschaffungszeitTage
                        +   ", Abweichung/Periode= " + abweichungPeriode
                            +   ", Abweichung/Tage=" + abweichungtage
                                + " , Diskontmenge/Stk= " + diskontMenge + "]";
    }
    }
}
