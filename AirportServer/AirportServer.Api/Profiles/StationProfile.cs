using AirportServer.Api.Contracts;
using AirportServer.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportServer.Api.Profiles
{
    public class StationProfile : Profile
    {
        public StationProfile()
        {
            //Source -> Target
            CreateMap<Station, StationClientDto>();
        }
    }
}
