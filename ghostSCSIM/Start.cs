using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using ghostSCSIM.XML;
using System.Data.OleDb;
using ghostSCSIM.DatenbankDataSetTableAdapters;
using ghostSCSIM.DAO;
using ghostSCSIM.Domain;
using ghostSCSIM.service;
using ghostSCSIM.Service.Disposition;
using ghostSCSIM.Views;

namespace ghostSCSIM
{
    public partial class Start : Form
    {
        //Risikovariable für die Abweichung der Lieferdaten
        public static double lieferRisiko = 0;


        //DaoHelper
        private DaoHelper dao = new DaoHelper();

        // Neuen Datenbehälter für den XML Input anlegen
        public static DataContainer xmlData = DataContainer.Instance;
        public static Dictionary<int, int> teile_Produktion = new Dictionary<int, int>();

        //Datenbehälter für den XML Output anlegen
        readonly DataContainerResult xmlResult = DataContainerResult.Instance;

        //Produktionsprogramm aus Prognosen
        private Produktionsprogramm produktionsProgramm = new Produktionsprogramm();

        //Teilestammdaten
        private static List<Teil> teileStammdaten = new List<Teil>();

        //Bestellliste
        private List<Bestellung> bestellungsListe;
                          
                 
        //Produktionsprogramm aus View, Key = Teilenummer
        private static Dictionary<int, int> produktionP1 = new Dictionary<int, int>();
        private static Dictionary<int, int> produktionP2 = new Dictionary<int, int>();
        private static Dictionary<int, int> produktionP3 = new Dictionary<int, int>();

        private List<DispositionErgebnis> dispoErgebnis { get; set; }

                

        
        
        private List<String> produktionsMengenAusView = new List<String>();

        public Start()
        {
            InitializeComponent();

        }

