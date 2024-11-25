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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainPanal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadForm(new LoginForm(this));
        }

        //Method
        public void LoadForm(Form form)
        {
            MainPanal.Controls.Clear();
            form.TopLevel = false;
            MainPanal.Controls.Add(form);
            form.Show();
        }

    }
}
