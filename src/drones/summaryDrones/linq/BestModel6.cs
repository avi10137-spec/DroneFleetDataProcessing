using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;
class BestModel6 // The best model by tasks
{
    public Drone? linqQuary(List<Drone> drones)
    {
        var BestModel = drones.OrderByDescending(n => n.MissionsCompleted).FirstOrDefault();
        return BestModel;
    }
}
