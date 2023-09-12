﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Models
{
    public class NationalPark
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

       
        public string State { get; set; }

        public byte[] Picture { get; set; }

        public DateTime Created { get; set; }

        public DateTime Established { get; set; }

    }
}
