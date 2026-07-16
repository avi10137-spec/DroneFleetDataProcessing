using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.drones
{
     class Drone
    {
        public int Id { get; init; }
        public string SerialNumber { get; }
        public string Model { get; }
        public string Category { get; }
        public string Base_location { get; }
        public double FlightHours { get; set; }
        public int BatteryHealth { get; set; }
        public double MaxRangeKm { get; }
        public int MissionsCompleted { get; set; }
        public string Status { get; set; }
        public Drone(int id,string serialnumber,
                     string model,string category,string base_location,
                     double flighthours,int batteryhealth,
                     double maxrangekm,int missionscompleted,string status)
        {
            Id = id;
            SerialNumber = serialnumber;
            Model = model;
            Category = category;
            Base_location = base_location;
            FlightHours = flighthours;
            BatteryHealth = batteryhealth;
            MaxRangeKm = maxrangekm;
            MissionsCompleted = missionscompleted;
            Status = status;
            
        }
    }
}
