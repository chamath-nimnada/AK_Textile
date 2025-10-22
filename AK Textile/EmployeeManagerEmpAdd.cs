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

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

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
                        this.textBox2.Text = "EMP001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        int numericPart = int.Parse(maxID.Substring(3));
                        string newID = "EMP" + (numericPart + 1).ToString("D3");
                        this.textBox2.Text = newID;
                    }
                    dr1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating ID: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // This method loads the Position ComboBox
        private void LoadPositions()
        {
            try
            {
                con.Open();
                string query = "SELECT PositionID, PName FROM Position";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Configure the Position ComboBox
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "PName";
                comboBox1.ValueMember = "PositionID";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading positions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        // Method to load departments into the new ComboBox
        private void LoadDepartments()
        {
            try
            {
                con.Open();
                string query = "SELECT DepID, DepName FROM Department";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Configure the Department ComboBox
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "DepName";
                comboBox2.ValueMember = "DepID"; 
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button8_Click(object sender, EventArgs e)
        {
            string employeeID = textBox2.Text;
            string fullName = textBox3.Text;
            string userName = textBox4.Text;
            string password = textBox1.Text;
            string contactNo = textBox8.Text;
            string homeNo = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;
            // Get the selected *Value* (ID) from both ComboBoxes
            object selectedPosition = comboBox1.SelectedValue;
            object selectedDepartment = comboBox2.SelectedValue;

            // Validate input
            if (string.IsNullOrWhiteSpace(employeeID) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Employee ID and Full Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validation check for both ComboBoxes
            if (selectedPosition == null)
            {
                MessageBox.Show("Please select a position.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selectedDepartment == null)
            {
                MessageBox.Show("Please select a department.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string positionID = selectedPosition.ToString();
            string departmentID = selectedDepartment.ToString(); 

            try
            {
                con.Open();
                string query = "INSERT INTO Employee (EmpID, EmpName, EmpUsername, EmpPswrd, PositionID, DepID, EmpContact, EmpStreetNo, EmpStreetName, EmpCity) " +
                               "VALUES (@EmpID, @EmpName, @EmpUsername, @EmpPswrd, @PositionID, @DepID, @EmpContact, @EmpStreetNo, @EmpStreetName, @EmpCity)";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@EmpID", employeeID);
                    command.Parameters.AddWithValue("@EmpName", fullName);
                    command.Parameters.AddWithValue("@EmpUsername", userName);
                    command.Parameters.AddWithValue("@EmpPswrd", password);
                    command.Parameters.AddWithValue("@PositionID", positionID);
                    command.Parameters.AddWithValue("@DepID", departmentID);
                    command.Parameters.AddWithValue("@EmpContact", contactNo);
                    command.Parameters.AddWithValue("@EmpStreetNo", homeNo);
                    command.Parameters.AddWithValue("@EmpStreetName", streetName);
                    command.Parameters.AddWithValue("@EmpCity", city);

                    // Execute query
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form and refresh parent grid
                ClearForm();
                empManagerEmployee.RefreshDataGrid();
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
