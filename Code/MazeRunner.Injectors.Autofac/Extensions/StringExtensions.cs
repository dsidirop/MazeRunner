namespace MazeRunner.Injectors.Autofac.Extensions;

static internal class StringExtensions
{
    static internal string? NullIfDud(this string? input)
    {
        return !string.IsNullOrWhiteSpace(input) ? input : null;
    }
}