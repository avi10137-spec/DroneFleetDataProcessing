using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;

interface IFieldsUniqueValidator//Field Uniqueness Check
{
    void UniqueValidator(string field, List<string> strings);
}
