using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Models
{
    public class Trail
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Distance { get; set; }

        [Required]
        public string Elevation { get; set; }
 
        public enum DifficultyType { Easy, Moderate, Difficult }
        [Required]

        public DifficultyType DifficultyTypes { get; set; }

        public int NationalParkId { get; set; }
        
        public NationalPark NationalPark { get; set; }
    }
}
