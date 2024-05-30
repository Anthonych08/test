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

namespace _2_InspectionBackEnd_Application.Logic.Testing.Queries.Tokopedia
{
    public class TokopediaItem_Get_Queries : TokopediaItem_Get_Request, IRequest<ResponseBuilder<TokopediaItem_Get_Response>>
    {

    }

    public class Handler : IRequestHandler<TokopediaItem_Get_Queries, ResponseBuilder<TokopediaItem_Get_Response>>
    {
        private readonly IInspection_Datasource _inspectionDatasource;
        public Handler(
            IInspection_Datasource inspectionDatasource
            )
        {
            _inspectionDatasource = inspectionDatasource;
        }

        public async Task<ResponseBuilder<TokopediaItem_Get_Response>> Handle(TokopediaItem_Get_Queries request, CancellationToken cancellationToken)
        {
            var tokpedPrices = WebScrapeExtension.Tokopedia(request.SearchItem);
            var averagePrice = 0;
            foreach (var item in tokpedPrices)
            {
                var splittedNumbers = Regex.Split(item, @"\D+");
                var price = int.Parse(string.Join(string.Empty, splittedNumbers));
                averagePrice += price;
            }
            averagePrice /= tokpedPrices.Count;
            return new TokopediaItem_Get_Response { AveragePrice = string.Concat("Rp", averagePrice.ToString("N")) }.ResponseRead();
        }
    }
}
