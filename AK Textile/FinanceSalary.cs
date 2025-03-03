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
    public partial class FinanceSalary : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;");
        public FinanceSalary(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadAllSalary();
        }
        private void LoadAllSalary()
        {
            // SQL query to fetch all data from the Product table
            string query = "SELECT * FROM Salary";
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

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceReport(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm (new FinanceDashboard(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceOrder(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSale(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmployeeId.Text=string .Empty;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new LoginForm(mainForm));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Get the value entered in the textbox
            string searchValue = EmployeeId.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Inventory ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query to fetch data based on PID or Pname
            string query = @"SELECT * FROM Salary
                             WHERE EmpID = @SearchValue OR InvCategory LIKE '%' + @SearchValue + '%'";

            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                        // Execute the query and load the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Check if any rows are returned
                        if (dataTable.Rows.Count > 0)
                        {
                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;

                            // Adjust columns to fit the grid width
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        else
                        {
                            MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = null; // Clear DataGridView if no data found
                            con.Close();
                            LoadAllSalary();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FinanceSalary_Load(object sender, EventArgs e)
        {

        }
    }
}
