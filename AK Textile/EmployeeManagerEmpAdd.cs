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

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        public EmployeeManagerEmpAdd(EmpManagerEmployee empManagerEmployee)
        {
            InitializeComponent();
            this.empManagerEmployee = empManagerEmployee;
        }
        // --- Auto-Generates the Employee ID (e.g., "EMP001") ---
        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(CAST(SUBSTRING(EmpID, 4, LEN(EmpID)) AS INT)) FROM Employee WHERE EmpID LIKE 'EMP%'", con);
                object result = cmd1.ExecuteScalar(); 

                if (result == DBNull.Value || result == null)
                {
                    textBox2.Text = "EMP001"; // Start from 001
                }
                else
                {
                    int numericPart = Convert.ToInt32(result);
                    string newID = "EMP" + (numericPart + 1).ToString("D3"); 
                    textBox2.Text = newID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Employee ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "EMP-ERR"; 
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
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

                // Configure the Position ComboBox
                comboBox1.DataSource = positionTable;
                comboBox1.DisplayMember = "PName";
                comboBox1.ValueMember = "PositionID";    // Store ID
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

                // Configure the Department ComboBox
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

        //clear button
        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        //cancel button
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
            comboBox2.SelectedIndex = -1;
            textBox8.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;

            // Regenerate the ID after clearing
            AutoGenerateID();
        }

        //add button
        private void button8_Click(object sender, EventArgs e)
        {
            // Validation ---
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
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


            // Get Data ---
            string empID = textBox2.Text;
            string fullName = textBox3.Text;
            string username = textBox4.Text;
            string password = textBox1.Text;
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
                string query = @"INSERT INTO Employee (EmpID, EmpName, EmpStreetNo, EmpStreetName, EmpCity, EmpUsername, EmpPswrd, EmpContact, DepID, PositionID)
                                 VALUES (@EmpID, @EmpName, @EmpStreetNo, @EmpStreetName, @EmpCity, @EmpUsername, @EmpPswrd, @EmpContact, @DepID, @PositionID)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmpID", empID);
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

                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                empManagerEmployee.RefreshDataGrid();
                ClearForm(); 
                AutoGenerateID();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627) // Primary Key violation
                {
                    MessageBox.Show($"Error: Employee ID '{empID}' already exists.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (sqlEx.Number == 547) // Foreign Key violation
                {
                    MessageBox.Show("Error: Invalid Department or Position selected.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error adding employee: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EmployeeManagerEmpAdd_Load(object sender, EventArgs e)
        {
            AutoGenerateID();
            LoadPositions();
            LoadDepartments();
        }
    }
}
