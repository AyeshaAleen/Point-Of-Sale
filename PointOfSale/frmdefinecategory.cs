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
    public partial class frmdefinecategory : Form
    {
        public frmdefinecategory()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            t.BackColor = Color.Yellow;
            t.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            t.BackColor = Color.White;
            t.ForeColor = Color.Black;
        }

        private void frmdefinecategory_Load(object sender, EventArgs e)
        {
            GetNextCode();

        }
        SqlConnection con;
        SqlCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            if(isUpdate == false)
            {
                cmd.CommandText = "Insert into Category (Code,Name,Description) values(" + txtcode.Text + ",'" + txtname.Text + "','" + txtdescription.Text + "')";

            }
            else
            {
                cmd.CommandText = "Update Category set Name ='"+ txtname.Text + "',Description ='" + txtdescription.Text + "' where code = " + txtcode.Text;
            }
            cmd.ExecuteNonQuery();
            isUpdate = false;
            MessageBox.Show("Category Saved Successfully");
            con.Close();
            cmd.Dispose();
            GetNextCode();
            this.txtname.Clear();
            this.txtdescription.Clear();
            txtname.Focus();
        }
        private void GetNextCode()
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = " select isnull(max(Code),0)+ 1 as NextCode from Category";
            object obj = cmd.ExecuteScalar();
            this.txtcode.Text = obj.ToString();
            con.Close();
            cmd.Dispose();
        }
        bool isUpdate = false;
        private void btnsearch_Click(object sender, EventArgs e)
        {
            frmSearchCategory frmsc = new frmSearchCategory();
            frmsc.ShowDialog();
            if(frmsc.DialogResult == DialogResult.OK)
            {
                this.txtcode.Text = frmsc.CurrentCategory.Code.ToString();
                this.txtname.Text = frmsc.CurrentCategory.Name;
                this.txtdescription.Text = frmsc.CurrentCategory.Description;
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
            cmd.CommandText = "Delete from Category where code =" + txtcode.Text;
            cmd.ExecuteNonQuery();
            isUpdate = false;
            MessageBox.Show("Category Deleted Successfully");
            con.Close();
            cmd.Dispose();
            GetNextCode();
            this.txtname.Clear();
            this.txtdescription.Clear();
            txtname.Focus();

        }
    }
}
