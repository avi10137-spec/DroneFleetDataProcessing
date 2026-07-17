using DroneFleetDataProcessing.JsonSanitizer;
using DroneFleetDataProcessing.src.drones.summaryDrones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using DroneFleetDataProcessing.src.JsonSanitizer.JsonValidator;
using DroneFleetDataProcessing.src.drones.summaryDrones.summary_interfces;
using DroneFleetDataProcessing.src.drones.summaryDrones.linq;
namespace DroneFleetDataProcessing.src.drones
{
    class CreateDronePipline
    {
        public void dronePipeline()
        {
            string inputFile = "drones_clean.json";
            string inputPath = "output";
            string outputFile = "analysis_report.txt";
            string outputPath = "output";
            IFindPath foundPath = new FindPathInFile();
            JsonValidator validate = new JsonValidator();
            ILinqObjekt linqobjekt = new DronebyStatus();
            ILinqObjekt linqObjektTopHour = new DroneTopHour();
            IlinqString linkObjectUniqeModel = new UniqeModel();
            IlinqString linqTopAvgModel = new TopAvarageFlightHour();
            IIntLinq droneforbase4 = new DronesForBase4();
            HealthForModel5 healthformodel = new HealthForModel5();
            BestModel6 basemodel6 = new BestModel6();
            IOutputWriter consuleWrite = new ToTheTerminal();
            ToFileWithAppend writerstring = new ToFileWithAppend();
            IStringWriter writeToTxt = new ToFileWithAppend();
            Console.WriteLine($"Step 4: Reloading clean data {inputFile} Loaded records from clean dataset");
            string filePath = foundPath.FoundPath(inputFile, inputPath);
            string fileOutputPath = foundPath.FoundPath(outputFile, outputPath);
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

                consuleWrite.write($"Step 5: Performing analysis... Analysis completed successfully");

                var nonOperational = linqobjekt.ResultQuery(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nNON-OPERATIONAL DRONES");
                foreach (Drone drone in nonOperational)
                {
                    writeToTxt.writeToFile(fileOutputPath ,$"{drone.SerialNumber} | {drone.Model} | {drone.Base_location} | {drone.Status}");
                }

                var listTopHour = linqObjektTopHour.ResultQuery(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nTOP 5 DRONES BY FLIGHT HOURS");
                int numDroneToPrint = 1;
                foreach (Drone drone in listTopHour)
                {
                    writeToTxt.writeToFile(fileOutputPath, $"{numDroneToPrint}. {drone.SerialNumber} | {drone.Model} | {drone.FlightHours}");
                    numDroneToPrint ++;
                }

                var listmodel = linkObjectUniqeModel.ResultQuery(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nAVAILABLE DRONE MODELS");
                foreach (string model in listmodel)
                {
                    writeToTxt.writeToFile(fileOutputPath, model);
                }

                var droneforbase = droneforbase4.linqQuary(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nDRONES BY BASE");
                foreach (dynamic drone in droneforbase)
                {
                    writeToTxt.writeToFile(fileOutputPath, $"{drone.Base}: {drone.Count}");
                }

                var healthBattery = healthformodel.linqQuary(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nAVERAGE BATTERY HEALTH BY MODEL");
                foreach (var item in healthBattery)
                {
                    writeToTxt.writeToFile(fileOutputPath, $"{item.Key}: {item.Value:F2}");
                }

                var higgestModelWithComplectedMissions = basemodel6.linqQuary(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nMODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
                if (higgestModelWithComplectedMissions != null)
                {
                    writeToTxt.writeToFile(fileOutputPath, $"Model: {higgestModelWithComplectedMissions.Model}\nTotal completed missions: {higgestModelWithComplectedMissions.MissionsCompleted}");
                }

                var TopAvgModel = linqTopAvgModel.ResultQuery(listdrones);
                writeToTxt.writeToFile(fileOutputPath, "\nTOP 3 MODELS WITH HIGHEST AVERAGE FLIGHT HOURS");
                int numDroneToPrint2 = 1;
                foreach (string model in TopAvgModel)
                {
                    writeToTxt.writeToFile(fileOutputPath, $"{numDroneToPrint2}. {model}");
                }
                consuleWrite.write($"Step 6: Generating report... Report generated successfully: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }
            finally
            {
                consuleWrite.write("=== Process completed successfully! ===");
            }
        }
    }
}
