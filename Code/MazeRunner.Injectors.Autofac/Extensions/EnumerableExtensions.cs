using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazeRunner.Injectors.Autofac.Extensions;

static internal class EnumerableExtensionsX
{
    static internal IEnumerable? NullIfEmpty(this IEnumerable? input)
    {
        return input != null && input.Cast<object>().Any() ? input : null;
    }

    static internal IEnumerable<T?>? NullIfEmpty<T>(this IEnumerable<T?>? input)
    {
        return input != null && input!.Any<T>() ? input : null;
    }
}
