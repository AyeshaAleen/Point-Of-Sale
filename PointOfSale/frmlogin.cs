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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if(txtusername.Text == "NFCIET" && txtpassword.Text == "BSCS")
            {
                frmMainScreen frms = new frmMainScreen();
                this.Hide();
                frms.ShowDialog();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Invalid User Name and Password");
                    this.txtusername.Focus();
            }
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {

        }
    }
}
