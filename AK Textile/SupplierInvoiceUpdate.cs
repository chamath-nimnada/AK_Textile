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
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");
        public SupplierInvoiceUpdate(SupplierInvoice supinvoiceform)
        {
            InitializeComponent();
            this.supinvoiceform = supinvoiceform;

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


        //auto-calculation
        private void CalculateTotalAmount(object sender, EventArgs e)
        {
            if (decimal.TryParse(qtytxt.Text, out decimal quantity) && decimal.TryParse(amounttxt.Text, out decimal amount))
            {
                totamount.Text = (quantity * amount).ToString("0.00");
            }
            else
            {
                totamount.Text = "0.00";
            }
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

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = searchtxt.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Supplier Invoice ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SupID, SIDate, SIItem, SIQty, SIUnitPrice, SITotalAmount FROM SupplierInvoice WHERE SInvoiceID = @Search", con);
                cmd.Parameters.AddWithValue("@Search", searchValue);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    supidtxt.Text = reader["SupID"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["SIDate"]);
                    itemtxt.Text = reader["SIItem"].ToString();
                    qtytxt.Text = reader["SIQty"].ToString();
                    amounttxt.Text = reader["SIUnitPrice"].ToString();
                    totamount.Text = reader["SITotalAmount"].ToString();
                }
                else
                {
                    MessageBox.Show("Supplier Invoice not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearing();
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

            string invoiceID = searchtxt.Text.Trim();

            if (string.IsNullOrWhiteSpace(supidtxt.Text) || string.IsNullOrWhiteSpace(itemtxt.Text) || string.IsNullOrWhiteSpace(invoiceID))
            {
                MessageBox.Show("Please search for an invoice and fill all fields before updating.");
                return;
            }

            if (!decimal.TryParse(qtytxt.Text, out decimal quantity) ||
                !decimal.TryParse(amounttxt.Text, out decimal unitPrice) ||
                !decimal.TryParse(totamount.Text, out decimal totalAmount))
            {
                MessageBox.Show("Quantity, Amount, and Total Amount must be valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                con.Open();
                string query = @"UPDATE SupplierInvoice 
                                 SET SupID = @supid, 
                                     SIDate = @Date, 
                                     SIItem = @Item, 
                                     SIQty = @Quantity, 
                                     SIUnitPrice = @Amount, 
                                     SITotalAmount = @TotalAmount 
                                 WHERE SInvoiceID = @invoiceid";

                SqlCommand cmd4 = new SqlCommand(query, con);

                cmd4.Parameters.AddWithValue("@supid", supidtxt.Text);
                cmd4.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                cmd4.Parameters.AddWithValue("@Item", itemtxt.Text);
                cmd4.Parameters.AddWithValue("@Quantity", quantity);
                cmd4.Parameters.AddWithValue("@Amount", unitPrice);
                cmd4.Parameters.AddWithValue("@TotalAmount", totalAmount);

                cmd4.Parameters.AddWithValue("@invoiceid", invoiceID);

                int r1 = cmd4.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Supplier Invoice updated successfully.");
                    supinvoiceform.RefreshDataGrid();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update failed. Invoice ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /*public SupplierInvoiceUpdate(SupplierInvoice supplierInvoice)
        {
            this.supplierInvoice = supplierInvoice;
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            clearing();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}


