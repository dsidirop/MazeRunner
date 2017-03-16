using MazeRunner.Shared.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MazeRunner.Shared.Helpers
{
    static public class U //utilities
    {
        static public readonly string ProductInstallationFolderpath_system;
        static U()
        {
            var assembly = Assembly.GetExecutingAssembly(); //tried application.startuppath but it didnt work
            var assemblyFilepath = assembly.Location;

            ProductInstallationFolderpath_system = Path.GetDirectoryName(assemblyFilepath);
        }

        static public string GetEngineName(this IMazeRunnerEngine engine) => engine?.GetType().Name;
        static public MazeSpecs GetMazeSpecs(this IMaze maze) => new MazeSpecs { Height = maze.Size.Height, Width = maze.Size.Width, RoadblockDensity = maze.RoadblocksCount / (((double)maze.Size.Width) * maze.Size.Height) };

        static private readonly Random RandomNumbersEngine = new Random();
        static public ReorderableDictionary<int, int> GenerateRandomNumbersWithoutDuplicates(int count, int min, int maxExclusive) //max is exclusive here
        {
            if (maxExclusive <= min || count < 0 || (count > maxExclusive - min && maxExclusive - min > 0)) throw new ArgumentOutOfRangeException($"Range {min} to {maxExclusive} ({maxExclusive - (long) min} values) or count {count} is illegal"); //need to use 64bit to support big ranges negative min positive max
            
            var candidates = new ReorderableDictionary<int, int>(); //start count values before max and end at max
            for (var top = maxExclusive - count; top < maxExclusive; top++)
            {
                var random = RandomNumbersEngine.Next(min, top + 1);
                if (!candidates.Contains(random)) // May strike a duplicate  Need to add +1 to make inclusive generator  +1 is safe even for MaxVal max value because top < max
                {
                    candidates.Add(random, random);
                    continue;
                }

                candidates.Add(top, top); // collision add inclusive max which could not possibly have been added before
            }
            return candidates;
        }
        //  initialize set S to empty
        //  for J := N-M + 1 to N do
        //    T := RandInt(1, J)
        //    if T is not in S then
        //      insert T in S
        //    else
        //      insert J in S
        //
        // adapted for C# which does not have an inclusive Next(..) and to make it from configurable range not just 1

        static public IList<T> Shuffle<T>(this IList<T> list) //fisheryates
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = RandomNumbersEngine.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        static public List<Point?> GetAdjacentPoints(this Point p) => Offsets.Select(i => new Point(p.X - i.X, p.Y - i.Y)).Cast<Point?>().ToList();
        static private readonly List<Point> Offsets = new List<Point> {new Point(x: 0, y: -1), new Point(x: 1, y: 0), new Point(x: 0, y: 1), new Point(x: -1, y: 0)};

        static public string NormalizeNewlines(this string input, string newlineToUse) => Regex.Replace(input, @"\r\n|\n\r|\n|\r", newlineToUse); //the order \r\n|\n\r|\n|\r is important

        static public string ToAsciiMap(this IMaze maze, Func<Point, char?> freepointEvaluator = null, string linesSeparator = null)
        {
            linesSeparator = linesSeparator ?? nl;

            var sb = new StringBuilder();
            for (var y = 0; y < maze.Size.Height; y++)
            {
                for (var x = 0; x < maze.Size.Width; x++)
                {
                    var p = new Point(x, y);
                    var hittest = maze.HitTest(p);
                    if (hittest == MazeHitTestEnum.Free)
                    {
                        sb.Append(freepointEvaluator?.Invoke(p) ?? '_');
                    }
                    else if (hittest == MazeHitTestEnum.Entrypoint)
                    {
                        sb.Append('S');
                    }
                    else if (hittest == MazeHitTestEnum.Exitpoint)
                    {
                        sb.Append('G');
                    }
                    else if (hittest == MazeHitTestEnum.Roadblock)
                    {
                        sb.Append('X');
                    }
                }
                sb.Append(linesSeparator);
            }

            return sb.ToString().Trim();
        }

        static public string Quotify(string input, bool doubleInsteadOfSingleQuotes = true, bool wrapInQuotes = true)
        {
            var quoteCharacter = doubleInsteadOfSingleQuotes ? @"""" : @"'";
            var escapedQuoteCharacter = @"\" + quoteCharacter;

            input = input.Replace(@"\", @"\\");
            input = input.Replace(quoteCharacter, escapedQuoteCharacter);
            if (!wrapInQuotes) return input;

            return quoteCharacter + input + quoteCharacter;
        }

        // ReSharper disable LoopCanBeConvertedToQuery
        static public IEnumerable<T> ConvertAll<T>(this IEnumerable en, Converter<object, T> converter)
        {
            foreach (var input in en) yield return converter(input);
        }

        static public IEnumerable<TOutput> ConvertAll<TInput, TOutput>(this IEnumerable<TInput> en, Converter<TInput, TOutput> converter)
        {
            foreach (var input in en) yield return converter(input);
        }

        static public void ForEach<T>(this IEnumerable<T> en, Action<T> action)
        {
            foreach (var obj in en) action(obj);
        }

        static public void Each<T>(this IEnumerable<T> en, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in en) action(e, i++);
        }
        // ReSharper restore LoopCanBeConvertedToQuery


        static public readonly ReadOnlyDictionary<ConclusionStatusTypeEnum, string> ConclusionToSymbol = new ReadOnlyDictionary<ConclusionStatusTypeEnum, string>(new Dictionary<ConclusionStatusTypeEnum, string>
        {
            { ConclusionStatusTypeEnum.Crashed, "⚠" },
            { ConclusionStatusTypeEnum.Completed, "✓" },
            { ConclusionStatusTypeEnum.Stopped, "✋" }
        });

        static public readonly string nl = Environment.NewLine;
        static public readonly string nl2 = nl + nl;
    }
}
