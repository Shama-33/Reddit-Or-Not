<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="WebApplication1.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- Include CSS and Bootstrap --%>
    <link href="Bootstrap/CSS/bootstrap.min.css" rel="stylesheet" />
    <style>
        .community-item {
            display: inline-block;
            margin: 10px;
            text-align: center;
            border: 1px solid #ccc;
            padding: 10px;
            transition: background-color 0.3s ease;
        }

        .community-item:hover {
            background-color: #f5f5f5;
            cursor: pointer;
        }

        .community-image {
            width: 10px;
            height: 10px;
            border-radius: 100%;
            overflow: hidden;
        }

       
         .no-results {
            color: red;
        }
         .form-control
         {
             width: 70%;
             margin: 20px auto 20px;
         }
         .btncls
         {
              margin: 20px auto 20px;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Search Communities</h1>
            <div class="input-group mb-3">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter search keyword"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btncls" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="no-results">
                    <asp:Literal ID="Literal1" runat="server" Visible="false" Text="No match found."></asp:Literal>
                </div>
            <asp:ListView ID="lvCommunities" runat="server" ItemPlaceholderID="itemPlaceholder">
                <LayoutTemplate>
                    <ul class="list-group">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="list-group-item">
                        <h4><%# Eval("Community_name") %></h4>
                         <a href='<%# "CommunityPageMember.aspx?communityName=" + Server.UrlEncode(Eval("Community_name").ToString()) %>'>View Community</a>
                    </li>
                </ItemTemplate>
            </asp:ListView>
             <div class="no-results">
            <asp:Literal ID="ltNoResults" runat="server" Visible="false">No matches found.</asp:Literal>
                 </div>
        </div>
    </form>
    <%-- Include JavaScript and Bootstrap --%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script defer src="Bootstrap/Javascript/bootstrap.bundle.min.js"></script>
    <script>
        // Add custom JavaScript code here
    </script>
</body>
</html>
