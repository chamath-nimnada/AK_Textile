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
    public partial class EmpManagerSalaryUpdate : Form
    {
        private EmpManagerSalary empManagerSalary;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");
        public EmpManagerSalaryUpdate(EmpManagerSalary empManagerSalary)
        {
            InitializeComponent();
            this.empManagerSalary = empManagerSalary;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string selectedPosition = comboBox1.SelectedItem?.ToString();
            string previousSalary = textBox3.Text;
            string newSalary = textBox1.Text;

            if (string.IsNullOrEmpty(selectedPosition) || string.IsNullOrEmpty(previousSalary) || string.IsNullOrEmpty(newSalary))
            {
                MessageBox.Show("Please fill all fields before updating!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Example: Update salary details in a database or a list
            MessageBox.Show($"Position: {selectedPosition}\nPrevious Salary: {previousSalary}\nNew Salary: {newSalary}", "Updated Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1; // Reset the dropdown
            textBox3.Clear();   // Clear the previous salary field
            textBox1.Clear();  // Clear the new salary field
        }

        private void EmpManagerSalaryUpdate_Load(object sender, EventArgs e)
        {

        }
    }
}
