using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Globalization;
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
            double productPrice ;
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
            // Validate the product price to ensure that the user uses an dot to separate the decimal
            if (!double.TryParse(txtPrice.Text, out productPrice))
            {
                lblMessage.Text = "Please enter a valid price. Use dot'.' as decimal separator";
                return;
            }
            
            // Get the farmer id from the session
            int farmerId = Convert.ToInt32(Session["Farmer_ID"]);


            //calling method that creates event
            if (CreateEvent(eventName, category, productPrice, farmerId, eventImage, eventDescription, farmerName, eventDate))
            {
                DisplayMessage("A new event has been added successfully");
                ClearFormFields();
                BindEvent();
            }
            else
            {
                DisplayMessage("Failed to add event");
            }
        }

        private void DisplayMessage(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }


        // method to create an event
        public bool CreateEvent(string eventName, string category, double productPrice, int farmerId, string eventImage, string eventDescription, string farmerName, DateTime eventDate)
        {
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

                string query = "INSERT INTO EventsTB(Event_Name, Category, Product_Price, Farmer_ID, Event_Image, Description, FarmerName, EventDate) " +
                                             "VALUES (@Event_Name, @Category, @Product_Price, @Farmer_ID, @Event_Image, @Description, @FarmerName, @EventDate)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Event_Name", eventName);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Product_Price", productPrice);
                        cmd.Parameters.AddWithValue("@Farmer_ID", farmerId);
                        cmd.Parameters.AddWithValue("@Event_Image", eventImage);
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
                return false;
            }
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

        protected void EnrollBtn_Click(object sender, EventArgs e)
        {
            // Get the event id from the hidden field
            int eventId = Convert.ToInt32(((Button)sender).CommandArgument);
            // Get the farmer id from the session
            int farmerId = Convert.ToInt32(Session["Farmer_ID"]);


            int employee = Convert.ToInt32(Session["Employee_ID"]);           
            // verifies if the user has already enrolled for the event
            if (VerifyEnrollment(eventId, farmerId))
            {
                lblMessage.Text = "You have already enrolled for this event";
            }
            // Enroll the farmer to the event
            if (EnrollEvent(eventId, farmerId))
            {
                // Displays a green message
                CorrectMessage.Text = "You have successfully enrolled for the event";

            }
            else
            {
                lblMessage.Text = "An error occurred while enrolling for the event";
            }
        }

        // Method to verify if user has already enrolled for the event
        private bool VerifyEnrollment(int eventId, int farmerId)
        {
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                string query = "SELECT * FROM EventEnrollments WHERE Event_ID = @Event_ID AND Farmer_ID = @Farmer_ID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Event_ID", eventId);
                        cmd.Parameters.AddWithValue("@Farmer_ID", farmerId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}<br/>{ex.StackTrace}";
            }
            return false;
        }

        private bool EnrollEvent(int eventId, int farmerId)
        {
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                string query = "INSERT INTO EventEnrollments (Event_ID, Farmer_ID) VALUES (@Event_ID, @Farmer_ID)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Event_ID", eventId);
                        cmd.Parameters.AddWithValue("@Farmer_ID", farmerId);
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