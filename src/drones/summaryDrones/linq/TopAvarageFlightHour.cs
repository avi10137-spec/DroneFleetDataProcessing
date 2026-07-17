using DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;
class TopAvarageFlightHour : IlinqString // The 3 drones with the highest average hourly rate - bonus
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
    }
}
