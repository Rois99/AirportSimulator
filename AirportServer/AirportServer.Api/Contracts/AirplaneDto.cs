using AirportServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportServer.Api.Contracts
{
    public class AirplaneDto
    {
        public int Id { get; set; }
        public int AirplainNumber { get; set; }
        public int AirplaneState { get; set; }
        public bool EnteredTheAirport { get; set; }
        public string Country { get; set; }
        //private static readonly Random getrandom = new Random();
        //public static string[] States => new string[] { "London EGKK", "Geneva LSGG", "Lisbon LSGG", "Barcelona LEBL", "Napoli LIRN", "Paris LFPB", "Athena LGAV", "Strasbourg LFST" };


        public AirplaneDto(Airplane airplane)
        {
            if(airplane != null)
            {
                Id = airplane.Id;
                AirplainNumber = airplane.AirplainNumber;
                AirplaneState = (int)airplane.AirplaneState;
                EnteredTheAirport = airplane.EnteredTheAirport;
                Country = airplane.Country;
                //var idx = getrandom.Next(0, 7);
                //Country = States[idx];
            }
        }
    }
}
