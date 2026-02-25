using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCommunities();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            SearchCommunities(searchKeyword);
        }

        private void BindCommunities()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT Community_name, CommunityImage FROM CommunityTable";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        lvCommunities.DataSource = dataTable;
                        lvCommunities.DataBind();
                    }
                }
            }
        }

        private void SearchCommunities(string searchKeyword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT Community_name, CommunityImage FROM CommunityTable WHERE Community_name LIKE @Keyword OR Description LIKE @Keyword";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + searchKeyword + "%");
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            lvCommunities.DataSource = dataTable;
                            lvCommunities.DataBind();
                            ltNoResults.Visible = false;
                        }
                        else
                        {
                            lvCommunities.DataSource = null;
                            lvCommunities.DataBind();
                            ltNoResults.Visible = true;
                        }
                    }
                }
            }
        }
    }
}
