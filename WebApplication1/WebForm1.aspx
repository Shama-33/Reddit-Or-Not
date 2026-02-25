<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" MasterPageFile="~/Home.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .post-item {
            border: 1px solid #ccc;
            padding: 10px;
            margin-bottom: 10px;
            transition: background-color 0.3s;
            width: 800px;
            margin: 0 auto 10px; /* Center align the item templates */
            
        }

            .post-item:hover {
                background-color: lightgray;
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
            border-width:0.8px;
            border-color:black;
            border-style:solid;
        }

        .posted-by {
            font-size: 14px;
            color: gray;
            font-weight:bold;
        }

        .post-content {
            margin-top: 10px;
        }

        

        .post-image img  {
            width: 70%;
            height: auto;
             display: block;
             max-width: 100%;
            margin: 10px auto;
            object-fit: contain;
            object-position: center;
            border-width:0.6px;
            border-color:black;
            border-style:solid;
        }

        .blank-row {
            height: 20px;
        }
    </style>
    <section>
        <div>

           <asp:Repeater ID="postRepeater" runat="server">
    <ItemTemplate>
        <div class="post-item">
            <div class="community-info">
                <asp:Image ID="communityImage" runat="server" CssClass="community-image" ImageUrl='<%# Bind("CommunityImage") %>' />
                
                <h5><%# Eval("Community_name") %></h5>
                <h5>&nbsp &nbsp</h5>
                <div class="posted-by">
                    Posted by <%# Eval("username") %> on <%# Eval("date") %>
                </div>
            </div>
            <div class="post-content">
               <h4> <%# Eval("post_content") %></h4>
            </div>
            <div class="post-image">
                 <asp:Image ID="postImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("post_image1")) %>' />

            </div>
            <br />
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

    </section>


</asp:Content>
