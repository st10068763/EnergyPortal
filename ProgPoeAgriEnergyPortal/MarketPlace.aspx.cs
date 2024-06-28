using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class MarketPlace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
                BindGreenProducts();
            }
        }
        //------------------------------_________________Bind Products_______________--------------------------------//
        // Binds the green products to the repeater
        private void BindGreenProducts()
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007"; using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT GreenMarket_ID, Product_Name, Quantity, Category, Product_Price, Product_Image, Description, FarmerName FROM GreenMarket";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GreenRepeater.DataSource = dt;
                    GreenRepeater.DataBind();
                }
            }
        }
        /// <summary>
        /// Binds the products to the repeater, this will display all the products in the database
        /// </summary>
        private void BindProducts()
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007"; using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Product_ID, ProductName, Quantity, Category, Product_Price, Product_Image, Description, FarmerName, ProductDate FROM Products";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ProductsRepeater.DataSource = dt;
                    ProductsRepeater.DataBind();
                }
            }
        }
        //------------------------------_________________Buy Buttons_______________--------------------------------//
        /// <summary>
        /// Buy button click event, this will redirect the user to the transaction page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuy_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            string query = $"SELECT Product_ID, ProductName, Category, ProductDate, FarmerName FROM Products WHERE Product_ID = @ProductId";
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string productName = reader["ProductName"].ToString();
                            string category = reader["Category"].ToString();
                            string productionDate = reader["ProductDate"].ToString();
                            string farmerName = reader["FarmerName"].ToString();

                            Response.Redirect($"TransactionsPage.aspx?productId={productId}&productName={productName}&category={category}&productionDate={productionDate}&farmerName={farmerName}");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Buy button click event for green products, this will redirect the user to the transaction page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuyGreen_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string greenmarketID = btn.CommandArgument;

            string query = $"SELECT GreenMarket_ID, Product_Name, Category, FarmerName FROM GreenMarket WHERE GreenMarket_ID = @greenmarketID";
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@greenmarketID", greenmarketID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string product_name = reader["product_name"].ToString();
                            string category = reader["category"].ToString();
                            string farmerName = reader["farmerName"].ToString();

                            Response.Redirect($"TransactionsPage.aspx?greenmarketID={greenmarketID}&product_name={product_name}&category={category}&farmerName={farmerName}");
                        }
                    }
                }
            }
        }
    }
}//--------------------------------dingDONG-END OF CODE-dingDONG--------------------------------//

