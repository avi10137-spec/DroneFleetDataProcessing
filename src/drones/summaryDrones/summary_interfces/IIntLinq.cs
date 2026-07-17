using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;

interface IIntLinq // Query interface
{
    List<object> linqQuary(List<Drone> drones);
}