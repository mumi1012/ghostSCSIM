using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class TeilDisposition
    {

    // Integer in Java != int32?
    Int32 reihe;
	Int32 nummer; 
	Int32 vertriebswunsch;
	Int32 geplante_lagermenge;
	Int32 lagerbestand_ende_vorperiode;
	Int32 auftraege_warteschlange;
	Int32 auftrage_bearbeitung;
	Int32 produktionsauftrag_naechste_periode;
	public Int32 getReihe() {
		return reihe;
	}
	public void setReihe(Int32 reihe) {
		this.reihe = reihe;
	}
	public Int32 getNummer() {
		return nummer;
	}
	public void setNummer(Int32 nummer) {
		this.nummer = nummer;
	}
	public Int32 getGeplante_lagermenge() {
		return geplante_lagermenge;
	}
	public void setGeplante_lagermenge(Int32 geplante_lagermenge) {
		this.geplante_lagermenge = geplante_lagermenge;
	}
	public Int32 getLagerbestand_ende_vorperiode() {
		return lagerbestand_ende_vorperiode;
	}
	public void setLagerbestand_ende_vorperiode(Int32 lagerbestand_ende_vorperiode) {
		this.lagerbestand_ende_vorperiode = lagerbestand_ende_vorperiode;
	}
	public Int32 getAuftraege_warteschlange() {
		return auftraege_warteschlange;
	}
	public void setAuftraege_warteschlange(Int32 auftraege_warteschlange) {
		this.auftraege_warteschlange = auftraege_warteschlange;
	}
	public Int32 getAuftrage_bearbeitung() {
		return auftrage_bearbeitung;
	}
	public void setAuftrage_bearbeitung(Int32 auftrage_bearbeitung) {
		this.auftrage_bearbeitung = auftrage_bearbeitung;
	}
	public Int32 getProduktionsauftrag_naechste_periode() {
		return produktionsauftrag_naechste_periode;
	}
	public void setProduktionsauftrag_naechste_periode(
			Int32 produktionsauftrag_naechste_periode) {
		this.produktionsauftrag_naechste_periode = produktionsauftrag_naechste_periode;
	}

	public override String toString() {
		return "Teil_dispohelp [reihe=" + reihe + ", nummer=" + nummer
				+ ", geplante_lagermenge=" + geplante_lagermenge
				+ ", lagerbestand_ende_vorperiode="
				+ lagerbestand_ende_vorperiode + ", auftraege_warteschlange="
				+ auftraege_warteschlange + ", auftrage_bearbeitung="
				+ auftrage_bearbeitung
				+ ", produktionsauftrag_naechste_periode="
				+ produktionsauftrag_naechste_periode + "]";
	}
    public Int32 getVertriebswunsch()
    {
		return vertriebswunsch;
	}
    public void setVertriebswunsch(Int32 vertriebswunsch)
    {
		this.vertriebswunsch = vertriebswunsch;
	}

    }
}
