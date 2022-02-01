using AirportServer.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportServer.Infra.Dto
{
    public class StationDto
    {
        public int Id { get; set; }
        public Airplane Airplain { get; set; }
        public List<Airplane> WaitingQ { get; set; }

        public Station DtoToNormal()
        {
            ConcurrentQueue<Airplane> newQ = new ConcurrentQueue<Airplane>();
            foreach(var airplane in WaitingQ)
            {
                newQ.Enqueue(airplane);
            }
            Station s = new Station(Id);
            s.Airplain = Airplain;
            s.WaitingQ = newQ;
            return s;
        }
    }
}
