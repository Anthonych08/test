using _2_InspectionBackEnd_Application.Logic.Authentication.Login;
using Microsoft.AspNetCore.Mvc;
using _4_InspectionBackEnd_Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using _2_InspectionBackEnd_Application.Logic.Authentication.LoginGoogle;
using _2_InspectionBackEnd_Application.Logic.Authentication.SignUp;

namespace _4_InspectionBackEnd_Api.Controllers
{
    [Route("master")]
    [Tags("auth")]
    public class AuthController : ApiController
    {
        public AuthController()
        {

        }
        //[HttpPost("get-list-dx"), Authorize(Roles = "Employee")]
        //public async Task<ActionResult> ContainerMaster_GetListDX([FromBody] GetUser_Command request)
        //{
        //    return Ok(await Mediator.Send(request));
        //}
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login_Command request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("login-google")]
        public async Task<ActionResult> LoginGoogle([FromBody] LoginGoogle_Command request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] SignUp_Command request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}