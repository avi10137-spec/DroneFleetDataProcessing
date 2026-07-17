using DroneFleetDataProcessing.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;
namespace DroneFleetDataProcessing.JsonSanitizer
{
    internal class JsonSanitizerPipline
    {
        public void jsonPipeline()
        {
            const double minflightHours = 0;
            const double maxflightHours = 2500;
            const double minbatteryHealth = -1;
            const double maxbatteryHealth = 101;
            const double minMaxRangeKm = 1;
            const double maxMaxRangeKm = 150;
            const double minMissionsCompleted = 0;
            const double maxMissionsCompleted = 5000;

            int valid = 0;
            int inValid = 0;

            List<string> validDronesJson = new List<string>();
            List<string> listId = new List<string>();
            List<string> listserialNumber = new List<string>();

            List<string> model = new List<string>() { "Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite" };
            List<string> category = new List<string>() { "Recon", "Patrol", "Mapping", "Delivery", "Search", };
            List<string> baseLocation = new List<string>() { "North", "South", "Central", "East", "West", };
            List<string> status = new List<string>() { "Operational", "Maintenance", "Grounded", "Training" };

            IFindPath pathfile = new FindPathInFile();
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
            IStringWriter writeToTxt = new ToFileWithAppend();
            IOutputWriter consuleWrite = new ToTheTerminal();

            string inputFileName = "drones_raw.json";
            string outputFileName = "drones_clean.json";
            string outputTxtFile = "analysis_report.txt";
            string inputParentFolder = "input";
            string inputChildFolder = "raw";
            string outputFolder = "output";
            string filePath = pathfile.FoundPath(inputFileName, inputParentFolder, inputChildFolder);
            string outputPath = pathfile.FoundPath(outputFileName, outputFolder);
            string outputTxtPath = pathfile.FoundPath(outputTxtFile, outputFolder);
            validate.FileIsExist(filePath);
            try
            {
                consuleWrite.write("=== Drone Fleet Data Processing System ===");
                string back = File.ReadAllText(filePath);
                validate.FileIsEmpty(back);
                validate.FileIsNull(back);
                consuleWrite.write($"Step 1: Reading raw data {inputFileName} Read records from raw file");
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
                    consuleWrite.write($"Step 2: Validating data and creating clean dataset {outputFileName} Valid records: {valid} Rejected records: {inValid}");
                    writer.writeToFile(outputPath, validDronesJson);
                    consuleWrite.write($"Step 3: Saving clean data {outputFileName} Clean data saved to: {outputPath}");
                    writeToTxt.writeToFile(outputTxtPath, "DRONE FLEET ANALYSIS REPORT\n");
                    writeToTxt.writeToFile(outputTxtPath, $"PROCESSING SUMMARY\nTotal raw records: {valid+inValid}\nValid records: {valid}\nRejected records: {inValid}");
                }
                else
                    consuleWrite.write("There are no valid values.");
            }
            catch (Exception ex)
            {
                consuleWrite.write($"Error caught: {ex.Message}");
            }
        }
    }
}



