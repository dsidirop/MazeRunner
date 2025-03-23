using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using MazeRunner.Contracts;

namespace MazeRunner.Cli.Engine;

static internal class CommandUtilsX
{
    static internal IReadOnlyCollection<string> ParseEngineNames(this string engineNames, IEnginesFactory enginesFactory)
        => engineNames.Equals("all", StringComparison.InvariantCultureIgnoreCase) ? enginesFactory.EnginesNames : engineNames.Split([","], StringSplitOptions.RemoveEmptyEntries);

    static internal string FindParameter(this string[] args, string parameter)
        => Array.FindLast(args, a => parameter.EndsWith('=') ? a.StartsWith($"--{parameter}", StringComparison.InvariantCultureIgnoreCase) : a.Equals($"--{parameter}", StringComparison.InvariantCultureIgnoreCase));

    static internal int TryGetParameterValueInt(this string parameter)
    {
        return parameter.TryGetParameterValue(m =>
        {
            var value = 0;
            return new Tuple<bool, int>(m.Success && int.TryParse(m.Groups["Value"].Value, out value), value);
        });
    }

    static internal double TryGetParameterValueDouble(this string parameter)
    {
        return parameter.TryGetParameterValue(m =>
        {
            var value = 0D;
            return new Tuple<bool, double>(m.Success && double.TryParse(m.Groups["Value"].Value, NumberStyles.Float, CultureInfo.CreateSpecificCulture("en-GB"), out value), value);
        });
    }

    static internal string TryGetParameterValueString(this string parameter) => parameter.TryGetParameterValue(m => new Tuple<bool, string>(m.Success, m.Success ? m.Groups["Value"].Value : null));

    static internal T TryGetParameterValue<T>(this string parameter, Func<Match, Tuple<bool, T>> converter)
    {
        if (converter == null) throw new ArgumentNullException(nameof(converter));
        if (parameter == null) return default(T);

        var result = converter(SimpleValueParser.Match(parameter));
        if (!result.Item1) throw new InvalidCommandLineArgumentException($@"Malformed parameter: {parameter}");

        return result.Item2;
    }
    static private readonly Regex SimpleValueParser = new Regex("""^.*?[=]"?(?<Value>.+?)"?$""");
}