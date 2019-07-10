using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale
{
    public partial class frmsplash : Form
    {
        public frmsplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value += 20;
            this.label1.Text += ".";
            if(progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                frmlogin frm1 = new frmlogin();
                this.Hide();
                frm1.ShowDialog();
            }
        }

        private void frmsplash_Load(object sender, EventArgs e)
        {

        }
    }
}
