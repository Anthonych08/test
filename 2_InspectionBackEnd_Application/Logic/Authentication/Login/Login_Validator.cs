using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using _2_InspectionBackEnd_Application.Interfaces;
using System.Text.RegularExpressions;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.Login
{
    public class Login_Validator : AbstractValidator<Login_Command>
    {
        private readonly IInspection_Datasource _inspectionDatasource;

        public Login_Validator(IInspection_Datasource inspection_DbContext)
        {
            _inspectionDatasource = inspection_DbContext;
            RuleFor(r => r.Email).NotEmpty().NotNull();
            RuleFor(r => r.Password).NotEmpty().NotNull().MinimumLength(8);
            RuleFor(r => r).Custom((data, context) => ValidateEmail(data, context));
        }
        private void ValidateEmail(Login_Command data, ValidationContext<Login_Command> context)
        {
            //1. Validate if email is valid
            if (data.Email != null)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(data.Email);
                if (match.Success == false)
                {
                    context.AddFailure($"Email is Not Valid, Please Enter Another Email.");
                }
            };
        }
    }
}