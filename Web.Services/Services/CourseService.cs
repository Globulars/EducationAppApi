using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Entities.Models;
using Web.Data.Interfaces;
using Web.DTO.Common;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly DbContext _dbContext;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IGenericRepository<Roles> _roleRepo;
        private readonly IGenericRepository<UserRoles> _userroleRepo;
        private readonly IGenericRepository<Components> _componentRepo;
        private readonly IGenericRepository<ComponentAccess> _componentAccessRepo;
        private readonly IGenericRepository<Courses> _courseRepo;

        IConfiguration _config;

        private IHostingEnvironment _environment;
        public CourseService(
            IConfiguration config,
            //DbContext dbContext,
            //IHostingEnvironment environment,
            IGenericRepository<Users> userRepo,
            IGenericRepository<Roles> roleRepo,
            IGenericRepository<UserRoles> userroleRepo,
            IGenericRepository<Components> componentRepo,
            IGenericRepository<ComponentAccess> componentsAccessRepo,
            IGenericRepository<Courses> courseRepo
            )

        {
            this._config = config;
            //this._environment = environment;
            this._userRepo = userRepo;
            //this._dbContext = dbContext;
            this._roleRepo = roleRepo;

            this._userroleRepo = userroleRepo;

            this._componentRepo = componentRepo;

            this._componentAccessRepo = componentsAccessRepo;

            this._courseRepo = courseRepo;


        }
        public BaseResponse SaveCourse(CourseDTO course)
        {
            BaseResponse response = new BaseResponse();
            if (course.CourseId > 0)
            {
                var dbCourse = this._courseRepo.Table.Where(x => x.CourseId == course.CourseId && x.IsActive != false).FirstOrDefault();

                if (dbCourse.CourseId == course.CourseId)
                {
                    dbCourse.CourseName = course.CourseName;
                    dbCourse.ModifiedBy = course.ModifiedBy;
                    dbCourse.ModifiedDate = DateTime.Now;
                    dbCourse.IsActive = true;

                    this._courseRepo.Update(dbCourse);
                    return new BaseResponse { Status = HttpStatusCode.OK, Message = "Course's data updated successfully" };
                }
                else
                {
                    return new BaseResponse { Status = HttpStatusCode.BadRequest, Message = "Password incorrect" };
                }
            }
            else
            {
                Courses newCourses = new Courses
                {
                    CourseName = course.CourseName,
                    CreatedBy = course.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                };
                this._courseRepo.Insert(newCourses);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Courses created successfully" };
            }


        }
        public IQueryable<CourseDTO> GetAllCourse()
        {
            var course = _courseRepo.Table.Where(r => r.IsActive == true);
            var courseList = _courseRepo.Table.Where(x => x.IsActive == true);
            var responseList = courseList.Select(course => new CourseDTO
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CreatedBy = course.CreatedBy,
                ModifiedBy = course.ModifiedBy,
                IsActive = true,

            }).AsQueryable();
            return responseList;
        }

        public BaseResponse GetCourseDetails(int CourseId)
        {
            if (CourseId > 0)
            {
                var course = this._courseRepo.Table.Where(x => x.CourseId == CourseId && x.IsActive != false).FirstOrDefault();
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Data returned", Body = course };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter CourseId", };

            }
        }

        public BaseResponse DeleteCourse(int CourseId)
        {
            if (CourseId > 0)
            {
                var dbCourse = this._courseRepo.Table.Where(x => x.CourseId == CourseId && x.IsActive != false).FirstOrDefault();
                this._courseRepo.Delete(dbCourse);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Courses Deleted", };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter CourseId", };

            }
        }
    }
}
