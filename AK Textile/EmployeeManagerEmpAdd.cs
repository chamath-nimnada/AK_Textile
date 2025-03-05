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
    public partial class EmployeeManagerEmpAdd : Form
    {
        private EmpManagerEmployee empManagerEmployee;
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True");
        public EmployeeManagerEmpAdd(EmpManagerEmployee empManagerEmployee)
        {
            InitializeComponent();
            this.empManagerEmployee = empManagerEmployee;
        }
        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(EmpID) FROM Employee", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    if (dr1[0] == DBNull.Value)
                    {
                        this.textBox2.Text = "INC001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                        string newID = "INC" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                        this.textBox2.Text = newID;
                    }
                    dr1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearForm()
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox1.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            textBox8.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Get values from input fields
            string employeeID = textBox2.Text;
            string fullName = textBox3.Text;
            string userName = textBox4.Text;
            string password = textBox1.Text;
            string position = comboBox1.Text;
            string contactNo = textBox8.Text;
            string homeNo = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(employeeID) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Employee ID and Full Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Database connection string
            string connectionString = "YourConnectionStringHere";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert query
                    string query = "INSERT INTO Employees (EmpID, EmpName, EmpUserName, EmpPswrd, PositionID, EmpContact, EmpStreetNo, EmpStreetName, EmpCity) VALUES (@EmpID, @EmpName, @EmpUserName, @EmpPswrd, @PositionID, @EmpContact, @EmpStreetNo, @EmpStreetName, @EmpCity)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@EmpID", employeeID);
                        command.Parameters.AddWithValue("@EmpName", fullName);
                        command.Parameters.AddWithValue("@EmpUserName", userName);
                        command.Parameters.AddWithValue("@EmpPswrd", password);
                        command.Parameters.AddWithValue("@PositionID", position);
                        command.Parameters.AddWithValue("@EmpContact", contactNo);
                        command.Parameters.AddWithValue("@EmpStreetNo", homeNo);
                        command.Parameters.AddWithValue("@EmpStreetName", streetName);
                        command.Parameters.AddWithValue("@EmpCity", city);

                        // Execute query
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
