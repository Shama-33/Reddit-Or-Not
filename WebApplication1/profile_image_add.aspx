<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="profile_image_add.aspx.cs" Inherits="WebApplication1.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #imgAvatar {
            max-width: 100%;
            height: auto;
        }
    </style>
    <script>
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('imgAvatar').src = e.target.result;
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center mt-5">
            <div class="col-md-6 text-center">
             <br />    
                   <h5 style="font-weight:bold">Hi <asp:Label ID="UsernameLabel_dp" runat="server" /></h5>
                <br />
    
                <img id="imgAvatar" runat="server" src="Images/default_avatar.jpg" alt="Avatar Image" class="img-fluid" />
                <br />
                <asp:Label ID="lblPrompt" runat="server" Text="Choose your avatar" CssClass="mt-3"></asp:Label>
                <br />
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="mt-3" onchange="previewImage(this);" />
                <br />
                <asp:LinkButton ID="lnkChangeAvatar" runat="server" Text="Upload" OnClick="lnkChangeAvatar_Click" CssClass="mt-3 btn btn-outline-success btn-lg"></asp:LinkButton>
                <br />
                <asp:LinkButton ID="lnkSkip" runat="server" Text="Next" OnClick="lnkSkip_Click" CssClass="mt-3 btn btn-outline-info btn-lg"></asp:LinkButton>
            </div>
        </div>
    </div>


    
</asp:Content>
