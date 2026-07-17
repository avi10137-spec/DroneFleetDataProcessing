using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

class isUnique : IFieldsUniqueValidator//Check if the value is unique
{
    public void UniqueValidator(string field, List<string> strings)
    {
        if (strings.Contains(field))
        {
            throw new CustomeException($"The value is not unique.");
        }
    }
}
