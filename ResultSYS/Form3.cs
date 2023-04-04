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
            txtboxTimetoNext.Text = Form1.interval2NextRace.ToString();
            txtboxTimetoErase.Text = Form1.lapAliveTime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.interval2NextRace =Int32.Parse( txtboxTimetoNext.Text);
            Form1.lapAliveTime =  Int32.Parse(txtboxTimetoErase.Text) ;
            this.Close();
        }
    }
}
