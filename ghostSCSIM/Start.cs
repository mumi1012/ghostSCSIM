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

namespace ghostSCSIM
{
    public partial class Start : Form
    {
        // Neuen Datenbehälter für den XML Input anlegen
        DataContainer xmlData = new DataContainer();

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
            foreach (Control c in this.Controls)
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

        /// <summary>
        /// Test Database-Connection 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void button2_Click(object sender, EventArgs e)
       {

           string connectionString =
                         @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                         @"Data Source=C:\users\michael\documents\visual studio 2013\Projects\ghostSCSIM\ghostSCSIM\Datenbank.accdb;";
                        
          
           OleDbConnection connection;
           OleDbDataAdapter oledbAdapter;
           DataSet ds = new DataSet();
           string sql = null;
           int i = 0;

           sql = "SELECT * FROM Lager";
           connection = new OleDbConnection(connectionString);

           try
           {
               connection.Open();
               oledbAdapter = new OleDbDataAdapter(sql, connection);
               oledbAdapter.Fill(ds);
               oledbAdapter.Dispose();
               connection.Close();
               //Ausgabe der Daten aus der DB an den Datagrid
               //dataGridView1.DataSource = ds.Tables[0];
               //MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
               //
               //for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
               //{
               //    MessageBox.Show(ds.Tables[0].Rows[i].ItemArray[0] + " -- " + ds.Tables[0].Rows[i].ItemArray[1]);
               //}
           }
           catch (Exception ex)
           {
               MessageBox.Show("Exception: "+ex.Message);
           }

           }

       private void xmlInputButton_Click(object sender, EventArgs e)
       {
           
           var fileDialog = new OpenFileDialog { };
           if (fileDialog.ShowDialog() == DialogResult.OK && xmlData.getXmlImported() == false)
           {
               XmlIO xmlInput = new XmlIO(fileDialog.FileName);
               xmlData = (DataContainer)xmlInput.xml;

               xmlData.setXmlImported(true);
               MessageBox.Show("XML_File erfolgreich importiert!");
           }
       }
       
       

