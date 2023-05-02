using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DTO.Common;
using Web.Models.Common;
using Web.Models.Response;

namespace Web.Services.Interfaces
{
    public interface IBookingService
    {
        BaseResponse SaveBooking(BookingDTO book);
        IQueryable<BookingDTO> GetAllBooking();
        BaseResponse GetBookingDetails(int BookingId);
        BaseResponse DeleteBooking(int BookingId);
    }
}
