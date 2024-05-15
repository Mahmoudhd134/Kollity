namespace Kollity.Reporting.Application.Extensions;

public static class NumberExtensions
{
    public static double ReplaceIfZero(this double number, double value)
    {
        return number == 0 ? value : number;
    }

    public static double ReplaceIfZero(this double? number, double value)
    {
        if (number is null)
            return value;

        return number == 0 ? value : number.Value;
    }

    public static int ReplaceIfZero(this int number, int value)
    {
        return number == 0 ? value : number;
    }

    public static int ReplaceIfZero(this int? number, int value)
    {
        if (number is null)
            return value;
        return number == 0 ? value : number.Value;
    }
}