       private void fillFormsWithData(object sender, EventArgs e)
       {           
           if (xmlData.getXmlImported())
           {
               pp_p1_1_lager.Text = xmlData.warehouseStock.article[0].amount.ToString();
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

               pp_p2_2_lager.Text = xmlData.warehouseStock.article[1].amount.ToString();
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

               pp_p3_3_lager.Text = xmlData.warehouseStock.article[2].amount.ToString();
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

               //Bestellung
               bestellung_k21_id.Text = xmlData.warehouseStock.article[20].id.ToString();
               bestellung_k22_id.Text = xmlData.warehouseStock.article[21].id.ToString();
               bestellung_k23_id.Text = xmlData.warehouseStock.article[22].id.ToString();
               bestellung_k24_id.Text = xmlData.warehouseStock.article[23].id.ToString();
               bestellung_k25_id.Text = xmlData.warehouseStock.article[24].id.ToString();
               bestellung_k27_id.Text = xmlData.warehouseStock.article[26].id.ToString();
               bestellung_k28_id.Text = xmlData.warehouseStock.article[27].id.ToString();
               bestellung_k32_id.Text = xmlData.warehouseStock.article[31].id.ToString();
               bestellung_k33_id.Text = xmlData.warehouseStock.article[32].id.ToString();
               bestellung_k34_id.Text = xmlData.warehouseStock.article[33].id.ToString();
               bestellung_k35_id.Text = xmlData.warehouseStock.article[34].id.ToString();
               bestellung_k36_id.Text = xmlData.warehouseStock.article[35].id.ToString();
               bestellung_k37_id.Text = xmlData.warehouseStock.article[36].id.ToString();
               bestellung_k38_id.Text = xmlData.warehouseStock.article[37].id.ToString();
               bestellung_k39_id.Text = xmlData.warehouseStock.article[38].id.ToString();
               bestellung_k40_id.Text = xmlData.warehouseStock.article[39].id.ToString();
               bestellung_k41_id.Text = xmlData.warehouseStock.article[40].id.ToString();
               bestellung_k42_id.Text = xmlData.warehouseStock.article[41].id.ToString();
               bestellung_k43_id.Text = xmlData.warehouseStock.article[42].id.ToString();
               bestellung_k44_id.Text = xmlData.warehouseStock.article[43].id.ToString();
               bestellung_k45_id.Text = xmlData.warehouseStock.article[44].id.ToString();
               bestellung_k46_id.Text = xmlData.warehouseStock.article[45].id.ToString();
               bestellung_k47_id.Text = xmlData.warehouseStock.article[46].id.ToString();
               bestellung_k48_id.Text = xmlData.warehouseStock.article[47].id.ToString();
               bestellung_k52_id.Text = xmlData.warehouseStock.article[51].id.ToString();
               bestellung_k53_id.Text = xmlData.warehouseStock.article[52].id.ToString();
               bestellung_k57_id.Text = xmlData.warehouseStock.article[56].id.ToString();
               bestellung_k58_id.Text = xmlData.warehouseStock.article[57].id.ToString();
               bestellung_k59_id.Text = xmlData.warehouseStock.article[58].id.ToString();

               bestellung_k21_bestand.Text = xmlData.warehouseStock.article[20].amount.ToString();
               bestellung_k22_bestand.Text = xmlData.warehouseStock.article[21].amount.ToString();
               bestellung_k23_bestand.Text = xmlData.warehouseStock.article[22].amount.ToString();
               bestellung_k24_bestand.Text = xmlData.warehouseStock.article[23].amount.ToString();
               bestellung_k25_bestand.Text = xmlData.warehouseStock.article[24].amount.ToString();
               bestellung_k27_bestand.Text = xmlData.warehouseStock.article[26].amount.ToString();
               bestellung_k28_bestand.Text = xmlData.warehouseStock.article[27].amount.ToString();
               bestellung_k32_bestand.Text = xmlData.warehouseStock.article[31].amount.ToString();
               bestellung_k33_bestand.Text = xmlData.warehouseStock.article[32].amount.ToString();
               bestellung_k34_bestand.Text = xmlData.warehouseStock.article[33].amount.ToString();
               bestellung_k35_bestand.Text = xmlData.warehouseStock.article[34].amount.ToString();
               bestellung_k36_bestand.Text = xmlData.warehouseStock.article[35].amount.ToString();
               bestellung_k37_bestand.Text = xmlData.warehouseStock.article[36].amount.ToString();
               bestellung_k38_bestand.Text = xmlData.warehouseStock.article[37].amount.ToString();
               bestellung_k39_bestand.Text = xmlData.warehouseStock.article[38].amount.ToString();
               bestellung_k40_bestand.Text = xmlData.warehouseStock.article[39].amount.ToString();
               bestellung_k41_bestand.Text = xmlData.warehouseStock.article[40].amount.ToString();
               bestellung_k42_bestand.Text = xmlData.warehouseStock.article[41].amount.ToString();
               bestellung_k43_bestand.Text = xmlData.warehouseStock.article[42].amount.ToString();
               bestellung_k44_bestand.Text = xmlData.warehouseStock.article[43].amount.ToString();
               bestellung_k45_bestand.Text = xmlData.warehouseStock.article[44].amount.ToString();
               bestellung_k46_bestand.Text = xmlData.warehouseStock.article[45].amount.ToString();
               bestellung_k47_bestand.Text = xmlData.warehouseStock.article[46].amount.ToString();
               bestellung_k48_bestand.Text = xmlData.warehouseStock.article[47].amount.ToString();
               bestellung_k52_bestand.Text = xmlData.warehouseStock.article[51].amount.ToString();
               bestellung_k53_bestand.Text = xmlData.warehouseStock.article[52].amount.ToString();
               bestellung_k57_bestand.Text = xmlData.warehouseStock.article[56].amount.ToString();
               bestellung_k58_bestand.Text = xmlData.warehouseStock.article[57].amount.ToString();
               bestellung_k59_bestand.Text = xmlData.warehouseStock.article[58].amount.ToString();

               //Warteschleife und InBearbeitung 
               pp_p1_1_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(1).ToString();
               pp_p1_1_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(1).ToString();
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

               pp_p2_2_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(2).ToString();
               pp_p2_2_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(2).ToString();
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

               pp_p3_3_ws.Text = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(3).ToString();
               pp_p3_3_bearb.Text = xmlData.ordersInWork.getInBearbeitungMengeByItem(3).ToString();
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

               //Vertriebswünsche P1 P2 P3
               pp_p1_1_vw.Text = kinder_prog_p1.Value.ToString();   
               pp_p2_2_vw.Text = damen_prog_p1.Value.ToString();
               pp_p3_3_vw.Text = herren_prog_p1.Value.ToString();        
               
               //Produktion 
               pp_p1_1_prod.Text = (int.Parse(pp_p1_1_vw.Text.ToString()) + int.Parse(pp_p1_1_sb.Text.ToString()) - int.Parse(pp_p1_1_lager.Text.ToString()) - int.Parse(pp_p1_1_ws.Text.ToString()) - int.Parse(pp_p1_1_bearb.Text.ToString())).ToString();
               pp_p1_26_prod.Text = (int.Parse(pp_p1_1_prod.Text.ToString()) + int.Parse(pp_p1_26_sb.Text.ToString()) - int.Parse(pp_p1_26_lager.Text.ToString()) - int.Parse(pp_p1_26_ws.Text.ToString()) - int.Parse(pp_p1_26_bearb.Text.ToString())).ToString();
               pp_p1_26_vw.Text = pp_p1_1_prod.Text;               
               pp_p1_51_prod.Text = (int.Parse(pp_p1_1_prod.Text.ToString()) + int.Parse(pp_p1_51_sb.Text.ToString()) - int.Parse(pp_p1_51_lager.Text.ToString()) - int.Parse(pp_p1_51_ws.Text.ToString()) - int.Parse(pp_p1_51_bearb.Text.ToString())).ToString();
               pp_p1_51_vw.Text = pp_p1_1_prod.Text;
               pp_p1_16_prod.Text = (int.Parse(pp_p1_51_prod.Text.ToString()) + int.Parse(pp_p1_16_sb.Text.ToString()) - int.Parse(pp_p1_16_lager.Text.ToString()) - int.Parse(pp_p1_16_ws.Text.ToString()) - int.Parse(pp_p1_16_bearb.Text.ToString())).ToString();
               pp_p1_16_vw.Text = pp_p1_51_prod.Text;
               pp_p1_17_prod.Text = (int.Parse(pp_p1_51_prod.Text.ToString()) + int.Parse(pp_p1_17_sb.Text.ToString()) - int.Parse(pp_p1_17_lager.Text.ToString()) - int.Parse(pp_p1_17_ws.Text.ToString()) - int.Parse(pp_p1_17_bearb.Text.ToString())).ToString();
               pp_p1_17_vw.Text = pp_p1_51_prod.Text;
               pp_p1_50_prod.Text = (int.Parse(pp_p1_51_prod.Text.ToString()) + int.Parse(pp_p1_50_sb.Text.ToString()) - int.Parse(pp_p1_50_lager.Text.ToString()) - int.Parse(pp_p1_50_ws.Text.ToString()) - int.Parse(pp_p1_50_bearb.Text.ToString())).ToString();
               pp_p1_50_vw.Text = pp_p1_51_prod.Text;
               pp_p1_4_prod.Text = (int.Parse(pp_p1_50_prod.Text.ToString()) + int.Parse(pp_p1_4_sb.Text.ToString()) - int.Parse(pp_p1_4_lager.Text.ToString()) - int.Parse(pp_p1_4_ws.Text.ToString()) - int.Parse(pp_p1_4_bearb.Text.ToString())).ToString();
               pp_p1_4_vw.Text = pp_p1_50_prod.Text;
               pp_p1_10_prod.Text = (int.Parse(pp_p1_50_prod.Text.ToString()) + int.Parse(pp_p1_10_sb.Text.ToString()) - int.Parse(pp_p1_10_lager.Text.ToString()) - int.Parse(pp_p1_10_ws.Text.ToString()) - int.Parse(pp_p1_10_bearb.Text.ToString())).ToString();
               pp_p1_10_vw.Text = pp_p1_50_prod.Text;
               pp_p1_49_prod.Text = (int.Parse(pp_p1_50_prod.Text.ToString()) + int.Parse(pp_p1_49_sb.Text.ToString()) - int.Parse(pp_p1_49_lager.Text.ToString()) - int.Parse(pp_p1_49_ws.Text.ToString()) - int.Parse(pp_p1_49_bearb.Text.ToString())).ToString();
               pp_p1_49_vw.Text = pp_p1_50_prod.Text;
               pp_p1_7_prod.Text = (int.Parse(pp_p1_49_prod.Text.ToString()) + int.Parse(pp_p1_7_sb.Text.ToString()) - int.Parse(pp_p1_7_lager.Text.ToString()) - int.Parse(pp_p1_7_ws.Text.ToString()) - int.Parse(pp_p1_7_bearb.Text.ToString())).ToString();
               pp_p1_7_vw.Text = pp_p1_49_prod.Text;
               pp_p1_13_prod.Text = (int.Parse(pp_p1_49_prod.Text.ToString()) + int.Parse(pp_p1_13_sb.Text.ToString()) - int.Parse(pp_p1_13_lager.Text.ToString()) - int.Parse(pp_p1_13_ws.Text.ToString()) - int.Parse(pp_p1_13_bearb.Text.ToString())).ToString();
               pp_p1_13_vw.Text = pp_p1_49_prod.Text;
               pp_p1_18_prod.Text = (int.Parse(pp_p1_49_prod.Text.ToString()) + int.Parse(pp_p1_18_sb.Text.ToString()) - int.Parse(pp_p1_18_lager.Text.ToString()) - int.Parse(pp_p1_18_ws.Text.ToString()) - int.Parse(pp_p1_18_bearb.Text.ToString())).ToString();
               pp_p1_18_vw.Text = pp_p1_49_prod.Text;

               pp_p2_2_prod.Text = (int.Parse(pp_p2_2_vw.Text.ToString()) + int.Parse(pp_p2_2_sb.Text.ToString()) - int.Parse(pp_p2_2_lager.Text.ToString()) - int.Parse(pp_p2_2_ws.Text.ToString()) - int.Parse(pp_p2_2_bearb.Text.ToString())).ToString();
               pp_p2_26_prod.Text = (int.Parse(pp_p2_2_vw.Text.ToString()) + int.Parse(pp_p2_26_sb.Text.ToString()) - int.Parse(pp_p2_26_lager.Text.ToString()) - int.Parse(pp_p2_26_ws.Text.ToString()) - int.Parse(pp_p2_26_bearb.Text.ToString())).ToString();
               pp_p2_26_vw.Text = pp_p2_2_prod.Text;
               pp_p2_56_prod.Text = (int.Parse(pp_p2_2_vw.Text.ToString()) + int.Parse(pp_p2_56_sb.Text.ToString()) - int.Parse(pp_p2_56_lager.Text.ToString()) - int.Parse(pp_p2_56_ws.Text.ToString()) - int.Parse(pp_p2_56_bearb.Text.ToString())).ToString();
               pp_p2_56_vw.Text = pp_p2_2_prod.Text;
               pp_p2_16_prod.Text = (int.Parse(pp_p2_56_vw.Text.ToString()) + int.Parse(pp_p2_16_sb.Text.ToString()) - int.Parse(pp_p2_16_lager.Text.ToString()) - int.Parse(pp_p2_16_ws.Text.ToString()) - int.Parse(pp_p2_16_bearb.Text.ToString())).ToString();
               pp_p2_16_vw.Text = pp_p2_56_prod.Text;
               pp_p2_17_prod.Text = (int.Parse(pp_p2_56_vw.Text.ToString()) + int.Parse(pp_p2_17_sb.Text.ToString()) - int.Parse(pp_p2_17_lager.Text.ToString()) - int.Parse(pp_p2_17_ws.Text.ToString()) - int.Parse(pp_p2_17_bearb.Text.ToString())).ToString();
               pp_p2_17_vw.Text = pp_p2_56_prod.Text;
               pp_p2_55_prod.Text = (int.Parse(pp_p2_56_vw.Text.ToString()) + int.Parse(pp_p2_55_sb.Text.ToString()) - int.Parse(pp_p2_55_lager.Text.ToString()) - int.Parse(pp_p2_55_ws.Text.ToString()) - int.Parse(pp_p2_55_bearb.Text.ToString())).ToString();
               pp_p2_55_vw.Text = pp_p2_56_prod.Text;
               pp_p2_5_prod.Text = (int.Parse(pp_p2_55_vw.Text.ToString()) + int.Parse(pp_p2_5_sb.Text.ToString()) - int.Parse(pp_p2_5_lager.Text.ToString()) - int.Parse(pp_p2_5_ws.Text.ToString()) - int.Parse(pp_p2_5_bearb.Text.ToString())).ToString();
               pp_p2_5_vw.Text = pp_p2_55_prod.Text;
               pp_p2_11_prod.Text = (int.Parse(pp_p2_55_vw.Text.ToString()) + int.Parse(pp_p2_11_sb.Text.ToString()) - int.Parse(pp_p2_11_lager.Text.ToString()) - int.Parse(pp_p2_11_ws.Text.ToString()) - int.Parse(pp_p2_11_bearb.Text.ToString())).ToString();
               pp_p2_11_vw.Text = pp_p2_55_prod.Text;
               pp_p2_54_prod.Text = (int.Parse(pp_p2_55_vw.Text.ToString()) + int.Parse(pp_p2_54_sb.Text.ToString()) - int.Parse(pp_p2_54_lager.Text.ToString()) - int.Parse(pp_p2_54_ws.Text.ToString()) - int.Parse(pp_p2_54_bearb.Text.ToString())).ToString();
               pp_p2_54_vw.Text = pp_p2_55_prod.Text;
               pp_p2_8_prod.Text = (int.Parse(pp_p2_54_vw.Text.ToString()) + int.Parse(pp_p2_8_sb.Text.ToString()) - int.Parse(pp_p2_8_lager.Text.ToString()) - int.Parse(pp_p2_8_ws.Text.ToString()) - int.Parse(pp_p2_8_bearb.Text.ToString())).ToString();
               pp_p2_8_vw.Text = pp_p2_54_prod.Text;
               pp_p2_14_prod.Text = (int.Parse(pp_p2_54_vw.Text.ToString()) + int.Parse(pp_p2_14_sb.Text.ToString()) - int.Parse(pp_p2_14_lager.Text.ToString()) - int.Parse(pp_p2_14_ws.Text.ToString()) - int.Parse(pp_p2_14_bearb.Text.ToString())).ToString();
               pp_p2_14_vw.Text = pp_p2_54_prod.Text;
               pp_p2_19_prod.Text = (int.Parse(pp_p2_54_vw.Text.ToString()) + int.Parse(pp_p2_19_sb.Text.ToString()) - int.Parse(pp_p2_19_lager.Text.ToString()) - int.Parse(pp_p2_19_ws.Text.ToString()) - int.Parse(pp_p2_19_bearb.Text.ToString())).ToString();
               pp_p2_19_vw.Text = pp_p2_54_prod.Text;

               pp_p3_3_prod.Text = (int.Parse(pp_p3_3_vw.Text.ToString()) + int.Parse(pp_p3_3_sb.Text.ToString()) - int.Parse(pp_p3_3_lager.Text.ToString()) - int.Parse(pp_p3_3_ws.Text.ToString()) - int.Parse(pp_p3_3_bearb.Text.ToString())).ToString();
               pp_p3_26_prod.Text = (int.Parse(pp_p3_3_vw.Text.ToString()) + int.Parse(pp_p3_26_sb.Text.ToString()) - int.Parse(pp_p3_26_lager.Text.ToString()) - int.Parse(pp_p3_26_ws.Text.ToString()) - int.Parse(pp_p3_26_bearb.Text.ToString())).ToString();
               pp_p3_26_vw.Text = pp_p3_3_prod.Text;
               pp_p3_31_prod.Text = (int.Parse(pp_p3_3_vw.Text.ToString()) + int.Parse(pp_p3_31_sb.Text.ToString()) - int.Parse(pp_p3_31_lager.Text.ToString()) - int.Parse(pp_p3_31_ws.Text.ToString()) - int.Parse(pp_p3_31_bearb.Text.ToString())).ToString();
               pp_p3_31_vw.Text = pp_p3_3_prod.Text;
               pp_p3_16_prod.Text = (int.Parse(pp_p3_31_vw.Text.ToString()) + int.Parse(pp_p3_16_sb.Text.ToString()) - int.Parse(pp_p3_16_lager.Text.ToString()) - int.Parse(pp_p3_16_ws.Text.ToString()) - int.Parse(pp_p3_16_bearb.Text.ToString())).ToString();
               pp_p3_16_vw.Text = pp_p3_31_prod.Text;
               pp_p3_17_prod.Text = (int.Parse(pp_p3_31_vw.Text.ToString()) + int.Parse(pp_p3_17_sb.Text.ToString()) - int.Parse(pp_p3_17_lager.Text.ToString()) - int.Parse(pp_p3_17_ws.Text.ToString()) - int.Parse(pp_p3_17_bearb.Text.ToString())).ToString();
               pp_p3_17_vw.Text = pp_p3_31_prod.Text;
               pp_p3_30_prod.Text = (int.Parse(pp_p3_31_vw.Text.ToString()) + int.Parse(pp_p3_30_sb.Text.ToString()) - int.Parse(pp_p3_30_lager.Text.ToString()) - int.Parse(pp_p3_30_ws.Text.ToString()) - int.Parse(pp_p3_30_bearb.Text.ToString())).ToString();
               pp_p3_30_vw.Text = pp_p3_31_prod.Text;
               pp_p3_6_prod.Text = (int.Parse(pp_p3_30_vw.Text.ToString()) + int.Parse(pp_p3_6_sb.Text.ToString()) - int.Parse(pp_p3_6_lager.Text.ToString()) - int.Parse(pp_p3_6_ws.Text.ToString()) - int.Parse(pp_p3_6_bearb.Text.ToString())).ToString();
               pp_p3_6_vw.Text = pp_p3_30_prod.Text;
               pp_p3_12_prod.Text = (int.Parse(pp_p3_30_vw.Text.ToString()) + int.Parse(pp_p3_12_sb.Text.ToString()) - int.Parse(pp_p3_12_lager.Text.ToString()) - int.Parse(pp_p3_12_ws.Text.ToString()) - int.Parse(pp_p3_12_bearb.Text.ToString())).ToString();
               pp_p3_12_vw.Text = pp_p3_30_prod.Text;
               pp_p3_29_prod.Text = (int.Parse(pp_p3_30_vw.Text.ToString()) + int.Parse(pp_p3_29_sb.Text.ToString()) - int.Parse(pp_p3_29_lager.Text.ToString()) - int.Parse(pp_p3_29_ws.Text.ToString()) - int.Parse(pp_p3_29_bearb.Text.ToString())).ToString();
               pp_p3_29_vw.Text = pp_p3_30_prod.Text;
               pp_p3_9_prod.Text = (int.Parse(pp_p3_29_vw.Text.ToString()) + int.Parse(pp_p3_9_sb.Text.ToString()) - int.Parse(pp_p3_9_lager.Text.ToString()) - int.Parse(pp_p3_9_ws.Text.ToString()) - int.Parse(pp_p3_9_bearb.Text.ToString())).ToString();
               pp_p3_9_vw.Text = pp_p3_29_prod.Text;
               pp_p3_15_prod.Text = (int.Parse(pp_p3_29_vw.Text.ToString()) + int.Parse(pp_p3_15_sb.Text.ToString()) - int.Parse(pp_p3_15_lager.Text.ToString()) - int.Parse(pp_p3_15_ws.Text.ToString()) - int.Parse(pp_p3_15_bearb.Text.ToString())).ToString();
               pp_p3_15_vw.Text = pp_p3_29_prod.Text;
               pp_p3_20_prod.Text = (int.Parse(pp_p3_29_vw.Text.ToString()) + int.Parse(pp_p3_20_sb.Text.ToString()) - int.Parse(pp_p3_20_lager.Text.ToString()) - int.Parse(pp_p3_20_ws.Text.ToString()) - int.Parse(pp_p3_20_bearb.Text.ToString())).ToString();
               pp_p3_20_vw.Text = pp_p3_29_prod.Text; 
    
               //Bestellung
               //TODO nicht editierbar, Rest hinzufügen
               bestellung_k21_id.Text = (int.Parse(bestellung_k21_id.Text.ToString()).ToString());
               bestellung_k22_id.Text = (int.Parse(bestellung_k22_id.Text.ToString()).ToString());
               bestellung_k23_id.Text = (int.Parse(bestellung_k23_id.Text.ToString()).ToString());
               bestellung_k24_id.Text = (int.Parse(bestellung_k24_id.Text.ToString()).ToString());
               bestellung_k25_id.Text = (int.Parse(bestellung_k25_id.Text.ToString()).ToString());
               bestellung_k27_id.Text = (int.Parse(bestellung_k27_id.Text.ToString()).ToString());
               bestellung_k28_id.Text = (int.Parse(bestellung_k28_id.Text.ToString()).ToString());
               bestellung_k32_id.Text = (int.Parse(bestellung_k32_id.Text.ToString()).ToString());
               bestellung_k33_id.Text = (int.Parse(bestellung_k33_id.Text.ToString()).ToString());
               bestellung_k34_id.Text = (int.Parse(bestellung_k34_id.Text.ToString()).ToString());
               bestellung_k35_id.Text = (int.Parse(bestellung_k35_id.Text.ToString()).ToString());
               bestellung_k36_id.Text = (int.Parse(bestellung_k36_id.Text.ToString()).ToString());
               bestellung_k37_id.Text = (int.Parse(bestellung_k37_id.Text.ToString()).ToString());
               bestellung_k38_id.Text = (int.Parse(bestellung_k38_id.Text.ToString()).ToString());
               bestellung_k39_id.Text = (int.Parse(bestellung_k39_id.Text.ToString()).ToString());
               bestellung_k40_id.Text = (int.Parse(bestellung_k40_id.Text.ToString()).ToString());
               bestellung_k41_id.Text = (int.Parse(bestellung_k41_id.Text.ToString()).ToString());
               bestellung_k42_id.Text = (int.Parse(bestellung_k42_id.Text.ToString()).ToString());
               bestellung_k43_id.Text = (int.Parse(bestellung_k43_id.Text.ToString()).ToString());
               bestellung_k44_id.Text = (int.Parse(bestellung_k44_id.Text.ToString()).ToString());
               bestellung_k45_id.Text = (int.Parse(bestellung_k45_id.Text.ToString()).ToString());
               bestellung_k46_id.Text = (int.Parse(bestellung_k46_id.Text.ToString()).ToString());
               bestellung_k47_id.Text = (int.Parse(bestellung_k47_id.Text.ToString()).ToString());
               bestellung_k48_id.Text = (int.Parse(bestellung_k48_id.Text.ToString()).ToString());
               bestellung_k52_id.Text = (int.Parse(bestellung_k52_id.Text.ToString()).ToString());
               bestellung_k53_id.Text = (int.Parse(bestellung_k53_id.Text.ToString()).ToString());
               bestellung_k57_id.Text = (int.Parse(bestellung_k57_id.Text.ToString()).ToString());
               bestellung_k58_id.Text = (int.Parse(bestellung_k58_id.Text.ToString()).ToString());
               bestellung_k59_id.Text = (int.Parse(bestellung_k59_id.Text.ToString()).ToString());

               bestellung_k21_bestand.Text = (int.Parse(bestellung_k21_bestand.Text.ToString()).ToString());
               bestellung_k22_bestand.Text = (int.Parse(bestellung_k22_bestand.Text.ToString()).ToString());
               bestellung_k23_bestand.Text = (int.Parse(bestellung_k23_bestand.Text.ToString()).ToString());
               bestellung_k24_bestand.Text = (int.Parse(bestellung_k24_bestand.Text.ToString()).ToString());
               bestellung_k25_bestand.Text = (int.Parse(bestellung_k25_bestand.Text.ToString()).ToString());
               bestellung_k27_bestand.Text = (int.Parse(bestellung_k27_bestand.Text.ToString()).ToString());
               bestellung_k28_bestand.Text = (int.Parse(bestellung_k28_bestand.Text.ToString()).ToString());
               bestellung_k32_bestand.Text = (int.Parse(bestellung_k32_bestand.Text.ToString()).ToString());
               bestellung_k33_bestand.Text = (int.Parse(bestellung_k33_bestand.Text.ToString()).ToString());
               bestellung_k34_bestand.Text = (int.Parse(bestellung_k34_bestand.Text.ToString()).ToString());
               bestellung_k35_bestand.Text = (int.Parse(bestellung_k35_bestand.Text.ToString()).ToString());
               bestellung_k36_bestand.Text = (int.Parse(bestellung_k36_bestand.Text.ToString()).ToString());
               bestellung_k37_bestand.Text = (int.Parse(bestellung_k37_bestand.Text.ToString()).ToString());
               bestellung_k38_bestand.Text = (int.Parse(bestellung_k38_bestand.Text.ToString()).ToString());
               bestellung_k39_bestand.Text = (int.Parse(bestellung_k39_bestand.Text.ToString()).ToString());
               bestellung_k40_bestand.Text = (int.Parse(bestellung_k40_bestand.Text.ToString()).ToString());
               bestellung_k41_bestand.Text = (int.Parse(bestellung_k41_bestand.Text.ToString()).ToString());
               bestellung_k42_bestand.Text = (int.Parse(bestellung_k42_bestand.Text.ToString()).ToString());
               bestellung_k43_bestand.Text = (int.Parse(bestellung_k43_bestand.Text.ToString()).ToString());
               bestellung_k44_bestand.Text = (int.Parse(bestellung_k44_bestand.Text.ToString()).ToString());
               bestellung_k45_bestand.Text = (int.Parse(bestellung_k45_bestand.Text.ToString()).ToString());
               bestellung_k46_bestand.Text = (int.Parse(bestellung_k46_bestand.Text.ToString()).ToString());
               bestellung_k47_bestand.Text = (int.Parse(bestellung_k47_bestand.Text.ToString()).ToString());
               bestellung_k48_bestand.Text = (int.Parse(bestellung_k48_bestand.Text.ToString()).ToString());
               bestellung_k52_bestand.Text = (int.Parse(bestellung_k52_bestand.Text.ToString()).ToString());
               bestellung_k53_bestand.Text = (int.Parse(bestellung_k53_bestand.Text.ToString()).ToString());
               bestellung_k57_bestand.Text = (int.Parse(bestellung_k57_bestand.Text.ToString()).ToString());
               bestellung_k58_bestand.Text = (int.Parse(bestellung_k58_bestand.Text.ToString()).ToString());
               bestellung_k59_bestand.Text = (int.Parse(bestellung_k59_bestand.Text.ToString()).ToString());
                             
               DaoHelper dao = new DaoHelper();
               List<Teil> teilListe = new List<Teil>();
               teilListe = dao.getTeilStammdaten();
             
               foreach(Teil teil in teilListe)
               {
                   string name = teil.getBezeichnung(); 
                   int nummer = teil.getNummer();
                   int lagerbestand = xmlData.warehouseStock.article[nummer-1].amount;

                   //TODO: Decimalzahl wird nicht übertragen von XML eingelesen -> immer null
                   decimal prozent = xmlData.warehouseStock.article[nummer-1].pct;

                   //TODO: Warteschlange wird eventuell nicht bei allen gelesen!
                   int warteschlange = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(nummer);
                   int bearbeitung = xmlData.ordersInWork.getInBearbeitungMengeByItem(nummer);
                   

                              
                   string[] row = { nummer.ToString(), name, lagerbestand.ToString(), prozent.ToString(), warteschlange.ToString(), bearbeitung.ToString()};
                   pp_uebersicht_grid.Rows.Add(row);           
               }

               dataGridView_kp_uebersicht.Rows.Add("1", "1", "1", "1", "1", "1", "50%", true, true, "1");

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

       private void pp_p1_p1_sb_TextChanged(object sender, EventArgs e)
       {

       }

       private void pp_p1_p1_vw_TextChanged(object sender, EventArgs e)
       {

       }

       private void dataGridView_best_kaufteileverbrauch_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {
           dataGridView_best_kaufteileverbrauch.ScrollBars = ScrollBars.Both; 
       } 
    }
}
