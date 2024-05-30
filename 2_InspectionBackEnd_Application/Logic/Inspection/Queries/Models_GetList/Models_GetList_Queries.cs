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

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Models_GetList
{
    public class Models_GetList_Queries : Models_GetList_Request, IRequest<ResponseBuilder<Models_GetList_Response>>
    {

    }

    public class Handler : IRequestHandler<Models_GetList_Queries, ResponseBuilder<Models_GetList_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<Models_GetList_Response>> Handle(Models_GetList_Queries request, CancellationToken cancellationToken)
        {
            var currBrand = _inspectionDatasource.MasterMerkMotors.Where(w => w.MERK_MOTOR_ID == request.MerkMotorId).FirstOrDefault();
            
            var response = new Models_GetList_Response();

            if (currBrand != null)
            {
                response.ModelsList = _inspectionDatasource.MasterModelMotors
                    .Where(w => w.MERK_MOTOR_ID == currBrand.MERK_MOTOR_ID)
                    .Select(s => new Models
                    {
                        ModelMotorId = s.MODEL_MOTOR_ID,
                        NamaModelMotor = s.NAMA_MODEL_MOTOR
                    }).ToList();
            }

            return response.ResponseRead();
        }
    }
}
