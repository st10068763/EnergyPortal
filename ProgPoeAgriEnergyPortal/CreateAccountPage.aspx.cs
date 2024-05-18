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
            string password = txtPassword.Text;
            string role = ddlRole.SelectedValue;

            if (CreateUserAccount(name, email, phone, password, role))
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

        private bool CreateUserAccount(string name, string email, string phone, string password, string role)
        {
            string connectionString = " "; 
            bool isSuccess = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Name, Email, Phone, Password, Role) VALUES (@Name, @Email, @Phone, @Password, @Role)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
            }

            return isSuccess;
        }
    }
}