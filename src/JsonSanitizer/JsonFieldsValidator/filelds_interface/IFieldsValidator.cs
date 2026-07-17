using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
interface IFieldsValidator //Normal Validity Check
{
    void RegularValidator(string field);
}