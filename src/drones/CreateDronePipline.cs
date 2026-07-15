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
        public void dronePipeline()
        {
            //FindPath findpath = new FindPath();
            FindPathDudi foundPath = new FindPathDudi();
            JsonValidator validate = new JsonValidator();
            ILinqObjekt linqobjekt = new DronebyStatus();
            ILinqObjekt linqObjektTopHour = new DroneTopHour();
            IlinqString linkObjectUniqeModel = new UniqeModel();
            IlinqString linqTopAvgModel = new TopAvarageFlightHour();
            IIntLinq droneforbase4 = new DronesForBase4();
            HealthForModel5 healthformodel = new HealthForModel5();
            BestModel6 basemodel6 = new BestModel6();
            IWriter writer = new ToFile();
            ToFileWithAppend writerstring = new ToFileWithAppend();
            Console.WriteLine($"Step 4: Reloading clean data drones_cleam.json Loaded records from clean dataset");
            string filePath = foundPath.FoundPath("drones_cleam.json", "output");
            validate.FileIsExist(filePath);

            try
            {
                string back = File.ReadAllText(filePath);
                validate.FileIsEmpty(back);
                validate.FileIsNull(back);
               
                List<Drone> listdrones = JsonSerializer.Deserialize<List<Drone>>(back, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Drone>();
                Console.WriteLine($"Step 5: Performing analysis... Analysis completed successfully");
                linqobjekt.ResultQuery(listdrones);
                linqObjektTopHour.ResultQuery(listdrones);
                List <string> listmodel = linkObjectUniqeModel.ResultQuery(listdrones);
                linqTopAvgModel.ResultQuery(listdrones);
                droneforbase4.linqQuary(listdrones);
                healthformodel.linqQuary(listdrones);
                basemodel6.linqQuary(listdrones);
                writer.writeToFile("analise_report.txt", listmodel);

                

                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }


        }
    }
}
