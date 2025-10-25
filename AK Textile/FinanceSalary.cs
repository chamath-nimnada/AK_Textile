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
    public partial class FinanceSalary : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public FinanceSalary(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FinanceSalary_Load(object sender, EventArgs e)
        {
            LoadAllSalary();
        }

        private void LoadAllSalary()
        {
            string query = @"SELECT 
                                S.SalaryID, 
                                E.EmpName, 
                                S.EmpID, 
                                S.SMonth, 
                                S.SAmount, 
                                S.SStatus 
                             FROM Salary S
                             INNER JOIN Employee E ON S.EmpID = E.EmpID";

            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();

            try
            {
                // Adapter handles open/close
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
            mainForm.LoadForm(new FinanceReport(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceDashboard(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceOrder(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSupplierPayment(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new FinanceSale(mainForm));
        }

        // "Clear" Search Button
        private void button6_Click(object sender, EventArgs e)
        {
            EmployeeId.Text = string.Empty;
            LoadAllSalary();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        // "Search" Button
        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = EmployeeId.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadAllSalary();
                return;
            }

            string query = @"SELECT 
                                S.SalaryID, 
                                E.EmpName, 
                                S.EmpID, 
                                S.SMonth, 
                                S.SAmount, 
                                S.SStatus 
                             FROM Salary S
                             INNER JOIN Employee E ON S.EmpID = E.EmpID
                             WHERE S.EmpID = @SearchValue OR E.EmpName LIKE @SearchPattern";

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
    }
}