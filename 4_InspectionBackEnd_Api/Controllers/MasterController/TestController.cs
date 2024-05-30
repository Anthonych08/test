using _2_InspectionBackEnd_Application.Logic.Authentication.Login;
using Microsoft.AspNetCore.Mvc;
using _4_InspectionBackEnd_Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using _2_InspectionBackEnd_Application.Logic.Testing.Queries.Tokopedia;
using _2_InspectionBackEnd_Application.Logic.Testing.Queries.Olx;

namespace _4_InspectionBackEnd_Api.Controllers
{
    [Route("test")]
    [Tags("test")]
    public class TestController : ApiController
    {
        public TestController()
        {

        }
        [HttpPost("tokopedia-search")]
        public async Task<ActionResult> TokopediaItem_Get_Queries([FromBody] TokopediaItem_Get_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("olx-search")]
        public async Task<ActionResult> OlxItem_Get_Queries([FromBody] OlxItem_Get_Queries request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}