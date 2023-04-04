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
    public interface IRoleService
    {
        IQueryable<RoleDTO> GetAllRole();

        BaseResponse GetRoleDetails(int RoleId);

        BaseResponse SaveRole(RoleDTO role);
    }
}
