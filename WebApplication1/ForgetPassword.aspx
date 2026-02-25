<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="WebApplication1.WebForm10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/bodymovin/5.7.7/lottie.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="home ">

        <div class="page-holder d-flex align-items-center">
            <div class="container">
                <div class="row align-items-center py-5">
                    <div class="col-5 col-lg-7 mx-auto mb-5 mb-lg-0">

                        <div class="pr-lg-5">
                            <div id="lottieContainer" class="img-fluid"></div>

                        </div>

                    </div>



                    <div class="col-lg-5 px-lg-4">

                        <h1 class="text-base text-primary text-uppercase mb-4">Reset Password</h1>

                        <h2 class="mb-4">Enter your Username</h2>



                        <div class="form-group mb-4">
                            <asp:TextBox CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Username" runat="server" ID="TextBox1" />

                        </div>
                        <h2 class="mb-4">Enter your email</h2>



                        <div class="form-group mb-4">
                            <asp:TextBox CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Your Email Address" TextMode="Email" runat="server" ID="verifyMail" />

                        </div>
                       



                        <asp:Button Text="Send Code" CssClass="btn btn-secondary" runat="server" ID="SendMailButton" OnClick="SendMailButton_Click" />

                        <br />
                        <br />
                        <asp:Label ID="sentText" runat="server" Text="Verification code sent" Visible="false"></asp:Label>
                        <br />


                        <div class="form-group mb-4">
                            <asp:TextBox CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Verification Code" TextMode="Number" runat="server" ID="verifyCode" />

                        </div>

                        <asp:Button Text="Verify" CssClass="btn btn-success" runat="server" ID="VerifyButton" OnClick="VerifyButton_Click"/>

                        <asp:Label ID="codeVerifyText" runat="server" Text="Password reset successfully" Visible="false"></asp:Label>

                        <div class="form-group mb-4">
                            <asp:TextBox CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Enter your new password" TextMode="Password" runat="server" ID="ResPass1" Visible="false" />

                        </div>

                        <div class="form-group mb-4">
                            <asp:TextBox CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Confirm your new password" TextMode="Password" runat="server" ID="ResPass2" Visible="false" />

                        </div>

                        <asp:Button Text="Reset Password" CssClass="btn btn-1" runat="server" ID="ResetPassButton" Visible="false" OnClick="ResetPassButton_Click" />


                        <asp:Label ID="resetText" runat="server" Text="Password reset successfully" Visible="false"></asp:Label>

                    </div>



                </div>

            </div>
        </div>


    </section>

    <script>
        var animation = bodymovin.loadAnimation({
            container: document.getElementById('lottieContainer'),
            renderer: 'svg',
            loop: true,
            autoplay: true,
            path: 'Lottiefile/reddit.json' 
        });
    </script>
</asp:Content>
