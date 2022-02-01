using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace AirportServer.Domain.Models
{
    public enum CurrentStation
    {
        NoStation, First, Second, Third, Forth, Fifth, Sixth, Seventh, Eighth
    };

    public class Station
    {
        public int Id { get; set; }
        public Airplane Airplain { get; set; }
        public ConcurrentQueue<Airplane> WaitingQ { get; set; }

        [NotMapped]
        public object Locker { get; set; }
        [NotMapped]
        public bool Avalable { get => Airplain == null; }


        public Station() { }

        public Station(int stationNum)
        {
            //Enum.Parse(typeof(CurrentStation), stationNum.ToString());
            Id = stationNum;
            Airplain = null;
            Locker = new object();
            WaitingQ = new ConcurrentQueue<Airplane>();
        }

        public void AddtoWaitingList(Airplane airplain)
        {
            WaitingQ.Enqueue(airplain);
        }

        public int GetHowMuchToWait()
        {
            return WaitingQ.Count;
        }
        
        public void StationMethod()
        {
            System.Diagnostics.Debug.WriteLine($"Airplane Number ---{Airplain.AirplainNumber}--- Entered To Station number ----{Id}");
            Thread.Sleep(6000);
            if (Id == 6)
                Thread.Sleep(1000);
            System.Diagnostics.Debug.WriteLine($"Airplane Number ---{Airplain.AirplainNumber}--- Finished his Method in Station number ----{Id}");
        }
    }




    //public int CompareTo(object obj)
    //{
    //    if (!obj.GetType().Equals(this.GetType()))
    //        throw new Exception("Not A Stations");
    //    Station station = (Station)obj;
    //    return this.StationNum > station.StationNum ? 1 : this.StationNum< station.StationNum? -1:0;
    //}

    //public int CompareTo(int num)
    //{
    //    return (int)this.StationNum > num ? 1 : (int)this.StationNum < num ? -1 : 0;
    //}

    //public Visit EnterStation(Airplain airplain, int CurrentStationWhenCalled)
    //{
    //    if (airplain.CurrentStation != CurrentStationWhenCalled)
    //        return null;

    //}
}

