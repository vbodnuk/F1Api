namespace Formula1ApiConnection.Utils;

public class StringToIntParser
{
    public static int Parse(string value)
    {
        var s = int.TryParse(value, out var intValue);
        return intValue;
    }
}