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
    public partial class FinanceSupplierPayment : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public FinanceSupplierPayment(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
       
        
        private void LoadAllSupplier()
        {
            string query = "SELECT * FROM SupplierPayment";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();

            try
            {
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceOrder(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm (new FinanceSale(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceSalary(mainForm));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceReport(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new LoginForm(mainForm));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm .LoadForm(new FinanceDashboard(mainForm));
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SupplierId.Text = string.Empty;
            LoadAllSupplier();
        }

        //Search button
        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = SupplierId.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadAllSupplier();
                return;
            }

            string query = @"SELECT P.* FROM SupplierPayment P
                             LEFT JOIN Supplier S ON P.SupID = S.SupID
                             WHERE P.SupPID = @SearchValue 
                                OR P.SupID = @SearchValue 
                                OR S.SupName LIKE @SearchPattern";

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

        private void button5_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (FinanceSupplierPyamentAdd financeSupplierPyamentAdd = new FinanceSupplierPyamentAdd(mainForm))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    financeSupplierPyamentAdd.Owner = formBackground;
                    financeSupplierPyamentAdd.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void FinanceSupplierPayment_Load(object sender, EventArgs e)
        {
            LoadAllSupplier();
        }
    }
}
