using AirportServer.Domain.Interfaces;
using AirportServer.Domain.Models;
using AirportServer.Infra.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AirportServer.Infra.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private object RepoLocker;
        private AirportContext _airportContext;


        public AirportRepository(AirportContext airportContext)
        {
            _airportContext = airportContext;
            RepoLocker = new object();
        }

        public void AddFlight(Airplane airplain)
        {
            airplain.Id = 0;
            lock (RepoLocker)
            {                
                _airportContext.Flights.Add(airplain);
                _airportContext.SaveChanges();
            }
        }

        public void AddStation(Station station)
        {
            lock (RepoLocker)
            {
                _airportContext.Stations.Add(new StationDto() { Airplain = station.Airplain, WaitingQ = station.WaitingQ.ToList() });
                _airportContext.SaveChanges();
            }
        }

        public void AddVisit(Visit visit)
        {
            lock (RepoLocker)
            {
                visit.Id = default;
                _airportContext.Visits.Add(visit);
                _airportContext.SaveChanges();
            }
        }

        public List<Airplane> GetPlannedFlights()
        {
            lock (RepoLocker)
            {
                return _airportContext.Flights.Where((a) => a.EnteredTheAirport == false).ToList();
            }
        }

        public void UpdateEnteredAirport(Airplane airplane)
        {
            lock (RepoLocker)
            {
                _airportContext.Flights.FirstOrDefault(f => f.AirplainNumber == airplane.AirplainNumber).EnteredTheAirport = true;
                _airportContext.SaveChanges();
            }
        }

        public void UpdateStationMovment(Station station, Airplane airplane)//if changing the airplane's current station to -1 its sigh that he flew away 
        {
            lock (RepoLocker)
            {
                _airportContext.Stations.FirstOrDefault((s) => s.Id == station.Id).Airplain = airplane;               
            }
        }

        public void UpdateMovmentInAirplane(int stationId , Airplane airplane)
        {
            lock(RepoLocker)
            {
                _airportContext.Flights.FirstOrDefault((a) => a.AirplainNumber == airplane.AirplainNumber).CurrentStation = stationId;
            }
        }

        public List<Station> GetStations()
        {
            lock (RepoLocker)
            {
                var stationsDto = _airportContext.Stations.Include(s => s.WaitingQ).ToList();
                List<Station> stations = new List<Station>();
                foreach (var station in stationsDto)
                {
                    stations.Add(station.DtoToNormal());
                }
                return stations;
            }
        }

        public List<Station> GetStationsWithAirplane()
        {
            lock (RepoLocker)
            {
                var stationsDto = _airportContext.Stations.Include(s => s.Airplain).ToList();
                List<Station> stations = new List<Station>();
                foreach (var station in stationsDto)
                {
                    stations.Add(station.DtoToNormal());
                }
                return stations;
            }
        }

    }
}
