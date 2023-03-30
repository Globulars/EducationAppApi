using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Interfaces;
using Web.Data.Entities.Models;
using Web.Models.Common;
using Web.Models.Response;
using Web.Services.Interfaces;

namespace Web.Services.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly DbContext _dbContext;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IGenericRepository<Roles> _roleRepo;
        private readonly IGenericRepository<UserRoles> _userroleRepo;
        IConfiguration _config;

        private IHostingEnvironment _environment;


        public UserAuthenticationService(
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

        public BaseResponse Login(UserCredentialDTO login)
        {
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(login.username) && !string.IsNullOrEmpty(login.password))
            {
                var user = this._userRepo.Table.Where(x => (x.UserName == login.username) && x.IsActive != false).FirstOrDefault();
                if (user != null)
                {

                    if (user.Password == login.password)
                    {
                        var AuthorizedUser = GenerateJSONWebToken(user);
                        response.Body = AuthorizedUser;
                        response.Status = HttpStatusCode.OK;
                        response.Message = "User found";
                        
                        
                        //this._dbContext.Log(login, "Users", user.UserId, ActivityLogActionEnums.SignIn.ToInt());
                    }
                    else
                    {
                        response.Body = null;
                        response.Status = HttpStatusCode.NotFound;
                        response.Message = "Password is incorrect";
                    }
                }
                else
                {
                    response.Body = null;
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Username is not valid";
                }
            }
            return response;

        }

        private object GenerateJSONWebToken(Users user)
        {
            var userrole = this._userroleRepo.Table.FirstOrDefault(r => r.UserIdFK == user.UserId);
            var roleId = userrole.RoleIdFK;
            var role = this._roleRepo.Table.FirstOrDefault(v => v.RoleId == userrole.RoleIdFK);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    
                     new Claim("FirstName", $"{user.FirstName}"),
                     new Claim("LastName", $"{user.LastName}"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
             _config["Jwt:ValidIssuer"],
             claims,

             signingCredentials: credentials);
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
               roleId = userrole.RoleIdFK,
               role = role.Role,
                firstName = user.FirstName,
                lastName = user.LastName
            };
        }
    }
}
