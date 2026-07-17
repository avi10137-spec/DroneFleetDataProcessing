using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonValidator;
class FileExption : Exception
{
    public FileExption(string massage) : base(massage) { }
}
