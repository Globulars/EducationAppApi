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
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.Services.Services
{
    public class RoleService : IRoleService

    {
        private readonly DbContext _dbContext;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IGenericRepository<Roles> _roleRepo;
        private readonly IGenericRepository<UserRoles> _userroleRepo;
        IConfiguration _config;

        private IHostingEnvironment _environment;
        public RoleService(
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

        public BaseResponse GetRoleDetails(int RoleId)
        {
            if (RoleId > 0)
            {
                var role = this._roleRepo.Table.Where(x => x.RoleId == RoleId && x.IsActive != false).FirstOrDefault();
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Data returned", Body = role };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter RoleId", };

            }
        }

        public IQueryable<RoleDTO> GetAllRole()
        {
            var role = _roleRepo.Table.Where(r => r.IsActive == true);
            var roleList = _roleRepo.Table.Where(x => x.IsActive == true);
            var responseList = roleList.Select(role => new RoleDTO
            {
                RoleId = role.RoleId,
                Role = role.Role,
                CreatedBy = role.CreatedBy,
                ModifiedBy = role.ModifiedBy,
                IsActive = role.IsActive,
               
            }).AsQueryable();
            return responseList;
        }

        public BaseResponse SaveRole(RoleDTO role)
        {
            BaseResponse response = new BaseResponse();
            if (role.RoleId > 0)
            {
                var dbUser = this._roleRepo.Table.Where(x => x.RoleId == role.RoleId && x.IsActive != false).FirstOrDefault();

                if (dbUser.RoleId == role.RoleId)
                {
                    dbUser.Role = role.Role;
                    dbUser.ModifiedBy = role.ModifiedBy;
                    dbUser.ModifiedDate = DateTime.Now;
                    dbUser.IsActive = true;

                    this._roleRepo.Update(dbUser);
                    return new BaseResponse { Status = HttpStatusCode.OK, Message = "Role's data updated successfully" };
                }
                else
                {
                    return new BaseResponse { Status = HttpStatusCode.BadRequest, Message = "RoleId incorrect" };
                }
            }
            else
            {

                Roles newRoles = new Roles
                {
                    Role = role.Role,
                    CreatedBy = role.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                };
                this._roleRepo.Insert(newRoles);
               
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Role created successfully" };
            }
        }
    }
}
