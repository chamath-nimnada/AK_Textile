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
    public partial class LoginForm : Form
    {
        private MainForm mainForm;
        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                txtPassword.PasswordChar = '\0'; // Show characters
            }
            else
            {
                txtPassword.PasswordChar = '*'; // Mask characters with '*'
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new AdminAdmin(mainForm));
        }
    }
}
