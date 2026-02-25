<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="YourCommunities.aspx.cs" Inherits="WebApplication1.YourCommunities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">
    <style>
        .button-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 50px;
            background-color: #f1f1f1;
            margin-bottom: 20px;
        }

        .button-option {
            margin: 0 10px;
            padding: 10px;
            text-decoration: none;
            color: #333;
            font-weight: bold;
            border: none;
            background: none;
            cursor: pointer;
        }

        .item-container {
            max-width: 800px;
            margin: 0 auto;
        }

        .community-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #ccc;
            padding: 10px;
        }

        .community-name {
            font-weight: bold;
        }

        .visit-link {
            text-decoration: none;
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">
    <div class="button-container">
        <asp:Button ID="btnOption2" runat="server" Text="Role: Moderator" CssClass="button-option btn-primary" OnClick="btnOption2_Click" />
        <asp:Button ID="ButtonOption3" runat="server" Text="Role: Member" CssClass="button-option btn-success" OnClick="btnOption3_Click" />
    </div>

    <div class="item-container">
        <asp:Repeater ID="communityRepeater" runat="server">
            <ItemTemplate>
                <div class="community-item">
                    <h4 class="community-name"><%# Eval("Community_name") %></h4>
                    <a href='<%# "CommunityPage.aspx?communityName=" + Eval("Community_name") %>' class="visit-link">Visit Community</a>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="Repeaternormal" runat="server">
            <ItemTemplate>
                <div class="community-item">
                    <h4 class="community-name"><%# Eval("CommunityName") %></h4>
                    <a href='<%# "CommunityPage.aspx?communityName=" + Eval("CommunityName") %>' class="visit-link">Visit Community</a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
