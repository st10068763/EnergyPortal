using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

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
            // this will hash the password before storing it in the database, it passes the password to the HashPassword method in the DataEncryptionClass
            string hashedPassword = DataEncryptionClass.HashPassword(password);
            // validate the user input no empty fields are allowed
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('All fields are required');</script>");
                return;
            }
            // check if the email is already in use
           if (IsEmailInUse(email))
            {
                Response.Write("<script>alert('Email already in use');</script>");
                return;
            }
            // ensures the user enters a valid email address
            if (!email.Contains("@") || !email.Contains("."))
            {
                Response.Write("<script>alert('Invalid email address');</script>");
                return;
            }
            // ensures the user enters a valid phone number with 10 digits
            if (phone.Length != 10)
            {
                Response.Write("<script>alert('Invalid phone number');</script>");
                return;
            }            
            // create the user account
            if (CreateUserAccount(name, email, phone, hashedPassword, role, location))
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

        private bool IsEmailInUse(string email)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007"; bool isEmailInUse = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Email FROM Users WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    isEmailInUse = reader.HasRows;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "' ;</script>");
            }
            return isEmailInUse;
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
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007"; bool isSuccess = false;

            try
            {              
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // using a transaction to ensure that the user details are inserted in both the users table and the Users or employees table
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = string.Empty;
                        SqlCommand cmd = new SqlCommand();

                        if (role == "Farmer")
                        {
                            // Only employees can create accounts for farmers
                            Response.Write("<script>alert(' Only employees can create accounts for farmers');</script>");
                        }
                        else if (role == "Employee")
                        {
                            query = "INSERT INTO Employee (EmployeeName, Email, PhoneNumber, Password, Location, Role) VALUES (@Name, @Email, @Phone, @Password, @Location, @Role)";
                            cmd = new SqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Password", password);
                            cmd.Parameters.AddWithValue("@Location", location);
                            cmd.Parameters.AddWithValue("@Role", role);
                            // execute the query to insert the employee details in the employees table
                            cmd.ExecuteNonQuery();

                            // query to insert the user details in the users table
                            string query2 = "INSERT INTO Users (UserName, Role, Email, Password, PhoneNumber, Location) VALUES (@UserName, @Role, @Email, @Password,@PhoneNumber, @Location)";
                            SqlCommand cmd2 = new SqlCommand(query2, conn, transaction);
                            cmd2.Parameters.AddWithValue("@UserName", name);
                            cmd2.Parameters.AddWithValue("@Role", role);
                            cmd2.Parameters.AddWithValue("@Email", email);
                            cmd2.Parameters.AddWithValue("@Password", password);
                            cmd2.Parameters.AddWithValue("@PhoneNumber", phone);
                            cmd2.Parameters.AddWithValue("@Location", location);
                            // execute the query to insert the user details in the users table
                            cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid role selected');</script>");
                            return false;
                        }

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.Write("<script>alert('Error: " + ex.Message + "' ;</script>");
                    }
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