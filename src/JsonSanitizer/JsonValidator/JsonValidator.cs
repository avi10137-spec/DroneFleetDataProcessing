using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonValidator;

class JsonValidator // Jason health checks
{
    public void FileIsExist(string path) // Is Jason
    {
        if (!File.Exists(path))
        {
            throw new FileExption($"the {path} is not exist");
        }
    }
    public void FileIsEmpty(string back) // Is the file empty
    {
        if (back.Contains("[]"))
        {
            throw new FileExption($"json file is empty");
        }

    }
    public void FileIsNull(string back) // Is there a nul in Jason
    {
        if (back.Contains("null"))
        {
            throw new FileExption("json file is null");
        }
    }
    public void NoPermissionReade() // No permission to read
    {
        throw new FileExption("no permission to read file");
    }
    public void NoPermissionWrite() // No permission to write
    {
        throw new FileExption("no permission to write");
    }
}
