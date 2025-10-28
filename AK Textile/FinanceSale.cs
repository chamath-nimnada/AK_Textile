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
    public partial class FinanceSale : Form
    {
        private MainForm mainForm;

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDatabase;Integrated Security=True");
        public FinanceSale(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        private void LoadFinalBill()
        {

            string query = @"SELECT * FROM FinalBill";

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

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceReport(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceOrder(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSupplierPayment(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSalary(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new LoginForm (mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceDashboard(mainForm));
        }

        //Search button
        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = EmployeeId.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadFinalBill();
                return;
            }
            string query = @"SELECT * FROM FinalBill WHERE FBID = @SearchValue";

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);

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

        //clear button
        private void button6_Click(object sender, EventArgs e)
        {
            EmployeeId.Text = string.Empty;
            LoadFinalBill();
        }
    }
}
