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

namespace _2_InspectionBackEnd_Application.Logic.InspectionHistory.Queries.History_GetList
{
    public class History_GetList_Queries : History_GetList_Request, IRequest<ResponseBuilder<History_GetList_Response>>
    {

    }

    public class Handler : IRequestHandler<History_GetList_Queries, ResponseBuilder<History_GetList_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<History_GetList_Response>> Handle(History_GetList_Queries request, CancellationToken cancellationToken)
        {
            var response = new History_GetList_Response();
            var getCurrUser = _inspectionDatasource.MasterUsers.Where(w => w.EMAIL == request.Email).FirstOrDefault();
            if (getCurrUser != null)
            {
                var inspectionHeaderList = (from a in _inspectionDatasource.InspectionHistoryHeaders

                                join b in _inspectionDatasource.MasterTipeMotors on a.TIPE_MOTOR_ID equals b.TIPE_MOTOR_ID into abGroup
                                from b in abGroup.DefaultIfEmpty()

                                join c in _inspectionDatasource.MasterModelMotors on b.MODEL_MOTOR_ID equals c.MODEL_MOTOR_ID into bcGroup
                                from c in bcGroup.DefaultIfEmpty()

                                join d in _inspectionDatasource.MasterMerkMotors on c.MERK_MOTOR_ID equals d.MERK_MOTOR_ID into cdGroup
                                from d in cdGroup.DefaultIfEmpty()

                                where a.MASTER_USER_ID == getCurrUser.USER_ID && a.IS_DELETED != true
                                select new HistoryCardHeader
                                {
                                    InspectionHistoryHeaderId = a.INSPECTION_HISTORY_HEADER_ID,
                                    MerkLogoUrl = d.MERK_MOTOR_LOGO,
                                    TipeMotorName = b.NAMA_TIPE_MOTOR,
                                    Year = a.TAHUN_MOTOR,
                                    TipeMotorPicUrl = b.GAMBAR_TIPE_MOTOR,
                                    AfterRepairPrice = a.HARGA_MOTOR_OLX - a.HARGA_PERBAIKAN
                                }).ToList();

                return new History_GetList_Response { History = inspectionHeaderList }.ResponseRead();
            }


            return new History_GetList_Response { History = null }.ResponseRead();
        }
    }
}
