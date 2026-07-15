using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DroneFleetDataProcessing.src;
namespace DroneFleetDataProcessing.JsonSanitizer
{
    internal class JsonSanitizerPipline
    {
        static void Main()
        {
            FindPath pathfile = new FindPath();
            JsonValidator validate = new JsonValidator();
            string filePath = pathfile.GetPath("input", "test_scenarios", "drones_null.json");
            Console.WriteLine($"path is {filePath}");
            validate.FileIsExist(filePath);

            try
            {
                string back = File.ReadAllText(filePath);
                validate.FileIsEmpty(back);
                validate.FileIsNull(back);
                Console.WriteLine(back);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error caught: {ex.Message}");
            }
            var listi = new List<string>();
            string nn = "aaaaaa";
            listi.Add(nn);
            IWriter writer = new ToFile();
            string path1 = pathfile.GetPath("output", "out.json");
            Console.WriteLine(path1);
            writer.writeToFile(path1, listi);
        }
    }
}



