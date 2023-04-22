using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace ResultSys
{
    public partial class frmsetup : Form
    {
        public frmsetup()
        {
            InitializeComponent();
            set_portNO_to_combobox();
            txtboxTimetoNext.Text = Form1.get_interval_2_next_race().ToString();
            txtboxTimetoErase.Text = Form1.get_interval_2_next_race().ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Form1.set_interval_2_next_race( Int32.Parse( txtboxTimetoNext.Text));
            Form1.set_lap_alive_time( Int32.Parse(txtboxTimetoErase.Text) );

            Form1.comPort = this.cmbBox.Text;


            this.Close();
        }
        private void set_portNO_to_combobox()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbBox.Items.Add(s);
            }

        }


    }
}
