using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Domain.Entities;
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

            resetMail.AppendLine("<html><body style='background-color: #fef9c7; font-family: Arial, sans-serif; color: #333;'>");
            resetMail.AppendLine($"<div style='padding: 20px;'>");

            resetMail.AppendLine("<h4 style='color: rgba(128, 0, 128, 1);'>Hello,</h4>");
            resetMail.AppendLine("<p>We received a request to reset your password. Click the link below to set a new one:</p>");

            resetMail.AppendLine($"<p><a target=\"_blank\" href=\"{clientUrl}/update-password/{userId}/{resetToken}\" style='background-color: rgba(128, 0, 128, 1); color: white; padding: 10px; text-decoration: none; border-radius: 5px;'>Reset Password</a></p>");
            resetMail.AppendLine("<br>");

            resetMail.AppendLine("<p>If you didn't request this, you can ignore this message. The link will expire in 12 hours.</p>");

            resetMail.AppendLine("<br>");
            resetMail.AppendLine("<p>Best regards,</p>");
            resetMail.AppendLine("<p><strong>E-Commerce Team</strong></p>");
            resetMail.AppendLine("</div>");
            resetMail.AppendLine("</body></html>");

            await SendEmailAsync(to, "Password Reset Request", resetMail.ToString(), true);
        }


        public async Task SendOrderMailAsync(Order order)
        {
            StringBuilder orderMail = new();
            orderMail.AppendLine("<html><body style='background-color: #fef9c7; font-family: Arial, sans-serif; color: #333;'>");
            orderMail.AppendLine($"<div style='padding: 20px;'>");

            orderMail.AppendLine($"<h4 style='color: rgba(128, 0, 128, 1);'>Dear {order.Basket.User.UserName},</h4>");
            orderMail.AppendLine($"<p>Thank you for your order placed on <strong>{order.CreatedDate:MMMM dd, yyyy}</strong>.</p>");
            orderMail.AppendLine($"<p>Here are the details of your purchase:</p>");

            orderMail.AppendLine("<table style='width: 100%; border-collapse: collapse; margin-top: 20px;'>");
            orderMail.AppendLine("<thead>");
            orderMail.AppendLine("<tr style='background-color: #fef9c7; color: rgba(128, 0, 128, 1); text-align: left;'>");
            orderMail.AppendLine("<th style='padding: 12px; border: 1px solid #ddd;'>Product</th>");
            orderMail.AppendLine("<th style='padding: 12px; border: 1px solid #ddd;'>Quantity</th>");
            orderMail.AppendLine("<th style='padding: 12px; border: 1px solid #ddd;'>Price</th>");
            orderMail.AppendLine("</tr>");
            orderMail.AppendLine("</thead>");
            orderMail.AppendLine("<tbody>");

            foreach (var item in order.Basket.BasketItems)
            {
                orderMail.AppendLine($"<tr>");
                orderMail.AppendLine($"<td style='padding: 12px; border: 1px solid #ddd;'>{item.Product.Name}</td>");
                orderMail.AppendLine($"<td style='padding: 12px; border: 1px solid #ddd;'>{item.Quantity}</td>");
                orderMail.AppendLine($"<td style='padding: 12px; border: 1px solid #ddd;'>${item.Product.Price:F2}</td>");
                orderMail.AppendLine($"</tr>");
            }

            orderMail.AppendLine("</tbody>");
            orderMail.AppendLine("</table>");

            orderMail.AppendLine("<hr style='border: 1px solid #ddd; margin-top: 20px;'>");
            orderMail.AppendLine($"<p><strong>Total Price:</strong> ${order.TotalPrice:F2}</p>");
            orderMail.AppendLine($"<p><strong>Description:</strong> {order.Description}</p>");
            orderMail.AppendLine($"<p><strong>Shipping Address:</strong> {order.Address}</p>");
            orderMail.AppendLine("<p>Your order will be processed and shipped soon.</p>");
            orderMail.AppendLine("<p>If you have any questions, feel free to contact us.</p>");

            orderMail.AppendLine("<br>");
            orderMail.AppendLine("<p>Best regards,</p>");
            orderMail.AppendLine("<p><strong>E-Commerce Team</strong></p>");
            orderMail.AppendLine("</div>");
            orderMail.AppendLine("</body></html>");

            await SendEmailAsync(order.Basket.User.Email, "We have received your order.", orderMail.ToString(), true);
        }



    }
}
