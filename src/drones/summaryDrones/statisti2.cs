using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneFleetDataProcessing.src.drones.summaryDrones;
interface IIntLinq
{
    List<object> linqQuary(List<Drone> drones);
}
class DronesForBase4: IIntLinq
{
    public List<object> linqQuary(List<Drone> drones)
    {
        var results = drones.GroupBy(n => n.Base_location)
                                    .Select(g => new { Base = g.Key, Count = (double)g.Count() })
                                    .ToList<object>();
        return results;
        //Console.WriteLine("\nDRONES BY BASE");
        //foreach (var drone in dronesByBase)
        //{
        //    Console.WriteLine($"{drone.Key}: {drone.Value}");
        //}
    }
}
class HealthForModel5
{
    public object linqQuary(List<Drone> drones)
    {
        var healthByModel = drones.GroupBy(n => n.Model).Select(g => new { model = g.Key, count = g.Count()}).OrderByDescending(g => g.count).FirstOrDefault();
        //Console.WriteLine($"Model: {healthByModel.model}\nCount: {healthByModel.Count}");
        return healthByModel;
    }
}
class BestModel6
{
    public object linqQuary(List<Drone> drones)
    {
        var BestModel = drones.OrderByDescending(n => n.MissionsCompleted).FirstOrDefault();
        return BestModel;
        //Console.WriteLine("\nMODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
        //Console.WriteLine($"Model: {BestModel.Model}\nTotal completed missions: {BestModel.MissionsCompleted}");
    }
}