using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MazeRunner.Shared.Interfaces;

namespace MazeRunner.TestbedUI.Helpers
{
    static internal class UtilsX
    {
        static internal MazeSpecs GetMazeSpecs(this IMaze maze) => new MazeSpecs {Height = maze.Size.Height, Width = maze.Size.Width, RoadblockDensity = maze.RoadblocksCount/(((double) maze.Size.Width)*maze.Size.Height)};

        static internal void AppendTextAndScrollToBottom(this TextBox textbox, string textToAppend)
        {
            textbox.Text += textToAppend;
            if (!textbox.Visible) return;

            textbox.SelectionStart = textbox.TextLength;
            textbox.ScrollToCaret();
        }

        static internal void Post(this SynchronizationContext context, SendOrPostCallback callback) => context.Post(callback, null);
        static internal void Send(this SynchronizationContext context, SendOrPostCallback callback) => context.Send(callback, null);

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
}