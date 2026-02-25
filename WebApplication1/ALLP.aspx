<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="ALLP.aspx.cs" Inherits="WebApplication1.ALLP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">




    <title>News Grid</title>
    <link href="CSS/postrepeater.css" rel="stylesheet" />
    <style>
        body{
            background-color:lightgrey;
        }
        /* CSS for the news grid */
        .news-container {
            display: flex;
            overflow-x: auto;
             scrollbar-width: none; /* For Firefox */
            -ms-overflow-style: none; /* For Internet Explorer and Edge */
            scrollbar-color: transparent; /* For Chrome, Safari, and Opera */
        }
        .news-container::-webkit-scrollbar {
            display: none; /* For Chrome, Safari, and Opera */
        }
        .news-box {
            flex: 0 0 auto;
            width: 300px; 
            margin: 10px;
            padding: 10px;
            border: 1px solid #ccc;
            background-color:lightsteelblue;
            border-radius:5%;
            color:white;
        }
        .news-box h3 {
            margin-top: 0;
        }

         .news-box .image-container {
            width: 100%;
            height: 170px; 
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">

     <asp:Panel ID="newsContainer" CssClass="news-container" runat="server">
        <!-- News boxes will be dynamically added here -->
    </asp:Panel>
    <br />
     <div class="post_container">
           <div> 
               <asp:Repeater ID="postRepeater" runat="server">
                <ItemTemplate>
                    <div class="post-item">
                        <div class="community-info">
                            <asp:Image ID="communityImage" runat="server" CssClass="community-image" ImageUrl='<%# string.IsNullOrEmpty(Eval("Community_name") as string) ? "Images/hacker.png" : Eval("CommunityImage") %>' />


                            <h5><%# string.IsNullOrEmpty(Eval("Community_name") as string) ? "Wall Post": Eval("Community_name") %></h5>
                            <h5>&nbsp &nbsp</h5>
                            <div class="posted-by">
                                Posted by <%# Eval("username") %> on <%# Eval("date") %>
                            </div>
                        </div>
                        <div class="post-content">
                            <h4><%# Eval("post_content") %></h4>
                        </div>
                        <br />
                        <div class="post-image">
                            <asp:Image ID="postImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("post_image1")) %>' />

                        </div>

                        <asp:HiddenField ID="postIdHiddenField" runat="server" Value='<%# Eval("post_id") %>' />

                        <div class="voting-section">
                            <asp:Button ID="upvoteButton" runat="server" CssClass="btn btn-success rounded-pill upvote-button" Text="Upvote" OnClick="upvoteButton_Click" />
                            <asp:Label ID="upvoteCountLabel" runat="server" CssClass="vote-count upvote-count" Text='<%# Eval("upvotes") %>'></asp:Label>
                            <asp:Button ID="downvoteButton" runat="server" CssClass="btn btn-danger rounded-pill downvote-button" Text="Downvote" OnClick="downvoteButton_Click" />
                            <asp:Label ID="downvoteCountLabel" runat="server" CssClass="vote-count downvote-count" Text='<%# Eval("downvotes") %>'></asp:Label>
                            <asp:Button ID="commentButton" runat="server" CssClass="btn btn-secondary square-button comment-button" Text="Comment" OnClick="commentButton_Click" />
                            <asp:Label ID="commentCountLabel" runat="server" CssClass="comment-count" Text='<%# Eval("no_comments") %>'></asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater> 

           </div>
        </div>
            <br />
            <br />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
  



    <script>
        $(document).ready(function () {
            // API request to fetch news articles
            var apiUrl = "https://newsapi.org/v2/top-headlines?sources=bbc-news&apiKey=ca8221b7553645f8a1622ed4a441ab08";
            $.get(apiUrl, function (data) {
                // Process the API response and populate the news grid
                var articles = data.articles;
                var newsContainer = $("#<%= newsContainer.ClientID %>"); // Update this line

            $.each(articles, function (index, article) {
                var newsBox = $("<div>", { class: "news-box" });
                var title = $("<h3>").text(article.title);
                var description = $("<p>").text(article.description);
                var imageContainer = $("<div>", { class: "image-container" });
                imageContainer.css("background-image", "url(" + article.urlToImage + ")");

                newsBox.append(title, description);
                newsContainer.append(newsBox);
            });
        });
    });
    </script>

</asp:Content>
