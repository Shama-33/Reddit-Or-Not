<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="CreateCommunity.aspx.cs" Inherits="WebApplication1.CreateCommunity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="CSS/CreateCommunity.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top:15px;">
        <h2 style="display:flex;justify-content:center;align-content:center;color:darkblue;text-shadow:10px;">Create Community</h2>
        <hr />

        <div style="font-size:large;font-weight:bold;"><asp:Label ID="lblUsername" runat="server" Text=""></asp:Label></div>
        <br />
        <br />

        <div class="class1">
            <label for="txtCommunityName" >Community Name:</label>
            <asp:TextBox ID="txtCommunityName" runat="server" CssClass="class2"></asp:TextBox>
            <asp:Label ID="lblCommunityNameTaken" runat="server" Text="Community name taken already" Visible="false" CssClass="error-message label1"></asp:Label>

        </div>

        <div class="class1">
            <label for="txtCommunityType">Community Type:</label>
    <asp:TextBox ID="txtCommunityType" runat="server" CssClass="class2"></asp:TextBox>
        </div>

        <div class="class1">
            <label for="fuCommunityImage">Community Image:</label>
            <div class="image-preview">
                <asp:Image ID="imgCommunityImage" runat="server" CssClass="uploaded-image" Visible="false" />
            </div>
            <div class="file-upload">
                <asp:FileUpload ID="fuCommunityImage" runat="server" />
            </div>
        </div>

        <div class="visibility-dropdown class1">
            <label for="ddlVisibility">Visibility:</label>
            <asp:DropDownList ID="ddlVisibility" runat="server" CssClass="class2">
                <asp:ListItem Text="Public" Value="Public"></asp:ListItem>
                <asp:ListItem Text="Restricted" Value="Restricted"></asp:ListItem>
                <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <br />

        <asp:Button ID="btnCreateCommunity" runat="server" Text="Create" OnClick="btnCreateCommunity_Click" CssClass="create-button" />
    </div>

</asp:Content>
