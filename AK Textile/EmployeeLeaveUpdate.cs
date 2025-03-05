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
using System.Xml.Serialization;

namespace AK_Textile
{
    public partial class EmployeeLeaveUpdate : Form
    {
        private Employee employeeForm; // Reference to Employee Leave

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;");
        public EmployeeLeaveUpdate(Employee employeeForm)
        {
            InitializeComponent();
            this.employeeForm = employeeForm;
        }

        private void EmployeeLeaveUpdate_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter an Employee ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string query = "SELECT * FROM Leave WHERE LeaveID = @LeaveID";
                using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@LeaveID", textBox1.Text.Trim());
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            textBox2.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No record found for the given Employee ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLeaveData()
        {
            string query = "SELECT LeaveID FROM Leave";
            using(SqlCommand cmd = new SqlCommand(query, con))
            using(SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);

               // textBox1.DataSource = dt;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFields();

        }

        private void ClearFields()
        {
            textBox1.Clear(); // Fixing the error by using Clear() method instead of SelectedIndex
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            comboBox2.Text = String.Empty;
            textBox2.Clear();
        }
    }
}
