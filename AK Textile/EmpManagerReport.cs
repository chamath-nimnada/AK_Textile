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
    public partial class EmpManagerReport : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");
        private MainForm mainForm;
        public EmpManagerReport(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void EmpManagerReport_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerLeave(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerSalary(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerEmployee(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new EmpManagerDashboard(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
