using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using Web.Models.Common;
using Web.Models.Response;

namespace Web.App.Controllers
{
    public class UserController : Controller
    {
        [AllowAnonymous]
        [Description("User Save")]
        [HttpPost("User/UserSave")]
        public BaseResponse Login([FromBody] UserDTO login)
        {
            return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = "", Body = ToString() };
        }
    }
}
