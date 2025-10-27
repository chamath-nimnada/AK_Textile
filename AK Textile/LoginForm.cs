
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

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KLQEI3V0;Initial Catalog=AKTextilesDB;Integrated Security=True;Encrypt=True;");
        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        public static class LoggedInUser
        {
            public static string UserId { get; set; }      // To store the User ID
            public static string Position { get; set; } // To store the Position
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void OpenSubForm(Form subForm)
        {
            Form formBackground = new Form(); // Initialize background form

            try
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

                // Set the background form as the owner of the subform
                subForm.Owner = formBackground;

                // Show the subform as a dialog
                subForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Dispose both forms
                formBackground.Dispose();
                subForm.Dispose();
            }
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
                        mainForm.LoadForm(new EmpManagerDashboard(mainForm));
                        break;
                    case "production manager":
                        mainForm.LoadForm(new ProductDashboard(mainForm));
                        break;
                    case "admin":
                        mainForm.LoadForm(new AdminDashboard(mainForm));
                        break;
                    case "finance manager":
                        mainForm.LoadForm(new FinanceDashboard(mainForm));
                        break;
                    case "Supplier manager":
                        mainForm.LoadForm(new SupplierDashboard(mainForm));
                        break;
                    case "inventory manager":
                        mainForm.LoadForm(new InventoryDashboard(mainForm));
                        break;
                    case "sales manager":
                        mainForm.LoadForm(new SalesDashboard(mainForm));
                        break;
                    case "employee":
                        mainForm.LoadForm(new Employee(mainForm));
                        break;
                    case "production employee":
                        mainForm.LoadForm(new Employee(mainForm));
                        break;
                    case "finance employee":
                        mainForm.LoadForm(new Employee(mainForm));
                        break;
                    case "inventory employee":
                        mainForm.LoadForm(new Employee(mainForm));
                        break;
                    case "sales employee":
                        mainForm.LoadForm(new Employee(mainForm));
                        break;
                    case "CEO":
                        mainForm.LoadForm(new CEODashboard(mainForm));
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

        // Existing code...

        private void btnForgetPassword_Click(object sender, EventArgs e)
        {
            OpenSubForm(new ForgotPassword(this));
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
