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
    public partial class SupplierInvoiceUpdate : Form
    {
        private SupplierInvoice supinvoiceform; // Reference to Supplier Invoice
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        public SupplierInvoiceUpdate(SupplierInvoice supinvoiceform)
        {
            InitializeComponent();
            this.supinvoiceform = supinvoiceform;
            // Add event handlers
            qtytxt.TextChanged += CalculateTotalAmount;
            amounttxt.TextChanged += CalculateTotalAmount;

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.searchtxt.Clear();
        }

        private void LoadSupplierInvoiceData(string supdata)
        {
            con.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT SupID, SIDate, SIItem, SIQty, SITotalAmount, SIUnitPrice  FROM SupplierInvoice WHERE SInvoiceID = @searchval", con);
            cmd3.Parameters.AddWithValue("@searchval", "%" + supdata + "%");

            try
            {
                SqlDataReader dr3 = cmd3.ExecuteReader();

                if (dr3.Read())
                {
                    // Populate the text boxes with data
                    supidtxt.Text = dr3["SupID"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr3["Date"]);
                    itemtxt.Text = dr3["SIItem"].ToString();
                    qtytxt.Text = dr3["SIQty"].ToString();
                    amounttxt.Text = dr3["SIUnitPrice"].ToString();
                    totamount.Text = dr3["SITotalAmount"].ToString();
                }
                else
                {
                    MessageBox.Show("No supplier found with the entered ID or Name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    supidtxt.Clear();
                    dateTimePicker1.Value = DateTime.Today;
                    itemtxt.Clear();
                    qtytxt.Clear();
                    amounttxt.Clear();
                    totamount.Clear();
                    dr3.Close();
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

        private void CalculateTotalAmount(object sender, EventArgs e)
        {
            if (decimal.TryParse(qtytxt.Text, out decimal quantity) && decimal.TryParse(amounttxt.Text, out decimal amount))
            {
                totamount.Text = (quantity * amount).ToString();
            }
            else
            {
                totamount.Text = "0";
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {

            string search = searchtxt.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                //calling the methods
                LoadSupplierInvoiceData(search);

            }
            else
            {
                MessageBox.Show("Please enter a Invoice ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //method for the update button
        private void invoiceUpdate()
        {
            if (string.IsNullOrWhiteSpace(supidtxt.Text) || string.IsNullOrWhiteSpace(itemtxt.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

                try
                {
                con.Open();
                SqlCommand cmd4 = new SqlCommand("UPDATE SupplierInvoice SET SupID = @supid SIDate = @Date, SIItem = @Item, SIQty = @Quantity, SIUnitPrice = @Amount, SITotalAmount = @TotalAmount WHERE SInvoiceID = @invoiceid");
                        cmd4.Parameters.AddWithValue("@supid", supidtxt.Text);
                        cmd4.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                        cmd4.Parameters.AddWithValue("@Item", itemtxt.Text);
                        cmd4.Parameters.AddWithValue("@Quantity", qtytxt.Text);
                        cmd4.Parameters.AddWithValue("@Amount", amounttxt.Text);
                        cmd4.Parameters.AddWithValue("@TotalAmount", totamount.Text);

                        int r1 = cmd4.ExecuteNonQuery();
                        if (r1 > 0)
                        {
                            MessageBox.Show("Invoice updated successfully.");
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

        private void clearing()
        {
            supidtxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            itemtxt.Clear();
            qtytxt.Clear();
            amounttxt.Clear();
            totamount.Clear();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            clearing();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            invoiceUpdate();
        }

        /*public SupplierInvoiceUpdate(SupplierInvoice supplierInvoice)
        {
            this.supplierInvoice = supplierInvoice;
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            clearing();
        }
    }
}
