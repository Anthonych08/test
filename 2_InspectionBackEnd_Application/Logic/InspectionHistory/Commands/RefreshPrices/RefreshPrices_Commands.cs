using _0_InspectionBackEnd_Shared.Responses;
using _1_InspectionBackEnd_Domain.Transaction;
using _2_InspectionBackEnd_Application.Exception;
using _2_InspectionBackEnd_Application.Extensions;
using _2_InspectionBackEnd_Application.Interfaces;
using MediatR;

namespace _2_InspectionBackEnd_Application.Logic.InspectionHistory.Commands.RefreshPrices
{
    public class RefreshPrices_Commands : RefreshPrices_Request, IRequest<ResponseBuilder<RefreshPrices_Response>>
    {

    }

    public class Handler : IRequestHandler<RefreshPrices_Commands, ResponseBuilder<RefreshPrices_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<RefreshPrices_Response>> Handle(RefreshPrices_Commands request, CancellationToken cancellationToken)
        {
            var getHistory = _inspectionDatasource.InspectionHistoryHeaders.Where(w => w.INSPECTION_HISTORY_HEADER_ID == request.HistoryHeaderId).FirstOrDefault();
            var getDetail = _inspectionDatasource.InspectionHistoryDetails.Where(w => w.INSPECTION_HISTORY_HEADER_ID == request.HistoryHeaderId).ToList();
            if (getHistory != null && getDetail != null)
            {
                var getNewTipeTahunMotor = _inspectionDatasource.TahunTipeMotors.Where(w => w.TIPE_MOTOR_ID == getHistory.TIPE_MOTOR_ID && w.TAHUN_TIPE_MOTOR == getHistory.TAHUN_MOTOR).FirstOrDefault();
                var currModelMOtor = _inspectionDatasource.MasterTipeMotors.Where(w => w.TIPE_MOTOR_ID == getHistory.TIPE_MOTOR_ID).FirstOrDefault();
                var getNewKomponenMotor = currModelMOtor != null ? 
                    _inspectionDatasource.KomponenModelMotors
                    .Where(w => getDetail
                    .Select(s => s.MASTER_KOMPONEN_ID)
                    .Contains(w.KOMPONEN_MOTOR_ID) && w.MODEL_MOTOR_ID == currModelMOtor.MODEL_MOTOR_ID).ToList() 
                    : null;

                if (getNewTipeTahunMotor != null && getNewKomponenMotor != null)
                {
                    var hargaPerbaikan = 0;
                    foreach (var item in getDetail)
                    {
                        var newKomponenPrices = getNewKomponenMotor.Where(w => w.KOMPONEN_MOTOR_ID == item.MASTER_KOMPONEN_ID).FirstOrDefault();
                        item.RefreshPrices(new InspectionHistoryDetailEntity
                        {
                            HargaKomponenTokopedia = newKomponenPrices != null ? newKomponenPrices.HARGA_KOMPONEN : item.HARGA_KOMPONEN_TOKOPEDIA,
                            WriterEmail = item.CREATED_BY
                        });
                        hargaPerbaikan = item.HARGA_KOMPONEN_TOKOPEDIA != null && item.STATUS_PERBAIKAN == true? hargaPerbaikan + (int)item.HARGA_KOMPONEN_TOKOPEDIA : hargaPerbaikan + 0;
                    }
                    getHistory.RefreshPrices(new InspectionHistoryHeaderEntity
                    {
                        HargaMotorOlx =  getNewTipeTahunMotor.HARGA_MOTOR_OLX,
                        HargaPerbaikan = hargaPerbaikan,
                        WriterEmail = getHistory.CREATED_BY
                    });
                }
                var transaction = _inspectionDatasource.Database.BeginTransaction();
                try
                {
                    _inspectionDatasource.InspectionHistoryHeaders.Update(getHistory);
                    _inspectionDatasource.InspectionHistoryDetails.UpdateRange(getDetail);
                    _inspectionDatasource.SaveChanges();
                    transaction.Commit();
                }
                catch (ArgumentException)
                {
                    transaction.Rollback();
                    throw new Validation_Exception("Something is wrong, Please Try Again!");
                }
            }

            return new RefreshPrices_Response { }.ResponseRead();
        }
    }
}
