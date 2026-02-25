<%@ Page Title="" Language="C#" MasterPageFile="~/NestedHome.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication1.WebForm11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderchildhead" runat="server">
    <%-- CSS; bootstrap, datatables, fontawesome --%>
    <link href="Bootstrap/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="DataTables/CSS/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="fontawesome/css/all.css" rel="stylesheet" />

    <%-- Javascript --%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Include jQuery -->
    <script defer src="Bootstrap/Javascript/bootstrap.bundle.min.js"></script>
    <script defer src="fontawesome\js\all.js" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bodymovin/5.7.7/lottie.min.js"></script>
    <style>
        .post-item {
            border: 0.8px dotted #bdbdbd;
            padding: 10px;
            margin-bottom: 10px;
            transition: background-color 0.3s;
            width: 800px;
            margin: 0 auto 10px; /* Center align the item templates */
            background-color: white;
        }

            .post-item:hover {
                border-style: solid;
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
            border-width: 0.8px;
            border-color: black;
            border-style: solid;
        }

        .posted-by {
            font-size: 14px;
            color: gray;
            font-weight: bold;
        }

        .post-content {
            margin-top: 10px;
        }



        .post-image img {
            width: 70%;
            height: auto;
            display: block;
            max-width: 100%;
            margin: 10px auto;
            object-fit: contain;
            object-position: center;
            border-width: 0.6px;
            border-color: lightblue;
            border-style: solid;
            border-style: dotted;
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
            background-color: lightgray;
        }

        .full-width {
            width: 100%;
        }

        body {
            background-color: lightgrey;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderchildbody" runat="server">
    <section>
        <div class="page-holder d-flex align-items-center">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <h3 class="centered">Your Profile</h3>
                        <div class="form-group box">
                            <label for="txtUsername" class="label">Username:</label>
                            <asp:Label ID="lblUsername" runat="server" CssClass="form-control full-width"></asp:Label>
                        </div>
                        <div class="form-group box">
                            <label for="txtEmail" class="label">Email:</label>
                            <asp:Label ID="lblEmail" runat="server" CssClass="form-control full-width"></asp:Label>
                        </div>
                        <h3 class="centered">Change Password</h3>
                        <div class="form-group box">
                            <label class="label">Old Password:</label>
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control full-width" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group box">
                            <label class="label">New Password:</label>
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control full-width" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group box">
                            <label class="label">Confirm New Password:</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control full-width" TextMode="Password"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-primary centered" OnClick="btnConfirm_Click" />


                        <asp:Label ID="lblerror" runat="server" CssClass="red-heading centered"></asp:Label>
                        <br />
                        <a href="ForgetPassword.aspx">Forgot Password? click here</a>


                        <hr />
                        <h4 class="centered">Your Currrent Image</h4>

                        <div class="centered">
                            <asp:Image ID="currentImage" runat="server" CssClass="" ImageUrl="Images/default_avatar.jpg" Height="150px" Width="150px" />
                        </div>
                        <br />
                        <h3 class="centered">Change Your Avatar?</h3>
                        <div class="form-group box">
                            <asp:FileUpload ID="fileAvatar" runat="server" CssClass="form-control-file" />
                        </div>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary centered" OnClick="UploadAvatarNew" />
                        <hr />
                        <h3 class="red-heading centered">Delete Your Account?</h3>
                        <div class="form-group box">
                            <label class="label4">Enter Password:</label>
                            <asp:TextBox ID="DeletePass" runat="server" CssClass="form-control full-width" TextMode="Password"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete" CssClass="btn btn-danger centered" OnClick="DeleteACC"/>
                        <asp:Label ID="Lbldelete" runat="server" CssClass="red-heading centered"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <br />
            <br />
            <h3 class="centered">Your Posts</h3>

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
                            <h4><%# Eval("post_content") %></h4>
                        </div>
                        <br />
                        <div class="post-image">
                            <asp:Image ID="postImage" runat="server" ImageUrl='<%# GetImageUrl(Eval("post_image1")) %>' />

                        </div>

                        <asp:HiddenField ID="postIdHiddenField" runat="server" Value='<%# Eval("post_id") %>' />

                        <div class="voting-section">
                            <asp:Button ID="upvoteButton" runat="server" CssClass="btn btn-success rounded-pill upvote-button" Text="Upvote" OnClick="upvoteButton_Click" />
                            <asp:Label ID="upvoteCountLabel" runat="server" CssClass="vote-count upvote-count" Text='<%# Eval("upvotes") %>'></asp:Label>
                            <asp:Button ID="downvoteButton" runat="server" CssClass="btn btn-danger rounded-pill downvote-button" Text="Downvote" OnClick="downvoteButton_Click" />
                            <asp:Label ID="downvoteCountLabel" runat="server" CssClass="vote-count downvote-count" Text='<%# Eval("downvotes") %>'></asp:Label>
                            <asp:Button ID="commentButton" runat="server" CssClass="btn btn-secondary square-button comment-button" Text="Comment" OnClick="commentButton_Click" />
                            <asp:Label ID="commentCountLabel" runat="server" CssClass="comment-count" Text='<%# Eval("no_comments") %>'></asp:Label>
                            <asp:Button ID="EditButton" runat="server" Text="Edit" CssClass="btn btn-warning rounded-pill downvote-button" OnClick="EditButton_Click" CommandArgument='<%# Eval("post_id") %>' />

                              <asp:LinkButton ID="removeButton" runat="server" CssClass="btn btn-danger rounded-pill downvote-button"
                Text="Remove" CommandName="Delete" CommandArgument='<%# Eval("post_id") %>' OnCommand="postRepeater_ItemCommand" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
    </section>
</asp:Content>
