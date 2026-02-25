<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PostDetails.aspx.cs" Inherits="WebApplication1.PostDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <title>Reddit Post</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:label Id="errorlbl" runat="server"></asp:label>
    
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
                <div>Upvotes : </div>
                <asp:Label ID="upvoteCountLabel" runat="server" CssClass="vote-count upvote-count" Text='<%# Eval("upvotes") %>'></asp:Label>
                <div>    Downvotes : </div>
                <asp:Label ID="downvoteCountLabel" runat="server" CssClass="vote-count downvote-count" Text='<%# Eval("downvotes") %>'></asp:Label>
                <div>    Comments : </div>
                <asp:Label ID="commentCountLabel" runat="server" CssClass="comment-count" Text='<%# Eval("no_comments") %>'></asp:Label>
         
                 
             
             </div>
               
        </div>
        </ItemTemplate>
</asp:Repeater>


  
</asp:Content>
