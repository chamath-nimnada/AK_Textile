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
using static AK_Textile.LoginForm;

namespace AK_Textile
{
    public partial class SupplierOrderAdd : Form
    {

        //database connection string
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;Initial Catalog=Textlies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        public SupplierOrderAdd()
        {
            InitializeComponent();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.itemname.Clear();
            this.qty.Clear();
            this.desc.Clear();
        }
        //methi to auto increment of the IDs
        public void autoincrement()
        {
            //To auto increment the Purchase order ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(POrderID) FROM PurchaseOrder", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.Porderid.Text = "SUP001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "PO" + (numericPart + 1).ToString("D3"); // Increment and format as "POXXX"
                    this.Porderid.Text = newID;
                }
            }
            dr1.Close();
            con.Close();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO PurchaseOrder(POrderID, SupManID, PItemName, PitemQty, PitemDesc) VALUES(@poid, @smid, @iname, @iqty, @idesc)", con);
            cmd2.Parameters.AddWithValue("@poid", Porderid.Text);
            cmd2.Parameters.AddWithValue("@smid", LoggedInUser.UserId);
            cmd2.Parameters.AddWithValue("@iname", itemname.Text);
            cmd2.Parameters.AddWithValue("@iqty", qty.Text);
            cmd2.Parameters.AddWithValue("@idesc", desc.Text);
            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Purchase Order added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Purchase Order !");
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

            this.itemname.Clear();
            this.qty.Clear();
            this.desc.Clear();

            //calling the auto increment method
            autoincrement();
        }

        private void SupplierOrderAdd_Load(object sender, EventArgs e)
        {
            //calling the auto increment method
            autoincrement();
        }
    }
}
