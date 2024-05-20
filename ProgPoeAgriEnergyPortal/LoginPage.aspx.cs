using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Login button that will check the user's credentials and redirect them to the appropriate page,farmers page or employees page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Check if the user is a farmer or an employee based on the email entered by the user if the user id has the email and the role is farmer then redirect to the farmers page
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            // Calls the VerifyUser method to check if the user exists in the database
            string role = VerifyUser(email, password);
            // Redirects the user to the appropriate page based on the role
            if (!string.IsNullOrEmpty(role))
            {
                if (role == "Farmer")
                {
                    Response.Redirect("FarmerDashboard.aspx");
                }
                else if (role == "Employee")
                {
                    Response.Redirect("EmployeesDashboard.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid login attempt');</script>");
            }
        }
        //---------------------------------------VERIFY USER------------------------------------------------//
        /// <summary>
        /// Method to verify the user based on the email and password entered by the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string VerifyUser(string email, string password)
        {
            string role = null;
            // connection string to connect to the database
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Role FROM Users WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    role = reader["Role"].ToString();
                }
                reader.Close();
            }
            return role;
        }
       //---------------------------------------END VERIFY USER------------------------------------------------//
    }
}//-------------------------------------------***dingDONG END OF CODE***-----------------------------------------//
