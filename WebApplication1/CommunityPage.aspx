<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="CommunityPage.aspx.cs" Inherits="WebApplication1.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">
    <style>


/*
        .post-container {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }
        
        .post-image {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
            object-position: center;
            margin-right: 10px;
        }
        
        .post-details {
            flex-grow: 1;
        }
        
        .post-title {
            font-weight: bold;
            margin-bottom: 5px;
        }
        
        .post-info {
            font-size: 14px;
            color: gray;
        }
        
        .post-content {
            margin-top: 10px;
        }
        
        .post-image-content {
            width: 100%;
            height: auto;
            margin-top: 10px;
        }
        
        .post-comment {
            margin-top: 10px;
        }
        
        .comment-button {
            background-color: #337ab7;
            color: white;
            padding: 5px 10px;
            border-radius: 5px;
            font-weight: bold;
        }
        
        .comment-count {
            margin-left: 5px;
            color: gray;
        }
        
        .post-vote {
            width: 50px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            text-align: center;
        }
        
        .vote-button {
            background-color: #337ab7;
            color: white;
            padding: 5px 10px;
            border-radius: 5px;
            font-weight: bold;
        }
        
        .vote-count {
            margin-top: 5px;
            color: gray;
        }
    */


.post_container {
  margin-top: 20px; /* Adjust as needed */
}

        
.post-item {
    border: 0.8px dotted #bdbdbd;
    border-radius:5px;
    padding: 10px;
    margin-bottom: 10px;
    transition: background-color 0.3s;
    width: 800px;
    margin: 0 auto 10px; 
    background-color: white;
}

    .post-item:hover {
        border-style: solid;
        border-color: black;
        box-shadow: 2px;
    }

.community-info {
    display: flex;
    align-items: center;
}

.community-image {
    width: 37px;
    height: 37px;
    border-radius: 100%;
    margin-right: 10px;
    object-fit: contain;
    object-position: center;
    border-width: 0.8px;
    border-color: black;
    border-style: solid;
}

.posted-by {
    font-size: 14px;
    color: gray;
    font-weight: bold;
}

.post-content {
    margin-top: 10px;
}



.post-image img {
    width: 70%;
    height: auto;
    display: block;
    max-width: 100%;
    margin: 10px auto;
    object-fit: contain;
    object-position: center;
    border-width: 0.6px;
    border-color: lightblue;
    border-style: solid;
    border-style: dotted;
}




        /***/
        .community-container {
            position: relative; /* Establishes the container as a positioning context */
            background-color:white;
        }
        
        .community-imagemain {
            height: 150px;
            width: 100%;
            object-fit: cover;
            object-position: center;
            max-height: 100%;
        }
        
        .community-details {
            position: absolute; /* Positioning the details over the image */
            top: 70%; /* Positions the details vertically at 50% from the top */
            left: 90%; /* Positions the details horizontally at 50% from the left */
            transform: translate(-50%, -50%); /* Centers the details */
            text-align: center; /* Centers the text horizontally */
            color: #337ab7;
            font-weight:bold;
        }
        
        .manage-button {
            padding: 10px 20px;
            background-color: #337ab7;
            color: white;
            font-weight: bold;
            text-decoration: none;
            border-radius: 5px;
        }



        
        .proim {
            display: flex;
            flex-direction: row;
            border-radius: 100%;
            height: 49px;
            width: 49px;
            border: 0;
        }

        .post-bar {
            background-color: rgb(248, 247, 248);
            border-radius: 5px;
            border-width: 0.8px;
            border-color: rgb(255,255,255);
            border-style: solid;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-stretch: 100%;
            font-style: normal;
            height: 40px;
            margin-left: 20px;
            margin-right: 10px;
            padding-left: 10px;
            width: 550px;
            border-width: 0.8px;
                border-color: lightgrey;
        }

            .post-bar:hover {
                border-width: 0.8px;
                border-color: blue;
            }

        .click-to-post {
            padding-right: 24px;
            background-color: white;
            width: 100%;
            max-width: 800px;
            margin-right: auto;
            margin-top: 20px;
            height: 60px;
            align-items: center;
            position: relative;
            padding-left: 5px;
            display: flex;
            flex-direction: row;
            border: 1px;
            border-style: solid;
            border-color: rgb(190, 190, 190);
            border-radius: 5px;
            cursor: pointer;
        }

        .click-icon {
            display: flex;
            flex-direction: row;
            height: 42px;
            width: 42px;
            border: 0;
            margin-left: 5px;
            cursor: pointer;
        }

        /* Adjust the styles for different screen sizes using media queries */
        @media (max-width: 768px) {
            .click-to-post {
                width: 90%;
                max-width: 90%;
                padding-right: 12px;
                margin-left:5px;
            }

            .post-bar {
                width: 90%;
            }
        }
         @media (min-width: 769px) {
            .click-to-post {
                
                margin-left:190px;
            }

            .post-bar {
              
            }
        }

         body
         {
             background-color:lightgrey;
         }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">
    <div class="community-container">
        <asp:Image ID="imgCommunity" runat="server" CssClass="community-imagemain" />
        <div class="community-details">
            <asp:Label ID="lblCommunityName" runat="server" Text="Label" CssClass="community-name"></asp:Label>
            <br />
            <asp:Button ID="btnManageSubreddit" runat="server" Text="Manage Subreddit" CssClass="manage-button" OnClick="btnManageSubreddit_Click" />
        </div>
         </div>

    <div class="navbar navbar-expand-lg navbar-light bg-light">
    <ul class="navbar-nav mr-auto">
       
        <li class="nav-item">
            <asp:Button ID="btnOption2" runat="server" Text="Your Posts" CssClass="nav-link" OnClick="btnOption2_Click" />
        </li>
         <li class="nav-item">
            <asp:Button ID="ButtonOption3" runat="server" Text="Popular" CssClass="nav-link" OnClick="btnOption3_Click" />
        </li>
         <li class="nav-item">
            <asp:Button ID="ButtonOption4" runat="server" Text="ALL" CssClass="nav-link" OnClick="btnOption4_Click" />
        </li>
       
    </ul>
</div>

        
         
        <div id="click_to_post" class="click-to-post" >
            <asp:Image ID="UserImage" runat="server" CssClass="proim" />
            <input class="post-bar" type="text" placeholder="Create Post">
            <img src="images/image-files.png" class="click-icon" alt="Image Files">
            <img src="images/attached-file.png" class="click-icon" alt="Attached File">
        </div>
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
                               <asp:Button ID="shareButton" runat="server" CssClass="btn btn-warning square-button comment-button" Text="Share" OnClick="shareButton_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater> 

           </div>
        </div>
            <br />
            <br />

            
    
           

    
   
     <script>
         $(document).ready(function () {
             $("#click_to_post").click(function () {
                 // Retrieve the community name from the label
                 var communityName = $("#<%= lblCommunityName.ClientID %>").text();
                // URL-encode the community name
                var encodedCommunityName = encodeURIComponent(communityName);
                // Redirect to the new page with the communityName query parameter
                window.location.href = "post_com.aspx?communityName=" + encodedCommunityName;
            });
        });
     </script>
</asp:Content>
