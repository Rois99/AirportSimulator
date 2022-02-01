using AirportServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Infra.Dto
{
    public class AirplaneDto
    {
        public int Id { get; set; }
        public int AirplainNumber { get; set; }
        public AirplaneState AirplaneState { get; set; }
        public bool EnteredTheAirport { get; set; }
    }
}
