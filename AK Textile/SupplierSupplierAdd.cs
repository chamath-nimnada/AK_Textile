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
    public partial class SupplierSupplierAdd : Form
    {
        private SupplierSupplier supplierform; // Reference to Supplier
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                 Initial Catalog=Textlies;
                                                 Integrated Security=True");
        public SupplierSupplierAdd(SupplierSupplier supplierform)
        {
            InitializeComponent();
        }

        //To auto increment the Supplier ID
        private void autoincrement()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(SupID) FROM Supplier", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.supidtxt.Text = "SUP001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "SUP" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.supidtxt.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }

        private void SupplierSupplierAdd_Load(object sender, EventArgs e)
        {
            //calling the auto increment method
            autoincrement();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.nametxt.Clear();
            this.emailtxt.Clear();
            this.contacttxt.Clear();
            this.addresstxt.Clear();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Supplier(SupID, SupName, SupAddress, SupEmail, SupContact) VALUES (@supid, @sname, @saddress, @smail, @scontact)", con);
            cmd2.Parameters.AddWithValue("@supid", supidtxt.Text);
            cmd2.Parameters.AddWithValue("@sname", nametxt.Text);
            cmd2.Parameters.AddWithValue("@saddress", addresstxt.Text);
            cmd2.Parameters.AddWithValue("@scontact", contacttxt.Text);
            cmd2.Parameters.AddWithValue("@smail", emailtxt.Text);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Supplier details added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Supplier !");
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
            this.emailtxt.Clear();
            this.contacttxt.Clear();
            this.addresstxt.Clear();

            //calling the auto increment method
            autoincrement();
        }
    }
}
