using AirportServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Domain.Interfaces
{
    public interface IAirportRepository
    {
        public void AddFlight(Airplane airplain);

        public void AddVisit(Visit visit);

        public void AddStation(Station station);

        public List<Airplane> GetPlannedFlights();

        public void UpdateEnteredAirport(Airplane airplane);

        public void UpdateStationMovment(Station statio, Airplane airplain);

        public void UpdateMovmentInAirplane(int statio, Airplane airplain);

        public List<Station> GetStations();

        public List<Station> GetStationsWithAirplane();
    }
}
