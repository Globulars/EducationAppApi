using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;
using Web.Services.Services;
using static Web.Models.Common.UserDTO;

namespace Web.App.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _authService;
        private IConfiguration _config;

        private IWebHostEnvironment _hostEnvironment;

        private IUserService _userService;
        public UserController(IUserService authService, IConfiguration config, IWebHostEnvironment environment, IUserService userService)
        {
            this._authService = authService;
            this._config = config;
            this._hostEnvironment = environment;
            this._userService = userService;

        }
        [AllowAnonymous]
        [Description("Save User")]
        [HttpPost("user/SaveUser")]
        public BaseResponse SaveUser([FromBody] UserDTO user)
        {

            try
            {
                BaseResponse response = _authService.SaveUser(user);
                return response;
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("All User")]
        [HttpGet("user/GetAllUser")]
        public BaseResponse GetAllUser()
        {

            try
            {
                var responseList = this._userService.GetAllUser();
                return new BaseResponse() { Status = HttpStatusCode.OK, Message = "User's list returned", Body = responseList };
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }

        [AllowAnonymous]
        [Description("Update Password")]
        [HttpPost("user/UpdatePassword")]
        public BaseResponse UpdatePassword([FromBody] UserPasswordDTO user)
        {

            try
            {
                return this._userService.UpdatePassword(user);
                
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("User Details")]
        [HttpGet("user/GetUserDetails")]
        public BaseResponse GetUserDetails(int UserId)
        {
            try
            {
                return this._userService.GetUserDetails(UserId);
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
    }
}
