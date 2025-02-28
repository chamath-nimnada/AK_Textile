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
    public partial class InventorySupplier : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;
                                        Initial Catalog=AKTextilesDB;
                                        Integrated Security=True;");
        public InventorySupplier(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadAllSuppliers();
        }

        private void LoadAllSuppliers()
        {
            // SQL query to fetch all data from the Supplier table
            string query = "SELECT * FROM Supplier";
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

        private void SearchSupplier() 
        {
            // Get the value entered in the textbox
            string searchValue = textBox1.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Supplier ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query1 = @"SELECT * FROM Supplier
                             WHERE SupID = @SearchValue OR SupName LIKE '%' + @SearchValue + '%'";

            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query1, con))
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
                            MessageBox.Show("No matching supplier found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = null; // Clear DataGridView if no data found
                            con.Close();
                            LoadAllSuppliers();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void LoadPurchaseOrders() 
        {
            // Get the value entered in the textbox
            string searchValue = textBox1.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                //MessageBox.Show("Please enter a Supplier ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Purchase Orders
            string query2 = @"
                                SELECT 
                                    po.SPOrderID, 
                                    po.POrderID, 
                                    po.IssuedDate 
                                FROM 
                                    SupplierPurchaseOrder po
                                INNER JOIN 
                                    Supplier s ON po.SupID = s.SupID
                                WHERE 
                                    s.SupID = @SearchValue OR s.SupName LIKE '%' + @SearchValue + '%'";
            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query2, con))
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
                            dataGridView2.DataSource = dataTable;

                            // Adjust columns to fit the grid width
                            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("No purchased order found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView2.DataSource = null; // Clear DataGridView if no data found
                            con.Close();
                            LoadAllSuppliers();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }
        private void InventorySupplier_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchSupplier();
            LoadPurchaseOrders();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the TextBox
            textBox1.Text = string.Empty;
            // Clear the DataGridView
            dataGridView2.DataSource = null;

            LoadAllSuppliers();
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
            mainForm.LoadForm(new InventoryCategory(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryInventory(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryGRN(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryReport(mainForm));
        }
    }
}
