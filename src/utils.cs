using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneFleetDataProcessing.src;
//class FindPath
//{
//    public string GetPath(params string[] pathSegment)
//    {
//        string baseDir = AppContext.BaseDirectory;
//        string[] allPaths = new[] { baseDir }.Concat(pathSegment).ToArray();
//        string filePath = Path.Combine(allPaths);
//        Console.WriteLine($"path is {filePath}");
//        return filePath;
//    }
//}

interface IWriter
{
    void writeToFile(string path, List<string> data);
}
interface IStringWriter
{
    void writeToFile(string path, string stringData);
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
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (var line in data)
            {
                writer.WriteLine(line);
            }
        }
    }
}
interface iFindPath
{
    string FoundPath(string filename, params string[] dir);
}
class FindPathInFile: iFindPath
{
    public string FoundPath(string filename, params string[] dir)
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        DirectoryInfo? projectDir = Directory.GetParent(baseDir).Parent.Parent.Parent;
        string FullPath = Path.Combine(projectDir.FullName, Path.Combine(dir), filename);
        return FullPath;
    }
}
class ToFileWithAppend :IStringWriter
{
    public void writeToFile(string path, string stringData)
    {
        string directoryPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        using (StreamWriter writer = new StreamWriter(path, append: true))
        {
            writer.WriteLine(stringData);
        }
    }
}