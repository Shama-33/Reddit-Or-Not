using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class YourCommunities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
            {
                string username = Session["Username"] as string;
                if (string.IsNullOrEmpty(username))
                {
                    Response.Redirect("Login.aspx");
                }

            }

        }








        protected void btnOption2_Click(object sender, EventArgs e)
        {
            Repeaternormal.Visible= false;
            communityRepeater.Visible = true;
            string username = Session["Username"] as string;
            if (!string.IsNullOrEmpty(username))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "SELECT Community_name FROM CommunityTable WHERE AdminName = @Username";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    communityRepeater.DataSource = dt;
                    communityRepeater.DataBind();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnOption3_Click(object sender, EventArgs e)
        {
            Repeaternormal.Visible = true;
            communityRepeater.Visible = false;
            string username = Session["Username"] as string;
            if (!string.IsNullOrEmpty(username))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "SELECT CommunityName FROM MembershipTable WHERE username = @Username";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Repeaternormal.DataSource = dt;
                    Repeaternormal.DataBind();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

    }
}