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
    public partial class InventoryInventoryAdd : Form
    {
        private InventoryInventory inventoryInventoryForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;
                                        Initial Catalog=AKTextilesDB;
                                        Integrated Security=True;");

        public InventoryInventoryAdd(InventoryInventory inventoryInventoryForm)
        {
            InitializeComponent();
            AutoGenerateID();
            LoadCategories();
            this.inventoryInventoryForm = inventoryInventoryForm;
        }
        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(InvID) FROM Inventory", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    if (dr1[0] == DBNull.Value)
                    {
                        this.invID.Text = "INV001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        if (maxID.StartsWith("INV") && int.TryParse(maxID.Substring(3), out int numericPart))
                        {
                            string newID = "INV" + (numericPart + 1).ToString("D3"); // Increment and format as "INVXXX"
                            this.invID.Text = newID;
                        }
                        else
                        {
                            MessageBox.Show("Invalid ID format in database.");
                        }
                    }
                }
                dr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating ID: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void LoadCategories()
        {
            try
            {
                {
                    string query = "SELECT InvCatID, InvCategory FROM InventoryCategory";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    invCategory.DisplayMember = "InvCategory";  // Show category name
                    invCategory.ValueMember = "InvCatID";        // Store category ID
                    invCategory.DataSource = dt;
                    invCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InventoryInventoryAdd_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(invID.Text) ||
                string.IsNullOrWhiteSpace(itemName.Text) ||
                string.IsNullOrWhiteSpace(itemQty.Text) ||
                invCategory.SelectedIndex == -1 ||
                stkLevel.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            string categoryID = invCategory.SelectedValue.ToString();
            string stockLevel = stkLevel.SelectedItem.ToString();

            // Validate itemQty before conversion
            if (!int.TryParse(itemQty.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid numeric quantity.");
                return;
            }

            try
            {
                string query = "INSERT INTO Inventory (InvCatID, InvID, InvItemName, InvQty, DateAdded, InvStockLevel) " +
                               "VALUES (@InvCatID, @InvID, @InvItemName, @InvQty, @DateAdded, @InvStockLevel)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@InvCatID", categoryID);
                    cmd.Parameters.AddWithValue("@InvID", invID.Text);
                    cmd.Parameters.AddWithValue("@InvItemName", itemName.Text);
                    cmd.Parameters.AddWithValue("@InvQty", quantity); // Now it's a valid integer
                    cmd.Parameters.AddWithValue("@DateAdded", dateTimePicker.Value);
                    cmd.Parameters.AddWithValue("@InvStockLevel", stockLevel);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data added successfully.");

                    AutoGenerateID();
                    itemName.Clear();
                    itemQty.Clear();
                    dateTimePicker.Value = DateTime.Now;
                    invCategory.SelectedIndex = -1;
                    stkLevel.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inventoryInventoryForm.RefreshDataGrid();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AutoGenerateID();
            itemName.Clear();
            itemQty.Clear();
            dateTimePicker.Value = DateTime.Now;
            invCategory.SelectedIndex = -1;  // Reset category selection
            stkLevel.SelectedIndex = -1;     // Reset stock level selection
        }
    }
}
