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
            this.Close();
        }

        private void Clear()
        {
            this.supidtxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            this.itemtxt.Clear();
            this.qtytxt.Clear();
            this.amounttxt.Clear();
            //to focus the cursor back to the main field
            supidtxt.Focus();
        }

        private void AddSupplierInvoice()
        {
            try
            {
                // Get values from textboxes
                string invoiceID = invidtxt.Text.Trim();
                DateTime date = dateTimePicker1.Value;
                string supplierID = supidtxt.Text.Trim();
                string item = itemtxt.Text.Trim();
                int quantity = int.Parse(qtytxt.Text.Trim());
                decimal amount = decimal.Parse(amounttxt.Text.Trim());

                // Calculate Total Amount
                decimal totalAmount = quantity * amount;

                con.Open();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO SupplierInvoice(SInvoiceID, SupID, SIDate, SIItem, SIQty, SITotalAmount, SIUnitPRice)" +
                    " VALUES (@siid, @sid, @sdate, @sitem, @sqty, @stotal, @suprice)", con);
                cmd2.Parameters.AddWithValue("@siid", invoiceID);
                cmd2.Parameters.AddWithValue("@sid", supplierID);
                cmd2.Parameters.AddWithValue("@sdate", date);
                cmd2.Parameters.AddWithValue("@sitem", item);
                cmd2.Parameters.AddWithValue("@sqty", quantity);
                cmd2.Parameters.AddWithValue("@stotal", amount);

                cmd2.ExecuteNonQuery();
                MessageBox.Show("Invoice added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //method to calculate total
        private void CalculateTotal()
        {
            if (int.TryParse(qtytxt.Text.Trim(), out int quantity) && decimal.TryParse(amounttxt.Text.Trim(), out decimal amount))
            {
                totaltxt.Text = (quantity * amount).ToString("F2");
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void amounttxt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void qtytxt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            AddSupplierInvoice();
        }
    }
}


