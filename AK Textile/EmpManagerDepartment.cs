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
    public partial class EmpManagerDepartment : Form
    {
        private MainForm mainForm;
        public EmpManagerDepartment(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void EmpManagerDepartment_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerLeave(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerReport(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DepId.Text = string.Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentAdd empmanagerdepartmentadd = new EmpManagerDepartmentAdd();
            empmanagerdepartmentadd.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentUpdate empmanagerdepartmentupdate = new EmpManagerDepartmentUpdate();
            empmanagerdepartmentupdate.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmpManagerDepartmentRemove empmanagerdepartmentremove = new EmpManagerDepartmentRemove();
            empmanagerdepartmentremove.ShowDialog();
        }
    }
}
