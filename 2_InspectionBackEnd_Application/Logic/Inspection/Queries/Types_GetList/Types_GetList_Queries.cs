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
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Models_GetList;

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Types_GetList
{
    public class Types_GetList_Queries : Types_GetList_Request, IRequest<ResponseBuilder<Types_GetList_Response>>
    {

    }

    public class Handler : IRequestHandler<Types_GetList_Queries, ResponseBuilder<Types_GetList_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<Types_GetList_Response>> Handle(Types_GetList_Queries request, CancellationToken cancellationToken)
        {
            var currModel = _inspectionDatasource.MasterModelMotors.Where(w => w.MODEL_MOTOR_ID == request.ModelMotorId).FirstOrDefault();

            var response = new Types_GetList_Response();

            if (currModel != null)
            {
                var allType = _inspectionDatasource.MasterTipeMotors
                    .Where(w => w.MODEL_MOTOR_ID == currModel.MODEL_MOTOR_ID)
                    .ToList();

                var allTahun = _inspectionDatasource.TahunTipeMotors.Where(w => allType.Select(s => s.TIPE_MOTOR_ID).Contains(w.TIPE_MOTOR_ID)).ToList();

                response.TypesList = allType
                    .Select(s => new Types
                    {
                        TipeMotorId = s.TIPE_MOTOR_ID,
                        TipeMotorName = s.NAMA_TIPE_MOTOR,
                        TipeMotorPicture = s.GAMBAR_TIPE_MOTOR,
                        TahunTipeMotor = allTahun
                            .Where(w2 => w2.TIPE_MOTOR_ID == s.TIPE_MOTOR_ID)
                            .Where(w2 => w2.HARGA_MOTOR_OLX != 0)
                            .OrderBy(s2 => s2.TAHUN_TIPE_MOTOR)
                            .Select(s2 => s2.TAHUN_TIPE_MOTOR)
                            .ToList()
                    }).ToList();
            }

            return response.ResponseRead();
        }
    }
}
