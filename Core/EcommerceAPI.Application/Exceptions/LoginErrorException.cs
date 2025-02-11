using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Exceptions
{
    internal class LoginErrorException : Exception
    {
        public LoginErrorException() : base("Login failed. Please try again.")

        {
        }
    }
}
