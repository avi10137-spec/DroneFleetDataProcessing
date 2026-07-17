using DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;
class DronebyStatus : ILinqObjekt // Number of drones that are not operational
{
    public List<Drone> ResultQuery(List<Drone> drones)
    {
        List<Drone> listByStatus = new List<Drone>();
        listByStatus = drones.Where(dr => dr.Status != "Operational").ToList();
        return listByStatus;
    }
}
