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
    public partial class AdminEmployeeAdd : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;Initial Catalog=Textlies;Integrated Security=True;");
        
        public AdminEmployeeAdd()
        {
            InitializeComponent();
        }

        private void AdminEmployeeAdd_Load(object sender, EventArgs e)
        {
            //To auto Increment
            //To auto increment the department ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(EmpID) FROM Employee", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.empIDtxtbox.Text = "Emp001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3));
                    string newID = "Emp" + (numericPart + 1).ToString("D3"); //D3 is used to convert a string into 3 digits
                    this.empIDtxtbox.Text = newID;
                }
            }
            dr1.Close();
            con.Close();
            try
            {
                //To load department names automatically into the department combo box
                con.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT DepName FROM Department", con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    deptmntcmb.Items.Add(dr2["DepName"]);
                }
                dr2.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.fullnametxt.Clear();
            this.usernametxt.Clear();
            this.passwordtxt.Clear();
            this.deptmntcmb.Text = "";
            this.positioncmb.Text = "";
            this.contactnotxt.Clear();
            this.address1txt.Clear();
            this.address2txt.Clear();
            this.address3txt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Method to add data to the database
        private void AddEmployee()
        {
            con.Open();
            SqlCommand cmd3 = new SqlCommand("INSERT INTO Employee(EmpID, EmpName, EmpUsername, EmpPswrd, EmpPosition, EmpStreetNo, EmpStreetName, EmpCity)" +
                " VALUES (@eid, @ename, @euname, @epswrd, @eposition, @esno, @esname, @ecity) ", con);
            cmd3.Parameters.AddWithValue("@eid", empIDtxtbox.Text);
            cmd3.Parameters.AddWithValue("@ename", fullnametxt.Text);
            cmd3.Parameters.AddWithValue("@euname", usernametxt.Text);
            cmd3.Parameters.AddWithValue("@epswrd", passwordtxt.Text);
            cmd3.Parameters.AddWithValue("@eposition",deptmntcmb.Text);
            //Department details should be added.
            cmd3.Parameters.AddWithValue("@esno", address1txt.Text);
            cmd3.Parameters.AddWithValue("@esname", address2txt.Text);
            cmd3.Parameters.AddWithValue("@ecity", address3txt.Text);

            try
            {
                int r1 = cmd3.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Employee added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding employee !");
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

        private void addbtn_Click(object sender, EventArgs e)
        {
            //Validations
            if (this.fullnametxt.Text == "")
            {
                this.errorProvider1.SetError(this.fullnametxt, "Full name cannot be empty");
                return;
            }
            else if (this.usernametxt.Text == "")
            {
                this.errorProvider1.SetError(this.usernametxt, "username cannot be empty");
                return;
            }
            else if (this.passwordtxt.Text == "")
            {
                this.errorProvider1.SetError(this.passwordtxt, "Password cannot be empty");
                return;
            }
            else if (deptmntcmb.SelectedIndex == -1)
            {
                this.errorProvider1.SetError(deptmntcmb, "Please select a position");
                return;
            }
            else if (positioncmb.SelectedIndex == -1)
            {
                this.errorProvider1.SetError(positioncmb, "Please select a department");
                return;
            }
            else if (this.contactnotxt.Text == "")
            {
                this.errorProvider1.SetError(this.contactnotxt, "Contact number cannot be empty");
                return;
            }
            else if (this.address1txt.Text == "")
            {
                this.errorProvider1.SetError(this.address1txt, "address cannot be empty");
                return;
            }
            else if (this.address2txt.Text == "")
            {
                this.errorProvider1.SetError(this.address2txt, "address cannot be empty");
                return;
            }
            else if (this.address3txt.Text == "")
            {
                this.errorProvider1.SetError(this.address3txt, "address cannot be empty");
                return;
            }
            //Calling the  method
            AddEmployee();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
