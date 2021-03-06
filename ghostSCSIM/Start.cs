﻿using System;
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
        //Aktuelle Sprache
        private String culInfo = Thread.CurrentThread.CurrentUICulture.Name;

        //Konstanten für Lokalisierung
        private const String de = "de";
        private const String deBruttobedarf = "Bruttobedarf";
        private const String enBruttobedarf = "Gross Requirements";
        private const String dePeriode = "Periode";
        private const String enPeriode = "Period";
        private const String deBestand = "Bestand";
        private const String enBestand = "Stock";
        private const String deSaveXML = "XML Speichern";
        private const String enSaveXML = "Save XML";


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
                          
        //Direktverkauf
        private List<Direktverkauf> direktVerkauf;          
        //Reihenfolgenplanung
        private LinkedList<Reihenfolgenplanung> listRfpglobal = new LinkedList<Reihenfolgenplanung>();

        //Wird 1 wenn Produktionsplan Übersicht geklickt wurde
        private int uebersicht_geklickt = 0;
        private int uebersicht_kapa = 0;

        //Wird 1 wenn Bestellung Tab geklickt wurde
        private int bestellung_geklickt = 0;

        private int bestellListeErzeugt = 0;

        //index für die neuen Reihenfolgen
        private int reihenIndex = 1000;

        //Bestellliste
        private List<Bestellung> bestellungsListe;
                
        //Arbeitsplätze
        private List<Arbeitsplatz> arbeitsplatzListe = new List<Arbeitsplatz>();
                 
        //Direktverkäufe
        private List<Direktverkauf> direktverkaufListe = new List<Direktverkauf>();

       //Kapazität
        private List<ArbeitsplatzKapa> kapazitaetsListe = new List<ArbeitsplatzKapa>();

        //Validierung
        private CellValidation validateCell = new CellValidation();
                
                 
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
                    culInfo = Thread.CurrentThread.CurrentUICulture.Name;
                    InitializeComponent();
                    break;
                case "en":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    Controls.Clear();
                    //Events.Dispose();
                    culInfo = Thread.CurrentThread.CurrentUICulture.Name;
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

                //MessageBox.Show("XML_File erfolgreich importiert!");
                if ((Thread.CurrentThread.CurrentUICulture).ToString() == "en")
                {
                    MessageBox.Show("XML-File successfully imported!");
            }
                else
                {
                    MessageBox.Show("XML-Datei erfolgreich importiert!");
        }

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

        //Delete Button Bestellliste
        private void dataGridView_best_bestellliste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && dataGridView_best_bestellliste.CurrentCell.Value != null)
            {
                dataGridView_best_bestellliste.Rows.RemoveAt(dataGridView_best_bestellliste.CurrentRow.Index);
            }
        }

        //Delete Button Direktverkauf
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


        #region Validierung

        //Nur numerische Zeichen als Eingabe beim Kapazitätsplan DataGridView akzeptieren
        private void dataGridView_rf_planung_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(onlynum_KeyPress);
        }

        //Nur numerische Zeichen als Eingabe beim Kapazitätsplan DataGridView akzeptieren
        private void dataGridView_kp_uebersicht_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(onlynum_KeyPress);
        }

        /// <summary>
        /// Validierungsanzeige für Kapazitätsplan
        /// </summary>
        /// <returns></returns>
        private void dataGridView_kp_uebersicht_ValidateRow(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Keine Validierung falls es sich um eine neue Zeile handelt
            if (dataGridView_kp_uebersicht.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewCell ueberstundenCell = dataGridView_kp_uebersicht.Rows[e.RowIndex].Cells[6];
            bool dreiSchichten = Convert.ToBoolean(dataGridView_kp_uebersicht.Rows[e.RowIndex].Cells[8].Value);
            kapazitaetsplan_ueberstunden_isValid(ueberstundenCell, dreiSchichten);

            e.Cancel = !kapazitaetsplan_ueberstunden_isValid(ueberstundenCell, dreiSchichten); //TODO: User am Verlassen der Zeile hindern?
        }

        //Hilfsmethode für die Validierung der "Überstunden"-Spalte im Kapazitätsplan
        private bool kapazitaetsplan_ueberstunden_isValid(DataGridViewCell ueberstundenCell, bool dreiSchichten)
        {
            ueberstundenCell.ErrorText = "";

            //Keine Eingabe oder keine positive Zahl
            if (!validateCell.kapaPlanUeberstundenIsValid(ueberstundenCell))
            {
                ueberstundenCell.ErrorText = "Bitte geben Sie eine gültige Überstundenanzahl an!";

                return false;
            }
            //Maximale Überstundenanzahl überschritten
            else if (!validateCell.kapaPlanUeberstundenMaxIsValid(ueberstundenCell))
            {
                ueberstundenCell.ErrorText = "Die maximale Überstundenanzahl beträgt 240 Minuten pro Tag!";

                return false;
            }
            //Überstunden trotz 3 Schichten angesetzt
            else if (!validateCell.kapaPlanUeberstundenSchichtIsValid(ueberstundenCell, dreiSchichten))
            {
                ueberstundenCell.ErrorText = "Bei einer Anzahl von 3 Schichten sind keine Überstunden möglich!";

                return false;
            }
            else
            {
                return true;
            }
        }


        //Nur numerische Zeichen als Eingabe bei der Bestellliste DataGridView akzeptieren
        private void dataGridView_best_bestellliste_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(onlynum_KeyPress);
        }

        /// <summary>
        /// Eingaben beim Datagridview für die Bestellliste validieren und gegebenenfalls die Zeile sperren
        /// </summary>
        /// <returns></returns>
        private void dataGridView_best_bestellliste_ValidateRow(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Keine Validierung falls es sich um eine neue Zeile handelt
            if (dataGridView_best_bestellliste.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewCell nummerCell = dataGridView_best_bestellliste.Rows[e.RowIndex].Cells[0];
            DataGridViewCell mengeCell = dataGridView_best_bestellliste.Rows[e.RowIndex].Cells[1];

            bestellliste_menge_isValid(mengeCell);
            bestellliste_nummer_isValid(nummerCell);
            e.Cancel = !bestellliste_menge_isValid(mengeCell) || !bestellliste_nummer_isValid(nummerCell);
        }

        //Hilfsmethode für die Validierung der "Nummer"-Spalte in der Bestellliste
        private bool bestellliste_nummer_isValid(DataGridViewCell teilenummerCell)
        {
            teilenummerCell.ErrorText = "";

            if (!validateCell.bestellungNummerIsValid(teilenummerCell))
            {
                teilenummerCell.ErrorText = "Bitte geben Sie eine gültige Kaufteilnummer an!";

                return false;
            }
            else
            {
                return true;
            }
        }

        //Hilfsmethode für die Validierung der "Menge"-Spalte in der Bestellliste
        private bool bestellliste_menge_isValid(DataGridViewCell mengeCell)
        {
            mengeCell.ErrorText = "";

            if (!validateCell.bestellungMengeIsValid(mengeCell))
            {
                mengeCell.ErrorText = "Bitte geben Sie die zu bestellende Menge an!";

                return false;
            }
            else
            {
                return true;
            }
        }


        //Nur numerische Zeichen oder numerische Zeichen und einen Punkt als Eingabe bei der Bestellliste DataGridView akzeptieren
        private void dataGridView_dirver_direktverkauf_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(direktverkauf_KeyPress);
        }

        /// <summary>
        /// Eingaben beim Datagridview für den Direktverkauf validieren und gegebenenfalls die Zeile sperren
        /// </summary>
        /// <returns></returns>
        private void dataGridView_dirver_direktverkauf_ValidateRow(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Keine Validierung falls es sich um eine neue Zeile handelt
            if (dataGridView_dirver_direktverkauf.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewCell nummerCell = dataGridView_dirver_direktverkauf.Rows[e.RowIndex].Cells[0];
            DataGridViewCell mengeCell = dataGridView_dirver_direktverkauf.Rows[e.RowIndex].Cells[1];
            DataGridViewCell preisCell = dataGridView_dirver_direktverkauf.Rows[e.RowIndex].Cells[2];
            DataGridViewCell strafeCell = dataGridView_dirver_direktverkauf.Rows[e.RowIndex].Cells[3];

            direktverkauf_strafe_isValid(strafeCell);
            direktverkauf_preis_isValid(preisCell);
            direktverkauf_menge_isValid(mengeCell);
            direktverkauf_nummer_isValid(nummerCell);
            e.Cancel = !direktverkauf_preis_isValid(preisCell) || !direktverkauf_menge_isValid(mengeCell) || !direktverkauf_nummer_isValid(nummerCell);
        }

        //Hilfsmethode für die Validierung der "Nummer"-Spalte im Direktverkauf
        private bool direktverkauf_nummer_isValid(DataGridViewCell teilenummerCell)
        {
            teilenummerCell.ErrorText = "";

            if (!validateCell.direktverkaufNummerIsValid(teilenummerCell))
            {
                teilenummerCell.ErrorText = "Bitte geben Sie eine gültige Artikelnummer an!";

                return false;
            }
            else
            {
                return true;
            }
        }

        //Hilfsmethode für die Validierung der "Menge"-Spalte im Direktverkauf
        private bool direktverkauf_menge_isValid(DataGridViewCell mengeCell)
        {
            mengeCell.ErrorText = "";

            if (!validateCell.direktverkaufMengeIsValid(mengeCell))
            {
                mengeCell.ErrorText = "Bitte geben Sie die zu verkaufende Menge an!";

                return false;
            }
            else
            {
                return true;
            }
        }

        //Hilfsmethode für die Validierung der "Preis"-Spalte im Direktverkauf
        private bool direktverkauf_preis_isValid(DataGridViewCell preisCell)
        {
            preisCell.ErrorText = "";

            if (!validateCell.direktverkaufPreisIsValid(preisCell))
            {
                preisCell.ErrorText = "Bitte geben Sie einen Verkaufspreis an!";

                return false;
            }
            else
            {
                return true;
            }
        }

        //Hilfsmethode für die Validierung der "Strafe"-Spalte im Direktverkauf
        private bool direktverkauf_strafe_isValid(DataGridViewCell strafeCell)
        {
            strafeCell.ErrorText = "";

            //Keine Eingabe
            if (!validateCell.direktverkaufStrafeIsValid(strafeCell))
            {
                strafeCell.ErrorText = "Bitte geben Sie die Höhe der Strafzahlung an!";

                return false;
            }
            else
            {
                return true;
            }
        }

        //TODO: Validierung Fertigung

        #endregion


        #region Daten von Kapa und DVerkauf in Listen speichern

        //Daten des Kapazitätsplan DataGridView speichern, sobald die Kapazitätsplan TabControl verlassen wird
        private void tabControl_kapa_Leave(object sender, EventArgs e)
        {
            TabControl senderTabControl = (TabControl)sender;

            if (senderTabControl == tabControl_kp)
            {
                arbeitsplatzListe.Clear();

                foreach (DataGridViewRow row in dataGridView_kp_uebersicht.Rows)
                {
                    saveAsArbeitsplatz(row);
                }
            }
        }

        private void saveAsArbeitsplatz(DataGridViewRow row)
        {
            int apNummer = Convert.ToInt32(row.Cells[0].Value);
            int ueberstunden = Convert.ToInt32(row.Cells[6].Value); ;
            int schichten = 1;
            if (Convert.ToBoolean(row.Cells[8].Value))
                schichten = 3;
            else if (Convert.ToBoolean(row.Cells[7].Value))
                schichten = 2;
            
            arbeitsplatzListe.Add(new Arbeitsplatz(apNummer, ueberstunden, schichten));
        }

        //Daten des Direktverkauf DataGridView speichern, sobald die Direktverkauf TabControl verlassen wird
        private void tabControl_dirver_Leave(object sender, EventArgs e)
        {
            TabControl senderTabControl = (TabControl)sender;

            if (senderTabControl == tabControl_dirver)
            {
                direktverkaufListe.Clear();

                foreach (DataGridViewRow row in dataGridView_dirver_direktverkauf.Rows)
                {
                    saveAsDirektverkauf(row);
                }
            }
        }

        private void saveAsDirektverkauf(DataGridViewRow row)
        {
            if (!row.IsNewRow)
            {
                Teil teil = getTeilByTeilenummer(Convert.ToInt32(row.Cells[0].Value));
                int menge = Convert.ToInt32(row.Cells[1].Value);
                double preis = Math.Round(Convert.ToDouble(row.Cells[2].Value), 2);
                double strafe = Math.Round(Convert.ToDouble(row.Cells[3].Value), 2);

                direktverkaufListe.Add(new Direktverkauf(teil, menge, preis, strafe));
            }
        }


        private Teil getTeilByTeilenummer(int teilenummer)
        {
            List<Teil> teileStammdaten = dao.getTeilStammdaten();
            foreach (Teil t in teileStammdaten)
            {
                if (t.getNummer() == teilenummer)
                    return t;
            }
            return null;
        }

        #endregion

       //Button für das Generieren der Input XML-Datei
        private void createXml_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog { };
            fileDialog.Title = (culInfo == de) ? deSaveXML : enSaveXML;
            fileDialog.Filter = "XML-Dateien (*.xml)|*.xml";

            int game = xmlData.game;
            int group = xmlData.group;
            int period = xmlData.period+1;

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
            xmlResult.setVertriebswuensche(Convert.ToInt32(kinder_prog_p1.Text), Convert.ToInt32(damen_prog_p1.Text), Convert.ToInt32(herren_prog_p1.Text));
            if (listRfpglobal != null)
            {
            xmlResult.setFertigung(listRfpglobal);
            }
            if (arbeitsplatzListe != null)
            {
            xmlResult.setArbeitsplaetze(arbeitsplatzListe);
            }
            if (bestellungsListe != null)
            {
            xmlResult.setBestellung(bestellungsListe);
            }
            if (direktverkaufListe != null)
            {
            xmlResult.setDirektverkauf(direktverkaufListe);
        }

            
            
        }

        /// <summary>
        /// Methode um den selected Tab mit Daten zu füllen
        /// </summary>
        /// private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        private void fillTabsWithData(object sender, EventArgs e)
        {
            #region tabPrognose
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPrognose"])
            {
               
            }
            #endregion
            #region tabProdplan
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabProdplan"])
            {
                if (xmlData.getXmlImported())
                {
                    //Erste TabPage laden
                    tabControl2.SelectedTab = tabControl2.TabPages["tabKinderf"];
                    refreshKinderFahrradView();
                                       
                }
            }
            #endregion
            #region tabKapa
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabKapa"])
            {
                
                if (xmlData.getXmlImported())
                {
                    if (uebersicht_kapa == 1)
                    {
                    dataGridView_kp_uebersicht.Rows.Clear();
                        kapazitaetsListe.Clear();
                    if (dataGridView_kp_uebersicht.Rows.Count == 0)
                    {
                            List<ArbeitsplatzKapa> liste = dao.getArbeitsplaetzeKapa();
                            int x = 0;
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

                                    x = ak.getTeilfk();
                                int ruestz = ak.getRuestzeit();
                                int fertigz = ak.getFertigungszeit();

                                int kapazitätsbedarfTeil = 0;
                                //Nur wenn der Key gefunden wurde berechnen, sonst KeyNotFoundException
                                    if (teile_Produktion.TryGetValue(x, out kapazitätsbedarfTeil))
                                    {
                                        kapazitätsbedarfTeil = fertigz * teile_Produktion[x];
                                }

                               
                                ruestzeit += ruestz;
                                kapazitätsbedarf += kapazitätsbedarfTeil;

                            }

                            int gesamt = kapazitätsbedarf + ruestzeit + rueckstaendekapa + rueckstaendez;


                            if (gesamt > 2400 && gesamt <= 3600)
                            { 
                                int differenz = gesamt - 2400;
                                int ueberstunden = differenz / 5;
                                    ArbeitsplatzKapa a = new ArbeitsplatzKapa(i, x, int.Parse(ruestzeit.ToString()), int.Parse(kapazitätsbedarf.ToString()), int.Parse(rueckstaendekapa.ToString()), int.Parse(rueckstaendez.ToString()), int.Parse(gesamt.ToString()), int.Parse(ueberstunden.ToString()), false, false);
                                    kapazitaetsListe.Add(a);
                            }

                            else if (gesamt > 3600 && gesamt <= 7200)
                            {
                                    ArbeitsplatzKapa a = new ArbeitsplatzKapa(i, x, int.Parse(ruestzeit.ToString()), int.Parse(kapazitätsbedarf.ToString()), int.Parse(rueckstaendekapa.ToString()), int.Parse(rueckstaendez.ToString()), int.Parse(gesamt.ToString()), 0, true, false);
                                    kapazitaetsListe.Add(a);
                            }
                                else if (gesamt > 7200 && gesamt <= 8400)
                            {
                                    int differenz = gesamt - 7200;
                                    int ueberstunden = differenz / 5;
                                    ArbeitsplatzKapa a = new ArbeitsplatzKapa(i, x, int.Parse(ruestzeit.ToString()), int.Parse(kapazitätsbedarf.ToString()), int.Parse(rueckstaendekapa.ToString()), int.Parse(rueckstaendez.ToString()), int.Parse(gesamt.ToString()), int.Parse(ueberstunden.ToString()), true, false);
                                    kapazitaetsListe.Add(a);
                                }
                                else if (gesamt > 8400)
                                {
                                    ArbeitsplatzKapa a = new ArbeitsplatzKapa(i, x, int.Parse(ruestzeit.ToString()), int.Parse(kapazitätsbedarf.ToString()), int.Parse(rueckstaendekapa.ToString()), int.Parse(rueckstaendez.ToString()), int.Parse(gesamt.ToString()), 0, true, true);
                                    kapazitaetsListe.Add(a);
                            }
                            else
                                {
                                    ArbeitsplatzKapa a = new ArbeitsplatzKapa(i, x, int.Parse(ruestzeit.ToString()), int.Parse(kapazitätsbedarf.ToString()), int.Parse(rueckstaendekapa.ToString()), int.Parse(rueckstaendez.ToString()), int.Parse(gesamt.ToString()), 0, false, false);
                                    kapazitaetsListe.Add(a);
                                }

                            }
                            for (int index = 0; index < kapazitaetsListe.Count; index++)
                            {
                                ArbeitsplatzKapa ak = kapazitaetsListe[index];
                                dataGridView_kp_uebersicht.Rows.Add(ak.getArbeitsplatz(), ak.getFertigungszeit(), ak.getRuestzeit(), ak.getRueckstaendekapa(), ak.getRueckstaendez(), ak.getGesamt(), ak.getUeberstunden(), ak.getSchicht2(), ak.getSchicht3());
                            }
                            uebersicht_kapa = 0;
                        }
                    }
                }
                    
            }
            #endregion
            #region tabBestellung
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

                    
                }
                
            }
            #endregion
            #region tabReihenfolge
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabReihenfolge"])
            {
                dataGridView_rf_planung.Rows.Clear();
                listRfpglobal.Clear();
                if (xmlData.getXmlImported())
                {
                    if(uebersicht_geklickt == 1)
                    {
                        refreshReihenfolgenplanung();    
                }
                
            }

            }
            #endregion
            #region tabDirektV
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabDirektV"])
            {
                if (dataGridView_dirver_direktverkauf.Rows.Count == 0)
                {
                    dataGridView_dirver_direktverkauf.Rows.Add(1, 0, 0.00, 0.00);
                    dataGridView_dirver_direktverkauf.Rows.Add(2, 0, 0.00, 0.00);
                    dataGridView_dirver_direktverkauf.Rows.Add(3, 0, 0.00, 0.00);
                }
            }
            #endregion
            #region tabUebersicht
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabUebersicht"])
            {
                
                fillFertigungDataGridView();
                fillKapaDataGridView();
                fillBestDataGridView();
                fillDirverDataGridView();
            }
            #endregion

        }


        #region Übersicht

        private void fillFertigungDataGridView()
        {
            dataGridView_uebersicht_fertigung.Rows.Clear();
            foreach (Reihenfolgenplanung r in listRfpglobal)
            {
                dataGridView_uebersicht_fertigung.Rows.Add(r.getTeil(), r.getMenge());
            }            
        }

        private void fillKapaDataGridView()
        {
            dataGridView_uebersicht_kapa.Rows.Clear();
            foreach (Arbeitsplatz ap in arbeitsplatzListe)
            {
                int lastRowAdded = dataGridView_uebersicht_kapa.Rows.Add(ap.getApNummer(), ap.getUeberstunden(), ap.getSchichten());
                dataGridView_uebersicht_kapa_ValidateRow(dataGridView_uebersicht_kapa.Rows[lastRowAdded]);
            }
        }

        private void fillBestDataGridView()
        {
            dataGridView_uebersicht_best.Rows.Clear();
            if (bestellungsListe != null)
            {
            foreach (Bestellung bestellung in bestellungsListe)
            {
                bool bestelltyp = false;
                if (bestellung.getBestellTyp().Equals(Bestelltyp.F))
                    bestelltyp = true;

                int lastRowAdded = dataGridView_uebersicht_best.Rows.Add(bestellung.getTeil().getNummer(), bestellung.getMenge(), bestelltyp);
                dataGridView_uebersicht_best_ValidateRow(dataGridView_uebersicht_best.Rows[lastRowAdded]);
            }
        }
        }

        private void fillDirverDataGridView()
        {
            dataGridView_uebersicht_dirver.Rows.Clear();
            foreach (Direktverkauf dv in direktverkaufListe)
            {
                int lastRowAdded = dataGridView_uebersicht_dirver.Rows.Add(dv.getTeil().getNummer(), dv.getMenge(), dv.getPreis(), dv.getStrafe());
                dataGridView_uebersicht_dirver_ValidateRow(dataGridView_uebersicht_dirver.Rows[lastRowAdded]);
            }
        }

        //TODO: Validate Reihenfolge

        /// <summary>
        /// Validierungsanzeige für Zeilen des DataGridView Kapazitätsplan im Tab Übersicht
        /// </summary>
        /// <returns></returns>
        private void dataGridView_uebersicht_kapa_ValidateRow(DataGridViewRow row)
        {
            bool dreiSchichten = Convert.ToInt32(row.Cells[2].Value) == 3;
            
            kapazitaetsplan_ueberstunden_isValid(row.Cells[1], dreiSchichten);
        }

        /// <summary>
        /// Validierungsanzeige für Zeilen des DataGridView Bestellung im Tab Übersicht
        /// </summary>
        /// <returns></returns>
        private void dataGridView_uebersicht_best_ValidateRow(DataGridViewRow row)
            {
            bestellliste_menge_isValid(row.Cells[1]);
            bestellliste_nummer_isValid(row.Cells[0]);
            }

        /// <summary>
        /// Validierungsanzeige für Zeilen des DataGridView Direktverkauf im Tab Übersicht
        /// </summary>
        /// <returns></returns>
        private void dataGridView_uebersicht_dirver_ValidateRow(DataGridViewRow row)
        {
            direktverkauf_strafe_isValid(row.Cells[3]);
            direktverkauf_preis_isValid(row.Cells[2]);
            direktverkauf_menge_isValid(row.Cells[1]);
            direktverkauf_nummer_isValid(row.Cells[0]);
        }

        #endregion


        private void refreshReihenfolgenplanung()
        {
            if (listRfpglobal.Count == 0)
            {
                string highestVW = getHighestVW();
                listRfpglobal = getReihenfolgeByHighestVW(highestVW);
                LinkedList<Reihenfolgenplanung> listKDH = getRfByKDH();
                foreach (Reihenfolgenplanung r in listKDH)
                {
                    listRfpglobal.AddFirst(r);
                }
            }
            if (bestellung_geklickt == 1)
            {
                List<int> kritisch = getKritischeTeile(dispoErgebnis);
                if (kritisch.Count() > 0)
                {
                    sortListCritical(kritisch);
                }
            }
            foreach (Reihenfolgenplanung r in listRfpglobal)
            {
                dataGridView_rf_planung.Rows.Add(r.getTeil(), r.getMenge().ToString(), r.getSplittmenge().ToString(), "▲", "▼", r.getIndex());
            }

        }

        private List<int> getKritischeTeile(List<DispositionErgebnis> dispositionsErgebnis)
        {
            List<int> liste = new List<int>();

            foreach (DispositionErgebnis de in dispositionsErgebnis)
            {
                if (de.getCriticalFlag() == 1)
                {
                    List<StuecklisteItem> teileVerwendungsnachweis = de.getTeileVerwendungsnachweis();
                    foreach (StuecklisteItem item in teileVerwendungsnachweis)
                    {
                        Teil verwendetesTeil = item.getParent();                        
                        liste.Add(verwendetesTeil.getNummer());
                        int x = 0;
                        foreach (int i in liste)
                        {
                            if(i == verwendetesTeil.getNummer())
                            {
                                x++;
                            }
                        }
                        if(x>1)
                        {
                            liste.Remove(verwendetesTeil.getNummer());
                        }
                        
                    }
                    
                }
            }
            return liste;
        }

        private void sortListCritical(List<int> li)
        {
            foreach(int i in li)
            {
                
                    LinkedListNode<Reihenfolgenplanung> gefunden = null;
                    //TODO: Alle Teile suchen! bisher immer noch ein wenn mehrmals auftritt
                        for (LinkedListNode<Reihenfolgenplanung> node = listRfpglobal.First; node != listRfpglobal.Last.Next; node = node.Next)
                        {
                            if (int.Parse(node.Value.getTeil()) == i)
                            {
                                gefunden = node;
                            }
                        }
                        if (listRfpglobal.Contains(gefunden.Value))
                        {
                            listRfpglobal.Remove(gefunden);
                            listRfpglobal.AddLast(gefunden);
                        }
                    
            }

        }



        private string getHighestVW()
        {
            int p1 = Convert.ToInt32(kinder_prog_p1.Value);
            int p2 = Convert.ToInt32(damen_prog_p1.Value);
            int p3 = Convert.ToInt32(herren_prog_p1.Value);

            if(p1 > p2 && p1 > p3)
            {
                return "p1";
            }
            if (p2 > p1 && p2 > p3)
            {
                return "p2";
            }
            if (p3 > p1 && p3 > p2)
            {
                return "p3";
            }
            return "p1";
        }

        

        private LinkedList<Reihenfolgenplanung> getReihenfolgeByHighestVW(string vw)
        {
            LinkedList<Reihenfolgenplanung> rf = new LinkedList<Reihenfolgenplanung>();
            
            if(vw == "p1")
            {
                LinkedList<Reihenfolgenplanung> r1 = getRfByEndprodukt(vw);
                foreach(Reihenfolgenplanung r in r1)
                {
                    rf.AddLast(r);
                }
                LinkedList<Reihenfolgenplanung> r2 = getRfByEndprodukt("p2");
                foreach (Reihenfolgenplanung r in r2)
                {
                    rf.AddLast(r);
                }
                LinkedList<Reihenfolgenplanung> r3 = getRfByEndprodukt("p3");
                foreach (Reihenfolgenplanung r in r3)
                {
                    rf.AddLast(r);
                }
                return rf;
            }

            if(vw == "p2")
            {
                LinkedList<Reihenfolgenplanung> r2 = getRfByEndprodukt(vw);
                foreach (Reihenfolgenplanung r in r2)
                {
                    rf.AddLast(r);
                }
                LinkedList<Reihenfolgenplanung> r1 = getRfByEndprodukt("p1");
                foreach (Reihenfolgenplanung r in r1)
                {
                    rf.AddLast(r);
                }
                LinkedList<Reihenfolgenplanung> r3 = getRfByEndprodukt("p3");
                foreach (Reihenfolgenplanung r in r3)
                {
                    rf.AddLast(r);
                }
                return rf;
            }

            if (vw == "p3")
            {
                LinkedList<Reihenfolgenplanung> r3 = getRfByEndprodukt(vw);
                foreach (Reihenfolgenplanung r in r3)
                {
                    rf.AddLast(r);
                }
                LinkedList<Reihenfolgenplanung> r1 = getRfByEndprodukt("p1");
                foreach (Reihenfolgenplanung r in r1)
                {
                    rf.AddLast(r);
                }
                LinkedList<Reihenfolgenplanung> r2 = getRfByEndprodukt("p2");
                foreach (Reihenfolgenplanung r in r2)
                {
                    rf.AddLast(r);
                }
                return rf;
            }

            return rf;
        }

        private LinkedList<Reihenfolgenplanung> getRfByKDH()
        {
            LinkedList<Reihenfolgenplanung> rKDH = new LinkedList<Reihenfolgenplanung>();
            if (produktionP1[16] >= 0 && produktionP2[16] >= 0 && produktionP3[16] >= 0)
            {
                int e16 = produktionP1[16] + produktionP2[16] + produktionP3[16];
                Reihenfolgenplanung r16 = new Reihenfolgenplanung(1, "16", e16, 0);
                rKDH.AddLast(r16);
            }
            if (produktionP1[17] >= 0 && produktionP2[17] >= 0 && produktionP3[17] >= 0)
            {
                int e17 = produktionP1[17] + produktionP2[17] + produktionP3[17];
                Reihenfolgenplanung r17 = new Reihenfolgenplanung(2, "17", e17, 0);
                rKDH.AddLast(r17);
            }
            if (produktionP1[26] >= 0 && produktionP2[26] >= 0 && produktionP3[26] >= 0)
            {
                int e26 = produktionP1[26] + produktionP2[26] + produktionP3[26];
                Reihenfolgenplanung r26 = new Reihenfolgenplanung(3, "26", e26, 0);
                rKDH.AddLast(r26);
            }

            return rKDH;
        }

        private LinkedList<Reihenfolgenplanung> getRfByEndprodukt(string p)
        {
            LinkedList<Reihenfolgenplanung> rf = new LinkedList<Reihenfolgenplanung>();            
            if (p == "p1")
            {
                if (produktionP1[1] > 0)
                {
                    Reihenfolgenplanung r1 = new Reihenfolgenplanung(4, "1", produktionP1[1], 0);
                    rf.AddLast(r1);
                }
                    if(produktionP1[51] > 0)
                {
                    Reihenfolgenplanung r51 = new Reihenfolgenplanung(5, "51", produktionP1[51], 0);
                    rf.AddLast(r51);
                }
                if (produktionP1[50] > 0)
                {
                    Reihenfolgenplanung r50 = new Reihenfolgenplanung(6, "50", produktionP1[50], 0);
                    rf.AddLast(r50);
                }       
                if (produktionP1[49] > 0)
                {
                    Reihenfolgenplanung r49 = new Reihenfolgenplanung(7, "49", produktionP1[49], 0);
                    rf.AddLast(r49);
                }
                if (produktionP1[4] > 0)
                {                    
                    double i = Math.Ceiling(Convert.ToDouble(produktionP1[4])/2);
                    Reihenfolgenplanung r4_1 = new Reihenfolgenplanung(8, "4", Convert.ToInt32(i), 0);
                    rf.AddLast(r4_1);
                }
                if (produktionP1[7] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP1[7]) / 2);
                    Reihenfolgenplanung r7_1 = new Reihenfolgenplanung(9, "7", Convert.ToInt32(i), 0);
                    rf.AddLast(r7_1);
                }
                if (produktionP1[4] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP1[4]) / 2);
                    Reihenfolgenplanung r4_2 = new Reihenfolgenplanung(10, "4", Convert.ToInt32(i), 0);
                    rf.AddLast(r4_2);
                }
                if (produktionP1[7] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP1[7]) / 2);
                    Reihenfolgenplanung r7_2 = new Reihenfolgenplanung(11, "7", Convert.ToInt32(i), 0);
                    rf.AddLast(r7_2);
                }
                if (produktionP1[10] > 0)
                {
                    Reihenfolgenplanung r10 = new Reihenfolgenplanung(12, "10", produktionP1[10], 0);
                    rf.AddLast(r10);
                }
                if (produktionP1[13] > 0)
                {
                    Reihenfolgenplanung r13 = new Reihenfolgenplanung(13, "13", produktionP1[13], 0);
                    rf.AddLast(r13);
                }
                if (produktionP1[18] > 0)
                {
                    Reihenfolgenplanung r18 = new Reihenfolgenplanung(14, "18", produktionP1[18], 0);
                    rf.AddLast(r18);
                }
                return rf;
            }

            if (p == "p2")
            {
                if (produktionP2[2] > 0)
                {
                    Reihenfolgenplanung r2 = new Reihenfolgenplanung(15, "2", produktionP2[2], 0);
                    rf.AddLast(r2);
                }
                if (produktionP2[56] > 0)
                {
                    Reihenfolgenplanung r56 = new Reihenfolgenplanung(16, "56", produktionP2[56], 0);
                    rf.AddLast(r56);
                }
                if (produktionP2[55] > 0)
                {
                    Reihenfolgenplanung r55 = new Reihenfolgenplanung(17, "55", produktionP2[55], 0);
                    rf.AddLast(r55);
                }
                if (produktionP2[54] > 0)
                {
                    Reihenfolgenplanung r54 = new Reihenfolgenplanung(18, "54", produktionP2[54], 0);
                    rf.AddLast(r54);
                }
                if (produktionP2[5] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP2[5]) / 2);
                    Reihenfolgenplanung r5_1 = new Reihenfolgenplanung(19, "5", Convert.ToInt32(i), 0);
                    rf.AddLast(r5_1);
                }
                if (produktionP2[8] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP2[8]) / 2);
                    Reihenfolgenplanung r8_1 = new Reihenfolgenplanung(20, "8", Convert.ToInt32(i), 0);
                    rf.AddLast(r8_1);
                }
                if (produktionP2[5] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP2[5]) / 2);
                    Reihenfolgenplanung r5_2 = new Reihenfolgenplanung(21, "5", Convert.ToInt32(i), 0);
                    rf.AddLast(r5_2);
                }
                if (produktionP2[8] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP2[8]) / 2);
                    Reihenfolgenplanung r8_2 = new Reihenfolgenplanung(22, "8", Convert.ToInt32(i), 0);
                    rf.AddLast(r8_2);
                }
                if (produktionP2[11] > 0)
                {
                    Reihenfolgenplanung r11 = new Reihenfolgenplanung(23, "11", produktionP2[11], 0);
                    rf.AddLast(r11);
                }
                if (produktionP2[14] > 0)
                {
                    Reihenfolgenplanung r14 = new Reihenfolgenplanung(24, "14", produktionP2[14], 0);
                    rf.AddLast(r14);
                }
                if (produktionP2[19] > 0)
                {
                    Reihenfolgenplanung r19 = new Reihenfolgenplanung(25, "19", produktionP2[19], 0);
                    rf.AddLast(r19);
                }
                return rf;
            }

            if (p == "p3")
            {
                if (produktionP3[3] > 0)
                {
                    Reihenfolgenplanung r3 = new Reihenfolgenplanung(26, "3", produktionP3[3], 0);
                    rf.AddLast(r3);
                }
                if (produktionP3[31] > 0)
                {
                    Reihenfolgenplanung r31 = new Reihenfolgenplanung(27, "31", produktionP3[31], 0);
                    rf.AddLast(r31);
                }
                if (produktionP3[30] > 0)
                {
                    Reihenfolgenplanung r30 = new Reihenfolgenplanung(28, "30", produktionP3[30], 0);
                    rf.AddLast(r30);
                }
                if (produktionP3[29] > 0)
                {
                    Reihenfolgenplanung r29 = new Reihenfolgenplanung(29, "29", produktionP3[29], 0);
                    rf.AddLast(r29);
                }
                if (produktionP3[6] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP3[6]) / 2);
                    Reihenfolgenplanung r6_1 = new Reihenfolgenplanung(30, "6", Convert.ToInt32(i), 0);
                    rf.AddLast(r6_1);
                }
                if (produktionP3[9] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP3[9]) / 2);
                    Reihenfolgenplanung r9_1 = new Reihenfolgenplanung(31, "9", Convert.ToInt32(i), 0);
                    rf.AddLast(r9_1);
                }
                if (produktionP3[6] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP3[6]) / 2);
                    Reihenfolgenplanung r6_2 = new Reihenfolgenplanung(32, "6", Convert.ToInt32(i), 0);
                    rf.AddLast(r6_2);
                }
                if (produktionP3[9] > 0)
                {
                    double i = Math.Ceiling(Convert.ToDouble(produktionP3[9]) / 2);
                    Reihenfolgenplanung r9_2 = new Reihenfolgenplanung(33, "9", Convert.ToInt32(i), 0);
                    rf.AddLast(r9_2);
                }
                if (produktionP3[12] > 0)
                {
                    Reihenfolgenplanung r12 = new Reihenfolgenplanung(34, "12", produktionP3[12], 0);
                    rf.AddLast(r12);
                }
                if (produktionP3[15] > 0)
                {
                    Reihenfolgenplanung r15 = new Reihenfolgenplanung(35, "15", produktionP3[15], 0);
                    rf.AddLast(r15);
                }
                if (produktionP3[20] > 0)
                {
                    Reihenfolgenplanung r20 = new Reihenfolgenplanung(36, "20", produktionP3[20], 0);
                    rf.AddLast(r20);
                }
                return rf;
            }

            return rf;
        }
    
        #region Bestellung
        private void fillBestellungsTabWithData(object sender, EventArgs e)
        {

            if (tabControl_best.SelectedTab == tabControl_best.TabPages["tabPage_best_bestellung"])
            {
                generateBestellListe();
            }

            if (tabControl_best.SelectedTab == tabControl_best.TabPages["tabPage_best_kaufteileverbrauch"])
            {
                if (xmlData.getXmlImported())
                {
                    bestellung_geklickt = 1;
                    //ColumnHeaderText Bedarf/Bestand an Periode anpassen
                    //TODO: Für Sprachsteuerung anpassbar machen
                    //TODO: Angleichen bei neuer Column Anordnung
                    int periodN = xmlData.period;
                    
                    string bruttoBedarfPeriodeLocalised = culInfo.Contains(de) ? (deBruttobedarf + " " + dePeriode) : (enBruttobedarf + " " + enPeriode);
                    string bestandPeriodeLocalised = culInfo.Contains(de) ? (deBestand + " " + dePeriode) : (enBestand + " " + enPeriode);

                    dataGridView_best_kaufteileverbrauch.Columns[4].HeaderText = bruttoBedarfPeriodeLocalised + periodN.ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[5].HeaderText = bruttoBedarfPeriodeLocalised + (periodN + 1).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[6].HeaderText = bruttoBedarfPeriodeLocalised + (periodN + 2).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[7].HeaderText = bruttoBedarfPeriodeLocalised + (periodN + 3).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[8].HeaderText = bestandPeriodeLocalised + (periodN + 1).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[9].HeaderText = bestandPeriodeLocalised + (periodN + 2).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[10].HeaderText = bestandPeriodeLocalised + (periodN + 3).ToString();
                    dataGridView_best_kaufteileverbrauch.Columns[11].HeaderText = bestandPeriodeLocalised + (periodN + 4).ToString();

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
                                
                        int indexOfNewRow = dataGridView_best_kaufteileverbrauch.Rows.Add(teilenummer.ToString(), lagerBestand.ToString(), lagerZugang.ToString(), false,
                                    bruttoBedarfP1.ToString(), bruttoBedarfP2.ToString(), bruttoBedarfP3.ToString(), bruttoBedarfP4.ToString(),
                                    bestandN1.ToString(), bestandN2.ToString(), bestandN3.ToString(), bestandN4.ToString());
                            
                            //Produktion kann nicht durchgeführt werden
                            if (criticalFlag == 2)
                            {
                                dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells["Column_best_kaufteileverbrauch_bestandN1"].Style.BackColor = Color.Red;
                                                               
                            }

                            if (criticalFlag == 1)
                            {
                            dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells["Column_best_kaufteileverbrauch_produktionskritisch"].Value = true;
                                //dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells["Column_best_kaufteileverbrauch_bestandN1"].Style.ForeColor = ControlPaint.Light(Color.Red);
                            }

                        int index = 0;
                        if (criticalFlag == 2)
                                {
                            index = 1;
                            }
                        //Zellen leicht rot einfärben wenn Bestand negativ
                        while (index < bestaende.Count())
                            {
                            if (bestaende[index] <= 0)
                                {
                                    
                                dataGridView_best_kaufteileverbrauch.Rows[indexOfNewRow].Cells[index + 8].Style.ForeColor = ControlPaint.Light(Color.Red);
                                    }
                            ++index;
                                    }

                                }
                            }
                        }
                    }

        private void generateBestellListe()
        {            
                if (xmlData.getXmlImported() && dispoErgebnis != null)
                {
                    dataGridView_best_bestellliste.Rows.Clear();
                                    
                    BestellVerwaltung bestellVerwaltung;
                    //Initial erstellen
                    if (bestellListeErzeugt == 0)
                    {
                        bestellVerwaltung = new BestellVerwaltung();
                        bestellVerwaltung.generiereBestellListe(dispoErgebnis);
                        bestellungsListe = bestellVerwaltung.getBestellungListe();
                    }
                    //Else = Liste schon erzeugt
                   
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
            
            
        //Neue Zeile zu den Bestellungrows hinzufügen
        private void btn_best_addRow_Click(object sender, EventArgs e)
        {
            dataGridView_best_bestellliste.Rows.Add();
            int newIndex = dataGridView_best_bestellliste.Rows.Count-1;
            dataGridView_best_bestellliste.CurrentCell = this.dataGridView_best_bestellliste["Column_best_bestelliste_nummer", newIndex];
            dataGridView_best_bestellliste.BeginEdit(true);
        }

        //Bestellliste speichern
        private void btn_best_save_Click(object sender, EventArgs e)
        {

            if (dataGridView_best_bestellliste.Rows.Count > 0)
            {
                if (bestellungsListe == null)
                {
                    bestellungsListe = new List<Bestellung>();
                }
                if (bestellungsListe.Count > 0)
                {
                    bestellungsListe.Clear();
                }
                
                foreach (DataGridViewRow row in dataGridView_best_bestellliste.Rows)
                {
                    int teileNummer = Convert.ToInt32(row.Cells[0].Value);
                    int menge = Convert.ToInt32(row.Cells[1].Value);
                    bool eil = Convert.ToBoolean(row.Cells[2].Value);
                    int bestellTyp = (eil == true) ? 4 : 5;

                    bestellungsListe.Add(new Bestellung(new Teil(teileNummer), menge, bestellTyp));

                }
            }
            bestellListeErzeugt = 1;
        }
        //Bestellungen Liste zurücksetzen
        private void btn_best_reset_Click(object sender, EventArgs e)
        {
            if (bestellungsListe != null)
            {
                bestellungsListe.Clear();
                
            }
            bestellListeErzeugt = 0;
            generateBestellListe();
        }

        
        //Hilfsmethode für die Validierung der "Nummer"-Spalte in der Bestellliste
        private bool bestellliste_nummer_validieren(DataGridViewCell teilenummerCell)
        {
            bool validationSuccess = true;

            DataGridViewRow row = dataGridView_best_bestellliste.Rows[teilenummerCell.RowIndex];
            teilenummerCell.ErrorText = "";
            row.ErrorText = "";
            int intValue;

            //Keine Eingabe
            if (string.IsNullOrEmpty(teilenummerCell.FormattedValue.ToString()))
            {
                validationSuccess = false;
                teilenummerCell.ErrorText = "Bitte geben Sie das zu bestellende Kaufteil an!";
                row.ErrorText = "Bitte geben Sie das zu bestellende Kaufteil an!";
                
        }

            //Keine Zahl
            else if (!int.TryParse(teilenummerCell.FormattedValue.ToString(), out intValue))
            {
                validationSuccess = false;
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
                    validationSuccess = false;
                    teilenummerCell.ErrorText = "Bitte geben Sie eine gültige Kaufteilnummer an!";
                    row.ErrorText = "Bitte geben Sie eine gültige Kaufteilnummer an!";
                }
                
            }
            return validationSuccess;
        }

        //Hilfsmethode für die Validierung der "Menge"-Spalte in der Bestellliste
        private bool bestellliste_menge_validieren(DataGridViewCell mengeCell)
        {
            bool validationSuccess = true;
            DataGridViewRow row = dataGridView_best_bestellliste.Rows[mengeCell.RowIndex];
            mengeCell.ErrorText = "";
            row.ErrorText = "";
            int intValue;

            if (string.IsNullOrEmpty(mengeCell.FormattedValue.ToString()))
            {
                validationSuccess = false;
                mengeCell.ErrorText = "Bitte geben Sie die zu bestellende Menge an!";
                row.ErrorText = "Bitte geben Sie die zu bestellende Menge an!";
            }
            else if (!int.TryParse(mengeCell.FormattedValue.ToString(), out intValue) || intValue <= 0)
            {
                validationSuccess = false;
                mengeCell.ErrorText = "Bitte geben Sie eine positive Ganzzahl für die zu bestellende Menge an!";
                row.ErrorText = "Bitte geben Sie eine positive Ganzzahl für die zu bestellende Menge an!";
            }
            return validationSuccess;
        }
        #endregion
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
                pp_p1_26_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(26).ToString();
                pp_p1_26_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(26).ToString();
                pp_p1_51_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(51).ToString();
                pp_p1_51_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(51).ToString();
                pp_p1_16_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(16).ToString();
                pp_p1_16_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(16).ToString();
                pp_p1_17_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(17).ToString();
                pp_p1_17_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(17).ToString();
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
                pp_p2_26_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(26).ToString();
                pp_p2_26_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(26).ToString();
                pp_p2_56_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(56).ToString();
                pp_p2_56_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(56).ToString();
                pp_p2_16_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(16).ToString();
                pp_p2_16_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(16).ToString();
                pp_p2_17_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(17).ToString();
                pp_p2_17_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(17).ToString();
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
                pp_p3_26_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(26).ToString();
                pp_p3_26_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(26).ToString();
                pp_p3_31_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(31).ToString();
                pp_p3_31_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(31).ToString();
                pp_p3_16_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(16).ToString();
                pp_p3_16_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(16).ToString();
                pp_p3_17_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByKHDItem(17).ToString();
                pp_p3_17_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByKHDItem(17).ToString();
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
                        uebersicht_kapa = 1;
                        uebersicht_geklickt = 1;
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
       
        //Keypress Handling speziell für das Direktverkauf DataGridView
        private void direktverkauf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView_dirver_direktverkauf.CurrentCell.ColumnIndex == 2 || dataGridView_dirver_direktverkauf.CurrentCell.ColumnIndex == 3)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == ','))
                    e.Handled = true;
            }
            else
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }
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

        private void dataGridView_rf_planung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;           
            
            if (senderGrid.Columns[e.ColumnIndex].HeaderText == "Vor")
            {                
                    int index = e.RowIndex;

                    Reihenfolgenplanung p = listRfpglobal.ElementAt(index);   
                   
                if (listRfpglobal.First.Value.getIndex() != p.getIndex())
                    {
                    int teilvor = int.Parse(senderGrid.Rows[index - 1].Cells["Column_rf_rfPlanung_Index"].Value.ToString());
                    Reihenfolgenplanung pvor = getItemByIndex(teilvor);

                    LinkedListNode<Reihenfolgenplanung> gefunden = null;

                    for (LinkedListNode<Reihenfolgenplanung> node = listRfpglobal.First; node != listRfpglobal.Last.Next; node = node.Next)
                    {
                        if (node.Value.getIndex() == pvor.getIndex())
                        {
                            gefunden = node;
                            gefunden.Value.setIndex(reihenIndex);
                            reihenIndex++;
                        }
                    }

                    listRfpglobal.Remove(p);
                    listRfpglobal.AddBefore(gefunden, p);
                }              
                dataGridView_rf_planung.Rows.Clear();
                refreshReihenfolgenplanung(); 
            }

            if (senderGrid.Columns[e.ColumnIndex].HeaderText == "Zurück")
            {
                int index = e.RowIndex;                
                Reihenfolgenplanung p = listRfpglobal.ElementAt(index);               

                if (listRfpglobal.Last.Value.getIndex() != p.getIndex())
                {
                    listRfpglobal.Remove(p);

                    int teildanach = int.Parse(senderGrid.Rows[index + 1].Cells["Column_rf_rfPlanung_Index"].Value.ToString());
                    Reihenfolgenplanung pnach = getItemByIndex(teildanach);

                    LinkedListNode<Reihenfolgenplanung> gefunden = null;

                    for (LinkedListNode<Reihenfolgenplanung> node = listRfpglobal.First; node != listRfpglobal.Last.Next; node = node.Next)
                    {
                        if (node.Value.getIndex() == pnach.getIndex())
                        {
                            gefunden = node;
                            gefunden.Value.setIndex(reihenIndex);
                            reihenIndex++;
                        }
                    }
                    listRfpglobal.AddAfter(gefunden, p);
                }
                dataGridView_rf_planung.Rows.Clear();
                refreshReihenfolgenplanung();
            }
          

        }               

        private Reihenfolgenplanung getItemByIndex(int i)
        {
            foreach (Reihenfolgenplanung r in listRfpglobal)
            {
                if (r.getIndex() == i)
                {
                    return r;
                }
            }
            return null;

        }


        private void rf_splitten_button_Click(object sender, EventArgs e)
        {
            DataGridView sender1 = dataGridView_rf_planung;
            var senderGrid = (DataGridView)sender1;            
            for(int i=0; i<senderGrid.Rows.Count; i++)
            {

                int menge = int.Parse(senderGrid.Rows[i].Cells["Column_rf_rfPlanung_menge"].Value.ToString());
                    int index = int.Parse(senderGrid.Rows[i].Cells["Column_rf_rfPlanung_Index"].Value.ToString());
                    int splitt = int.Parse(senderGrid.Rows[i].Cells["Column_rf_rfPlanung_Splitt"].Value.ToString());
                    if (splitt > 0 && splitt <= menge)
                    {
                        Reihenfolgenplanung p = getItemByIndex(index);
                        Reihenfolgenplanung pold = new Reihenfolgenplanung(reihenIndex, p.getTeil(), p.getMenge() - splitt, 0);
                        reihenIndex++;
                        Reihenfolgenplanung pnew = new Reihenfolgenplanung(reihenIndex, p.getTeil(), splitt, 0);
                        reihenIndex++;
                       
                        listRfpglobal.AddLast(pold);
                        listRfpglobal.AddLast(pnew);
                        listRfpglobal.Remove(p);
                    }
                    
                
            }
            dataGridView_rf_planung.Rows.Clear();
            refreshReihenfolgenplanung();
        }

        private void button_kapa_safe_Click(object sender, EventArgs e)
        {
            DataGridView sender1 = dataGridView_kp_uebersicht;
            var senderGrid = (DataGridView)sender1;     
            foreach (ArbeitsplatzKapa ak in kapazitaetsListe)
            {
                for (int i = 0; i < dataGridView_kp_uebersicht.Rows.Count; i++)
                {
                    if (int.Parse(dataGridView_kp_uebersicht.Rows[i].Cells["Column_kp_uebersicht_arbeitsplatz"].Value.ToString()) == ak.getArbeitsplatz() && int.Parse(dataGridView_kp_uebersicht.Rows[i].Cells["Column_kp_uebersicht_ueberstunden"].Value.ToString()) != ak.getUeberstunden())
                    {
                        int ueberstunden = int.Parse(dataGridView_kp_uebersicht.Rows[i].Cells["Column_kp_uebersicht_ueberstunden"].Value.ToString());
                        ak.setUeberstunden(ueberstunden);
                    }

                }
            }


        }

        private void btn_direv_save_Click(object sender, EventArgs e)
        {
            if (direktVerkauf == null)
            {
                direktVerkauf = new List<Direktverkauf>();
            }
            else
            {
                direktVerkauf.Clear();
            }

            if (dataGridView_dirver_direktverkauf.Rows.Count > 0)
            {
              foreach (DataGridViewRow row in dataGridView_dirver_direktverkauf.Rows)
                {
                    saveAsDirektverkauf(row);
                }
            }
        }
    }
}