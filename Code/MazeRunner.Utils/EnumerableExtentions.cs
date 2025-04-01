using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace MazeRunner.Utils;

static public class EnumerableExtensions
{
    static public IEnumerable<T> UnderCancellationToken<T>(this IEnumerable<T> en, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        foreach (var item in en)
        {
            token.ThrowIfCancellationRequested();
            yield return item;
        }
        
        token.ThrowIfCancellationRequested();
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
