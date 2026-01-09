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
    public partial class ProductionProduct : Form
    {
        public MainForm mainForm; //Step 01

        //database connection
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        Form formBackground = null; // Declare outside to access in 'finally'
        public ProductionProduct(MainForm mainForm/*Step 02*/)
        {
            InitializeComponent();
            this.mainForm = mainForm; //Step 03
        }

        // --- FIX 1: This is the public method the update form needs ---
        // This allows the 'ProductProductUpdate' form to refresh this grid.
        public void RefreshDataGrid()
        {
            loadproduct();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Create an instance of the form and pass it to the method
            OpenSubForm(new ProductProductAdd(this));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new ProductProductUpdate(this));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new ProductProductRemove(this));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductSchedule(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductRaw(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductOrder(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductReport(mainForm));
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            searchtxt.Clear();
            loadproduct();
        }

        private void loadproduct()
        {
            // to load product data in to the datagrid view

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Product", con);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Adjust columns to fit the grid width
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchProduct(string searchValue)
        {
            //to view the searched data into the data grd view
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Product WHERE PID = @pid OR PName LIKE @pname", con);
            cmd2.Parameters.AddWithValue("@pid", searchValue);
            cmd2.Parameters.AddWithValue("@pname", "%" + searchValue + "%");

            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            DataTable dt1 = new DataTable();

            try
            {
                da1.Fill(dt1); 

                if (dt1.Rows.Count > 0) 
                {
                    dataGridView1.DataSource = dt1;

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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductionProduct_Load(object sender, EventArgs e)
        {
            loadproduct();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = searchtxt.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                // If search is empty, just reload all products
                loadproduct();
                return;
            }
            //Calling the method to search supplier invoice
            searchProduct(searchValue);
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

                loadproduct();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}