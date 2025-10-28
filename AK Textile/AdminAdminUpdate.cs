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
    public partial class AdminAdminUpdate : Form
    {
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");
        public AdminAdminUpdate()
        {
            InitializeComponent();
        }

        private void clear1_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.fnametxt.Clear();
            this.usernametxt.Clear();
            this.pswrdtxt.Clear();
            this.contacttxt.Clear();
            this.address1txt.Clear();
            this.address2txt.Clear();
            this.address3txt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchval = textBox1.Text.Trim();

            // Check if the input is empty
            if (string.IsNullOrEmpty(searchval))
            {
                MessageBox.Show("Please enter Admin ID or name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT EmpName, EmpUsername, EmpPswrd, EmpContact, EmpStreetNo, EmpStreetName, EmpCity" +
                        " FROM Employee WHERE EmpID = @searchval OR EmpName LIKE @searchval", con);
                // Add parameters to prevent SQL Injection
                cmd2.Parameters.AddWithValue("@searchval", searchval);

                SqlDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.Read())
                {
                    // Populate the text boxes with the fetched data
                    fnametxt.Text = dr2["EmpName"].ToString();
                    usernametxt.Text = dr2["EmpUsername"].ToString();
                    pswrdtxt.Text = dr2["EmpPswrd"].ToString();
                    contacttxt.Text = dr2["EmpContact"].ToString();
                    address1txt.Text = dr2["EmpStreetNo"].ToString();
                    address2txt.Text = dr2["EmpStreetName"].ToString();
                    address3txt.Text = dr2["EmpCity"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        //method to update data 
        private void UpdateAdmin()
        {
            string adminNameOrId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(adminNameOrId))
            {
                MessageBox.Show("Please search for an admin before updating.");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Employee SET EmpName = @empname, EmpUsername = @username, EmpPswrd = @pswrd, EmpContact = @ContactNo," +
                    " EmpStreetNo = @HomeNo, EmpStreetName = @StreetName, EmpCity = @City WHERE EmpName = @AdminName OR EmpID = @AdminID", con);
                    {
                        cmd.Parameters.AddWithValue("@empname", fnametxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@username", usernametxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@pswrd", pswrdtxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNo", contacttxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@HomeNo", address1txt.Text.Trim());
                        cmd.Parameters.AddWithValue("@StreetName", address2txt.Text.Trim());
                        cmd.Parameters.AddWithValue("@City", address3txt.Text.Trim());
                        cmd.Parameters.AddWithValue("@AdminName", adminNameOrId);
                        cmd.Parameters.AddWithValue("@AdminID", adminNameOrId);

                        int r1 = cmd.ExecuteNonQuery();
                        if (r1 > 0)
                        {
                            MessageBox.Show("Admin details updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Please try again.");
                        }
                    }
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

        private void updatebtn_Click(object sender, EventArgs e)
        {
            UpdateAdmin();
        }
    }
}
