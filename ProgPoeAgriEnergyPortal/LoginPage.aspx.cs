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
                Response.Write("<script>alert('Invalid email or password');</script>");
            }
        }
        //---------------------------------------VERIFY USER------------------------------------------------//
        private (string UserID, string Role)? VerifyUser(string email, string password)
        {
            (string UserID, string Role)? userInfo = null;

            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // checks in the farmers table if the user exists
                    string farmerQuery = "SELECT Farmer_ID AS UserID, 'Farmer' AS Role FROM Farmer WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand farmerCmd = new SqlCommand(farmerQuery, conn))
                    {
                        farmerCmd.Parameters.AddWithValue("@Email", email);
                        farmerCmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader farmerReader = farmerCmd.ExecuteReader())
                        {
                            if (farmerReader.Read())
                            {
                                userInfo = (farmerReader["UserID"].ToString(), farmerReader["Role"].ToString());
                                return userInfo;
                            }
                        }
                    }

                    // checks in the employees table if the user exists
                    string employeeQuery = "SELECT Employee_ID AS UserID, 'Employee' AS Role FROM Employee WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand employeeCmd = new SqlCommand(employeeQuery, conn))
                    {
                        employeeCmd.Parameters.AddWithValue("@Email", email);
                        employeeCmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader employeeReader = employeeCmd.ExecuteReader())
                        {
                            if (employeeReader.Read())
                            {
                                userInfo = (employeeReader["UserID"].ToString(), employeeReader["Role"].ToString());

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
               // displays an error message if an error occurs
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");

            }            
            return userInfo;
            
        }        
       //---------------------------------------END VERIFY USER------------------------------------------------//
    }
}//-------------------------------------------***dingDONG END OF CODE***-----------------------------------------//
