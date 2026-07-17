using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.JsonSanitizer.JsonFieldsValidator.validators;

public class CustomeException : Exception//Custom error class
{
    public CustomeException(string message) : base(message)
    {
    }
}
