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
    public partial class PostDetails : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the post ID or shareable link from the query string
                string postId = Request.QueryString["postId"]; // Replace "postId" with the actual query string parameter name
                if (string.IsNullOrEmpty(postId) )
                {
                    errorlbl.Text = "Post Not Found";
                    return;

                }
                // Load the post details
                DataTable postTable = GetPostData(postId);
                foreach (DataRow row in postTable.Rows)
                {
                    if (row["Community_name"] == DBNull.Value)
                    {
                        //row["Community_name"] = "Wall Post";
                        errorlbl.Text = "Not a Community Post . Unable to share.";
                    }

                    if (row["CommunityImage"] == DBNull.Value)
                    {
                        //row["CommunityImage"] = "images/default_avatar.jpg";
                        errorlbl.Text = "Not a Community Post . Unable to share.";
                    }
                }
                postRepeater.DataSource = postTable;
                postRepeater.DataBind();
            }
        }

        private DataTable GetPostData(string post_Id)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
                /*string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1 " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";*/
                string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " +
                              "WHERE post_id=@POST_ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@POST_ID", post_Id);
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
               // imageUrl = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs="; // Base64 representation of a 1x1 transparent GIF image
            }

            return imageUrl;
        }


    }
}