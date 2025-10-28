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
    public partial class SalesDashboard : Form
    {
        private MainForm mainForm;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");

        public SalesDashboard(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesOrder(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesSalesReturn(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesPayment(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesInvoice(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesCustomer(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesSchedule(mainForm));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesBill(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new SalesReport(mainForm));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
