using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Entities.Models;
using Web.Data.Interfaces;
using Web.Data.Migrations;
using Web.DTO.Common;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly DbContext _dbContext;
        private readonly IGenericRepository<Bookings> _bookingRepo;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IGenericRepository<Roles> _roleRepo;
        private readonly IGenericRepository<UserRoles> _userroleRepo;
        private readonly IGenericRepository<Components> _componentRepo;
        private readonly IGenericRepository<ComponentAccess> _componentAccessRepo;
        IConfiguration _config;

        private IHostingEnvironment _environment;

        public BookingService(
            IConfiguration config,
            //DbContext dbContext,
            //IHostingEnvironment environment,
            IGenericRepository<Bookings> bookingRepo,
            //DbContext dbContext,
            //IHostingEnvironment environment,
            IGenericRepository<Users> userRepo,
            IGenericRepository<Roles> roleRepo,
            IGenericRepository<UserRoles> userroleRepo,
            IGenericRepository<Components> componentRepo,
            IGenericRepository<ComponentAccess> componentsAccessRepo
            )
        {
            this._config = config;
            this._bookingRepo = bookingRepo;
            this._userRepo = userRepo;
            //this._dbContext = dbContext;
            this._roleRepo = roleRepo;

            this._userroleRepo = userroleRepo;

            this._componentRepo = componentRepo;

            this._componentAccessRepo = componentsAccessRepo;

        }

        public BaseResponse SaveBooking(BookingDTO booking)
        {
           
            BaseResponse response = new BaseResponse();
            if (booking.BookingId > 0)
            {
                var dbBooking = this._bookingRepo.Table.Where(b => b.BookingId == booking.BookingId && b.IsActive != false).FirstOrDefault();

                if (dbBooking.BookingId == booking.BookingId)
                {
                    dbBooking.BookingDate = booking.BookingDate;
                    dbBooking.BookingNo = booking.BookingNo;
                    dbBooking.BookingStatus = booking.BookingStatus;
                    dbBooking.ModifiedBy = booking.ModifiedBy;
                    dbBooking.ModifiedDate = DateTime.Now;
                    dbBooking.IsActive = true;

                    this._bookingRepo.Update(dbBooking);
                    return new BaseResponse { Status = HttpStatusCode.OK, Message = "Booking data updated successfully" };
                }
                else
                {
                    return new BaseResponse { Status = HttpStatusCode.BadRequest, Message = "Password incorrect" };
                }
            }
            else
            {
                Bookings newBooking = new Bookings
                {
                    BookingNo = booking.BookingNo,
                    BookingStatus = booking.BookingStatus,
                    CreatedBy = booking.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                };
                this._bookingRepo.Insert(newBooking);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Booking created successfully" };
            }


        }
        public IQueryable<BookingDTO> GetAllBooking()
        {
            var booking = _bookingRepo.Table.Where(r => r.IsActive == true);
            var bookingList = _bookingRepo.Table.Where(x => x.IsActive == true);
            var responseList = bookingList.Select(booking => new BookingDTO
            {
                BookingId = booking.BookingId,
                BookingDate = booking.BookingDate,
                BookingNo = booking.BookingNo,
                BookingStatus = booking.BookingStatus,
                ModifiedBy  = booking.ModifiedBy,
                ModifiedDate = booking.ModifiedDate,
                CreatedDate = booking.CreatedDate,
                CreatedBy = booking.CreatedBy,
                IsActive = true,

            }).AsQueryable();
            return responseList;
        }
        public BaseResponse GetBookingDetails(int BookingId)
        {
            if (BookingId > 0)
            {
                var booking = this._bookingRepo.Table.Where(x => x.BookingId == BookingId && x.IsActive != false).FirstOrDefault();
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Data returned", Body = booking };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter BookingId", };

            }
        }
        public BaseResponse DeleteBooking(int BookingId)
        {
            if (BookingId > 0)
            {
                var dbBooking = this._bookingRepo.Table.Where(x => x.BookingId == BookingId && x.IsActive != false).FirstOrDefault();
                this._bookingRepo.Delete(dbBooking);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Booking Deleted", };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter BookingId", };

            }
        }
    }
}
