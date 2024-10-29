using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class CommunicationHub : Page
    {
        //Connection string 
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\AEPDatabase.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if either an employee or a farmer is logged in
                if (Session["EmployeeID"] == null && Session["FarmerID"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
                else
                {
                    LoadMessages();
                    LoadUsers();
                }
            }
        }

        /// <summary>
        /// This method loads all the available users in the system into the dropdown list so the sender can select a receiver
        /// </summary>
        private void LoadUsers()
        {
            string query = @"SELECT EmployeeID AS UserID, EmployeeName AS UserName FROM Employee
                UNION
                SELECT FarmerID AS UserID, FarmerName AS UserName FROM Farmer";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlReceiver.Items.Clear();

                        while (reader.Read())
                        {
                            string userId = reader["UserID"].ToString();
                            string userName = reader["UserName"].ToString();
                            ddlReceiver.Items.Add(new ListItem(userName, userId));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This will save the message in the database
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <param name="message"></param>
        private void SaveMessage(string senderId, string receiverId, string message)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Messages (SenderId, ReceiverId, MessageText, Timestamp) VALUES (@SenderId, @ReceiverId, @MessageText, @Timestamp)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SenderId", senderId);
                cmd.Parameters.AddWithValue("@ReceiverId", receiverId);
                cmd.Parameters.AddWithValue("@MessageText", message);
                cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to load all the messages from the database in the chat box so that employees and farmers can communicate and see the messages from each other
        /// </summary>
        private void LoadMessages()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Query to get all the messages from the database and order them by time it was sent
                string query = @"SELECT m.MessageText, m.Timestamp, u.UserName AS SenderName FROM Messages m JOIN Users u ON m.SenderId = u.UserID
                                 ORDER BY m.Timestamp";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> messages = new List<string>();

                // Loop through the messages and display them in the chat box
                while (reader.Read())
                {
                    string senderName = reader["SenderName"].ToString();
                    string messageText = reader["MessageText"].ToString();
                    string timestamp = Convert.ToDateTime(reader["Timestamp"]).ToString("yyyy-MM-dd HH:mm:ss");

                    messages.Add($"<b>{senderName}</b>: {messageText} <i>({timestamp})</i>");
                }
                chatBox.InnerHtml = string.Join("<br />", messages);
            }
        }

        /// <summary>
        /// Button click event to send the message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Get the message from the text box
            string message = txtMessage.Text.Trim();
            // Get the sender ID from the session (either Employee_ID or Farmer_ID)
            string senderId = Session["EmployeeID"] != null ? Session["EmployeeID"].ToString() : Session["FarmerID"].ToString();
            string receiverId = ddlReceiver.SelectedValue;
            // Check if the message is not empty if its not empty save the message in the database and displays it in the chat box
            if (!string.IsNullOrEmpty(message))
            {
                SaveMessage(senderId, receiverId, message);
                txtMessage.Text = string.Empty;
                LoadMessages();
            }
        }
    }
}
