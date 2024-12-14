
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
    public partial class LoginForm : Form
    {
        private MainForm mainForm;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-93ORV8S;Initial Catalog=AK-Textiles;Integrated Security=True;");
        
        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        public static class LoggedInUser
        {
            public static string UserId { get; set; }      // To store the User ID
            public static string Position { get; set; }    // To store the Position
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                txtPassword.PasswordChar = '\0'; // Show characters
            }
            else
            {
                txtPassword.PasswordChar = '*'; // Mask characters with '*'
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Open the connection
            con.Open();

            // Define the SQL query to check username and password, and retrieve the position
            SqlCommand com1 = new SqlCommand(
           @"SELECT 
                E.EmpID, 
                E.EmpUsername, 
                P.PName 
              FROM 
                Employee E
              INNER JOIN 
                Position P ON E.PositionID = P.PositionID
              WHERE 
                E.EmpUsername = @uname AND E.EmpPswrd = @pswrd",
           con
        );

            // Add parameters to prevent SQL injection
            com1.Parameters.AddWithValue("@uname", this.txtUsername.Text.Trim());
            com1.Parameters.AddWithValue("@pswrd", this.txtPassword.Text.Trim());

            // Execute the query and read the results
            SqlDataReader dr = com1.ExecuteReader();

            if (dr.Read()) // Check if any record matches the credentials
            {
                // Store the logged-in user's details in global variables
                LoggedInUser.UserId = dr["EmpID"].ToString();
                LoggedInUser.Position = dr["PName"].ToString();

                // Navigate to the appropriate dashboard based on the position
                switch (LoggedInUser.Position)
                {
                    case "employee manager":
                        //mainForm.LoadForm(new EmpManagerDashboard(mainForm));
                        break;
                    case "production manager":
                        mainForm.LoadForm(new ProductDashboard(mainForm));
                        break;
                    case "admin":
                        mainForm.LoadForm(new AdminDashboard(mainForm));
                        break;
                    case "finance manager":
                        //mainForm.LoadForm(new FinanceDashboard(mainForm));
                        break;
                    case "supplier manager":
                        mainForm.LoadForm(new SupplierDashboard(mainForm));
                        break;
                    case "inventory manager":
                        mainForm.LoadForm(new InventoryDashboard(mainForm));
                        break;
                    case "sales manager":
                        mainForm.LoadForm(new SalesDashboard(mainForm));
                        break;
                    case "employee":
                        //mainForm.LoadForm(new Employee(mainForm));
                        break;
                    default:
                        MessageBox.Show("Unknown position. Contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            else
            {
                // Invalid credentials
                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear the input fields
                txtUsername.Clear();
                txtPassword.Clear();
            }

            // Close the connection
            con.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
