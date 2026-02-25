using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Check if the remember me cookie exists
                if (Request.Cookies["Username"] != null)
                {
                    // Retrieve the username from the cookie
                    string username = Request.Cookies["Username"].Value;

                    // Set the session variable with the username
                    Session["Username"] = username;

                    // Redirect to the desired page
                    Response.Redirect("homepage_webform.aspx");
                }
            }


            var loginLink = Master.FindControl("LinkButton1") as LinkButton;
            // var signupLink = Master.FindControl("LinkButton2") as LinkButton;

            // Disable the login 
            if (loginLink != null)
            {
                loginLink.Visible = false;
                //loginLink.CssClass = "disabled-link";
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserLogin();
            //Response.Redirect("WebForm1.aspx?username=" + TextBox1.Text);
        }

        void UserLogin()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UserInfo WHERE username = @username AND password = @password", conn);
                    cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());

                    int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (rowCount == 1)
                    {
                        string username = GetUsername(TextBox1.Text.Trim());
                        Session["Username"] = username;



                        // Check if the "Remember Me" checkbox is checked
                        if (rememberLogin.Checked)
                        {
                            // Create a new cookie to store the username
                            HttpCookie cookie = new HttpCookie("Username", TextBox1.Text);

                            // Set the cookie expiration date (e.g., 7 days from now)
                            cookie.Expires = DateTime.Now.AddDays(7);

                            // Add the cookie to the response
                            Response.Cookies.Add(cookie);
                        }

                        // Set the session variable with the username
                        Session["Username"] = TextBox1.Text;

                        



                        string script = "alert('Login Successful!');";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "LoginAlert", script, true);

                        // Response.Redirect("WebForm1.aspx");
                        Response.Redirect("homepage_webform.aspx");
                    }
                    else
                    {
                        string script = "alert('Invalid Credentials!');";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "LoginAlert", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
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
    }
}
