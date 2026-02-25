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
    public partial class Site2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call the GetUsername method to fetch the username from the session
                string username = GetUsername();
                if (!string.IsNullOrEmpty(username))
                {
                    //usernameHiddenField.Value = username;
                    string script = $@"<script type='text/javascript'>
                          document.getElementById('usernamePlaceholder').textContent = '{username}';
                      </script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SetUsername", script);
                }


            }

            // Call the GetUsername method to fetch the username from the session
            string usernameSpan = GetUsername();
            if (!string.IsNullOrEmpty(usernameSpan))
            {
                var usernameSpanControl = new LiteralControl
                {
                    Text = $"<span class='username'>{usernameSpan}</span>"
                };

                // Add the username span after the dropdown menu
                var navbar = FindControl("navbarSupportedContent") as Control;
                if (navbar != null)
                {
                    navbar.Controls.Add(usernameSpanControl);
                }
            }


        }

        protected string GetUsername()
        {
            return Session["Username"] as string;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "8") // Log Out option selected
            {
                // Clear session and cookie
                Session.Clear();
                Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);

                // Redirect to the login page
                Response.Redirect("Login.aspx");
            }
            else if (DropDownList1.SelectedValue == "7")
            {
                Response.Redirect("About_Reddit.aspx");
            }
            else if (DropDownList1.SelectedValue == "6")
            {
                Response.Redirect("YourCommunities.aspx");
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                Response.Redirect("CreateCommunity.aspx");
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                Response.Redirect("Profile.aspx");
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                Response.Redirect("ALLP.aspx");
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                Response.Redirect("popularall.aspx");
            }
            else if (DropDownList1.SelectedValue == "1")
            {
                Response.Redirect("homepage_webform.aspx");
            }
            else
            {

            }
        }







    }
}
