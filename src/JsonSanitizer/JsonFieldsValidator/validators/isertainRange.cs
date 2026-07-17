using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

class isertainRange : IFieldsCertainRangeValidator//Check for the number is double and in a certain range
{
    public void ertainRangeValidator(string field, double min, double max)
    {
        if (!double.TryParse(field, out var value))
        {
            throw new CustomeException($"The value is not an decimal number.");
        }
        if (value < min)
        {
            throw new CustomeException($"The number is too small.");
        }
        else if (value > max)
        {
            throw new CustomeException($"The number is too large.");
        }
    }
}
