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
    public partial class frmMainScreen : Form
    {
        public frmMainScreen()
        {
            InitializeComponent();
        }

        private void aboutPointOfSaleApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmaboutpos frmsp = new frmaboutpos();
            frmsp.Show();
        }

        private void exitPointOfSaleAplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void defineCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmdefinecategory frmdc = new frmdefinecategory();
            frmdc.MdiParent = this;
            frmdc.Show();
        }

        private void defineProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmproduct frmp = new frmproduct();
            frmp.MdiParent = this;
            frmp.Show();
        }

        private void showClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmclock frmc = new frmclock();
            frmc.MdiParent = this;
            frmc.Show();
        }

        private void defineVenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSupplier frms = new frmSupplier();
            frms.Show();
        }

        private void recordPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmRecordPurchase().Show();
        }
    }
}
