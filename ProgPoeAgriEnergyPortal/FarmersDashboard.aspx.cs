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

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Get the product details from the form
            string name = txtProductName.Text;
            string category = CategoryDL.SelectedValue;
            string description = txtDescription.Text;
            string price = txtProductPrice.Text;
            string quantity = txtProductQuantity.Text;
            DateTime productDate = productionDate.Value;
            string product_image = txtProductImage.Text;
            int farmer_id = 1;

        }
        //-----------------------------------------ADD PRODUCT------------------------------------------------//
        // method to add product to the database
        private bool AddProduct(string name,string category, string description, string price, string quantity,DateTime productDate, string product_image, int farmer_id)
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
                    string query = "INSERT INTO Products (ProductName, Description, Price, Quantity, Category, ProductDate, Product_Image, Farmer_ID) VALUES (@Name, @Description, @Price, @Quantity, @ProductDate, @Product_Image, @Farmer_ID)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@ProductDate", productDate);
                        cmd.Parameters.AddWithValue("@Product_Image", product_image);
                        cmd.Parameters.AddWithValue("@Farmer_ID", farmer_id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                    }  
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
                catch (Exception ex)
                {
                    DisplayMessage("Error: " + ex);
                }
                
            }
            return false;
        } 

        //------------------------------------------------------------------------------------------//

        // Method to display message to the user
        private void DisplayMessage(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }
    }
}