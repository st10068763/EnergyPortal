using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class FarmingHub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadPosts();
                BindPosts();
            }
        }

        private void LoadPosts()
        {
            List<Post> posts = new List<Post>();
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PostsTB ORDER BY DateCreated DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Post post = new Post
                        {
                            PostID = Convert.ToInt32(reader["PostID"]),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString(),
                            ReplyCount = Convert.ToInt32(reader["ReplyCount"])
                        };
                        posts.Add(post);
                    }
                }
            }
        }
        /// <summary>
        /// this class represents a post in the database
        /// </summary>
        public class Post
        {
            public int PostID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public int ReplyCount { get; set; }
            public DateTime DateCreated { get; set; }
            public string CreatedBy { get; set; }
        }

        protected void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                // get the post ID
                Button btnReply = (Button)sender;
                int postID;

                // Check if CommandArgument is valid
                if (!int.TryParse(btnReply.CommandArgument, out postID))
                {
                    lblMessage.Text = "Invalid Post ID.";
                    return;
                }

                // get the reply content
                TextBox txtReply = (TextBox)btnReply.Parent.FindControl("txtReply");
                if (txtReply == null)
                {
                    lblMessage.Text = "Reply text box not found.";
                    return;
                }

                string replyContent = txtReply.Text;

                // Check if session variables are null
                if (Session["Farmer_ID"] == null)
                {
                    lblMessage.Text = "Session expired. Please log in again.";
                    return;
                }

                string createdBy = Session["Farmer_ID"].ToString();

                //if (Session["Farmer_ID"] == null)
                //{
                //    lblMessage.Text = "Session expired. Please log in again.";
                //    return;
                //}

                int userId;
                if (!int.TryParse(Session["Farmer_ID"].ToString(), out userId))
                {
                    lblMessage.Text = "Invalid user ID.";
                    return;
                }

                if (string.IsNullOrEmpty(replyContent))
                {
                    lblMessage.Text = "Write something to be posted in the post content field.";
                    return;
                }

                if (CreateReply(postID, replyContent, createdBy))
                {
                    lblMessage.Text = "Reply created successfully.";
                    BindPosts();
                }
                else
                {
                    lblMessage.Text = "An error occurred while creating the reply.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"An error occurred while creating the reply: {ex.Message}<br/>{ex.StackTrace}";
            }
        }


        private bool CreateReply(int postID, string replyContent, string createdBy)
        {
            bool isSuccess = false;
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO RepliesTB (PostID, Content, DateCreated, CreatedBy) VALUES (@PostID, @Content, @DateCreated, @CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PostID", postID);
                        cmd.Parameters.AddWithValue("@Content", replyContent);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["Farmer_ID"]);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        isSuccess = rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}<br/>{ex.StackTrace}";
            }
            return isSuccess;
        }


        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            string postTitle = txtPostTitle.Text;
            string postContent = txtPostContent.Text;
            //gets the user ID of the logged in user
            int userId = Convert.ToInt32(Session["Farmer_ID"]);
            

            if (string.IsNullOrEmpty(postTitle) || string.IsNullOrEmpty(postContent))
            {
                lblMessage.Text = "Please fill in all fields";
                return;
            }
            // create the post
            if (CreatePost(postTitle, postContent))
            {
                lblMessage.Text = "Post created successfully";
                BindPosts();
                CleanFields();
            }
            else
            {
                lblMessage.Text = "An error occurred while creating the post";
            }
        }

        private bool CreatePost(string postTitle, string postContent)
        {
            bool isSuccess = false;
            
            try
            {
                string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO PostsTB (Title, Content, DateCreated, CreatedBy) VALUES (@Title, @Content, @DateCreated, @CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", postTitle);
                        cmd.Parameters.AddWithValue("@Content", postContent);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["Farmer_ID"]);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        isSuccess = rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}<br/>{ex.StackTrace}";
            }
            return isSuccess;
        }

        private void BindPosts()
        {
            List<Post> posts = new List<Post>();
            string connectionString = "Data Source=agrisqlserver.database.windows.net;Initial Catalog=AgriEnergyDB;Persist Security Info=True;User ID=st10068763;Password=MyName007";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM PostsTB ORDER BY DateCreated DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                PostID = Convert.ToInt32(reader["PostID"]),
                                Title = reader["Title"].ToString(),
                                Content = reader["Content"].ToString(),
                                ReplyCount = Convert.ToInt32(reader["ReplyCount"]),
                                DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                                CreatedBy = reader["CreatedBy"].ToString()
                            };
                            posts.Add(post);
                        }
                    }
                }
                PostsRepeater.DataSource = posts;
                PostsRepeater.DataBind();
            }
            
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}<br/>{ex.StackTrace}";
            }
            
        }

        private void CleanFields()
        {
            txtPostTitle.Text = string.Empty;
            txtPostContent.Text = string.Empty;
        }
    }
}