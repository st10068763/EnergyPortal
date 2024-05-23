using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class FarmersDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Farmer_ID"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
                else
                {
                    int farmerId = Convert.ToInt32(Session["Farmer_ID"]);
                    DisplayProducts(farmerId);
                }
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;
            string category = CategoryDL.SelectedValue;
            string description = txtDescription.Text;
            string farmerName = txtFarmerName.Text;
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
            if (!DateTime.TryParse(productionDate.Value, out productDate))
            {
                DisplayMessage("Please enter a valid date");
                return;
            }

            int farmerId = Convert.ToInt32(Session["Farmer_ID"]);

            if (AddProduct(name, category, description, farmerName, price, quantity, productDate, productImage, farmerId))
            {
                DisplayMessage("A new product has been added successfully");
                ClearFormFields();
                DisplayProducts(farmerId);
            }
            else
            {
                DisplayMessage("Failed to add product");
            }
        }

        private bool AddProduct(string name, string category, string description, string farmerName, decimal price, int quantity, DateTime productDate, string productImage, int farmerId)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Products (ProductName, Description, FarmerName, Product_Price, Quantity, Category, ProductDate, Product_Image, Farmer_ID) " +
                        "VALUES (@Name, @Description, @FarmerName, @Price, @Quantity, @Category, @ProductDate, @Product_Image, @Farmer_ID)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@FarmerName", farmerName);
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
                    DisplayMessage($"Error adding product: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        DisplayMessage($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return false;
                }
            }
        }

        private void DisplayProducts(int farmerId)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products WHERE Farmer_ID = @Farmer_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Farmer_ID", farmerId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ProductsTable.Rows.Clear();
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
                            row.Cells.Add(new TableCell { Text = reader["FarmerName"].ToString() });
                            ProductsTable.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void DisplayMessage(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }

        private void ClearFormFields()
        {
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtProductPrice.Text = "";
            txtProductQuantity.Text = "";
            productionDate.Value = "";
            txtProductImage.Text = "";
            txtFarmerName.Text = "";
        }
    }
}
