using ParkAPI_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.Repository.IRepository
{
   public interface IUserRepository
    {
        bool IsUniqueUser(string UserName);

        User Authenticate(string UserName, string Password);

        User Register(string UserName, string Password);
    }
}
