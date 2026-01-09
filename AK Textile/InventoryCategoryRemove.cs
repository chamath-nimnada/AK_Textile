using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class InventoryCategoryRemove : Form
    {
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        private InventoryCategory inventoryCategoryForm; // Reference to Inventory Category
        public InventoryCategoryRemove(InventoryCategory inventoryCategoryForm)
        {
            InitializeComponent();
            this.inventoryCategoryForm = inventoryCategoryForm;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                {
                    connection.Open();

                    // SQL Query to search data
                    string query = "SELECT * FROM InventoryCategory WHERE InvCatID LIKE @search OR InvCategory LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text.Trim() + "%");

                        // Use SqlDataAdapter to fetch and display data
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Display the result in DataGridView
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                // Get the CategoryID of the selected row (as a string)
                string selectedCategoryID = dataGridView1.SelectedRows[0].Cells["InvCatID"].Value.ToString();

                try
                {
                    using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                    {
                        connection.Open();

                        // SQL Query to delete data
                        string deleteQuery = "DELETE FROM InventoryCategory WHERE InvCatID = @InvCatID";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@InvCatID", selectedCategoryID);

                            // Execute the delete command
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh DataGridView after deletion
                                button9.PerformClick();

                                // Call the public method from InventoryCategory
                                inventoryCategoryForm.RefreshDataGrid();

                                this.Close();
                            }
                            else
                            {
                                //MessageBox.Show("Failed to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the text box
            txtSearch.Text = string.Empty;

            // Clear the data grid view
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Call the public method from InventoryCategory
            inventoryCategoryForm.RefreshDataGrid(); 

            this.Close();
        }

        private void InventoryCategoryRemove_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
