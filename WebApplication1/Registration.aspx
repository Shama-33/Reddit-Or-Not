<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebApplication1.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        $("#<%= TextBox1.ClientID %>").keyup(function () {
            var username = $(this).val().trim();
            if (username !== "") {
                $.ajax({
                    type: "POST",
                    url: "Registration.aspx/CheckUsernameAvailability",
                    data: JSON.stringify({ username: username }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            $("#usernameMessage").text("");
                        } else {
                            $("#usernameMessage").text("Username is already taken");
                        }
                    },
                    failure: function (response) {
                        console.log(response.d);
                    }
                });
            }
        });
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card" style="margin-top:10px">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="Images/generaluser.png" width="100 px"/>
                                </center>
                            </div>
                        </div>

                         <div class="row">
                            <div class="col">
                                <center>
                                    <h2 style="color:navy;font-weight:bold;margin-top:15px">Sign Up</h2>
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
                                  <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" style="border-radius:20px; margin-bottom:10px; "></asp:TextBox>

                              </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                   
                              <div class="form-group">
                                  <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Username" style="border-radius:20px; margin-bottom:10px"></asp:TextBox>
                                  <span id="usernameMessage" style="color: red;"></span>


                              </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                               
                              <div class="form-group">
                                  <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"
                                      style="border-radius:20px; margin-bottom:10px"></asp:TextBox>

                              </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                               
                              <div class="form-group">
                                  <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Confirm Password" TextMode="Password"
                                      style="border-radius:20px; margin-bottom:10px"></asp:TextBox>

                              </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                               
                              <div class="form-group">
                                  <asp:Button ID="Button1" runat="server" Text="Signup" class="btn btn-primary w-100 btn-lg"
                                      Style="border-radius: 20px; margin-top: 10px; margin-bottom: 10px" OnClick="Button1_Click" />

                              </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                               <label style="margin-bottom:10px; margin-top:20px;font-weight:bold;">Already have an account?</label>
                              <div class="form-group">
                                  <asp:Button ID="Button2" runat="server" Text="Login" class="btn btn-success w-100 btn-lg"
                                      Style="border-radius: 20px; margin-bottom: 10px" OnClick="Button2_Click" />

                              </div>
                            </div>
                        </div>


                    </div>

                </div>

                <a href="Login.aspx" style="margin-top:10px"> >>Back </a>

            </div>

        </div>

    </div>
</asp:Content>
