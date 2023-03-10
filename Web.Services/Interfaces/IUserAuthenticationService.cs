using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Common;
using Web.Models.Response;

namespace Web.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        BaseResponse Login(UserCredentialDTO login);
    }
}
