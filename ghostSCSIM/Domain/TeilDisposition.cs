using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    class TeilDisposition
    {
        private Teil teil;
        private int vertriebswunsch;
        private int geplante_lagermenge;
        private int lagerbestand_ende_vorperiode;
        private int auftraege_warteschlange;
	    private int auftrage_bearbeitung;
	    private int produktionsauftrag_naechste_periode;


        public TeilDisposition(Teil teil)
        {
            this.teil = teil;
        }

        public TeilDisposition() { }

	    public int getGeplante_lagermenge() {
		    return geplante_lagermenge;
	    }
	    public void setGeplante_lagermenge(int geplante_lagermenge) {
		    this.geplante_lagermenge = geplante_lagermenge;
	    }
	    public int getLagerbestand_ende_vorperiode() {
		    return lagerbestand_ende_vorperiode;
	    }
	    public void setLagerbestand_ende_vorperiode(int lagerbestand_ende_vorperiode) {
		    this.lagerbestand_ende_vorperiode = lagerbestand_ende_vorperiode;
	    }
	    public int getAuftraege_warteschlange() {
		    return auftraege_warteschlange;
	    }
	    public void setAuftraege_warteschlange(int auftraege_warteschlange) {
		    this.auftraege_warteschlange = auftraege_warteschlange;
	    }
	    public int getAuftrage_bearbeitung() {
		    return auftrage_bearbeitung;
	    }
	    public void setAuftrage_bearbeitung(int auftrage_bearbeitung) {
		    this.auftrage_bearbeitung = auftrage_bearbeitung;
	    }
	    public int getProduktionsauftrag_naechste_periode() {
		    return produktionsauftrag_naechste_periode;
	    }
	    public void setProduktionsauftrag_naechste_periode(
			    int produktionsauftrag_naechste_periode) {
		    this.produktionsauftrag_naechste_periode = produktionsauftrag_naechste_periode;
	    }

        public int getVertriebswunsch()
        {
            return vertriebswunsch;
        }
        public void setVertriebswunsch(int vertriebswunsch)
        {
            this.vertriebswunsch = vertriebswunsch;
        }

	    public override string ToString() {
		    return "Teil_dispohelp [Teil=" + teil.ToString() + ", geplante_lagermenge=" + geplante_lagermenge
				    + ", lagerbestand_ende_vorperiode="
				    + lagerbestand_ende_vorperiode + ", auftraege_warteschlange="
				    + auftraege_warteschlange + ", auftrage_bearbeitung="
				    + auftrage_bearbeitung
				    + ", produktionsauftrag_naechste_periode="
				    + produktionsauftrag_naechste_periode + "]";
	    }
     

    }
}
