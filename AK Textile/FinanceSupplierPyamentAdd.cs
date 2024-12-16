using CrystalDecisions.CrystalReports.ViewerObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class FinanceSupplierPyamentAdd : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1AMUUF3;Initial Catalog=""new database"";Integrated Security=True;"); // Update with your actual connection string

        public FinanceSupplierPyamentAdd(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            AutoIncrementSupPID();
        }

        private void AutoIncrementSupPID()
        {
            //To auto increment the Supplier ID
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAX(SupPID) FROM SupplierPayment", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                if (dr1[0] == DBNull.Value)
                {
                    this.supid.Text = "SUPP001"; // Changed 'sup' to 'supid'
                }
                else
                {
                    string maxID = dr1[0].ToString();
                    int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                    string newID = "SUPP" + (numericPart + 1).ToString("D3"); // Increment and format as "SUPXXX"
                    this.supid.Text = newID;
                }
                dr1.Close();
                con.Close();
            }
        }
        private void AllClear()
        {
            supid.Text = string.Empty;
            SupplierId.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            SupplierPMethod.Text = string.Empty;
            SupplierPAmount.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InsertSupplierPayment();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        private void InsertSupplierPayment()
        {
            string query = "INSERT INTO SupplierPayment (SupID, SupPDate, SupPMethod, SupPAmount) VALUES (@SupID, @SupPDate, @SupPMethod, @SupPAmount)";

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;Initial Catalog=AK-Textiles-PVT(LTD);"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupID", SupplierId.Text);
                    command.Parameters.AddWithValue("@SupPDate", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@SupPMethod", SupplierPMethod.Text);
                    command.Parameters.AddWithValue("@SupPAmount", SupplierPAmount.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SupplierPMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
