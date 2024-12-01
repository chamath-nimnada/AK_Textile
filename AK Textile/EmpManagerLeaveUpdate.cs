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
    public partial class EmpManagerLeaveUpdate : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerLeaveUpdate()
        {
            InitializeComponent();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.lname.Clear();
            this.ldesc.Clear();
            this.lamount.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmpManagerLeaveUpdate_Load(object sender, EventArgs e)
        {
            //To load leavetype names automatically into the leave type combo box
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT LTName FROM LeaveType", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            try
            {
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
        //method to reload the items to the combo box after updating
        private void LoadLtypes()
        {
            con.Open();
            SqlCommand cmd4 = new SqlCommand("SELECT LTName FROM LeaveType", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd4);
            DataTable dt = new DataTable();

            try
            {
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

        private void ltypecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ltname = ltypecmb.SelectedItem.ToString();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT LTName, LTDesc, LTAmount FROM LeaveType WHERE LTName = @ltname", con);
            cmd2.Parameters.AddWithValue("@ltname", ltname);

            try
            {
                SqlDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.Read())
                {
                    // Populate the text boxes with the fetched data
                    lname.Text = dr2["LTName"].ToString();
                    ldesc.Text = dr2["LTDesc"].ToString();
                    lamount.Text = dr2["LTAmount"].ToString();
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

        private void updatebtn_Click(object sender, EventArgs e)
        {
            string selectedltype = ltypecmb.SelectedItem.ToString();
            string newltype = lname.Text.Trim();
            string newldesc = ldesc.Text.Trim();
            string newlamount = lamount.Text.Trim();

            if (string.IsNullOrEmpty(newltype) || string.IsNullOrEmpty(newldesc) || string.IsNullOrEmpty(newlamount))
            {
                MessageBox.Show("Please update the selected Leave Types.");
                return;
            }
            con.Open();
            SqlCommand cmd3 = new SqlCommand("UPDATE LeaveType SET LTName = @ltname, LTDesc = @ltdesc, LTAmount = @ltamount WHERE LTName = @oldltype", con);
            cmd3.Parameters.AddWithValue("@ltname", newltype);
            cmd3.Parameters.AddWithValue("@ltdesc", newldesc);
            cmd3.Parameters.AddWithValue("@oldltype", newlamount);

            try
            {
                int r1 = cmd3.ExecuteNonQuery();

                if (r1 > 0)
                {
                    MessageBox.Show("Leave type details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // To refresh the combobox by updated items
                    LoadLtypes();
                }
                else
                {
                    MessageBox.Show("Failed to update Leave type details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
