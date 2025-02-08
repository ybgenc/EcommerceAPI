﻿namespace EcommerceAPI.Application.DTOs.User
{
    public class CreateUser
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AgreeTerms { get; set; }
    }
}
