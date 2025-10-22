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

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        Form formBackground = null;
        public Employee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

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
            string query1 = "SELECT * FROM Salary WHERE EmpID = @EmpID";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Parameters.AddWithValue("@EmpID", LoginForm.LoggedInUser.UserId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView2.DataSource = dataTable;
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
            string query2 = "SELECT LeaveID, LeaveTypeID, LReason, LStartDate, LEndDate, LStatus FROM Leave WHERE EmpID = @EmpID";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@EmpID", LoginForm.LoggedInUser.UserId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
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


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadAllSearchCategory()
        {
            string searchValue = SalaryId.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Salary ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query3 = "SELECT * FROM Salary WHERE SalaryID = @SearchValue AND EmpID = @EmpID";

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query3, con))
                {
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                    cmd.Parameters.AddWithValue("@EmpID", LoginForm.LoggedInUser.UserId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dataTable;
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        MessageBox.Show("No matching records found for your account.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView2.DataSource = null;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button6_Click(object sender, EventArgs e)
        {
            SalaryId.Text = string.Empty;

            LoadAllSalary();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        // This is the "Add" button
        private void button7_Click(object sender, EventArgs e)
        {
            OpenSubForm(new EmployeeLeaveAdd(this));
        }

        // This is the "Update" button
        private void button8_Click(object sender, EventArgs e)
        {
            OpenSubForm(new EmployeeLeaveUpdate(this));
        }

        // This is the "Remove" button
        private void button9_Click(object sender, EventArgs e)
        {
            OpenSubForm(new EmployeeLeaveRemove(this));
        }

        // This is the "Search" button
        private void button10_Click(object sender, EventArgs e)
        {
            LoadAllSearchCategory();
        }
    }
}