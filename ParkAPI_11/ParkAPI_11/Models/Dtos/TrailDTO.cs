using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ParkAPI_11.Models.Trail;

namespace ParkAPI_11.Models.Dtos
{
    public class TrailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Distance { get; set; }

        public string Elevation { get; set; }

        public DifficultyType DifficultyType { get; set; }

        public int NationalParkId { get; set; }

        public NationalPark NationalPark { get; set; }
    }
}
