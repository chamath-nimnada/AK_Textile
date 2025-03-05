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
    public partial class SupplierSupplierUpdate : Form
    {
        private SupplierSupplier supplierform;
        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                 Initial Catalog=Textlies;
                                                 Integrated Security=True");

        private string selectedSupplierID; // Store Supplier ID for updates
        public SupplierSupplierUpdate(SupplierSupplier supplierform)
        {
            InitializeComponent();
            this.supplierform = supplierform;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            this.nametxt.Clear();
            this.emailtxt.Clear();
            this.contaccttxt.Clear();
            this.addresstxt.Clear();
        }

 

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Supplier ID or Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SupID, SupName, SupAddress, SupEmail, SupContact FROM Supplier WHERE SupID = @Search OR SupName LIKE @SearchName", con);
                cmd.Parameters.AddWithValue("@Search", searchValue);
                cmd.Parameters.AddWithValue("@SearchName", "%" + searchValue + "%"); // Partial match for names

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    selectedSupplierID = reader["SupID"].ToString(); // Store full SupplierID
                    nametxt.Text = reader["SupName"].ToString();
                    addresstxt.Text = reader["SupAddress"].ToString();
                    emailtxt.Text = reader["SupEmail"].ToString();
                    contaccttxt.Text = reader["SupContact"].ToString();

                }
                else
                {
                    MessageBox.Show("Supplier not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void ClearFields()
        {
            nametxt.Clear();
            emailtxt.Clear();
            contaccttxt.Clear();
            addresstxt.Clear();
        }

 

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedSupplierID))
            {
                MessageBox.Show("Please search for a supplier before updating.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE Supplier SET SupName = @name, SupAddress = @address, SupEmail = @Email, SupContact = @ContactNo WHERE SupID = @SupplierID", con);
                {
                    cmd2.Parameters.AddWithValue("@SupplierID", selectedSupplierID);
                    cmd2.Parameters.AddWithValue("@name", nametxt.Text);
                    cmd2.Parameters.AddWithValue("@address", addresstxt.Text);
                    cmd2.Parameters.AddWithValue("@Email", emailtxt.Text);
                    cmd2.Parameters.AddWithValue("@ContactNo", contaccttxt.Text);


                    int rowsAffected = cmd2.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update supplier details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
/*string search = textBox1.Text.Trim();
if (!string.IsNullOrEmpty(search))
{
    //calling the methods
    LoadSupplierData(search);

}
else
{
    MessageBox.Show("Please enter a Supplier ID or Name to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}

  string supplierId = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(supplierId))
            {
                // calling the method to update
                UpdateSupplier(supplierId);
            }
            else
            {
                MessageBox.Show("Please enter a supplier ID to update !", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/

//method to load data to the textboxes
/* private void LoadSupplierData(string supdata)
 {
     con.Open();
     SqlCommand cmd3 = new SqlCommand("SELECT SupID, SupName, SupEmail, SupContact, SupAddress FROM Supplier WHERE SupID = @searchval OR SupName LIKE @searchval", con);
     cmd3.Parameters.AddWithValue("@searchval", "%" + supdata + "%");

     try
     {
         SqlDataReader dr3 = cmd3.ExecuteReader();

         if (dr3.Read())
         {
             // Populate the text boxes with data
             nametxt.Text = dr3["SupName"].ToString();
             emailtxt.Text = dr3["SupEmail"].ToString();
             contaccttxt.Text = dr3["SupContact"].ToString();
             addresstxt.Text = dr3["SupAddress"].ToString();
         }
         else
         {
             MessageBox.Show("No supplier found with the entered ID or Name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);

         }
     }
     catch (Exception ex)
     {
         MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
     }
     finally
     {
         con.Close();
     }
 }*/
//method to update data in the textboxes
/*private void UpdateSupplier(string supid)
{
    con.Open();
    SqlCommand cmd = new SqlCommand("UPDATE Supplier SET SupName = @name, SupEmail = @email, SupContact = @contact, SupAddress = @address WHERE SupID = @supid", con);

    cmd.Parameters.AddWithValue("@supid", supid);
    cmd.Parameters.AddWithValue("@name", nametxt.Text.Trim());
    cmd.Parameters.AddWithValue("@email", emailtxt.Text.Trim());
    cmd.Parameters.AddWithValue("@contact", contaccttxt.Text.Trim());
    cmd.Parameters.AddWithValue("@address", addresstxt.Text.Trim());

    try
    {
        int r = cmd.ExecuteNonQuery();

        if (r > 0)
        {
            MessageBox.Show("Supplier details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Failed to update supplier details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        con.Close();
    }
}*/
