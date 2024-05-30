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
using _1_InspectionBackEnd_Domain.Transaction;
using Newtonsoft.Json;
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.InspectionResult_Get;

namespace _2_InspectionBackEnd_Application.Logic.InspectionHistory.Queries.History_Get
{
    public class History_Get_Queries : History_Get_Request, IRequest<ResponseBuilder<History_Get_Response>>
    {

    }

    public class Handler : IRequestHandler<History_Get_Queries, ResponseBuilder<History_Get_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<History_Get_Response>> Handle(History_Get_Queries request, CancellationToken cancellationToken)
        {
            var response = new History_Get_Response();
            var getCurrHeader = _inspectionDatasource.InspectionHistoryHeaders.Where(w => w.INSPECTION_HISTORY_HEADER_ID == request.InspectionHeaderId).FirstOrDefault();
            if (getCurrHeader != null)
            {
                var inspectionDetailList = (from a in _inspectionDatasource.InspectionHistoryDetails

                                            join b in _inspectionDatasource.MasterKomponenMotors on a.MASTER_KOMPONEN_ID equals b.KOMPONEN_MOTOR_ID into abGroup
                                            from b in abGroup.DefaultIfEmpty()

                                            where a.INSPECTION_HISTORY_HEADER_ID == getCurrHeader.INSPECTION_HISTORY_HEADER_ID
                                            select new
                                            {
                                                InspectionHistoryDetail = a,
                                                MasterKomponen = b
                                            }).OrderBy(o => o.MasterKomponen.KOMPONEN_MOTOR_ID).ToList();

                var headerComponents = inspectionDetailList
                    .DistinctBy(w => w.MasterKomponen.TIPE_KOMPONEN_MOTOR).Select(s => new HeaderComponent
                {
                    HeaderComponentName = s.MasterKomponen.TIPE_KOMPONEN_MOTOR,
                    DetailComponents = inspectionDetailList
                    .Where(w2 => w2.MasterKomponen.TIPE_KOMPONEN_MOTOR == s.MasterKomponen.TIPE_KOMPONEN_MOTOR)
                    .Select(s2 => new DetailComponent
                    {
                        KomponenMotorId = s2.MasterKomponen.KOMPONEN_MOTOR_ID,
                        HargaKomponenMotor = s2.InspectionHistoryDetail.HARGA_KOMPONEN_TOKOPEDIA,
                        NamaKomponenMotor = s2.MasterKomponen.NAMA_KOMPONEN_MOTOR,
                        NeedReplacement = s2.InspectionHistoryDetail.STATUS_PERBAIKAN
                    }).OrderBy(o => o.KomponenMotorId).ToList()
                }).ToList();

                return new History_Get_Response
                {
                    HargaMotorOlx = getCurrHeader.HARGA_MOTOR_OLX,
                    HargaPerbaikan = getCurrHeader.HARGA_PERBAIKAN,
                    HeaderComponents = headerComponents
                }.ResponseRead();
            }


            return new History_Get_Response { }.ResponseRead();
        }
    }
}
