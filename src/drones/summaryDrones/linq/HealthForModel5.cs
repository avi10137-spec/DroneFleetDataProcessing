using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;
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