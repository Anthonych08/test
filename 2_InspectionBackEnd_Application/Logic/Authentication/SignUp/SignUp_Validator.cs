using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using _2_InspectionBackEnd_Application.Interfaces;
using System.Text.RegularExpressions;
using _2_InspectionBackEnd_Application.Exception;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.SignUp
{
    public class SignUp_Validator : AbstractValidator<SignUp_Command>
    {
        private readonly IInspection_Datasource _inspectionDatasource;

        public SignUp_Validator(IInspection_Datasource inspection_DbContext)
        {
            _inspectionDatasource = inspection_DbContext;
            RuleFor(r => r.Email).NotEmpty().NotNull();
            RuleFor(r => r.Password).NotEmpty().NotNull().MinimumLength(8);
            RuleFor(r => r.ConfirmPassword).NotEmpty().NotNull().MinimumLength(8);
            RuleFor(r => r).Custom((data, context) => ValidateEmail(data, context));
            RuleFor(r => r).Custom((data, context) => ValidateConfirmPassword(data, context));
        }
        private void ValidateEmail(SignUp_Command data, ValidationContext<SignUp_Command> context)
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

            //2. Validate if email is used
            var existEmail = _inspectionDatasource.MasterUsers.Where(w => w.EMAIL == data.Email).FirstOrDefault();
            if (existEmail != null)
            {
                throw new Validation_Exception("Email Has Already Been Registered, Please Try Another Email.");
            }
        }
        private void ValidateConfirmPassword(SignUp_Command data, ValidationContext<SignUp_Command> context)
        {
            //1. Validate if confirm password is the same as password
            if (data.Password != null && data.ConfirmPassword != null && !data.Password.Equals(data.ConfirmPassword))
            {
                context.AddFailure($"Confirm Password is not same as Password.");
            };
        }
    }
}