namespace ghostSCSIM
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.spracheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warehouseStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersInWorkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.futureInwardStockMovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waitingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            // 
            // dataGridLabel
            // 
            resources.ApplyResources(this.dataGridLabel, "dataGridLabel");
            this.dataGridLabel.Name = "dataGridLabel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spracheToolStripMenuItem,
            this.selectDataToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // spracheToolStripMenuItem
            // 
            this.spracheToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dEToolStripMenuItem,
            this.eNToolStripMenuItem});
            this.spracheToolStripMenuItem.Name = "spracheToolStripMenuItem";
            resources.ApplyResources(this.spracheToolStripMenuItem, "spracheToolStripMenuItem");
            // 
            // dEToolStripMenuItem
            // 
            this.dEToolStripMenuItem.Name = "dEToolStripMenuItem";
            resources.ApplyResources(this.dEToolStripMenuItem, "dEToolStripMenuItem");
            this.dEToolStripMenuItem.Click += new System.EventHandler(this.dEToolStripMenuItem_Click);
            // 
            // eNToolStripMenuItem
            // 
            this.eNToolStripMenuItem.Name = "eNToolStripMenuItem";
            resources.ApplyResources(this.eNToolStripMenuItem, "eNToolStripMenuItem");
            this.eNToolStripMenuItem.Click += new System.EventHandler(this.eNToolStripMenuItem_Click);
            // 
            // selectDataToolStripMenuItem
            // 
            this.selectDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warehouseStockToolStripMenuItem,
            this.waitingListToolStripMenuItem,
            this.ordersInWorkToolStripMenuItem,
            this.futureInwardStockMovementToolStripMenuItem});
            this.selectDataToolStripMenuItem.Name = "selectDataToolStripMenuItem";
            resources.ApplyResources(this.selectDataToolStripMenuItem, "selectDataToolStripMenuItem");
            // 
            // warehouseStockToolStripMenuItem
            // 
            this.warehouseStockToolStripMenuItem.Name = "warehouseStockToolStripMenuItem";
            resources.ApplyResources(this.warehouseStockToolStripMenuItem, "warehouseStockToolStripMenuItem");
            this.warehouseStockToolStripMenuItem.Click += new System.EventHandler(this.warehouseStockToolStripMenuItem_Click);
            // 
            // ordersInWorkToolStripMenuItem
            // 
            this.ordersInWorkToolStripMenuItem.Name = "ordersInWorkToolStripMenuItem";
            resources.ApplyResources(this.ordersInWorkToolStripMenuItem, "ordersInWorkToolStripMenuItem");
            this.ordersInWorkToolStripMenuItem.Click += new System.EventHandler(this.ordersInWorkToolStripMenuItem_Click_1);
            // 
            // futureInwardStockMovementToolStripMenuItem
            // 
            this.futureInwardStockMovementToolStripMenuItem.Name = "futureInwardStockMovementToolStripMenuItem";
            resources.ApplyResources(this.futureInwardStockMovementToolStripMenuItem, "futureInwardStockMovementToolStripMenuItem");
            this.futureInwardStockMovementToolStripMenuItem.Click += new System.EventHandler(this.futureInwardStockMovementToolStripMenuItem_Click_1);
            // 
            // waitingListToolStripMenuItem
            // 
            this.waitingListToolStripMenuItem.Name = "waitingListToolStripMenuItem";
            resources.ApplyResources(this.waitingListToolStripMenuItem, "waitingListToolStripMenuItem");
            this.waitingListToolStripMenuItem.Click += new System.EventHandler(this.waitingListToolStripMenuItem_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridLabel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label dataGridLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem spracheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warehouseStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersInWorkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem futureInwardStockMovementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waitingListToolStripMenuItem;
    }
}

