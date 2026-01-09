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
    public partial class InventoryInventoryUpdate : Form
    {
        private InventoryInventory inventoryform;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        private string currentInvID = null;
        private string originalInvCatID = null;

        public InventoryInventoryUpdate(InventoryInventory inventoryform)
        {
            InitializeComponent();
            this.inventoryform = inventoryform;
        }

        private void ClearDetails()
        {
            comboBox1.SelectedIndex = -1;
            itemtxt.Clear();
            qtytxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox2.SelectedIndex = -1;
            currentInvID = null;
            originalInvCatID = null;
            itemtxt.Focus();
        }
        private void LoadCategories()
        {
            try
            {
                string query = "SELECT InvCatID, InvCategory FROM InventoryCategory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);

                comboBox1.DataSource = categoryTable;
                comboBox1.DisplayMember = "InvCategory";
                comboBox1.ValueMember = "InvCatID";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load inventory categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InventoryInventoryUpdate_Load(object sender, EventArgs e)
        {
            LoadCategories();
            ClearDetails();
        }


        //search button
        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchID = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchID))
            {
                MessageBox.Show("Please enter an Inventory ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                string query = "SELECT InvCatID, InvItemName, InvQty, DateAdded, InvStockLevel FROM Inventory WHERE InvID = @InvID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@InvID", searchID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    currentInvID = searchID;
                    originalInvCatID = reader["InvCatID"].ToString();

                    // Populate fields
                    itemtxt.Text = reader["InvItemName"].ToString();
                    qtytxt.Text = reader["InvQty"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateAdded"]);
                    comboBox2.Text = reader["InvStockLevel"].ToString(); 

                    // Set the category dropdown
                    comboBox1.SelectedValue = originalInvCatID;
                }
                else
                {
                    MessageBox.Show("Inventory item with that ID not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearDetails(); // Clear fields if not found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching inventory: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearDetails();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        //search clear
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        //form clear
        private void clearbtn_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (currentInvID == null)
            {
                MessageBox.Show("Please search for and load an inventory item first.", "No Item Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select an inventory category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(itemtxt.Text))
            {
                MessageBox.Show("Please enter an item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(qtytxt.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Quantity must be a valid non-negative whole number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a stock level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newInvCatID = comboBox1.SelectedValue.ToString();
            string itemName = itemtxt.Text;
            DateTime dateAdded = dateTimePicker1.Value;
            string stockLevel = comboBox2.Text;

            try
            {
                con.Open();
                string query = @"UPDATE Inventory
                                 SET InvCatID = @NewInvCatID,
                                     InvItemName = @InvItemName,
                                     InvQty = @InvQty,
                                     DateAdded = @DateAdded,
                                     InvStockLevel = @InvStockLevel
                                 WHERE InvID = @CurrentInvID AND InvCatID = @OriginalInvCatID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@NewInvCatID", newInvCatID);
                cmd.Parameters.AddWithValue("@InvItemName", itemName);
                cmd.Parameters.AddWithValue("@InvQty", quantity);
                cmd.Parameters.AddWithValue("@DateAdded", dateAdded);
                cmd.Parameters.AddWithValue("@InvStockLevel", stockLevel);
                cmd.Parameters.AddWithValue("@CurrentInvID", currentInvID);
                cmd.Parameters.AddWithValue("@OriginalInvCatID", originalInvCatID);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Inventory item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inventoryform.RefreshDataGrid();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update failed. Item not found or no changes made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating inventory item: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
