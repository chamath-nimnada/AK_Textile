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

namespace AK_Textile
{
    public partial class SupplierSupllierRemove : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public SupplierSupllierRemove()
        {
            InitializeComponent();
        }

        private void SupplierSupllierRemove_Load(object sender, EventArgs e)
        {

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.snametxt.Clear();
            dataGridView1.DataSource = null; // clears the datagrid view 
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            // declaring a variable for thee textbox
            string search = snametxt.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Please enter Supplier ID or Name to search.");
                return;
            }
            {
                //to select data from the database after searching a id  or name adn to fill the data grid view
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM Supplier WHERE SupID = @search OR SupName LIKE '%' + @search + '%'", con);
                ad.SelectCommand.Parameters.AddWithValue("@search", search);

                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a supplier to remove.");
                return;
            }
            //declaring a variable fo the selected data in the datagrid view
            string supID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            con.Open();
            //to delete the selected data from the data grid view after clicking remove button
            SqlCommand cmd = new SqlCommand("DELETE FROM Supplier WHERE SupID = @supid", con);
            cmd.Parameters.AddWithValue("@supid", supID);

            int r1 = cmd.ExecuteNonQuery();
            con.Close();

            if (r1 > 0)
            {
                MessageBox.Show("Supplier removed successfully.");
                /*Refresh the grid
                searchbtn_Click(sender, e);*/
            }
            else
            {
                MessageBox.Show("Failed to remove the supplier. Please try again.");
            }
        }
        //Supplier remove button eke case ekk have.
    }
}
