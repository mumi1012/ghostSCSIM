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
                MessageBox.Show("Exception: " + ex.Message);
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
 
                //Vertriebswünsche P1 P2 P3
                pp_p1_p1_vw.Text = kinder_prog_p1.Value.ToString();   
                pp_p2_p2_vw.Text = damen_prog_p1.Value.ToString();
                pp_p3_p3_vw.Text = herren_prog_p1.Value.ToString();        
                
                //Produktion 
               
                pp_p1_p1_prod.Text = (int.Parse(pp_p1_p1_vw.Text.ToString()) + int.Parse(pp_p1_p1_sb.Text.ToString()) - int.Parse(pp_p1_p1_lager.Text.ToString()) - int.Parse(pp_p1_p1_ws.Text.ToString()) - int.Parse(pp_p1_p1_bearb.Text.ToString())).ToString();
                pp_p1_26_prod.Text = (int.Parse(pp_p1_p1_prod.Text.ToString()) + int.Parse(pp_p1_26_sb.Text.ToString()) - int.Parse(pp_p1_26_lager.Text.ToString()) - int.Parse(pp_p1_26_ws.Text.ToString()) - int.Parse(pp_p1_26_bearb.Text.ToString())).ToString();
                pp_p1_26_vw.Text = pp_p1_p1_prod.Text;               
                pp_p1_51_prod.Text = (int.Parse(pp_p1_p1_prod.Text.ToString()) + int.Parse(pp_p1_51_sb.Text.ToString()) - int.Parse(pp_p1_51_lager.Text.ToString()) - int.Parse(pp_p1_51_ws.Text.ToString()) - int.Parse(pp_p1_51_bearb.Text.ToString())).ToString();
                pp_p1_51_vw.Text = pp_p1_p1_prod.Text;
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
 
                pp_p2_p2_prod.Text = (int.Parse(pp_p2_p2_vw.Text.ToString()) + int.Parse(pp_p2_p2_sb.Text.ToString()) - int.Parse(pp_p2_p2_lager.Text.ToString()) - int.Parse(pp_p2_p2_ws.Text.ToString()) - int.Parse(pp_p2_p2_bearb.Text.ToString())).ToString();
                pp_p2_26_prod.Text = (int.Parse(pp_p2_p2_vw.Text.ToString()) + int.Parse(pp_p2_26_sb.Text.ToString()) - int.Parse(pp_p2_26_lager.Text.ToString()) - int.Parse(pp_p2_26_ws.Text.ToString()) - int.Parse(pp_p2_26_bearb.Text.ToString())).ToString();
                pp_p2_26_vw.Text = pp_p2_p2_prod.Text;
                pp_p2_56_prod.Text = (int.Parse(pp_p2_p2_vw.Text.ToString()) + int.Parse(pp_p2_56_sb.Text.ToString()) - int.Parse(pp_p2_56_lager.Text.ToString()) - int.Parse(pp_p2_56_ws.Text.ToString()) - int.Parse(pp_p2_56_bearb.Text.ToString())).ToString();
                pp_p2_56_vw.Text = pp_p2_p2_prod.Text;
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
 
                pp_p3_p3_prod.Text = (int.Parse(pp_p3_p3_vw.Text.ToString()) + int.Parse(pp_p3_p3_sb.Text.ToString()) - int.Parse(pp_p3_p3_lager.Text.ToString()) - int.Parse(pp_p3_p3_ws.Text.ToString()) - int.Parse(pp_p3_p3_bearb.Text.ToString())).ToString();
                pp_p3_26_prod.Text = (int.Parse(pp_p3_p3_vw.Text.ToString()) + int.Parse(pp_p3_26_sb.Text.ToString()) - int.Parse(pp_p3_26_lager.Text.ToString()) - int.Parse(pp_p3_26_ws.Text.ToString()) - int.Parse(pp_p3_26_bearb.Text.ToString())).ToString();
                pp_p3_26_vw.Text = pp_p3_p3_prod.Text;
                pp_p3_31_prod.Text = (int.Parse(pp_p3_p3_vw.Text.ToString()) + int.Parse(pp_p3_31_sb.Text.ToString()) - int.Parse(pp_p3_31_lager.Text.ToString()) - int.Parse(pp_p3_31_ws.Text.ToString()) - int.Parse(pp_p3_31_bearb.Text.ToString())).ToString();
                pp_p3_31_vw.Text = pp_p3_p3_prod.Text;
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
     
               

                             
                DaoHelper dao = new DaoHelper();
                List<Teil> teilListe = new List<Teil>();
                teilListe = dao.getTeilStammdaten();

                
                    foreach (Teil teil in teilListe)
                    {
                        string name = teil.getBezeichnung();
                        int nummer = teil.getNummer();
                        int lagerbestand = xmlData.warehouseStock.article[nummer - 1].amount;

                        //TODO: Decimalzahl wird nicht übertragen von XML eingelesen -> immer null
                        decimal prozent = xmlData.warehouseStock.article[nummer - 1].pct;

                        //TODO: Warteschlange wird eventuell nicht bei allen gelesen!
                        int warteschlange = xmlData.waitingListWorkstations.getWarteschlangeMengeByItem(nummer);
                        int bearbeitung = xmlData.ordersInWork.getInBearbeitungMengeByItem(nummer);



                        string[] row = { nummer.ToString(), name, lagerbestand.ToString(), prozent.ToString(), warteschlange.ToString(), bearbeitung.ToString() };
                        pp_uebersicht_grid.Rows.Add(row);

                    }
                

              //dataGridView_kp_uebersicht.Rows.Add("1", "1", "1", "1", "1", "1", "50%", true, true, "1");

              pp_p3_20_vw.Text = pp_p3_29_prod.Text;


              //Bestellung Tab
              List<Teil> teileStammdaten = dao.getTeilStammdaten();

              if (dataGridView_best_kaufteillager.Rows.Count == 0)
              {
                  foreach (Teil teil in teileStammdaten)
                  {
                      int teilenummer = teil.getNummer();

                      if (teil.getBuchstabe().ToUpper().Equals("K"))
                      {
                          String bezeichnung = teil.getBezeichnung();
                          int warehouseStockIndex = xmlData.warehouseStock.getIndexOfArticleById(teilenummer);
                          String bestand = xmlData.warehouseStock.article[warehouseStockIndex].amount.ToString();

                          TeilLieferdaten lieferdaten = dao.getTeilLieferdatenByTeilenummer(teilenummer);
                          String lieferdauerTage = lieferdaten.getWiederbeschaffungszeitTage().ToString();
                          String diskontmenge = lieferdaten.getDiskontMenge().ToString();
                          String bestellkosten = lieferdaten.getBestellkosten().ToString();

                          //Kaufteillager DataGridView befüllen
                          dataGridView_best_kaufteillager.Rows.Add(teilenummer.ToString(), bezeichnung, bestand, lieferdauerTage, diskontmenge, bestellkosten);

                          int ausstehendeBestellungen = 0;

                          foreach (Order o in xmlData.futureInwardStockMovement.orders)
                          {
                              if (o.article == teilenummer)
                              {
                                  ausstehendeBestellungen = ausstehendeBestellungen + o.amount;
                              }
                          }

                          //Kaufteilbedarf DataGridView befüllen
                          dataGridView_best_kaufteileverbrauch.Rows.Add(teilenummer.ToString(), bestand.ToString(), "bedarf n", "bedarf n+1", "bestand n+1", "bestand n+2", ausstehendeBestellungen.ToString());
                      }
                  }
              }

              //Bestellliste DataGridView
              //Eingaben validieren! Festlegen wann die Bestellung gesichert wird! > Extra Methode
              foreach (DataGridViewRow row in dataGridView_best_bestellliste.Rows)
              {
                  int teilenummer = Convert.ToInt32(row.Cells[0].Value); //hier sollten nur teilenummern von k teilen angenommen werden
                  int bestellmenge = Convert.ToInt32(row.Cells[1].Value);
                  bool eil = (DataGridViewCheckBoxCell)row.Cells[2].Value != null;
              }

              //Direktverkauf DataGridView
              //Eingaben validieren! Festlegen wann der Direktverkauf gesichert wird! > Extra Methode
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
        }
    }
}