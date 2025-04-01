using System.Collections.Generic;

namespace MazeRunner.Utils;

static public class StringJoinExtensions
{
    static public string CommaJoinify(this IEnumerable<string> en) => string.Join(", ", en);

    static public string LineJoinify(this IEnumerable<string> en, string separator = "\n") => string.Join(separator, en);
    static public string LineJoinifyPs(this IEnumerable<string> en) => string.Join(U.nl, en); //platform sensitive new lines
    static public string LineJoinifyPs2(this IEnumerable<string> en) => string.Join(U.nl2, en); //platform sensitive new lines

    static public string Joinify(this IEnumerable<string> en, string separator) => string.Join(separator, en);
}