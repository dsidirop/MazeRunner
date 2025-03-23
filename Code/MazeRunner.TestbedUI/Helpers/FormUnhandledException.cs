using System;
using System.Windows.Forms;
using MazeRunner.Utils;

namespace MazeRunner.TestbedUI.Helpers;

public partial class FormUnhandledException : Form
{
    public FormUnhandledException(Exception exception) : this(StringFormatException(exception))
    {
    }
        
    public FormUnhandledException(string exceptionData)
    {
        InitializeComponent();

        txtExceptionData.Text = exceptionData;
        txtExceptionData.SelectionStart = 0;
        txtExceptionData.SelectionLength = 0;
    }

    private static string StringFormatException(Exception exception)
    {
        return exception == null
            ? $"(exception is null - no stacktrace available){U.nl}"
            : $"Exception = {exception.GetType()}{U.nl}Message = {exception.Message}{U.nl}FullText = {exception}{U.nl}";
    }
}