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
    public partial class ProductProductUpdate : Form
    {
        private ProductionProduct productionProductForm;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        public ProductProductUpdate(ProductionProduct productionProductForm)
        {
            InitializeComponent();
            this.productionProductForm = productionProductForm;
        }

        private void clear1btn_Click(object sender, EventArgs e)
        {
            searchtxt.Clear();
        }

        private void Cleartexts()
        {
            this.invidtxt.Clear();
            this.nametxt.Clear();
            this.pricetxt.Clear();
            this.qtytxt.Clear();
        }

        private void clear2btn_Click(object sender, EventArgs e)
        {
            Cleartexts();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //method to load data to the textboxes
        private void LoadProductData(string data)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT InvID, PName, PPrice, PQty FROM Product WHERE PID = @searchval OR PName LIKE @searchval", con);
            cmd1.Parameters.AddWithValue("@searchval", "%" + data + "%");

            try
            {
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    // Populate the text boxes with data
                    invidtxt.Text = dr1["InvID"].ToString();
                    nametxt.Text = dr1["PName"].ToString();
                    pricetxt.Text = dr1["PPrice"].ToString();
                    qtytxt.Text = dr1["PQty"].ToString();
                }
                else
                {
                    MessageBox.Show("No Product found with the entered ID or Name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleartexts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string search = searchtxt.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                //calling the methods
                LoadProductData(search);

            }
            else
            {
                MessageBox.Show("Please enter a Product ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(invidtxt.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

                try
                {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET InvID= @invid ,PName = @Name, PPrice = @Price, PQty = @Quantity WHERE PID = @PID", con);
                cmd.Parameters.AddWithValue("@invid", invidtxt.Text);
                cmd.Parameters.AddWithValue("@Name", nametxt.Text);
                cmd.Parameters.AddWithValue("@Price", pricetxt.Text);
                cmd.Parameters.AddWithValue("@Quantity", qtytxt.Text);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Product updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Update failed.");
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            finally
            {
                con.Close();
            }
        }
    }
}
