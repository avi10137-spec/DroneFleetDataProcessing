using DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones.summaryDrones.linq;
class UniqeModel : IlinqString // Presents existing model
{
    public List<string> ResultQuery(List<Drone> drones)
    {
        List<string> ListUniqemodel = new List<string>();
        ListUniqemodel = drones.Select(dr => dr.Model).Distinct().ToList();
        return ListUniqemodel;
    }
}
