using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
interface IlinqString // Query interface returns a string
{
    List<string> ResultQuery(List<Drone> drones);
}
