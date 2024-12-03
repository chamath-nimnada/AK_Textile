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
        //database connection string
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public SupplierSupplierAdd()
        {
            InitializeComponent();
        }

        private void SupplierSupplierAdd_Load(object sender, EventArgs e)
        {
            //To auto increment the Supplier ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(SupID) FROM Supplier", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.supid.Text = "SUP001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "SUP" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.supid.Text = newID;
                }
            }
            dr1.Close();
            con.Close();
        }
        
        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.supname.Clear();
            this.supmail.Clear();
            this.supnumber.Clear();
            this.supaddress.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Supplier(SupID, SupName, SupAddress, SupEmail) VALUES (@supid, @sname, @saddress, @smail)", con);
            SqlCommand cmd3 = new SqlCommand("INSERT INTO SupplierContact(SupID, SupContact) VALUES (@supid, @supcontact)", con);
            cmd2.Parameters.AddWithValue("@supid", supid.Text);
            cmd2.Parameters.AddWithValue("@sname", supname.Text);
            cmd2.Parameters.AddWithValue("@saddress", supaddress.Text);
            cmd2.Parameters.AddWithValue("@smail", supmail.Text);
            cmd3.Parameters.AddWithValue("@supcontact", supnumber.Text);
            cmd3.Parameters.AddWithValue("@supid", supid.Text);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                int r2 = cmd3.ExecuteNonQuery();
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
            this.supname.Clear();
            this.supmail.Clear();
            this.supnumber.Clear();
            this.supaddress.Clear();

        }
        //defects
        //after adding the supplier ID should automatically increase
        //after cancelling the data grid view should be updated instantly
        //update form defects is also there
        //remove form defects
    }
}
