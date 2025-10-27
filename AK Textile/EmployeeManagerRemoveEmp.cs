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

        // Store the EmpID of the currently selected row in the grid
        private string selectedEmpID = null;

        private EmpManagerEmployee employeeManRemoveEmpForm; // Reference to employee manager employee
        public EmployeeManagerRemoveEmp(EmpManagerEmployee employeeManRemoveEmpForm)
        {
            InitializeComponent();
            this.employeeManRemoveEmpForm = employeeManRemoveEmpForm;
        }

        //remove button
        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmpID))
            {
                MessageBox.Show("Please search for and select an employee from the grid to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to permanently delete employee {selectedEmpID}?",
                                                   "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
            {
                return; 
            }

            try
            {
                con.Open();
                string query = "DELETE FROM Employee WHERE EmpID = @EmpID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmpID", selectedEmpID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Employee removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    employeeManRemoveEmpForm.RefreshDataGrid(); // Refresh the main grid
                    this.Close(); 
                }
                else
                {

                    MessageBox.Show("Deletion failed. Employee might have been removed already.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("Cannot delete this employee because they have related records (e.g., leave requests, salary entries). Please remove those first.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error removing employee: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        //Cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearSelection()
        {
            selectedEmpID = null;
        }

        //Clear button
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ClearSelection();
            dataGridView1.DataSource = null;
        }

        //search button
        private void button5_Click(object sender, EventArgs e)
        {
            string searchVal = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchVal))
            {
                MessageBox.Show("Please enter an Employee ID, Username, or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClearSelection(); // Clear previous selection

            try
            {
                string query = @"SELECT EmpID, EmpName, EmpUsername, PositionID, DepID
                                 FROM Employee
                                 WHERE EmpID = @SearchVal
                                    OR EmpUsername = @SearchVal
                                    OR EmpName LIKE @SearchPattern";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@SearchVal", searchVal);
                adapter.SelectCommand.Parameters.AddWithValue("@SearchPattern", "%" + searchVal + "%");

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
                }
                else
                {
                    MessageBox.Show("No employee found matching the criteria.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching employees: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedEmpID = row.Cells["EmpID"].Value.ToString();
            }
        }
    }
}
