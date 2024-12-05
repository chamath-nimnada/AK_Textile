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
    public partial class SupplierSupplier : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public SupplierSupplier(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SupplierSupplier_Load(object sender, EventArgs e)
        {
            // to load supplier data in to the datagrid view
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT Supplier.SupID, Supplier.SupName, Supplier.SupAddress, Supplier.SupEmail, SupplierContact.SupContact " +
            "FROM Supplier LEFTJOIN SupplierContact ON Supplier.SupID = SupplierContact.SupID;", con);
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
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierOrder(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierInvoice(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SupplierReport(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
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
            SupplierSupplierAdd suppliersupplieradd = new SupplierSupplierAdd();
            suppliersupplieradd.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            SupplierSupplierUpdate supplierupdate = new SupplierSupplierUpdate();
            supplierupdate.ShowDialog();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            SupplierSupllierRemove supplierremovev = new SupplierSupllierRemove();
            supplierremovev.ShowDialog();
        }
        //method
        private void SearchSupplier(string searchValue)
        {
            //to view the searched data into the data grod view
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Supplier WHERE SupID LIKE @searchval OR SupName LIKE @searchval", con);
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
                MessageBox.Show("Please enter a valid Supplier ID or name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Calling the method to search supplier invoice
            SearchSupplier(searchValue);
        }
    }
}
//