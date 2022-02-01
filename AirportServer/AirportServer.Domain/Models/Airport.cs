using AirportServer.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportServer.Domain.Models
{
    public class Airport
    {
        public ConcurrentQueue<Airplane> PlannedLandings { get; set; }
        public ConcurrentQueue<Airplane> PlannedDeparturing { get; set; }
        public List<Station> Stations;
        public GraphPath PathCreator;
        private IAirportRepository _airportRepo;


        public Airport(IAirportRepository airportRepository)//מזריק את הרפו שומר לתוכו דברים
        {
            _airportRepo = airportRepository;
            InitializeStations();
            PathCreator = new GraphPath(Stations);
            InitializePlannedFlights();
            Task.Run(() => StartAirport());
        }

        private void InitializeStations()
        {
            Stations = _airportRepo.GetStations();
            if (Stations.Count == 0)//במידה ודאטא בייס לא מכיל את הסטיישינים יוצר חדשים
                EnterBaseStations();
        }

        public void InitializePlannedFlights()
        {
            List<Airplane> airplanes = _airportRepo.GetPlannedFlights();
            foreach (var airplane in airplanes)
            {
                if (airplane.AirplaneState == 0)
                    airplane.Path = PathCreator.CreateDeparturingRoute();
                else
                    airplane.Path = PathCreator.CreateLandingRoute();
            }
            PlannedDeparturing = new ConcurrentQueue<Airplane>();
            PlannedLandings = new ConcurrentQueue<Airplane>();
            if (!(airplanes is null || airplanes.Count == 0))
            {
                foreach (var airplane in airplanes)
                {
                    if (airplane.AirplaneState == 0)
                        PlannedDeparturing.Enqueue(airplane);
                    else
                        PlannedLandings.Enqueue(airplane);
                }
            }
        }

        public void StartAirport()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    TryEnterPlannedFlightsToAirport();
                }
            });

            Task.Run(async () =>
              {
                  while (true)
                  {
                      foreach (var station in Stations)
                      {
                          var stat = station;
                          if (station.Avalable && station.WaitingQ.Count != 0)
                          {
                              station.WaitingQ.TryPeek(out Airplane newAirplane);
                              _= Task.Run(() => 
                                {
                                    TryEnterToStation(stat, newAirplane);
                                });
                          }
                      }
                      await Task.Delay(1000);
                  }
              });
        }

        private void TryEnterPlannedFlightsToAirport()
        {
            if (PlannedLandings.Count != 0)
            {
                if (!PlannedLandings.TryPeek(out Airplane landing))
                    return;
                var station = Stations.FirstOrDefault(s => s.Id == landing.GetBestNextStationsId());
                if (station.Avalable)
                {
                    PlannedLandings.TryDequeue(out Airplane airplane);
                    airplane.EnterAirport();
                    _airportRepo.UpdateEnteredAirport(airplane);
                    station.AddtoWaitingList(landing);
                }
            }

            if (PlannedDeparturing.Count != 0)
            {
                if (!PlannedDeparturing.TryPeek(out Airplane departure))
                    return;
                var station = Stations[departure.GetBestNextStationsId()];
                if (station.Avalable)
                {
                    PlannedDeparturing.TryDequeue(out Airplane a);
                    departure.EnterAirport();
                    _airportRepo.UpdateEnteredAirport(departure);
                    station.AddtoWaitingList(departure);
                }
            }
        }

        private void TryEnterToStation(Station station, Airplane newAirplane)
        {
            lock (station.Locker)
            {
                if (!(station.WaitingQ.TryDequeue(out Airplane nextAirplane) && nextAirplane.Id == newAirplane.Id && station.Avalable))
                    return;

                ExitPrevStationIfExists(nextAirplane);
                Visit currentVisit;

                //register the new airplain into the station
                currentVisit = new Visit(station.Id, nextAirplane.AirplainNumber);
                nextAirplane.CurrentStation = station.Id;//updates the station in the airplane model
                station.Airplain = nextAirplane;
                _airportRepo.UpdateStationMovment(station, newAirplane);
                _airportRepo.UpdateMovmentInAirplane(station.Id, newAirplane);

                station.StationMethod();//do what need to be done in the station

                //when ended station method
                if (nextAirplane.IsInLastStation())
                {
                    station.Airplain = null;
                    _airportRepo.UpdateStationMovment(station, null);
                    _airportRepo.UpdateMovmentInAirplane(100, nextAirplane);
                    currentVisit.ExitTime = DateTime.Now;
                    _airportRepo.AddVisit(currentVisit);
                    System.Diagnostics.Debug.WriteLine($"Airplane Number {nextAirplane.AirplainNumber} Flew ayay");
                    return;
                }
                nextAirplane.CurrentVisit = currentVisit;
                Station nextStation = Stations.FirstOrDefault(s => s.Id == nextAirplane.GetBestNextStationsId());
                nextStation.AddtoWaitingList(nextAirplane);//add the airplane to the next station's waiting list
            }
        }

        private void ExitPrevStationIfExists(Airplane nextAirplane)
        {
            if (nextAirplane.CurrentVisit == null)//if its his first station
                return;
            Visit prevVisit;
            GetStationByID(nextAirplane.CurrentStation).Airplain = null;//Cleans The previous station
            _airportRepo.UpdateStationMovment(GetStationByID(nextAirplane.CurrentStation), null);
            System.Diagnostics.Debug.WriteLine($"Airplane Number ---{nextAirplane.AirplainNumber}---Officialy left station ----{nextAirplane.CurrentStation}");
            prevVisit = nextAirplane.CurrentVisit;
            prevVisit.ExitTime = DateTime.Now;
            _airportRepo.AddVisit(prevVisit);
        }

        public Station GetStationByID(int id)
        {
            return Stations.FirstOrDefault((s) => s.Id == id);
        }

        private void EnterBaseStations()
        {
            for (int i = 1; i < 9; i++)
            {
                var station = new Station(i);
                Stations.Add(station);
                _airportRepo.AddStation(station);
            }
        }
    }
}
