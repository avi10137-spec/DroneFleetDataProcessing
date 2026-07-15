using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace DroneFleetDataProcessing.src.drones.summaryDrones
{
    interface ILinqObjekt
    {
         List<Drone> ResultQuery(List<Drone> drones); 
    }
    interface IlinqString
    {
        List<string> ResultQuery(List<Drone> drones);
    }
    class DronebyStatus : ILinqObjekt
    {
        public List<Drone> ResultQuery(List<Drone> drones)
        {
            List<Drone> listByStatus = new List<Drone>();
            listByStatus = drones.Where(dr => dr.Status != "Operational").ToList();
            return listByStatus;
            //Console.WriteLine("\n NON-OPERATIONAL DRONES");
            //foreach(Drone drone in listByStatus)
            //{
            //    Console.WriteLine($"{drone.SerialNumber} | {drone.Model} | {drone.Base_location} | {drone.Status}");
            //}
            
        }
    }
    class DroneTopHour : ILinqObjekt
    {
        public List<Drone> ResultQuery(List<Drone> drones)
        {
            List<Drone> listTopHour = new List<Drone>();
            listTopHour = drones.OrderByDescending(dr => dr.FlightHours).Take(5).ToList();
            return listTopHour;
            //Console.WriteLine("\nTOP 5 DRONES BY FLIGHT HOURS");
            //foreach (Drone drone in listTopHour)
            //{
            //    Console.WriteLine($"{drone.SerialNumber} | {drone.Model} | {drone.FlightHours}");
            //}
        }

    }
    class UniqeModel : IlinqString
    {
        public List<string> ResultQuery(List<Drone> drones)
        {
            List<string> ListUniqemodel = new List<string>();
            ListUniqemodel = drones.Select(dr => dr.Model).Distinct().ToList();
            return ListUniqemodel;
            //Console.WriteLine("\nAVAILABLE DRONE MODELS");
            //foreach (string model in ListUniqemodel)
            //{
            //    Console.WriteLine(model);
            //}


        }
    }
    class TopAvarageFlightHour : IlinqString
    {
        public List<string> ResultQuery(List<Drone> drones)
        {
            List<string> ListTopAvg = new List<string>();
            ListTopAvg = drones
                .GroupBy(dr => dr.Model) 
                .Select(g => new
                {
                    Model = g.Key,
                    AverageFlightHours = g.Average(dr => dr.FlightHours) 
                })
                .OrderByDescending(x => x.AverageFlightHours) 
                .Take(3).Select(dr => dr.Model)
                .ToList();
                 return ListTopAvg;
            //Console.WriteLine("\nMODELS WITH HIGHEST AVERAGE FLIGHT HOUR");
            //foreach(string model in ListTopAvg)
            //{
            //    Console.WriteLine(model);
            //}
                    }
         
         }





}
