using System;
namespace DroneFleetDataProcessing.JsonFieldsValidator;

interface IFieldsValidator //Normal Validity Check
{
    void RegularValidator(string field);
}
interface IFieldsUniqueValidator//Field Uniqueness Check
{
    void UniqueValidator(string field, List<string> strings);
}
interface IFieldsCertainRangeValidator//Range Validity Check
{
    void ertainRangeValidator(string field, double min, double max);
}
public class CustomeException : Exception//Custom error class
{
    public CustomeException(string message) : base(message)
    {
    }
}
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
class isUnique: IFieldsUniqueValidator//Check if the value is unique
{
    public void UniqueValidator(string field, List<string> strings)
    {
        if(strings.Contains(field))
        {
            throw new CustomeException($"The value is not unique.");
        }
    }
}
class EmptyOrSpaces: IFieldsValidator//Check if the value is not empty
{
    public void RegularValidator(string field)
    {
        if(field.Length == 0 || string.IsNullOrEmpty(field))
        {
            throw new CustomeException($"The value is empty.");
        }
    }
}
class serialNumberFormat: IFieldsValidator//Check if the value is in format
{
    public void RegularValidator(string field)
    {
        string[] parts = field.Split("-");
        if(parts[0] != "DR")
        {
            throw new CustomeException($"The value is Does not start with DR.");
        }
        if (!int.TryParse(parts[1], out var value) || parts[1].Length < 4)
        {
            throw new CustomeException($"The value is not in the required format.");
        }
    }
}
class isAertainWord : IFieldsUniqueValidator//Check for a valid value
{
    public void UniqueValidator(string field, List<string> strings)
    {
        if (!strings.Contains(field))
        {
            throw new CustomeException($"The value is not in the specific values.");
        }
    }
}
class isertainRange: IFieldsCertainRangeValidator//Check for the number is double and in a certain range
{
    public void ertainRangeValidator(string field, double min, double max)
    {
        if(!double.TryParse(field, out var value))
        {
            throw new CustomeException($"The value is not an decimal number.");
        }
        if (value < min)
        {
            throw new CustomeException($"The number is too small.");
        }
        else if(value > max)
        {
            throw new CustomeException($"The number is too large.");
        }
    }
}
class smallBatteryHealth//Check for low battery health status
{
    public void ValidsmallBatteryHealth(string batteryHealth, string status)
    {
        if(double.TryParse(batteryHealth, out double value) && value < 20 && status == "Operational")
        {
            throw new CustomeException($"The status cannot be Operational because the battery health is less than 20.");
        }
    }
}