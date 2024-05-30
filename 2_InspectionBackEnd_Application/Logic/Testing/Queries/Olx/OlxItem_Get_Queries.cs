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

namespace _2_InspectionBackEnd_Application.Logic.Testing.Queries.Olx
{
    public class OlxItem_Get_Queries : OlxItem_Get_Request, IRequest<ResponseBuilder<OlxItem_Get_Response>>
    {

    }

    public class Handler : IRequestHandler<OlxItem_Get_Queries, ResponseBuilder<OlxItem_Get_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<OlxItem_Get_Response>> Handle(OlxItem_Get_Queries request, CancellationToken cancellationToken)
        {
            var olxPrices = WebScrapeExtension.Olx(request.SearchItem);
            long averagePrice = 0;
            foreach (var item in olxPrices)
            {
                var splittedNumbers = Regex.Split(item, @"\D+");
                var price = int.Parse(string.Join(string.Empty, splittedNumbers));
                averagePrice += price;
            }
            averagePrice /= olxPrices.Count;
            string avgPrive = averagePrice.ToString("N");
            return new OlxItem_Get_Response { AveragePrice = string.Concat("Rp", avgPrive)  }.ResponseRead();
        }
    }
}
