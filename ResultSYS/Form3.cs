using ResultSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowLaneOrder
{
    public partial class frmsetup : Form
    {
        public frmsetup()
        {
            InitializeComponent();
            txtboxTimetoNext.Text = Form1.get_interval_2_next_race().ToString();
            txtboxTimetoErase.Text = Form1.get_interval_2_next_race().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.set_interval_2_next_race( Int32.Parse( txtboxTimetoNext.Text));
            Form1.set_lap_alive_time( Int32.Parse(txtboxTimetoErase.Text) );
            this.Close();
        }
    }
}
