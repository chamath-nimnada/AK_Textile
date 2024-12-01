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
    public partial class EmpManagerLeaveAdd : Form
    {
        //database connection string
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public EmpManagerLeaveAdd()
        {
            InitializeComponent();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.lname.Clear();
            this.ldesc.Clear();
            this.lamount.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmpManagerLeaveAdd_Load(object sender, EventArgs e)
        {
            //to auto increment leave type ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(LeaveTypeID) FROM LeaveType", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.leaveid.Text = "LVT001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "LVT" + (numericPart + 1).ToString("D3"); // Increment and format as "DepXXX"
                    this.leaveid.Text = newID;
                }
            }
            dr1.Close();
            con.Close();

        }

        private void adddbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO LeaveType(LeavetypeID, LTName, LTDescription, LTAmount) VALUES(@ltid, ltname, ltdesc, ltamount)", con);
            cmd2.Parameters.AddWithValue("@ltid", leaveid.Text);
            cmd2.Parameters.AddWithValue("@ltname", lname.Text);
            cmd2.Parameters.AddWithValue("@ltdesc", ldesc.Text);
            cmd2.Parameters.AddWithValue("@ltamount", lamount.Text);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Leave type added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Leave type !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
