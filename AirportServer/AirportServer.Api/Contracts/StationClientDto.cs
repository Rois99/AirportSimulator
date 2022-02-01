using AirportServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportServer.Api.Contracts
{
    public class StationClientDto
    {
        public int Id { get; set; }
        public AirplaneDto Airplain { get; set; }

        public StationClientDto(Station station)
        {
            Id = station.Id;
            Airplain = new AirplaneDto(station.Airplain);
        }
    }
}
