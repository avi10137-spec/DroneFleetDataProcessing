using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using DroneFleetDataProcessing.JsonSanitizer;
namespace DroneFleetDataProcessing.src.drones
{
    class CreateDronePipline
    {
   static void Main()
        {
            FindPath findpath = new FindPath();
            JsonValidator validate = new JsonValidator();
            string filePath = findpath.GetPath("output","out.json");
            Console.WriteLine($"path is {filePath}");
            validate.FileIsExist(filePath);

            try
            {
                string back = File.ReadAllText(filePath);
                validate.FileIsEmpty(back);
                validate.FileIsNull(back);
                Console.WriteLine(back);
                List<Drone> listdrones = JsonSerializer.Deserialize<List<Drone>>(back, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                }) ?? new List<Drone>();
                var id64 = listdrones.Where(r => r.Id == 64).Select(x => x.Model).ToList();
                Console.WriteLine(id64);
                foreach (Drone drone in listdrones)
                {
                    Console.WriteLine(drone);
                    Console.WriteLine(drone.Id);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }     


        }
    }
}
