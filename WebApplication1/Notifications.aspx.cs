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
    public partial class Notifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Session["Username"]?.ToString() ?? string.Empty; // Get the username from session
                if (string.IsNullOrEmpty(username))
                {
                    Response.Redirect("Login.aspx");
                }

                DataTable notificationTable = GetNotifications(username);

                if (notificationTable.Rows.Count > 0)
                {
                    notificationRepeater.DataSource = notificationTable;
                    notificationRepeater.DataBind();
                }
            }
        }

        private DataTable GetNotifications(string username)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
                string query = "SELECT TOP 10 CommentTable.commenter, CommentTable.date, PostTable.post_id " +
                               "FROM CommentTable " +
                               "INNER JOIN PostTable ON CommentTable.post_id = PostTable.post_id " +
                               "WHERE PostTable.username = @username " +
                               "ORDER BY CONVERT(datetime, CommentTable.date, 101) DESC";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
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
    }
}
