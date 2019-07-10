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
    public partial class frmSearchCategory : Form
    {
        public frmSearchCategory()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        private void frmSearchCategory_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Category";
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            dgvCategories.DataSource = ds.Tables[0];
            con.Close();
            cmd.Dispose();
        }
        public Category CurrentCategory { get; set; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            CurrentCategory = new Category();
            CurrentCategory.Code = Convert.ToInt32(this.dgvCategories.Rows[this.dgvCategories.CurrentCell.RowIndex].Cells[0].Value);
            CurrentCategory.Name = this.dgvCategories.Rows[this.dgvCategories.CurrentCell.RowIndex].Cells[1].Value.ToString();
            CurrentCategory.Description = this.dgvCategories.Rows[this.dgvCategories.CurrentCell.RowIndex].Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtSearchCategoryName_TextChanged(object sender, EventArgs e)
        {
            var dt = ds.Tables[0].Select("Name like'" + txtSearchCategoryName.Text + "%'");
            DataTable dts = ds.Tables[0].Clone();
            foreach(DataRow item in dt)
            {
                object[] values = { item[0], item[1], item[2] };
                dts.Rows.Add(values);
            }
            this.dgvCategories.DataSource = null;
            this.dgvCategories.DataSource = dts;
        }
    }
}
