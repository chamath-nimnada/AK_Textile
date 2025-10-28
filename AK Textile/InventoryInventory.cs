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
    public partial class InventoryInventory : Form
    {
        private MainForm mainForm;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        Form formBackground = null;
        public InventoryInventory(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void InventoryInventory_Load(object sender, EventArgs e)
        {
            LoadAllInventory();
        }

        private void LoadAllInventory()
        {
            string query = "SELECT * FROM Inventory";

            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();

            try
            {
                // Adapter handles open/close
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshDataGrid()
        {
            LoadAllInventory();
        }
        private void SearchInventoryItem()
        {
            string searchValue = textBox1.Text.Trim(); // Assumes search box is textBox1


            string query = @"SELECT Inventory.*
                             FROM Inventory
                             JOIN InventoryCategory ON Inventory.InvCatID = InventoryCategory.InvCatID
                             WHERE Inventory.InvID = @SearchValue 
                                OR InventoryCategory.InvCategory LIKE @SearchPattern";

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                    cmd.Parameters.AddWithValue("@SearchPattern", "%" + searchValue + "%");

                    // Adapter handles open/close
                    adapter.Fill(dataTable);
                }

                // Check if any rows are returned
                if (dataTable.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Clear DataGridView if no data found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                subForm.Owner = formBackground;
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

                RefreshDataGrid();
            }
        }



        // "Search" button
        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim())) // Assumes search box is textBox1
            {
                MessageBox.Show("Please enter an Inventory ID or Category to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SearchInventoryItem();
        }

        // "Clear Search" button
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty; // Assumes search box is textBox1
            LoadAllInventory(); // Reload original data
        }

        // Navigation buttons
        private void pictureBox3_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new InventoryDashboard(mainForm)); }
        private void pictureBox2_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new LoginForm(mainForm)); }
        private void button2_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new InventoryProduct(mainForm)); }
        private void button3_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new InventoryCategory(mainForm)); }
        private void button1_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new InventorySupplier(mainForm)); }
        private void button4_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new InventoryGRN(mainForm)); } // Changed target form
        private void button8_Click(object sender, EventArgs e)
        { mainForm.LoadForm(new InventoryReport(mainForm)); }

        // Add/Update/Remove buttons
        private void button9_Click(object sender, EventArgs e) // Add
        { OpenSubForm(new InventoryInventoryAdd(this)); }
        private void button7_Click(object sender, EventArgs e) // Update (Assuming this is Update btn)
        { OpenSubForm(new InventoryInventoryUpdate(this)); }
        private void button6_Click(object sender, EventArgs e) // Remove
        { OpenSubForm(new InventoryInventoryRemove(this)); } // Assuming Remove form exists

    }
}