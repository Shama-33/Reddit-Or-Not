using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MimeKit;
using MailKit.Net.Smtp;



namespace WebApplication1
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        string storedCode;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SendMailButton_Click(object sender, EventArgs e)
        {
            string enteredUsername = TextBox1.Text;
            string enteredEmail = verifyMail.Text;

            // Perform database query to check if the entered username and email match
            bool isMatch = CheckUsernameAndEmail(enteredUsername, enteredEmail);

            if (isMatch)
            {
                // Generate a 6-digit verification code
                Random random = new Random();
                int verificationCode = random.Next(100000, 999999);
                storedCode = verificationCode.ToString();
                Session["VerificationCode"] = storedCode;

                // Send the verification code to the entered email
                SendVerificationCode(enteredEmail, verificationCode);

                // Display the success message
                sentText.Text = "Verification code sent";
                sentText.Visible = true;
            }
            else
            {
                // Display the error message
                sentText.Text = "Invalid username or email";
                sentText.Visible = true;
            }
        }

        private bool CheckUsernameAndEmail(string Username, string Email)
        {
           
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string query = "SELECT COUNT(*) FROM UserInfo WHERE username = @Username AND email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Email", Email);

                    
                    int matchingUserCount = (int)command.ExecuteScalar();

                    // Return true if there is a match, false otherwise
                    return matchingUserCount > 0;
                }
            }
        }

        /*
        private void SendVerificationCode(string email, int code)
        {

 
            // Configure the email settings
            
            string senderEmail = ConfigurationManager.AppSettings["SenderEmail"];
            string senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            // Compose the email message
            MailMessage mailMessage = new MailMessage(senderEmail, email);
            mailMessage.Subject = "Reddit Verification Code";
            mailMessage.Body = $"Your verification code is: {code}";

            // Send the email
            smtpClient.Send(mailMessage);
        }*/

        private void SendVerificationCode(string email, int code)
        {
            // Configure the email settings
            string senderEmail = ConfigurationManager.AppSettings["SenderEmail"];
            string senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

            // Create a new MimeMessage
            MimeMessage message = new MimeMessage();

            // Set the sender and recipient addresses
            message.From.Add(new MailboxAddress("", senderEmail));
            message.To.Add(new MailboxAddress("", email));

            // Set the subject and body of the email
            message.Subject = "Reddit Verification Code";
            message.Body = new TextPart("plain")
            {
                Text = $"Your verification code is: {code}"
            };

            // Configure the SMTP client
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                // Connect to the SMTP server
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with the SMTP server
                client.Authenticate(senderEmail, senderPassword);

                // Send the email
                client.Send(message);

                // Disconnect from the SMTP server
                client.Disconnect(true);
            }
        }

        protected void VerifyButton_Click(object sender, EventArgs e)
        {
            string enteredCode = verifyCode.Text;
            string storedCode1 = Session["VerificationCode"] as string;

            // Retrieve the verification code and other relevant data from session or database for validation

            // Compare the entered code with the stored code
            if (enteredCode == storedCode1)
            {
                // Verification code matched, allow the user to reset the password
                ResPass1.Visible = true;
                ResPass2.Visible = true;
                ResetPassButton.Visible = true;
                codeVerifyText.Visible = false;
            }
            else
            {
                // Verification code did not match, display error message and clear the fields
                codeVerifyText.Text = "Invalid verification code";
                codeVerifyText.Visible = true;
                TextBox1.Text = "";
                verifyMail.Text = "";
                verifyCode.Text = "";
            }
        }

        protected void ResetPassButton_Click(object sender, EventArgs e)
        {
            string enteredUsername = TextBox1.Text;
            string enteredPassword = ResPass1.Text;
            string enteredPassword2 = ResPass2.Text;
            if(enteredPassword!=enteredPassword2)
            {
                resetText.Text = "Passwords do not match";
                return;
            }

            // Update the password in the database for the entered username
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Prepare the SQL query
                string query = "UPDATE UserInfo SET password = @NewPassword WHERE username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", enteredPassword);
                    command.Parameters.AddWithValue("@Username", enteredUsername);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }

            // Display the success message
            resetText.Text = "Password reset successfully";
            resetText.Visible = true;
        }

    }
}