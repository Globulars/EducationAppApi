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
    public interface IComponentService
    {
        BaseResponse SaveComponent(ComponentDTO component);

        BaseResponse GetComponentDetails(int ComponentId);

        IQueryable<ComponentDTO> GetAllComponent();
    }
}
