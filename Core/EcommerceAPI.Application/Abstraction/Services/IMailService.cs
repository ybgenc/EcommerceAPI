﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendEmailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetEmailAsync(string to, string userId, string resetToken);

    }
}
