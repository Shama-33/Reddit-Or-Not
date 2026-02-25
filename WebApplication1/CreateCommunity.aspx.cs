using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class CreateCommunity : System.Web.UI.Page
    {
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            if (Session["Username"] == null)
            {
                // Redirect to the login page
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                // Retrieve the username from the session and display it
                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();
                    lblUsername.Text = "Admin : " + username;
                }

                
            }
        }

        protected void btnCreateCommunity_Click(object sender, EventArgs e)
        {
            // Get the values from the form controls
            string communityName = txtCommunityName.Text;
            string adminName = Session["Username"].ToString();
            // string description = ddlDescription.SelectedValue;
             string description = txtCommunityType.Text;
            string visibility = ddlVisibility.SelectedValue;
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            // Upload the community image file
            string imagePath = "Images/default_avatar.jpg";
            if (fuCommunityImage.HasFile)
            {
                try
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fuCommunityImage.FileName);
                    string uploadDirectory = Server.MapPath("~/Avatars/");
                    string filePath = Path.Combine(uploadDirectory, fileName);
                    fuCommunityImage.SaveAs(filePath);
                    imagePath = "~/Avatars/" + fileName;
                }
                catch (Exception ex)
                {
                    
                }
            }

            // Check if the community name is unique
            

            if (!IsCommunityNameUnique(communityName))
            {
                // Display error message for duplicate community name
                lblCommunityNameTaken.Text = "Community name is already taken";
                lblCommunityNameTaken.Visible = true;
                return;
            }

           
            if (InsertCommunity(communityName, adminName, description, date, imagePath, visibility))
            {
               
                Response.Redirect("~/CommunityPage.aspx?communityName=" + Server.UrlEncode(communityName));

            }
            else
            {
              
            }
        }

        private bool IsCommunityNameUnique(string communityName)
        {
           
            string query = "SELECT COUNT(*) FROM CommunityTable WHERE Community_name = @Community_name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count == 0;
                }
            }
        }

        private bool InsertCommunity(string communityName, string adminName, string description, string date, string imagePath, string visibility)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "INSERT INTO CommunityTable (Community_name, AdminName, Description, Date, CommunityImage, CommunityVisibilty) " +
                           "VALUES (@Community_name, @AdminName, @Description, @Date, @CommunityImage, @CommunityVisibility)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Community_name", communityName);
                    command.Parameters.AddWithValue("@AdminName", adminName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@CommunityImage", imagePath);
                    command.Parameters.AddWithValue("@CommunityVisibility", visibility);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("No rows affected. Community creation failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        lblCommunityNameTaken.Text = "An error occurred while creating the community: " + ex.Message;
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


    }
}
