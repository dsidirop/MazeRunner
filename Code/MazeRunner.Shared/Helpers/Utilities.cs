using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MazeRunner.Shared.Maze;

namespace MazeRunner.Shared.Helpers
{
    static public class Utilities
    {
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

        static public IList<T> Shuffle<T>(this IList<T> list)
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

        static public string ToAsciiMap(this IMaze maze)
        {
            var sb = new StringBuilder();
            for (var y = 0; y < maze.Size.Height; y++)
            {
                for (var x = 0; x < maze.Size.Width; x++)
                {
                    var hittest = maze.HitTest(new Point(x, y));
                    if (hittest == MazeHitTestEnum.Free)
                    {
                        sb.Append('_');
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
                sb.AppendLine();
            }

            return sb.ToString().Trim();
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
    }
}
