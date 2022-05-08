using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ValheimMinus;

internal static class StringExtensions
{
    private static char[] Slash = { '/', '\\' };

    private static Regex Whitespace = new Regex(@"^\s*$", RegexOptions.Compiled);

    public static IEnumerable<string> SplitBySlash(this string value, bool toUpper = false)
        => value.SplitBy(Slash, toUpper).ToArray();

    private static IEnumerable<string> SplitBy(this string value, char[] chars, bool toUpper = false)
    {
        string[] split = value.Split(chars, StringSplitOptions.RemoveEmptyEntries);

        return (split?.Length ?? 0) == 0 ? Enumerable.Empty<string>() : split.Select(Clean);

        string Clean(string x)
        {
            string result = x.Trim();
            return toUpper ? result.ToUpperInvariant() : result;
        }
    }

    public static bool IsEmpty(this string input)
    {
        return input.Length == 0 || Whitespace.IsMatch(input);
    }
}