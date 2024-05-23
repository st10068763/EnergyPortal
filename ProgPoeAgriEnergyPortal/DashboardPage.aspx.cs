﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    
    public partial class DashboardPage : System.Web.UI.Page
    {
        string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashoardData();
                BindProducts();
            }
        }

        private void LoadDashoardData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT (SELECT COUNT(*) FROM Farmer) AS TotalFarmers, (SELECT COUNT(*) FROM Products) AS TotalProducts", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTotalFarmers.Text = reader["TotalFarmers"].ToString();
                                txtTotalProducts.Text = reader["TotalProducts"].ToString();                                
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
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
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

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            string query = $"SELECT Product_ID, ProductName, Category, ProductDate, FarmerName FROM Products WHERE Product_ID = @ProductId";
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

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

    }
}
