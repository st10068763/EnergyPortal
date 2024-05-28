using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class EducationHub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEvent();
            }
        }       
        /// <summary>
        /// create event button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            string eventName = txtEventName.Text;
            DateTime eventDate;
            string farmerName = txtLectureName.Text;
            float productPrice = float.Parse(txtPrice.Text);
            string eventDescription = txtEventDescription.Text;
            string category = ddlEventType.SelectedValue;
            string eventImage = txtEventImage.Text;

            // validate the fields
            if (!DateTime.TryParse(txtEventDate.Text, out eventDate))
            {
                lblMessage.Text = "Please enter a valid date";
                return;
            }
            // check if the fields are empty
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(eventDescription) || string.IsNullOrEmpty(eventImage) || !DateTime.TryParse(txtEventDate.Text, out eventDate))
            {
                lblMessage.Text = "Please fill in all the fields";
                return;
            }
            // Get the farmer id from the session
            int farmerId = Convert.ToInt32(Session["FarmerID"]);

            // Create the event entered by the user
            if (CreateEvent(eventName, category, productPrice, farmerId, eventImage, eventDescription, farmerName, eventDate))
            {
                lblMessage.Text = "Event created successfully";
                BindEvent();
                ClearFormFields();
            }           
            else
            {
                lblMessage.Text = "An error occured while creating the event, verify the event information";
            }
        }

        // method to create an event
        private bool CreateEvent(string eventName, string category, float productPrice, int farmerId, string eventImage, string eventDescription, string farmerName, DateTime eventDate)
        {
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                // bool isSuccess = false;
                string query = "INSERT INTO EventsTB (Event_Name, Category, Product_Price, Farmer_ID, Event_Image, Description, FarmerName, EventDate) " +
                                             "VALUES (@EventName, @Category, @ProductPrice, @FarmerId, @EventImage, @Description, @FarmerName, @EventDate)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EventName", eventName);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@ProductPrice", productPrice);
                        cmd.Parameters.AddWithValue("@FarmerId", farmerId);
                        cmd.Parameters.AddWithValue("@EventImage", eventImage);
                        cmd.Parameters.AddWithValue("@Description", eventDescription);
                        cmd.Parameters.AddWithValue("@FarmerName", farmerName);
                        cmd.Parameters.AddWithValue("@EventDate", eventDate);
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }                     
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}<br/>{ex.StackTrace}";
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
                    string query = "SELECT * FROM EventsTB";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        rptEvents.DataSource = reader;
                        rptEvents.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                // Message to display if an error occurs
                lblMessage.Text = "Error: Failed to load events";
            }
           
        }

        private void ClearFormFields()
        {
            txtEventName.Text = "";
            txtEventDescription.Text = "";
            txtLectureName.Text = "";
            ddlEventType.SelectedIndex = 0;
            txtEventDate.Text = "";
            txtEventImage.Text = "";
            txtPrice.Text = "";
        }
    }
}