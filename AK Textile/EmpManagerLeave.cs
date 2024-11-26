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
    public partial class EmpManagerLeave : Form
    {
        private MainForm mainForm;
        public EmpManagerLeave(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void EmpManagerLeave_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDashboard(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerEmployee(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
         mainForm.LoadForm(new EmpManagerSalary(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDepartment(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerReport(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
           EmpManagerLeaveAdd empmanagerleaveadd = new EmpManagerLeaveAdd();
            empmanagerleaveadd.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmpManagerLeaveUpdate empmanagerleaveupdate = new EmpManagerLeaveUpdate();
            empmanagerleaveupdate.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmpManagerLeaveRemove empmanagerleaveremove = new EmpManagerLeaveRemove();
            empmanagerleaveremove.ShowDialog();
        }
    }
}
