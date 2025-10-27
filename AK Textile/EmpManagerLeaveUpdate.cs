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
    public partial class EmpManagerLeaveUpdate : Form
    {

        private EmpManagerLeave leaveform;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private string currentLeaveTypeID = null;

        public EmpManagerLeaveUpdate(EmpManagerLeave leaveform)
        {
            InitializeComponent();
            this.leaveform = leaveform;
            // Wire up the event handler for selection change
            comboBoxSelectLeave.SelectedIndexChanged += ComboBoxSelectLeave_SelectedIndexChanged;
        }

        //update button
        private void button8_Click(object sender, EventArgs e)
        {
            // 1. Check if a leave type is loaded
            if (currentLeaveTypeID == null)
            {
                MessageBox.Show("Please select a leave type from the dropdown first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validation
            string ltName = textBox3.Text.Trim();
            string description = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(ltName))
            {
                MessageBox.Show("Please enter the Leave Type Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(textBox8.Text, out decimal leaveAmount) || leaveAmount < 0)
            {
                MessageBox.Show("Please enter a valid non-negative number for Leave Amount Annually.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Database Update
            try
            {
                con.Open();
                string query = @"UPDATE LeaveType
                                 SET LTName = @LTName,
                                     LTDescription = @LTDescription,
                                     LTAmount = @LTAmount
                                 WHERE LeaveTypeID = @CurrentLeaveTypeID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LTName", ltName);
                cmd.Parameters.AddWithValue("@LTDescription", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                cmd.Parameters.AddWithValue("@LTAmount", leaveAmount);
                cmd.Parameters.AddWithValue("@CurrentLeaveTypeID", currentLeaveTypeID); // The ID from selection

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Leave type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    leaveform.RefreshDataGrid(); 
                    this.Close(); // Close this form
                }
                else
                {
                    MessageBox.Show("Update failed. Leave type not found or no changes made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    MessageBox.Show("Error: A leave type with that name already exists.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error updating leave type: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating leave type: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearDetails()
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox8.Clear();
            currentLeaveTypeID = null;
             comboBoxSelectLeave.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (currentLeaveTypeID != null)
            {
                LoadLeaveTypeDetails(currentLeaveTypeID);
            }
            else
            {
                ClearDetails();
            }

        }

        //method to load leave type
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
                currentLeaveTypeID = comboBoxSelectLeave.SelectedValue.ToString();
                LoadLeaveTypeDetails(currentLeaveTypeID);
            }
            else
            {
                ClearDetails();
            }
        }

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
                    textBox3.Text = reader["LTName"].ToString();
                    textBox4.Text = reader["LTDescription"] != DBNull.Value ? reader["LTDescription"].ToString() : "";
                    textBox8.Text = reader["LTAmount"] != DBNull.Value ? reader["LTAmount"].ToString() : "";
                }
                else
                {
                    // Should not happen if ID came from ComboBox, but good practice
                    MessageBox.Show("Selected leave type details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void EmpManagerLeaveUpdate_Load(object sender, EventArgs e)
        {
            LoadLeaveTypesIntoComboBox();
            ClearDetails();  
        }
    }
}
