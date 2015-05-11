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

namespace ghostSCSIM
{
    public partial class Start : Form
    {
        // Neuen Datenbehälter für den XML Input anlegen
        DataContainer xmlData = null;

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
           xmlData = new DataContainer();
           var fileDialog = new OpenFileDialog { };
           if (fileDialog.ShowDialog() == DialogResult.OK)
           {
               XmlIO xmlInput = new XmlIO(fileDialog.FileName);

               xmlData = (DataContainer)xmlInput.xml;

               if (xmlData != null)
               {
                   MessageBox.Show("XML Datei: " + fileDialog.FileName + " erfolgreich importiert!");
               }
           }
       }
       private void fillFormsWithData(object sender, EventArgs e)
       {
           if (xmlData != null)
           {
               pp_p1_lager.Text = xmlData.warehouseStock.article[0].amount.ToString();
           }
         
           
          
       }

       

        
       

      

      
    }
}