        private void dEToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SwitchLanguage("de");

        }
        private void eNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchLanguage("en");

        }
        private void SwitchLanguage(string language)
        {
            /*foreach (Control c in this.Controls)
            {
                ComponentResourceManager resourcesManager = new ComponentResourceManager(typeof(Start));
                resourcesManager.ApplyResources(c, c.Name, new CultureInfo(language));

                if (c.GetType() == typeof(DataGridView))
                {
                    var dgv = (DataGridView)c;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        resourcesManager.ApplyResources(col, col.Name, new CultureInfo(language));
                    }
                }
            }*/

            switch (language)
            {
                case "de":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
                    Controls.Clear();
                    //Events.Dispose();
                    InitializeComponent();
                    break;
                case "en":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    Controls.Clear();
                    //Events.Dispose();
                    InitializeComponent();
                    break;
            }

        }



        //private void changeLabelTextAndVisibility(String clickedItem) 
        //   {
        //       if (dataGridLabel.Visible == false) 
        //           dataGridLabel.Visible = true;

        //       dataGridLabel.Text = clickedItem;
        //   }

        //  private void warehouseStockToolStripMenuItem_Click(object sender, EventArgs e)
        //  {
        //      changeLabelTextAndVisibility(this.warehouseStockToolStripMenuItem.Text);
        //      dataGridView1.DataSource = xmlData.warehouseStock.article;


        //  }

        //  private void ordersInWorkToolStripMenuItem_Click_1(object sender, EventArgs e)
        //  {
        //      changeLabelTextAndVisibility(this.ordersInWorkToolStripMenuItem.Text);
        //      dataGridView1.DataSource = xmlData.ordersInWork.workplace;
        //  }

        //  private void futureInwardStockMovementToolStripMenuItem_Click_1(object sender, EventArgs e)
        //  {
        //      changeLabelTextAndVisibility(this.futureInwardStockMovementToolStripMenuItem.Text);
        //      dataGridView1.DataSource = xmlData.futureInwardStockMovement.orders;
        //  }

        //  private void waitingListToolStripMenuItem_Click(object sender, EventArgs e)
        //  {
        //      changeLabelTextAndVisibility(this.waitingListToolStripMenuItem.Text);
        //      dataGridView1.DataSource = xmlData.waitingListWorkstations.workplaces;

        //  }

        private void openSettings(object sender, EventArgs e)
        {
            Einstellungen settings = new Einstellungen();
            settings.Show();

            
        }

        private void xmlInputButton_Click(object sender, EventArgs e)
        {

            var fileDialog = new OpenFileDialog { };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlIO xmlInput = new XmlIO(fileDialog.FileName);
                xmlData = (DataContainer)xmlInput.xml;

                xmlData.setXmlImported(true);
                lbl_xml_period.Text = xmlData.period.ToString();
                lbl_xml_period.Visible = true;

                MessageBox.Show("XML_File erfolgreich importiert!");
            }
        }



        private void fillFormsWithData(object sender, EventArgs e)
        {
            if (xmlData.getXmlImported())
            {
            



                

                //Direktverkauf DataGridView
                //TODO: Eingaben validieren! Festlegen wann der Direktverkauf gesichert wird! > Extra Methode
                foreach (DataGridViewRow row in dataGridView_dirver_direktverkauf.Rows)
                {
                    int teilenummer = Convert.ToInt32(row.Cells[0].Value);
                    int verkaufsMenge = Convert.ToInt32(row.Cells[1].Value);
                    double preis = Convert.ToDouble(row.Cells[2].Value);
                }

            }



        }



        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label_pp_p2_59_Click(object sender, EventArgs e)
        {


        }


        private void Start_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "datenbankDataSet.Bestelldaten". Sie können sie bei Bedarf verschieben oder entfernen.
            this.bestelldatenTableAdapter.Fill(this.datenbankDataSet.Bestelldaten);


        }

        private void testButton_Click(object sender, EventArgs e)
        {
            DaoHelper daoHelper = new DaoHelper();

            List<Teil> teileListe = daoHelper.getTeilStammdaten();

            TeilLieferdaten lieferDaten = daoHelper.getTeilLieferdatenByTeilenummer(21);

            MessageBox.Show(teileListe[0].getVerwendung().ToString());
            MessageBox.Show(lieferDaten.ToString());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_best_bestellliste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && dataGridView_best_bestellliste.CurrentCell.Value != null)
            {
                dataGridView_best_bestellliste.Rows.RemoveAt(dataGridView_best_bestellliste.CurrentRow.Index);
            }
        }

        private void tabPage_best_kaufteilverbrauch_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_best_kaufteillager_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView_dirver_direktverkauf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && dataGridView_dirver_direktverkauf.CurrentCell.Value != null)
            {
                dataGridView_dirver_direktverkauf.Rows.RemoveAt(dataGridView_dirver_direktverkauf.CurrentRow.Index);
            }
        }
        private void dataGridView_best_kaufteileverbrauch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_best_kaufteileverbrauch.ScrollBars = ScrollBars.Both;

            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var teileInformation = new Teilinformation("kteil", Convert.ToInt32(dataGridView_best_kaufteileverbrauch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                teileInformation.GetTeilvonETeilMitMenge(dispoErgebnis);
               
                teileInformation.Show();
            }
        }

       //Testbutton für das Generieren der Input XML-Datei
        private void createXml_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog { };
            fileDialog.Title = "XML speichern";
            fileDialog.Filter = "XML-Dateien (*.xml)|*.xml";

            int game = xmlData.game;
            int group = xmlData.group;
            int period = xmlData.period;

            fileDialog.FileName = game + "_" + group + "_" + period + "input";

            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {
                collectOutputData();
                XmlOutput xmlFile = new XmlOutput();
                xmlFile.createXml(fileDialog.FileName);
            }
        }

        private void collectOutputData()
        {
            //Vertriebswünsche:
            //xmlResult.setVertriebswuensche(Convert.ToInt32(pp_p1_p1_vw.Text.), Convert.ToInt32(pp_p2_p2_vw.Text), Convert.ToInt32(pp_p3_p3_vw.Text));
            xmlResult.setVertriebswuensche(100, 100, 100);

            //Bestellliste
            foreach (DataGridViewRow row in dataGridView_best_bestellliste.Rows)
            {
                int teilenummer = Convert.ToInt32(row.Cells[0].Value);
                //TODO: Teil anhand der Teilenummer finden (ohne Pfusch)!
                Teil teil = dao.getKaufteileStammdaten()[teilenummer];
                int menge = Convert.ToInt32(row.Cells[1].Value);
                bool eilbestellung = false;
                if (row.Cells[2].Value != null)
                {
                    eilbestellung = true;
                }
                xmlResult.addBestellposition(teil, menge, eilbestellung);
            }

            //Direktverkauf:
            foreach (DataGridViewRow row in dataGridView_dirver_direktverkauf.Rows)
            {
                int teilenummer = Convert.ToInt32(row.Cells[0].Value);
                //TODO: Teil anhand der Teilenummer finden (ohne Pfusch)!
                Teil teil = dao.getKaufteileStammdaten()[teilenummer];
                int menge = Convert.ToInt32(row.Cells[1].Value);
                double preis = Convert.ToDouble(row.Cells[2].Value);
                xmlResult.addDirektverkauf(teil, menge, preis);
            }
        }

        /// <summary>
        /// Methode um den selected Tab mit Daten zu füllen
        /// </summary>
        /// private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        private void fillTabsWithData(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPrognose"])
            {
               
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabProdplan"])
            {
                if (xmlData.getXmlImported())
                {
                    //Erste TabPage laden
                    tabControl2.SelectedTab = tabControl2.TabPages["tabKinderf"];
                    refreshKinderFahrradView();
                                       
                }
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabKapa"])
            {
                if (xmlData.getXmlImported())
                {

                    List<ArbeitsplatzKapa> liste = dao.getArbeitsplaetzeKapa();
                    dataGridView_kp_uebersicht.Rows.Clear();
                    if (dataGridView_kp_uebersicht.Rows.Count == 0)
                    {
                        for (int i = 1; i < 16; i++)
                        {
                            List<ArbeitsplatzKapa> l = liste.Where(arbeitsplatzKapa => arbeitsplatzKapa.getArbeitsplatz().Equals(i)).ToList();
                            int kapazitätsbedarf = 0;
                            int ruestzeit = 0;
                            int rueckstaendez = 0;
                            List<Waitinglist> waitinglist = xmlData.waitingListWorkstations.getWaitinglistByArbeitsplatzId(i);
                            int rueckstaendekapa = 0;
                            foreach (Waitinglist wl in waitinglist)
                            {
                                rueckstaendekapa += wl.timeneed;
                                int teil = wl.item;
                                ArbeitsplatzKapa t = liste.Single(a => a.getTeilfk().Equals(teil) && a.getArbeitsplatz().Equals(i));
                                rueckstaendez += t.getRuestzeit();
                            }
                            rueckstaendekapa += xmlData.ordersInWork.getInBearbeitungMengeByArbeitsplatz(i);

                            foreach (ArbeitsplatzKapa ak in l)
                            {

                                int teil = ak.getTeilfk();
                                int ruestz = ak.getRuestzeit();
                                int fertigz = ak.getFertigungszeit();

                                int kapazitätsbedarfTeil = 0;
                                //Nur wenn der Key gefunden wurde berechnen, sonst KeyNotFoundException
                                if(teile_Produktion.TryGetValue(teil, out kapazitätsbedarfTeil)) {
                                   kapazitätsbedarfTeil = fertigz * teile_Produktion[teil];
                                }

                               
                                ruestzeit += ruestz;
                                kapazitätsbedarf += kapazitätsbedarfTeil;

                            }

                            int gesamt = kapazitätsbedarf + ruestzeit + rueckstaendekapa + rueckstaendez;

                            if (gesamt > 3600 && gesamt <= 7200)
                            {
                                dataGridView_kp_uebersicht.Rows.Add(i, kapazitätsbedarf.ToString(), ruestzeit.ToString(), rueckstaendekapa.ToString(), rueckstaendez.ToString(), gesamt.ToString(), "0", true, false);
                            }
                            else if (gesamt > 7200)
                            {
                                dataGridView_kp_uebersicht.Rows.Add(i, kapazitätsbedarf.ToString(), ruestzeit.ToString(), rueckstaendekapa.ToString(), rueckstaendez.ToString(), gesamt.ToString(), "0", true, true);
                            }
                            else
                                dataGridView_kp_uebersicht.Rows.Add(i, kapazitätsbedarf.ToString(), ruestzeit.ToString(), rueckstaendekapa.ToString(), rueckstaendez.ToString(), gesamt.ToString(), "0", false, false);
                        }
                    }
                }
            }

            //Bestellungen Tabs befüllen
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabBestellung"])
            {
                
                if (xmlData.getXmlImported())
                {

                    tabControl_best.SelectedTab = tabControl_best.TabPages["tabPage_best_kaufteillager"];
                    //Bestellung Tab
                    if (Start.teileStammdaten.Count == 0)
                    {
                        Start.teileStammdaten = dao.getKaufteileStammdaten();
                    }
                   
                    

                    Disposition kaufteileDisposition = new Disposition();
                    kaufteileDisposition.setProduktionsProgramm(produktionsProgramm);
                    kaufteileDisposition.einkaufProgrammBerechnen();

                    
                    
                    
                    if (dataGridView_best_kaufteillager.Rows.Count == 0)
                    {


                        foreach (Teil teil in teileStammdaten)
                        {
                            int teilenummer = teil.getNummer();

                                String bezeichnung = teil.getBezeichnung();
                                int warehouseStockIndex = xmlData.warehouseStock.getIndexOfArticleById(teilenummer);
                                String bestand = xmlData.warehouseStock.article[warehouseStockIndex].amount.ToString();

                                TeilLieferdaten lieferdaten = dao.getTeilLieferdatenByTeilenummer(teilenummer);
                                String lieferdauerTage = lieferdaten.getWiederbeschaffungszeitTage().ToString();
                                String diskontmenge = lieferdaten.getDiskontMenge().ToString();
                                String bestellkosten = lieferdaten.getBestellkosten().ToString();

                                //Kaufteillager DataGridView befüllen
                                dataGridView_best_kaufteillager.Rows.Add(teilenummer.ToString(), bezeichnung, bestand, lieferdauerTage, diskontmenge, bestellkosten);

                                

                                
                                
                                
                        }
                    }

                    //Bestellliste DataGridView
                    //TODO: Eingaben validieren! Festlegen wann die Bestellung gesichert wird! > Extra Methode
                    foreach (DataGridViewRow row in dataGridView_best_bestellliste.Rows)
                    {
                        int teilenummer = Convert.ToInt32(row.Cells[0].Value); //hier sollten nur teilenummern von k teilen angenommen werden
                        int bestellmenge = Convert.ToInt32(row.Cells[1].Value);
                        bool eil = (DataGridViewCheckBoxCell)row.Cells[2].Value != null;
                    }
                }
                
            }

            

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabDirektV"])
            {
                MessageBox.Show("direktv");
            }

        }

        private void fillBestellungsTabWithData(object sender, EventArgs e) {

            if (tabControl_best.SelectedTab == tabControl_best.TabPages["tabPage_best_bestellung"])
            {
                generateBestellListe();
            }

            if (tabControl_best.SelectedTab == tabControl_best.TabPages["tabPage_best_kaufteileverbrauch"])
            {
                if (xmlData.getXmlImported())
                {

                    //ColumnHeaderText Bedarf/Bestand an Periode anpassen
                    //TODO: Für Sprachsteuerung anpassbar machen
                    //TODO: Angleichen bei neuer Column Anordnung
                    int periodN = xmlData.period;
                    
                    dataGridView_best_kaufteileverbrauch.Columns[4].HeaderText = "Bruttobedarf Periode " + periodN.ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[5].HeaderText = "Bruttobedarf Periode " + (periodN + 1).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[6].HeaderText = "Bruttobedarf Periode " + (periodN + 2).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[7].HeaderText = "Bruttobedarf Periode " + (periodN + 3).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[8].HeaderText = "Bestand Periode " + (periodN + 1).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[9].HeaderText = "Bestand Periode " + (periodN + 2).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[10].HeaderText = "Bestand Periode " + (periodN + 3).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[11].HeaderText = "Bestand Periode " + (periodN + 4).ToString();

                    //DataGrid vor dem Befüllen noch mal leeren um die aktuellen Daten zu erhalten
                    if (dataGridView_best_kaufteileverbrauch.Rows.Count > 0)
                    {
                        dataGridView_best_kaufteileverbrauch.Rows.Clear();
                    }
                    Disposition kaufteileDisposition = new Disposition();
                    kaufteileDisposition.setProduktionsProgramm(produktionsProgramm);
                    kaufteileDisposition.einkaufProgrammBerechnen();

                    dispoErgebnis = kaufteileDisposition.getDispositionsErgebnisse();

                    foreach (Teil teil in teileStammdaten)
                    {
                        int teilenummer = teil.getNummer();
                        int warehouseStockIndex = xmlData.warehouseStock.getIndexOfArticleById(teilenummer);
                        String bestand = xmlData.warehouseStock.article[warehouseStockIndex].amount.ToString();
                                int ausstehendeBestellungen = 0;

                                foreach (Order o in xmlData.futureInwardStockMovement.orders)
                                {
                                    if (o.article == teilenummer)
                                    {
                                        ausstehendeBestellungen = ausstehendeBestellungen + o.amount;
                                    }
                                }


                                //Kaufteilbedarf DataGridView befüllen
                                //TODO nochmal genau anschauen ob das auch so passt
                                DispositionErgebnis einDispoErgebnis = dispoErgebnis.First(dispo => dispo.getTeil().Equals(teil));


                                int bruttoBedarfP1 = einDispoErgebnis.getBruttoBedarfPeriode1();
                                int bruttoBedarfP2 = einDispoErgebnis.getBruttoBedarfPeriode2();
                                int bruttoBedarfP3 = einDispoErgebnis.getBruttoBedarfPeriode3();
                                int bruttoBedarfP4 = einDispoErgebnis.getBruttoBedarfPeriode4();
                                
                                //Lagerbestand nach Periode 1 wenn noch Restteile aus angekommener Bestellung vorhanden sind
                                int lagerBestand = einDispoErgebnis.getLieferDaten().getLagerMenge();
                                int lagerZugang = einDispoErgebnis.getLagerzugang();


                                int[] bestaende = einDispoErgebnis.getBestaende();
                                int bestandN1 = bestaende[0];
                                int bestandN2 = bestaende[1];
                                int bestandN3 = bestaende[2];
                                int bestandN4 = bestaende[3];
                                
                                                                     
                                int criticalFlag = einDispoErgebnis.getCriticalFlag();
                                
                                int indexOfNewRow = dataGridView_best_kaufteileverbrauch.Rows.Add(teilenummer.ToString(), lagerBestand.ToString(),lagerZugang.ToString(), false,
                                    bruttoBedarfP1.ToString(), bruttoBedarfP2.ToString(), bruttoBedarfP3.ToString(), bruttoBedarfP4.ToString(),
                                    bestandN1.ToString(), bestandN2.ToString(), bestandN3.ToString(), bestandN4.ToString());
                            
                            //Produktion kann nicht durchgeführt werden
                            if (criticalFlag == 2)
                            {
                                dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells["Column_best_kaufteileverbrauch_bestandN1"].Style.BackColor = Color.Red;
                                                               
                            }

                            if (criticalFlag == 1)
                            {
                                dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells["Column_best_kaufteileverbrauch_produktionskritisch"].Selected = true;
                                //dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells["Column_best_kaufteileverbrauch_bestandN1"].Style.ForeColor = ControlPaint.Light(Color.Red);
                            }

                            //Zellen leicht rot einfärben wenn Bestand negativ
                            for (int i = 0; i < bestaende.Count() ; ++i)
                            {
                                if (bestaende[i] <= 0)
                                {
                                    dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells[i+8].Style.ForeColor = ControlPaint.Light(Color.Red);
                                }
                            }
                            //TODO: Nur Dropdown erlauben, keinen Item Change zulassen
                            DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells[12];

                            comboCell.Items.Add(ausstehendeBestellungen.ToString() + " Gesamt");
                            comboCell.Value = comboCell.Items[0]; ;
                            foreach (Order o in xmlData.futureInwardStockMovement.orders)
                            {
                                if (o.article == teilenummer)
                                {
                                    //Prüfen ob Bestellung Anfang dieser Periode ankommt, dann zum Lagerbestand dazurechnen
                                    
                                    if (o.mode == 4)
                                    {
                                        comboCell.Items.Add(o.amount.ToString() + " Eil");
                                    }
                                    else
                                    {
                                        comboCell.Items.Add(o.amount.ToString() + " Normal");
                                    }
                                }
                            }
                        }
                    }

                    
                        
                }

                            
            
            

        }

        private void generateBestellListe()
        {            
                if (xmlData.getXmlImported() && dispoErgebnis != null)
                {
                    if (dataGridView_best_bestellliste.Rows.Count > 0)
                    {
                        dataGridView_best_bestellliste.Rows.Clear();
                    }
                    BestellVerwaltung bestellVerwaltung = new BestellVerwaltung();
                    bestellVerwaltung.generiereBestellListe(dispoErgebnis);

                    bestellungsListe = bestellVerwaltung.getBestellungListe();
                    if (bestellungsListe != null && bestellungsListe.Count > 0)
                    {
                        foreach (Bestellung bestellung in bestellungsListe)
                        {
                            bool bestellungType = false;
                            if (bestellung.getBestellTyp().Equals(Bestelltyp.F)) {
                                bestellungType = true;
                            }
                           
                            dataGridView_best_bestellliste.Rows.Add(bestellung.getTeil().getNummer(), bestellung.getMenge(), bestellungType);
                        }
                    }
                
            }
        }

        //Eingaben in Datenstruktur speichern
        //private void neueBestellungZufuegen(object sender, DataGridViewCellEventArgs e)
        //{
        //    int teileNummer = Convert.ToInt32(dataGridView_best_bestellliste.Rows[e.RowIndex].Cells["Column_best_bestelliste_nummer"]);
        //    int menge = Convert.ToInt32(dataGridView_best_bestellliste.Rows[e.RowIndex].Cells["Column_best_bestelliste_menge"]);
        //    int bestellTyp = 5;
        //    if (Convert.ToBoolean(dataGridView_best_bestellliste.Rows[e.RowIndex].Cells["Column_best_bestelliste_eil"]) == true)
        //    {
        //        bestellTyp = 4;
        //    }
        //    bestellungsListe.Add(new Bestellung(new Teil(teileNummer), menge, bestellTyp));
            
            
        //}

        //Eingaben in der Bestellliste validieren
        private void dataGridView_best_bestellliste_ValidateRow(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Keine Validierung falls es sich um eine neue Zeile handelt
            if (dataGridView_best_bestellliste.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewCell nummerCell = dataGridView_best_bestellliste.Rows[e.RowIndex].Cells[0];
            DataGridViewCell mengeCell = dataGridView_best_bestellliste.Rows[e.RowIndex].Cells[1];

            bestellliste_menge_validieren(mengeCell);
            bestellliste_nummer_validieren(nummerCell);

            

            //TODO: Falls User bei falschen Eingaben am Wechseln der Zeile gehindert werden soll (dazu Hilfsmethoden statt void bool):
            //e.Cancel = !bestellliste_menge_validieren(mengeCell) && !bestellliste_nummer_validieren(nummerCell);
        }

        //Hilfsmethode für die Validierung der "Nummer"-Spalte in der Bestellliste
        private void bestellliste_nummer_validieren(DataGridViewCell teilenummerCell)
        {
            DataGridViewRow row = dataGridView_best_bestellliste.Rows[teilenummerCell.RowIndex];
            teilenummerCell.ErrorText = "";
            row.ErrorText = "";
            int intValue;

            //Keine Eingabe
            if (string.IsNullOrEmpty(teilenummerCell.FormattedValue.ToString()))
            {
                teilenummerCell.ErrorText = "Bitte geben Sie das zu bestellende Kaufteil an!";
                row.ErrorText = "Bitte geben Sie das zu bestellende Kaufteil an!";
        }

            //Keine Zahl
            else if (!int.TryParse(teilenummerCell.FormattedValue.ToString(), out intValue))
            {
                teilenummerCell.ErrorText = "Bitte geben Sie eine Ganzzahl für das zu bestellende Kaufteil an!";
                row.ErrorText = "Bitte geben Sie eine Ganzzahl für das zu bestellende Kaufteil an!";
            }

            //Keine gültige Kaufteilnummer
            else
            {
                List<Teil> teileListe = new List<Teil>(dao.getKaufteileStammdaten());
                //Liste aller K-Teilenummern
                List<int> kTeilenummern = new List<int>();
                
                foreach (Teil teil in teileListe)
                {
                    kTeilenummern.Add(teil.getNummer());
                }

                if (!kTeilenummern.Contains(int.Parse(teilenummerCell.FormattedValue.ToString())))
                {
                    teilenummerCell.ErrorText = "Bitte geben Sie eine gültige Kaufteilnummer an!";
                    row.ErrorText = "Bitte geben Sie eine gültige Kaufteilnummer an!";
                }
            }
        }

        //Hilfsmethode für die Validierung der "Menge"-Spalte in der Bestellliste
        private void bestellliste_menge_validieren(DataGridViewCell mengeCell)
        {
            DataGridViewRow row = dataGridView_best_bestellliste.Rows[mengeCell.RowIndex];
            mengeCell.ErrorText = "";
            row.ErrorText = "";
            int intValue;

            if (string.IsNullOrEmpty(mengeCell.FormattedValue.ToString()))
            {
                mengeCell.ErrorText = "Bitte geben Sie die zu bestellende Menge an!";
                row.ErrorText = "Bitte geben Sie die zu bestellende Menge an!";
            }
            else if (!int.TryParse(mengeCell.FormattedValue.ToString(), out intValue) || intValue <= 0)
            {
                mengeCell.ErrorText = "Bitte geben Sie eine positive Ganzzahl für die zu bestellende Menge an!";
                row.ErrorText = "Bitte geben Sie eine positive Ganzzahl für die zu bestellende Menge an!";
            }
        }

        private void refreshKinderFahrradView()
        {
            produktionP1.Clear();
            if (xmlData.getXmlImported())
            {
                pp_p1_p1_lager.Text = xmlData.warehouseStock.article[0].amount.ToString();
                pp_p1_26_lager.Text = ((xmlData.warehouseStock.article[25].amount) / 3).ToString();
                pp_p1_51_lager.Text = xmlData.warehouseStock.article[50].amount.ToString();
                pp_p1_16_lager.Text = ((xmlData.warehouseStock.article[15].amount) / 3).ToString();
                pp_p1_17_lager.Text = ((xmlData.warehouseStock.article[16].amount) / 3).ToString();
                pp_p1_50_lager.Text = xmlData.warehouseStock.article[49].amount.ToString();
                pp_p1_4_lager.Text = xmlData.warehouseStock.article[3].amount.ToString();
                pp_p1_10_lager.Text = xmlData.warehouseStock.article[9].amount.ToString();
                pp_p1_49_lager.Text = xmlData.warehouseStock.article[48].amount.ToString();
                pp_p1_7_lager.Text = xmlData.warehouseStock.article[6].amount.ToString();
                pp_p1_13_lager.Text = xmlData.warehouseStock.article[12].amount.ToString();
                pp_p1_18_lager.Text = xmlData.warehouseStock.article[18].amount.ToString();

                //Warteschleife und InBearbeitung 
                pp_p1_p1_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(1).ToString();
                pp_p1_p1_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(1).ToString();
                pp_p1_26_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(26).ToString();
                pp_p1_26_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(26).ToString();
                pp_p1_51_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(51).ToString();
                pp_p1_51_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(51).ToString();
                pp_p1_16_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(16).ToString();
                pp_p1_16_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(16).ToString();
                pp_p1_17_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(17).ToString();
                pp_p1_17_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(17).ToString();
                pp_p1_50_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(50).ToString();
                pp_p1_50_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(50).ToString();
                pp_p1_4_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(4).ToString();
                pp_p1_4_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(4).ToString();
                pp_p1_10_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(10).ToString();
                pp_p1_10_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(10).ToString();
                pp_p1_49_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(49).ToString();
                pp_p1_49_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(49).ToString();
                pp_p1_7_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(7).ToString();
                pp_p1_7_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(7).ToString();
                pp_p1_13_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(13).ToString();
                pp_p1_13_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(13).ToString();
                pp_p1_18_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(18).ToString();
                pp_p1_18_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(18).ToString();

                //Vertriebswünsche P1 P2 P3
                pp_p1_p1_vw.Text = kinder_prog_p1.Value.ToString();

                //Produktion 
                pp_p1_p1_prod.Text = (int.Parse(pp_p1_p1_vw.Text.ToString()) + int.Parse(pp_p1_p1_sb.Text.ToString()) - int.Parse(pp_p1_p1_lager.Text.ToString()) - int.Parse(pp_p1_p1_ws.Text.ToString()) - int.Parse(pp_p1_p1_bearb.Text.ToString())).ToString();
                produktionP1.Add(1, (int.Parse(pp_p1_p1_prod.Text)));

                pp_p1_26_prod.Text = (int.Parse(pp_p1_p1_prod.Text.ToString()) + int.Parse(pp_p1_26_sb.Text.ToString()) - int.Parse(pp_p1_26_lager.Text.ToString()) - int.Parse(pp_p1_26_ws.Text.ToString()) - int.Parse(pp_p1_26_bearb.Text.ToString())).ToString();
                produktionP1.Add(26, (int.Parse(pp_p1_26_prod.Text)));
                pp_p1_26_vw.Text = pp_p1_p1_prod.Text;

                pp_p1_51_prod.Text = (int.Parse(pp_p1_p1_prod.Text.ToString()) + int.Parse(pp_p1_51_sb.Text.ToString()) - int.Parse(pp_p1_51_lager.Text.ToString()) - int.Parse(pp_p1_51_ws.Text.ToString()) - int.Parse(pp_p1_51_bearb.Text.ToString())).ToString();
                pp_p1_51_vw.Text = pp_p1_p1_prod.Text;
                produktionP1.Add(51, (int.Parse(pp_p1_51_prod.Text)));

                pp_p1_16_prod.Text = (int.Parse(pp_p1_51_prod.Text.ToString()) + int.Parse(pp_p1_16_sb.Text.ToString()) - int.Parse(pp_p1_16_lager.Text.ToString()) - int.Parse(pp_p1_16_ws.Text.ToString()) - int.Parse(pp_p1_16_bearb.Text.ToString())).ToString();
                produktionP1.Add(16, (int.Parse(pp_p1_16_prod.Text)));
                pp_p1_16_vw.Text = pp_p1_51_prod.Text;

                pp_p1_17_prod.Text = (int.Parse(pp_p1_51_prod.Text.ToString()) + int.Parse(pp_p1_17_sb.Text.ToString()) - int.Parse(pp_p1_17_lager.Text.ToString()) - int.Parse(pp_p1_17_ws.Text.ToString()) - int.Parse(pp_p1_17_bearb.Text.ToString())).ToString();
                produktionP1.Add(17, (int.Parse(pp_p1_17_prod.Text)));

                pp_p1_17_vw.Text = pp_p1_51_prod.Text;
                pp_p1_50_prod.Text = (int.Parse(pp_p1_51_prod.Text.ToString()) + int.Parse(pp_p1_50_sb.Text.ToString()) - int.Parse(pp_p1_50_lager.Text.ToString()) - int.Parse(pp_p1_50_ws.Text.ToString()) - int.Parse(pp_p1_50_bearb.Text.ToString())).ToString();
                produktionP1.Add(50, (int.Parse(pp_p1_50_prod.Text)));

                pp_p1_50_vw.Text = pp_p1_51_prod.Text;
                pp_p1_4_prod.Text = (int.Parse(pp_p1_50_prod.Text.ToString()) + int.Parse(pp_p1_4_sb.Text.ToString()) - int.Parse(pp_p1_4_lager.Text.ToString()) - int.Parse(pp_p1_4_ws.Text.ToString()) - int.Parse(pp_p1_4_bearb.Text.ToString())).ToString();
                produktionP1.Add(4, (int.Parse(pp_p1_4_prod.Text)));

                pp_p1_4_vw.Text = pp_p1_50_prod.Text;
                pp_p1_10_prod.Text = (int.Parse(pp_p1_50_prod.Text.ToString()) + int.Parse(pp_p1_10_sb.Text.ToString()) - int.Parse(pp_p1_10_lager.Text.ToString()) - int.Parse(pp_p1_10_ws.Text.ToString()) - int.Parse(pp_p1_10_bearb.Text.ToString())).ToString();
                produktionP1.Add(10, (int.Parse(pp_p1_10_prod.Text)));

                pp_p1_10_vw.Text = pp_p1_50_prod.Text;
                pp_p1_49_prod.Text = (int.Parse(pp_p1_50_prod.Text.ToString()) + int.Parse(pp_p1_49_sb.Text.ToString()) - int.Parse(pp_p1_49_lager.Text.ToString()) - int.Parse(pp_p1_49_ws.Text.ToString()) - int.Parse(pp_p1_49_bearb.Text.ToString())).ToString();
                produktionP1.Add(49, (int.Parse(pp_p1_49_prod.Text)));

                pp_p1_49_vw.Text = pp_p1_50_prod.Text;
                pp_p1_7_prod.Text = (int.Parse(pp_p1_49_prod.Text.ToString()) + int.Parse(pp_p1_7_sb.Text.ToString()) - int.Parse(pp_p1_7_lager.Text.ToString()) - int.Parse(pp_p1_7_ws.Text.ToString()) - int.Parse(pp_p1_7_bearb.Text.ToString())).ToString();
                produktionP1.Add(7, (int.Parse(pp_p1_7_prod.Text)));

                pp_p1_7_vw.Text = pp_p1_49_prod.Text;
                pp_p1_13_prod.Text = (int.Parse(pp_p1_49_prod.Text.ToString()) + int.Parse(pp_p1_13_sb.Text.ToString()) - int.Parse(pp_p1_13_lager.Text.ToString()) - int.Parse(pp_p1_13_ws.Text.ToString()) - int.Parse(pp_p1_13_bearb.Text.ToString())).ToString();
                produktionP1.Add(13, (int.Parse(pp_p1_13_prod.Text)));

                pp_p1_13_vw.Text = pp_p1_49_prod.Text;
                pp_p1_18_prod.Text = (int.Parse(pp_p1_49_prod.Text.ToString()) + int.Parse(pp_p1_18_sb.Text.ToString()) - int.Parse(pp_p1_18_lager.Text.ToString()) - int.Parse(pp_p1_18_ws.Text.ToString()) - int.Parse(pp_p1_18_bearb.Text.ToString())).ToString();
                produktionP1.Add(18, (int.Parse(pp_p1_18_prod.Text)));

                pp_p1_18_vw.Text = pp_p1_49_prod.Text;
            }
        }

        private void refreshDamenfahrradView()
        {
            produktionP2.Clear();

            if (xmlData.getXmlImported())
            {
                pp_p2_p2_lager.Text = xmlData.warehouseStock.article[1].amount.ToString();
                pp_p2_26_lager.Text = ((xmlData.warehouseStock.article[25].amount) / 3).ToString();
                pp_p2_56_lager.Text = xmlData.warehouseStock.article[55].amount.ToString();
                pp_p2_16_lager.Text = ((xmlData.warehouseStock.article[15].amount) / 3).ToString();
                pp_p2_17_lager.Text = ((xmlData.warehouseStock.article[16].amount) / 3).ToString();
                pp_p2_55_lager.Text = xmlData.warehouseStock.article[54].amount.ToString();
                pp_p2_5_lager.Text = xmlData.warehouseStock.article[4].amount.ToString();
                pp_p2_11_lager.Text = xmlData.warehouseStock.article[10].amount.ToString();
                pp_p2_54_lager.Text = xmlData.warehouseStock.article[53].amount.ToString();
                pp_p2_8_lager.Text = xmlData.warehouseStock.article[7].amount.ToString();
                pp_p2_14_lager.Text = xmlData.warehouseStock.article[13].amount.ToString();
                pp_p2_19_lager.Text = xmlData.warehouseStock.article[18].amount.ToString();

                pp_p2_p2_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(2).ToString();
                pp_p2_p2_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(2).ToString();
                pp_p2_26_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(26).ToString();
                pp_p2_26_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(26).ToString();
                pp_p2_56_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(56).ToString();
                pp_p2_56_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(56).ToString();
                pp_p2_16_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(16).ToString();
                pp_p2_16_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(16).ToString();
                pp_p2_17_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(17).ToString();
                pp_p2_17_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(17).ToString();
                pp_p2_55_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(55).ToString();
                pp_p2_55_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(55).ToString();
                pp_p2_5_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(5).ToString();
                pp_p2_5_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(5).ToString();
                pp_p2_11_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(11).ToString();
                pp_p2_11_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(11).ToString();
                pp_p2_54_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(54).ToString();
                pp_p2_54_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(54).ToString();
                pp_p2_8_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(8).ToString();
                pp_p2_8_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(8).ToString();
                pp_p2_14_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(14).ToString();
                pp_p2_14_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(14).ToString();
                pp_p2_19_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(19).ToString();
                pp_p2_19_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(19).ToString();

                //Vertriebswünsche
                pp_p2_p2_vw.Text = damen_prog_p1.Value.ToString();

                pp_p2_p2_prod.Text = (int.Parse(pp_p2_p2_vw.Text.ToString()) + int.Parse(pp_p2_p2_sb.Text.ToString()) - int.Parse(pp_p2_p2_lager.Text.ToString()) - int.Parse(pp_p2_p2_ws.Text.ToString()) - int.Parse(pp_p2_p2_bearb.Text.ToString())).ToString();
                produktionP2.Add(2, (int.Parse(pp_p2_p2_prod.Text)));

                pp_p2_26_prod.Text = (int.Parse(pp_p2_p2_vw.Text.ToString()) + int.Parse(pp_p2_26_sb.Text.ToString()) - int.Parse(pp_p2_26_lager.Text.ToString()) - int.Parse(pp_p2_26_ws.Text.ToString()) - int.Parse(pp_p2_26_bearb.Text.ToString())).ToString();
                produktionP2.Add(26, (int.Parse(pp_p2_26_prod.Text)));


                pp_p2_26_vw.Text = pp_p2_p2_prod.Text;
                pp_p2_56_prod.Text = (int.Parse(pp_p2_p2_vw.Text.ToString()) + int.Parse(pp_p2_56_sb.Text.ToString()) - int.Parse(pp_p2_56_lager.Text.ToString()) - int.Parse(pp_p2_56_ws.Text.ToString()) - int.Parse(pp_p2_56_bearb.Text.ToString())).ToString();
                produktionP2.Add(56, (int.Parse(pp_p2_56_prod.Text)));

                pp_p2_56_vw.Text = pp_p2_p2_prod.Text;
                pp_p2_16_prod.Text = (int.Parse(pp_p2_56_vw.Text.ToString()) + int.Parse(pp_p2_16_sb.Text.ToString()) - int.Parse(pp_p2_16_lager.Text.ToString()) - int.Parse(pp_p2_16_ws.Text.ToString()) - int.Parse(pp_p2_16_bearb.Text.ToString())).ToString();
                produktionP2.Add(16, (int.Parse(pp_p2_16_prod.Text)));

                pp_p2_16_vw.Text = pp_p2_56_prod.Text;
                pp_p2_17_prod.Text = (int.Parse(pp_p2_56_vw.Text.ToString()) + int.Parse(pp_p2_17_sb.Text.ToString()) - int.Parse(pp_p2_17_lager.Text.ToString()) - int.Parse(pp_p2_17_ws.Text.ToString()) - int.Parse(pp_p2_17_bearb.Text.ToString())).ToString();
                produktionP2.Add(17, (int.Parse(pp_p2_17_prod.Text)));

                pp_p2_17_vw.Text = pp_p2_56_prod.Text;
                pp_p2_55_prod.Text = (int.Parse(pp_p2_56_vw.Text.ToString()) + int.Parse(pp_p2_55_sb.Text.ToString()) - int.Parse(pp_p2_55_lager.Text.ToString()) - int.Parse(pp_p2_55_ws.Text.ToString()) - int.Parse(pp_p2_55_bearb.Text.ToString())).ToString();
                produktionP2.Add(55, (int.Parse(pp_p2_55_prod.Text)));

                pp_p2_55_vw.Text = pp_p2_56_prod.Text;
                pp_p2_5_prod.Text = (int.Parse(pp_p2_55_vw.Text.ToString()) + int.Parse(pp_p2_5_sb.Text.ToString()) - int.Parse(pp_p2_5_lager.Text.ToString()) - int.Parse(pp_p2_5_ws.Text.ToString()) - int.Parse(pp_p2_5_bearb.Text.ToString())).ToString();
                produktionP2.Add(5, (int.Parse(pp_p2_5_prod.Text)));

                pp_p2_5_vw.Text = pp_p2_55_prod.Text;
                pp_p2_11_prod.Text = (int.Parse(pp_p2_55_vw.Text.ToString()) + int.Parse(pp_p2_11_sb.Text.ToString()) - int.Parse(pp_p2_11_lager.Text.ToString()) - int.Parse(pp_p2_11_ws.Text.ToString()) - int.Parse(pp_p2_11_bearb.Text.ToString())).ToString();
                produktionP2.Add(11, (int.Parse(pp_p2_11_prod.Text)));

                pp_p2_11_vw.Text = pp_p2_55_prod.Text;
                pp_p2_54_prod.Text = (int.Parse(pp_p2_55_vw.Text.ToString()) + int.Parse(pp_p2_54_sb.Text.ToString()) - int.Parse(pp_p2_54_lager.Text.ToString()) - int.Parse(pp_p2_54_ws.Text.ToString()) - int.Parse(pp_p2_54_bearb.Text.ToString())).ToString();
                produktionP2.Add(54, (int.Parse(pp_p2_54_prod.Text)));

                pp_p2_54_vw.Text = pp_p2_55_prod.Text;
                pp_p2_8_prod.Text = (int.Parse(pp_p2_54_vw.Text.ToString()) + int.Parse(pp_p2_8_sb.Text.ToString()) - int.Parse(pp_p2_8_lager.Text.ToString()) - int.Parse(pp_p2_8_ws.Text.ToString()) - int.Parse(pp_p2_8_bearb.Text.ToString())).ToString();
                produktionP2.Add(8, (int.Parse(pp_p2_8_prod.Text)));

                pp_p2_8_vw.Text = pp_p2_54_prod.Text;
                pp_p2_14_prod.Text = (int.Parse(pp_p2_54_vw.Text.ToString()) + int.Parse(pp_p2_14_sb.Text.ToString()) - int.Parse(pp_p2_14_lager.Text.ToString()) - int.Parse(pp_p2_14_ws.Text.ToString()) - int.Parse(pp_p2_14_bearb.Text.ToString())).ToString();
                produktionP2.Add(14, (int.Parse(pp_p2_14_prod.Text)));

                pp_p2_14_vw.Text = pp_p2_54_prod.Text;
                pp_p2_19_prod.Text = (int.Parse(pp_p2_54_vw.Text.ToString()) + int.Parse(pp_p2_19_sb.Text.ToString()) - int.Parse(pp_p2_19_lager.Text.ToString()) - int.Parse(pp_p2_19_ws.Text.ToString()) - int.Parse(pp_p2_19_bearb.Text.ToString())).ToString();
                produktionP2.Add(19, (int.Parse(pp_p2_19_prod.Text)));

                pp_p2_19_vw.Text = pp_p2_54_prod.Text;
            }
        }

        private void refreshHerrenfahrradView()
        {
            produktionP3.Clear();

            if (xmlData.getXmlImported())
            {
                pp_p3_p3_lager.Text = xmlData.warehouseStock.article[2].amount.ToString();
                pp_p3_26_lager.Text = ((xmlData.warehouseStock.article[25].amount) / 3).ToString();
                pp_p3_31_lager.Text = xmlData.warehouseStock.article[30].amount.ToString();
                pp_p3_16_lager.Text = ((xmlData.warehouseStock.article[15].amount) / 3).ToString();
                pp_p3_17_lager.Text = ((xmlData.warehouseStock.article[16].amount) / 3).ToString();
                pp_p3_30_lager.Text = xmlData.warehouseStock.article[29].amount.ToString();
                pp_p3_6_lager.Text = xmlData.warehouseStock.article[5].amount.ToString();
                pp_p3_12_lager.Text = xmlData.warehouseStock.article[11].amount.ToString();
                pp_p3_29_lager.Text = xmlData.warehouseStock.article[28].amount.ToString();
                pp_p3_9_lager.Text = xmlData.warehouseStock.article[8].amount.ToString();
                pp_p3_15_lager.Text = xmlData.warehouseStock.article[14].amount.ToString();
                pp_p3_20_lager.Text = xmlData.warehouseStock.article[19].amount.ToString();

                pp_p3_p3_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(3).ToString();
                pp_p3_p3_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(3).ToString();
                pp_p3_26_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(26).ToString();
                pp_p3_26_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(26).ToString();
                pp_p3_31_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(31).ToString();
                pp_p3_31_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(31).ToString();
                pp_p3_16_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(16).ToString();
                pp_p3_16_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(16).ToString();
                pp_p3_17_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(17).ToString();
                pp_p3_17_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(17).ToString();
                pp_p3_30_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(30).ToString();
                pp_p3_30_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(30).ToString();
                pp_p3_6_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(6).ToString();
                pp_p3_6_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(6).ToString();
                pp_p3_12_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(12).ToString();
                pp_p3_12_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(12).ToString();
                pp_p3_29_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(29).ToString();
                pp_p3_29_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(29).ToString();
                pp_p3_9_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(9).ToString();
                pp_p3_9_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(9).ToString();
                pp_p3_15_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(15).ToString();
                pp_p3_15_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(15).ToString();
                pp_p3_20_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(20).ToString();
                pp_p3_20_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(20).ToString();

                //Vertriebswünsche
                pp_p3_p3_vw.Text = herren_prog_p1.Value.ToString();

                pp_p3_p3_prod.Text = (int.Parse(pp_p3_p3_vw.Text.ToString()) + int.Parse(pp_p3_p3_sb.Text.ToString()) - int.Parse(pp_p3_p3_lager.Text.ToString()) - int.Parse(pp_p3_p3_ws.Text.ToString()) - int.Parse(pp_p3_p3_bearb.Text.ToString())).ToString();
                produktionP3.Add(3, (int.Parse(pp_p3_p3_prod.Text)));
                pp_p3_26_prod.Text = (int.Parse(pp_p3_p3_vw.Text.ToString()) + int.Parse(pp_p3_26_sb.Text.ToString()) - int.Parse(pp_p3_26_lager.Text.ToString()) - int.Parse(pp_p3_26_ws.Text.ToString()) - int.Parse(pp_p3_26_bearb.Text.ToString())).ToString();
                produktionP3.Add(26, (int.Parse(pp_p3_26_prod.Text)));

                pp_p3_26_vw.Text = pp_p3_p3_prod.Text;
                pp_p3_31_prod.Text = (int.Parse(pp_p3_p3_vw.Text.ToString()) + int.Parse(pp_p3_31_sb.Text.ToString()) - int.Parse(pp_p3_31_lager.Text.ToString()) - int.Parse(pp_p3_31_ws.Text.ToString()) - int.Parse(pp_p3_31_bearb.Text.ToString())).ToString();
                produktionP3.Add(31, (int.Parse(pp_p3_31_prod.Text)));

                pp_p3_31_vw.Text = pp_p3_p3_prod.Text;
                pp_p3_16_prod.Text = (int.Parse(pp_p3_31_vw.Text.ToString()) + int.Parse(pp_p3_16_sb.Text.ToString()) - int.Parse(pp_p3_16_lager.Text.ToString()) - int.Parse(pp_p3_16_ws.Text.ToString()) - int.Parse(pp_p3_16_bearb.Text.ToString())).ToString();
                produktionP3.Add(16, (int.Parse(pp_p3_16_prod.Text)));

                pp_p3_16_vw.Text = pp_p3_31_prod.Text;
                pp_p3_17_prod.Text = (int.Parse(pp_p3_31_vw.Text.ToString()) + int.Parse(pp_p3_17_sb.Text.ToString()) - int.Parse(pp_p3_17_lager.Text.ToString()) - int.Parse(pp_p3_17_ws.Text.ToString()) - int.Parse(pp_p3_17_bearb.Text.ToString())).ToString();
                produktionP3.Add(17, (int.Parse(pp_p3_17_prod.Text)));

                pp_p3_17_vw.Text = pp_p3_31_prod.Text;
                pp_p3_30_prod.Text = (int.Parse(pp_p3_31_vw.Text.ToString()) + int.Parse(pp_p3_30_sb.Text.ToString()) - int.Parse(pp_p3_30_lager.Text.ToString()) - int.Parse(pp_p3_30_ws.Text.ToString()) - int.Parse(pp_p3_30_bearb.Text.ToString())).ToString();
                produktionP3.Add(30, (int.Parse(pp_p3_30_prod.Text)));

                pp_p3_30_vw.Text = pp_p3_31_prod.Text;
                pp_p3_6_prod.Text = (int.Parse(pp_p3_30_vw.Text.ToString()) + int.Parse(pp_p3_6_sb.Text.ToString()) - int.Parse(pp_p3_6_lager.Text.ToString()) - int.Parse(pp_p3_6_ws.Text.ToString()) - int.Parse(pp_p3_6_bearb.Text.ToString())).ToString();
                produktionP3.Add(6, (int.Parse(pp_p3_6_prod.Text)));

                pp_p3_6_vw.Text = pp_p3_30_prod.Text;
                pp_p3_12_prod.Text = (int.Parse(pp_p3_30_vw.Text.ToString()) + int.Parse(pp_p3_12_sb.Text.ToString()) - int.Parse(pp_p3_12_lager.Text.ToString()) - int.Parse(pp_p3_12_ws.Text.ToString()) - int.Parse(pp_p3_12_bearb.Text.ToString())).ToString();
                produktionP3.Add(12, (int.Parse(pp_p3_12_prod.Text)));

                pp_p3_12_vw.Text = pp_p3_30_prod.Text;
                pp_p3_29_prod.Text = (int.Parse(pp_p3_30_vw.Text.ToString()) + int.Parse(pp_p3_29_sb.Text.ToString()) - int.Parse(pp_p3_29_lager.Text.ToString()) - int.Parse(pp_p3_29_ws.Text.ToString()) - int.Parse(pp_p3_29_bearb.Text.ToString())).ToString();
                produktionP3.Add(29, (int.Parse(pp_p3_29_prod.Text)));

                pp_p3_29_vw.Text = pp_p3_30_prod.Text;
                pp_p3_9_prod.Text = (int.Parse(pp_p3_29_vw.Text.ToString()) + int.Parse(pp_p3_9_sb.Text.ToString()) - int.Parse(pp_p3_9_lager.Text.ToString()) - int.Parse(pp_p3_9_ws.Text.ToString()) - int.Parse(pp_p3_9_bearb.Text.ToString())).ToString();
                produktionP3.Add(9, (int.Parse(pp_p3_9_prod.Text)));

                pp_p3_9_vw.Text = pp_p3_29_prod.Text;
                pp_p3_15_prod.Text = (int.Parse(pp_p3_29_vw.Text.ToString()) + int.Parse(pp_p3_15_sb.Text.ToString()) - int.Parse(pp_p3_15_lager.Text.ToString()) - int.Parse(pp_p3_15_ws.Text.ToString()) - int.Parse(pp_p3_15_bearb.Text.ToString())).ToString();
                produktionP3.Add(15, (int.Parse(pp_p3_15_prod.Text)));

                pp_p3_15_vw.Text = pp_p3_29_prod.Text;
                pp_p3_20_prod.Text = (int.Parse(pp_p3_29_vw.Text.ToString()) + int.Parse(pp_p3_20_sb.Text.ToString()) - int.Parse(pp_p3_20_lager.Text.ToString()) - int.Parse(pp_p3_20_ws.Text.ToString()) - int.Parse(pp_p3_20_bearb.Text.ToString())).ToString();
                produktionP3.Add(20, (int.Parse(pp_p3_20_prod.Text)));

                pp_p3_20_vw.Text = pp_p3_29_prod.Text;
            }
        }

        public void fillProdprogrammWithData(object sender, EventArgs e)
        {
            if (xmlData.getXmlImported()) { 
                if (tabControl2.SelectedTab == tabControl2.TabPages["tabKinderf"])
                {
                    refreshKinderFahrradView();
                }

                if (tabControl2.SelectedTab == tabControl2.TabPages["tabDamenf"])
                {
                    refreshDamenfahrradView();
                }

                if (tabControl2.SelectedTab == tabControl2.TabPages["tabHerrenf"])
                {
                    refreshHerrenfahrradView();
                }

                if (tabControl2.SelectedTab == tabControl2.TabPages["tabPUebersicht"])
                {
                    if (xmlData.getXmlImported())
                    {
                        
                        getProduktionsDict();
                        List<Teil> teilListe = new List<Teil>();
                        teilListe = dao.getFertigerzeugnisseStammdaten();

                        pp_uebersicht_grid.Rows.Clear();
                        
                        foreach (Teil teil in teilListe)
                        {
                            string name = teil.getBezeichnung();
                            int nummer = teil.getNummer();
                            int lagerbestand = xmlData.warehouseStock.article[nummer - 1].amount;

                            //TODO: Decimalzahl wird nicht übertragen von XML eingelesen -> immer null
                            string prozent = xmlData.warehouseStock.article[nummer - 1].pctFormatted;

                            //TODO: Warteschlange wird eventuell nicht bei allen gelesen!
                            int warteschlange = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(nummer);
                            int bearbeitung = xmlData.ordersInWork.getInBearbeitungMengeByItem(nummer);

                            int programm = 0;
                            
                            if (teile_Produktion.TryGetValue(teil.getNummer(), out programm)) {
                                 string[] row = { nummer.ToString(), name, lagerbestand.ToString(), prozent.ToString(), warteschlange.ToString(), bearbeitung.ToString(), programm.ToString() };
                                 pp_uebersicht_grid.Rows.Add(row);
                            }                          
}
                    }
                }
            }

        }
        /// <summary>
        /// Produktionsmengen aus einzelnen Produktionsplan übernehme und für Übersicht aufbereiten
        /// </summary>
        /// <returns></returns>
        private void getProduktionsDict()
        {
            teile_Produktion.Clear();

            int nullen = 0;
            int e16 = 0;
            int e17 = 0;
            int e26 = 0;

            //Elemente aus den einzelnen Produktionsplänen-Dictionaries zu Teile_Produktion zufügen 
            if (produktionP1 != null && produktionP1.Count > 0)
            {
                //Wenn e16 > 0 dann zur Variable addieren, sonst vorherigen Wert nehmen
                e16 = (produktionP1[16] > 0) ? e16 + produktionP1[16] : e16;
                e17 = (produktionP1[17] > 0) ? e17 + produktionP1[17] : e17;
                e26 = (produktionP1[26] > 0) ? e26 + produktionP1[26] : e26;

                foreach (int i in produktionP1.Keys)
                {
                    if (!(i.Equals(16) || i.Equals(17) || i.Equals(26)))
                    {
                        if (produktionP1[i] > 0)
                        {
                            teile_Produktion.Add(i, produktionP1[i]);
                        }
                        else
                        {
                            teile_Produktion.Add(i, nullen);
                        }
                        
                    }
                }
            }

            if (produktionP2 != null && produktionP2.Count > 0) 
            {
                e16 = (produktionP2[16] > 0) ? e16 + produktionP2[16] : e16;
                e17 = (produktionP2[17] > 0) ? e17 + produktionP2[17] : e17;
                e26 = (produktionP2[26] > 0) ? e26 + produktionP2[26] : e26;

                foreach (int i in produktionP2.Keys)
                {
                    if (!(i.Equals(16) || i.Equals(17) || i.Equals(26)))
                    {
                        if (produktionP2[i] > 0)
                        {
                            teile_Produktion.Add(i, produktionP2[i]);
                        }
                        else
                        {
                            teile_Produktion.Add(i, nullen);
                        }
                    }
                }
            }

            if (produktionP3 != null && produktionP3.Count > 0)
            {
                e16 = (produktionP3[16] > 0) ? e16 + produktionP3[16] : e16;
                e17 = (produktionP3[17] > 0) ? e17 + produktionP3[17] : e17;
                e26 = (produktionP3[26] > 0) ? e26 + produktionP3[26] : e26;

                foreach (int i in produktionP3.Keys)
                {
                    if (!(i.Equals(16) || i.Equals(17) || i.Equals(26)))
                    {
                        if (produktionP3[i] > 0)
                        {
                            teile_Produktion.Add(i, produktionP3[i]);
                        }
                        else
                        {
                            teile_Produktion.Add(i, nullen);
                        }
                    }
                }
            }

            teile_Produktion.Add(16, e16);
            teile_Produktion.Add(17, e17);
            teile_Produktion.Add(26, e26);

            
            
        }

       
        private void btn_kf_save_Click(object sender, EventArgs e)
        {
            refreshKinderFahrradView();
        }

        private void btn_df_save_Click(object sender, EventArgs e)
        {
            refreshDamenfahrradView();
        }

        private void btn_hf_save_Click(object sender, EventArgs e)
        {
            refreshHerrenfahrradView();
        }
        //Delegate Keypress Events, nur numerische Zeichen in TextBox erlaubt
        private void onlynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;


        }
       
        /// <summary>
        /// Prognosen für Periode 2 - 4 aus in Produktionsprogramm übernehmen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_prog_save_Click(object sender, EventArgs e)
        {
            produktionsProgramm.P1_2 = Convert.ToInt32(kinder_prog_p2.Value);
            produktionsProgramm.P1_3 = Convert.ToInt32(kinder_prog_p3.Value);
            produktionsProgramm.P1_4 = Convert.ToInt32(kinder_prog_p4.Value);

            produktionsProgramm.P2_2 = Convert.ToInt32(damen_prog_p2.Value);
            produktionsProgramm.P2_3 = Convert.ToInt32(damen_prog_p3.Value);
            produktionsProgramm.P2_4 = Convert.ToInt32(damen_prog_p4.Value);

            produktionsProgramm.P3_2 = Convert.ToInt32(herren_prog_p2.Value);
            produktionsProgramm.P3_3 = Convert.ToInt32(herren_prog_p3.Value);
            produktionsProgramm.P3_4 = Convert.ToInt32(herren_prog_p4.Value);



        }
    }
}