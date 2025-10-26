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
    public partial class InventoryGRN : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");

        private MainForm mainForm;
        public InventoryGRN(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            // Add event handler for supplier selection change
            comboBoxSupplier.SelectedIndexChanged += ComboBoxSupplier_SelectedIndexChanged;
        }

        // --- Load Suppliers into comboBoxSupplier ---
        private void LoadSuppliers()
        {
            try
            {
                string query = "SELECT SupID, SupName FROM Supplier";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con); // Adapter handles connection
                DataTable supplierTable = new DataTable();
                adapter.Fill(supplierTable);

                comboBoxSupplier.DisplayMember = "SupName";
                comboBoxSupplier.ValueMember = "SupID";
                comboBoxSupplier.DataSource = supplierTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for when supplier selection changes
        private void ComboBoxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only load if a valid supplier is selected
            if (comboBoxSupplier.SelectedValue != null)
            {
                LoadInvoices();
                LoadOrder();
            }
        }

        // --- Load Purchase Orders ---
        private void LoadOrder()
        {
            // Clear previous items first
            comboBoxOrder.DataSource = null;

            try
            {
                string query = "SELECT POrderID, PItemName FROM PurchaseOrder";
                SqlCommand command = new SqlCommand(query, con);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable orderTable = new DataTable();
                adapter.Fill(orderTable);

                comboBoxOrder.DisplayMember = "PItemName";
                comboBoxOrder.ValueMember = "POrderID";
                comboBoxOrder.DataSource = orderTable;
                comboBoxOrder.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message, "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Load Invoices based on selected supplier ---
        private void LoadInvoices()
        {
            comboBoxInvoice.DataSource = null;

            if (comboBoxSupplier.SelectedValue == null) return;

            string supplierId = comboBoxSupplier.SelectedValue.ToString();

            try
            {
                string query = "SELECT SInvoiceID FROM SupplierInvoice WHERE SupID = @SupID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@SupID", supplierId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable invoiceTable = new DataTable();
                adapter.Fill(invoiceTable);

                comboBoxInvoice.DisplayMember = "SInvoiceID";
                comboBoxInvoice.ValueMember = "SInvoiceID";
                comboBoxInvoice.DataSource = invoiceTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoices: " + ex.Message, "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Load ALL GRNs into the top DataGridView ---
        private void LoadAllGRN()
        {
            string query = "SELECT * FROM GRN"; // Simple query, maybe join later if needed
            SqlDataAdapter adapter = new SqlDataAdapter(query, con); // Adapter handles connection
            DataTable dataTable = new DataTable();
            try
            {
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Assumes top grid is dataGridView1
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading GRN data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- AutoGenerate GRN ID ---
        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(GRNID) FROM GRN", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    if (dr1[0] == DBNull.Value)
                    {
                        this.textGRN.Text = "GRN001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString(); 
                        if (maxID.StartsWith("GRN") && int.TryParse(maxID.Substring(3), out int numericPart))
                        {
                            string newID = "GRN" + (numericPart + 1).ToString("D3");
                            this.textGRN.Text = newID;
                        }
                        else
                        {
                            this.textGRN.Text = "GRN001"; // Fallback
                            // Optionally show error: MessageBox.Show("Invalid ID format in database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating ID: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // Clear Method
        private void ClearForm()
        {
            comboBoxSupplier.SelectedIndex = -1;
            comboBoxInvoice.DataSource = null;
            comboBoxOrder.DataSource = null;
            textBoxQty.Clear();
            dateTimePicker.Value = DateTime.Now;
        }




        private bool IsPOrderIDValid(string pOrderID)
        {
            try
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM PurchaseOrder WHERE POrderID = @POrderID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@POrderID", pOrderID);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating Purchase Order ID: " + ex.Message, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        // --- Save the new GRN record ---
        private void SaveGRN()
        {
            // Validation
            if (comboBoxSupplier.SelectedValue == null) { MessageBox.Show("Please select a supplier."); return; }
            if (comboBoxInvoice.SelectedValue == null) { MessageBox.Show("Please select an invoice."); return; }
            if (comboBoxOrder.SelectedValue == null) { MessageBox.Show("Please select a purchase order."); return; }
            if (!int.TryParse(textBoxQty.Text, out int qty) || qty <= 0) { MessageBox.Show("Please enter a valid positive quantity."); return; }

            try
            {
                con.Open();
                // --- FIX: Omitting GRNItemDetails as UI doesn't have it ---
                string query = @"INSERT INTO GRN (GRNID, SInvoiceID, SupplierID, POrderID, GRNDate, ItemQty, GRNItemDetails)
                                 VALUES (@GRNID, @SInvoiceID, @SupplierID, @POrderID, @GRNDate, @ItemQty, @idetails)";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@GRNID", textGRN.Text);
                command.Parameters.AddWithValue("@SInvoiceID", comboBoxInvoice.SelectedValue);
                command.Parameters.AddWithValue("@SupplierID", comboBoxSupplier.SelectedValue); 
                command.Parameters.AddWithValue("@POrderID", comboBoxOrder.SelectedValue);
                command.Parameters.AddWithValue("@GRNDate", dateTimePicker.Value);
                command.Parameters.AddWithValue("@idetails", textBox2.Text);
                command.Parameters.AddWithValue("@ItemQty", qty);

                command.ExecuteNonQuery();

                MessageBox.Show("GRN record added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //refreshes the datagrid.
                LoadAllGRN();
                ClearForm();
                AutoGenerateID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving GRN record: " + ex.Message, "Database Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) 
                    con.Close();
            }
        }

        // Search the GRN Grid
        private void SearchGRN(string searchValue)
        {

            string query = @"SELECT * FROM GRN
                             WHERE GRNID = @SearchValue
                                OR SInvoiceID = @SearchValue
                                OR SupplierID = @SearchValue
                                OR POrderID = @SearchValue";

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
                    MessageBox.Show("No matching GRN records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching GRN data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InventoryGRN_Load(object sender, EventArgs e)
        {
            LoadAllGRN();
            LoadSuppliers();
            AutoGenerateID();
            ClearForm(); 
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
            mainForm.LoadForm(new InventoryCategory(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventorySupplier(mainForm));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryInventory(mainForm));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mainForm.LoadForm(new InventoryReport(mainForm));
        }

        // Clears search button
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            LoadAllGRN();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearForm();
            AutoGenerateID();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SaveGRN();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchValue))
            {
                LoadAllGRN();
            }
            else
            {
                SearchGRN(searchValue);
            }
        }
    }
}
