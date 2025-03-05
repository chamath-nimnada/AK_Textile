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
    public partial class EmployeeManagerUpdateEmp : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True");

        private EmpManagerEmployee employeeManRemoveEmpForm;
        public EmployeeManagerUpdateEmp(EmpManagerEmployee employeeManRemoveEmpForm)
        {
            InitializeComponent();
            this.employeeManRemoveEmpForm = employeeManRemoveEmpForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string employeeIDOrName = textBox1.Text;

            if (string.IsNullOrWhiteSpace(employeeIDOrName))
            {
                MessageBox.Show("Please enter Employee ID or Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "YourConnectionStringHere";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Employee WHERE EmpID = @EmplID OR EmpUserName = @EmpUserName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpID", employeeIDOrName);
                        command.Parameters.AddWithValue("@EmpUserName", employeeIDOrName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox3.Text = reader["EmpName"].ToString();
                                textBox4.Text = reader["EmpUserName"].ToString();
                                textBox2.Text = reader["EmpPswrd"].ToString();
                                textBox8.Text = reader["EmpContact"].ToString();
                                comboBox1.Text = reader["PositionID"].ToString();
                                textBox5.Text = reader["EmpStreetNo"].ToString();
                                textBox6.Text = reader["EmpStreetName"].ToString();
                                textBox7.Text = reader["EmpCity"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            textBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            textBox8.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string employeeIDOrName = textBox1.Text;
            string fullName = textBox3.Text;
            string userName = textBox4.Text;
            string password = textBox2.Text;
            string position = comboBox1.Text;
            string contactNo = textBox8.Text;
            string homeNo = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;

            if (string.IsNullOrWhiteSpace(employeeIDOrName))
            {
                MessageBox.Show("Please select an Employee first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "YourConnectionStringHere";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Employee SET EmpName = @EmpName, EmpUserName = @EmpUserName, EmpPswrd = @EmpPswrd, PositionID = @PositionID, EmpContact = @EmpContact, EmpStreetNo = @EmpStreetNo, EmpStreetName = @EmpStreetName, EmpCity = @EmpCity WHERE EmplID = @EmplID OR EmpUserName = @EmpUserName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpName", fullName);
                        command.Parameters.AddWithValue("@EmpUserName", userName);
                        command.Parameters.AddWithValue("@EmpPswrd", password);
                        command.Parameters.AddWithValue("@PositionID", position);
                        command.Parameters.AddWithValue("@EmpContact", contactNo);
                        command.Parameters.AddWithValue("@EmpStreetNo", homeNo);
                        command.Parameters.AddWithValue("@EmpStreetName", streetName);
                        command.Parameters.AddWithValue("@EmpCity", city);
                        command.Parameters.AddWithValue("@EmpID", employeeIDOrName);
                        command.Parameters.AddWithValue("@EmpUserName", employeeIDOrName);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
