using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Entities.Models;
using Web.Data.Interfaces;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;
using static Web.Models.Common.UserDTO;

namespace Web.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IGenericRepository<Roles> _roleRepo;
        private readonly IGenericRepository<UserRoles> _userroleRepo;
        IConfiguration _config;

        private IHostingEnvironment _environment;
        public UserService(
            IConfiguration config,
            //DbContext dbContext,
            //IHostingEnvironment environment,
            IGenericRepository<Users> userRepo,
            IGenericRepository<Roles> roleRepo,
            IGenericRepository<UserRoles> userroleRepo
            )

        {
            this._config = config;
            //this._environment = environment;
            this._userRepo = userRepo;
            //this._dbContext = dbContext;
            this._roleRepo = roleRepo;

            this._userroleRepo = userroleRepo;



        }

        public BaseResponse GetUserDetails(int UserId)
        {
            if (UserId > 0)
            {
                var user = this._userRepo.Table.Where(x => x.UserId == UserId && x.IsActive != false).FirstOrDefault();
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Data returned", Body = user };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter UserId", };

            }
        }


        public BaseResponse SaveUser(UserDTO user)
        {
            BaseResponse response = new BaseResponse();
            if (user.UserId > 0)
            {
                var dbUser = this._userRepo.Table.Where(x => x.UserId == user.UserId && x.IsActive != false).FirstOrDefault();

                if (dbUser.Password == user.Password)
                {
                    dbUser.UserName = user.UserName;
                    dbUser.FullName = user.FullName;
                    dbUser.Email = user.Email;
                    dbUser.Phone = user.Phone;
                    dbUser.Password = user.Password;
                    dbUser.Address = user.Address;
                    dbUser.ModifiedBy = user.ModifiedBy;
                    dbUser.ModifiedDate = DateTime.Now;
                    dbUser.IsActive = true;
                    
                    this._userRepo.Update(dbUser);
                    return new BaseResponse { Status = HttpStatusCode.OK, Message = "User's data updated successfully" };
                }
                else
                {
                    return new BaseResponse { Status = HttpStatusCode.BadRequest, Message = "Password incorrect" };
                }
            }
            else
            {

                Users newUsers = new Users
                {
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    CreatedBy = user.CreatedBy,
                    CreatedDate = DateTime.Now,
                    Password = user.Password,
                    IsActive = true,

                };
                this._userRepo.Insert(newUsers);
                UserRoles userRole = new UserRoles()
                {
                    RoleIdFK = user.RoleId,
                    UserIdFK = newUsers.UserId,
                    CreatedBy = user.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                this._userroleRepo.Insert(userRole);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "User created successfully" };
            }
        }
        public IQueryable<UserDTO> GetAllUser()
        {
            var role = _roleRepo.Table.Where(r => r.IsActive == true);
            var userList = _userRepo.Table.Where(x => x.IsActive == true);
            var responseList = userList.Select(user => new UserDTO 
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.UserId,
                Role = this._roleRepo.Table.Where(r => r.RoleId == user.UserId).Select(r => r.Role).FirstOrDefault(),
                Address = user.Address,
                CreatedBy = user.CreatedBy,
                Password = user.Password,
            }).AsQueryable();
            return responseList;
        }
        public BaseResponse UpdatePassword(UserPasswordDTO user)
        {
            var dbUser = this._userRepo.Table.Where(x => x.UserId == user.UserId && x.IsActive != false).FirstOrDefault();
            
            if (dbUser.Password == user.OldPassword)
            {
                dbUser.Password = (user.NewPassword);
                this._userRepo.Update(dbUser);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Password changed successfully" };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.BadRequest, Message = "Password incorrect" };
            }
        }

        //public BaseResponse CheckIfUsernameAvailable(string UserName)
        //{
        //    var usernameCount = this._userRepo.Table.Count(x => x.UserName == UserName);
        //    if (usernameCount > 0)
        //    {
        //        return new BaseResponse { Status = HttpStatusCode.OK, Message = "Username already exists", Body = new { usernameAvailable = false, message = "Username already exists" } };
        //    }
        //    else
        //    {
        //        return new BaseResponse { Status = HttpStatusCode.OK, Message = "Username available", Body = new { usernameAvailable = true, message = "Username available" } };

        //    }
        //}
    }
}
