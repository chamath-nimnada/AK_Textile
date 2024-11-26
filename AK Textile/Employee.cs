using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class Employee : Form
    {
        private MainForm mainForm;
        public Employee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmployeeLeaveUpdate employeeleavesupdate = new EmployeeLeaveUpdate();
            employeeleavesupdate.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EmployeeLeaveAdd employeeleaveadd = new EmployeeLeaveAdd();
            employeeleaveadd.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           EmployeeLeaveRemove employeeleaveremove = new EmployeeLeaveRemove();
            employeeleaveremove.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            salaryId.Text = string.Empty;
        }
    }
}
