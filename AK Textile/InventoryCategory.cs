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
    public partial class InventoryCategory : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;
                                        Initial Catalog=AKTextilesDB;
                                        Integrated Security=True;");
        Form formBackground = null; // Declare outside to access in 'finally'

        public InventoryCategory(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadAllCategory();
        }

        // Public method to refresh data grid
        public void RefreshDataGrid()
        {
            LoadAllCategory();
        }

        private void LoadAllCategory()
        {
            // SQL query to fetch all data from the Product table
            string query = "SELECT * FROM InventoryCategory";
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

        private void LoadSearchCategory()
        {
            // Get the value entered in the textbox
            string searchValue = textBox1.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Inventory Category ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query to fetch data based on PID or Pname
            string query = @"SELECT * FROM InventoryCategory
                             WHERE InvCatID = @SearchValue OR InvCategory LIKE '%' + @SearchValue + '%'";

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
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = null; // Clear DataGridView if no data found
                            con.Close();
                            LoadAllCategory();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        private void OpenSubForm(Form subForm)
        {
            Form formBackground = new Form(); // Initialize background form

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

                // Set the background form as the owner of the subform
                subForm.Owner = formBackground;

                // Show the subform as a dialog
                subForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Dispose both forms
                formBackground.Dispose();
                subForm.Dispose();
            }
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void InventoryRaw_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryProduct(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventorySupplier(mainForm));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryInventory(mainForm));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryGRN(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryReport(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new InventoryCategoryRemove(this));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new InventoryCategoryUpdate(this));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new InventoryCategoryAdd(this));
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            LoadSearchCategory();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;

            LoadAllCategory();
        }
    }
}
