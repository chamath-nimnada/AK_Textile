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
    public partial class SalesCustomerAdd : Form
    {
        public SalesCustomerAdd()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void nametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            // --- 1. Get Input and Combine Address Fields ---
            string customerID = cusidtxt.Text.Trim();
            string customerName = nametxt.Text.Trim();
            string customerEmail = emailtxt.Text.Trim();
            string customerContact = contacttxt.Text.Trim();

            // Combine address parts into a single string for the database, if needed
            string streetNo = textBox5.Text.Trim();
            string streetName = textBox6.Text.Trim();
            string city = textBox7.Text.Trim();

            // Create a full address string
            string customerAddress = $"{streetNo}, {streetName}, {city}".Trim();

            // --- 2. Validation Checks ---
            if (string.IsNullOrEmpty(customerID) || string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(customerContact))
            {
                MessageBox.Show("Please fill in the Customer ID, Name, and Contact fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- 3. Database Insertion Logic ---
            // Note: Adjust the table and column names (Customer, CustomerID, Name, Email, Contact, Address) 
            // to match your actual database schema.
            string query = "INSERT INTO Customer (CustomerID, Name, Email, Contact, Address) " +
                           "VALUES (@ID, @Name, @Email, @Contact, @Address)";

            // Assuming 'con' is a globally available and initialized SqlConnection object
            using (SqlConnection connection = new SqlConnection(/* Your Connection String Here or use 'con' */))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@ID", customerID);
                command.Parameters.AddWithValue("@Name", customerName);
                command.Parameters.AddWithValue("@Email", customerEmail);
                command.Parameters.AddWithValue("@Contact", customerContact);
                command.Parameters.AddWithValue("@Address", customerAddress);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Customer details added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to add customer details. The Customer ID may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Ensure the connection is always closed
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            cusidtxt.Clear();
            nametxt.Clear();
            emailtxt.Clear();
            contacttxt.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {

        }

        private void cusidtxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
