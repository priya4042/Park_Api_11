using AutoMapper;
using ParkAPI_11.Models;
using ParkAPI_11.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();

            CreateMap<Trail, TrailDTO>().ReverseMap();
        }
    }
}
