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
    public partial class InventoryInventoryAdd : Form
    {
        private InventoryInventory inventoryform;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        public InventoryInventoryAdd(InventoryInventory inventoryform)
        {
            InitializeComponent();
            this.inventoryform = inventoryform;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
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

            string invID = invid.Text;
            string invCatID = comboBox1.SelectedValue.ToString();
            string itemName = itemtxt.Text;
            DateTime dateAdded = dateTimePicker1.Value;
            string stockLevel = comboBox2.Text;

            try
            {
                con.Open();
                // Ensure column names match your DB schema exactly
                string query = @"INSERT INTO Inventory (InvCatID, InvID, InvItemName, InvQty, DateAdded, InvStockLevel)
                                 VALUES (@InvCatID, @InvID, @InvItemName, @InvQty, @DateAdded, @InvStockLevel)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@InvCatID", invCatID);
                cmd.Parameters.AddWithValue("@InvID", invID);
                cmd.Parameters.AddWithValue("@InvItemName", itemName);
                cmd.Parameters.AddWithValue("@InvQty", quantity);
                cmd.Parameters.AddWithValue("@DateAdded", dateAdded);
                cmd.Parameters.AddWithValue("@InvStockLevel", stockLevel);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Inventory item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                inventoryform.RefreshDataGrid();
                cleartexts();
                AutoGenerateID();
            }
            catch (Exception ex)
            {
                // Check for potential primary key violation if composite key logic is complex
                MessageBox.Show("Error adding inventory item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void cleartexts()
        {
            comboBox1.SelectedIndex = -1;
            itemtxt.Clear();
            qtytxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox2.SelectedIndex = -1;
            comboBox1.Focus();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            cleartexts();
        }

        private void InventoryInventoryAdd_Load(object sender, EventArgs e)
        {
            AutoGenerateID();
            LoadCategories();
            cleartexts(); 
        }

        //auto generates inventory ID
        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                // Find the highest numeric part of InvID across all categories
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(CAST(SUBSTRING(InvID, 4, LEN(InvID)) AS INT)) FROM Inventory WHERE InvID LIKE 'INV%'", con);
                object result = cmd1.ExecuteScalar(); // Use ExecuteScalar for single value

                if (result == DBNull.Value || result == null)
                {
                    invid.Text = "INV001";
                }
                else
                {
                    int numericPart = Convert.ToInt32(result);
                    string newID = "INV" + (numericPart + 1).ToString("D3"); // Increment and format
                    invid.Text = newID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Inventory ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                invid.Text = "INV-ERR";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //  Loads Inventory Categories into the ComboBox 
        private void LoadCategories()
        {
            try
            {
                string query = "SELECT InvCatID, InvCategory FROM InventoryCategory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);

                // Configure the Category ComboBox
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



    }
}
