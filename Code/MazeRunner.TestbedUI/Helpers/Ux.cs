using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MazeRunner.TestbedUI.Helpers;

static internal class Ux //utility extensions
{
    static internal void AppendTextAndScrollToBottom(this TextBox textbox, string textToAppend)
    {
        textbox.AppendText(textToAppend);
    }

    static private readonly Regex TrailingNonWhitespaceChars = new Regex(@"[^\s]{1,10}$", RegexOptions.IgnoreCase);
    static private readonly Regex PrependedNonWhitespaceChars = new Regex(@"^[^\s]{1,10}", RegexOptions.IgnoreCase);
    static public string GenerateEllipsisInMiddle(string text, int maxLength = 50, int prefixMaxLength = 0, int postfixMaxLength = 0)
    {
        if (string.IsNullOrEmpty(text) || text.Length < maxLength) return text;

        var defaultMaxLength = (int)(0.35 * maxLength);
        prefixMaxLength = Math.Min(defaultMaxLength, prefixMaxLength > 0 ? prefixMaxLength : int.MaxValue);
        postfixMaxLength = Math.Min(defaultMaxLength, postfixMaxLength > 0 ? postfixMaxLength : int.MaxValue);
        return $"{TrailingNonWhitespaceChars.Replace(text.Substring(0, prefixMaxLength), "")} ... {PrependedNonWhitespaceChars.Replace(text.Substring(text.Length - postfixMaxLength, postfixMaxLength), "")}";
    }
}