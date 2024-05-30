using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.LoginGoogle
{
    public class LoginGoogle_Request
    {
        public string? Email { get; set; }
        public bool? isValidEmail { get; set; }
    }


}
