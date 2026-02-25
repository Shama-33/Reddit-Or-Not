using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            // BindPostData();
            if (!IsPostBack)
            {
                DataTable postTable = GetPostData();
                postRepeater.DataSource = postTable;
                postRepeater.DataBind();
            }



        }


        

        private DataTable GetPostData()
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                /*string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1 " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";*/
                string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
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
                imageUrl = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs="; // Base64 representation of a 1x1 transparent GIF image
            }

            return imageUrl;
        }

        

        protected void downvoteButton_Click(object sender, EventArgs e)
        {
            // Handle downvote button click event
        }

        protected void commentButton_Click(object sender, EventArgs e)
        {
            // Handle comment button click event
        }




        protected void shareButton_Click(object sender, EventArgs e)
        {
            // Handle comment button click event
            Button buttonShare = (Button)sender;
            RepeaterItem repeaterItem = (RepeaterItem)buttonShare.NamingContainer;
            HiddenField postIdHiddenField = (HiddenField)repeaterItem.FindControl("postIdHiddenField");
            string postId = postIdHiddenField.Value;

            string sharableLink = Request.Url.Scheme + "://" + Request.Url.Authority + "/PostDetails.aspx?postId=" + postId;

            // Display the sharable link in a dialog box or any other desired way
            string script = $"alert('Sharable Link: {sharableLink}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ShareLinkScript", script, true);
        }






        protected void upvoteButton_Click(object sender, EventArgs e)
        {
            Button upvoteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)upvoteButton.NamingContainer;
            Label upvoteCountLabel = (Label)item.FindControl("upvoteCountLabel");

            // Get the post_id and username
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;
            //string username = Session["username"].ToString();
            string username = "shama";

            // Check if the user has already upvoted
            if (!HasUpvoted(postId, username))
            {
                // Increase upvotes in postTable
                IncreaseUpvotes(postId);

                // Insert the upvote into UpvoteTable
                InsertUpvote(postId, username);
            }

            // Retrieve the updated upvotes from postTable
            int upvotes = GetUpvotes(postId);

            // Update the upvoteCountLabel
            upvoteCountLabel.Text = upvotes.ToString();
        }

        private bool HasUpvoted(string postId, string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT COUNT(*) FROM UpvoteTable WHERE post_id = @postId AND username = @username";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or display an error message
                return false;
            }
        }
        private void IncreaseUpvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "UPDATE postTable SET upvotes = upvotes + 1 WHERE post_id = @postId";

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
                // Handle any exceptions or display an error message
            }
        }

        private void InsertUpvote(string postId, string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "INSERT INTO UpvoteTable (post_id, username) VALUES (@postId, @username)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or display an error message
            }
        }
        private int GetUpvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT upvotes FROM postTable WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    object result = cmd.ExecuteScalar();

                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or display an error message
                return 0;
            }
        }






    }
}
