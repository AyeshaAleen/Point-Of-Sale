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
    public partial class frmSeachProduct : Form
    {
        public frmSeachProduct()
        {
            InitializeComponent();
        }
        public Product CurrentProduct { get; set; }
        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CurrentProduct = new Product();
            CurrentProduct.Code = Convert.ToInt32(this.dgvProduct.Rows[this.dgvProduct.CurrentCell.RowIndex].Cells["Code"].Value);
            CurrentProduct.Name = this.dgvProduct.Rows[this.dgvProduct.CurrentCell.RowIndex].Cells["Name"].Value.ToString();
            CurrentProduct.UnitOfMeasure = this.dgvProduct.Rows[this.dgvProduct.CurrentCell.RowIndex].Cells["UnitOfMeasure"].Value.ToString();
            CurrentProduct.Price = Convert.ToDecimal(this.dgvProduct.Rows[this.dgvProduct.CurrentCell.RowIndex].Cells["Price"].Value);
            CurrentProduct.Category = new Category();
            CurrentProduct.Category.Code = Convert.ToInt32(this.dgvProduct.Rows[this.dgvProduct.CurrentCell.RowIndex].Cells["Code"].Value);
            this.DialogResult = DialogResult.OK;
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        private void frmSeachProduct_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Product";
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            dgvProduct.DataSource = ds.Tables[0];
            con.Close();
            cmd.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearchProductName_TextChanged(object sender, EventArgs e)
        {
            var dt = ds.Tables[0].Select("Name like'" + txtSearchProductName.Text + "%'");
            DataTable dts = ds.Tables[0].Clone();
            foreach (DataRow item in dt)
            {
                object[] values = { item[0], item[1], item[2] };
                dts.Rows.Add(values);
            }
            this.dgvProduct.DataSource = null;
            this.dgvProduct.DataSource = dts;
        }
    }
}
