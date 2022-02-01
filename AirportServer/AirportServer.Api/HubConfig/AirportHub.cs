using AirportServer.Api.Contracts;
using AirportServer.Api.Profiles;
using AirportServer.Domain.Interfaces;
using AirportServer.Domain.Models;
using AirportServer.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirportServer.Api.Hubs
{
    public class AirportHub : Hub
    {
        private readonly IAirportRepository _airportRepository;

        public AirportHub(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task ConnectAsync()
        {
            while (true)
            {
                await SendAirplanesOnTrack();
                await SendWaitingAirplanes();
                Thread.Sleep(1000);
            }
        }

        public async Task<Task> SendAirplanesOnTrack()
        {
            var stations = await Task.Run(() => _airportRepository.GetStationsWithAirplane());
            List<StationClientDto> stationsClient = new List<StationClientDto>();
            foreach (var station in stations)
            {
                stationsClient.Add(new StationClientDto(station));
            }
            stationsClient = stationsClient.OrderBy(x => x.Id).ToList();
            return Clients.All.SendAsync("AirplanesOnTrack", stationsClient);
        }

        public async Task<Task> SendWaitingAirplanes()
        {
            var airplanes = await Task.Run(() => _airportRepository.GetPlannedFlights());
            List<AirplaneDto> airplanesClient = new List<AirplaneDto>();
            foreach (var airplane in airplanes)
            {
                airplanesClient.Add(new AirplaneDto(airplane));
            }
            return Clients.All.SendAsync("WaitingAirplanes", airplanesClient);
        }
    }
}
