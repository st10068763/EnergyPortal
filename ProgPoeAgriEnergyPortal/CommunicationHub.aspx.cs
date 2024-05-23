using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class CommunicationHub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["Employee_ID"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
                else
                {
                    LoadMessages();
                }
            }

        }
        /// <summary>
        /// save the message to the database
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <param name="message"></param>
        private void SaveMessage(string senderId, string receiverId, string message)
        {
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
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
        /// Load the messages from the database
        /// </summary>
        private void LoadMessages()
        {

            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SenderId, MessageText, Timestamp FROM Messages ORDER BY Timestamp";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> messages = new List<string>();

                while (reader.Read())
                {
                    string senderId = reader["SenderId"].ToString();
                    string messageText = reader["MessageText"].ToString();
                    string timestamp = Convert.ToDateTime(reader["Timestamp"]).ToString("yyyy-MM-dd HH:mm:ss");

                    messages.Add($"<b>{senderId}</b>: {messageText} <i>({timestamp})</i>");
                }

                chatBox.InnerHtml = string.Join("<br />", messages);
            }
        }
        /// <summary>
        /// button to send the message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Get the message from the text box
            string message = txtMessage.Text.Trim();
            // Get the sender and receiver IDs from the session
            string senderId = Session["Employee_ID"].ToString(); 
            string receiverId = Session["Farmer_ID"].ToString(); 
            
            if (!string.IsNullOrEmpty(message))
            {
                SaveMessage(senderId, receiverId, message);
                txtMessage.Text = string.Empty;
                LoadMessages();
            }
        }
    }
}
