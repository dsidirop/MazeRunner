#nullable enable

namespace MazeRunner.Utils;

static public class StringExtensions
{
    public static string? NullIfDud(this string? input)
    {
        return !string.IsNullOrWhiteSpace(input) ? input : null;
    }
}