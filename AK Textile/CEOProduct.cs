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
    public partial class CEOProduct : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=Textiles;Integrated Security=True;");

        public CEOProduct(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadAllProduct();
        }

        private void LoadAllProduct()
        {
            // SQL query to fetch all data from the Product table
            string query = "SELECT * FROM Product";
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
        private void CEOProduct_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the TextBox
            textBox1.Text = string.Empty;

            LoadAllProduct();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Get the value entered in the textbox
            string searchValue = textBox1.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Product ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query to fetch data based on PID or Pname
            string query = @"SELECT * FROM Product
                             WHERE ID = @SearchValue OR PName LIKE '%' + @SearchValue + '%'";

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
                            LoadAllProduct();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
