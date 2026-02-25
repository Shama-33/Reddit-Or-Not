<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card" style="margin-top: 10px">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="Images/generaluser.png" width="150px" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3 style="color: navy; font-weight: bold; margin-top: 15px">Login</h3>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">

                                <div class="form-group">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Username" Style="border-radius: 20px; margin-bottom: 10px"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">

                                <div class="form-group">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"
                                        Style="border-radius: 20px; margin-bottom: 10px"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">

                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="LOGIN" class="btn btn-primary w-100 btn-lg"
                                        Style="border-radius: 20px; margin-top: 10px; margin-bottom: 10px" OnClick="Button1_Click" />

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <label style="margin-bottom: 10px; margin-top: 20px; font-weight: bold;">Don't have an account?</label>
                                <div class="form-group">
                                    <asp:Button ID="Button2" runat="server" Text="SIGN UP" class="btn btn-success w-100 btn-lg"
                                        Style="border-radius: 20px; margin-bottom: 10px" OnClick="Button2_Click" />

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                                <div class="form-group mb-4">
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox Text="&nbsp&nbsp&nbspRemember Me" runat="server" ID="rememberLogin" />

                                    </div>

                                </div>


                                <div class="row">
                                    <div class="col">
                                        <h3 class="h6 text-uppercase mb-0 mt-3" style="text-decoration: none; color: darkblue;"><a href="ForgetPassword.aspx">Forgot password?</a> </h3>

                                    </div>

                                </div>




                            </div>
                        </div>
                    </div>


                </div>

            </div>

            <a href="Registration.aspx" style="margin-top: 10px">>>Back </a>

        </div>

    </div>



</asp:Content>
