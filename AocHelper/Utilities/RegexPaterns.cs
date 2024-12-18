using System.Text.RegularExpressions;

namespace AocHelper.Utilities;

public static partial class RegexPaterns
{
    [GeneratedRegex(@"(\d+),(\d+)")]
    private static partial Regex VectorRegexFunc();
    public static readonly Regex VectorRegex = VectorRegexFunc();
}