using _0_InspectionBackEnd_Shared.Responses;
using _0_InspectionBackEnd_Shared.Constants;
using _2_InspectionBackEnd_Application.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using FluentValidation;
using _0_InspectionBackEnd_Shared.Constants;
using _0_InspectionBackEnd_Shared.Extensions;

namespace _4_InspectionBackEnd_Api.Extension
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _env;

        public ExceptionFilter(
            IWebHostEnvironment env)
        {
            _env = env;
        }

        public override void OnException(ExceptionContext context)
        {

            var type = context.Exception.GetType();

            // ValidationException fluent valiadtion
            if (type == typeof(ValidationException))
            {
                var exception = context.Exception as ValidationException;

                var validationError = new ResponseBuilder<List<string>>
                {
                    Error = new Error
                    {
                        IsError = true,
                        //ErrorId = _log.ErrorId(),
                        ErrorType = Error_Type.Validation.ToDescription(),
                        ErrorMessages = exception.Errors.Select(s => s.ErrorMessage).ToList()
                    }
                };

                context.Result = new BadRequestObjectResult(validationError);

                context.ExceptionHandled = true;
                return;
            }
            // ValidationException fluent validation non Validator
            if (type == typeof(Validation_Exception))
            {
                var exception = context.Exception as Validation_Exception;

                var validationError = new ResponseBuilder<List<string>>
                {
                    Error = new Error
                    {
                        IsError = true,
                        //ErrorId = _log.ErrorId(),
                        ErrorType = Error_Type.Validation.ToDescription(),
                        ErrorMessages = new List<string>() { exception.Message }
                    }
                };

                context.Result = new BadRequestObjectResult(validationError);

                context.ExceptionHandled = true;
                return;
            }

            // UnauthorizedAccessException
            if (type == typeof(UnauthorizedAccessException))
            {
                var exception = context.Exception as UnauthorizedAccessException;

                var unauthorizedAccessException = new ResponseBuilder<List<string>>
                {
                    Error = new Error
                    {
                        IsError = true,
                        //ErrorId = _log.ErrorId(),
                        ErrorType = Error_Type.UnauthorizedAccess.ToDescription(),
                        ErrorMessages = new List<string> { exception.Message },
                    }
                };

                if (_env.EnvironmentName.ToLower().Contains("dev"))
                {
                    unauthorizedAccessException.Error.ErrorMessages.Add(context.Exception.StackTrace);
                }


                context.Result = new UnauthorizedObjectResult(unauthorizedAccessException);

                context.ExceptionHandled = true;
                return;
            }



            // untuk error yang belum di handle
            var unknownError = new ResponseBuilder<List<string>>
            {
                Error = new Error
                {
                    IsError = true,
                    ErrorType = Error_Type.Unknown.ToDescription(),
                    ErrorMessages = new List<string> { "Internal Server Error" }
                }
            };

            unknownError.Error.ErrorMessages.Add(context.Exception.Message);


            if (context.Exception.InnerException != null)
            {
                unknownError.Error.ErrorMessages.Add("InnerException ==> ");
                unknownError.Error.ErrorMessages.Add(context.Exception.InnerException.Message);
                unknownError.Error.ErrorMessages.Add(context.Exception.InnerException.StackTrace);

                if (context.Exception.InnerException.InnerException != null)
                {
                    unknownError.Error.ErrorMessages.Add("InnerException ==> ");
                    unknownError.Error.ErrorMessages.Add(context.Exception.InnerException.InnerException.Message);
                    unknownError.Error.ErrorMessages.Add(context.Exception.InnerException.InnerException.StackTrace);

                    if (context.Exception.InnerException.InnerException.InnerException != null)
                    {
                        unknownError.Error.ErrorMessages.Add("InnerException ==> ");
                        unknownError.Error.ErrorMessages.Add(context.Exception.InnerException.InnerException.InnerException.Message);
                        unknownError.Error.ErrorMessages.Add(context.Exception.InnerException.InnerException.InnerException.StackTrace);
                    }
                }

            }
            unknownError.Error.ErrorMessages.Add(context.Exception.StackTrace);


            context.Result = new ObjectResult(unknownError)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;


        }

    }
}
