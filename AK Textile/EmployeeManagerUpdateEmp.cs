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
    public partial class EmployeeManagerUpdateEmp : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True");

        private EmpManagerEmployee employeeManRemoveEmpForm;
        public EmployeeManagerUpdateEmp(EmpManagerEmployee employeeManRemoveEmpForm)
        {
            InitializeComponent();
            this.employeeManRemoveEmpForm = employeeManRemoveEmpForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string adminIDOrName = textBox1.Text;

            if (string.IsNullOrWhiteSpace(adminIDOrName))
            {
                MessageBox.Show("Please enter Admin Name or ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Simulating data retrieval for demonstration
            // Replace this with actual database lookup code
            if (adminIDOrName == "123" || adminIDOrName.ToLower() == "admin")
            {
                textBox3.Text = "Admin User";
                textBox4.Text = "admin123";
                textBox2.Text = "password";
                textBox8.Text = "1234567890";
                comboBox1.SelectedItem = "Manager";
                textBox5.Text = "101";
                textBox6.Text = "Main Street";
                textBox7.Text = "CityName";
            }
            else
            {
                MessageBox.Show("No admin found with the given ID or name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Clear();
            textBox8.Clear();
            comboBox1.SelectedIndex = -1;
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string fullName = textBox3.Text;
            string userName = textBox4.Text;
            string password = textBox2.Text;
            string contactNo = textBox8.Text;
            string position = comboBox1.SelectedItem?.ToString();
            string homeNo = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(contactNo) ||
                string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Simulate update operation
            // Replace with actual database update code
            MessageBox.Show("Admin details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
