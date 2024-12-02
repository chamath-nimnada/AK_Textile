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

        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerDepartment(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void EmpManagerDepartment_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Department", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerEmployee(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerSalary(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerLeave(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerReport(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DepId.Text = string.Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentAdd empmanagerdepartmentadd = new EmpManagerDepartmentAdd();
            empmanagerdepartmentadd.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentUpdate empmanagerdepartmentupdate = new EmpManagerDepartmentUpdate();
            empmanagerdepartmentupdate.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentRemove empmanagerdepartmentremove = new EmpManagerDepartmentRemove();
            empmanagerdepartmentremove.ShowDialog();
        }

        /*method to search button to filter data accordingto the entered
         name or ID */
        private void SearchDep(string searchValue)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Department WHERE DepID LIKE @searchval OR DepName LIKE @searchval", con);
            cmd2.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd2);
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

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
