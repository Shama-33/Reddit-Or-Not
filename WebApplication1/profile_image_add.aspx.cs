using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Find the login and sign-up links in the master page
            var loginLink = Master.FindControl("LinkButton1") as LinkButton;
            var signupLink = Master.FindControl("LinkButton2") as LinkButton;

            // Hide the login and sign-up links
            if (loginLink != null)
                loginLink.Visible = false;
            if (signupLink != null)
                signupLink.Visible = false;

            // Get the username from the session
            string username = Session["Username"] as string;
             /*
            // Display "Hi User" with the username
            if (!string.IsNullOrEmpty(username))
            {
                var greetingLabel = new Label
                {
                    ID = "lblGreeting",
                    Text = $"Hi {username}",
                    CssClass = "nav-link",
                    EnableViewState = false
                };

                // Add the greeting label to the navbar
                var navbar = Master.FindControl("navbarSupportedContent") as Control;
                if (navbar != null)
                    navbar.Controls.Add(greetingLabel);
            }*/

            // if page loads for the first time, check
            if (!IsPostBack)
            {
                // first state
                lblPrompt.Text = "Choose your avatar";
                //lnkChangeAvatar.Text = "Upload";
            }

            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    string username1 = Session["Username"].ToString();
                    UsernameLabel_dp.Text = username1;
                }
                else
                {
                    Response.Redirect("Registration.aspx"); // Redirect to login/reg page if not logged in
                }
            }
        }

        protected void lnkSkip_Click(object sender, EventArgs e)
        {
            // Get the current username
            string username = Session["Username"] as string;

            // Redirect to WebForm1.aspx with the current username as a query parameter
            // Response.Redirect("WebForm1.aspx?username=" + username);
            Response.Redirect("homepage_webform.aspx");
        }

        protected void lnkChangeAvatar_Click(object sender, EventArgs e)
        {
            // Check if a file was uploaded
            if (fileUpload.HasFile)
            {
                // Save the uploaded file to a specific location
                string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                string filePath = Server.MapPath("~/Avatars/") + fileName;
                fileUpload.SaveAs(filePath);

                // Update the user's avatar URL in the database
                UpdateUserAvatarUrl(Session["Username"] as string, "~/Avatars/" + fileName);

                // Update the prompt and link button text
                lblPrompt.Text = "Your avatar";
               // lnkChangeAvatar.Text = "Change avatar";

                // Update the image source with the newly uploaded image
                imgAvatar.Src = ResolveUrl("~/Avatars/" + fileName);


            }
        }

        private void UpdateUserAvatarUrl(string username, string imageUrl)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE UserInfo SET image = @imageUrl WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@imageUrl", imageUrl);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                lblPrompt.Text = "Image not updated";
                imgAvatar.Src = ResolveUrl("~/Avatars/default_avatar.png");
                return;
            }
        }
    }
}
