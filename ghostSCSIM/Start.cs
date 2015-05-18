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
               pp_p1_p1_lager.Text = xmlData.warehouseStock.article[0].amount.ToString();
               pp_p1_26_lager.Text = xmlData.warehouseStock.article[25].amount.ToString();
               pp_p1_51_lager.Text = xmlData.warehouseStock.article[50].amount.ToString();
               pp_p1_16_lager.Text = xmlData.warehouseStock.article[15].amount.ToString();
               pp_p1_17_lager.Text = xmlData.warehouseStock.article[16].amount.ToString();
               pp_p1_50_lager.Text = xmlData.warehouseStock.article[49].amount.ToString();
               pp_p1_4_lager.Text = xmlData.warehouseStock.article[3].amount.ToString();
               pp_p1_10_lager.Text = xmlData.warehouseStock.article[9].amount.ToString();
               pp_p1_49_lager.Text = xmlData.warehouseStock.article[48].amount.ToString();
               pp_p1_7_lager.Text = xmlData.warehouseStock.article[6].amount.ToString();
               pp_p1_13_lager.Text = xmlData.warehouseStock.article[12].amount.ToString();
               pp_p1_18_lager.Text = xmlData.warehouseStock.article[18].amount.ToString();

               pp_p2_p2_lager.Text = xmlData.warehouseStock.article[1].amount.ToString();
               pp_p2_26_lager.Text = xmlData.warehouseStock.article[25].amount.ToString();
               pp_p2_56_lager.Text = xmlData.warehouseStock.article[55].amount.ToString();
               pp_p2_16_lager.Text = xmlData.warehouseStock.article[15].amount.ToString();
               pp_p2_17_lager.Text = xmlData.warehouseStock.article[16].amount.ToString();
               pp_p2_55_lager.Text = xmlData.warehouseStock.article[54].amount.ToString();
               pp_p2_5_lager.Text = xmlData.warehouseStock.article[4].amount.ToString();
               pp_p2_11_lager.Text = xmlData.warehouseStock.article[10].amount.ToString();
               pp_p2_54_lager.Text = xmlData.warehouseStock.article[53].amount.ToString();
               pp_p2_8_lager.Text = xmlData.warehouseStock.article[7].amount.ToString();
               pp_p2_14_lager.Text = xmlData.warehouseStock.article[13].amount.ToString();
               pp_p2_19_lager.Text = xmlData.warehouseStock.article[18].amount.ToString();

               pp_p3_p3_lager.Text = xmlData.warehouseStock.article[2].amount.ToString();
               pp_p3_26_lager.Text = xmlData.warehouseStock.article[25].amount.ToString();
               pp_p3_31_lager.Text = xmlData.warehouseStock.article[30].amount.ToString();
               pp_p3_16_lager.Text = xmlData.warehouseStock.article[15].amount.ToString();
               pp_p3_17_lager.Text = xmlData.warehouseStock.article[16].amount.ToString();
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


              

               //Vertriebswünsche 
               pp_p1_p1_vw.Text = kinder_prog_p1.Value.ToString();
               pp_p2_p2_vw.Text = damen_prog_p1.Value.ToString();
               pp_p3_p3_vw.Text = herren_prog_p1.Value.ToString();

               //Produktion 
               pp_p1_p1_prod.Text = (int.Parse(pp_p1_p1_vw.Text.ToString()) + int.Parse(pp_p1_p1_sb.Text.ToString()) - int.Parse(pp_p1_p1_lager.Text.ToString()) - int.Parse(pp_p1_p1_ws.Text.ToString()) - int.Parse(pp_p1_p1_bearb.Text.ToString())).ToString();
               

                        
                   
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
    }
}
