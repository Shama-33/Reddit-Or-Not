<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="WebApplication1.post" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>


    <style>
    .card {
        border: 1px solid #ccc;
        border-radius: 5px;
        padding: 20px;
        width: 600px;
        margin: 10px auto;
    }

    .center-image {
        display: block;
        margin: 0 auto;
        max-width: 200px;
        border:0px;
        border-radius:0;
    }
     .thumbnail-image {
            max-width: 50px;
            max-height: 50px;
            border: 0px;
            border-radius: 5px;
        }

        .hidden {
            display: none;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card">
        <div class="card-body">
            <div class="text-center">
                <asp:Image ID="Image1" runat="server" CssClass="thumbnail-image" ImageUrl="~/Images/generaluser.png" />
            </div>
            <br />
            
            <div class="form-group">
                <textarea id="TextArea1" runat="server" class="form-control" rows="8" placeholder="Reddit Post"></textarea>
            </div>
            <div><h4><p>Want to add an Image?</p></h4></div>
            <div class="text-center">
                <asp:Image ID="Image2" runat="server" CssClass="center-image" ImageUrl="~/Images/default_avatar.jpg" />
            </div>
            <br />
           
            <div class="form-group">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
           
            <br />
            <div class="text-center">
                <asp:Button ID="Button2" runat="server" Text="Post" CssClass="btn btn-success" OnClick="Button2_Click" />
            </div>
            <br />
        </div>
    </div>
</asp:Content>




