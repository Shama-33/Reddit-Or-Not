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
    public partial class CommunityPageMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Visible = false;
                btnDelete.Enabled = false;
                // Check if the community name query parameter exists
                if (Request.QueryString["communityName"] != null)
                {
                    // Retrieve the community name from the query parameter
                    string communityName = Server.UrlDecode(Request.QueryString["communityName"]);

                   
                    lblCommunityName.Text = " " + communityName;

                    // Fetch the community image based on the community name
                    string imagePath = GetCommunityImage(communityName);

                    // Set the image source
                    imgCommunity.ImageUrl = imagePath;

                    string username = Session["Username"] as string;
                    if (IsCommunityAdmin(communityName, username))
                    {
                        // Redirect to the CommunityPage
                        Response.Redirect("CommunityPage.aspx?communityName=" + Server.UrlEncode(communityName));
                    }
                    else
                    {
                        SetType(communityName);
                        // Check membership status
                        CheckMembershipStatus(communityName, username);
                        CheckCommunityVisibility(communityName);
                    }
                }
                else
                {
                    Response.Redirect("homepage_webform.aspx");
                }
            }
        }

        private void SetType(string communityName)
        {
            // Check if the username matches the community admin in the database
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT Description FROM CommunityTable WHERE Community_name = @Community_name " ;
            string type;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);
                   
                    connection.Open();
                    type = (string) command.ExecuteScalar();
                }
            }
            ComTopic.Text = "Community Type : " + type;

            // If count is greater than 0, it means the username is the community admin
         
        }

        private bool IsCommunityAdmin(string communityName, string username)
        {
            // Check if the username matches the community admin in the database
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT COUNT(*) FROM CommunityTable WHERE Community_name = @Community_name AND AdminName = @AdminName";
            int count;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);
                    command.Parameters.AddWithValue("@AdminName", username);
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }

            // If count is greater than 0, it means the username is the community admin
            return count > 0;
        }

        private string GetCommunityImage(string communityName)
        {
            // Fetch the image path from the database based on the community name
            string imagePath = string.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT CommunityImage FROM CommunityTable WHERE Community_name = @Community_name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);
                    connection.Open();
                    imagePath = command.ExecuteScalar()?.ToString();
                }
            }

            //  set a default image path if not fetched from the database
            if (string.IsNullOrEmpty(imagePath))
            {
                imagePath = "Images/default_avatar.jpg";
            }

            return imagePath;
        }


        //button click
        protected void btnJoin_Click(object sender, EventArgs e)
        {
            string communityName = lblCommunityName.Text.Trim();
            string username = Session["Username"] as string;

            if (!string.IsNullOrEmpty(communityName) && !string.IsNullOrEmpty(username))
            {
                // Generate a unique ID for the membership
                string membershipID = Guid.NewGuid().ToString();

                // Set the membership status as "pending"
                string status = "pending";

                // Insert the membership details into the MembershipTable
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "INSERT INTO MembershipTable (MembershipID, username, CommunityName, Status) VALUES (@MembershipID, @Username, @CommunityName, @Status)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MembershipID", membershipID);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@CommunityName", communityName);
                        command.Parameters.AddWithValue("@Status", status);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Membership details successfully inserted
                            // You can perform any additional actions or redirect to a different page
                            btnjoin.Text = "Requested";
                            //Response.Write("Request Sent");
                            btnjoin.Enabled = false;
                            btnDelete.Visible = true;
                            btnDelete.Enabled = true;
                        }
                        else
                        {
                           
                            Response.Write("Failed to join the community. Please try again.");
                        }
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string communityName = lblCommunityName.Text.Trim();
            string username = Session["Username"] as string;

            if (!string.IsNullOrEmpty(communityName) && !string.IsNullOrEmpty(username))
            {
                // Delete the membership entry from the MembershipTable
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "DELETE FROM MembershipTable WHERE CommunityName = @CommunityName AND username = @Username";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CommunityName", communityName);
                        command.Parameters.AddWithValue("@Username", username);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Membership details successfully deleted
                            // You can perform any additional actions or display a success message
                            btnDelete.Visible = false;
                            btnDelete.Enabled = false;
                            btnjoin.Enabled = true;
                            btnjoin.Text = "Join";
                        }
                        else
                        {
                            // Failed to delete membership details
                            // Handle the error or display an appropriate message
                            Response.Write("Failed to delete request. Please try again.");
                        }
                    }
                }
            }
        }






        private void CheckMembershipStatus(string communityName, string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT Status FROM MembershipTable WHERE CommunityName = @CommunityName AND username = @Username";
            string status;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CommunityName", communityName);
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    status = command.ExecuteScalar()?.ToString();
                }
            }

            if (status == "pending")
            {
                // User has already requested to join the community
                btnjoin.Text = "Requested";
                btnjoin.Enabled = false; // Disable the button
                btnDelete.Visible = true;
                btnDelete.Enabled = true;
            }
            else if (status == "approved")
            {
                //Response.Redirect("JoinedCommunity.aspx");
                Response.Redirect("JoinedCommunity.aspx?communityName=" + Server.UrlEncode(communityName));
            }
        }


        private void CheckCommunityVisibility(string communityName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT CommunityVisibilty FROM CommunityTable WHERE Community_name = @Community_name";
            string visibility;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);
                    connection.Open();
                    visibility = command.ExecuteScalar()?.ToString();
                }
            }

            if (!string.IsNullOrEmpty(visibility))
            {
                if (visibility == "Private")
                {
                    // Display message for private community
                  
                    lblPrivate.Text = "This community is private.";
                    postRepeater.Visible = false;
                }
                else if (visibility == "Restricted")
                {
                    lblPrivate.Text = "This community is Restricted. Join to vote and Comment";
                    if (!IsPostBack)
                    {
                        DataTable postTable = GetPostData(communityName);
                        Repeater1Res.DataSource = postTable;
                        Repeater1Res.DataBind();
                    }



                }
                else if (visibility == "Public")
                {
                    lblPrivate.Text = "This community is public.";
                    //lblPrivate.Text = "This community is Public.";
                    // string username = Session["Username"] as string;
                    if (!IsPostBack)
                    {
                        DataTable postTable = GetPostData(communityName);
                        postRepeater.DataSource = postTable;
                        postRepeater.DataBind();
                    }

                }
                else
                {
                }
                // Add more conditions for other visibility types if needed
            }
            else
            {
                lblPrivate.Text = "This community is not available.";

            }
        }











        //


        private DataTable GetPostData(string comname)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                               "FROM postTable " +
                               "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.CommunityName = @Comname";
                /*string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";*/

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Comname", comname);
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

        protected string GetImageUrl(object image)
        {
            string imageUrl = image.ToString();

            if (string.IsNullOrEmpty(imageUrl))
            {
                //imageUrl = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs="; // Base64 representation of a 1x1 transparent GIF image
            }

            return imageUrl;
        }



        protected void downvoteButton_Click(object sender, EventArgs e)
        {
            // Handle downvote button click event
            Button downvoteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)downvoteButton.NamingContainer;
            Label downvoteCountLabel = (Label)item.FindControl("downvoteCountLabel");

            // Get the post_id and username
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;
            string username = Session["username"].ToString();
            //string username = "shama";

            // Check if the user has already upvoted
            if (!HasDownvoted(postId, username))
            {
                // Increase upvotes in postTable
                IncreaseDownvotes(postId);

                // Insert the upvote into UpvoteTable
                InsertDownvote(postId, username);
            }


            // Retrieve the updated upvotes from postTable
            int downvotes = GetDownvotes(postId);

            // Update the upvoteCountLabel
            downvoteCountLabel.Text = downvotes.ToString();
        }

        private bool HasDownvoted(string postId, string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT COUNT(*) FROM DownvoteTable WHERE post_id = @postId AND username = @username";

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
                
                return false;
            }
        }
        private void IncreaseDownvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "UPDATE postTable SET downvotes = downvotes + 1 WHERE post_id = @postId";

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

        private void InsertDownvote(string postId, string username)
        {
            int status = 1;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "INSERT INTO DownvoteTable (post_id, username,status) VALUES (@postId, @username,@status)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@status", status);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
            }
        }


        private int GetDownvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT downvotes FROM postTable WHERE post_id = @postId";

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
                
                return 0;
            }
        }














        protected void commentButton_Click(object sender, EventArgs e)
        {
            // Handle comment button click event
            Button commentButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)commentButton.NamingContainer;

            // Get the post_id
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;

            // Redirect to comment.aspx page with the post ID as a query parameter
            Response.Redirect("Comment.aspx?postId=" + postId);
        }






        protected void upvoteButton_Click(object sender, EventArgs e)
        {
            Button upvoteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)upvoteButton.NamingContainer;
            Label upvoteCountLabel = (Label)item.FindControl("upvoteCountLabel");

            // Get the post_id and username
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;
            string username = Session["username"].ToString();
            //string username = "shama";

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
            int status = 1;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "INSERT INTO UpvoteTable (post_id, username,status) VALUES (@postId, @username,@status)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@status", status);
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