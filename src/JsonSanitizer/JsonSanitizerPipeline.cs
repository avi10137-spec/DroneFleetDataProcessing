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
            double minbatteryHealth = 0;
            double maxbatteryHealth = 100;
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
                            intAndPositive.RegularValidator(idElement.ToString());
                            isUnique.UniqueValidator(idElement.ToString(), listId);
                            listId.Add(idElement.ToString());

                            JsonElement serialNumberElement = drone.GetProperty("serialNumber");
                            emptyOrSpace.RegularValidator(serialNumberElement.ToString());
                            isUnique.UniqueValidator(serialNumberElement.ToString(), listserialNumber);
                            listserialNumber.Add(serialNumberElement.ToString());
                            format.RegularValidator(serialNumberElement.ToString());

                            JsonElement modelElement = drone.GetProperty("model");
                            fieldsUnique.UniqueValidator(modelElement.ToString(), model);

                            JsonElement categoryElement = drone.GetProperty("category");
                            fieldsUnique.UniqueValidator(categoryElement.ToString(), category);

                            JsonElement locationElement = drone.GetProperty("base_location");
                            fieldsUnique.UniqueValidator(locationElement.ToString(), baseLocation);

                            JsonElement flightHoursElement = drone.GetProperty("flightHours");
                            isErtainRange.ertainRangeValidator(flightHoursElement.ToString(), minflightHours, maxflightHours);

                            JsonElement batteryHealthElement = drone.GetProperty("batteryHealth");
                            intAndPositive.RegularValidator(batteryHealthElement.ToString());
                            isErtainRange.ertainRangeValidator(batteryHealthElement.ToString(), minbatteryHealth, maxbatteryHealth);

                            JsonElement maxRangeKmElement = drone.GetProperty("maxRangeKm");
                            isErtainRange.ertainRangeValidator(maxRangeKmElement.ToString(), minMaxRangeKm, maxMaxRangeKm);

                            JsonElement missionsCompletedElement = drone.GetProperty("missionsCompleted");
                            intAndPositive.RegularValidator(missionsCompletedElement.ToString());
                            isErtainRange.ertainRangeValidator(missionsCompletedElement.ToString(), minMissionsCompleted, maxMissionsCompleted);
                            
                            JsonElement statusElement = drone.GetProperty("status");
                            fieldsUnique.UniqueValidator(statusElement.ToString(), status);

                            valid++;
                            validDronesJson.Add(drone.GetRawText());

                        }
                        catch (Exception ex) 
                        {
                            inValid++;
                            //Console.WriteLine($"Erorr caught: {ex.Message}");
                        }
                        
                    }
                }
                if(validDronesJson.Count > 1)
                {
                    validDronesJson.Add("]");
                    Console.WriteLine($"Step 2: Validating data and creating clean dataset 'validDronesJson' Valid records: {valid} Rejected records: {inValid}");
                    writer.writeToFile(outputPath, validDronesJson);
                    Console.WriteLine($"Step 3: Saving clean data {outputFileName} Clean data saved to: ");
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



