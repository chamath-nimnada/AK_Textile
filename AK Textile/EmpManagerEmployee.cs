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
    public partial class EmpManagerEmployee : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerEmployee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void EmpManagerEmployee_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Employee", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    // Bind the data to the DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            finally
            {
                con.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDashboard(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerSalary(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerLeave(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDepartment(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerReport(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmployeeId.Text=string.Empty;
        }

        /*method to search button to filter data according to the entered
          name or ID */
        private void SearchEmployee(string searchValue)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Employee WHERE EmpID LIKE @searchval OR EmpName LIKE @searchval", con);
            cmd2.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();

                try
                {
                //To get the  searched data
                da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Matching item Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            finally
            {
                con.Close();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = EmployeeId.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter Employee ID or Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Calling the method to search employee
            SearchEmployee(searchValue);
        }
    }
}

