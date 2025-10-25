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
    public partial class EmployeeLeaveAdd : Form
    {
        private Employee employeeForm; // Reference to Employee Leave

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        public EmployeeLeaveAdd(Employee employeeForm)
        {
            InitializeComponent();
            this.employeeForm = employeeForm;
        }
        private void EmployeeLeaveAdd_Load(object sender, EventArgs e)
        {
            AutoGenerateID();
            LoadLeaveTypes();
            AllClear();
        }

        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(LeaveID) FROM Leave", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    if (dr1[0] == DBNull.Value)
                    {
                        this.textBox2.Text = "L001"; 
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        int numericPart = int.Parse(maxID.Substring(1)); // Extract "001"
                        string newID = "L" + (numericPart + 1).ToString("D3"); // Format as "LXXX"
                        this.textBox2.Text = newID;
                    }
                    dr1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Leave ID: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void LoadLeaveTypes()
        {
            try
            {
                con.Open();
                string query = "SELECT LeaveTypeID, LTName FROM LeaveType";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "LTName"; 
                comboBox2.ValueMember = "LeaveTypeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load leave types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        //method to add leaves
        private void AddLeave()
        {
            string leaveID = textBox2.Text;
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            string reason = textBox1.Text;
            object selectedLeaveType = comboBox2.SelectedValue;
            string loggedInEmpID = LoginForm.LoggedInUser.UserId;

            //Validation
            if (selectedLeaveType == null)
            {
                MessageBox.Show("Please select a leave type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Please enter a reason for your leave.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (endDate < startDate)
            {
                MessageBox.Show("End date cannot be before the start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string leaveTypeID = selectedLeaveType.ToString();
            string status = "Pending";

            try
            {
                con.Open();
                string query = "INSERT INTO Leave (LeaveID, EmpID, LeaveTypeID, LReason, LStartDate, LEndDate, LStatus) " +
                               "VALUES (@LeaveID, @EmpID, @LeaveTypeID, @LReason, @LStartDate, @LEndDate, @LStatus)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@LeaveID", leaveID);
                cmd.Parameters.AddWithValue("@EmpID", loggedInEmpID);
                cmd.Parameters.AddWithValue("@LeaveTypeID", leaveTypeID);
                cmd.Parameters.AddWithValue("@LReason", reason);
                cmd.Parameters.AddWithValue("@LStartDate", startDate);
                cmd.Parameters.AddWithValue("@LEndDate", endDate);
                cmd.Parameters.AddWithValue("@LStatus", status);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Leave added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                employeeForm.RefreshDataGrid(); // Refresh the main employee grid
                this.Close(); // Close the form after success
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding leave: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void AllClear()
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
            textBox1.Text = string.Empty;
        }


        // "Add" button
        private void button8_Click(object sender, EventArgs e)
        {
            AddLeave();
        }

        // "Cancel" button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // "Clear" button
        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();
            AutoGenerateID();
        }
    }
}