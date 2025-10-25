using System;
using System.Collections;
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
    public partial class FinanceOrder : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public FinanceOrder(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void LoadCustomerOrders()
        {
            //innerjoin query to load the customer name too
            string query = @"SELECT 
                                CO.COrderID, 
                                C.CusName, 
                                CO.CusID, 
                                CO.COrderDate, 
                                CO.CItemQty, 
                                CO.CItemPrice 
                             FROM CustomerOrder CO
                             INNER JOIN Customer C ON CO.CusID = C.CusID";

            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();

            try
            {
                // Adapter handles connection open/close
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSale(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSalary(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceReport(mainForm));
        }

        // "Clear" Search Button
        private void button6_Click(object sender, EventArgs e)
        {
            searchtxt.Text = string.Empty;
            LoadCustomerOrders();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceDashboard(mainForm));
        }

        // "Search" Button
        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = searchtxt.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadCustomerOrders();
                return;
            }
            string query = @"SELECT 
                                CO.COrderID, 
                                C.CusName, 
                                CO.CusID, 
                                CO.COrderDate, 
                                CO.CItemQty, 
                                CO.CItemPrice 
                             FROM CustomerOrder CO
                             INNER JOIN Customer C ON CO.CusID = C.CusID
                             WHERE CO.COrderID = @SearchValue 
                                OR CO.CusID = @SearchValue 
                                OR C.CusName LIKE @SearchPattern";

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                    cmd.Parameters.AddWithValue("@SearchPattern", "%" + searchValue + "%");

                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FinanceOrder_Load(object sender, EventArgs e)
        {
            LoadCustomerOrders();
        }
    }
}