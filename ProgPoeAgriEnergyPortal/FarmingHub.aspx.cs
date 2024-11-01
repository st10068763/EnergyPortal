﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgPoeAgriEnergyPortal
{
    public partial class FarmingHub : System.Web.UI.Page
    {
        //Connection string 
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\AEPDatabase.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FarmerID"] == null)
            {
                Response.Redirect("DashboardPage.aspx");
                // Message to display if the user is not an farmer
                Response.Write("<script>alert('You are not authorized to view this page');</script>");
            }
            if (!IsPostBack)
            {                
                BindPosts();
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


        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            string postTitle = txtPostTitle.Text;
            string postContent = txtPostContent.Text;
            // Change to "User_ID"
            int userId = Convert.ToInt32(Session["FarmerID"]);
            

            if (string.IsNullOrEmpty(postTitle) || string.IsNullOrEmpty(postContent))
            {
                lblMessage.Text = "Please fill in all fields";
                return;
            }
            // create the post
            if (CreatePost(postTitle, postContent))
            {
                PostMessageSuccess.Text = "Post created successfully";
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
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT p.*, (SELECT COUNT(*) FROM RepliesTB r WHERE r.PostID = p.PostID) AS ReplyCount FROM PostsTB p ORDER BY DateCreated DESC";

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

        protected void PostsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater replyRepeater = (Repeater)e.Item.FindControl("ReplyRepeater");
                int postId = (int)DataBinder.Eval(e.Item.DataItem, "PostID");

                 using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM RepliesTB WHERE PostID = @PostID ORDER BY DateCreated ASC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PostID", postId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    replyRepeater.DataSource = reader;
                    replyRepeater.DataBind();
                }
            }
        }

        protected void btnReply_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox txtReply = (TextBox)item.FindControl("txtReply");
            int postId = int.Parse(btn.CommandArgument);
            string replyContent = txtReply.Text;
            // gets the username of the logged in user
            int replyAuthor = Convert.ToInt32(Session["UserName"]);
            //string replyAuthor = "User"; 

            if (string.IsNullOrEmpty(replyContent))
            {
                lblMessage.Text = "The reply field must not be empty, write something to reply to the post.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO RepliesTB (PostID, Content, DateCreated, CreatedBy) VALUES (@PostID, @Content, @DateCreated, @CreatedBy)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PostID", postId);
                cmd.Parameters.AddWithValue("@Content", replyContent);
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", replyAuthor);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            // cleans the form
            CleanFields();
            BindPosts();
            txtReply.Text = string.Empty;
        }

        private void CleanFields()
        {           
            txtPostTitle.Text = string.Empty;
            txtPostContent.Text = string.Empty;
        }
    }
}