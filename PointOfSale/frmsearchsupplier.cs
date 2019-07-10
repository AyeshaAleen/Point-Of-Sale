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
    public partial class frmsearchsupplier : Form
    {
        public frmsearchsupplier()
        {
            InitializeComponent();
        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private void frmsearchsupplier_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = DBHelper.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Supplier";
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            dgvSupplier.DataSource = ds.Tables[0];
            con.Close();
            cmd.Dispose();

        }
        public Supplier CurrentSupplier { get; set; }
        private void txtSearchSupplierName_TextChanged(object sender, EventArgs e)
        {
            var dt = ds.Tables[0].Select("Name like'" + txtSearchSupplierName.Text + "%'");
            DataTable dts = ds.Tables[0].Clone();
            foreach (DataRow item in dt)
            {
                object[] values = { item[0], item[1], item[2] };
                dts.Rows.Add(values);
            }
            this.dgvSupplier.DataSource = null;
            this.dgvSupplier.DataSource = dts;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            CurrentSupplier = new Supplier();
            CurrentSupplier.code = Convert.ToInt32(this.dgvSupplier.Rows[this.dgvSupplier.CurrentCell.RowIndex].Cells[0].Value);
            CurrentSupplier.name = this.dgvSupplier.Rows[this.dgvSupplier.CurrentCell.RowIndex].Cells[1].Value.ToString();
            CurrentSupplier.Address = this.dgvSupplier.Rows[this.dgvSupplier.CurrentCell.RowIndex].Cells[2].Value.ToString();
            CurrentSupplier.ContactNumber = this.dgvSupplier.Rows[this.dgvSupplier.CurrentCell.RowIndex].Cells[3].Value.ToString();
            this.DialogResult = DialogResult.OK;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
