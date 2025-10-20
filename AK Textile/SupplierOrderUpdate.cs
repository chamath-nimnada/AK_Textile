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
    public partial class SupplierOrderUpdate : Form
    {
        private SupplierOrder supplierOrderForm; // Reference to Supplier
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        public SupplierOrderUpdate(SupplierOrder supplierOrderForm)
        {
            InitializeComponent();
            this.supplierOrderForm = supplierOrderForm;

            // Event for selecting a row in DataGridView
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

        }

        private void search()
        {
            if (string.IsNullOrWhiteSpace(seaerchtxt.Text))
            {
                MessageBox.Show("Please enter an Order ID.");
                return;
            }

            try
            {
                con.Open();;
                SqlCommand cmd1 = new SqlCommand("SELECT POrderID, PItemName, PItemQty, PItemDesc FROM PurchaseOrder WHERE POrderID = @OrderID", con);
                cmd1.Parameters.AddWithValue("@OrderID", seaerchtxt.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No order found.");
                        dataGridView1.DataSource = null;
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

        private void clear()
        {
            inametxt.Clear();
            qtytxt.Clear();
            desctxt.Clear();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            search();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                inametxt.Text = row.Cells["PItemName"].Value.ToString();
                qtytxt.Text = row.Cells["PItemQty"].Value.ToString();
                desctxt.Text = row.Cells["PItemDesc"].Value.ToString();
            }
        }

        private void clearbtn2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clearbtn1_Click(object sender, EventArgs e)
        {
            seaerchtxt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrderUpdate()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order from the list.");
                return;
            }

            string orderId = dataGridView1.SelectedRows[0].Cells["POrderID"].Value.ToString();
                try
                {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE PurchaseOrder SET PitemName = @iname, PItemQty = @qty, PitemDesc = @desc WHERE POrderID = @orderid", con);
                    
                cmd2.Parameters.AddWithValue("@orderid", orderId);
                cmd2.Parameters.AddWithValue("@iname", inametxt.Text);
                cmd2.Parameters.AddWithValue("@qty", qtytxt.Text);
                cmd2.Parameters.AddWithValue("@desc", desctxt.Text);

                int rows = cmd2.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Order updated successfully.");
                    searchbtn.PerformClick(); // Refresh the DataGridView
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
            }
    }
}
