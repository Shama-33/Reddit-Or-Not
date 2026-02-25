using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ALLP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchNews();

                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();



                    DataTable postTable = GetPostData(username);
                    postRepeater.DataSource = postTable;
                    postRepeater.DataBind();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void FetchNews()
        {
            try
            {
                string apiKey = "ca8221b7553645f8a1622ed4a441ab08";
                string apiUrl = "https://newsapi.org/v2/top-headlines?sources=bbc-news&apiKey=" + apiKey;

                using (WebClient client = new WebClient())
                {
                    string newsJson = client.DownloadString(apiUrl);
                    dynamic newsData = JsonConvert.DeserializeObject(newsJson);

                    if (newsData != null && newsData.articles != null)
                    {
                        foreach (var article in newsData.articles)
                        {
                            string title = article.title;
                            string description = article.description;
                            //
                            string imageUrl = article.urlToImage;

                            //

                            // Create the news box dynamically and add it to the news container
                            var newsBox = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            newsBox.Attributes["class"] = "news-box";

                            var titleLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("h3");
                            titleLabel.InnerText = title;

                            var descriptionLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                            descriptionLabel.InnerText = description;


                            //
                            var imageElement = new System.Web.UI.HtmlControls.HtmlImage();
                            imageElement.Src = imageUrl;
                            imageElement.Alt = "News Image";
                            //

                            newsBox.Controls.Add(titleLabel);
                            newsBox.Controls.Add(descriptionLabel);
                            //
                            //newsBox.Controls.Add(imageElement);
                            newsBox.Style["background-image"] = "url('" + imageUrl + "')";
                            newsBox.Style["background-position"] = "center";
                            newsBox.Style["background-repeat"] = "no-repeat";
                            newsBox.Style["background-size"] = "cover";
                            //

                            newsContainer.Controls.Add(newsBox);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                // Display an error message or log the exception
            }
        }











        //bind section

        private DataTable GetPostData(string username)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
                /* string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                                "FROM postTable " +
                                "LEFT OUTER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name "  ;*/
                /*string query = "SELECT  CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments ,postTable.post_id " +
                              "FROM postTable " +
                              "INNER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name";*/
                string query = "SELECT CommunityTable.Community_name, CommunityTable.CommunityImage, postTable.username, postTable.date, postTable.post_content, postTable.post_image1, postTable.upvotes, postTable.downvotes, postTable.no_comments, postTable.post_id, " +
                      "CONVERT(DATETIME, postTable.date, 101) AS actual_date " +
                      "FROM postTable " +
                      "LEFT OUTER JOIN CommunityTable ON postTable.CommunityName = CommunityTable.Community_name " +
                      "WHERE CommunityVisibilty = 'Public' OR postTable.CommunityName IS null " +
                      "ORDER BY actual_date DESC"; 


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                   
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
            // Handle downvote button click event
            Button downvoteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)downvoteButton.NamingContainer;
            Label downvoteCountLabel = (Label)item.FindControl("downvoteCountLabel");

            // Get the post_id and username
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;
            string username = Session["username"].ToString();
            //string username = "shama";

            // Check if the user has already upvoted
            if (!HasDownvoted(postId, username))
            {
                // Increase upvotes in postTable
                IncreaseDownvotes(postId);

                // Insert the upvote into UpvoteTable
                InsertDownvote(postId, username);
            }


            // Retrieve the updated upvotes from postTable
            int downvotes = GetDownvotes(postId);

            // Update the upvoteCountLabel
            downvoteCountLabel.Text = downvotes.ToString();
        }

        private bool HasDownvoted(string postId, string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT COUNT(*) FROM DownvoteTable WHERE post_id = @postId AND username = @username";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        private void IncreaseDownvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "UPDATE postTable SET downvotes = downvotes + 1 WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void InsertDownvote(string postId, string username)
        {
            int status = 1;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "INSERT INTO DownvoteTable (post_id, username,status) VALUES (@postId, @username,@status)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@status", status);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
            }
        }


        private int GetDownvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT downvotes FROM postTable WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    object result = cmd.ExecuteScalar();

                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }














        protected void commentButton_Click(object sender, EventArgs e)
        {
            // Handle comment button click event
            Button commentButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)commentButton.NamingContainer;

            // Get the post_id
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;

            // Redirect to comment.aspx page with the post ID as a query parameter
            Response.Redirect("Comment.aspx?postId=" + postId);
        }






        protected void upvoteButton_Click(object sender, EventArgs e)
        {
            Button upvoteButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)upvoteButton.NamingContainer;
            Label upvoteCountLabel = (Label)item.FindControl("upvoteCountLabel");

            // Get the post_id and username
            string postId = ((HiddenField)item.FindControl("postIdHiddenField")).Value;
            string username = Session["username"].ToString();
            //string username = "shama";

            // Check if the user has already upvoted
            if (!HasUpvoted(postId, username))
            {
                // Increase upvotes in postTable
                IncreaseUpvotes(postId);

                // Insert the upvote into UpvoteTable
                InsertUpvote(postId, username);
            }



            // Retrieve the updated upvotes from postTable
            int upvotes = GetUpvotes(postId);

            // Update the upvoteCountLabel
            upvoteCountLabel.Text = upvotes.ToString();
        }

        private bool HasUpvoted(string postId, string username)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "SELECT COUNT(*) FROM UpvoteTable WHERE post_id = @postId AND username = @username";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        private void IncreaseUpvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "UPDATE postTable SET upvotes = upvotes + 1 WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
            }
        }


        private void InsertUpvote(string postId, string username)
        {
            int status = 1;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "INSERT INTO UpvoteTable (post_id, username,status) VALUES (@postId, @username,@status)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@status", status);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        private int GetUpvotes(string postId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; // Replace with your actual connection string
                string query = "SELECT upvotes FROM postTable WHERE post_id = @postId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    con.Open();
                    object result = cmd.ExecuteScalar();

                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
               
                return 0;
            }
        }







    }
}