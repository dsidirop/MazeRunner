using System.Windows.Forms;

namespace MazeRunner.TestbedUI.Helpers
{
    public partial class FormUnhandledException : Form
    {
        public FormUnhandledException(string exceptionData)
        {
            InitializeComponent();

            txtExceptionData.Text = exceptionData;
            txtExceptionData.SelectionStart = 0;
            txtExceptionData.SelectionLength = 0;
        }
    }
}