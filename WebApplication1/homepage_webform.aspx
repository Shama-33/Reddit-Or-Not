<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="homepage_webform.aspx.cs" Inherits="WebApplication1.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">

    <%--CSS; bootstrap,datatables,fontawesome--%>
    <link href="Bootstrap/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="DataTables/CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="fontawesome/css/all.css" rel="stylesheet" />

    <%--Javascript--%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Include jQuery -->

    <script defer src="Bootstrap/Javascript/bootstrap.bundle.min.js"></script>

    <script defer src="fontawesome\js\all.js" crossorigin="anonymous"></script>

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
     width: 100%;
   max-width: 650px;
    margin: 0 ; 
    margin-bottom:10px;
    background-color: white;
    cursor:pointer;
}

    .post-item:hover {
        border-width: 1px;
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
            width: 400px;
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
            max-width: 650px;
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
              margin-left: 5px;
          }

          .post-bar {
              width: 90%;
          }

          .post_container {
              margin-left: 5px;
               width: 90%;
              max-width: 90%;
          }

          .absolute-image {
              position: absolute;
              top: 50px;
              right: 30px;
              width: 200px;
              visibility: hidden;
          }
          .absolute-button {
    visibility: hidden;
    /* Add any necessary styling for the absolute buttons */
}
      }
         @media (min-width: 769px) {
            .click-to-post {
                
                margin-left:55px;
            }

            .post-bar {
              
            }
            .post_container
            {
                  margin-left:55px;
            }
             .absolute-image {
             position: absolute;
                  top: 100px;
                    right: 60px;
                 width: 370px;
                 height:auto;
                 object-fit:contain;
                 object-position:center;
        }
             .absolute-button {
    position: absolute;
    /* Add any necessary styling for the absolute buttons */
}

         body
         {
             background-color:lightgray;
         }

        
}

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">
    
    <div>
        <div id="click_to_post" class="click-to-post">
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

          <div>
        <img src="Images/personal_home.png" class="absolute-image" alt="Absolute Image">

              <asp:LinkButton ID="LinkButton1" runat="server"  PostBackUrl="~/CreateCommunity.aspx" CssClass="btn btn-primary absolute-button" OnClientClick="createCommunity()" Style="top: 310px; right: 60px; width: 370px; border-radius: 10px;">Create Community</asp:LinkButton>

              <asp:LinkButton ID="LinkButton2" runat="server"  PostBackUrl="~/SearchPage.aspx" CssClass="btn btn-success absolute-button" OnClientClick="joinCommunity()" Style="top: 360px; right: 60px; width: 370px; border-radius: 10px;">Join Community</asp:LinkButton>
    </div>

    </div>

    <script>
        // Wait for the document to be ready
        $(document).ready(function () {
            // Attach a click event handler to the click_to_post element
            $("#click_to_post").click(function () {
                // Redirect to the new page
                window.location.href = "post.aspx";
            });
        });


       
    </script>
      
</asp:Content>
