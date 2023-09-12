using ParkAPI_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.Repository.IRepository
{
   public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();

        ICollection<Trail> GetTrailsInNationalPark(int nationalParkid);

        Trail GetTrail(int trailId);

        bool TrailExists(int trailId);

        bool TrailExists(string trailname);

        bool CreateTrail(Trail trail);

        bool UpdateTrail(Trail trail);

        bool DeleteTrail(Trail trail);

        bool Save();

    }
}
