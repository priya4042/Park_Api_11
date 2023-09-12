using ParkAPI_11.Data;
using ParkAPI_11.Models;
using ParkAPI_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _Context;
        public NationalParkRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _Context.NationalParks.Add( nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _Context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int NationalParkId)
        {
            return _Context.NationalParks.Find(NationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _Context.NationalParks.ToList();
        }

        public bool NationalParkExists(int NationalParkId)
        {
            return _Context.NationalParks.Any(np => np.Id == NationalParkId);
        }

        public bool NationalParkExists(string NationalParkName)
        {
            return _Context.NationalParks.Any(np => np.Name == NationalParkName);
        }

        public bool Save()
        {
            return _Context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _Context.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
