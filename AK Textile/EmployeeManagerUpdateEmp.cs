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
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private EmpManagerEmployee employeeManRemoveEmpForm;

        // Variable to store the ID of the employee being updated
        private string currentEmpID = null;

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

        // --- Loads Positions into the ComboBox ---
        private void LoadPositions()
        {
            try
            {
                string query = "SELECT PositionID, PName FROM Position";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable positionTable = new DataTable();
                adapter.Fill(positionTable);

                comboBox1.DataSource = positionTable;
                comboBox1.DisplayMember = "PName"; 
                comboBox1.ValueMember = "PositionID";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load positions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Loads Departments into the ComboBox ---
        private void LoadDepartments()
        {
            try
            {
                string query = "SELECT DepID, DepName FROM Department";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable departmentTable = new DataTable();
                adapter.Fill(departmentTable);

                comboBox2.DataSource = departmentTable;
                comboBox2.DisplayMember = "DepName";
                comboBox2.ValueMember = "DepID";
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string searchVal = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchVal))
            {
                MessageBox.Show("Please enter an Employee ID or Username to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClearForm();

            try
            {
                con.Open();
                string query = @"SELECT EmpID, EmpName, EmpStreetNo, EmpStreetName, EmpCity, EmpUsername, EmpPswrd, EmpContact, DepID, PositionID
                                 FROM Employee
                                 WHERE EmpID = @SearchVal OR EmpUsername = @SearchVal";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SearchVal", searchVal);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Store the ID of the found employee for the update query
                    currentEmpID = reader["EmpID"].ToString();

                    // Populate fields
                    textBox3.Text = reader["EmpName"].ToString();
                    textBox4.Text = reader["EmpUsername"].ToString();
                    textBox2.Text = reader["EmpPswrd"].ToString(); // Consider security implications of showing password
                    textBox8.Text = reader["EmpContact"].ToString();
                    textBox5.Text = reader["EmpStreetNo"] != DBNull.Value ? reader["EmpStreetNo"].ToString() : "";
                    textBox6.Text = reader["EmpStreetName"].ToString();
                    textBox7.Text = reader["EmpCity"].ToString();

                    // Set the dropdowns using SelectedValue
                    comboBox1.SelectedValue = reader["PositionID"];
                    comboBox2.SelectedValue = reader["DepID"];
                }
                else
                {
                    MessageBox.Show("Employee with that ID or Username not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching employee: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
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
            // 1. Check if an employee is loaded
            if (currentEmpID == null)
            {
                MessageBox.Show("Please search for and load an employee first.", "No Employee Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validation
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedValue == null ||
                comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all required fields (Name, Username, Password, Position, Department).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(textBox5.Text) && !int.TryParse(textBox5.Text, out _))
            {
                MessageBox.Show("Home Number must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Get Updated Data
            string fullName = textBox3.Text;
            string username = textBox4.Text;
            string password = textBox2.Text;
            string contact = textBox8.Text;
            string positionID = comboBox1.SelectedValue.ToString();
            string depID = comboBox2.SelectedValue.ToString();
            string homeNoText = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;

            int? homeNo = null;
            if (int.TryParse(homeNoText, out int parsedHomeNo))
            {
                homeNo = parsedHomeNo;
            }
            try
            {
                con.Open();
                string query = @"UPDATE Employee
                                 SET EmpName = @EmpName,
                                     EmpStreetNo = @EmpStreetNo,
                                     EmpStreetName = @EmpStreetName,
                                     EmpCity = @EmpCity,
                                     EmpUsername = @EmpUsername,
                                     EmpPswrd = @EmpPswrd,
                                     EmpContact = @EmpContact,
                                     DepID = @DepID,
                                     PositionID = @PositionID
                                 WHERE EmpID = @CurrentEmpID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmpName", fullName);

                if (homeNo.HasValue)
                    cmd.Parameters.AddWithValue("@EmpStreetNo", homeNo.Value);
                else
                    cmd.Parameters.AddWithValue("@EmpStreetNo", DBNull.Value);

                cmd.Parameters.AddWithValue("@EmpStreetName", streetName);
                cmd.Parameters.AddWithValue("@EmpCity", city);
                cmd.Parameters.AddWithValue("@EmpUsername", username);
                cmd.Parameters.AddWithValue("@EmpPswrd", password); 
                cmd.Parameters.AddWithValue("@EmpContact", contact);
                cmd.Parameters.AddWithValue("@DepID", depID);
                cmd.Parameters.AddWithValue("@PositionID", positionID);
                cmd.Parameters.AddWithValue("@CurrentEmpID", currentEmpID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    employeeManRemoveEmpForm.RefreshDataGrid(); // Refresh parent
                    this.Close(); // Close this form
                }
                else
                {
                    MessageBox.Show("Update failed. Employee not found or no changes made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle specific SQL errors like FK violation if DepID/PositionID becomes invalid
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("Error: Invalid Department or Position selected.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Handle potential unique constraint violation if username is changed to an existing one
                else if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    MessageBox.Show("Error: The Username chosen already exists.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error updating employee: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void EmployeeManagerUpdateEmp_Load_1(object sender, EventArgs e)
        {

        }
    }
}