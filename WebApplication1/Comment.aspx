<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="WebApplication1.WebForm12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">

    <style>
        .full-width_div {
            width: 800px;
            box-sizing: border-box;
            padding: 10px;
            margin-bottom: 10px;
             margin: 0 auto 10px; 
        }
    
        .post-item {
            border: 0.8px dotted #bdbdbd;
            padding: 10px;
            margin-bottom: 10px;
            transition: background-color 0.3s;
            width: 800px;
            margin: 0 auto 10px; /* Center align the item templates */
            background-color:white;
            
        }

            .post-item:hover {
                border-style:solid;
                border-color: lightgray;
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
            border-color:lightblue;
            border-style:solid;
            border-style:dotted;
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
        }

        .full-width {
            width: 100%;
        }

        body {
            background-color: #f1f1f1;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">
    <section>    

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
               <h4> <%# Eval("post_content") %></h4>
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

         <div>
                <asp:Button ID="Btncmnt" runat="server" Text="Manage Comments" OnClick="cmntButton_Click" CssClass="btn btn-secondary" />
            </div>

        <div>
            <h3>Leave a Comment</h3>
            <div class="full-width_div">
                <asp:TextBox ID="commentTextBox" runat="server" TextMode="MultiLine" Rows="5" CssClass="full-width"></asp:TextBox>
            </div>
            
            <div>
                <asp:Button ID="uploadButton" runat="server" Text="Upload" OnClick="uploadButton_Click" CssClass="btn btn-secondary" />
            </div>
        </div>

        <div>
          <asp:Repeater ID="CommentRepeater" runat="server">
           <ItemTemplate>
        <div class="post-item">
            <div class="community-info">
               <asp:Image ID="userImage" runat="server" CssClass="community-image" ImageUrl='<%# GetImageUrl(Eval("image")) %>' />

                 
                 <h5><%# Eval("commenter") %></h5>
                <h5>&nbsp &nbsp</h5>
                <div class="commented-on">
                     <%# Eval("date") %>
                </div>
            </div>
            <div class="post-content">
               <h4> <%# Eval("comment_content") %></h4>
            </div>
            <br />

            <asp:HiddenField ID="commentIdHiddenField" runat="server" Value='<%# Eval("comment_id") %>' />

         
             </div>
    </ItemTemplate>
              </asp:Repeater>
    </div>
        

        </section>
</asp:Content>
