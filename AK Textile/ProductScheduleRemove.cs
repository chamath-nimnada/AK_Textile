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
    public partial class ProductScheduleRemove : Form
    {
        private ProductSchedule productScheduleform;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        public ProductScheduleRemove(ProductSchedule productScheduleform)
        {
            InitializeComponent();
            this.productScheduleform = productScheduleform;
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            searchtxt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchtxt.Text))
            {
                MessageBox.Show("Please enter a Product Schedule ID or Name.");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT PScheduleID, PScheduleName, ProdType, ProdQty, ProdStartDate, ProdEndDate FROM" +
                    " ProductionSchedule WHERE PScheduleID =@Search OR PscheduleName LIKE @SearchPattern", con);
                {
                    cmd.Parameters.AddWithValue("@Search", searchtxt.Text);
                    cmd.Parameters.AddWithValue("@SearchPattern", "%" + searchtxt.Text + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
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

        private void removebtn_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a production Schedule to remove.");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected production Schedule", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
                return;

            string SchID = dataGridView1.SelectedRows[0].Cells["PScheduleID"].Value.ToString();

            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("DELETE FROM ProductionSchedule WHERE PScheduleID = @schid", con);
                {
                    cmd2.Parameters.AddWithValue("@schid", SchID);

                    int rowsAffected = cmd2.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Production Schedule removed successfully.");
                        searchbtn.PerformClick(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Deletion failed.");
                    }
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
