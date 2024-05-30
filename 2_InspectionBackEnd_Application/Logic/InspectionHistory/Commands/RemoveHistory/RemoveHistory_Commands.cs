using _0_InspectionBackEnd_Shared.Responses;
using _2_InspectionBackEnd_Application.Extensions;
using _2_InspectionBackEnd_Application.Interfaces;
using MediatR;

namespace _2_InspectionBackEnd_Application.Logic.InspectionHistory.Commands.RemoveHistory
{
    public class RemoveHistory_Commands : RemoveHistory_Request, IRequest<ResponseBuilder<RemoveHistory_Response>>
    {

    }

    public class Handler : IRequestHandler<RemoveHistory_Commands, ResponseBuilder<RemoveHistory_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<RemoveHistory_Response>> Handle(RemoveHistory_Commands request, CancellationToken cancellationToken)
        {
            var getHistory = _inspectionDatasource.InspectionHistoryHeaders.Where(w => w.INSPECTION_HISTORY_HEADER_ID == request.HistoryHeaderId).FirstOrDefault();
            if ( getHistory != null )
            {
                getHistory.SoftDelete();
                _inspectionDatasource.InspectionHistoryHeaders.Update( getHistory );
                await _inspectionDatasource.SaveChangesAsync();
            }

            return new RemoveHistory_Response { }.ResponseRead();
        }
    }
}
