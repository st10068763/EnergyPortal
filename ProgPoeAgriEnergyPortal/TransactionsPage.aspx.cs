using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class TransactionsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductDetails();
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
            // Display the product details
            if (!string.IsNullOrEmpty(productId))
            {
                txtProductName.Text = productName;
                txtCategory.Text = category;
                txtProductionDate.Text = productionDate;
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
            string productName = txtProductName.Text;
            string category = txtCategory.Text;
            string productionDate = txtProductionDate.Text;
            string buyerName = txtBuyerName.Text;
            string buyerEmail = txtBuyerEmail.Text;
            string buyerAddress = txtBuyerAddress.Text;
            string cardNumber = txtcardNumber.Text;
            string cvv = txtCVV.Text;

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

            // Add the transaction to the database
            if (AddTransaction(productId, productName, category, productionDate, buyerName, buyerEmail, buyerAddress, cardNumber, cvv))
            {
                Response.Write("<script>alert('Transaction successful');</script>");
            }
            else
            {
                Response.Write("<script>alert('Failed to complete transaction');</script>");
            }
        }

        //Add the transaction to the database
        private bool AddTransaction(string productId, string productName, string category, string productionDate, string buyerName, string buyerEmail, string buyerAddress, string cardNumber, string cvv)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Transactions (ProductID, ProductName, Category, ProductionDate, BuyerName, BuyerEmail, BuyerAddress, CardNumber, CVV) " +
                                "VALUES (@ProductID, @ProductName, @Category, @ProductionDate, @BuyerName, @BuyerEmail, @BuyerAddress, @CardNumber, @CVV)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Category", category);
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
                Response.Write(ex);
            }
            return isSuccess;
        }

    }
}