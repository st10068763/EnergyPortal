using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProgPoeAgriEnergyPortal
{
    public partial class ProjectsAndFunds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEvent();
            }
        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            string projectName = txtProjectName.Text;
            string description = txtProjectDescription.Text;
            string projectType = ddlProjectType.SelectedValue;
            DateTime startDate;
            DateTime endDate;
            string projectLeaderName = txtProjectLeaderName.Text;

            if(!DateTime.TryParse(txtStartDate.Text, out startDate) || !DateTime.TryParse(txtEndDate.Text, out endDate))
            {
                lblErrorMessage.Text = "Please enter a valid date";
                return;
            }
            // validates the start and end date, end date cant be earlier than the start date
            if (startDate > endDate)
            {
                lblErrorMessage.Text = "End date cannot be earlier than the start date";
                return;
            }

            // calls the create project method
            CreateProject(projectName, description, projectType, startDate, endDate, projectLeaderName);
        }

        // Inserts project data in the database
        private bool CreateProject(string projectName, string description, string projectType, DateTime startDate, DateTime endDate, string projectLeaderName)
        {
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Projects (ProjectName, Description, ProjectType, StartDate, EndDate, ProjectLeaderName) VALUES (@ProjectName, @Description, @ProjectType, @StartDate, @EndDate, @ProjectLeaderName)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", projectName);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@ProjectType", projectType);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@ProjectLeaderName", projectLeaderName);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occured: " + ex;
            }

            return false;
        }

        private void BindEvent()
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Projects";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        ProductsRepeater.DataSource = reader;
                        ProductsRepeater.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Message to display if an error occurs
                lblErrorMessage.Text = "An error occured: " + ex;
            }

        }


        protected void btnJoinProject_Click(object sender, EventArgs e)
        {

        }

        protected void btnFundProject_Click(object sender, EventArgs e)
        {

        }
    }
}