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
            catch
            {
                validate.NoPermissionReade();
            }
            //End day




        }
    }
}



