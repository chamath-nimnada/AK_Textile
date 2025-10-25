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
    public partial class SupplierOrderAdd : Form
    {
        private SupplierOrder supplierOrderForm;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        public SupplierOrderAdd(SupplierOrder supplierOrderForm)
        {
            InitializeComponent();
            AutoGenerateID();
            this.supplierOrderForm = supplierOrderForm;

        }
        private void AutoGenerateID()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(POrderID) FROM PurchaseOrder", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.orderidtxt.Text = "PO001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "PO" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.orderidtxt.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }
        private void addorder()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO PurchaseOrder(POrderID, PItemName, PItemQty, PItemDesc) VALUES (@pid, @piname, @pqty, @pdesc)", con);
            cmd2.Parameters.AddWithValue("@pid", orderidtxt.Text);
            cmd2.Parameters.AddWithValue("@piname", inametxt.Text);
            cmd2.Parameters.AddWithValue("@pqty", qtytxt.Text);
            cmd2.Parameters.AddWithValue("@pdesc", desctxt.Text);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Purchase order added successfully !");
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

            cleartext();
            //calling the auto increment method
            AutoGenerateID();
        }

        private void cleartext()
        {
            this.inametxt.Clear();
            this.qtytxt.Clear();
            this.desctxt.Clear();
        }
        private void SupplierOrderAdd_Load(object sender, EventArgs e)
        {
            AutoGenerateID();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            addorder();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            cleartext();
        }
    }
}
