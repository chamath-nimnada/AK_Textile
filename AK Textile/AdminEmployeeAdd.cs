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
    public partial class AdminEmployeeAdd : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        
        public AdminEmployeeAdd()
        {
            InitializeComponent();
        }

        private void AdminEmployeeAdd_Load(object sender, EventArgs e)
        {
            //To auto Increment
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(EmpID) FROM Employee", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            dr1.Read();
            {
                if (dr1.GetValue(0).ToString() == "")
                {
                    this.empIDtxtbox.Text = "1";
                }
                else
                {
                    this.empIDtxtbox.Text = (Convert.ToInt32(dr1.GetValue(0).ToString()) + 1).ToString();
                }
                dr1.Close();
                con.Close();
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.fullnametxt.Clear();
            this.usernametxt.Clear();
            this.passwordtxt.Clear();
            this.positioncmb.Text = "";
            this.contactnotxt.Clear();
            this.address1txt.Clear();
            this.address2txt.Clear();
            this.address3txt.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            //to add data to the database
            con.Open();
            
        }
    }
}
