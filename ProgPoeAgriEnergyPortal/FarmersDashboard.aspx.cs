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

        }
        //---------------------------------------ADD PRODUCT------------------------------------------------//

        // method to add product to the database
        private bool AddProduct(string name, string description, string price, string quantity)
        {
            string connectionString = " "; // Replace with your actual connection string
            bool isSuccess = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Name, Description, Price, Quantity) VALUES (@Name, @Description, @Price, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
            }
            return isSuccess;
        }
    }
}