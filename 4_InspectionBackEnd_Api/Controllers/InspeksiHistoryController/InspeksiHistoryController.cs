using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _2_InspectionBackEnd_Application.Logic.InspectionHistory.Queries.History_GetList;
using _2_InspectionBackEnd_Application.Logic.InspectionHistory.Commands.RemoveHistory;
using _2_InspectionBackEnd_Application.Logic.InspectionHistory.Queries.History_Get;
using _2_InspectionBackEnd_Application.Logic.InspectionHistory.Commands.RefreshPrices;

namespace _4_InspectionBackEnd_Api.Controllers.InspeksiHistoryController
{
    [Route("inspeksi-history")]
    [Tags("inspeksi-history")]
    public class InspeksiHistoryController : ApiController
    {
        [HttpPost("history-getlist"), Authorize()]
        public async Task<ActionResult> History_GetList([FromBody] History_GetList_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("history-get"), Authorize()]
        public async Task<ActionResult> History_Get([FromBody] History_Get_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("history-delete"), Authorize()]
        public async Task<ActionResult> History_Delete([FromBody] RemoveHistory_Commands request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("refresh-prices"), Authorize()]
        public async Task<ActionResult> Refresh_Prices([FromBody] RefreshPrices_Commands request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}