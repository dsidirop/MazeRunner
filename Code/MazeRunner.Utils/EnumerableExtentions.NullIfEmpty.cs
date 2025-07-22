#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazeRunner.Utils;

static public partial class EnumerableExtensions
{
    public static IEnumerable? NullIfEmpty(this IEnumerable? input)
    {
        return input != null && input.Cast<object>().Any() ? input : null;
    }

    public static IEnumerable<T?>? NullIfEmpty<T>(this IEnumerable<T?>? input)
    {
        return input != null && input!.Any<T>() ? input : null;
    }
}
