using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Common;
using Web.Models.Response;
using static Web.Models.Common.UserDTO;

namespace Web.Services.Interfaces
{
    public interface IUserService
    {
        BaseResponse GetUserDetails(int UserId);
        BaseResponse SaveUser(UserDTO user);
        BaseResponse UpdatePassword(UserPasswordDTO user);
        IQueryable<UserDTO> GetAllUser();

        //BaseResponse CheckIfUsernameAvailable(string UserName);
    }
}
