using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ghostSCSIM.Service.Disposition;
using ghostSCSIM.Domain;
using System.Text;


namespace ghostSCSIM
{
    public partial class Teilinformation : Form
    {
        // Get current language
        private String _culInfo = Thread.CurrentThread.CurrentUICulture.Name;
        // -----------------------------------------------------------------
        private const String de = "de";
        private const String deKTeil = "Kaufteil Nr.: ";
        private const String enKTeil = "Ordering part No.: ";
        private const String deAPlatz = "Arbeitsplatz Nr.: ";
        private const String enAPlatz = "Workstation No.: ";
        private const String deLDauer = "Lieferdauer: ";
        private const String enLDauer = "Delivery duration: ";
        private const String deDMenge = "Diskontmenge: ";
        private const String enDMenge = "Discount amount: ";
        private const String dePreis = "Preis: ";
        private const String enPreis = "Price: ";
        private const String deBKosten = "Bestellkosten: ";
        private const String enBKosten = "Order costs: ";
        private const String deSumme = "Summe: ";
        private const String enSumme = "Sum: ";
        private const String deRZeit = "Rüstungszeit ";
        private const String enRZeit = "Setup time: ";
        private const String deKBedarf = "Kapazitätsbedarf: ";
        private const String enKBedarf = "Capacity demand: ";
        private const String deTeil = "Teil ";
        private const String enTeil = "Part ";
        private const String deInTeil = "In Teil ";
        private const String enInTeil = "In part ";
        // -----------------------------------------------------------------
        private int _nummer;
        DataContainer _dc = DataContainer.Instance;
        
        
        // Constructor
        public Teilinformation(string name, int nummer)
        {
            InitializeComponent();
            _nummer = nummer;
           

            // Set label to ordering part
            if (name.Equals("kteil"))
            {
                label1.Text = _culInfo.Contains(de) ? deKTeil + _nummer : enKTeil + _nummer;
            }
            // Set label to working station
            else if (name.Equals("arbeitsplatz"))
            {
                label1.Text = _culInfo.Contains(de) ? deAPlatz + _nummer : enAPlatz + _nummer;
            }
        }
        internal void GetTeilvonETeilMitMenge(List<DispositionErgebnis> dispositionsErgebnis)
        {
            if (dispositionsErgebnis != null)
            {


                DispositionErgebnis einDispoErgebnis = dispositionsErgebnis.First(dispo => dispo.getTeil().getNummer().Equals(_nummer));
                int sum = 0;
                
                if (einDispoErgebnis != null)
                {

                    StringBuilder sb = new StringBuilder();
                    List<StuecklisteItem> teileVerwendungsnachweis = einDispoErgebnis.getTeileVerwendungsnachweis();
                    foreach (StuecklisteItem item in teileVerwendungsnachweis)
                    {
                        int value = 0;
                        Teil verwendetesTeil = item.getParent();
                        value = item.getAnzahl() * item.getNettobedarf();


                        sb.AppendLine((_culInfo.Contains(de) ? deInTeil : enInTeil) + verwendetesTeil.getNummer());
                        sb.AppendLine("\t" + item.getAnzahl() + " * " + item.getNettobedarf() + " = " + value);
                        sb.AppendLine();
                        
                        ausgabe.Text = sb.ToString();
                                   
                        sum += item.getAnzahl() * item.getNettobedarf();


                    }
                    ausgabe.Text += (_culInfo.Contains(de) ? deSumme : enSumme) + sum;
                }

            }          
        }
       
        private void TeilInformation_Load(object sender, EventArgs e)
        {

        }
    }
}
