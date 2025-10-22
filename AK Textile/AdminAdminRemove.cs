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
    public partial class AdminAdminRemove : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public AdminAdminRemove()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                // Get the Employee ID of the selected row (as a string)
                string selectedEmpID = dataGridView1.SelectedRows[0].Cells["EmpID"].Value.ToString();

                try
                {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("DELETE * FROM Employee WHERE EmpID = @empid", con);
                        {
                            cmd2.Parameters.AddWithValue("@empid", selectedEmpID);

                            // Execute the delete command
                            int result = cmd2.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //Refresh DataGridView after deletion
                                //button9.PerformClick();

                                // Call the public method from InventoryCategory
                                //inventoryCategoryForm.RefreshDataGrid();

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            // Call the public method from InventoryCategory
            //AdminAdmin.RefreshDataGrid();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE EmpID LIKE @search OR EmpName LIKE @search", con);
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + textBox1.Text.Trim() + "%");

                        // Use SqlDataAdapter to fetch and display data
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Display the result in DataGridView
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            // Clear the text box
            textBox1.Text = string.Empty;

            // Clear the data grid view
            dataGridView1.DataSource = null;
        }
    }
}
