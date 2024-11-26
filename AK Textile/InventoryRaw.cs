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
    public partial class InventoryRaw : Form
    {
        private MainForm mainForm;
        public InventoryRaw(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void InventoryRaw_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MaterialId.Text=string.Empty;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InventoryRawAdd inventoryrawadd = new InventoryRawAdd();
            inventoryrawadd.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InventoryRawRemove inventoryrawremove = new InventoryRawRemove();
            inventoryrawremove.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InventoryRawUpdate inventoryrawupdate = new InventoryRawUpdate();
            inventoryrawupdate.ShowDialog();
        }
    }
}
