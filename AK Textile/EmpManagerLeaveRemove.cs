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
    public partial class EmpManagerLeaveRemove : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerLeaveRemove()
        {
            InitializeComponent();
        }

        private void EmpManagerLeaveRemove_Load(object sender, EventArgs e)
        {

            //To load Leave type names automatically into the leave type combo box
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT LTName FROM LeaveType", con);

            try
            {
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    ltypecmb.Items.Add(dr1["LTName"]);
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

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ltypecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // To load data to the datagridview when a item is selected from the dropdown
            string ltname = ltypecmb.SelectedItem.ToString();
            {
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM LeaveType WHERE LTName = @ltname", con);
                cmd2.Parameters.AddWithValue("@Dname", ltname);

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
        private void LoadLeaveType()
        {
            {
                SqlCommand cmd3 = new SqlCommand("SELECT LTName FROM LeaveType", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd3);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    ltypecmb.DataSource = dt;
                    ltypecmb.DisplayMember = "LTName";
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

        private void removebtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data is selected to be removed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the LeaveType ID from the DataGridView
            string ltype = dataGridView1.Rows[0].Cells["LeaveTypeID"].Value.ToString();

            {
                SqlCommand cmd4 = new SqlCommand("DELETE FROM LeaveType WHERE LeaveTypeID = @ltid", con);
                cmd4.Parameters.AddWithValue("@ltid", ltype);

                try
                {
                    con.Open();
                    int r1 = cmd4.ExecuteNonQuery();

                    if (r1 > 0)
                    {
                        MessageBox.Show("Leave Type removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the DataGridView and refresh the ComboBox
                        dataGridView1.DataSource = null;
                        LoadLeaveType(); // Reload the ComboBox with updated leavetype list
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the Leave Type !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
