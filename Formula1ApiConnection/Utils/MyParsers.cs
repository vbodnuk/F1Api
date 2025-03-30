namespace Formula1ApiConnection.Utils;

public sealed class MyParsers
{
    public static int StringToIntParser(string value)
    {
        var s = int.TryParse(value, out var intValue);
        return intValue;
    }

    public static DateTime StringToDateTimeParser(string value)
    {
        var s = DateTime.TryParse(value, out var dateTime);
        return dateTime.Date;
    }
    
}