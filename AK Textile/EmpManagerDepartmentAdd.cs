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
    public partial class EmpManagerDepartmentAdd : Form
    {
        //database connection string
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;Initial Catalog=Textlies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        public EmpManagerDepartmentAdd()
        {
            InitializeComponent();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.depnametxt.Clear();
            this.deploctxt.Clear();
        }

        private void EmpManagerDepartmentAdd_Load(object sender, EventArgs e)
        {
            //To auto increment the department ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(DepID) FROM Department", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.depIDtxt.Text = "Dep001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "Dep" + (numericPart + 1).ToString("D3"); // Increment and format as "DepXXX"
                    this.depIDtxt.Text = newID;
                }
            }
            dr1.Close();
            con.Close();

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            //Validations
            if (this.depnametxt.Text == "")
            {
                this.errorProvider1.SetError(this.depnametxt, "Department namecannot be empty");
                return;
            }
            else if (this.deploctxt.Text == "")
            {
                this.errorProvider1.SetError(this.deploctxt, "Location cannot be empty");
                return;
            }
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Department(DepID, DepName, DepLocation) VALUES (@depid, @dname, @dloc)",con);
            cmd2.Parameters.AddWithValue("@depid", depIDtxt.Text);
            //cmd2.Parameters.AddWithValue("@empmanid", LoginForm.LoggedInUser.UserId); , EmpManID   @empmanid
            cmd2.Parameters.AddWithValue("@dname", depnametxt.Text);
            cmd2.Parameters.AddWithValue("@dloc", deploctxt.Text); 

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Department added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Department !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
