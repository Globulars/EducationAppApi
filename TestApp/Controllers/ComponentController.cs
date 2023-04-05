using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using Web.DTO.Common;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;
using Web.Services.Services;

namespace Web.App.Controllers
{
    public class ComponentController : Controller
    {

        private readonly IComponentService _authService;
        private IConfiguration _config;

        private IWebHostEnvironment _hostEnvironment;

        private IComponentService _componentService;
        public ComponentController(IComponentService authService, IConfiguration config, IWebHostEnvironment environment, IComponentService componentService)
        {
            this._authService = authService;
            this._config = config;
            this._hostEnvironment = environment;
            this._componentService = componentService;

        }

        [AllowAnonymous]
        [Description("All Components")]
        [HttpGet("role/GetAllComponent")]
        public BaseResponse GetAllComponent()
        {

            try
            {
                var responseList = this._componentService.GetAllComponent();
                return new BaseResponse() { Status = HttpStatusCode.OK, Message = "Component's list returned", Body = responseList };
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("Save Component")]
        [HttpPost("component/SaveComponent")]
        public BaseResponse SaveComponent([FromBody] ComponentDTO component)
        {

            try
            {
                BaseResponse response = _authService.SaveComponent(component);
                return response;
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("Component Details")]
        [HttpGet("component/GetComponentDetails")]
        public BaseResponse GetComponentDetails(int ComponentId)
        {
            try
            {
                return this._componentService.GetComponentDetails(ComponentId);
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
    }
}
