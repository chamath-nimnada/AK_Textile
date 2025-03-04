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
    public partial class EmployeeManagerEmpAdd : Form
    {
        private EmpManagerEmployee empManagerEmployee;
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True");
        public EmployeeManagerEmpAdd(EmpManagerEmployee empManagerEmployee)
        {
            InitializeComponent();
            this.empManagerEmployee = empManagerEmployee;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            textBox8.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string employeeID = textBox2.Text;
            string fullName = textBox3.Text;
            string userName = textBox4.Text;
            string password = textBox1.Text;
            string position = comboBox1.SelectedItem?.ToString();
            string contactNo = textBox8.Text;
            string homeNo = textBox5.Text;
            string streetName = textBox6.Text;
            string city = textBox7.Text;

            if (string.IsNullOrWhiteSpace(employeeID) || string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(position) || string.IsNullOrWhiteSpace(contactNo))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Employee Added Successfully!\n\nEmployee ID: {employeeID}\nFull Name: {fullName}\nPosition: {position}\nContact No: {contactNo}\nAddress: {homeNo}, {streetName}, {city}",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
