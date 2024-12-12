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
    public partial class CEOCustomer : Form
    {
        private MainForm mainForm;
        public CEOCustomer(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the TextBox
            textBox1.Text = string.Empty;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void CEOCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
