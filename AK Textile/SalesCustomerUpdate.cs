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
    public partial class SalesCustomerUpdate : Form
    {
        public SalesCustomerUpdate()
        {
            InitializeComponent();
        }

        private void updatebtn_Click(object sender, EventArgs e)

        {/* --- 1. Pre-Update Checks ---
            if (string.IsNullOrEmpty(_currentCustomerID))
            {
                MessageBox.Show("Please search for a customer first before updating.", "Search Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the modified data from the input fields
            string customerName = nametxt.Text.Trim();
            string customerEmail = emailtxt.Text.Trim();
            string customerContact = contacttxt.Text.Trim();

            string streetNo = textBox5.Text.Trim();
            string streetName = textBox6.Text.Trim();
            string city = textBox7.Text.Trim();
            string customerAddress = $"{streetNo}, {streetName}, {city}".Trim();

            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(customerContact))
            {
                MessageBox.Show("Customer Name and Contact are required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- 2. Database Update Logic ---
            string query = "UPDATE Customer SET CustomerName = @Name, CustomerEmail = @Email, CustomerContact = @Contact, CustomerAddress = @Address WHERE CustomerID = @ID";

            using (SqlConnection connection = new SqlConnection(/* Your Connection String ))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters for the modified data
                command.Parameters.AddWithValue("@Name", customerName);
                command.Parameters.AddWithValue("@Email", customerEmail);
                command.Parameters.AddWithValue("@Contact", customerContact);
                command.Parameters.AddWithValue("@Address", customerAddress);

                // Use the stored ID for the WHERE clause
                command.Parameters.AddWithValue("@ID", _currentCustomerID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer details updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the form and reset the ID after a successful update
                        ClearDetailsFields();
                        _currentCustomerID = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Update failed. No record found with that ID or no changes were made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during update: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }*/
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchInput = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchInput))
            {
                MessageBox.Show("Please enter a Customer ID or Name to search.", "Search Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Query to fetch customer data
                string query = "SELECT CustomerID, CustomerName, CustomerEmail, CustomerContact, CustomerAddress FROM Customer WHERE CustomerID = @Input OR CustomerName LIKE @NameInput";

                using (SqlConnection connection = new SqlConnection(/* Your Connection String */))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Input", searchInput);
                    cmd.Parameters.AddWithValue("@NameInput", "%" + searchInput + "%");

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 1. Store the ID (Crucial for the Update button)
                            //_currentCustomerID = reader["CustomerID"].ToString();

                            // 2. Load the details into the text boxes
                            nametxt.Text = reader["CustomerName"].ToString();
                            emailtxt.Text = reader["CustomerEmail"].ToString();
                            contacttxt.Text = reader["CustomerContact"].ToString();

                            // Assuming you need a function to split the address for the form
                            // This is complex, so for simplicity, we load the full address into StreetNo for now.
                            // string fullAddress = reader["CustomerAddress"].ToString();
                            // txtStreetNo.Text = fullAddress; 
                            // You would need to implement address splitting logic here if necessary.

                            MessageBox.Show("Customer record found. Details loaded for update.", "Search Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No customer found matching the input.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //ClearDetailsFields(); // Clear if nothing is found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error during search: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            nametxt.Clear();
            emailtxt.Clear();
            contacttxt.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            //_currentCustomerID = string.Empty;
        }
    }
}
