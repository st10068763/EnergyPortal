using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class FarmersDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["FarmerID"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            // Get the farmer id from the session (Singleton object)
            int farmerId = Convert.ToInt32(Session["FarmerID"]);
            // Display the farmer's products
            DisplayProducts(farmerId);
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Get the product details from the form
            string name = txtProductName.Text;
            string category = CategoryDL.SelectedValue;
            string description = txtDescription.Text;
            decimal price;
            int quantity;
            DateTime productDate;
            string productImage = txtProductImage.Text;
            
            if (!decimal.TryParse(txtProductPrice.Text, out price))
            {
                DisplayMessage("Please enter a valid price");
                return;
            }
            if (!int.TryParse(txtProductQuantity.Text, out quantity))
            {
                DisplayMessage("Please enter a valid quantity");
                return;
            }
            if(!DateTime.TryParse(productionDate.Value, out productDate))
            {
                DisplayMessage("Please enter a valid date");
                return;
            }

            // Get the farmer id from the session (Singleton object)
            int farmerId = Convert.ToInt32(Session["FarmerID"]);

            // Call the AddProduct method to add the product to the database
            if (AddProduct(name, category, description, price, quantity, productDate, productImage, farmerId))
            {
                // Display message to the user
                DisplayMessage("A new product has been added successfully");
                //***************Method to display the product list after new product has been added********************
                // cleans all the form fields
                txtProductName.Text = "";
                txtDescription.Text = "";
                txtProductPrice.Text = "";
                txtProductQuantity.Text = "";
                productionDate.Value = "";
                txtProductImage.Text = "";
            }           
        }
        //-----------------------------------------ADD PRODUCT------------------------------------------------//
        // method to add product to the database
        private bool AddProduct(string name,string category, string description, decimal price, int quantity,DateTime productDate, string productImage, int farmerId)
        {
            // connection string to connect to the database
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007"; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // open the connection
                    conn.Open();
                    // query to insert the product into the database
                    string query = "INSERT INTO Products (ProductName, Description, Product_Price, Quantity, Category, ProductDate, Product_Image, Farmer_ID) VALUES (@Name, @Description, @Price, @Category, @Quantity, @ProductDate, @Product_Image, @Farmer_ID)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@ProductDate", productDate);
                        cmd.Parameters.AddWithValue("@Product_Image", productImage);
                        cmd.Parameters.AddWithValue("@Farmer_ID", farmerId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }  
                }
                catch (Exception ex)
                {
                    DisplayMessage("Error: " + ex.Message);
                    return false;
                }  
                finally
                {
                    conn.Close();
                }
            }            
        } 
        //------------------------------------------------------------------------------------------//

        //-----------------------------------------DISPLAY PRODUCTS------------------------------------------------//
        // Method to display the farmer's products
        private void DisplayProducts(int farmerId)
        {
            // connection string to connect to the database
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // query to get the farmer's products
                string query = "SELECT * FROM Products WHERE Farmer_ID = @Farmer_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Farmer_ID", farmerId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // loop through the products and display them
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();
                            row.Cells.Add(new TableCell { Text = reader["ProductName"].ToString() });
                            row.Cells.Add(new TableCell { Text = reader["Description"].ToString() });
                            row.Cells.Add(new TableCell { Text = reader["Product_Price"].ToString() });
                            row.Cells.Add(new TableCell { Text = reader["Quantity"].ToString() });
                            row.Cells.Add(new TableCell { Text = reader["Category"].ToString() });
                            row.Cells.Add(new TableCell { Text = reader["ProductDate"].ToString() });
                            row.Cells.Add(new TableCell { Text = reader["Product_Image"].ToString() });

                            ProductsTable.Rows.Add(row);
                        }
                    }
                }
            }
        }

        // Method to display message to the user
        private void DisplayMessage(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }
    }
}