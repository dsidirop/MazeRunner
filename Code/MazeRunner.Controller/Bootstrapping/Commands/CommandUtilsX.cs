using System;
using System.Text.RegularExpressions;

namespace MazeRunner.Controller.Bootstrapping.Commands
{
    static internal class CommandUtilsX
    {
        static internal string FindParameter(this string[] args, string parameter) => Array.FindLast(args, a => a.StartsWith($"--{parameter}", StringComparison.InvariantCultureIgnoreCase));

        static internal int TryGetParameterValueInt(this string parameter, bool isoptional = false)
        {
            return parameter.TryGetParameterValue(m =>
            {
                var value = 0;
                return new Tuple<bool, int>(m.Success && int.TryParse(m.Groups["Value"].Value, out value), value);
            }, isoptional);
        }

        static internal double TryGetParameterValueDouble(this string parameter)
        {
            return parameter.TryGetParameterValue(m =>
            {
                var value = 0D;
                return new Tuple<bool, double>(m.Success && double.TryParse(m.Groups["Value"].Value, out value), value);
            });
        }

        static internal string TryGetParameterValueString(this string parameter, bool isoptional = false) => parameter.TryGetParameterValue(m => new Tuple<bool, string>(m.Success, m.Success ? m.Groups["Value"].Value : null), isoptional);

        static internal T TryGetParameterValue<T>(this string parameter, Func<Match, Tuple<bool, T>> converter, bool isoptional = false)
        {
            if (converter == null) throw new ArgumentNullException(nameof(converter));

            var result = converter(SimpleValueParser.Match(parameter));
            if (!isoptional && !result.Item1)
            {
                Console.Error.WriteLine($@"Malformed parameter: {parameter}");
                Environment.Exit(1);
            }

            return result.Item2;
        }
        static private readonly Regex SimpleValueParser = new Regex(@"^.*?[=]""?(?<Value>.+>)""?$");
    }
}