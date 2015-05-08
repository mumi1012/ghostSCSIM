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

namespace ghostSCSIM
{
    public partial class Form1 : Form
    {
        // Neuen Datenbehälter für den XML Input anlegen
        DataContainer xmlData = new DataContainer();

        public Form1()
        {
            InitializeComponent();
            
        }
        //Datenbehälter mit den XML Daten füllen
        private void button1_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog { };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlIO xmlInput = new XmlIO(fileDialog.FileName);
                
                xmlData = (DataContainer)xmlInput.xml;
                
               
               
                
                
            }
            
           
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog { };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlIO xmlInput = new XmlIO(fileDialog.FileName);

                xmlData = (DataContainer)xmlInput.xml;

            }
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
                ComponentResourceManager resourcesManager = new ComponentResourceManager(typeof(Form1));
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

              

       private void changeLabelTextAndVisibility(String clickedItem) 
        {
            if (dataGridLabel.Visible == false) 
                dataGridLabel.Visible = true;

            dataGridLabel.Text = clickedItem;
        }

       private void warehouseStockToolStripMenuItem_Click(object sender, EventArgs e)
       {
           changeLabelTextAndVisibility(this.warehouseStockToolStripMenuItem.Text);
           dataGridView1.DataSource = xmlData.warehouseStock.article;
           
           
       }

       private void ordersInWorkToolStripMenuItem_Click_1(object sender, EventArgs e)
       {
           changeLabelTextAndVisibility(this.ordersInWorkToolStripMenuItem.Text);
           dataGridView1.DataSource = xmlData.ordersInWork.workplace;
       }

       private void futureInwardStockMovementToolStripMenuItem_Click_1(object sender, EventArgs e)
       {
           changeLabelTextAndVisibility(this.futureInwardStockMovementToolStripMenuItem.Text);
           dataGridView1.DataSource = xmlData.futureInwardStockMovement.orders;
       }

       private void waitingListToolStripMenuItem_Click(object sender, EventArgs e)
       {
           changeLabelTextAndVisibility(this.waitingListToolStripMenuItem.Text);
           dataGridView1.DataSource = xmlData.waitingListWorkstations.workplaces;
       }
         
       

        
       

      

      
    }
}
