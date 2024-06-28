using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    
    public partial class DashboardPage : System.Web.UI.Page
    {
        string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashoardData();
                BindProducts();
                BindGreenProducts();
            }
        }

        [WebMethod]
        public static object GetProductsBoughtData()
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            List<string> labels = new List<string>();
            List<int> productsBought = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductName, COUNT(*) AS ProductCount FROM Transactions GROUP BY ProductName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        labels.Add(reader["ProductName"].ToString());
                        productsBought.Add(Convert.ToInt32(reader["ProductCount"]));
                    }
                }
            }

            return new
            {
                labels = labels,
                productsBought = productsBought
            };
        }
    
        /// <summary>
        /// loads the dashboard data, this will display the total number of farmers, products and green products in the database
        /// </summary>
        private void LoadDashoardData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT (SELECT COUNT(*) FROM Farmer) AS TotalFarmers, (SELECT COUNT(*) FROM Products) AS TotalProducts, (SELECT COUNT(*) FROM GreenMarket) AS TotalGreenProducts", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTotalFarmers.Text = reader["TotalFarmers"].ToString();
                                txtTotalProducts.Text = reader["TotalProducts"].ToString();
                                txtTotalGreenProducts.Text = reader["TotalGreenProducts"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: "+ex);
            }
        }

        /// <summary>
        /// Binds the products to the repeater, this will display all the products in the database
        /// </summary>
        private void BindProducts()
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
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

        // Binds the green products to the repeater
        private void BindGreenProducts()
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
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

                            Response.Redirect($"TransactionsPage.aspx?productId={productId}&productName={productName}&category={category}&productionDate={productionDate}");
                        }
                    }
                }
            }
        }
        
        // Buy green product button click event
        protected void btnBuyGreen_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string greenMarketId = btn.CommandArgument;

            string query = $"SELECT GreenMarket_ID, Product_Name, Category, FarmerName FROM GreenMarket WHERE GreenMarket_ID = @GreenMarketId";
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GreenMarketId", greenMarketId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string productName = reader["Product_Name"].ToString();
                            string category = reader["Category"].ToString();
                            string farmerName = reader["FarmerName"].ToString();

                            Response.Redirect($"TransactionsPage.aspx?greenMarketId={greenMarketId}&productName={productName}&category={category}");
                        }
                    }
                }
            }
        }

    }
}
