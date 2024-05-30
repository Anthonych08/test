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

namespace _2_InspectionBackEnd_Application.Logic.Inspection.Commands.SaveInspection
{
    public class SaveInspection_Commands : SaveInspection_Request, IRequest<ResponseBuilder<SaveInspection_Response>>
    {

    }

    public class Handler : IRequestHandler<SaveInspection_Commands, ResponseBuilder<SaveInspection_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<SaveInspection_Response>> Handle(SaveInspection_Commands request, CancellationToken cancellationToken)
        {
            var email = _inspectionDatasource.MasterUsers.Where(w => w.EMAIL ==  request.Email).FirstOrDefault();
            if (email != null)
            {
                var transaction = _inspectionDatasource.Database.BeginTransaction();
                try
                {
                    var inspectionHistoryHeader = InspectionHistoryHeader.CreateInspectionHistoryHeader(new InspectionHistoryHeaderEntity
                    {
                        MasterUserId = email.USER_ID,
                        TipeMotorId = request.TipeMotorId,
                        TahunMotor = request.TahunMotor,
                        HargaMotorOlx = request.HargaMotorOlx,
                        HargaPerbaikan = request.HargaPerbaikan,
                        IsDeleted = false,
                        WriterEmail = request.Email
                    });
                    _inspectionDatasource.InspectionHistoryHeaders.Add(inspectionHistoryHeader);
                    _inspectionDatasource.SaveChanges();

                    var listInspectionHistoryDetail = new List<InspectionHistoryDetail>();
                    if (request.DetailComponents != null)
                    {
                        foreach (var component in request.DetailComponents)
                        {
                            listInspectionHistoryDetail.Add(InspectionHistoryDetail.CreateInspectionHistoryDetail(new InspectionHistoryDetailEntity
                            {
                                InspectionHistoryHeaderId = inspectionHistoryHeader.INSPECTION_HISTORY_HEADER_ID,
                                MasterKomponenId = component.KomponenMotorId,
                                HargaKomponenTokopedia = component.HargaKomponenMotor,
                                StatusPerbaikan = component.NeedReplacement,
                                WriterEmail = request.Email
                            }));
                        };
                    };

                    _inspectionDatasource.InspectionHistoryDetails.AddRange(listInspectionHistoryDetail);
                    _inspectionDatasource.SaveChanges();

                    transaction.Commit();
                }
                catch (ArgumentException)
                {
                    transaction.Rollback();
                    return new SaveInspection_Response { Message = "Inspection Result Have Some Error, Please Try Again." }.ResponseRead();
                }

            };

            return new SaveInspection_Response { Message = "Inspection Result Has Succesfully Been Saved!" }.ResponseRead();
        }
    }
}
