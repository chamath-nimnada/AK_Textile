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
    public partial class SalesOrderAdd : Form
    {
        private SalesOrder salesorder; // Reference to salesOrder
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public SalesOrderAdd(SalesOrder salesorder)
        {
            InitializeComponent();
            this.salesorder = salesorder;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
