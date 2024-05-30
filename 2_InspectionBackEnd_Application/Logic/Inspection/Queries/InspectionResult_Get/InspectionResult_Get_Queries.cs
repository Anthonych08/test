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

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Queries.InspectionResult_Get
{
    public class InspectionResult_Get_Queries : InspectionResult_Get_Request, IRequest<ResponseBuilder<InspectionResult_Get_Response>>
    {

    }

    public class Handler : IRequestHandler<InspectionResult_Get_Queries, ResponseBuilder<InspectionResult_Get_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<InspectionResult_Get_Response>> Handle(InspectionResult_Get_Queries request, CancellationToken cancellationToken)
        {
            var currMasterTipe = _inspectionDatasource.MasterTipeMotors
                .Where(w => w.TIPE_MOTOR_ID == request.TypeMotorId)
                .FirstOrDefault();

            var currTahunTipe = _inspectionDatasource.TahunTipeMotors
                .Where(w => w.TIPE_MOTOR_ID == request.TypeMotorId)
                .Where(w => w.TAHUN_TIPE_MOTOR == request.TahunMotor)
                .FirstOrDefault();

            var response = new InspectionResult_Get_Response();

            if (currMasterTipe != null && currTahunTipe != null)
            {
                var currDetailTipeComponent = _inspectionDatasource.KomponenModelMotors
                    .Where(w => w.MODEL_MOTOR_ID == currMasterTipe.MODEL_MOTOR_ID)
                    .ToList();
                var allHeader = _inspectionDatasource.MasterKomponenMotors.ToList();
                var headerComponents = allHeader
                    .GroupBy(w => w.TIPE_KOMPONEN_MOTOR)
                    .Select(s => new HeaderComponent
                    {
                        HeaderComponentName = s.First().TIPE_KOMPONEN_MOTOR,
                        DetailComponents = s.Select(s2 => new DetailComponent
                        {
                            KomponenMotorId = s2.KOMPONEN_MOTOR_ID,
                            KomponenModelMotorId = currDetailTipeComponent
                            .Where(w => s2.KOMPONEN_MOTOR_ID == w.KOMPONEN_MOTOR_ID).FirstOrDefault() != null? currDetailTipeComponent
                            .Where(w => s2.KOMPONEN_MOTOR_ID == w.KOMPONEN_MOTOR_ID).First().KOMPONEN_MODEL_MOTOR_ID : null,
                            NamaKomponenMotor = s2.NAMA_KOMPONEN_MOTOR,
                            HargaKomponenMotor = currDetailTipeComponent
                            .Where(w => s2.KOMPONEN_MOTOR_ID == w.KOMPONEN_MOTOR_ID).FirstOrDefault() != null? currDetailTipeComponent
                            .Where(w => s2.KOMPONEN_MOTOR_ID == w.KOMPONEN_MOTOR_ID).First().HARGA_KOMPONEN : null,
                        }).ToList()
                    }).ToList();

                response = new InspectionResult_Get_Response
                {
                    AverageMarketEstimatedCost = currTahunTipe.HARGA_MOTOR_OLX,
                    MotorcycleTypeUrl = currMasterTipe.GAMBAR_TIPE_MOTOR,
                    HeaderComponents = headerComponents
                };
            }

            return response.ResponseRead();
        }
    }
}
