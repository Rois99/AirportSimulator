using AirportServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Services.Interfaces
{
    public interface IAirportLogic
    {
        public void AddLanding(Airplane airplain);

        public void AddDeparturing(Airplane airplain);
    }
}

