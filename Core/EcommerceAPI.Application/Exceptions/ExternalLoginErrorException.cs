using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Exceptions
{
    public class ExternalLoginErrorException : Exception
    {
        public ExternalLoginErrorException() : base("Google login failed. Please try again.")
        {
        }
    }
}
