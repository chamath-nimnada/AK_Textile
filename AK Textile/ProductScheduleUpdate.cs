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
    public partial class ProductScheduleUpdate : Form
    {
        private ProductSchedule productScheduleform;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                 Initial Catalog=Textlies;
                                                 Integrated Security=True");
        public ProductScheduleUpdate(ProductSchedule productScheduleform)
        {
            InitializeComponent();
            this.productScheduleform = productScheduleform;
        }

        private void clearbtn1_Click(object sender, EventArgs e)
        {
            searchtxt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cleartexts()
        {
            nametxt.Clear();
            nametxt.Clear();
            nametxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            //to focus the cursor back to the main field
            nametxt.Focus();
        }

        private void clearbtn2_Click(object sender, EventArgs e)
        {
            cleartexts();
        }

        //method to load data to the textboxes
        private void LoadProductData(string data)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT PScheduleName, ProdType, ProdQty, ProdStartDate, ProdEndDate FROM ProductionSchedule WHERE PScheduleID = @searchval OR PScheduleName LIKE @searchval", con);
            cmd1.Parameters.AddWithValue("@searchval", "%" + data + "%");

            try
            {
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    // Populate the text boxes with data
                    nametxt.Text = dr1["PScheduleName"].ToString();
                    ptypetxt.Text = dr1["ProdType"].ToString();
                    qtytxt.Text = dr1["ProdQty"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr1["ProdStartDate"]);
                    dateTimePicker2.Value = Convert.ToDateTime(dr1["ProdEndDate"]);
                }
                else
                {
                    MessageBox.Show("No Production Schedule found with the entered ID or Name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartexts();
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
                MessageBox.Show("Please enter a Production Schedule ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(nametxt.Text) || string.IsNullOrWhiteSpace(ptypetxt.Text) || string.IsNullOrWhiteSpace(qtytxt.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductionSchedule SET PScheduleName= @name ,ProdType = @type, ProdQty = @qty, ProdStartDate = @sdate, ProdEndDate = @edate WHERE PScheduleID = @PID", con);
                cmd.Parameters.AddWithValue("@name", nametxt.Text);
                cmd.Parameters.AddWithValue("@type", ptypetxt.Text);
                cmd.Parameters.AddWithValue("@qty", qtytxt.Text);
                cmd.Parameters.AddWithValue("@sdate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@edate", dateTimePicker2.Value);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Production Schedule updated successfully.");
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
