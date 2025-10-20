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

    public partial class SupplierSupplier : Form
    {
        private MainForm mainForm;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        Form formBackground = null; // Declare outside to access in 'finally'
        public SupplierSupplier(MainForm mainForm)
        {
            InitializeComponent();
            LoadSupplier();
            this.mainForm = mainForm;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierDashboard(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierOrder(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierInvoice(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierReport(mainForm));
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new SupplierSupplierAdd(this));
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new SupplierSupplierUpdate(this));
        }

        private void rmvbtn_Click(object sender, EventArgs e)
        {
            // Create an instance of the form and pass it to the method
            OpenSubForm(new SupplierSupllierRemove(this));
        }

        private void LoadSupplier()
        {
            // to load supplier data in to the datagrid view
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Supplier", con);
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
        private void SupplierSupplier_Load(object sender, EventArgs e)
        {
            LoadSupplier();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Supplier ID or Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SupID, SupName, SupAddress, SupEmail, SupContact FROM Supplier WHERE SupID = @Search OR SupName LIKE @SearchName", con);
                
                    cmd.Parameters.AddWithValue("@Search", searchValue);
                    cmd.Parameters.AddWithValue("@SearchName", "%" + searchValue + "%"); // For partial name search

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt; // Display filtered results in DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No matching supplier found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // Clear DataGridView
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
