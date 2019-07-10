using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PointOfSale
{
    public partial class frmproduct : Form
    {
        public frmproduct()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private void frmproduct_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Category";
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            cbxCategory.DisplayMember = "Name";
            cbxCategory.ValueMember = "Code";
            cbxCategory.DataSource = ds.Tables[0];
            con.Close();
            cmd.Dispose();
            GetNextCode();
        }
        private void GetNextCode()
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select isnull(max(code),0)+1 as NextCode from Product";
            object obj = cmd.ExecuteScalar();
            this.txtCode.Text = obj.ToString();
            con.Close();
            cmd.Dispose();
        }
        bool isUpdate = false;
        private void tsbSave_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            if (isUpdate == false)
            {
                cmd.CommandText = "insert into Product(Code,Name,Category,Price,UnitOfMeasure) values(" + txtCode.Text + ",'" +
                txtName.Text + "'," + cbxCategory.SelectedValue + "," + txtPrice.Text + ",'" + txtUnitOfMeasure.Text + "')";
            }
            else
            {
                cmd.CommandText = "Update Product set Name='" + txtName.Text + "',UnitOfMeasure='" + txtUnitOfMeasure.Text + "',Price=" +
                   txtPrice.Text + ",Category=" + cbxCategory.SelectedValue + " where code=" + txtCode.Text;
            }

            cmd.ExecuteNonQuery();
            isUpdate = false;
            MessageBox.Show("Product Saved Successfully");
            con.Close();
            cmd.Dispose();
            GetNextCode();
            this.txtName.Clear();
            this.txtPrice.Clear();
            this.txtUnitOfMeasure.Clear();
            txtName.Focus();
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            frmSeachProduct frmsp = new frmSeachProduct();
            frmsp.ShowDialog();
            if(frmsp.CurrentProduct != null)
            {
                txtCode.Text = frmsp.CurrentProduct.Code.ToString();
                txtName.Text = frmsp.CurrentProduct.Name;
                txtPrice.Text = frmsp.CurrentProduct.Price.ToString();
                txtUnitOfMeasure.Text = frmsp.CurrentProduct.UnitOfMeasure;
                cbxCategory.SelectedValue = frmsp.CurrentProduct.Category.Code;
                isUpdate = true;
            }

        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Product where code =" + txtCode.Text;
            cmd.ExecuteNonQuery();
            isUpdate = false;
            MessageBox.Show("Product Deleted Successfully");
            con.Close();
            cmd.Dispose();
            GetNextCode();
            this.txtName.Clear();
            this.txtPrice.Clear();
            this.txtUnitOfMeasure.Clear();
            txtName.Focus();
        }
    }
}
