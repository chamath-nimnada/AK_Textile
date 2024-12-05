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
    public partial class InventoryInventory : Form
    {
        private MainForm mainForm;
        public InventoryInventory(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void InventoryInventory_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            InventoryInventoryAdd inventoryinventoryadd = new InventoryInventoryAdd();
            inventoryinventoryadd.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InventoryInventoryUpdate inventoryinventoryupdate = new InventoryInventoryUpdate();
            inventoryinventoryupdate.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InventoryInventoryRemove inventoryinventoryremove = new InventoryInventoryRemove();
            inventoryinventoryremove.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ItemId.Text=string.Empty;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryDashboard(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryProduct(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryRaw(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventorySupplier(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryGRN(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryReport(mainForm));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }
    }
}
