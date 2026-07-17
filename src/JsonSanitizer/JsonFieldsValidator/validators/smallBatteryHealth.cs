using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

class smallBatteryHealth//Check for low battery health status
{
    public void ValidsmallBatteryHealth(string batteryHealth, string status)
    {
        if (double.TryParse(batteryHealth, out double value) && value < 20 && status == "Operational")
        {
            throw new CustomeException($"The status cannot be Operational because the battery health is less than 20.");
        }
    }
}
