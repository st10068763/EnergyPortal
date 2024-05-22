using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class CreateAccountPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string location = txtLocation.Text;
            string password = txtPassword.Text;
            string role = ddlRole.SelectedValue;

            if (CreateUserAccount(name, email, phone, password, role, location))
            {
                Response.Write("<script>alert('Account created successfully');</script>");
                // Redirect to the login page
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                Response.Write("<script>alert('Failed to create account');</script>");
            }
        }

        /// <summary>
        /// Creates a new user account in the database and inserts the user details in farmers table or employees table based on the role chosen
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool CreateUserAccount(string name, string email, string phone, string password, string role, string location)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007"; 
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    
                    string query = string.Empty;
                    SqlCommand cmd = new SqlCommand();

                    if (role == "Farmer")
                    {
                        query = "INSERT INTO Farmer (FarmerName, Email, CellphoneNumber, Password, Location, Role) VALUES (@Name, @Email, @Phone, @Password, @Location, @Role)";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Location", location);
                        cmd.Parameters.AddWithValue("@Role", role);

                    }
                    else if (role == "Employee")
                    {                        
                        query = "INSERT INTO Employee (EmployeeName, Email, PhoneNumber, Password, Location, Role) VALUES (@Name, @Email, @Phone, @Password, @Location, @Role)";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Location", location);
                        cmd.Parameters.AddWithValue("@Role", role);
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid role selected');</script>");
                    }                  

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isSuccess = rowsAffected > 0;
                }

                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "' ;</script>");                
            }
            return isSuccess;           
        }
    }
}