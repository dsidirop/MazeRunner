using System;
using System.Collections.Generic;

namespace MazeRunner.Utils;

static public partial class EnumerableExtensions
{
    // ReSharper disable LoopCanBeConvertedToQuery
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
