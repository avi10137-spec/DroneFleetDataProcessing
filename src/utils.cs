using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src
{
    class FindPath
    {
        public string GetPath(params string[] pathSegment)
        {
          string baseDir = AppContext.BaseDirectory;
           string[] allPaths = new[] { baseDir }.Concat(pathSegment).ToArray();
           string filePath = Path.Combine(allPaths);
           Console.WriteLine($"path is {filePath}");
            return filePath;
        }
    }
}
