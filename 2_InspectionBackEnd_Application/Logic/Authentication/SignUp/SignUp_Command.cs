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
using _2_InspectionBackEnd_Application.Logic.Authentication.Login;

namespace _2_InspectionBackEnd_Application.Logic.Authentication.SignUp
{
    public class SignUp_Command : SignUp_Request, IRequest<ResponseBuilder<SignUp_Response>>
    {

    }

    public class Handler : IRequestHandler<SignUp_Command, ResponseBuilder<SignUp_Response>>
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

        public async Task<ResponseBuilder<SignUp_Response>> Handle(SignUp_Command request, CancellationToken cancellationToken)
        {
            var data = _inspectionDatasource.MasterUsers.Select(w => w.EMAIL).ToList();

            if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
            {
                var inputPassword = PasswordOperation.HashPassword(request.Password);
                var newlyCreatedUser = MasterUser.CreateMasterUser(new UserEntity
                {
                    Email = request.Email,
                    Password = inputPassword,
                    ProfilePicture = null,
                    RoleId = 2,
                    LoginType = "Database",
                    Writer = request.Email
                });
                _inspectionDatasource.MasterUsers.Add(newlyCreatedUser);
                _inspectionDatasource.SaveChanges();

                var roleName = _inspectionDatasource.MasterRoles.Where(w => w.ROLE_ID == 2).FirstOrDefault();
                var token = new SignUp_Response
                {
                    Email = request.Email,
                    Token = _jwt.GenerateToken(newlyCreatedUser, roleName.ROLE_NAME)
                };
                return token.ResponseRead();
            }
            else
            {
                throw new Validation_Exception("Fill The Required Input");
            }
        }
    }
}
