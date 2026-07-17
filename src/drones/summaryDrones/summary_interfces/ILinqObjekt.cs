using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
interface ILinqObjekt // Query interface returns a drone
{
    List<Drone> ResultQuery(List<Drone> drones);
}