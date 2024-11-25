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
    public partial class AdminEmployee : Form
    {
        private MainForm mainForm;

        public AdminEmployee(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminAdmin(mainForm));
        }
    }
}
