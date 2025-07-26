using System;
using System.Collections;
using System.Collections.Generic;

namespace MazeRunner.Utils;

static public partial class EnumerableExtensions
{
    // ReSharper disable LoopCanBeConvertedToQuery
    static public IEnumerable<T> ConvertAll<T>(this IEnumerable en, Converter<object, T> converter)
    {
        foreach (var input in en) yield return converter(input);
    }

    static public IEnumerable<TOutput> ConvertAll<TInput, TOutput>(this IEnumerable<TInput> en, Converter<TInput, TOutput> converter)
    {
        foreach (var input in en) yield return converter(input);
    }
}
