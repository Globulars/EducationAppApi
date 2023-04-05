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
    public class ComponentService : IComponentService
    {

        private readonly DbContext _dbContext;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IGenericRepository<Roles> _roleRepo;
        private readonly IGenericRepository<UserRoles> _userroleRepo;
        private readonly IGenericRepository<Components> _componentRepo;
        private readonly IGenericRepository<ComponentAccess> _componentAccessRepo;

        IConfiguration _config;

        private IHostingEnvironment _environment;
        public ComponentService(
            IConfiguration config,
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
            //this._environment = environment;
            this._userRepo = userRepo;
            //this._dbContext = dbContext;
            this._roleRepo = roleRepo;

            this._userroleRepo = userroleRepo;

            this._componentRepo = componentRepo;

            this._componentAccessRepo = componentsAccessRepo;


        }

        public IQueryable<ComponentDTO> GetAllComponent()
        {
            var component = _componentRepo.Table.Where(r => r.IsActive == true);
            var componentList = _componentRepo.Table.Where(x => x.IsActive == true);
            var responseList = componentList.Select(component => new ComponentDTO
            {

                ComModuleName = component.ComModuleName,
                PageUrl = component.PageUrl,
                PageName = component.PageName,
                PageTitle = component.PageTitle,
                PageDescription = component.PageDescription,
                SortOrder = component.SortOrder,
                ModuleImage = component.ModuleImage,
                CreatedBy = component.CreatedBy,
                IsActive = true,

            }).AsQueryable();
            return responseList;
        }
        public BaseResponse GetComponentDetails(int ComponentId)
        {
            if (ComponentId > 0)
            {
                var components = this._componentRepo.Table.Where(x => x.ComponentId == ComponentId && x.IsActive != false).FirstOrDefault();
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Data returned", Body = components };
            }
            else
            {
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Please Enter ComponentId", };

            }
        }
        public BaseResponse SaveComponent(ComponentDTO component)
        {
            BaseResponse response = new BaseResponse();
            if (component.ComponentId > 0)
            {
                var dbComponent = this._componentRepo.Table.Where(x => x.ComponentId == component.ComponentId && x.IsActive != false).FirstOrDefault();

                if (dbComponent.ComponentId == component.ComponentId)
                {
                    dbComponent.ComModuleName = component.ComModuleName;
                    dbComponent.ParentComponentId = component.ParentComponentId;
                    dbComponent.PageUrl = component.PageUrl;
                    dbComponent.PageName = component.PageName;
                    dbComponent.PageTitle = component.PageTitle;
                    dbComponent.PageDescription = component.PageDescription;
                    dbComponent.SortOrder = component.SortOrder;
                    dbComponent.ModuleImage = component.ModuleImage;
                    dbComponent.ModifiedBy = component.ModifiedBy;
                    dbComponent.ModifiedDate = DateTime.Now;
                    dbComponent.IsActive = true;

                    this._componentRepo.Update(dbComponent);
                    return new BaseResponse { Status = HttpStatusCode.OK, Message = "Component's data updated successfully" };
                }
                else
                {
                    return new BaseResponse { Status = HttpStatusCode.BadRequest, Message = "Password incorrect" };
                }
            }
            else
            {

                Components newComponents = new Components
                {
                    ComModuleName = component.ComModuleName,
                    PageUrl = component.PageUrl,
                    PageName = component.PageName,
                    PageTitle = component.PageTitle,
                    PageDescription = component.PageDescription,
                    SortOrder = component.SortOrder,
                    ModuleImage =component.ModuleImage,
                    CreatedBy = component.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                };
                this._componentRepo.Insert(newComponents);
                ComponentAccess ComponentAccess = new ComponentAccess()
                {
                    ComponentIdFK = newComponents.ComponentId,
                    CreatedBy = component.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                this._componentAccessRepo.Insert(ComponentAccess);
                return new BaseResponse { Status = HttpStatusCode.OK, Message = "Component created successfully" };
            }


        }
    }
}
