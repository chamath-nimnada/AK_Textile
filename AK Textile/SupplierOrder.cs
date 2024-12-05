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
    public partial class SupplierOrder : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies;Integrated Security=True");
        public SupplierOrder(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SupplierOrder_Load(object sender, EventArgs e)
        {
            //to load data into the data grid view of the form
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM PurchaseOrder", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierSupplier(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierPayment(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierInvoice(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierReport(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            supidtxt.Text = string.Empty;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            SupplierOrderAdd suporderadd = new SupplierOrderAdd();
            suporderadd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            SupplierOrderUpdate suporderupdate = new SupplierOrderUpdate();
            suporderupdate.ShowDialog();
        }

        //method
        private void SearchPurchaseOrder(string searchValue)
        {
            // to search data using the purchase order ID
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM PurchaseOrder WHERE POrderID LIKE @searchval", con);
            cmd2.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();

            try
            {
                //To get the  searched data
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Matching item Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = supidtxt.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter Purchase Order ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Calling the method to search supplier invoice
            SearchPurchaseOrder(searchValue);
        }
    }
}
