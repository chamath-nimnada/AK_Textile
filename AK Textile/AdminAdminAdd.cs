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
            AddAdmin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.nametxt.Clear();
            this.usernametxt.Clear();
            this.pswrdtxt.Clear();
            this.contacttxt.Clear();
            this.address1txt.Clear();
            this.address2txt.Clear();
            this.address3txt.Clear();
        }

        //To auto increment the Admin ID
        private void autoincrement()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(SupID) FROM Supplier", con);
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
                    string newID = "EMP" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.empid.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }

        private void AdminAdminAdd_Load(object sender, EventArgs e)
        {
            //calling the auto increment method
            autoincrement();
        }

        private void AddAdmin()
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
            else if (this.contacttxt.Text == "")
            {
                this.errorProvider1.SetError(this.contacttxt, "Admin contat cannot be empty");
                return;
            }
            else if (this.pswrdtxt.Text == "")
            {
                this.errorProvider1.SetError(this.pswrdtxt, "Password cannot be empty");
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

            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Employee(EmpID, EmpName, EmpStreetNo, EmpStreetName, EmpCity, EmpUsername, empPswrd, EmpContact, DepID, PositionID)" +
                " VALUES (@empid, @ename,@esno, @estno, @ecity, @eusername, @epswrd, @econtact,@edepid, @eposid )", con);
            cmd2.Parameters.AddWithValue("@empid", empid.Text);
            cmd2.Parameters.AddWithValue("@ename", nametxt.Text);
            cmd2.Parameters.AddWithValue("@esno", address1txt.Text);
            cmd2.Parameters.AddWithValue("@estno", address2txt.Text);
            cmd2.Parameters.AddWithValue("@ecity", address3txt.Text);
            cmd2.Parameters.AddWithValue("@econtact", contacttxt.Text);
            cmd2.Parameters.AddWithValue("@eusername", usernametxt.Text);
            cmd2.Parameters.AddWithValue("@epswrd", pswrdtxt.Text);
            cmd2.Parameters.AddWithValue("@edepid", textBox1.Text);
            cmd2.Parameters.AddWithValue("@eposid", positionIDtxt.Text);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Admin details added successfully !");
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
            this.nametxt.Clear();
            this.usernametxt.Clear();
            this.pswrdtxt.Clear();
            this.contacttxt.Clear();
            this.address1txt.Clear();
            this.address2txt.Clear();
            this.address3txt.Clear();

            //calling the auto increment method
            autoincrement();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
