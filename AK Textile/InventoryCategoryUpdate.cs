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
    public partial class InventoryCategoryUpdate : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private InventoryCategory inventoryCategoryForm; // Reference to Inventory Category
        public InventoryCategoryUpdate(InventoryCategory inventoryCategoryForm)
        {
            InitializeComponent();
            this.inventoryCategoryForm = inventoryCategoryForm;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Get the search keyword from textBox1
            string searchKeyword = textBox1.Text.Trim();

            // Check if the input is empty
            if (string.IsNullOrEmpty(searchKeyword))
            {
                MessageBox.Show("Please enter an Item ID or Category Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // SQL query to search based on InvCatID or InvCategory
                string query = "SELECT InvCategory FROM InventoryCategory WHERE InvCatID = @SearchKeyword OR InvCategory LIKE @SearchKeywordPattern";

                using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL Injection
                        cmd.Parameters.AddWithValue("@SearchKeyword", searchKeyword);
                        cmd.Parameters.AddWithValue("@SearchKeywordPattern", "%" + searchKeyword + "%");

                        connection.Open();

                        // Execute the query
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Show the result in textBox2
                            textBox3.Text = result.ToString();
                        }
                        else
                        {
                            // Show a message if no data is found
                            MessageBox.Show("No matching inventory category found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox3.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Ensure textBox2 and textBox3 are not empty
            if (string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                MessageBox.Show("Please search for a record first before updating.", "Search Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                MessageBox.Show("Please enter the new category name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Your SQL query to update the InvCategory
                string query = "UPDATE InventoryCategory SET InvCategory = @NewInvCategory WHERE InvCategory = @OldInvCategory";

                using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to prevent SQL Injection
                        cmd.Parameters.AddWithValue("@NewInvCategory", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@OldInvCategory", textBox3.Text.Trim());

                        con.Open();

                        // Execute the UPDATE query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Update textBox2 with the new category name and clear textBox3
                            textBox3.Text = textBox3.Text.Trim();
                            textBox2.Clear();

                            // Call the public method from InventoryCategory
                            inventoryCategoryForm.RefreshDataGrid();
                    }
                        else
                        {
                            MessageBox.Show("No record was updated. Please check the data.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the contents of textBox1 and textBox3
            textBox1.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Call the public method from InventoryCategory
            inventoryCategoryForm.RefreshDataGrid();

            this.Close();
        }

        private void InventoryCategoryUpdate_Load(object sender, EventArgs e)
        {

        }
    }
}
