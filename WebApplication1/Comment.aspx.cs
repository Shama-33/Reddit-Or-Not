using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication1
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Btncmnt.Visible = false;
            if (!IsPostBack)
            {
                string postId;
                if (Request.QueryString["postId"] != null)
                {
                     postId = Request.QueryString["postId"];
                    DataTable postTable = GetPostData(postId);
                    postRepeater.DataSource = postTable;
                    postRepeater.DataBind();
                    
                    DataTable CommentTable = GetCommentData(postId);
                    CommentRepeater.DataSource = CommentTable;
                    CommentRepeater.DataBind();
                    string sessionUsername = Session["Username"] as string;
                    bool isButtonVisible = CheckButtonVisibility(postId, sessionUsername);
                    Btncmnt.Visible = isButtonVisible;
                }
                else
                {
                    
                    Response.Redirect("Login.aspx");
                }

               
            }

        }

        protected void cmntButton_Click(object sender, EventArgs e)
        {
            string postId = Request.QueryString["postId"];
            Response.Redirect("ManageComments.aspx?postId=" + postId);

        }


        private bool CheckButtonVisibility(string postId, string sessionUsername)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
                string query = "SELECT username FROM postTable WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string postUsername = reader["username"].ToString();
                            if (postUsername == sessionUsername)
                            {
                                reader.Close();
                                return true;
                            }
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        private DataTable GetCommentData(string postID)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                /*string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1 " +
                               "FROM postTable " +
                               "LEFT OUTER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.username = @Username";*/
                string query = "SELECT  CommentTable.comment_id, CommentTable.post_id, CommentTable.commenter, CommentTable.date, CommentTable.comment_content, UserInfo.image " +
                              "FROM CommentTable " +
                              "LEFT OUTER JOIN UserInfo ON CommentTable.commenter = UserInfo.username " +
                              "WHERE CommentTable.post_id= @postID ";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@postID", postID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
               
            }

            return dt;
        }


        private DataTable GetPostData(string postID)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                /*string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1 " +
                               "FROM postTable " +
                               "LEFT OUTER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.username = @Username";*/
                string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "LEFT OUTER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + 
                              "WHERE postTable.post_id= @postID ";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@postID", postID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or display an error message
            }

            return dt;
        }
        protected string GetImageUrl(object image)
        {
            string imageUrl = image.ToString();

            if (string.IsNullOrEmpty(imageUrl))
            {
                //imageUrl = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs="; // Base64 representation of a 1x1 transparent GIF image
            }

            return imageUrl;
        }


        protected void uploadButton_Click(object sender, EventArgs e)
        {
            string comment = commentTextBox.Text;
            string postId = Request.QueryString["postId"];
            string username = Session["username"] as string;

            // Generate a unique comment ID
            string commentId = Guid.NewGuid().ToString();


            // Save the comment to the database

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "INSERT INTO CommentTable (comment_id,post_id, commenter,comment_content,date) VALUES (@comment_id,@post_id, @commenter,@comment_content,@date)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@comment_id", commentId);
                    cmd.Parameters.AddWithValue("@post_id", postId);
                    cmd.Parameters.AddWithValue("@commenter", username);
                    cmd.Parameters.AddWithValue("@comment_content", comment);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    IncreaseComments(postId);
                }
            }
            catch (Exception ex)
            {
                
            }


            // Redirect to the current page to clear the form inputs
            Response.Redirect(Request.Url.ToString());
        }


        private void IncreaseComments(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "UPDATE postTable SET no_comments = no_comments + 1 WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
            }
        }

    }
}