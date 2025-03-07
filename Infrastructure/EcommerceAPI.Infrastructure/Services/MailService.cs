using EcommerceAPI.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendEmailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendEmailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:UserName"], _configuration["Mail:MailSender"], System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
            smtp.Port = int.Parse(_configuration["Mail:Port"]);
            smtp.Host = _configuration["Mail:Host"];
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetEmailAsync(string to, string userId, string resetToken)
        {
            var clientUrl = _configuration["ClientUrl"];
            StringBuilder resetMail = new();
            resetMail.AppendLine("Hello,<br> We received a request to reset your password. Click the link below to set a new one:<br>");
            resetMail.AppendLine($@"<strong><a target=""_blank"" href=""{clientUrl}/update-password/{userId}/{resetToken}"">Reset Password</a></strong><br><br>");
            resetMail.AppendLine("If you didn't request this, you can ignore this message. The link will expire in 12 hours.");
            await SendEmailAsync(to, "Password Reset Request", resetMail.ToString());

        }
    }
}
