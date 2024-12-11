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
    public partial class InventoryRaw : Form
    {
        private MainForm mainForm;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;Initial Catalog=AK-Textiles-PVT(LTD);Integrated Security=True;");
        
        public InventoryRaw(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadAllRaw();
        }

        private void LoadAllRaw() 
        {
            string query = "SELECT * FROM RawMaterials";

            try
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch 
            {
                MessageBox.Show("An error occurred while loading data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Raw ID or Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string query = "SELECT * FROM RawMaterials WHERE MatID = @searchValue OR MatName = @searchValue";

                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@searchValue", searchValue);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        else
                        {
                            MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = null;
                            con.Close();
                            LoadAllRaw();
                        }
                    }
                }

                catch (Exception x)
                {
                    MessageBox.Show("An error occurred while fetching data: " + x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InventoryRaw_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryDashboard(mainForm));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new LoginForm(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryProduct(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventorySupplier(mainForm));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryInventory(mainForm));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryGRN(mainForm));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryReport(mainForm));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the TextBox
            textBox1.Text = string.Empty;

            LoadAllRaw();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (InventoryRawRemove inventoryRawRemove = new InventoryRawRemove())
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

                    inventoryRawRemove.Owner = formBackground;
                    inventoryRawRemove.ShowDialog();

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

        private void button8_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (InventoryRawUpdate inventoryRawUpdate = new InventoryRawUpdate())
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

                    inventoryRawUpdate.Owner = formBackground;
                    inventoryRawUpdate.ShowDialog();

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

        private void button7_Click(object sender, EventArgs e)
        {
            //Dark the back main window and open sub window
            Form formBackground = new Form();
            try
            {
                using (InventoryRawAdd inventoryRawAdd = new InventoryRawAdd())
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

                    inventoryRawAdd.Owner = formBackground;
                    inventoryRawAdd.ShowDialog();

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
    }
}
