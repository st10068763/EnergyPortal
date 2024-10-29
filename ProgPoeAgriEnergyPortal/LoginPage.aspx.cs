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
        //Connection string 
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\AEPDatabase.mdf;Integrated Security=True";

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
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Ensure that both email and password are provided
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblErrorMessage.Text = "Please enter both email and password";
                lblErrorMessage.Visible = true;
                return;
            }

            // Calls the VerifyUser method to check if the user exists in the database
            var userInfo = VerifyUser(email, password);

            // Redirects the user to the appropriate page based on the role
           if (userInfo != null)
            {
                if (userInfo.Value.Role == "Farmer")
                {
                    Session["Farmer_ID"] = userInfo.Value.UserID;
                    Response.Redirect("FarmersDashboard.aspx");
                }
                else if (userInfo.Value.Role == "Employee")
                {
                    Session["Employee_ID"] = userInfo.Value.UserID;
                    Response.Redirect("EmployeesDashboard.aspx");
                }
            }
            else
            {
                lblErrorMessage.Text = "Invalid email or password";
                lblErrorMessage.Visible = true;
            }
        }
        //---------------------------------------VERIFY USER------------------------------------------------//
        private (string UserID, string Role)? VerifyUser(string email, string password)
        {
            (string UserID, string Role)? userInfo = null;
           
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT User_ID, Role, Password FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["Password"].ToString();
                                if (DataEncryptionClass.VerifyPassword(password, storedHash))
                                {
                                    userInfo = (reader["User_ID"].ToString(), reader["Role"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = $"Error: {ex.Message}<br/>{ex.StackTrace}";
                lblErrorMessage.Visible = true;
            }

            return userInfo;
        }

        //---------------------------------------END VERIFY USER------------------------------------------------//
    }
}//-------------------------------------------***dingDONG END OF CODE***-----------------------------------------//
