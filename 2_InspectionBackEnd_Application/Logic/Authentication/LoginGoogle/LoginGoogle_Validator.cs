using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using _2_InspectionBackEnd_Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using _2_InspectionBackEnd_Application.Logic.Authentication.SignUp;
using System.Text.RegularExpressions;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.LoginGoogle
{
    public class LoginGoogle_Validator : AbstractValidator<LoginGoogle_Command>
    {
        private readonly IInspection_Datasource _inspectionDatasource;

        public LoginGoogle_Validator(IInspection_Datasource inspection_DbContext)
        {
            _inspectionDatasource = inspection_DbContext;

            RuleFor(r => r).Custom((data, context) => ValidateEmail(data, context));

        }
        private void ValidateEmail(LoginGoogle_Command data, ValidationContext<LoginGoogle_Command> context)
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