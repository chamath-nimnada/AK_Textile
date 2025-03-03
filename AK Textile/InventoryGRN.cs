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
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;
                                        Initial Catalog=AKTextilesDB;
                                        Integrated Security=True;");

        private MainForm mainForm;
        public InventoryGRN(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadAllGRN();
            LoadSuppliers();
            AutoGenerateID();

            // Add event handler for supplier selection change
            comboBoxSupplier.SelectedIndexChanged += ComboBoxSupplier_SelectedIndexChanged;
        }

        // Load Suppliers into comboBoxSupplier
        private void LoadSuppliers()
        {
            try
            {
                {
                    con.Open();
                    string query = "SELECT SupID, SupName FROM Supplier";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable supplierTable = new DataTable();
                    adapter.Fill(supplierTable);

                    // Set up combobox to display supplier name but use supplier ID as value
                    comboBoxSupplier.DisplayMember = "SupName";
                    comboBoxSupplier.ValueMember = "SupID";
                    comboBoxSupplier.DataSource = supplierTable;

                    // Add empty selection as default
                    comboBoxSupplier.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error loading suppliers: " + ex.Message, "Database Error",
                    //MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
        }

        // Event handler for when supplier selection changes
        private void ComboBoxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        // Load invoices based on selected supplier
        private void LoadInvoices()
        {
            try
            {
                // Clear previous items
                comboBoxInvoice.DataSource = null;
                comboBoxInvoice.Items.Clear();

                // If no supplier selected, do nothing
                if (comboBoxSupplier.SelectedValue == null)
                    return;

                // Since SupID is varchar, we can use it directly as a string
                string supplierId = comboBoxSupplier.SelectedValue.ToString();

                {
                    con.Open();
                    string query = "SELECT SInvoiceID FROM SupplierInvoice WHERE SupID = @SupID";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@SupID", supplierId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable invoiceTable = new DataTable();
                    adapter.Fill(invoiceTable);

                    comboBoxInvoice.DisplayMember = "SInvoiceID";
                    comboBoxInvoice.ValueMember = "SInvoiceID";
                    comboBoxInvoice.DataSource = invoiceTable;

                    // Only set SelectedIndex if there are items
                    if (invoiceTable.Rows.Count > 0)
                        comboBoxInvoice.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error loading invoices: " + ex.Message, "Database Error",
                    //MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
        }

        private void LoadAllGRN()
        {
            // SQL query to fetch all data from the Product table
            string query = "SELECT * FROM GRN";
            {
                try
                {
                    // Open the connection
                    con.Open();
                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Execute the query and load the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                        // Adjust columns to fit the grid width
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        private bool IsPOrderIDValid(string pOrderID)
        {
            try
            {
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM PurchaseOrder WHERE POrderID = @POrderID";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@POrderID", pOrderID);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating Purchase Order ID: " + ex.Message, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { con.Close(); }
        }

        private void SaveGRN()
        {
            try
            {
                    con.Open();
                    string query = @"INSERT INTO GRN (SInvoiceID, SupplierID, POrderID, GRNDate, ItemQty) 
                            VALUES (@SInvoiceID, @SupplierID, @POrderID, @GRNDate, @ItemQty)";

                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@SInvoiceID", comboBoxInvoice.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@SupplierID", comboBoxSupplier.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@POrderID", textBoxOrder.Text.Trim());
                    command.Parameters.AddWithValue("@GRNDate", dateTimePicker.Value);
                    command.Parameters.AddWithValue("@ItemQty", Convert.ToInt32(textBoxQty.Text));

                    command.ExecuteNonQuery();

                    MessageBox.Show("GRN record added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear form fields for next entry
                    ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving GRN record: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
        }

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
                        this.textGRN.Text = "INGRN001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        if (maxID.StartsWith("INGRN") && int.TryParse(maxID.Substring(3), out int numericPart))
                        {
                            string newID = "INGRN" + (numericPart + 1).ToString("D3"); // Increment and format as "INVXXX"
                            this.textGRN.Text = newID;
                        }
                        else
                        {
                            MessageBox.Show("Invalid ID format in database.");
                        }
                    }
                }
                dr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating ID: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void ClearForm() 
        {
            comboBoxSupplier.SelectedIndex = -1;
            comboBoxInvoice.DataSource = null;
            textBoxOrder.Clear();
            textBoxQty.Clear();
            dateTimePicker.Value = DateTime.Now;
        }

        private void InventoryGRN_Load(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            // Clear the text in textBox1
            textBox1.Clear();

            // Clear the data in dataGridView1
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            LoadAllGRN();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearForm();    
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (comboBoxSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxInvoice.SelectedValue == null)
            {
                MessageBox.Show("Please select an invoice.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxOrder.Text))
            {
                MessageBox.Show("Please enter a Purchase Order ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxQty.Text) || !int.TryParse(textBoxQty.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate POrderID
            string pOrderID = textBoxOrder.Text.Trim();
            if (!IsPOrderIDValid(pOrderID))
            {
                MessageBox.Show("Invalid Purchase Order ID. Please enter a valid ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save to GRN table
            SaveGRN();
        }
    }
}
