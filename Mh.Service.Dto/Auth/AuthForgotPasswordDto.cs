using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.Dto.Auth
{
    public class AuthForgotPasswordDto
    {
        public string Email { get; set; }
        public Enums.Languages LanguageId { get; set; }
    }
}