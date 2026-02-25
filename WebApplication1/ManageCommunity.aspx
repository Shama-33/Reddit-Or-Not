<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ManageCommunity.aspx.cs" Inherits="WebApplication1.ManageCommunity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/CreateCommunity.css" rel="stylesheet" />


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

    .even-row {
        background-color: #f9f9f9; /* Specify the background color for even rows */
    }

    .odd-row {
        background-color: #eef1f4; /* Specify the background color for odd rows */
    }

    .membership-id {
        display: block; /* Set display to block for better alignment */
    }

    .username {
        display: block; /* Set display to block for better alignment */
        margin-top: 5px; /* Add some margin to separate the two spans */
    }

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="page-holder d-flex align-items-center">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <h3 class="centered">Your Community</h3>
                        <div class="form-group box">
                            <label for="txtCommunityname" class="label">Community Name:</label>
                            <asp:Label ID="lblCommunityname" runat="server" CssClass="form-control full-width"></asp:Label>
                        </div>
                        <div class="form-group box">
                            <label for="txtType" class="label">Community Type:</label>
                            <asp:Label ID="lbltype" runat="server" CssClass="form-control full-width"></asp:Label>
                        </div>
                        <div class="form-group box">
                            <label for="txtprivacy" class="label">Privacy Level :</label>
                            <asp:Label ID="Labelprivacy" runat="server" CssClass="form-control full-width"></asp:Label>
                        </div>
                        <br />
                        <h3 class="centered">Change Community Details?</h3>
                        <div class="visibility-dropdown class1">
                            <label for="ddlVisibility">Visibility:</label>
                            <asp:DropDownList ID="ddlVisibility" runat="server" CssClass="class2">
                                <asp:ListItem Text="Public" Value="Public"></asp:ListItem>
                                <asp:ListItem Text="Restricted" Value="Restricted"></asp:ListItem>
                                <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
                            </asp:DropDownList>


                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm Change" CssClass="btn btn-primary centered" OnClick="btnConfirm_Click" />


                            <asp:Label ID="lblerror" runat="server" CssClass="red-heading centered"></asp:Label>
                            <br />


                            <hr />
                            <h4 class="centered">Your Currrent Image</h4>

                            <div class="centered">
                                <asp:Image ID="currentImage" runat="server" CssClass="" ImageUrl="Images/default_avatar.jpg" Height="150px" Width="150px" />
                            </div>
                            <br />
                            <h3 class="centered">Change Your Community Image?</h3>
                            <div class="form-group box">
                                <asp:FileUpload ID="fileAvatar" runat="server" CssClass="form-control-file" />
                            </div>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary centered" OnClick="UploadAvatarNew" />
                            <hr />
                            <h3 class="red-heading centered">Delete Your Community?</h3>
                            <div class="form-group box">
                                <label class="label4">Type "CONFIRM" to delete:</label>
                                <asp:TextBox ID="Deletecom" runat="server" CssClass="form-control full-width" ></asp:TextBox>
                            </div>
                            <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete" CssClass="btn btn-danger centered" OnClick="DeleteCOM" />
                            <asp:Label ID="Lbldelete" runat="server" CssClass="red-heading centered"></asp:Label>
                        </div>
                    </div>
            </div>
        </div>
             </div>
        
            <br />
            <br />
            <h3 class="centered">Members , Requests and Community Posts</h3>

            <div>
                <div class="navbar navbar-expand-lg navbar-light bg-light">
    <ul class="navbar-nav mr-auto">
        <li class="nav-item">
            <asp:Button ID="btnOption1" runat="server" Text="Membership Requests" CssClass="nav-link" OnClick="btnOption1_Click" />
        </li>
        <li class="nav-item">
            <asp:Button ID="btnOption2" runat="server" Text="Members" CssClass="nav-link" OnClick="btnOption2_Click" />
        </li>
        <li class="nav-item">
            <asp:Button ID="btnOption3" runat="server" Text="Community Posts" CssClass="nav-link" OnClick="btnOption3_Click" />
        </li>
    </ul>
</div>

            </div>

        <div class="text-center my-4">
    <h4> <asp:Label ID="lblRequests" runat="server"></asp:Label></h4>


<asp:Repeater ID="repeaterMembers" runat="server">
    <ItemTemplate>
        <div class="text-center my-3 <%# Container.ItemIndex % 2 == 0 ? "even-row" : "odd-row" %>">
            <span class="membership-id">Membership ID: <%# Eval("MembershipID") %></span>
            <span class="username">Username: <%# Eval("username") %></span>
            <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("MembershipID") %>' CssClass="btn btn-success mx-2" OnCommand="btnApprove_Command" />
            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("MembershipID") %>' CssClass="btn btn-danger mx-2" OnCommand="btnRemove_Command"/>
        </div>
    </ItemTemplate>
</asp:Repeater>





</div>

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
                           
                           <asp:LinkButton ID="removeButton" runat="server" CssClass="btn btn-danger rounded-pill downvote-button"
                Text="Remove" CommandName="Delete" CommandArgument='<%# Eval("post_id") %>' OnCommand="postRepeater_ItemCommand" />
                           
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

           
    </section>
</asp:Content>
