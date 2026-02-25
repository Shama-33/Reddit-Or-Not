<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="About_Reddit.aspx.cs" Inherits="WebApplication1.About_Reddit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/bodymovin/5.7.7/lottie.min.js"></script>

    <link href="CSS/AboutReddit.css" rel="stylesheet" />


   

    <style>


        <style>
        .enhanceImage {
       
        cursor:pointer;
    }
    .enhanceImage:hover {
        filter: brightness(120%);
        transition: filter 0.3s ease;
        cursor:pointer;
    }



        /**/

        .contact {
  background-color: #f7f7f7;
  padding: 60px 0;
}

.contact .section-title {
  text-align: center;
  margin-bottom: 40px;
}

.contact .section-title h2 {
  color: black;
}

.contact .section-title span {
  color: darkblue;
}

.contact-info-item {
  margin-bottom: 30px;
}

.contact-info-item img {
  width: 40px;
  margin-bottom: 10px;
}

.contact-info-item h4 {
  color: black;
  font-size: 18px;
  margin-bottom: 10px;
}

.contact-info-item p {
  color: black;
  font-size: 16px;
}

.contact-form .form-group {
  margin-bottom: 20px;
}

.contact-form input,
.contact-form textarea {
  border: 1px solid #ccc;
  padding: 12px;
  width: 100%;
  border-radius: 4px;
  font-size: 16px;
}

.contact-form .btn {
  background-color: #3a983b;
  color: #fff;
  border: none;
  padding: 12px 20px;
  font-size: 16px;
  cursor: pointer;
}

.contact-form .btn:hover {
  background-color: #ff5252;
}

.contact-form #SentConfirmationText {
  color: #008000;
  margin-top: 10px;
  display: block;
}




         body {
      /*background-color: #b1b1b1; */
      background-color: white; 
    }

         .carousel-item {
             margin-top:15px;
             width:100%;
    height: 100vh; /* Set height to 100% of viewport height */
  }

         .carousel-item img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      margin: 0 auto;
     
    }

        #lottieContainer {
      width: 50%;
      height: auto;
        margin: 0 auto;
      
    }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container">
  <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
    <ol class="carousel-indicators">
      <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
      <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
      <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
    </ol>
    <div class="carousel-inner">
      <div class="carousel-item active">
        <img class="d-block w-100 img-fluid" src="Images/c1.jpg" alt="Connect with People">
        <div class="carousel-caption d-none d-md-block">
          <h5 style="color:black;font-weight:bold;">Connect With People</h5>
        </div>
      </div>
      <div class="carousel-item">
        <img class="d-block w-100 img-fluid" src="Images/c3.jpg" alt="Share your thoughts">
        <div class="carousel-caption d-none d-md-block">
          <h5 style="color:black;font-weight:bold;">Share Your Thoughts</h5>
        </div>
      </div>
      <div class="carousel-item">
        <img class="d-block w-100 img-fluid" src="Images/c2.jpg" alt="Maintain Anonymous Identity">
        <div class="carousel-caption d-none d-md-block">
          <h5 style="color:black;font-weight:bold;">Maintain Anonymous Identity</h5>
        </div>
      </div>
    </div>
    <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
      <span class="carousel-control-prev-icon" aria-hidden="true"></span>
      <span class="sr-only">Previous</span>
    </a>
    <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
      <span class="carousel-control-next-icon" aria-hidden="true"></span>
      <span class="sr-only">Next</span>
    </a>
  </div>
</div>
     <br />
    <br />

    <div class="container">
    <div class="row">
        <div class="col-lg-4">
            <img src="Images/share.jpg" class="img-fluid enhanceImage" alt="Image" id="enhanceImage1">
        </div>
        <div class="col-lg-8">
            <p style="font-family:Arial, Helvetica, sans-serif; color:grey;font-size:16px; display: flex;
    align-items: center;
    height: 100%;">
                Reddit (/ˈrɛdɪt/) is an American social news aggregation, content rating, and discussion website.
                Registered users (commonly referred to as "Redditors") submit content to the site such as links,
                text posts, images, and videos, which are then voted up or down by other members.
            </p>
        </div>
    </div>
</div>


    

    <div class="container">
    <div class="row">
       
        <div class="col-lg-8">
            <p style="font-family:Arial, Helvetica, sans-serif; color:grey;font-size:16px; display: flex;
    align-items: center;
    height: 100%;">
                 Posts are organized by subject into
        user-created boards called "communities" or "subreddits". Submissions with more upvotes appear towards the top of their subreddit and, if they receive enough 
        upvotes, ultimately on the site's front page.
              
            </p>
        </div>
         <div class="col-lg-4">
            <img src="Images/anonymouscat.jpg" class="img-fluid enhanceImage" alt="Image" id="enhanceImage3">
        </div>
    </div>
</div>
    
   
     <div class="container">
    <div class="row">
        <div class="col-lg-4">
            <img src="Images/connectcat.jpg" class="img-fluid enhanceImage" alt="Image" id="enhanceImage2">
        </div>
        <div class="col-lg-8">
            <p style="font-family:Arial, Helvetica, sans-serif; color:grey;font-size:16px; display: flex;
    align-items: center;
    height: 100%;">
                Reddit administrators moderate the communities. 
        Moderation is also conducted by community-specific moderators, who are not Reddit employees.[6]<br />
            </p>
        </div>
    </div>
</div>
    
    
    
    
    <br />
    <br />


     <div id="lottieContainer"></div>
  
  <script>
      var animation = bodymovin.loadAnimation({
          container: document.getElementById('lottieContainer'),
          renderer: 'svg',
          loop: true,
          autoplay: true,
          path: 'Lottiefile/reddit.json' 
      });
  </script>



    <!--contact-->

    <section class="contact section-padding" id="contact" style="background-color:grey;">
        <div class=" container">
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="section-title">
                        <h2>Contact <span>us</span></h2>
                    </div>     
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-5">
                    <div class="contact-info">
                        <h3>For any queries and support </h3>
                       
                        <div class="contact-info-item">
                          
                            <h4>Write to us</h4>
                            <p>abc@gmail.com</p>

                        </div>
                        <div class="contact-info-item">
                           
                            <h4>Call us on</h4>
                            <p>+880 1 000 000 000</p>

                        </div>

                    </div>
                </div>
                <div class="col-lg-8 col-md-7">
                    <div class="contact-form">
                        
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Your Name" BorderStyle="Outset" CssClass="form-control" TextMode="SingleLine"></asp:TextBox> 
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBox2" runat="server" placeholder="Your Email" BorderStyle="Outset" CssClass="form-control" TextMode="Email"></asp:TextBox> 
                                    </div>
                                </div>
                                 
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBox3" runat="server" placeholder="Your Phone" BorderStyle="Outset" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                                    </div>

                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBox4" runat="server" placeholder="Subject" BorderStyle="Outset" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="TextBox5" runat="server" placeholder="Your Message" BorderStyle="Outset" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Button ID="Button1" runat="server" Text="Send Message"  CssClass="btn btn-1" OnClick="Button1_Click" />
                                        <asp:Label Text="" runat="server" ID="SentConfirmationText" />
                                    </div>

                                </div>

                            </div>
                        

                    </div>
                </div>

            </div>
        </div>
    </section>

   

</asp:Content>
