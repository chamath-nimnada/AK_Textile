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
    public partial class EmpManagerLeaveAdd : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True");
        public EmpManagerLeaveAdd()
        {
            InitializeComponent();
        }

        private void AddCategory()
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox8.Text = string.Empty;

            MessageBox.Show("All fields have been cleared.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string leaveID = textBox2.Text.Trim();
            string LTName = textBox3.Text.Trim();
            string description = textBox4.Text.Trim();
            string leaveAmountPerMonth = textBox8.Text.Trim();

            if (string.IsNullOrEmpty(leaveID) || string.IsNullOrEmpty(LTName) || string.IsNullOrEmpty(leaveAmountPerMonth))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True"; // Replace with your database connection string.
            string query = "INSERT INTO LeaveType (LeaveID, LTName, LTDescription, LTAmount) VALUES (@LeaveID, @LTName, @LTDescription, @LTAmount)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaveID", leaveID);
                    command.Parameters.AddWithValue("@LTName", LTName);
                    command.Parameters.AddWithValue("@LTDescription", description);
                    command.Parameters.AddWithValue("@LTAmount", leaveAmountPerMonth);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Leave type added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add leave type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
