#pragma warning disable CA1810 //disable warning about static initializers
#pragma warning disable CA5394 //disable warning about random not being cryptographically secure

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace MazeRunner.Utils;

static public class U //utilities
{
    static public readonly string ProductInstallationFolderpath_system;
    static U()
    {
        var assembly = Assembly.GetExecutingAssembly(); //tried application.startuppath but it didnt work
        var assemblyFilepath = assembly.Location;

        ProductInstallationFolderpath_system = Path.GetDirectoryName(assemblyFilepath);
    }

    static private readonly Random RandomNumbersEngine = new();
    static public ReorderableDictionary<int, int> GenerateRandomNumbersWithoutDuplicates(int count, int min, int maxExclusive, CancellationToken? cancellationToken = null) //max is exclusive here
    {
        if (maxExclusive <= min || count < 0 || (count > maxExclusive - min && maxExclusive - min > 0)) throw new ArgumentOutOfRangeException($"Range {min} to {maxExclusive} ({maxExclusive - (long) min} values) or count {count} is illegal"); //need to use 64bit to support big ranges negative min positive max
        
        var ct = cancellationToken ?? CancellationToken.None;
            
        var candidates = new ReorderableDictionary<int, int>(); //start count values before max and end at max
        for (var top = maxExclusive - count; top < maxExclusive; top++)
        {
            ct.ThrowIfCancellationRequested();
            
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

    static public T[] Shuffle<T>(this T[] list) //fisher-yates
    {
        var n = list.Length;
        while (n > 1)
        {
            n--;
            var k = RandomNumbersEngine.Next(n + 1);
            
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
    
    static public IList<T> Shuffle<T>(this IList<T> list) //fisher-yates
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = RandomNumbersEngine.Next(n + 1);
            
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }

    static public IEnumerable<Point> GetAdjacentPoints(this Point p) => Offsets.Select(i => new Point(p.X - i.X, p.Y - i.Y));

    static private readonly Point[] Offsets =
    [
        new(x: 0, y: -1),
        new(x: 1, y: 0),
        new(x: 0, y: 1),
        new(x: -1, y: 0)
    ];

    static public string NormalizeNewlines(this string input, string newlineToUse) => Regex.Replace(input, @"\r\n|\n\r|\n|\r", newlineToUse); //the order \r\n|\n\r|\n|\r is important

    static public string Quotify(string input, bool doubleInsteadOfSingleQuotes = true, bool wrapInQuotes = true)
    {
        var quoteCharacter = doubleInsteadOfSingleQuotes ? @"""" : @"'";
        var escapedQuoteCharacter = @"\" + quoteCharacter;

        input = input.Replace(@"\", @"\\", StringComparison.InvariantCultureIgnoreCase);
        input = input.Replace(quoteCharacter, escapedQuoteCharacter, StringComparison.InvariantCultureIgnoreCase);
        if (!wrapInQuotes) return input;

        return quoteCharacter + input + quoteCharacter;
    }

    static public readonly string nl = Environment.NewLine;
    static public readonly string nl2 = nl + nl;
}