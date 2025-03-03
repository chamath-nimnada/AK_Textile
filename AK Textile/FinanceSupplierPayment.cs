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
    public partial class FinanceSupplierPayment : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=AKTextilesDB;Integrated Security=True;");
        public FinanceSupplierPayment(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
       
        
        public void LoadAllSupplier()
        {
            string query = "SELECT * FROM SupplierPayment";
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

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceOrder(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSale(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceSalary(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceReport(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceDashboard(mainForm));
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SupplierId.Text = string.Empty;
            LoadAllSupplier();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Get the value entered in the textbox
            string searchValue = SupplierId.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Supplier ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query to fetch data based on SupplierID or Suplliername
            string query = @"SELECT * FROM SupplierPayment
                 WHERE SupID = @SearchValue OR SupName LIKE '%' + @SearchValue + '%'";

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
                            LoadAllSupplier();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (FinanceSupplierPyamentAdd financeSupplierPyamentAdd = new FinanceSupplierPyamentAdd(mainForm))
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

                    financeSupplierPyamentAdd.Owner = formBackground;
                    financeSupplierPyamentAdd.ShowDialog();

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

        private void FinanceSupplierPayment_Load(object sender, EventArgs e)
        {
            LoadAllSupplier();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
