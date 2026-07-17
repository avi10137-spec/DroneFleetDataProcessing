using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

class EmptyOrSpaces : IFieldsValidator//Check if the value is not empty
{
    public void RegularValidator(string field)
    {
        if (field.Length == 0 || string.IsNullOrEmpty(field))
        {
            throw new CustomeException($"The value is empty.");
        }
    }
}
