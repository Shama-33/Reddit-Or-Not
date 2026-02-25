<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="CommunityPageMember.aspx.cs" Inherits="WebApplication1.CommunityPageMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">
     <style>
       /***/
        .community-container {
            position: relative; /* Establishes the container as a positioning context */
            margin-bottom:20px;
        }
        
        .community-imagebig {
            height: 150px;
            width: 90%;
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
        .manage-buttondlt {
            padding: 10px 20px;
            background-color: #a10000;
            color: white;
            font-weight: bold;
            text-decoration: none;
            border-radius: 5px;
        }
        .private-message {
   font-weight: bold;
   color: red;
}

        .display
        {

        }


         .post-item {
            border: 0.8px dotted #bdbdbd;
            border-radius:5px;
            padding: 10px;
            margin-bottom: 10px;
            transition: background-color 0.3s;
            width: 800px;
            margin: 0 auto 10px; /* Center align the item templates */
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

        .blank-row {
            height: 20px;
        }

        .label {
            display: block;
            margin-bottom: 5px;
        }

        .red-heading {
            color: red;
        }

        .centered {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .box {
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }

        .form-group {
            margin-bottom: 15px;
            background-color: lightgray;
        }

        .full-width {
            width: 100%;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">
    <div class="community-container">
    <asp:Image ID="imgCommunity" runat="server" CssClass="community-imagebig"  />
         <div class="community-details">
    <asp:Label ID="lblCommunityName" runat="server" Text="Label"></asp:Label>
    <br />
     <asp:Button ID="btnjoin" runat="server" Text="Join" CssClass="manage-button" OnClick="btnJoin_Click" />
              <asp:Button ID="btnDelete" runat="server" Text="Cancel Request" CssClass="manage-buttondlt" OnClick="btnDelete_Click" />
        </div>
        </div>
     <div class="display" >
        <asp:Label ID="ComTopic" runat="server" CssClass="private-message" Text=""></asp:Label>

    </div>

    <div class="display" >
        <asp:Label ID="lblPrivate" runat="server" CssClass="private-message" Text=""></asp:Label>

    </div>
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
    <div>
        <asp:Repeater ID="Repeater1Res" runat="server">
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

                       
                    </div>
                </ItemTemplate>
            </asp:Repeater>
    </div>

   
    
</asp:Content>