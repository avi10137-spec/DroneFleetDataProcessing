using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
interface IWriter
{
    void writeToFile(string path, List<string> data);
}
class ToFile : IWriter
{
    public void writeToFile(string path, List<string> data)
    {
        string directoryPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        using (StreamWriter writer = new StreamWriter(path, append: true))
        {
            foreach (var line in data)
            {
                writer.WriteLine(line);
            }
        }
    }
}

