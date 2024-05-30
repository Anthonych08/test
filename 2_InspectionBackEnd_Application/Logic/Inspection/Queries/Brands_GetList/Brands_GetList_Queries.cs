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
using System.Text.RegularExpressions;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Brands_GetList
{
    public class Brands_GetList_Queries : Brands_GetList_Request, IRequest<ResponseBuilder<Brands_GetList_Response>>
    {

    }

    public class Handler : IRequestHandler<Brands_GetList_Queries, ResponseBuilder<Brands_GetList_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<Brands_GetList_Response>> Handle(Brands_GetList_Queries request, CancellationToken cancellationToken)
        {
            var brandsDb = _inspectionDatasource.MasterMerkMotors.Select(w => new Brands
            {
                MerkMotorId = w.MERK_MOTOR_ID,
                MerkMotorLogo = w.MERK_MOTOR_LOGO,
                NamaMerkMotor = w.NAMA_MERK_MOTOR
            }).ToList();

            return new Brands_GetList_Response { BrandsList = brandsDb }.ResponseRead();
        }
    }
}
