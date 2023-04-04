using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Net;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;
using Web.Services.Services;

namespace Web.App.Controllers
{
    
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private IConfiguration _config;
        
        private IWebHostEnvironment _hostEnvironment;
        public UserAuthenticationController(IUserAuthenticationService authService, IConfiguration config, IWebHostEnvironment environment)
        {
            this._authService = authService;
            this._config = config;
            this._hostEnvironment = environment;
           
        }

       

        
        [Description("User Login")]
        [HttpPost("auth/Login")]
        public BaseResponse Login([FromBody] UserCredentialDTO login)
        {

            try
            {
                BaseResponse response =  _authService.Login(login);
                return response;
            }
            catch (Exception ex)
            {
                
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
    }
}
