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
    public partial class ProductScheduleAdd : Form
    {
        public ProductScheduleAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty; // Clear the text in the textbox
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }
    }
}
