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
    public partial class ProductProductAdd : Form
    {
        private ProductionProduct productionProductForm;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        public ProductProductAdd(ProductionProduct productionProductForm)
        {
            InitializeComponent();
            autoincrement();
            this.productionProductForm = productionProductForm;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //To auto increment the Supplier ID
        private void autoincrement()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(PID) FROM Product", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.pidtxt.Text = "PRO001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "PRO" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.pidtxt.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }

        private void ProductProductAdd_Load(object sender, EventArgs e)
        {
            autoincrement();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clear()
        {
            this.invidtxt.Clear();
            this.nametxt.Clear();
            this.pricetxt.Clear();
            this.qtytxt.Clear();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void addproduct()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Product(PID, InvID, PName, PPrice, PQty) VALUES (@pid, @invid, @name, @price, @qty)", con);
            cmd2.Parameters.AddWithValue("@pid", pidtxt.Text);
            cmd2.Parameters.AddWithValue("@invid", invidtxt.Text);
            cmd2.Parameters.AddWithValue("@name", nametxt.Text);
            cmd2.Parameters.AddWithValue("@price", pricetxt.Text);
            cmd2.Parameters.AddWithValue("@qty", qtytxt.Text);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Product Details added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Product details !");
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
            clear();
            autoincrement();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            addproduct();
        }
    }
}
