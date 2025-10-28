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
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        private EmpManagerEmployee employeeManRemoveEmpForm;
        public EmployeeManagerUpdateEmp(EmpManagerEmployee employeeManRemoveEmpForm)
        {
            InitializeComponent();
            this.employeeManRemoveEmpForm = employeeManRemoveEmpForm;
        }

        private void EmployeeManagerUpdateEmp_Load(object sender, EventArgs e)
        {
            LoadPositions();
            LoadDepartments();
            ClearForm();
        }

        private void LoadPositions()
        {
            try
            {
                con.Open();
                string query = "SELECT PositionID, PName FROM Position";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "PName";
                comboBox1.ValueMember = "PositionID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load positions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        //NEW: Method to load departments
        private void LoadDepartments()
        {
            try
            {
                con.Open();
                string query = "SELECT DepID, DepName FROM Department";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "DepName"; 
                comboBox2.ValueMember = "DepID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        //Search Button
        private void button5_Click(object sender, EventArgs e)
        {
            string employeeIDOrName = textBox1.Text;

            if (string.IsNullOrWhiteSpace(employeeIDOrName))
            {
                MessageBox.Show("Please enter Employee ID or Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                con.Open();
                string query1 = "SELECT * FROM Employee WHERE EmpID = @EmpID OR EmpUserName = @EmpUserName";

                using (SqlCommand command = new SqlCommand(query1, con))
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
                            textBox5.Text = reader["EmpStreetNo"].ToString();
                            textBox6.Text = reader["EmpStreetName"].ToString();
                            textBox7.Text = reader["EmpCity"].ToString();
                            // This sets the value, and the ComboBox shows the matching name.
                            comboBox1.SelectedValue = reader["PositionID"];
                            comboBox2.SelectedValue = reader["DepID"];
                        }
                        else
                        {
                            MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
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
            textBox8.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        // Update Button
        private void button8_Click(object sender, EventArgs e)
        {
            string employeeIDOrName = textBox1.Text;
            string fullName = textBox3.Text;
            string userName = textBox4.Text;
            string password = textBox2.Text;
            string contactNo = textBox8.Text;
            string homeNo = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;

            object selectedPosition = comboBox1.SelectedValue;
            object selectedDepartment = comboBox2.SelectedValue; 

            if (string.IsNullOrWhiteSpace(employeeIDOrName))
            {
                MessageBox.Show("Please search for and select an Employee first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedPosition == null || selectedDepartment == null)
            {
                MessageBox.Show("Please ensure Position and Department are selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string positionID = selectedPosition.ToString();
            string departmentID = selectedDepartment.ToString();

            try
            {
                con.Open();

                string query = "UPDATE Employee SET EmpName = @EmpName, EmpUsername = @EmpUsername, " +
                               "EmpPswrd = @EmpPswrd, PositionID = @PositionID, DepID = @DepID, EmpContact = @EmpContact, " +
                               "EmpStreetNo = @EmpStreetNo, EmpStreetName = @EmpStreetName, EmpCity = @EmpCity " +
                               "WHERE EmpID = @EmpID OR EmpUserName = @EmpUserName";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@EmpName", fullName);
                    command.Parameters.AddWithValue("@EmpUsername", userName);
                    command.Parameters.AddWithValue("@EmpPswrd", password);
                    command.Parameters.AddWithValue("@PositionID", positionID);
                    command.Parameters.AddWithValue("@DepID", departmentID);
                    command.Parameters.AddWithValue("@EmpContact", contactNo);
                    command.Parameters.AddWithValue("@EmpStreetNo", homeNo);
                    command.Parameters.AddWithValue("@EmpStreetName", streetName);
                    command.Parameters.AddWithValue("@EmpCity", city);
                    command.Parameters.AddWithValue("@EmpID", employeeIDOrName);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh data grid
                employeeManRemoveEmpForm.RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
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