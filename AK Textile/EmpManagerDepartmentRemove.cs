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
    public partial class EmpManagerDepartmentRemove : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerDepartmentRemove()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmpManagerDepartmentRemove_Load(object sender, EventArgs e)
        {
            try
            {
                //To load department names automatically into the department combo box
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT DepName FROM Department", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    depcmb.Items.Add(dr1["DepName"]);
                }
                dr1.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }

        private void depcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // To load data to the datagridview when a item is selected from the dropdown
            string depname = depcmb.SelectedItem.ToString();
            {
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM Department WHERE DepName = @Dname", con);
                cmd2.Parameters.AddWithValue("@Dname", depname);

                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);   
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void LoadDepartments()
        {
            {
                SqlCommand cmd4 = new SqlCommand("SELECT DepName FROM Department", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd4);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    depcmb.DataSource = dt;
                    depcmb.DisplayMember = "DepartmentName";
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
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data is selected to be removed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the DepartmentID (or any unique identifier) from the DataGridView
            string depid = dataGridView1.Rows[0].Cells["DepID"].Value.ToString();

            {
                SqlCommand cmd3 = new SqlCommand("DELETE FROM Department WHERE DepID = @depid", con);
                cmd3.Parameters.AddWithValue("@depid", depid);

                try
                {
                    con.Open();
                    int r1 = cmd3.ExecuteNonQuery();

                    if (r1 > 0)
                    {
                        MessageBox.Show("Department removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the DataGridView and refresh the ComboBox
                        dataGridView1.DataSource = null;
                        LoadDepartments(); // Reload the ComboBox with updated department list
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the department !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }
    }
}