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
    public partial class ProductSchedule : Form
    {
        private MainForm mainForm;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        Form formBackground = null; // Declare outside to access in 'finally'

        public ProductSchedule(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        public void RefreshDataGrid()
        {
            LoadSchedule();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Create an instance of the form and pass it to the method
            OpenSubForm(new ProductScheduleAdd(this));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            LoadSchedule();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductDashboard(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mainForm.LoadForm(new ProductProduction(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new ProductRaw(mainForm));
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

        private void updatebtn_Click(object sender, EventArgs e)
        {
            //Create an instance of the form and pass it to the method
            OpenSubForm(new ProductScheduleUpdate(this));
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            //Create an instance of the form and pass it to the method
            OpenSubForm(new ProductScheduleRemove(this));
        }


        private void schedulesearch(string searchValue)
        {
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM ProductionSchedule WHERE PScheduleID = @pid OR PScheduleName LIKE @pname", con);
            cmd1.Parameters.AddWithValue("@pid", searchValue);
            cmd1.Parameters.AddWithValue("@pname", "%" + searchValue + "%");

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();

            try
            {
                da1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dt1;
                    // Adjust columns to fit the grid width
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSchedule()
        {
            // to load schedule data in to the datagrid view

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ProductionSchedule", con);
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
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadSchedule();
                return;
            }
            //Calling the method to search Production schedule
            schedulesearch(searchValue);
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

        private void ProductSchedule_Load(object sender, EventArgs e)
        {
            LoadSchedule();
        }
    }
}