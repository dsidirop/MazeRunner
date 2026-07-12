using System.Collections.Generic;

namespace MazeRunner.Injectors.Autofac.Extensions;

static internal class StringJoinExtensions
{
    static internal string Joinify(this IEnumerable<string> en, string separator) => string.Join(separator, en);
}