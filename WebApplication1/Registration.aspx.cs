using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            
             var signupLink = Master.FindControl("LinkButton2") as LinkButton;


            if (signupLink != null)
            {
                signupLink.Visible = false;
           
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Check if any of the required fields are empty
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text))
            {
                string script = "alert('Please fill in all the required fields!');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "EmptyFieldsAlert", script, true);
                return; // Stop further execution
            }

            if (CheckUniqueEmail())
            {
                if (CheckPasswordsMatch())
                {
                    UserReg();
                   // Response.Redirect("WebForm1.aspx?username=" + TextBox1.Text);
                }
                else
                {
                    string script = "alert('Passwords do not match!');";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PasswordMatchAlert", script, true);
                }
            }
            else
            {
                string script = "alert('You already have an account');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "EmailExistsAlert", script, true);
                return;
            }
            
        }

        void UserReg()
        {
            try
            {
                string defaultImageUrl = "Images/default_avatar.jpg";

                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    /*
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserInfo (email, username, password) VALUES (@email, @username, @password)", conn);
                    cmd.Parameters.AddWithValue("@email", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@username", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text);*/

                    SqlCommand cmd = new SqlCommand("INSERT INTO UserInfo (email, username, password, image) VALUES (@email, @username, @password, @image)", conn);
                    cmd.Parameters.AddWithValue("@email", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@username", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@image", defaultImageUrl);

                    cmd.ExecuteNonQuery();
                }

                string script = "alert('Sign Up Successful!');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SignUpAlert", script, true);

                string username = GetUsername(TextBox1.Text.Trim());
                Session["Username"] = username;

                //Response.Redirect("WebForm1.aspx");
                Response.Redirect("Profile_Image_Add.aspx");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool CheckUniqueEmail()
        {
            try
            {
  

                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UserInfo WHERE email=@email", conn);
                    cmd.Parameters.AddWithValue("@email", TextBox3.Text);

                    int emailCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (emailCount > 0)
                    {
                        return false; // Email already exists
                    }
                    else
                    {
                        return true; // Email does not exist
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        bool CheckPasswordsMatch()
        {
            return TextBox2.Text == TextBox4.Text;
        }


        string GetUsername(string inputUsername)
        {
            string username = string.Empty;
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT username FROM UserInfo WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", inputUsername);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    username = reader["username"].ToString();
                }
            }
            return username;
        }


        [WebMethod]
        public static bool CheckUsernameAvailability(string username)
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UserInfo WHERE username=@username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                   // string script = "alert('Username Already Taken');";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "EmptyFieldsAlert", script, true);
                    return false; // Username already exists
                }
                else
                {
                    return true; // Username is available
                }
            }
        }
    }
}
