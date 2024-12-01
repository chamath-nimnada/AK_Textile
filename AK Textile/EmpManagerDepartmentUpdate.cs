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
    public partial class EmpManagerDepartmentUpdate : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerDepartmentUpdate()
        {
            InitializeComponent();
        }

        private void EmpManagerDepartmentUpdate_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.depnametxt.Clear();
            this.dloc.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //method to reload the items to the combo box after updating
        private void LoadDeps()
        {
            con.Open();
            SqlCommand cmd4 = new SqlCommand("SELECT DepName FROM Department", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd4);
            DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);

                    depcmb.DataSource = dt;
                    depcmb.DisplayMember = "DepName";
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

        private void updatebtn_Click(object sender, EventArgs e)
        {
                string selectedDep = depcmb.SelectedItem.ToString();
                string newdepname = depnametxt.Text.Trim();
                string newloc = dloc.Text.Trim();

                if (string.IsNullOrEmpty(newdepname) || string.IsNullOrEmpty(newloc))
                {
                    MessageBox.Show("Please update the selected departments.");
                    return;
                }
 ;              con.Open();
                SqlCommand cmd3 = new SqlCommand("UPDATE Department SET DepName = @ndepname, DepLocation = @nloc WHERE DepName = @olddepname", con);
                cmd3.Parameters.AddWithValue("@ndepname", newdepname);
                cmd3.Parameters.AddWithValue("@nloc", newloc);
                cmd3.Parameters.AddWithValue("@olddepname", selectedDep);

                    try
                    {
                        int r1 = cmd3.ExecuteNonQuery();

                        if (r1 > 0)
                        {
                            MessageBox.Show("Department details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // To refresh the combobox by updated items
                            LoadDeps();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update department details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void depcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string depname = depcmb.SelectedItem.ToString();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT DepName, DepLocation FROM Department WHERE DepName = @Dname", con);
            cmd2.Parameters.AddWithValue("@Dname", depname);

                try
                {
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    if (dr2.Read())
                    {
                        // Populate the text boxes with the fetched data
                        depnametxt.Text = dr2["Depname"].ToString();
                        dloc.Text = dr2["DepLocation"].ToString();
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
