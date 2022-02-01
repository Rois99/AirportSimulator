using AirportServer.Domain.Interfaces;
using AirportServer.Domain.Models;
using AirportServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportController : ControllerBase
    {
        public IAirportLogic _logic;
        public IAirportRepository _airportRepository;

        public AirportController(IAirportLogic logic, IAirportRepository airportRepository)
        {
            _logic = logic;
            _airportRepository = airportRepository;
        }


        [HttpGet("Land/{id}")]
        public ActionResult AddLandingById(int id)
        {
            Console.WriteLine(id);
            Airplane airplain = new Airplane() { AirplainNumber = id, AirplaneState = AirplaneState.Land, EnteredTheAirport = false };
            _airportRepository.AddFlight(airplain);
            _logic.AddLanding(airplain);
            return Ok();
        }

        [HttpGet("Departure/{id}")]
        public void AddDeparturing(int id)
        {
            Console.WriteLine(id);
            Airplane airplain = new Airplane() { AirplainNumber = id, AirplaneState = AirplaneState.Departure, EnteredTheAirport = false };
            _airportRepository.AddFlight(airplain);
            _logic.AddDeparturing(airplain);
        }
    }
}
