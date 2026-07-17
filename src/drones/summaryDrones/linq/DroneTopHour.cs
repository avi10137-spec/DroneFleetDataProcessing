using DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;

class DroneTopHour : ILinqObjekt // The 5 drones with the highest flight hours
{
    public List<Drone> ResultQuery(List<Drone> drones)
    {
        List<Drone> listTopHour = new List<Drone>();
        listTopHour = drones.OrderByDescending(dr => dr.FlightHours).Take(5).ToList();
        return listTopHour;
    }
}