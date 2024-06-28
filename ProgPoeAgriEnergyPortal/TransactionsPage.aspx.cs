using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data;

namespace ProgPoeAgriEnergyPortal
{
    public partial class TransactionsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the logged-in user's ID
                int userId = Convert.ToInt32(Session["userId"]);
                BindProductDetails();
                BindGreenProductDetails();
                // Display the transaction history
                DisplayTransactions(userId);
            }

        }
        /// <summary>
        /// Method to bind the product details to the textboxes
        /// </summary>
        private void BindProductDetails()
        {
            string productId = Request.QueryString["productId"];
            string productName = Request.QueryString["productName"];
            string category = Request.QueryString["category"];
            string productionDate = Request.QueryString["productionDate"];
            string FarmerName = Request.QueryString["FarmerName"];
            // Display the product details
            if (!string.IsNullOrEmpty(productId))
            {
                txtProductName.Text = productName;
                txtCategory.Text = category;
                txtProductionDate.Text = productionDate;
                txtFarmerName.Text = FarmerName;
            }
        }

        // Binds the green products details to the textboxes
        private void BindGreenProductDetails()
        {
            string productId = Request.QueryString["greenmarketID"];
            string product_Name = Request.QueryString["product_name"];
            string category = Request.QueryString["category"];
            string FarmerName = Request.QueryString["FarmerName"];
            // Display the product details
            if (!string.IsNullOrEmpty(productId))
            {
                txtProductName.Text = product_Name;
                txtCategory.Text = category;
                txtFarmerName.Text = FarmerName;
            }
        }

        /// <summary>
        /// confirm the purchase and adds the transaction to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirmPurchase_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["productId"];
            string greenMarketId = Request.QueryString["greenMarketID"];
            string productName = txtProductName.Text;
            string category = txtCategory.Text;
            string farmerName = txtFarmerName.Text;
            string productionDate = txtProductionDate.Text;
            string buyerName = txtBuyerName.Text;
            string buyerEmail = txtBuyerEmail.Text;
            string buyerAddress = txtBuyerAddress.Text;
            string cardNumber = txtcardNumber.Text;
            string cvv = txtCVV.Text;
            // Encrypt the card number before storing it in the database
            string encryptedCardNumber = DataEncryptionClass.EncryptCardNumber(cardNumber);
            // Encrypt the CVV number before storing it in the database
            string encryptedCVV = DataEncryptionClass.EncryptVCCNumber(cvv);

            // validates the card number and cvv
            if (cardNumber.Length != 16)
            {
                Response.Write("<script>alert('Invalid card number, please insert 16 numerical digits');</script>");
                return;
            }
            if (cvv.Length != 3)
            {
                Response.Write("<script>alert('Invalid CVV');</script>");
                return;
            }

            // Add the transaction to the database with the encrypted card number
            if (AddTransaction(productId, greenMarketId, productName, category,farmerName, productionDate, buyerName, buyerEmail, buyerAddress, encryptedCardNumber, encryptedCVV))
            {
                Response.Write("<script>alert('Thanks for shopping with us :)');</script>");
                //Clear the form fields
                ClearFormFields();
            }
            else
            {
                Response.Write("<script>alert('Failed to complete transaction');</script>");
            }
        }
        /// <summary>
        /// Add the transaction to the database
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="category"></param>
        /// <param name="productionDate"></param>
        /// <param name="buyerName"></param>
        /// <param name="buyerEmail"></param>
        /// <param name="buyerAddress"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvv"></param>
        /// <returns></returns>
        private bool AddTransaction(string productId, string greenMarketId, string productName, string category, string farmerName, string productionDate, string buyerName, string buyerEmail, string buyerAddress, string cardNumber, string cvv)
        {
            // Check if either productId or greenMarketId is provided
            if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(greenMarketId))
            {
                return false;
            }

            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007"; bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Transactions (ProductID, GreenMarketID, ProductName, Category, FarmerName, ProductionDate, BuyerName, BuyerEmail, BuyerAddress, CardNumber, CVV) " +
                                   "VALUES (@ProductID, @GreenMarketID, @ProductName, @Category, @FarmerName, @ProductionDate, @BuyerName, @BuyerEmail, @BuyerAddress, @CardNumber, @CVV)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters for ProductID and GreenMarketID
                        cmd.Parameters.AddWithValue("@ProductID", string.IsNullOrEmpty(productId) ? DBNull.Value : (object)productId);
                        cmd.Parameters.AddWithValue("@GreenMarketID", string.IsNullOrEmpty(greenMarketId) ? DBNull.Value : (object)greenMarketId);
                        // Add other parameters
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@FarmerName", farmerName);
                        cmd.Parameters.AddWithValue("@ProductionDate", productionDate);
                        cmd.Parameters.AddWithValue("@BuyerName", buyerName);
                        cmd.Parameters.AddWithValue("@BuyerEmail", buyerEmail);
                        cmd.Parameters.AddWithValue("@BuyerAddress", buyerAddress);
                        cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                        cmd.Parameters.AddWithValue("@CVV", cvv);

                        int rows = cmd.ExecuteNonQuery();
                        isSuccess = rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Response.Write(ex);
            }
            return isSuccess;
        }
        /// <summary>
        /// this method displays the transaction history
        /// </summary>
        /// <param name="userId"></param>
        private void DisplayTransactions(int userId)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AEPDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007"; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Transactions WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    PastTransactionsRepeater.DataSource = dt;
                    PastTransactionsRepeater.DataBind();                    
                }
            }
        }
        // cleans the form fields
        private void ClearFormFields()
        {
            txtProductName.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtProductionDate.Text = string.Empty;
            txtFarmerName.Text = string.Empty;
            txtBuyerName.Text = string.Empty;
            txtBuyerEmail.Text = string.Empty;
            txtBuyerAddress.Text = string.Empty;
            txtcardNumber.Text = string.Empty;
            txtCVV.Text = string.Empty;
        }
    }
}