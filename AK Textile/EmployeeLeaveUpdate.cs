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
    public partial class EmployeeLeaveUpdate : Form
    {
        public EmployeeLeaveUpdate()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LeaveId.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmployeeLeaveRemove employeeleaveremove = new EmployeeLeaveRemove();
            employeeleaveremove.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
