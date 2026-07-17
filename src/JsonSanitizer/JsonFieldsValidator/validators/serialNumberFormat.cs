using DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.filelds_interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

class serialNumberFormat : IFieldsValidator//Check if the value is in format
{
    public void RegularValidator(string field)
    {
        string[] parts = field.Split("-");
        if (parts[0] != "DR")
        {
            throw new CustomeException($"The value is Does not start with DR.");
        }
        if (!int.TryParse(parts[1], out var value) || parts[1].Length < 4)
        {
            throw new CustomeException($"The value is not in the required format.");
        }
    }
}