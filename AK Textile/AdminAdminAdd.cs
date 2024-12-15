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
    public partial class AdminAdminAdd : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public AdminAdminAdd()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Validations
            if (this.nametxt.Text == "")
            {
                this.errorProvider1.SetError(this.nametxt, "Admin name cannot be empty");
                return;
            }
            else if (this.usernametxt.Text == "")
            {
                this.errorProvider1.SetError(this.usernametxt, "Username cannot be empty");
                return;
            }
            else if (this.pswrdtxt.Text == "")
            {
                this.errorProvider1.SetError(this.pswrdtxt, "Password cannot be empty");
                return;
            }
            else if (this.contacttxt.Text == "")
            {
                this.errorProvider1.SetError(this.contacttxt, "Contact number cannot be empty");
                return;
            }
            else if (this.address1txt.Text == "")
            {
                this.errorProvider1.SetError(this.address1txt, "Address cannot be empty");
                return;
            }
            else if (this.address2txt.Text == "")
            {
                this.errorProvider1.SetError(this.address2txt, "Address cannot be empty");
                return;
            }
            else if (this.address3txt.Text == "")
            {
                this.errorProvider1.SetError(this.address3txt, "Address cannot be empty");
                return;
            }
            AddAdmin();
            cleardata();
            IDIncrement();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void positionIDtxt_TextChanged(object sender, EventArgs e)
        {

        }
        public void cleardata()
        {
            this.nametxt.Clear();
            this.usernametxt.Clear();
            this.pswrdtxt.Clear();
            this.contacttxt.Clear();
            this.address1txt.Clear();
            this.address2txt.Clear();
            this.address3txt.Clear();
        }

        //method to auto increment Admin ID
        private void IDIncrement()
        {
            //To auto increment the department ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(EmpID) FROM Employee", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.empid.Text = "EMP001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "EMP" + (numericPart + 1).ToString("D3"); // Increment and format as "DXXX"
                    this.empid.Text = newID;
                }
            }
            dr1.Close();
            con.Close();
        }
        private void AdminAdminAdd_Load(object sender, EventArgs e)
        {
            IDIncrement();
        }
        private void clearbtn_Click(object sender, EventArgs e)
        {
            cleardata();
        }
        // Method to determine Department ID based on Position ID
        public string GetDepartmentID(string positionID)
        {
            if (positionID == "P003")
            {
                return "D003";
            }
            else
            {
                return "D000";
            }
        }

        private void AddAdmin()
        {
            //declaring variables to get the department ID
            string positionID = positionIDtxt.Text;
            //calling the get department method to enter into the database
            string departmentID = GetDepartmentID(positionID);

            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Employee(EmpID, EmpName, EmpStreetNo, EmpStreetName, EmpCity," +
                        " EmpUsername, EmpPswrd, EmpContact, DepID, PositionID)VALUES (@empid, @empname, @address1, @address2," +
                        " @address3, @username, @pswrd, @contact, @depid, @posid)", con);
            cmd2.Parameters.AddWithValue("@empid", empid.Text);
            cmd2.Parameters.AddWithValue("@empname", nametxt.Text);
            cmd2.Parameters.AddWithValue("@address1", address1txt.Text);;
            cmd2.Parameters.AddWithValue("@address2", address2txt.Text);
            cmd2.Parameters.AddWithValue("@address3", address3txt.Text);
            cmd2.Parameters.AddWithValue("@username", usernametxt.Text);
            cmd2.Parameters.AddWithValue("@pswrd", pswrdtxt.Text);
            cmd2.Parameters.AddWithValue("@contact", contacttxt.Text);
            cmd2.Parameters.AddWithValue("@depid", departmentID);
            cmd2.Parameters.AddWithValue("@posid", positionID);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Admin added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Admin !");
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
