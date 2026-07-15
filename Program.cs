using System;
using System.IO;
namespace program;
using DroneFleetDataProcessing.JsonSanitizer;
using System.Text.Json;

class ProgramEx
{
    static void Main()
    {
        JsonSanitizerPipline jsonSanitizer = new JsonSanitizerPipline();
        jsonSanitizer.jsonPipeline();
    }
    
}

