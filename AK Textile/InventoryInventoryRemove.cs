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
    public partial class InventoryInventoryRemove : Form
    {
        private InventoryInventory inventoryform;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");
        public InventoryInventoryRemove(InventoryInventory inventoryform)
        {
            InitializeComponent();
            this.inventoryform = inventoryform;
        }

        //search button
        private void button5_Click(object sender, EventArgs e)
        {
            string searchID = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchID))
            {
                MessageBox.Show("Please enter an Inventory ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT InvCatID, InvID, InvItemName, InvQty, DateAdded, InvStockLevel FROM Inventory WHERE InvID = @InvID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con); 
                adapter.SelectCommand.Parameters.AddWithValue("@InvID", searchID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
                }
                else
                {
                    MessageBox.Show("No inventory item found with that ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching inventory: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        //search clear
        private void button6_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            dataGridView1.DataSource = null; // Clear the grid
        }

        //remove button
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please search for and select an inventory item to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to permanently delete this inventory item?",
                                                   "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
            {
                return;
            }

            string invIDToRemove = dataGridView1.SelectedRows[0].Cells["InvID"].Value.ToString();
            string invCatIDToRemove = dataGridView1.SelectedRows[0].Cells["InvCatID"].Value.ToString();

            try
            {
                con.Open();
                string query = "DELETE FROM Inventory WHERE InvID = @InvID AND InvCatID = @InvCatID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@InvID", invIDToRemove);
                cmd.Parameters.AddWithValue("@InvCatID", invCatIDToRemove);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Inventory item removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inventoryform.RefreshDataGrid(); 
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Deletion failed. Item might have been removed already.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing inventory item: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        //cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
