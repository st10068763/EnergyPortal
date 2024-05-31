using System;
using System.Collections.Generic;
using System.Configuration;
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
        //-----------------------------___________BUTTONS____________-----------------------------//
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

            // this will hash the password before storing it in the database, it passes the password to the HashPassword method in the DataEncryptionClass
            string hashedPassword = DataEncryptionClass.HashPassword(password);
            // validates the contact number to ensure its 10 number
            if (contact.Length != 10)
            {
                GrantErrorMessage.Text = "Please enter 10 digits for contact number, exclude the country code";
                return;
            }
            // validates email
            if(email.Contains("@") == false)
            {
                GrantErrorMessage.Text = "Please enter a valid email address";
                return;
            }  
            // Calls the AddFarmer method to add the farmer to the database
            AddFarmer(name, contact, location, email, hashedPassword, role);
            // Cleans the form
            cleanForms();
        }
        /// <summary>
        /// Search button to search for products in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            // Bind the products to the repeater
            if (string.IsNullOrEmpty(txtSearchProduct.Text))
            {
                // if the search query is empty, a message will display
                GrantErrorMessage.Text = "Please enter a value in the search field";
                //BindProductRepeater();
            }
            else
            {
                BindProductRepeater(txtSearchProduct.Text);
            }
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
        /// <summary>
        /// Button to add a new grant to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        //-----------------------------___________METHODS____________-----------------------------//

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
                   GrantSuccessMessage.Text = "A new farmer has been added in the database";
                }
                // Will display a message if any error occurs
                catch (Exception ex)
                {
                    // Rollback the transaction if an error occurs
                    transaction.Rollback();
                    // Display the error message
                    GrantErrorMessage.Text = "Error: " + ex.Message;
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
        //----------------------------------BIND METHODS--------------------------------------//
        /// <summary>
        /// Method to bind the products to the repeater and search for products in the database
        /// </summary>
        /// <param name="searchQuery"></param>
        private void BindProductRepeater(string searchQuery = "")
        {
            try
            {
                // Get the search query and sort option
                string sortOption = ddlSortOptions.SelectedValue;
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                // Open the connection to the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductName, Quantity, Category, Product_Price, Product_Image, Description, ProductDate, FarmerName FROM Products";
                    // Add the search query to the query
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE ProductName LIKE @SearchQuery OR Category LIKE @SearchQuery";
                    }
                    // Add the sort option to the query
                    if (!string.IsNullOrEmpty(sortOption))
                    {
                        query += $" ORDER BY {sortOption}";
                    }
                    // Execute the query entered by the user
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the search query to the parameters
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                        }
                        // Execute the query
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {                            
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            RepeaterProducts.DataSource = dt;
                            RepeaterProducts.DataBind();
                        }
                    }
                }
            }
            catch (Exception)
            {
                GrantErrorMessage.Text = "Error: Failed to retrieve products";
            }           
        }       
        //------------------------------SEARCH METHODS-----------------------------------//      
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
        /// <summary>
        /// Method to insert a new grant in the database
        /// </summary>
        /// <param name="grantName"></param>
        /// <param name="grantDescription"></param>
        /// <param name="grantGroup"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Select event that sorts the products in the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSortOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProductRepeater(txtSearchProduct.Text);          
        }
    }
}