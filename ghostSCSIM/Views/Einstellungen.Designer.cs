namespace ghostSCSIM.Views
{
    partial class Einstellungen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Einstellungen));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_lieferrisiko = new System.Windows.Forms.TabPage();
            this.lbl_lieferrisiko = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_save_lrisiko = new System.Windows.Forms.Button();
            this.radio_affin = new System.Windows.Forms.RadioButton();
            this.radio_neutral = new System.Windows.Forms.RadioButton();
            this.radio_avers = new System.Windows.Forms.RadioButton();
            this.lbl_lieferrisko_text = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tab_lieferrisiko.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tab_lieferrisiko);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tab_lieferrisiko
            // 
            resources.ApplyResources(this.tab_lieferrisiko, "tab_lieferrisiko");
            this.tab_lieferrisiko.Controls.Add(this.lbl_lieferrisiko);
            this.tab_lieferrisiko.Controls.Add(this.panel1);
            this.tab_lieferrisiko.Controls.Add(this.lbl_lieferrisko_text);
            this.tab_lieferrisiko.Name = "tab_lieferrisiko";
            this.tab_lieferrisiko.UseVisualStyleBackColor = true;
            // 
            // lbl_lieferrisiko
            // 
            resources.ApplyResources(this.lbl_lieferrisiko, "lbl_lieferrisiko");
            this.lbl_lieferrisiko.Name = "lbl_lieferrisiko";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btn_save_lrisiko);
            this.panel1.Controls.Add(this.radio_affin);
            this.panel1.Controls.Add(this.radio_neutral);
            this.panel1.Controls.Add(this.radio_avers);
            this.panel1.Name = "panel1";
            // 
            // btn_save_lrisiko
            // 
            resources.ApplyResources(this.btn_save_lrisiko, "btn_save_lrisiko");
            this.btn_save_lrisiko.Name = "btn_save_lrisiko";
            this.btn_save_lrisiko.UseVisualStyleBackColor = true;
            this.btn_save_lrisiko.Click += new System.EventHandler(this.btn_save_lrisiko_Click);
            // 
            // radio_affin
            // 
            resources.ApplyResources(this.radio_affin, "radio_affin");
            this.radio_affin.Name = "radio_affin";
            this.radio_affin.TabStop = true;
            this.radio_affin.UseVisualStyleBackColor = true;
            // 
            // radio_neutral
            // 
            resources.ApplyResources(this.radio_neutral, "radio_neutral");
            this.radio_neutral.Name = "radio_neutral";
            this.radio_neutral.TabStop = true;
            this.radio_neutral.UseVisualStyleBackColor = true;
            // 
            // radio_avers
            // 
            resources.ApplyResources(this.radio_avers, "radio_avers");
            this.radio_avers.Name = "radio_avers";
            this.radio_avers.TabStop = true;
            this.radio_avers.UseVisualStyleBackColor = true;
            // 
            // lbl_lieferrisko_text
            // 
            resources.ApplyResources(this.lbl_lieferrisko_text, "lbl_lieferrisko_text");
            this.lbl_lieferrisko_text.Name = "lbl_lieferrisko_text";
            // 
            // Einstellungen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "Einstellungen";
            this.tabControl1.ResumeLayout(false);
            this.tab_lieferrisiko.ResumeLayout(false);
            this.tab_lieferrisiko.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_lieferrisiko;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radio_neutral;
        private System.Windows.Forms.RadioButton radio_avers;
        private System.Windows.Forms.Label lbl_lieferrisko_text;
        private System.Windows.Forms.RadioButton radio_affin;
        private System.Windows.Forms.Button btn_save_lrisiko;
        private System.Windows.Forms.Label lbl_lieferrisiko;
    }
}