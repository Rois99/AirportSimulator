using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Domain.Models
{
    public class GraphPath
    {
        public List<Station> AirportStations { get; set; }
        public List<Station>[] StationsGraph { get; set; }
        public int CurrentStation { get; set;}


        public GraphPath(List<Station> airportStations)
        {
            CurrentStation = 0;
            StationsGraph = new List<Station>[airportStations.Count()+1];
            AirportStations = airportStations;
            for (int i = 0; i <= airportStations.Count(); i++)
            {
                StationsGraph[i] = new List<Station>();
            }
        }

        public GraphPath CreateLandingRoute()
        {
            GraphPath landingPath = new GraphPath(AirportStations);
            landingPath.Add(0, 1);
            landingPath.Add(1, 2);
            landingPath.Add(2, 3);
            landingPath.Add(3, 4);
            landingPath.Add(4, 5);
            landingPath.Add(5, 6);
            landingPath.Add(5, 7);
            return landingPath;
        }

        public GraphPath CreateDeparturingRoute()
        {
            GraphPath departuringPath = new GraphPath(AirportStations);
            departuringPath.Add(0, 6);
            departuringPath.Add(0, 7);
            departuringPath.Add(6, 8);
            departuringPath.Add(7, 8);
            departuringPath.Add(8, 4);
            return departuringPath;
        }

        public void Add(int parrentS, int childS)
        {
            this.StationsGraph[parrentS].Add(AirportStations.FirstOrDefault(s => s.Id == childS));
        }

        public bool IsLast()
        {
            return true;
        }
    }
}
