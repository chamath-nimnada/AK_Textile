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

    public partial class ProductRaw : Form
    {
        private MainForm mainForm;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        Form formBackground = null; // Declare outside to access in 'finally'

        public ProductRaw(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.searchtxt.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductDashboard(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductSchedule(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mainForm.LoadForm(new ProductProduction(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductionProduct(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductOrder(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductReport(mainForm));
        }

        private void LoadRawMat()
        {
            // to load raw materials data in to the datagrid view
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Inventory WHERE InvCatID = 'INC001' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;

                // Adjust columns to fit the grid width
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void SearchInv(string searchValue)
        {
            //to view the searched data into the data grod view
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Inventory WHERE InvID LIKE @searchval OR InvItemName LIKE @searchval", con);
            cmd2.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            DataTable dt1 = new DataTable();

            try
            {
                if (dt1.Rows.Count > 0)
                {
                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dt1;

                    // Adjust columns to fit the grid width
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    con.Close();
                }
                else
                {
                    MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Clear DataGridView if no data found
                    con.Close();
                    LoadRawMat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductRaw_Load(object sender, EventArgs e)
        {
            LoadRawMat();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = searchtxt.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Inventory ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Calling the method to search supplier invoice
            SearchInv(searchValue);
            searchtxt.Clear();
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
    }
}
