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

        private string selectedLeaveTypeID = null;

        public EmpManagerLeaveRemove(EmpManagerLeave leaveform)
        {
            InitializeComponent();
            this.leaveform = leaveform;
            comboBoxSelectLeave.SelectedIndexChanged += ComboBoxSelectLeave_SelectedIndexChanged;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (selectedLeaveTypeID == null)
            {
                MessageBox.Show("Please select a leave type from the dropdown to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedName = comboBoxSelectLeave.Text; 
            DialogResult confirm = MessageBox.Show($"Are you sure you want to permanently delete the leave type '{selectedName}'?",
                                                   "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
            {
                return; // User cancelled
            }

            // 3. Execute Delete Query
            try
            {
                con.Open();
                string query = "DELETE FROM LeaveType WHERE LeaveTypeID = @LeaveTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LeaveTypeID", selectedLeaveTypeID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Leave type removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    leaveform.RefreshDataGrid(); 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Deletion failed. Leave type might have been removed already.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle potential FK constraint errors if this LeaveType is used in the Leave table
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("Cannot delete this leave type because it is currently assigned to one or more leave requests. Please update or remove those requests first.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error removing leave type: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing leave type: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void ClearDetails()
        {
            selectedLeaveTypeID = null; 
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        //load leave types
        private void LoadLeaveTypesIntoComboBox()
        {
            try
            {
                string query = "SELECT LeaveTypeID, LTName FROM LeaveType ORDER BY LTName";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable leaveTypeTable = new DataTable();
                adapter.Fill(leaveTypeTable);


                comboBoxSelectLeave.DataSource = leaveTypeTable;
                comboBoxSelectLeave.DisplayMember = "LTName";
                comboBoxSelectLeave.ValueMember = "LeaveTypeID";
                comboBoxSelectLeave.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load leave types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBoxSelectLeave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectLeave.SelectedValue != null && comboBoxSelectLeave.SelectedValue is string)
            {
                selectedLeaveTypeID = comboBoxSelectLeave.SelectedValue.ToString();
                LoadLeaveTypeDetails(selectedLeaveTypeID);
            }
            else
            {
                ClearDetails();
            }
        }

        //to Load data
        private void LoadLeaveTypeDetails(string leaveTypeID)
        {
            try
            {
                con.Open();
                string query = "SELECT LTName, LTDescription, LTAmount FROM LeaveType WHERE LeaveTypeID = @LeaveTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LeaveTypeID", leaveTypeID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Format the details string
                    string name = reader["LTName"].ToString();
                    string desc = reader["LTDescription"] != DBNull.Value ? reader["LTDescription"].ToString() : "N/A";
                    string amount = reader["LTAmount"] != DBNull.Value ? reader["LTAmount"].ToString() : "N/A";

                    //textBoxDetails.Text = $"Name: {name}\r\nDescription: {desc}\r\nAmount Annually: {amount}";
                }
                else
                {
                    ClearDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading leave type details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearDetails();
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void EmpManagerLeaveRemove_Load(object sender, EventArgs e)
        {
            LoadLeaveTypesIntoComboBox();
            ClearDetails();
        }
    }
}
