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
    public partial class JoinedCommunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {

                    // Check if the community name query parameter exists
                    if (Request.QueryString["communityName"] != null)
                    {
                        // Retrieve the community name from the query parameter
                        string communityName = Server.UrlDecode(Request.QueryString["communityName"]);


                        // Use the community name as needed (e.g., display it on the page)
                        lblCommunityName.Text = "" + communityName;

                        // Fetch the community image based on the community name
                        string imagePath = GetCommunityImage(communityName);

                        // Set the image source
                        imgCommunity.ImageUrl = imagePath;



                        string username = Session["Username"].ToString();

                        // Fetch and display the user's image from the database
                        string imageUrl = GetUserImage(username);
                        UserImage.ImageUrl = ResolveUrl(imageUrl);


                        DataTable postTable = GetPostData(communityName);
                        postRepeater.DataSource = postTable;
                        postRepeater.DataBind();
                        postRepeater.Visible = true;
                        Repeater1.Visible = false;

                    }

                    else
                    {
                        // Redirect to login page if not logged in
                        Response.Redirect("homepage_webform.aspx");
                    }






                }
                else
                {
                    // Redirect to login page if not logged in
                    Response.Redirect("Login.aspx");
                }

            }
        }

        protected string GetUserImage(string username)
        {
            string imageUrl = "~/Images/default_avatar.jpg"; // Default image URL if not found in the database

            try
            {
                // Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to fetch the image URL based on the username
                    string query = "SELECT image FROM UserInfo WHERE username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    // Execute the query and retrieve the image URL
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        imageUrl = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                // ...
            }

            return imageUrl;
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

            // For demonstration purposes, set a default image path if not fetched from the database
            if (string.IsNullOrEmpty(imagePath))
            {
                imagePath = "Images/default_avatar.jpg";
            }

            return imagePath;
        }


        //button click
        protected void btncancel_Click(object sender, EventArgs e)
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
                            Response.Redirect("homepage_webform.aspx");
                        }
                        else
                        {
                            // Failed to delete membership details
                            // Handle the error or display an appropriate message
                            Response.Write("Failed to delete membership. Please try again.");
                        }
                    }
                }
            }
        }
       
        protected void btnOption1_Click(object sender, EventArgs e)
        {
            ComAdmin.Visible = false;
            ComTopic.Visible = false;
            lblRequests.Visible = true;
            repeaterMembers.Visible = true;
            postRepeater.Visible = true;
            Repeater1.Visible = false;
            // Functionality for Option 3
            // This function will be called when Option 3 button is clicked
            // Add your code here

            // Calculate the count of approved requests
            int approvedCount = 0;
            string communityName = lblCommunityName.Text; // Get the community name from the label

            // Assuming you have a connection object named "con" for your SQL Server
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM MembershipTable WHERE CommunityName = @CommunityName AND Status = 'approved'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CommunityName", communityName);
                    approvedCount = (int)command.ExecuteScalar();
                }
            }

            // Display the count of approved requests
            lblRequests.Text = "Members: " + approvedCount;

            // Fetch the approved membership records for the repeater
            DataTable dtMembers = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT MembershipID, username FROM MembershipTable WHERE CommunityName = @CommunityName AND Status = 'approved'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CommunityName", communityName);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtMembers);
                    }
                }
            }

            // Bind the repeater with the membership records
            repeaterMembers.DataSource = dtMembers;
            repeaterMembers.DataBind();

            // Disable and hide the approve button
           
        }
        protected void btnOption2_Click(object sender, EventArgs e)
        {
            ComAdmin.Visible = false;
            ComTopic.Visible = false;
            lblRequests.Visible = false;
            repeaterMembers.Visible= false;
            postRepeater.Visible = true;
            Repeater1.Visible = false;
            // Functionality for Option 3
            // This function will be called when Option 3 button is clicked
            // Add your code here
            String communityName = lblCommunityName.Text;

            DataTable postTable = GetPostData_Your(communityName);
            postRepeater.DataSource = postTable;
            postRepeater.DataBind();
        }
        protected void btnOption3_Click(object sender, EventArgs e)
        {
            ComAdmin.Visible = false;
            ComTopic.Visible = false;
            lblRequests.Visible = false;
            repeaterMembers.Visible = false;
            postRepeater.Visible = true;
            Repeater1.Visible = false;
            // Functionality for Option 3
            // This function will be called when Option 3 button is clicked
            // Add your code here
            String communityName = lblCommunityName.Text;

            DataTable postTable = GetPostData_vote(communityName);
            postRepeater.DataSource = postTable;
            postRepeater.DataBind();
        }
        protected void btnOption4_Click(object sender, EventArgs e)
        {
            ComAdmin.Visible = false;
            ComTopic.Visible= false;
            lblRequests.Visible = false;
            repeaterMembers.Visible = false;
            postRepeater.Visible = true;
            Repeater1.Visible = false;
            // Functionality for Option 3
            // This function will be called when Option 3 button is clicked
            // Add your code here
            String communityName = lblCommunityName.Text;

            DataTable postTable = GetPostData_Latest(communityName);
            postRepeater.DataSource = postTable;
            postRepeater.DataBind();
        }


        protected void btnOption5_Click(object sender, EventArgs e)
        {
            ComTopic.Visible = true;
            ComAdmin.Visible = true;
            lblRequests.Visible = false;
            repeaterMembers.Visible = false;
            postRepeater.Visible = true;
            Repeater1.Visible = false;
            String communityName = lblCommunityName.Text;
            SetType(communityName);
            SetAdminName(communityName);



        }
        private void SetType(string communityName)
        {
            // Check if the username matches the community admin in the database
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT Description FROM CommunityTable WHERE Community_name = @Community_name ";
            string type;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);

                    connection.Open();
                    type = (string)command.ExecuteScalar();
                }
            }
            ComTopic.Text = "Community Type : " + type;

            // If count is greater than 0, it means the username is the community admin

        }
        private void SetAdminName(string communityName)
        {
            // Check if the username matches the community admin in the database
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT AdminName FROM CommunityTable WHERE Community_name = @Community_name ";
            string type;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);

                    connection.Open();
                    type = (string)command.ExecuteScalar();
                }
            }
            ComAdmin.Text = "Community Admin : " + type;

            // If count is greater than 0, it means the username is the community admin

        }


        protected void btnOption6_Click(object sender, EventArgs e)
        {
            ComTopic.Visible = false;
            ComAdmin.Visible = false;
            lblRequests.Visible = false;
            repeaterMembers.Visible = false;
            postRepeater.Visible = false;
            Repeater1.Visible = true;
            String communityName = lblCommunityName.Text;
            //SetType(communityName);
            //SetAdminName(communityName);
            DataTable postTable = GetPostData_Your(communityName);
            Repeater1.DataSource = postTable;
            Repeater1.DataBind();



        }
        protected void Repeater1_ItemCommand(object source, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string postId = e.CommandArgument.ToString();

                // Use the obtained postId to delete the record from the postTable
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM PostTable WHERE post_id = @postId";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@postId", postId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Refresh the data or perform any other desired action
                string communityName = lblCommunityName.Text;
                // GetPostData(communityName); // Example: assuming you have a method to bind post data to the repeater

                DataTable postTable = GetPostData(communityName);
                postRepeater.DataSource = postTable;
                postRepeater.DataBind();

            }
        }












        //CONVERT(DATETIME, postTable.date, 101)
        private DataTable GetPostData_Latest(string comname)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                               "FROM postTable " +
                               "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.CommunityName = @Comname " +
                               "ORDER BY CONVERT(DATETIME, postTable.date, 101) DESC";
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
                // Handle any exceptions or display an error message
            }

            return dt;
        }


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
                // Handle any exceptions or display an error message
            }

            return dt;
        }

        private DataTable GetPostData_Your(string comname)
        {
            string username = Session["Username"] as string;
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                               "FROM postTable " +
                               "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.CommunityName = @Comname AND username = @Username";
                /*string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";*/

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Comname", comname);
                    cmd.Parameters.AddWithValue("@Username", username);
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





        private DataTable GetPostData_vote(string comname)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                               "FROM postTable " +
                               "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.CommunityName = @Comname " + 
                               "ORDER BY  upvotes DESC";
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
                // Handle any exceptions or display an error message
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
                // Handle any exceptions or display an error message
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
                // Handle any exceptions or display an error message
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
                // Handle any exceptions or display an error message
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


        //


    }
}