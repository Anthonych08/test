using _2_InspectionBackEnd_Application.Logic.Authentication.Login;
using Microsoft.AspNetCore.Mvc;
using _4_InspectionBackEnd_Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using _2_InspectionBackEnd_Application.Logic.Testing.Queries.Tokopedia;
using _2_InspectionBackEnd_Application.Logic.Testing.Queries.Olx;
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Brands_GetList;
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Models_GetList;
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.Types_GetList;
using _2_InspectionBackEnd_Application.Logic.Inspection.Queries.InspectionResult_Get;
using _2_InspectionBackEnd_Application.Logic.Inspection.Commands.SaveInspection;

namespace _4_InspectionBackEnd_Api.Controllers.InspeksiController
{
    [Route("inspeksi")]
    [Tags("inspeksi")]
    public class InspeksiController : ApiController
    {
        [HttpGet("brands-getlist"), Authorize()]
        public async Task<ActionResult> Brands_GetList([FromQuery] Brands_GetList_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpGet("models-getlist"), Authorize()]
        public async Task<ActionResult> Models_GetList([FromQuery] Models_GetList_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpGet("types-getlist"), Authorize()]
        public async Task<ActionResult> Types_GetList([FromQuery] Types_GetList_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("inspection-result-get"), Authorize()]
        public async Task<ActionResult> InspectionResult_Get([FromBody] InspectionResult_Get_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("save-inspection"), Authorize()]
        public async Task<ActionResult> SaveInspection([FromBody] SaveInspection_Commands request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}