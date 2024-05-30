using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class EmployeesDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddFarmer_Click(object sender, EventArgs e)
        {
            // variables to store the input values for the farmer
            string name = txtFarmerName.Text;
            string contact = txtFarmerContact.Text;
            string location = txtFarmerLocation.Text;
            string email = txtFarmerEmail.Text;
            string password = txtFarmerPassword.Text;
            string role = "Farmer";

            // validates the contact number to ensure its 9 number
            if(contact.Length != 9)
            {
                ShowSuccess("Please enter 9 digits for contact number, exclude the country code");
                return;
            }
            // validates email
            if(email.Contains("@") == false)
            {
                ShowSuccess("Please enter a valid email address");
                return;
            }  
            // Calls the AddFarmer method to add the farmer to the database
            AddFarmer(name, contact, location, email, password, role);
            // Cleans the form
            cleanForms();
        }
        //----------------------------------ADD NEW FARMER--------------------------------------//
        // Method to add a new farmer to the database
        private bool AddFarmer(string name, string contact, string location, string email, string password, string role)
        {
           // connection string to connect to the database
           string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // using a transaction to ensure that the data is added to the database 
                SqlTransaction transaction = null;
                // using try catch block to catch any exceptions that may occur when adding a new farmer in the database
                try
                {
                    // open the connection to the database
                    conn.Open();
                    // Begin a transaction
                     transaction = conn.BeginTransaction();
                    // Sql query to insert the new farmer into the database
                    string query = "INSERT INTO Farmer (FarmerName, CellphoneNumber, Location, Email, Password, Role) VALUES (@Name, @CellphoneNumber, @Location, @Email, @Password,@Role)";
                    using (SqlCommand command = new SqlCommand(query, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@CellphoneNumber", contact);
                        command.Parameters.AddWithValue("@Location", location);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("Role", role);
                        // execute the query
                        int rowsAffected = command.ExecuteNonQuery();                       
                    }

                    // SQL query to insert the new user into the Users table
                    string userQuery = "INSERT INTO Users (UserName, Email, PhoneNumber, Password, Role, Location) VALUES (@UserName, @Email, @PhoneNumber, @Password, @Role, @Location)";
                    using (SqlCommand userCommand = new SqlCommand(userQuery, conn, transaction))
                    {
                        userCommand.Parameters.AddWithValue("@UserName", name);
                        userCommand.Parameters.AddWithValue("@Email", email);
                        userCommand.Parameters.AddWithValue("@PhoneNumber", contact);
                        userCommand.Parameters.AddWithValue("@Password", password);
                        userCommand.Parameters.AddWithValue("@Role", role);
                        userCommand.Parameters.AddWithValue("@Location", location);
                        // Execute the query
                        userCommand.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                    // Message to display that the farmer has been added successfully
                    ShowSuccess("A new farmer has been added in the database");
                    // cleans the forms
                                 
                }
                // Will display a message if any error occurs
                catch (Exception ex)
                {
                    // Rollback the transaction if an error occurs
                    transaction.Rollback();
                    // Display the error message
                  ShowSuccess("Error: " + ex.Message);
                }
                // closes the connection to the database
                finally
                {
                    conn.Close();
                }                
            }
            // returns true if the farmer has been added successfully
            return true;          
        }
        /// <summary>
        /// Search button to search for products in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            // Logic to search products
            string query = txtSearchProduct.Text;
            // Perform search and bind results to GridViewProducts
            DataTable products = SearchProducts(query);
            GridViewProducts.DataSource = products;
            GridViewProducts.DataBind();
        }
        /// <summary>
        /// search button to search for farmers in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchFarmer_Click(object sender, EventArgs e)
        {
            string query = txtSearchFarmer.Text;
            // Perform search and bind results to GridViewFarmers
            DataTable farmers = SearchFarmers(query);
            GridViewFarmers.DataSource = farmers;
            GridViewFarmers.DataBind();
        }
        //------------------------------SEARCH METHODS-----------------------------------//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable SearchProducts(string query)
        {
            var searchQuery = "%" + query + "%";
            // connection string to connect to the database
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // open the connection to the database
                conn.Open();
                // Sql query to search for products in the database
                // string sqlQuery = "SELECT * FROM Products WHERE ProductName LIKE @SearchQuery OR Description LIKE @SearchQuery";

                string sqlQuery = "SELECT ProductName, Quantity, Category, Product_Price, Product_Image, Description FROM Products " +
                                  "WHERE ProductName LIKE @SearchQuery OR Quantity LIKE @SearchQuery OR Category LIKE @SearchQuery " +
                                  "OR Description LIKE @SearchQuery";
                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    // execute the query
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable products = new DataTable();
                        adapter.Fill(products);
                        return products;
                    }
                }
            }           
        }
        /// <summary>
        /// Method to search for farmers in the database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable SearchFarmers(string query)
        {
            // the % are used to search for the query in the database
            var searchQuery = "%" + query + "%";
            // connection string to connect to the database
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // open the connection to the database
                conn.Open();
                // Sql query to search for products in the database
                string sqlQuery = "SELECT FarmerName, Email, CellphoneNumber, Location FROM Farmer " +
                    "WHERE FarmerName LIKE @SearchQuery OR Email LIKE @SearchQuery OR CellphoneNumber LIKE @SearchQuery" +
                    " OR Location LIKE @SearchQuery";

                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    // Add the search query to the parameters
                    command.Parameters.AddWithValue("@SearchQuery",searchQuery);
                    // execute the query
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // create a new data table to store the results of the search query
                        DataTable farmer = new DataTable();
                        adapter.Fill(farmer);
                        return farmer;
                    }
                }
            }
        }

        // Method to display the success message
        private void ShowSuccess(string message)
        {
            // Display the success message
            ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", "alert('" + message + "');", true);
        }

        protected void btnAddGrant_Click(object sender, EventArgs e)
        {
            string grantName = txtGrantName.Text;
            string grantDescription = txtGrantDescription.Text;
            string grantGroup = ddlGrantGroup.SelectedValue;

            // Adds a new grants
            AddGrant(grantName, grantDescription, grantGroup);
            // Cleans the forms
            cleanForms();
        }

        private bool AddGrant(string grantName, string grantDescription, string grantGroup)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = null;
                try
                {
                    transaction = conn.BeginTransaction();
                    string query = "INSERT INTO Grants (GrantName, GrantDescription, GrantGroup) VALUES (@GrantName, @GrantDescription, @GrantGroup)";
                    using (SqlCommand command = new SqlCommand(query, conn, transaction))
                    {
                        command.Parameters.AddWithValue("@GrantName", grantName);
                        command.Parameters.AddWithValue("@GrantDescription", grantDescription);
                        command.Parameters.AddWithValue("@GrantGroup", grantGroup);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    GrantSuccessMessage.Text= "A new grant has been added in the database";                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    GrantErrorMessage.Text= "Error: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }
        /// <summary>
        /// Cleans the forms
        /// </summary>
        private void cleanForms()
        {
            txtGrantName.Text = "";
            txtGrantDescription.Text = "";
            ddlGrantGroup.SelectedIndex = 0;
            txtFarmerContact.Text = "";
            txtFarmerEmail.Text = "";
            txtFarmerLocation.Text = "";
            txtFarmerName.Text = "";
            txtFarmerPassword.Text = "";
        }
    }
}