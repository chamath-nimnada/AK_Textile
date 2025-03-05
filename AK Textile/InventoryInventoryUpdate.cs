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
    public partial class InventoryInventoryUpdate : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;
                                        Initial Catalog=AKTextilesDB;
                                        Integrated Security=True;");

        private InventoryInventory inventoryInventoryForm;

        public InventoryInventoryUpdate(InventoryInventory inventoryInventoryForm)
        {
            InitializeComponent();
            this.inventoryInventoryForm = inventoryInventoryForm;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            textSearch.Clear();
            invCategory.SelectedIndex = -1;
            itemName.Clear();
            itemQty.Clear();
            dateTimePicker.Value = DateTime.Now;
            stkLevel.SelectedIndex = -1;
        }

        private void LoadCategories()
        {
            try
            {
                {
                    string query = "SELECT InvCatID, InvCategory FROM InventoryCategory";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        con.Open();
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        invCategory.DataSource = dt;
                        invCategory.DisplayMember = "InvCategory";  // Show category names
                        invCategory.ValueMember = "InvCatID";  // Store category IDs internally
                        invCategory.SelectedIndex = -1;  // Default to "no selection"
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSearch.Text))
            {
                MessageBox.Show("Please enter an Inventory ID.");
                return;
            }

            try
            {
                {
                    string query = "SELECT InvCatID, InvItemName, InvQty, DateAdded, InvStockLevel FROM Inventory WHERE InvID = @InvID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InvID", textSearch.Text);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read()) // If a record is found
                        {
                            //invCategory.SelectedValue = reader["InvCatID"]; // Set Category ID
                            itemName.Text = reader["InvItemName"].ToString();
                            itemQty.Text = reader["InvQty"].ToString();
                            dateTimePicker.Value = Convert.ToDateTime(reader["DateAdded"]);
                            stkLevel.SelectedItem = reader["InvStockLevel"].ToString();
                            con.Close();

                            LoadCategories();
                        }
                        else
                        {
                            MessageBox.Show("No record found for this Inventory ID.");
                            con.Close();
                            ClearFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InventoryInventoryUpdate_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSearch.Text) ||
                        invCategory.SelectedIndex == -1 ||
                        string.IsNullOrWhiteSpace(itemName.Text) ||
                        string.IsNullOrWhiteSpace(itemQty.Text) ||
                        stkLevel.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields before updating.");
                return;
            }

            if (!int.TryParse(itemQty.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid numeric quantity.");
                return;
            }

            try
            {
                {
                    string query = "UPDATE Inventory SET InvCatID = @InvCatID, InvItemName = @InvItemName, " +
                                   "InvQty = @InvQty, DateAdded = @DateAdded, InvStockLevel = @InvStockLevel " +
                                   "WHERE InvID = @InvID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InvCatID", invCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@InvItemName", itemName.Text);
                        cmd.Parameters.AddWithValue("@InvQty", quantity);
                        cmd.Parameters.AddWithValue("@DateAdded", dateTimePicker.Value);
                        cmd.Parameters.AddWithValue("@InvStockLevel", stkLevel.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@InvID", textSearch.Text);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.");
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Check the Inventory ID.");
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
