using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ManageCommunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the community name query parameter exists
                if (Request.QueryString["communityName"] != null)
                {
                    // Retrieve the community name from the query parameter
                    string communityName = Server.UrlDecode(Request.QueryString["communityName"]);

                    // Use the community name as needed (e.g., display it on the page)
                    lblCommunityname.Text = communityName;

                    // Fetch and display the community details from the database
                    PopulateCommunityDetails(communityName);

                    if (!IsPostBack)
                    {
                        DataTable postTable = GetPostData(communityName);
                        postRepeater.DataSource = postTable;
                        postRepeater.DataBind();
                    }
                }

                // Check if the username exists in the session
                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();

                    
                }
                else
                {
                    // Redirect to the login page if the username is not available in the session
                    Response.Redirect("Login.aspx");
                }
            }

        }



        /*protected void PopulateCommunityDetails(string communityName)
        {
            try
            {
                // Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to fetch the community details based on the community name
                    string query = "SELECT Description, CommunityVisibilty FROM CommunityTable WHERE Community_name = @CommunityName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CommunityName", communityName);

                    // Execute the query and retrieve the community details
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the community description
                            string description = reader["Description"].ToString();
                            lbltype.Text = description;

                            // Populate the community visibility (privacy)
                            string visibility = reader["CommunityVisibilty"].ToString();
                            Labelprivacy.Text = visibility;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                // ...
            }
        }*/

        protected void PopulateCommunityDetails(string communityName)
        {
            try
            {
                // Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to fetch the community details based on the community name
                    string query = "SELECT Description, CommunityVisibilty, CommunityImage FROM CommunityTable WHERE Community_name = @CommunityName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CommunityName", communityName);

                    // Execute the query and retrieve the community details
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the community description
                            string description = reader["Description"].ToString();
                            lbltype.Text = description;

                            // Populate the community visibility (privacy)
                            string visibility = reader["CommunityVisibilty"].ToString();
                            Labelprivacy.Text = visibility;

                            // Populate the current community image
                            string imagePath = reader["CommunityImage"].ToString();

                            // Set the image URL, or use a default image if the path is null or empty
                            currentImage.ImageUrl = !string.IsNullOrEmpty(imagePath) ? ResolveUrl(imagePath) : ResolveUrl("Images/default_avatar.jpg");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                // ...
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            // Get the selected visibility from the dropdown list
            string visibility = ddlVisibility.SelectedValue;

            // Update the CommunityVisibilty attribute in the database table
            UpdateCommunityVisibility(visibility);

            // Optionally, you can display a success message or perform any other necessary actions
        }

        private void UpdateCommunityVisibility(string visibility)
        {
            // Update the CommunityVisibilty attribute in the database table
            try
            {
                // Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to update the CommunityVisibilty attribute
                    string query = "UPDATE CommunityTable SET CommunityVisibilty = @Visibility WHERE Community_name = @Community_name";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Visibility", visibility);
                    command.Parameters.AddWithValue("@Community_name", lblCommunityname.Text);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                // ...
            }
        }

        protected void UploadAvatarNew(object sender, EventArgs e)
        {
            // Check if a file is uploaded
            if (fileAvatar.HasFile)
            {
                try
                {
                    // Save the uploaded file to a specific location
                    string fileName = Path.GetFileName(fileAvatar.PostedFile.FileName);
                    string filePath = Server.MapPath("~/Avatars/") + fileName;
                    fileAvatar.SaveAs(filePath);

                    // Update the user's avatar URL in the database
                    string username = Session["Username"] as string;
                    UpdateCommunityImage( "~/Avatars/" + fileName);

                    // Set the current image to the uploaded image
                    currentImage.ImageUrl = "~/Avatars/" + fileName;











                   
                }
                catch (Exception ex)
                {
                    // Handle the exception or log the error
                    // ...
                }
            }
        }


        private void UpdateCommunityImage(string imagePath)
        {
            // Update the CommunityImage attribute in the database table
            try
            {
                // Establish a connection to the database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to update the CommunityImage attribute
                    string query = "UPDATE CommunityTable SET CommunityImage = @Image WHERE Community_name = @Community_name";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Image", imagePath);
                    command.Parameters.AddWithValue("@Community_name", lblCommunityname.Text);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
               // currentImage.ImageUrl = ResolveUrl(imagePath);
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                // ...
            }
        }




        protected void btnOption1_Click(object sender, EventArgs e)
        {
            
            // This function will be called when Option 1 button is clicked

            // Calculate the count of pending requests
            int pendingCount = 0;
            string communityName = lblCommunityname.Text; // Get the community name from the label

            // Assuming you have a connection object named "con" for your SQL Server
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM MembershipTable WHERE CommunityName = @CommunityName AND Status = 'pending'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CommunityName", communityName);
                    pendingCount = (int)command.ExecuteScalar();
                }
            }

            // Display the count of pending requests
            lblRequests.Text = "Requests: " + pendingCount;

            // Fetch the pending membership records for the repeater
            DataTable dtMembers = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT MembershipID, username FROM MembershipTable WHERE CommunityName = @CommunityName AND Status = 'pending'";
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
        }


        /* protected void btnOption2_Click(object sender, EventArgs e)
         {
             // Functionality for Option 2
             // This function will be called when Option 2 button is clicked
             // Add your code here


             // Calculate the count of pending requests
             int pendingCount = 0;
             string communityName = lblCommunityname.Text; // Get the community name from the label

             // Assuming you have a connection object named "con" for your SQL Server
             using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
             {
                 connection.Open();
                 string query = "SELECT COUNT(*) FROM MembershipTable WHERE CommunityName = @CommunityName AND Status = 'approved'";
                 using (SqlCommand command = new SqlCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@CommunityName", communityName);
                     pendingCount = (int)command.ExecuteScalar();
                 }
             }

             // Display the count of pending requests
             lblRequests.Text = "Requests: " + pendingCount;

             // Fetch the pending membership records for the repeater
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
         }*/

        protected void btnOption2_Click(object sender, EventArgs e)
        {
            // Functionality for Option 2
            // This function will be called when Option 2 button is clicked
           

            // Calculate the count of approved requests
            int approvedCount = 0;
            string communityName = lblCommunityname.Text; // Get the community name from the label

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
            foreach (RepeaterItem item in repeaterMembers.Items)
            {
                Button btnApprove = (Button)item.FindControl("btnApprove");
                btnApprove.Enabled = false;
                btnApprove.Visible = false;
            }
        }


        protected void btnOption3_Click(object sender, EventArgs e)
        {
            repeaterMembers.Visible= false;
            lblRequests.Visible= false;
           
            string communityName = lblCommunityname.Text;
            if (!IsPostBack)
            {
                DataTable postTable = GetPostData(communityName);
                postRepeater.DataSource = postTable;
                postRepeater.DataBind();
            }
        }

  

        protected void btnApprove_Command(object sender, CommandEventArgs e)
        {
            string membershipID = e.CommandArgument.ToString();

            // Update the status to 'approved' in the MembershipTable
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Open();
                string query = "UPDATE MembershipTable SET Status = 'approved' WHERE MembershipID = @MembershipID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MembershipID", membershipID);
                    command.ExecuteNonQuery();
                }
            }

            // Refresh the pending requests and membership records
            btnOption1_Click(null, EventArgs.Empty);
        }

        protected void btnRemove_Command(object sender, CommandEventArgs e)
        {
            string membershipID = e.CommandArgument.ToString();

            // Delete the row from the MembershipTable
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM MembershipTable WHERE MembershipID = @MembershipID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MembershipID", membershipID);
                    command.ExecuteNonQuery();
                }
            }

            // Refresh the pending requests and membership records
            btnOption1_Click(null, EventArgs.Empty);
        }

        private DataTable GetPostData(string ComName)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
                string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                               "FROM postTable " +
                               "LEFT OUTER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " + "WHERE postTable.CommunityName = @CommunityName";
                /*string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";*/

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CommunityName", ComName);
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
            string communityName = lblCommunityname.Text;
            Button downvoteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)downvoteButton.NamingContainer;
            HiddenField postIdHiddenField = (HiddenField)item.FindControl("postIdHiddenField");
            string postId = postIdHiddenField.Value;

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
            GetPostData(communityName); // Example: assuming you have a method to bind post data to the repeater
        }

        protected void postRepeater_ItemCommand(object source, CommandEventArgs e)
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
                string communityName = lblCommunityname.Text;
                // GetPostData(communityName); // Example: assuming you have a method to bind post data to the repeater
              
                    DataTable postTable = GetPostData(communityName);
                    postRepeater.DataSource = postTable;
                    postRepeater.DataBind();
                
            }
        }

       

        protected void DeleteCOM(object sender, EventArgs e)
        {
            string userCaptcha = Deletecom.Text.Trim();
            string storedCaptcha = "CONFIRM";// Retrieve the stored CAPTCHA value from session
            if (userCaptcha == storedCaptcha)
            {
                string communityName = lblCommunityname.Text;
                // Use the obtained postId to delete the record from the postTable
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM PostTable WHERE CommunityName = @CommunityName";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CommunityName", communityName);
                        cmd.ExecuteNonQuery();
                    }
                }
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM MembershipTable WHERE CommunityName = @CommunityName";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CommunityName", communityName);
                        cmd.ExecuteNonQuery();
                    }
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM CommunityTable WHERE Community_name = @CommunityName";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CommunityName", communityName);
                        cmd.ExecuteNonQuery();
                    }
                }

                // CAPTCHA validation successful, proceed with deletion
                // Delete community logic here
                Response.Redirect("homepage_webform.aspx");
                Lbldelete.Text = "Community deleted successfully.";
            }
            else
            {
                // CAPTCHA validation failed, display an error message
                Lbldelete.Text = "Invalid Input. Please try again.";
            }
        }







    }
}