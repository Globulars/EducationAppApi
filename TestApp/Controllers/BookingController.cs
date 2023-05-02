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
    public class BookingController : Controller
    {
        private readonly IBookingService _authService;
        private IConfiguration _config;

        private IWebHostEnvironment _hostEnvironment;

        private IBookingService _bookingService;
        public BookingController(IBookingService authService, IConfiguration config, IWebHostEnvironment environment, IBookingService bookingService)
        {
            this._authService = authService;
            this._config = config;
            this._hostEnvironment = environment;
            this._bookingService = bookingService;

        }
        [AllowAnonymous]
        [Description("Save Booking")]
        [HttpPost("booking/SaveBooking")]
        public BaseResponse SaveBooking([FromBody] BookingDTO booking)
        {

            try
            {
                BaseResponse response = _authService.SaveBooking(booking);
                return response;
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("All Booking")]
        [HttpGet("booking/GetAllBooking")]
        public BaseResponse GetAllBooking()
        {

            try
            {
                var responseList = this._bookingService.GetAllBooking();
                return new BaseResponse() { Status = HttpStatusCode.OK, Message = "Booking list returned", Body = responseList };
            }
            catch (Exception ex)
            {

                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("Booking Details")]
        [HttpGet("booking/GetBookingDetails")]
        public BaseResponse GetBookingDetails(int BookingId)
        {
            try
            {
                return this._bookingService.GetBookingDetails(BookingId);
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }

        }
        [AllowAnonymous]
        [Description("Booking Delete")]
        [HttpDelete("booking/DeleteBooking")]
        public BaseResponse DeleteBooking(int BookingId)
        {
            try
            {
                return this._bookingService.DeleteBooking(BookingId);
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Status = HttpStatusCode.BadRequest, Message = ex.Message.ToString(), Body = ex.ToString() };
            }
        }
    }
}
