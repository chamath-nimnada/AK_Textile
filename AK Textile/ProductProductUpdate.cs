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
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");
        private string selectedProductID = null;

        public ProductProductUpdate(ProductionProduct productionProductForm)
        {
            InitializeComponent();
            this.productionProductForm = productionProductForm;
        }

        // Clear button for search
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

            this.selectedProductID = null;
        }

        // "Clear" button for details
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
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT PID, InvID, PName, PPrice, PQty FROM Product WHERE PID = @pid OR PName LIKE @pname", con);

                cmd1.Parameters.AddWithValue("@pid", data);
                cmd1.Parameters.AddWithValue("@pname", "%" + data + "%");

                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    selectedProductID = dr1["PID"].ToString();

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
            if (string.IsNullOrWhiteSpace(selectedProductID))
            {
                MessageBox.Show("Please search for and load a product first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(pricetxt.Text, out decimal price))
            {
                MessageBox.Show("Price must be a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(qtytxt.Text, out int quantity))
            {
                MessageBox.Show("Quantity must be a valid whole number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET InvID= @invid ,PName = @Name, PPrice = @Price, PQty = @Quantity WHERE PID = @PID", con);

                cmd.Parameters.AddWithValue("@invid", invidtxt.Text);
                cmd.Parameters.AddWithValue("@Name", nametxt.Text);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                cmd.Parameters.AddWithValue("@PID", selectedProductID);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Product updated successfully.");
                    productionProductForm.RefreshDataGrid(); // Refresh parent grid
                    this.Close(); // Close form
                }
                else
                {
                    MessageBox.Show("Update failed. Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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