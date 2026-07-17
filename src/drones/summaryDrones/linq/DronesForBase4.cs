using DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;

class DronesForBase4 : IIntLinq // Drone query by base
{
    public List<object> linqQuary(List<Drone> drones)
    {
        var results = drones.GroupBy(n => n.Base_location)
                                    .Select(g => new { Base = g.Key, Count = (double)g.Count() })
                                    .ToList<object>();
        return results;
    }
}