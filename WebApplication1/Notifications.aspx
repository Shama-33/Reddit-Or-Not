<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="WebApplication1.Notifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .notification {
    margin-bottom: 10px;
     border: 1px solid #ccc;
     margin-top:10px;
     padding:10px;
     margin-left:200px;
     margin-right:200px;
}

.notification-link {
    color: #337ab7;
    text-decoration: none;
}

.commenter {
    font-weight: bold;
}

.date {
    color: #999;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="font-weight:bold;color:darkblue;margin: 20px auto 20px;">Notifications</h2>
    <asp:Repeater ID="notificationRepeater" runat="server">
    <ItemTemplate>
        <div class="notification">
            <a class="notification-link" href='<%# "Comment.aspx?postId=" + Eval("post_id") %>'>
                <span class="commenter"><%# Eval("commenter") %></span> commented on your post on
                <span class="date"><%# Eval("date") %></span>
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>


</asp:Content>
