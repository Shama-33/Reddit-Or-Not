using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        private string postImage = String.Empty; // Declare a class-level variable
        private bool isImageUploaded; // Flag to track if an image has been uploaded
        private string uploadedImageFileName; // Variable to store the uploaded image file name
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["postId"] != null)
                { string postId = Request.QueryString["postId"]; 
                    LoadPostContent(postId);
                    LoadPostImage(postId);
                }
                else
                {
                    Response.Redirect("Profile.aspx");
                }
                // Check if the username is stored in the session
                if (Session["Username"] == null)
                {
                    // Redirect to the login page
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    // Fetch and display the user's image from the database
                    string username = Session["Username"].ToString();
                    string imageUrl = GetUserImage(username);
                    Image1.ImageUrl = ResolveUrl(imageUrl);
                }
            }
        }

        protected string GetUserImage(string username)
        {
            string imageUrl = "~/Images/generaluser.png"; // Default image URL if not found in the database

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
               
            }

            return imageUrl;
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string postId = Request.QueryString["postId"];
            string postContent = TextArea1.Value;
            // string postImage = "abc";

          

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE PostTable SET post_content = @PostContent " +
                        "WHERE post_id= @PostID ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PostID", postId);
                 
                    command.Parameters.AddWithValue("@PostContent", postContent);
                   

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "UpdateSuccessful", "alert('Update Successful.');", true);


                    }
                    else
                    {
                        // Failed to insert the post
                        ScriptManager.RegisterStartupScript(this, GetType(), "UploadFailed", "alert('Upload failed.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }








        protected void Button2_Click(object sender, EventArgs e)
        {
            string postId = Request.QueryString["postId"];
            //string id = Guid.NewGuid().ToString();
            //string username = Session["Username"].ToString();
            //string s_date = DateTime.Now.ToString();
            //string postContent = TextArea1.Value;
            // string postImage = "abc";

            if (FileUpload1.HasFile)
            {
                try
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.FileName);
                    string uploadDirectory = Server.MapPath("~/Avatars/");
                    string filePath = Path.Combine(uploadDirectory, fileName);
                    FileUpload1.SaveAs(filePath);
                    postImage = "~/Avatars/" + fileName;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "UploadFailed", "alert('Upload failed.');", true);
                    return;
                }
            }

            if (isImageUploaded) // Check if an image has been uploaded
            {
                postImage = uploadedImageFileName; // Use the uploaded image file name
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE PostTable SET post_image1 = @PostImage " +
                        "WHERE post_id = @PostID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PostID", postId);

                    command.Parameters.AddWithValue("@PostImage", postImage);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                       

                        if (FileUpload1.HasFile)
                        {
                            try
                            {
                                string fileNames = Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.FileName);
                                string filePaths = Server.MapPath("~/Avatars/") + fileNames;
                                FileUpload1.SaveAs(filePaths);
                                isImageUploaded = true; // Set the flag to true
                                                        //uploadedImageFileName = fileNames;
                                                        //Image2.ImageUrl = "~/Avatars/" + fileNames;


                                string imageUrl = postImage;
                                Image2.ImageUrl = ResolveUrl(imageUrl);
                            }
                            catch (Exception ex)
                            {
                               
                            }
                        }
                        //Image2.ImageUrl = ResolveUrl(postImage);
                    }
                    else
                    {
                        // Failed to insert the post
                        ScriptManager.RegisterStartupScript(this, GetType(), "UploadFailed", "alert('Upload failed.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }



        //
        private void LoadPostContent(string postId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT post_content FROM PostTable WHERE post_id = @PostID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PostID", postId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string postContent = reader["post_content"].ToString();
                            TextArea1.Value = postContent;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void LoadPostImage(string postId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT post_image1 FROM PostTable WHERE post_id = @PostID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PostID", postId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string postImage = reader["post_image1"].ToString();
                            Image2.ImageUrl = ResolveUrl(postImage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}