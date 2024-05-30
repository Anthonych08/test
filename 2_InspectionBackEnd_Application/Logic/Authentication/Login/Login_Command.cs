using _0_InspectionBackEnd_Shared.GeneralFunctions;
using _0_InspectionBackEnd_Shared.Responses;
using _1_InspectionBackEnd_Domain.Master;
using _2_InspectionBackEnd_Application.Exception;
using _2_InspectionBackEnd_Application.Extensions;
using _2_InspectionBackEnd_Application.Interfaces;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using System.Net.Mail;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.Login
{
    public class Login_Command : Login_Request, IRequest<ResponseBuilder<Login_Response>>
    {

    }

    public class Handler : IRequestHandler<Login_Command, ResponseBuilder<Login_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        private readonly IJwt _jwt;
        public Handler(
            IInspection_Datasource inspectionDatasource,
            IJwt jwt
            )
        {
            _inspectionDatasource = inspectionDatasource;
            _jwt = jwt;
        }

        public async Task<ResponseBuilder<Login_Response>> Handle(Login_Command request, CancellationToken cancellationToken)
        {
            var data = _inspectionDatasource.MasterUsers.Select(w => w.EMAIL).ToList();

            if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
            {
                var loggedInUser = _inspectionDatasource.MasterUsers.Where(w => w.EMAIL == request.Email).FirstOrDefault();
                if (loggedInUser == null)
                {
                    throw new Validation_Exception("Account not found, Please Sign Up or Try Other Type of Login.");
                }
                else if (loggedInUser != null && loggedInUser.LOGIN_TYPE != "Database")
                {
                    throw new Validation_Exception("Different Type of Login. Please Use Another Type of Login!");
                }
                else
                {
                    var inputPassword = PasswordOperation.HashPassword(request.Password);

                    if (!inputPassword.Equals(loggedInUser.PASSWORD))
                    {
                        throw new Validation_Exception("Wrong Password");
                    }
                    else
                    {
                        var roleName = _inspectionDatasource.MasterRoles.Where(w => w.ROLE_ID == loggedInUser.ROLE_ID).FirstOrDefault();
                        var token = new Login_Response
                        {
                            Email = request.Email,
                            Token = _jwt.GenerateToken(loggedInUser, roleName.ROLE_NAME)
                        };
                        return token.ResponseRead();
                    }

                }
            }
            else
            {
                throw new Validation_Exception("Fill The Required Input");
            }
        }
    }
}
