using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MimeKit;

namespace WebApplication1
{
    public partial class About_Reddit : System.Web.UI.Page
    {
        private string mail, credential;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string from, to, pass, messageBody, subject;
            get_credential();

            var message = new MimeMessage();
            to = mail;
            from = mail;
            pass = credential;

            message.From.Add(new MailboxAddress("", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = "Message from " + TextBox1.Text + " Subject: " + TextBox4.Text;

            var bodyBuilder = new BodyBuilder();
            messageBody = "Message from user \nName: " + TextBox1.Text + "\nEmail: " + TextBox2.Text + "\nPhone:" + TextBox3.Text + "\nMessage: " + TextBox5.Text;
            bodyBuilder.TextBody = messageBody;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(from, pass);
                client.Send(message);
                client.Disconnect(true);
            }

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Thanks for your message.')", true);
        }

        protected void get_credential()
        {
            mail = ConfigurationManager.AppSettings["SenderEmail"];
            credential = ConfigurationManager.AppSettings["SenderPassword"];
        }

    }
}