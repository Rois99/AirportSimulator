using AirportServer.Domain.Interfaces;
using AirportServer.Domain.Models;
using AirportServer.Services.Interfaces;


namespace AirportServer.Services.Services
{
    public class AirportLogicService : IAirportLogic
    {
        public Airport _airport;


        public AirportLogicService(IAirportRepository airportRepository)
        {
            _airport = new Airport(airportRepository);
        }

        public void AddLanding(Airplane airplain)
        {
            airplain.Path = _airport.PathCreator.CreateLandingRoute();
            airplain.CurrentStation = 0;
            _airport.PlannedLandings.Enqueue(airplain);
        }

        public void AddDeparturing(Airplane airplain)
        {
            airplain.Path = _airport.PathCreator.CreateDeparturingRoute();
            _airport.PlannedDeparturing.Enqueue(airplain);
        }
    }
}
