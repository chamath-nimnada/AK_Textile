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
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;");
        public Employee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void LoadAllLeaves()
        {
            string query = "SELECT * FROM Leave";
            {
                try
                {
                    // Open the connection
                    con.Open();
                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Execute the query and load the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                        // Adjust columns to fit the grid width
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SalaryId.Text = string.Empty;
            LoadAllLeaves();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmployeeLeaveAdd emplpoyeeleaveadd = new EmployeeLeaveAdd())
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

                    emplpoyeeleaveadd.Owner = formBackground;
                    emplpoyeeleaveadd.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmployeeLeaveUpdate employeeleaveupdate = new EmployeeLeaveUpdate())
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

                    employeeleaveupdate.Owner = formBackground;
                    employeeleaveupdate.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (EmployeeLeaveRemove employeeleaveremove = new EmployeeLeaveRemove())
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

                    employeeleaveremove.Owner = formBackground;
                    employeeleaveremove.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Get the value eneterd in the textbox
            string searchValue = SalaryId.Text.Trim();

            //Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a value to search!", "Validation error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //SQL query to fetch data based on SalaryID
            string query = "SELECT * FROM Leave WHERE LeaveID = @LeaveID";
            {
                try
                {
                    //Open the connection
                    con.Open();
                    //Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        //Add the parameter to the query
                        cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                        //Execute the query and load the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        //Check if any rows are returened
                        if (dataTable.Rows.Count > 0)
                        {
                            //Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                            //Adjust columns to fit the grid width
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        else
                        {
                            MessageBox.Show("No records found!", "No records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = null;//Clear Datagridview if no data found
                            con.Close();
                            LoadAllLeaves();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}

