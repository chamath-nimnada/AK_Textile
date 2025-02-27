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
    public partial class SupplierInvoiceAdd : Form

    {
        private SupplierInvoice supinvoiceform; // Reference to Supplier Invoice
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                 Initial Catalog=Textlies;
                                                 Integrated Security=True");
        public SupplierInvoiceAdd(SupplierInvoice supinvoiceform)
        {
            InitializeComponent();
            this.supinvoiceform = supinvoiceform;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //To auto increment the Supplier ID
        private void autoincrement()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(SInvoiceID) FROM SupplierInvoice", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.invidtxt.Text = "SI001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "SI" + (numericPart + 1).ToString("D3"); // Increment and format as "SIXXX"
                    this.invidtxt.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }
        private void SupplierInvoiceAdd_Load(object sender, EventArgs e)
        {
            autoincrement();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            // Call the public method from InventoryCategory
            //SupplierInvoice.RefreshDataGrid();
            //not working
            this.Close();
        }

        private void AddSupplierInvoice()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Supplier(SInvoiceID, SupID, SIDate, SIQty, SITotalAmount, SIUnitPrice) VALUES (@supid, @sname, @saddress, @smail, @scontact)", con);
            //cmd2.Parameters.AddWithValue("@supinvid", supidtxt.Text);
            //cmd2.Parameters.AddWithValue("@supdate", supidtxt.Text);
            //cmd2.Parameters.AddWithValue("@supqty", nametxt.Text);
            //cmd2.Parameters.AddWithValue("@suptotal", addresstxt.Text);
            //cmd2.Parameters.AddWithValue("@smail", emailtxt.Text);
            //cmd2.Parameters.AddWithValue("@supcontact", contacttxt.Text);

            //check with form


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
        }

        private void Clear()
        {
            this.supidtxt.Clear();
            this.itemtxt.Clear();
        }
        //there are some questions talk with  chamath about that
    }
}
