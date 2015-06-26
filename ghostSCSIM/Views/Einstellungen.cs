using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ghostSCSIM.Views
{
    public partial class Einstellungen : Form
    {
        public Einstellungen()
        {
            InitializeComponent();
            this.radio_neutral.Checked = true;
        }

        private void btn_save_lrisiko_Click(object sender, EventArgs e)
        {
            if (this.radio_avers.Checked == true)
            {
                Start.lieferRisiko = 50;
            }

            if (this.radio_neutral.Checked == true)
            {
                Start.lieferRisiko = 0;
            }

            if (this.radio_affin.Checked == true)
            {
                Start.lieferRisiko = -50;
            }
        }

        

    }
}
