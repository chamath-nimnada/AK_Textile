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
    public partial class EmployeeManagerRemoveEmp : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private EmpManagerEmployee employeeManRemoveEmpForm; // Reference to employee manager employee
        public EmployeeManagerRemoveEmp(EmpManagerEmployee employeeManRemoveEmpForm)
        {
            InitializeComponent();
            this.employeeManRemoveEmpForm = employeeManRemoveEmpForm;
        }


        private void button9_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                // Get the EmpID of the selected row (as a string)
                string selectedCategoryID = dataGridView1.SelectedRows[0].Cells["EmpID"].Value.ToString();

                try
                {
                    {
                        con.Open();

                        // SQL Query to delete data
                        string deleteQuery = "DELETE FROM Employee WHERE EmpID = @EmpID";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpID", selectedCategoryID);

                            // Execute the delete command
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh DataGridView after deletion
                                button9.PerformClick();

                                // Call the public method from EmpManagerEmployee
                                employeeManRemoveEmpForm.RefreshDataGrid();

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Call the public method from EmpManagerEmployee
            employeeManRemoveEmpForm.RefreshDataGrid();

            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the text box
            textBox1.Text = string.Empty;

            // Clear the data grid view
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    con.Open();
                    // SQL Query to search data
                    string query = "SELECT * FROM Employee WHERE EmpID LIKE @search OR EmpName LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(query, con))
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
            finally
            {
                con.Close();
            }
        }
    }
}
