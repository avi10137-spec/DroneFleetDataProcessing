using DroneFleetDataProcessing.JsonFieldsValidator;
using DroneFleetDataProcessing.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
namespace DroneFleetDataProcessing.JsonSanitizer
{
    internal class JsonSanitizerPipline
    {
        public void jsonPipeline()
        {
            double minflightHours = 0;
            double maxflightHours = 2500;
            double minbatteryHealth = -1;
            double maxbatteryHealth = 101;
            double minMaxRangeKm = 1;
            double maxMaxRangeKm = 150;
            double minMissionsCompleted = 0;
            double maxMissionsCompleted = 5000;
            int valid = 0;
            int inValid = 0;
            List<string> validDronesJson = new List<string>();
            List<string> listId = new List<string>();
            List<string> listserialNumber = new List<string>();
            List<string> model = new List<string>() { "Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite" };
            List<string> category = new List<string>() { "Recon", "Patrol", "Mapping", "Delivery", "Search", };
            List<string> baseLocation = new List<string>() { "North", "South", "Central", "East", "West", };
            List<string> status = new List<string>() { "Operational", "Maintenance", "Grounded", "Training" };
            FindPathDudi pathfile = new FindPathDudi();
            JsonValidator validate = new JsonValidator();
            CustomeException customeException = new CustomeException("invalid input");
            IFieldsValidator intAndPositive = new isIntAndPositive();
            IFieldsUniqueValidator isUnique = new isUnique();
            IFieldsValidator emptyOrSpace = new EmptyOrSpaces();
            IFieldsValidator format = new serialNumberFormat();
            IFieldsUniqueValidator fieldsUnique = new isAertainWord();
            IFieldsCertainRangeValidator isErtainRange = new isertainRange();
            smallBatteryHealth smallBatteryHealth = new smallBatteryHealth();
            IWriter writer = new ToFile();
            string inputFileName = "drones_raw.json";
            string outputFileName = "drones_cleam.json";
            string filePath = pathfile.FoundPath(inputFileName, "input", "raw");
            string outputPath = pathfile.FoundPath(outputFileName, "output");
            validate.FileIsExist(filePath);
            try
            {
                string back = File.ReadAllText(filePath);
                validate.FileIsEmpty(back);
                validate.FileIsNull(back);
                Console.WriteLine($"Step 1: Reading raw data {inputFileName} Read records from raw file");
                string jsonContent = File.ReadAllText(filePath);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    JsonElement root = doc.RootElement;
                    validDronesJson.Add("[");
                    foreach (JsonElement drone in root.EnumerateArray())
                    {
                        try
                        {
                            JsonElement idElement = drone.GetProperty("id");
                            intAndPositive.RegularValidator(idElement.ToString().Trim());
                            isUnique.UniqueValidator(idElement.ToString().Trim(), listId);
                            listId.Add(idElement.ToString().Trim());

                            JsonElement serialNumberElement = drone.GetProperty("serialNumber");
                            emptyOrSpace.RegularValidator(serialNumberElement.ToString().Trim());
                            isUnique.UniqueValidator(serialNumberElement.ToString().Trim(), listserialNumber);
                            listserialNumber.Add(serialNumberElement.ToString().Trim());
                            format.RegularValidator(serialNumberElement.ToString().Trim());

                            JsonElement modelElement = drone.GetProperty("model");
                            fieldsUnique.UniqueValidator(modelElement.ToString().Trim(), model);

                            JsonElement categoryElement = drone.GetProperty("category");
                            fieldsUnique.UniqueValidator(categoryElement.ToString().Trim(), category);

                            JsonElement locationElement = drone.GetProperty("base_location");
                            fieldsUnique.UniqueValidator(locationElement.ToString().Trim(), baseLocation);

                            JsonElement flightHoursElement = drone.GetProperty("flightHours");
                            isErtainRange.ertainRangeValidator(flightHoursElement.ToString().Trim(), minflightHours, maxflightHours);

                            JsonElement batteryHealthElement = drone.GetProperty("batteryHealth");
                            intAndPositive.RegularValidator(batteryHealthElement.ToString().Trim());
                            isErtainRange.ertainRangeValidator(batteryHealthElement.ToString().Trim(), minbatteryHealth, maxbatteryHealth);

                            JsonElement maxRangeKmElement = drone.GetProperty("maxRangeKm");
                            isErtainRange.ertainRangeValidator(maxRangeKmElement.ToString().Trim(), minMaxRangeKm, maxMaxRangeKm);

                            JsonElement missionsCompletedElement = drone.GetProperty("missionsCompleted");
                            intAndPositive.RegularValidator(missionsCompletedElement.ToString().Trim());
                            isErtainRange.ertainRangeValidator(missionsCompletedElement.ToString().Trim(), minMissionsCompleted, maxMissionsCompleted);
                            
                            JsonElement statusElement = drone.GetProperty("status");
                            fieldsUnique.UniqueValidator(statusElement.ToString().Trim(), status);
                            smallBatteryHealth.ValidsmallBatteryHealth(batteryHealthElement.ToString().Trim(), statusElement.ToString());

                            valid++;
                            validDronesJson.Add(drone.GetRawText());
                            validDronesJson.Add(",");

                        }
                        catch (Exception ex) 
                        {
                            inValid++;
                            //Console.WriteLine($"Erorr caught: {ex.Message}: {drone.GetProperty("id")}");
                        }
                        
                    }
                }
                if(validDronesJson.Count > 1)
                {
                    validDronesJson.RemoveAt(validDronesJson.Count -1);
                    validDronesJson.Add("]");
                    Console.WriteLine($"Step 2: Validating data and creating clean dataset 'validDronesJson' Valid records: {valid} Rejected records: {inValid}");
                    writer.writeToFile(outputPath, validDronesJson);
                    Console.WriteLine($"Step 3: Saving clean data {outputFileName} Clean data saved to: {outputPath}");
                }
                else
                Console.WriteLine("There are no valid values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }
            
        }
    }
}



