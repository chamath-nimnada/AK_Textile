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

        private string selectedScheduleID = null;

        public ProductScheduleUpdate(ProductSchedule productScheduleform)
        {
            InitializeComponent();
            this.productScheduleform = productScheduleform;
        }

        private void clearbtn1_Click(object sender, EventArgs e)
        {
            searchtxt.Clear();
            cleartexts();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cleartexts()
        {
            nametxt.Clear();
            comboBox1.SelectedIndex = -1;
            qtytxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            selectedScheduleID = null;
            nametxt.Focus();
        }

        private void clearbtn2_Click(object sender, EventArgs e)
        {
            cleartexts();
        }

        private void LoadProductData(string data)
        {
            try
            {
                con.Open();

                string query = "SELECT PScheduleName, ProdType, ProdQty, ProdStartDate, ProdEndDate FROM ProductionSchedule WHERE PScheduleID = @pid";
                SqlCommand cmd1 = new SqlCommand(query, con);

                cmd1.Parameters.AddWithValue("@pid", data);

                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {

                    selectedScheduleID = data;

                    // Populate the text boxes with data
                    nametxt.Text = dr1["PScheduleName"].ToString();
                    qtytxt.Text = dr1["ProdQty"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr1["ProdStartDate"]);
                    dateTimePicker2.Value = Convert.ToDateTime(dr1["ProdEndDate"]);

                    comboBox1.Text = dr1["ProdType"].ToString();
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
                LoadProductData(search);
            }
            else
            {
                MessageBox.Show("Please enter a Production Schedule ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedScheduleID))
            {
                MessageBox.Show("Please search for and load a schedule before updating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(nametxt.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }
            if (!int.TryParse(qtytxt.Text, out int quantity))
            {
                MessageBox.Show("Quantity must be a valid whole number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string prodType = comboBox1.Text;

            try
            {
                con.Open();
                string query = "UPDATE ProductionSchedule SET PScheduleName= @name ,ProdType = @type, ProdQty = @qty, ProdStartDate = @sdate, ProdEndDate = @edate WHERE PScheduleID = @PID";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", nametxt.Text);
                cmd.Parameters.AddWithValue("@type", prodType);   // Pass the string
                cmd.Parameters.AddWithValue("@qty", quantity);   // Pass the number
                cmd.Parameters.AddWithValue("@sdate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@edate", dateTimePicker2.Value);

                cmd.Parameters.AddWithValue("@PID", selectedScheduleID);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Production Schedule updated successfully.");
                    productScheduleform.RefreshDataGrid(); 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update failed. Schedule not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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