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
        }       

        //protected void ExploreForums_Click(object sender, EventArgs e)
        //{

        //}

        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            string postTitle = txtPostTitle.Text;
            string postContent = txtPostContent.Text;

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
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["FarmerID"]);

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
                                DateCreated = Convert.ToDateTime(reader["DateCreated"])
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