using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.Login
{
    public class Login_Request
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }


}
