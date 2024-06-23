namespace Kollity.Common.Extensions;

public static class StringExtensions
{
    public static string Combine(this string join, params string[] roles)
    {
        return string.Join(join, roles);
    }
}