﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.Models.Dtos
{
    public class NationalParkDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }
        
        
        public byte[] Picture { get; set; }

        public DateTime Created { get; set; }

        public DateTime  Establised { get; set; }
    }
}
