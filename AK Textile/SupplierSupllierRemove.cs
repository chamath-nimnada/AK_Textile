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
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                 Initial Catalog=Textlies;
                                                 Integrated Security=True");
        public SupplierSupllierRemove()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            dataGridView1.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SupplierSupllierRemove_Load(object sender, EventArgs e)
        {
            // to load supplier data in to the datagrid view
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Supplier", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
        private void SearchSupplier(string searchValue)
        {
            //to view the searched data into the data grod view
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Supplier WHERE SupID LIKE @searchval OR SupName LIKE @searchval", con);
            cmd2.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            DataTable dt1 = new DataTable();

            try
            {
                //To get the  searched data
                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;

                if (dt1.Rows.Count == 0)
                {
                    MessageBox.Show("No Matching item Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a valid Supplier ID or name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Calling the method to search supplier invoice
            SearchSupplier(searchValue);
        }

        //method to remove dat from the supplier table
        private void RemoveSupplier(string supplierId)
        {
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("DELETE FROM Suppliers WHERE SupID = @supid", con);
                cmd3.Parameters.AddWithValue("@supid", supplierId);

                try
                {
                    int r1 = cmd3.ExecuteNonQuery();

                    if (r1 > 0)
                    {
                        MessageBox.Show($"Supplier with ID {supplierId} removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"No supplier found with ID {supplierId}.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string supplierId = row.Cells["SupID"].Value.ToString();
                    //calling the supplier remove method
                    RemoveSupplier(supplierId);
                }

                // Refresh DataGridView
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Please select a supplier to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
