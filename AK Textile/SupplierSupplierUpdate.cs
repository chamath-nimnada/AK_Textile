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
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public SupplierSupplierUpdate()
        {
            InitializeComponent();
        }

        private void clear1btn_Click(object sender, EventArgs e)
        {
            this.supnametxt.Clear();
            dataGridView1.DataSource = null;
        }

        private void clear2btn_Click(object sender, EventArgs e)
        {
            this.supnametxt.Clear();
            this.mailtxt.Clear();
            this.contacttxt.Clear();
            this.addresstxt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            // declaring a variable for thee textbox
            string search = supnametxt.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Please enter Supplier ID or Name to search.");
                return;
            }
            {
                //to select data from the database after searching a id  or name adn to fill the data grid view
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM Supplier WHERE SupID = @search OR SupName LIKE '%' + @search + '%'", con);
                ad.SelectCommand.Parameters.AddWithValue("@search", search);

                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
            }
           /* con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT SupName, SupEmail,  FROM Department WHERE DepName = @Dname", con);
            cmd2.Parameters.AddWithValue("@Dname", depname);

            try
            {
                SqlDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.Read())
                {
                    // Populate the text boxes with the fetched data
                    depnametxt.Text = dr2["Depname"].ToString();
                    dloc.Text = dr2["DepLocation"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }*/
        }
    }
}
