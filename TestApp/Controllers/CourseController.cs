using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using Web.DTO.Common;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.App.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseService _authService;
        private IConfiguration _config;

        private IWebHostEnvironment _hostEnvironment;

        private ICourseService _courseService;
        public CourseController(ICourseService authService, IConfiguration config, IWebHostEnvironment environment, ICourseService courseService)
        {
            this._authService = authService;
            this._config = config;
            this._hostEnvironment = environment;
            this._courseService = courseService;

        }
        [AllowAnonymous]
        [Description("Save Course")]
        [HttpPost("course/SaveCourse")]
        public BaseResponse SaveCourse([FromBody] CourseDTO course)
        {

            try
            {
                BaseResponse response = _authService.SaveCourse(course);
                return response;
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("All Course")]
        [HttpGet("course/GetAllCourse")]
        public BaseResponse GetAllCourse()
        {

            try
            {
                var responseList = this._courseService.GetAllCourse();
                return new BaseResponse() { Status = HttpStatusCode.OK, Message = "Course's list returned", Body = responseList };
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("Course Details")]
        [HttpGet("course/GetCourseDetails")]
        public BaseResponse GetCourseDetails(int CourseId)
        {
            try
            {
                return this._courseService.GetCourseDetails(CourseId);
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
    }
}
