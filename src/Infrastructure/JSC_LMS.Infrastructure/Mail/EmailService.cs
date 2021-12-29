using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JSC_LMS.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }

        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;

        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending failed");

            return false;
        }
        public bool SendSmtpEmail(string fromEmail, string toEmail, string password, string subject, string body, string host, string port)
        {
            //var fromMail = new MailAddress(fromEmail);
            //var toMail = new MailAddress(toEmail);
            //var emailPassowrd = password;
            //bool result = false;
            //using (MailMessage mm = new MailMessage(fromMail, toMail))
            //{
            //    mm.Subject = subject;
            //    mm.Body = body;
            //    mm.IsBodyHtml = true;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Host = host;
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = true;  // for gmail it will false
            //    NetworkCredential NetworkCred = new NetworkCredential(Convert.ToString(fromMail), emailPassowrd);
            //    smtp.Credentials = NetworkCred;
            //    smtp.Port = Convert.ToInt32(port);
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtp.Send(mm);
            //    result = true;
            //}
            //return result;

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("ashish.verma.neo01@gmail.com");
            message.To.Add(new MailAddress(toEmail));
            message.Subject = "Test";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("ashish.verma.neo01@gmail.com", "Ashish12!@");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            return true;
        }
    }
}
