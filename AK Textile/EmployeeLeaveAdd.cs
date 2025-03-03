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
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;");
        public EmployeeLeaveAdd(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            AutoIncrementLeaveID();
        }
        private void AutoIncrementLeaveID()
        {
            //To auto increment the Leave ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(LeaveID) FROM Leave", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.textBox2.Text = "LEA001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "LEA" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.textBox2.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            InsertLeave();
        }

        private void InsertLeave()
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Leave ID cannot be empty!", "Validation error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Leave (LeaveID, LeaveTypeID, LReason, LStartDate, LEndDate) VALUES (@LeaveID, @LeaveTypeID, @LReason, LStartDate, LEndDate)";

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaveID", textBox2.Text);
                    command.Parameters.AddWithValue("@LeaveTypeID", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("@LReason", textBox1.Text);
                    command.Parameters.AddWithValue("@LStartDate", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@LEndDate", dateTimePicker2.Value);

                  
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            comboBox2.Text = string.Empty;
            textBox1.Text = string.Empty;
        }
    }
}
