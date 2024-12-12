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
    public partial class SupplierSupplierUpdate : Form
    {
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                 Initial Catalog=Textlies;
                                                 Integrated Security=True");
        public SupplierSupplierUpdate()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.nametxt.Clear();
            this.emailtxt.Clear();
            this.contaccttxt.Clear();
            this.addresstxt.Clear();
        }
        //method to load supplier data to the data grid view
        private void SearchSupplier(string searchValue)
        {
            //to view the searched data into the data grod view
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Supplier WHERE SupID LIKE @searchval OR SupName LIKE @searchval", con);
            cmd2.Parameters.AddWithValue("@searchval", "%" + searchValue + "%");
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            DataTable dt1 = new DataTable();

            try
            {
                //To get the  searched data
                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;

                if (dt1.Rows.Count == 0)
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

        //method to load data to the textboxes
        private void LoadSupplierData(string supdata)
        {
            con.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT SupplierID, SupplierName, Email, ContactNo, Address FROM Suppliers WHERE SupplierID = @searchval OR SupplierName LIKE @searchval", con);
            cmd3.Parameters.AddWithValue("@searchval", "%" + supdata + "%");

                try
                {
                    SqlDataReader dr3 = cmd3.ExecuteReader();

                    if (dr3.Read())
                    {
                        // Populate the text boxes with data
                        nametxt.Text = dr3["SupName"].ToString();
                        emailtxt.Text = dr3["SupEmail"].ToString();
                        contaccttxt.Text = dr3["SupContact"].ToString();
                        addresstxt.Text = dr3["SupAddress"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No supplier found with the entered ID or Name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nametxt.Clear();
                    emailtxt.Clear();
                    contaccttxt.Clear();
                    addresstxt.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        private void searchbtn_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                //calling the methods
                LoadSupplierData(search);

            }
            else
            {
                MessageBox.Show("Please enter a Supplier ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
