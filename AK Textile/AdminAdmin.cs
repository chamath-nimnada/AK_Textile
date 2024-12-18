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
    public partial class AdminAdmin : Form
    {
        private MainForm mainForm; //Step 01

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        Form formBackground = null; // Declare outside to access in 'finally'

        public AdminAdmin(MainForm mainForm/*Step 02*/)
        {
            InitializeComponent();
            this.mainForm = mainForm; //Step 03
            LoadAdmins();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Step 04
            //Load Employee Form in Main Panal
            mainForm.LoadForm(new EmpManagerDepartment(mainForm));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminAdmin_Load(object sender, EventArgs e)
        {
            LoadAdmins();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminAdminUpdate adupdate = new AdminAdminUpdate();
            adupdate.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AdminAdminRemove adremove = new AdminAdminRemove();
            adremove.ShowDialog();
        }
        public void LoadAdmins()
        {
                try
                {
                    con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE PositionID ='P003'", con);
                    {
                        // Execute the query and load the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dt;

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

        // Public method to refresh data grid
        public void RefreshDataGrid()
        {
            LoadAdmins();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            LoadAdmins();
        }

        //method to search button to filter data according to the entered name or ID
        private void SearchAdmin(string searchValue)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Employee WHERE EmpID LIKE @searchval OR EmpName LIKE @searchval", con);
            cmd1.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            try
            {
                //To get the  searched data
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Matching item Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
