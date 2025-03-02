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
    public partial class InventoryInventoryRemove : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;
                                        Initial Catalog=AKTextilesDB;
                                        Integrated Security=True;");

        private InventoryInventory inventoryInventoryForm;
        public InventoryInventoryRemove(InventoryInventory inventoryInventoryForm)
        {
            InitializeComponent();
            this.inventoryInventoryForm = inventoryInventoryForm;
        }

        private void clearAll()
        {
            // Clear the text box
            txtSearch.Text = string.Empty;

            // Clear the data grid view
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }

        private void InventoryInventoryRemove_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                {
                    connection.Open();

                    // SQL Query to search data
                    string query = "SELECT * FROM Inventory WHERE InvID LIKE @search OR InvItemName LIKE @search";

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
                string selectedID = dataGridView1.SelectedRows[0].Cells["InvID"].Value.ToString();

                try
                {
                    using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                    {
                        connection.Open();

                        // SQL Query to delete data
                        string deleteQuery = "DELETE FROM Inventory WHERE InvID = @InvID";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@InvID", selectedID);

                            // Execute the delete command
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh DataGridView after deletion
                                button9.PerformClick();

                                // Call the public method from InventoryCategory
                                inventoryInventoryForm.RefreshDataGrid();
                                clearAll();

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

        private void button1_Click(object sender, EventArgs e)
        {
            inventoryInventoryForm.RefreshDataGrid();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
