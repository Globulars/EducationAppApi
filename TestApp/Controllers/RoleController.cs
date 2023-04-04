using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using Web.DTO.Common;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.App.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _authService;
        private IConfiguration _config;

        private IWebHostEnvironment _hostEnvironment;

        private IRoleService _roleService;
        public RoleController(IRoleService authService, IConfiguration config, IWebHostEnvironment environment, IRoleService roleService)
        {
            this._authService = authService;
            this._config = config;
            this._hostEnvironment = environment;
            this._roleService = roleService;

        }

        [AllowAnonymous]
        [Description("All Role")]
        [HttpGet("role/GetAllRole")]
        public BaseResponse GetAllRole()
        {

            try
            {
                var responseList = this._roleService.GetAllRole();
                return new BaseResponse() { Status = HttpStatusCode.OK, Message = "Role's list returned", Body = responseList };
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }

        [AllowAnonymous]
        [Description("Role Details")]
        [HttpGet("role/GetRoleDetails")]
        public BaseResponse GetRoleDetails(int RoleId)
        {
            try
            {
                return this._roleService.GetRoleDetails(RoleId);
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }

        [AllowAnonymous]
        [Description("Save Role")]
        [HttpPost("role/SaveRole")]
        public BaseResponse SaveRole([FromBody] RoleDTO role)
        {

            try
            {
                BaseResponse response = _authService.SaveRole(role);
                return response;
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
    }
}
