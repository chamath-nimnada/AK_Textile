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
    public partial class Employee : Form
    {
        private MainForm mainForm;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        Form formBackground = null;
        public Employee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            // These correctly load data for the specific user
            LoadAllSalary();
            LoadLeaveData();
        }
        public void RefreshDataGrid()
        {
            LoadAllSalary();
            LoadLeaveData();
        }

        private void LoadAllSalary()
        {
            // This query is perfect, it only gets salary for the logged-in user
            string query1 = "SELECT * FROM Salary WHERE EmpID = @EmpID";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    // This line is the key: it uses the ID from the login form
                    cmd.Parameters.AddWithValue("@EmpID", LoginForm.LoggedInUser.UserId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView2.DataSource = dataTable; // Assumes dataGridView2 is Salary
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading salary data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadLeaveData()
        {
            // This query is also perfect
            string query2 = "SELECT LeaveID, LeaveTypeID, LReason, LStartDate, LEndDate, LStatus FROM Leave WHERE EmpID = @EmpID";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    // Uses the ID from the login form
                    cmd.Parameters.AddWithValue("@EmpID", LoginForm.LoggedInUser.UserId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Assumes dataGridView1 is Leaves
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading leave data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void OpenSubForm(Form subForm)
        {
            Form formBackground = new Form();

            try
            {
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = true;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                subForm.Owner = formBackground;
                subForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
                subForm.Dispose();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // "Logout" button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        // "Add" button
        private void button7_Click(object sender, EventArgs e)
        {
            OpenSubForm(new EmployeeLeaveAdd(this));
        }

        // "Update" button
        private void button8_Click(object sender, EventArgs e)
        {
            OpenSubForm(new EmployeeLeaveUpdate(this));
        }

        // "Remove" button
        private void button9_Click(object sender, EventArgs e)
        {
            OpenSubForm(new EmployeeLeaveRemove(this));
        }
    }
}