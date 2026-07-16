using System;
using System.IO;
namespace program;

using DroneFleetDataProcessing.JsonSanitizer;
using DroneFleetDataProcessing.src.drones;
using System.Text.Json;

class ProgramEx
{
    static void Main()
    {
        JsonSanitizerPipline jsonSanitizer = new JsonSanitizerPipline();
        jsonSanitizer.jsonPipeline();
        CreateDronePipline dronePipline = new CreateDronePipline();
        dronePipline.dronePipeline();
    }

}

