using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
interface IFieldsCertainRangeValidator//Range Validity Check
{
    void ertainRangeValidator(string field, double min, double max);
}
