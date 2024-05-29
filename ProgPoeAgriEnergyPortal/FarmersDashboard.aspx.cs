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
                    string query = "INSERT INTO Products (ProductName, Description, FarmerName, Product_Price, Quantity, Category, ProductDate, Product_Image, Farmer_ID) VALUES (@Name, @Description, @FarmerName, @Price, @Quantity, @Category, @ProductDate, @Product_Image, @Farmer_ID)";
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
        //-----------------------------------Add product in the green market place-----------------------------------//
        /// <summary>
        /// Method to add a new product to in the green market table
        /// </summary>
        /// <param name="GnproductName"></param>
        /// <param name="Gnquantity"></param>
        /// <param name="Gncategory"></param>
        /// <param name="GnPrice"></param>
        /// <param name="farmer_Id"></param>
        /// <param name="Gnproduct_Image"></param>
        /// <param name="Gndescription"></param>
        /// <param name="GnfarmerName"></param>
        /// <returns></returns>
        private bool AddGreenProduct(string GnproductName, int Gnquantity, string Gncategory, decimal GnPrice, int farmer_Id, string Gnproduct_Image, string Gndescription, string GnfarmerName)
        {
            try
            {
                // Add the product to the database
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                string query = "INSERT INTO GreenMarket (Product_Name, Quantity, Category, Product_Price, Farmer_ID, Product_Image, Description, FarmerName)" +
                    " VALUES (@Product_Name, @Quantity, @Category, @Product_Price, @Farmer_ID, @Product_Image, @Description, @FarmerName)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Product_Name", GnproductName);
                        cmd.Parameters.AddWithValue("@Quantity", Gnquantity);
                        cmd.Parameters.AddWithValue("@Category", Gncategory);
                        cmd.Parameters.AddWithValue("@Product_Price", GnPrice);
                        cmd.Parameters.AddWithValue("@Farmer_ID", farmer_Id);
                        cmd.Parameters.AddWithValue("@Product_Image", Gnproduct_Image);
                        cmd.Parameters.AddWithValue("@Description", Gndescription);
                        cmd.Parameters.AddWithValue("@FarmerName", GnfarmerName);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
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
            // cleans the forms for green products
            txtGreenProductName.Text = "";
            GreenproductImage.Text = "";
            GreenProductQuantity.Text = "";
            GreenProduct_Price.Text = "";
            GreenFarmerName.Text = "";
            GreenProductDescription.Text = "";
        }
        /// <summary>
        /// Button to add the green product to the green market place
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddGreenProduct_Click(object sender, EventArgs e)
        {
            // change all fild to green product
            string GnproductName = txtGreenProductName.Text;
            int Gnquantity;
            string Gncategory = CategoryDL.SelectedValue;
            decimal GnPrice;
            int farmer_Id = Convert.ToInt32(Session["Farmer_ID"]);
            string Gnproduct_Image = GreenproductImage.Text;
            string Gndescription = GreenProductDescription.Text;
            string GnfarmerName = GreenFarmerName.Text;

            if (!decimal.TryParse(GreenProduct_Price.Text, out GnPrice))
            {
                DisplayMessage("Please enter a valid price for the Green energy product");
                return;
            }
            if (!int.TryParse(GreenProductQuantity.Text, out Gnquantity))
            {
                DisplayMessage("Please enter a valid quantity for the Green energy product");
                return;
            }
            int farmerId = Convert.ToInt32(Session["Farmer_ID"]);

            // calls the method to add the green product to the green market place           
            if (AddGreenProduct(GnproductName, Gnquantity, Gncategory, GnPrice, farmer_Id, Gnproduct_Image, Gndescription, GnfarmerName))
            {
                DisplayMessage("A new green product has been added successfully");
                ClearFormFields();
               // DisplayProducts(farmerId);
            }
            else
            {
                DisplayMessage("Failed to add green product");
            }
            
        }
    }
}
