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
            this.panel1 = new System.Windows.Forms.Panel();
            this.radio_neutral = new System.Windows.Forms.RadioButton();
            this.radio_avers = new System.Windows.Forms.RadioButton();
            this.lbl_lieferrisko_text = new System.Windows.Forms.Label();
            this.tab_Kaparisiko = new System.Windows.Forms.TabPage();
            this.radio_affin = new System.Windows.Forms.RadioButton();
            this.btn_save_lrisiko = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_lieferrisiko.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_lieferrisiko);
            this.tabControl1.Controls.Add(this.tab_Kaparisiko);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 394);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_lieferrisiko
            // 
            this.tab_lieferrisiko.Controls.Add(this.panel1);
            this.tab_lieferrisiko.Controls.Add(this.lbl_lieferrisko_text);
            this.tab_lieferrisiko.Location = new System.Drawing.Point(4, 29);
            this.tab_lieferrisiko.Name = "tab_lieferrisiko";
            this.tab_lieferrisiko.Padding = new System.Windows.Forms.Padding(3);
            this.tab_lieferrisiko.Size = new System.Drawing.Size(674, 361);
            this.tab_lieferrisiko.TabIndex = 0;
            this.tab_lieferrisiko.Text = "Lieferrisiko";
            this.tab_lieferrisiko.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_save_lrisiko);
            this.panel1.Controls.Add(this.radio_affin);
            this.panel1.Controls.Add(this.radio_neutral);
            this.panel1.Controls.Add(this.radio_avers);
            this.panel1.Location = new System.Drawing.Point(21, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 162);
            this.panel1.TabIndex = 1;
            // 
            // radio_neutral
            // 
            this.radio_neutral.AutoSize = true;
            this.radio_neutral.Location = new System.Drawing.Point(23, 69);
            this.radio_neutral.Name = "radio_neutral";
            this.radio_neutral.Size = new System.Drawing.Size(126, 24);
            this.radio_neutral.TabIndex = 1;
            this.radio_neutral.TabStop = true;
            this.radio_neutral.Text = "Risikoneutral";
            this.radio_neutral.UseVisualStyleBackColor = true;
            // 
            // radio_avers
            // 
            this.radio_avers.AutoSize = true;
            this.radio_avers.Location = new System.Drawing.Point(23, 29);
            this.radio_avers.Name = "radio_avers";
            this.radio_avers.Size = new System.Drawing.Size(115, 24);
            this.radio_avers.TabIndex = 0;
            this.radio_avers.TabStop = true;
            this.radio_avers.Text = "Risikoavers";
            this.radio_avers.UseVisualStyleBackColor = true;
            // 
            // lbl_lieferrisko_text
            // 
            this.lbl_lieferrisko_text.AutoSize = true;
            this.lbl_lieferrisko_text.Location = new System.Drawing.Point(17, 17);
            this.lbl_lieferrisko_text.Name = "lbl_lieferrisko_text";
            this.lbl_lieferrisko_text.Size = new System.Drawing.Size(549, 100);
            this.lbl_lieferrisko_text.TabIndex = 0;
            this.lbl_lieferrisko_text.Text = resources.GetString("lbl_lieferrisko_text.Text");
            // 
            // tab_Kaparisiko
            // 
            this.tab_Kaparisiko.Location = new System.Drawing.Point(4, 29);
            this.tab_Kaparisiko.Name = "tab_Kaparisiko";
            this.tab_Kaparisiko.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Kaparisiko.Size = new System.Drawing.Size(674, 361);
            this.tab_Kaparisiko.TabIndex = 1;
            this.tab_Kaparisiko.Text = "Kapazitätsrisiko";
            this.tab_Kaparisiko.UseVisualStyleBackColor = true;
            // 
            // radio_affin
            // 
            this.radio_affin.AutoSize = true;
            this.radio_affin.Location = new System.Drawing.Point(23, 109);
            this.radio_affin.Name = "radio_affin";
            this.radio_affin.Size = new System.Drawing.Size(108, 24);
            this.radio_affin.TabIndex = 2;
            this.radio_affin.TabStop = true;
            this.radio_affin.Text = "Risikoaffin";
            this.radio_affin.UseVisualStyleBackColor = true;
            // 
            // btn_save_lrisiko
            // 
            this.btn_save_lrisiko.Location = new System.Drawing.Point(384, 102);
            this.btn_save_lrisiko.Name = "btn_save_lrisiko";
            this.btn_save_lrisiko.Size = new System.Drawing.Size(98, 39);
            this.btn_save_lrisiko.TabIndex = 3;
            this.btn_save_lrisiko.Text = "Speichern";
            this.btn_save_lrisiko.UseVisualStyleBackColor = true;
            this.btn_save_lrisiko.Click += new System.EventHandler(this.btn_save_lrisiko_Click);
            // 
            // Einstellungen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 418);
            this.Controls.Add(this.tabControl1);
            this.Name = "Einstellungen";
            this.Text = "Einstellungen";
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
        private System.Windows.Forms.TabPage tab_Kaparisiko;
        private System.Windows.Forms.RadioButton radio_affin;
        private System.Windows.Forms.Button btn_save_lrisiko;
    }
}