using Microsoft.EntityFrameworkCore;
using ParkAPI_11.Data;
using ParkAPI_11.Models;
using ParkAPI_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _Context;
        public TrailRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public bool CreateTrail(Trail trail)
        {
            _Context.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _Context.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _Context.Trails.
                Include(np => np.NationalPark).FirstOrDefault(t => t.Id == trailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return _Context.Trails.Include(np => np.NationalPark).ToList();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int nationalParkid)
        {
          return  _Context.Trails.Include(np => np.NationalPark).
                 Where(t => t.NationalParkId == nationalParkid).ToList();
        }

        public bool Save()
        {
            return _Context.SaveChanges() == 1 ? true : false;
        }

        public bool TrailExists(int trailId)
        {
            return _Context.Trails.Any(t => t.Id == trailId);
        }

        public bool TrailExists(string trailname)
        {
            return _Context.Trails.Any(t => t.Name == trailname);
        }

        public bool UpdateTrail(Trail trail)
        {
            _Context.Trails.Update(trail);
            return Save();
        }
    }
}
