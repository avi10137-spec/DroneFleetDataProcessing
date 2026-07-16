using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneFleetDataProcessing.src;
interface IWriter // List Writing Interface
{
    void writeToFile(string path, List<string> data);
}
interface IStringWriter // String Writing Interface
{
    void writeToFile(string path, string stringData);
}
interface IOutputWriter // Terminal writing interface
{
    void write(string stringData);
}
interface IFindPath // Pathfinding Interface
{
    string FoundPath(string filename, params string[] dir);
}
class ToFile : IWriter // Writing to a file
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
class FindPathInFile: IFindPath // Pathfinding
{
    public string FoundPath(string filename, params string[] dir)
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        DirectoryInfo? projectDir = Directory.GetParent(baseDir).Parent.Parent.Parent;
        string FullPath = Path.Combine(projectDir.FullName, Path.Combine(dir), filename);
        return FullPath;
    }
}
class ToFileWithAppend : IStringWriter // Writing to a file is being updated.
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
class ToTheTerminal: IOutputWriter //write to terminal
{
    public void write(string stringData)
    {
        Console.WriteLine(stringData);
    }
}