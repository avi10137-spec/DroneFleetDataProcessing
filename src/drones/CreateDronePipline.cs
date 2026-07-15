using DroneFleetDataProcessing.JsonSanitizer;
using DroneFleetDataProcessing.src.drones.summaryDrones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
namespace DroneFleetDataProcessing.src.drones
{
    class CreateDronePipline
    {
        static void Main()
        {
            //FindPath findpath = new FindPath();
            FindPathDudi foundPath = new FindPathDudi();
            JsonValidator validate = new JsonValidator();
            ILinqObjekt linqobjekt = new DronebyStatus();
            ILinqObjekt linqObjektTopHour = new DroneTopHour();
            ILinqObjekt linkObjectUniqeModel = new UniqeModel();
            ILinqObjekt linqTopAvgModel = new TopAvarageFlightHour();

            string filePath = foundPath.FoundPath("drones_cleam.json", "output");
            Console.WriteLine($"path is {filePath}");
            validate.FileIsExist(filePath);

            try
            {
                string back = File.ReadAllText(filePath);
                validate.FileIsEmpty(back);
                validate.FileIsNull(back);
                //Console.WriteLine(back);
                List<Drone> listdrones = JsonSerializer.Deserialize<List<Drone>>(back, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Drone>();
                //var id64 = listdrones.Where(r => r.Id == 64).Select(x => x.Model).ToList();
                //Console.WriteLine(id64);
                linqobjekt.ResultQuery(listdrones);
                linqObjektTopHour.ResultQuery(listdrones);
                linkObjectUniqeModel.ResultQuery(listdrones);
                linqTopAvgModel.ResultQuery(listdrones);
                

                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }


        }
    }
}
