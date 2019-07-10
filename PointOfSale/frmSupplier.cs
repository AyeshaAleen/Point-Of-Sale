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
using System.Data;
namespace PointOfSale
{
    public partial class frmSupplier : Form
    {
        public frmSupplier()
        {
            InitializeComponent();
        }
        private void GetNextCode()
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = " select isnull(max(Code),0)+ 1 as NextCode from Supplier";
            object obj = cmd.ExecuteScalar();
            this.txtcode.Text = obj.ToString();
            con.Close();
            cmd.Dispose();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private void frmSupplier_Load(object sender, EventArgs e)
        {
            GetNextCode();
        }
        bool isUpdate = false;
        private void btnsave_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            if (isUpdate == false)
            {
                cmd.CommandText = "Insert into Supplier (Code,Name,Address,ContactNumber) values(" + txtcode.Text + ",'" + txtname.Text + "','" + txtAddress.Text + "','" + txtContactNumber.Text + "')";

            }
            else
            {
                cmd.CommandText = "Update Supplier set Name ='" + txtname.Text + "',Address ='" + txtAddress.Text + "',ContactNumber ='" +txtContactNumber.Text + "' where code = " + txtcode.Text;
            }
            cmd.ExecuteNonQuery();
            isUpdate = false;
            MessageBox.Show("Supplier Saved Successfully");
            con.Close();
            cmd.Dispose();
            GetNextCode();
            this.txtname.Clear();
            this.txtAddress.Clear();
            this.txtContactNumber.Clear();
            txtname.Focus();
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            frmsearchsupplier frmss = new frmsearchsupplier();
            frmss.ShowDialog();
            if(frmss.CurrentSupplier != null)
            {
                txtcode.Text = frmss.CurrentSupplier.code.ToString();
                txtname.Text = frmss.CurrentSupplier.name;
                txtAddress.Text = frmss.CurrentSupplier.Address;
                txtContactNumber.Text = frmss.CurrentSupplier.ContactNumber;
                isUpdate = true;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Supplier where code =" + txtcode.Text;
            cmd.ExecuteNonQuery();
            isUpdate = false;
            MessageBox.Show("Supplier Deleted Successfully");
            con.Close();
            cmd.Dispose();
            GetNextCode();
            this.txtname.Clear();
            this.txtAddress.Clear();
            this.txtContactNumber.Clear();
            txtname.Focus();
        }
    }
}
