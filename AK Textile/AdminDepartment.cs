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
    public partial class EmpManagerDepartment : Form
    {
        private MainForm mainForm; //Step 01
                                   //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");
        public EmpManagerDepartment(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void LoadAllData()
        {
            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Department", con))
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminAdmin(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminDashboard(mainForm));
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentAdd depadd = new EmpManagerDepartmentAdd();
            depadd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentUpdate depupdate = new EmpManagerDepartmentUpdate();
            depupdate.ShowDialog();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentRemove depremove = new EmpManagerDepartmentRemove();
            depremove.ShowDialog();
        }

        private void EmpManagerDepartment_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

            /*method to search button to filter data accordingto the entered
         name or ID */
            private void SearchDep(string searchValue)
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM Department WHERE DepID LIKE @searchval OR DepName LIKE @searchval", con);
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

        private void clearbtn_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            LoadAllData();

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a valid Admin ID or name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Calling the method to search supplier invoice
            SearchDep(searchValue);
        }
    }
}
