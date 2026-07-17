using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

class isIntAndPositive : IFieldsValidator//Check if the number is integer and positive
{
    public void RegularValidator(string field)
    {
        if (!int.TryParse(field, out var value))
        {
            throw new CustomeException($"The value is not an integer.");
        }
        else if (value < 0)
        {
            throw new CustomeException($"The number is not an positive.");
        }
    }
}