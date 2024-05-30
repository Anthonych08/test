using FluentValidation;
using FluentValidation.Results;

namespace _2_InspectionBackEnd_Application.Exception
{
    public class Validation_Exception : ValidationException
    {
        public Validation_Exception(string message) : base(message)
        {
        }

        public Validation_Exception(ValidationResult validationResult) : base(validationResult.Errors)
        {
        }

        public Validation_Exception(string message, ValidationResult validationResult) : base(message, validationResult.Errors)
        {
        }
    }
}
