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
    public partial class ProductProductionUpdate : Form
    {
        public ProductProductionUpdate()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty; // Clear the text in the textbox
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty; // Clear the text in the textbox
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
