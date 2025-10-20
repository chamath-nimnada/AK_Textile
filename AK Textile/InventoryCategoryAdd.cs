using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AK_Textile
{
    public partial class InventoryCategoryAdd : Form
    {
        private InventoryCategory inventoryCategoryForm; // Reference to Inventory Category

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;Initial Catalog=Textlies");

        public InventoryCategoryAdd(InventoryCategory inventoryCategoryForm)
        {
            InitializeComponent();
            AutoGenerateID();
            this.inventoryCategoryForm = inventoryCategoryForm;
        }

        private void AutoGenerateID()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT MAX(InvCatID) FROM InventoryCategory", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    if (dr1[0] == DBNull.Value)
                    {
                        this.catID.Text = "INC001";
                    }
                    else
                    {
                        string maxID = dr1[0].ToString();
                        int numericPart = int.Parse(maxID.Substring(3)); // Extract "001" and convert to integer
                        string newID = "INC" + (numericPart + 1).ToString("D3");
                        this.catID.Text = newID;
                    }
                    dr1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void AddCategory()
        {
            string catID = this.catID.Text;
            string categoryName = textBox3.Text;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO InventoryCategory (InvCatID, InvCategory) VALUES (@catID, @categoryName)", con);
                cmd.Parameters.AddWithValue("@catID", catID);
                cmd.Parameters.AddWithValue("@categoryName", categoryName);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category added successfully!");
                textBox3.Text = string.Empty;
                con.Close();
                AutoGenerateID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddCategory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Call the public method from InventoryCategory
            inventoryCategoryForm.RefreshDataGrid();

            this.Close();
            //LoadAllCategory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
        }
    }
}
