using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace PointOfSale
{
    public partial class frmRecordPurchase : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        public frmRecordPurchase()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        private int GetNextSerialNo(SqlConnection con, SqlTransaction trans)
        {
            int SerialNo = 1;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = trans;
            cmd.CommandText = "select isnull(max(serialno),0)+1 as NextCode from purchase_detail";
            object obj = cmd.ExecuteScalar();
            SerialNo = Convert.ToInt32(obj.ToString());
            return SerialNo;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = trans;
            cmd.CommandText = "insert into purchase(invoiceno,invoicedate,suppliercode) values ("
                + this.txtInvoiceNo.Text + ",'" + this.dtpInvoiceDate.Value.Date
                + "'," + this.txtSupplierCode.Text + ")";
            cmd.ExecuteNonQuery();
            for (int i = 0; i < dgvPurchase.Rows.Count; i++)
            {
                if (dgvPurchase.Rows[i].Cells["colProductCode"].Value != null)
                {
                    cmd.CommandText = "insert into purchase_detail(serialno,invoiceno,productcode,quantity,price) values (" + i + "," + txtInvoiceNo.Text + ","
                      + dgvPurchase.Rows[i].Cells["colProductCode"].Value + "," + dgvPurchase.Rows[i].Cells["colQuantity"].Value
                      + "," + dgvPurchase.Rows[i].Cells["colPrice"].Value + ")";
                    cmd.ExecuteNonQuery();
                }
            }
            trans.Commit();
            MessageBox.Show("Purchased Saved Successfully!!!");
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void frmRecordPurchase_Load(object sender, EventArgs e)
        {
            GetNextCode();
        }
        private void GetNextCode()
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select isnull(max(invoiceno),0)+1 as NextCode from Purchase";
            object obj = cmd.ExecuteScalar();
            this.txtInvoiceNo.Text = obj.ToString();
            con.Close();
            cmd.Dispose();
        }

        private void txtSupplierCode_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                ShowSupplier();
            }
        }

        private void btnSupplierSearch_Click(object sender, EventArgs e)
        {
            ShowSupplier();
        }
        void ShowSupplier()
        {
            frmsearchsupplier frmss = new frmsearchsupplier();
            frmss.ShowDialog();
            if(frmss.CurrentSupplier != null)
            {
                this.txtSupplierCode.Text = frmss.CurrentSupplier.code.ToString();
                this.txtSupplierName.Text = frmss.CurrentSupplier.name;
            }
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            
        }
        void ShowProduct()
        {
            frmSeachProduct frmsp = new frmSeachProduct();
            frmsp.ShowDialog();
            if(frmsp.CurrentProduct != null)
            {
                this.txtProductCode.Text = frmsp.CurrentProduct.Code.ToString();
                this.txtProductName.Text = frmsp.CurrentProduct.Name;
                this.txtPrice.Text = frmsp.CurrentProduct.Price.ToString();
                txtQuantity.Focus();
            }
        }

        private void txtProductCode_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                ShowProduct();
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ShowProduct();
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            double linetotal = 0;
            if(this.txtQuantity.Text.Trim() != string.Empty)
            {
                linetotal = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantity.Text);
            }
            txtLineTotal.Text = linetotal.ToString();
        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            double linetotal = 0;
            if (this.txtQuantity.Text.Trim() != string.Empty)
            {
                linetotal = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantity.Text);
            }
            if(this.txtDiscount.Text.Trim() != string.Empty)
            {
                linetotal = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantity.Text) - Convert.ToDouble(txtDiscount.Text);  
            }
            txtLineTotal.Text = linetotal.ToString();
        }

        private void txtLineTotal_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                object[] values = { txtProductCode.Text, txtProductName.Text, txtPrice.Text, txtQuantity.Text, txtDiscount.Text, txtLineTotal.Text };
                this.dgvPurchase.Rows.Add(values);
                txtLineTotal.Clear();
                txtDiscount.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
                txtProductCode.Clear();
                txtProductName.Clear();
                txtProductCode.Focus();
                CalculateInvoiceTotal();
            }
        }
        void CalculateInvoiceTotal()
        {
            double invoicetotal = 0;
            for(int i = 0; i < dgvPurchase.Rows.Count; i++)
            {
                if (dgvPurchase.Rows[i].Cells["colLineTotal"].Value != null)
                    invoicetotal += Convert.ToDouble(dgvPurchase.Rows[i].Cells["colLineTotal"].Value);
            }
            txtTotal.Text = invoicetotal.ToString();
        }
    }
}
