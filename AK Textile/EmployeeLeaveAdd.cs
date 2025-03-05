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

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;");
        public EmployeeLeaveAdd(Employee employeeForm)
        {
            InitializeComponent();
            AutoGenerateID();
            this.employeeForm = employeeForm;
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
                        this.textBox2.Text = "LE001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                        string newID = "LE" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
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

        private void AddLeave()
        {
            string leaveID = textBox2.Text;
            string startDate = dateTimePicker1.Text;
            string endDate = dateTimePicker2.Text;
            string leaveType = comboBox2.Text;
            string reason = textBox1.Text;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Leave (LeaveID, LeaveTypeID,  LReason, LStartDate, LEndDate) VALUES (@LeaveID, @LeaveTypeID, @LReason, @LStartDate, @LEndDate, ,)", con);
                cmd.Parameters.AddWithValue("@LeaveID", leaveID);
                cmd.Parameters.AddWithValue("@LStartDate", startDate);
                cmd.Parameters.AddWithValue("@LEndDate", endDate);
                cmd.Parameters.AddWithValue("@LeaveTypeId", leaveType);
                cmd.Parameters.AddWithValue("@LReason", reason);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Leave added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AutoGenerateID();
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
        private void AllClear()
        {
            dateTimePicker1.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            comboBox2.Text = string.Empty;
            textBox1.Text = string.Empty;
        }



        private void button8_Click(object sender, EventArgs e)
        {
            AddLeave();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Call the public method from EmployeeForm
            employeeForm.RefreshDataGrid();

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        private void EmployeeLeaveAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
