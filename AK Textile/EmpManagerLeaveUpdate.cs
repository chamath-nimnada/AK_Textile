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
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True");

        public EmpManagerLeaveUpdate()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string leaveName = textBox3.Text;
            string description = textBox4.Text;
            string leaveAmountText = textBox8.Text;

            if (string.IsNullOrWhiteSpace(leaveName) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(leaveAmountText))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(leaveAmountText, out decimal leaveAmount))
            {
                MessageBox.Show("Leave amount must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Simulate updating the leave details
            // Replace this with actual database update logic
            MessageBox.Show("Leave details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox8.Clear();
        }
    }
}
