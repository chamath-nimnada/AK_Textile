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
    public partial class EmpManagerSalaryAdd : Form
    {
        private EmpManagerSalary empManagerSalary;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        public EmpManagerSalaryAdd(EmpManagerSalary empManagerSalary)
        {
            InitializeComponent();
            this.empManagerSalary = empManagerSalary;
        }

        private void EmpManagerSalaryAdd_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string selectedPosition = comboBox1.SelectedItem?.ToString();
            string basicSalary = textBox3.Text;

            if (string.IsNullOrEmpty(selectedPosition) || string.IsNullOrEmpty(basicSalary))
            {
                MessageBox.Show("Please fill all fields before adding!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Example: Add the data to a database or a list
            MessageBox.Show($"Position: {selectedPosition}\nBasic Salary: {basicSalary}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1; // Reset the dropdown
            textBox3.Clear();                 // Clear the salary text field
        }
    }
}
