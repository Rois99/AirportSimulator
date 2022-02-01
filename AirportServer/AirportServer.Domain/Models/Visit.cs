using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Domain.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int StationNumber { get; set; }
        public int PlainID { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }


        public Visit(int stationNumber, int plainID)
        {
            StationNumber = stationNumber;
            PlainID = plainID;
            EnterTime = DateTime.Now;
        }
    }
}
