using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkAPI_11.Data;
using ParkAPI_11.Models;
using ParkAPI_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkAPI_11.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly AppSetting _appSettings;

        public UserRepository(ApplicationDbContext context,IOptions<AppSetting> appsettings)
        {
            _Context = context;
            _appSettings = appsettings.Value;
        }
        public User Authenticate(string UserName, string Password)
        {
            var UserInDb = _Context.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (UserInDb == null)
                return null;
            //JWT Authentication
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescritor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name,UserInDb.Id.ToString()),
                    new Claim(ClaimTypes.Role,UserInDb.Role)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescritor);
            UserInDb.Token = tokenHandler.WriteToken(token);
            UserInDb.Password = "";
            return UserInDb;
        }

        public bool IsUniqueUser(string UserName)
        {
            var user = _Context.Users.FirstOrDefault(u => u.UserName == UserName);
            if (user == null)
                return true;
            else
                return false;
        }

        public User Register(string UserName, string Password)
        {
            User user = new User()
            {
                UserName = UserName,
                Password = Password,
                Role = "Admin"
            };
            _Context.Users.Add(user);
            _Context.SaveChanges();
            return user;

        }
    }
}
