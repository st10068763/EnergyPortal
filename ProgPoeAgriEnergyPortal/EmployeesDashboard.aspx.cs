using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class EmployeesDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddFarmer_Click(object sender, EventArgs e)
        {
            // Logic to add a new farmer
            string name = txtFarmerName.Text;
            string contact = txtFarmerContact.Text;
            string location = txtFarmerLocation.Text;

            // add new farmer to the database

            // Clear the input fields
            txtFarmerName.Text = "";
            txtFarmerContact.Text = "";
            txtFarmerLocation.Text = "";
        }

        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            // Logic to search products
            string query = txtSearchProduct.Text;

            // Perform search and bind results to GridViewProducts
            DataTable products = SearchProducts(query);
            GridViewProducts.DataSource = products;
            GridViewProducts.DataBind();
        }

        protected void btnSearchFarmer_Click(object sender, EventArgs e)
        {
            // Logic to search farmers
            string query = txtSearchFarmer.Text;

            // Perform search and bind results to GridViewFarmers
            DataTable farmers = SearchFarmers(query);
            GridViewFarmers.DataSource = farmers;
            GridViewFarmers.DataBind();
        }

        private DataTable SearchProducts(string query)
        {
            // Implement product search logic here and return a DataTable
            // For demo purposes, returning an empty DataTable
            return new DataTable();
        }

        private DataTable SearchFarmers(string query)
        {
            // Implement farmer search logic here and return a DataTable
            // For demo purposes, returning an empty DataTable
            return new DataTable();
        }
    }
}