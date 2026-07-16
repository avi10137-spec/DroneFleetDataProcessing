using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneFleetDataProcessing.src.drones.summaryDrones;
interface IIntLinq // Query interface
{
    List<object> linqQuary(List<Drone> drones);
}
class DronesForBase4: IIntLinq // Drone query by base
{
    public List<object> linqQuary(List<Drone> drones)
    {
        var results = drones.GroupBy(n => n.Base_location)
                                    .Select(g => new { Base = g.Key, Count = (double)g.Count() })
                                    .ToList<object>();
        return results;
    }
}
class HealthForModel5 // Average battery health for each model
{
    public Dictionary<string, double> linqQuary(List<Drone> drones)
    {
        return drones
            .GroupBy(d => d.Model)
            .ToDictionary(
                g => g.Key ?? "Unknown",
                g => g.Average(d => (double)d.BatteryHealth)
            );
    }
}
class BestModel6 // The best model by tasks
{
    public Drone? linqQuary(List<Drone> drones)
    {
        var BestModel = drones.OrderByDescending(n => n.MissionsCompleted).FirstOrDefault();
        return BestModel;
    }
}