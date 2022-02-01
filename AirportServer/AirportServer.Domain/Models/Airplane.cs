using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Domain.Models
{
    public enum AirplaneState {Departure, Land };
    public class Airplane
    {
        public int Id { get; set; }
        public int AirplainNumber { get; set; }
        public AirplaneState AirplaneState { get; set; }
        public bool EnteredTheAirport { get; set; }
        public string Country { get; set; }
        public Visit CurrentVisit { get; set; }

        [NotMapped]
        public int CurrentStation { get; set; }
        [NotMapped]
        public GraphPath Path { get; set; }
        [NotMapped]
        private static readonly Random getrandom = new Random();
        [NotMapped]
        public static string[] States => new string[] { "London EGKK", "Geneva LSGG", "Lisbon LSGG", "Barcelona LEBL", "Napoli LIRN", "Paris LFPB", "Athena LGAV", "Strasbourg LFST" };


        public Airplane()
        {
            var idx = getrandom.Next(0, 7);
            Country = States[idx];
        }

        public Airplane(int flightNumber, int airplainState, GraphPath plainPath, bool needsEmergancyLanding = false)
        {
            AirplainNumber = flightNumber;
            CurrentStation = 0;
            //NeedsEmergancyLanding = needsEmergancyLanding;
            EnteredTheAirport = false;
            AirplaneState = (AirplaneState)Enum.ToObject(typeof(AirplaneState), airplainState);
            Path = plainPath;

            var idx = getrandom.Next(0, 7);
            Country = States[idx];
        }

        public void EnterAirport() => EnteredTheAirport = true;

        public bool IsInLastStation() => (Path.StationsGraph[CurrentStation].Count == 0);

        public int GetBestNextStationsId()
        {
            if (Path.StationsGraph[CurrentStation].Count == 1)
                return Path.StationsGraph[CurrentStation][0].Id;
            List<Station> nextStations = Path.StationsGraph[CurrentStation].ToList();
            foreach(var station in nextStations)
            {
                if (station.Avalable)
                    return station.Id;
            }
            int minWaiting = nextStations.Min(s => s.WaitingQ.Count);
            return nextStations.FirstOrDefault(s => s.WaitingQ.Count == minWaiting).Id;
        }


    }

}
