using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.JsonSanitizer
{    
    class FileExption : Exception
    {
        public FileExption (string massage) : base(massage) { }
       
    }
     class JsonValidator
    {
          public void FileIsExist(string path)
           {
            if (!File.Exists(path))
            {
                throw new FileExption($" the {path} is not exist ");
            }
           }
          public void FileIsEmpty(string back)
        {
            if (back.Contains("[]"))
            {
                throw new FileExption($"json file is empty");
            }
            
        }
        public void FileIsNull(string back)
        {
            if (back.Contains("null"))
            {
                throw new FileExption("json file is null");
            }
        }
        public void NoPermissionReade()
        {
            throw new FileExption("no permission to read file");
        }
        public void NoPermissionWrite()
        {
            throw new FileExption("no permission to write");
        }

    }
}
