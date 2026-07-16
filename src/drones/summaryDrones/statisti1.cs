using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace DroneFleetDataProcessing.src.drones.summaryDrones;

    interface ILinqObjekt // Query interface returns a drone
{
    List<Drone> ResultQuery(List<Drone> drones);
}
interface IlinqString // Query interface returns a string
{
    List<string> ResultQuery(List<Drone> drones);
}
class DronebyStatus : ILinqObjekt // Number of drones that are not operational
{
    public List<Drone> ResultQuery(List<Drone> drones)
    {
        List<Drone> listByStatus = new List<Drone>();
        listByStatus = drones.Where(dr => dr.Status != "Operational").ToList();
        return listByStatus;
    }
}
class DroneTopHour : ILinqObjekt // The 5 drones with the highest flight hours
{
    public List<Drone> ResultQuery(List<Drone> drones)
    {
        List<Drone> listTopHour = new List<Drone>();
        listTopHour = drones.OrderByDescending(dr => dr.FlightHours).Take(5).ToList();
        return listTopHour;
    }

}
class UniqeModel : IlinqString // Presents existing model
{
    public List<string> ResultQuery(List<Drone> drones)
    {
        List<string> ListUniqemodel = new List<string>();
        ListUniqemodel = drones.Select(dr => dr.Model).Distinct().ToList();
        return ListUniqemodel;
    }
}
class TopAvarageFlightHour : IlinqString // The 3 drones with the highest average hourly rate
{
    public List<string> ResultQuery(List<Drone> drones)
    {
        List<string> ListTopAvg = new List<string>();
        ListTopAvg = drones
            .GroupBy(dr => dr.Model)
            .Select(g => new
            {
                Model = g.Key,
                AverageFlightHours = g.Average(dr => dr.FlightHours)
            })
            .OrderByDescending(x => x.AverageFlightHours)
            .Take(3).Select(dr => dr.Model)
            .ToList();
        return ListTopAvg;
    }
}


