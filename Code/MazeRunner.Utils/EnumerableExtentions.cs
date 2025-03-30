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
}