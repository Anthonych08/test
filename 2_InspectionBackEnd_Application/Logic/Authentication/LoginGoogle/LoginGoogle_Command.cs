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
using System.Net;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.LoginGoogle
{
    public class LoginGoogle_Command : LoginGoogle_Request, IRequest<ResponseBuilder<LoginGoogle_Response>>
    {

    }

    public class Handler : IRequestHandler<LoginGoogle_Command, ResponseBuilder<LoginGoogle_Response>>
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

        public async Task<ResponseBuilder<LoginGoogle_Response>> Handle(LoginGoogle_Command request, CancellationToken cancellationToken)
        {
            var data = _inspectionDatasource.MasterUsers.Select(w => w.EMAIL).ToList();

            if (!string.IsNullOrEmpty(request.Email) && request.isValidEmail != false)
            {
                var loggedInUser = _inspectionDatasource.MasterUsers.Where(w => w.EMAIL == request.Email).FirstOrDefault();
                if (loggedInUser != null && loggedInUser.LOGIN_TYPE != "Google")
                {
                    throw new Validation_Exception("Different Type of Login. Please Use Another Type of Login!");
                }
                else if (loggedInUser == null)
                {
                    loggedInUser = MasterUser.CreateMasterUser(new UserEntity
                    {
                        Email = request.Email,
                        RoleId = 2,
                        LoginType = "Google",
                        Writer = "SYSTEM"
                    });
                    
                    _inspectionDatasource.MasterUsers.Add(loggedInUser);
                    _inspectionDatasource.SaveChanges();
                };

                var roleName = _inspectionDatasource.MasterRoles.Where(w => w.ROLE_ID == loggedInUser.ROLE_ID).FirstOrDefault();
                var token = new LoginGoogle_Response
                {
                    Email = request.Email,
                    Token = _jwt.GenerateToken(loggedInUser, roleName.ROLE_NAME)
                };
                return token.ResponseRead();
            }
            else
            {
                throw new Validation_Exception("test");
            }
        }
    }
}
