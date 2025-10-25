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

        // Database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        public FinanceSupplierPyamentAdd(MainForm mainForm)
        {

        //Check this
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FinanceSupplierPaymentAdd_Load(object sender, EventArgs e)
        {
            AutoGenerateID();
            LoadSupplierNames();
        }

        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(SupPID) FROM SupplierPayment", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    if (dr1[0] == DBNull.Value)
                    {
                        payidtxt.Text = "SP001"; // 2-letter prefix
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        int numericPart = int.Parse(maxID.Substring(2));
                        string newID = "SP" + (numericPart + 1).ToString("D3"); 
                        payidtxt.Text = newID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Payment ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // This new function loads supplier names
        private void LoadSupplierNames()
        {
            try
            {
                con.Open();
                string query = "SELECT SupID, SupName FROM Supplier";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "SupName";
                comboBox1.ValueMember = "SupID";     
                comboBox1.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load supplier names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //Add button
        private void button8_Click(object sender, EventArgs e)
        {
            //validations
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(pamounttxt.Text, out decimal paymentAmount))
            {
                MessageBox.Show("Payment amount must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // get data
            string supPID = payidtxt.Text;
            string supID = comboBox1.SelectedValue.ToString();
            DateTime payDate = dateTimePicker1.Value;
            string payMethod = comboBox2.Text;

            //database code to add data to the database
            try
            {
                con.Open();
                string query = "INSERT INTO SupplierPayment (SupPID, SupID, SupPDate, SupPMethod, SupPAmount) " +
                               "VALUES (@SupPID, @SupID, @SupPDate, @SupPMethod, @SupPAmount)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@SupPID", supPID);
                cmd.Parameters.AddWithValue("@SupID", supID);
                cmd.Parameters.AddWithValue("@SupPDate", payDate);
                cmd.Parameters.AddWithValue("@SupPMethod", payMethod);
                cmd.Parameters.AddWithValue("@SupPAmount", paymentAmount);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Supplier payment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AllClear();
                AutoGenerateID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void AllClear()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            pamounttxt.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.Focus();
        }

        // "Cancel" button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // "Clear" button
        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void SupplierPMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}