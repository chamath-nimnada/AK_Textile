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
    public partial class ProductScheduleAdd : Form
    {
        private ProductSchedule productScheduleform;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        public ProductScheduleAdd(ProductSchedule productScheduleform)
        {
            InitializeComponent();
            autoincrement();
            this.productScheduleform = productScheduleform;
        }

        private void autoincrement()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(PScheduleID) FROM ProductionSchedule", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.schidtxt.Text = "PRS001";
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "PRS" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.schidtxt.Text = newID;
                }
                dr1.Close();
                con.Close();
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ProductScheduleAdd_Load(object sender, EventArgs e)
        {
            autoincrement();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cleartexts()
        {
            nametxt.Clear();
            comboBox1.SelectedIndex = -1;
            qtytxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            //to focus the cursor back to the main field
            nametxt.Focus();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            cleartexts();
        }

        private void addproductionSchedule()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO ProductionSchedule(PScheduleID, PScheduleName, ProdType, ProdQty, ProdStartDate, ProdEndDate)" +
                " VALUES (@pschid, @name, @type, @qty, @sdate, @edate)", con);
            cmd2.Parameters.AddWithValue("@pschid", schidtxt.Text);
            cmd2.Parameters.AddWithValue("@name", nametxt.Text);
            cmd2.Parameters.AddWithValue("@type", comboBox1.SelectedItem?.ToString() ?? "");
            cmd2.Parameters.AddWithValue("@qty", qtytxt.Text);
            cmd2.Parameters.AddWithValue("@sdate", dateTimePicker1.Value);
            cmd2.Parameters.AddWithValue("@edate", dateTimePicker2.Value);

            try
            {
                int r1 = cmd2.ExecuteNonQuery();
                if (r1 > 0)
                {
                    MessageBox.Show("Production Schedule Details added successfully !");
                }
                else
                {
                    MessageBox.Show("Error adding Production Schedule details !");
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
            cleartexts();
            autoincrement();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            addproductionSchedule();
        }
    }
}
