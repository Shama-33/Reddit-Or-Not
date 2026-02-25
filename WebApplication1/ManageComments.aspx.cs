using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    //string sessionUsername = Session["Username"] as string;
                    //bool isButtonVisible = CheckButtonVisibility(postId, sessionUsername);
                    //Btncmnt.Visible = isButtonVisible;
                }
                else
                {
                    // Handle the case when postId is not provided
                    Response.Redirect("Login.aspx");
                }


            }

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
                // Handle any exceptions or display an error message
            }

            return dt;
        }


        private DataTable GetPostData(string postID)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
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

        //comment_delete
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)deleteButton.NamingContainer;
            HiddenField commentIdHiddenField = (HiddenField)item.FindControl("commentIdHiddenField");
            string commentId = commentIdHiddenField.Value;

            // Delete the comment from the CommentTable using the commentId
            DeleteComment(commentId);

            // Refresh the CommentRepeater to reflect the updated comments
            BindCommentData();
        }

        private void DeleteComment(string commentId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
                string query = "DELETE FROM CommentTable WHERE comment_id = @commentId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@commentId", commentId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or display an error message
            }
        }

        private void BindCommentData()
        {
            // Get the postId from the query string
            string postId = Request.QueryString["postId"];

            // Call the GetCommentData method to retrieve the comment data for the postId
            DataTable commentTable = GetCommentData(postId);

            // Bind the commentTable to the CommentRepeater
            CommentRepeater.DataSource = commentTable;
            CommentRepeater.DataBind();
        }


    }
}