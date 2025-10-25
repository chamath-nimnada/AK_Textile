using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class EmpManagerLeaveRemove : Form
    {
        private EmpManagerLeave leaveform; // Reference to Supplier
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerLeaveRemove(EmpManagerLeave leaveform)
        {
            InitializeComponent();
            this.leaveform = leaveform;
        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                // Get the LeaveID of the selected row (as a string)
                string selectedCategoryID = dataGridView1.SelectedRows[0].Cells["LeaveId"].Value.ToString();

                try
                {

                        con.Open();
                        // SQL Query to delete data
                        string deleteQuery = "DELETE FROM Leave WHERE LeaveId = @LeaveId";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@LeaveId", selectedCategoryID);

                        // Execute the delete command
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh DataGridView after deletion
                            button9.PerformClick();

                            // Call the public method from InventoryCategory
                            //inventoryCategoryForm.RefreshDataGrid();

                            this.Close();
                        }
                        else
                        {
                            //MessageBox.Show("Failed to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Call the public method from InventoryCategory
            //inventoryCategoryForm.RefreshDataGrid();

            this.Close();
        }
    }
}
