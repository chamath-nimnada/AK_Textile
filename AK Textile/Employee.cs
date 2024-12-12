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
    }
}
