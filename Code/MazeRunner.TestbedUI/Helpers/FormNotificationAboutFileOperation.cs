using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace MazeRunner.TestbedUI.Helpers;

public sealed partial class FormNotificationAboutFileOperation : Form
{
    private string _filePath;
    private string _fileGeneratedSuccessfullyMessage;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FileGeneratedSuccessfullyMessage
    {
        get => _fileGeneratedSuccessfullyMessage;
        set => _fileGeneratedSuccessfullyMessage = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FilePath
    {
        get => _filePath;
        set => _filePath = value;
    }

    public FormNotificationAboutFileOperation()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs ea)
    {
        try
        {
            lblFileLocation.Text = _filePath;
            lblFileGeneratedSuccessfullyMessage.Text = _fileGeneratedSuccessfullyMessage;
        }
        finally
        {
            base.OnLoad(ea);
        }
    }

    protected override void OnShown(EventArgs ea)
    {
        btnOK.Focus(); //0

        base.OnShown(ea);
    }
    //0 resorted to this technique for setting the ok button as the initially focused control because the taborder was playing tricks and wasnt behaving as intended

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if ((keyData == Keys.Enter && !lnkShowInFolder.Focused) || keyData == Keys.Escape) //0
        {
            DialogResult = keyData == Keys.Enter ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
    //0 had to resort to using this technique due to the fact that acceptbutton and cancelbutton only works if there is a textbox around  If there isnt any textbox in the form then no button gets pressed with enter or escape

    private void lnkShowInFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs ea)
    {
        Process.Start("explorer.exe", $"""
                                       {(!File.Exists(_filePath) ? "" : "/select, ")}"{_filePath}"
                                       """); //0

        Close();
    }
    //0 When we mean to select a file we need to use /select followed directly by a comma
}