using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Repository.IRepository
{
   public interface IRepository <T> where T: class
    {
        Task<T> GetAsync(string url, int id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url, T ObjToCreate);
        Task<bool> UpdateAsync(string url, T ObjToUpdate);
        Task<bool> DeleteAsync(string url, int id);
    }
}
