using System.Windows.Forms;
using MazeRunner.TestbedUI.Controls;

namespace MazeRunner.TestbedUI
{
    partial class FormMazeRunnerTester
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mstripFile = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loadMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            reshuffleCurrentMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            generateRandomMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tlpUniversal = new System.Windows.Forms.TableLayoutPanel();
            splitBoxHorizontal = new System.Windows.Forms.SplitContainer();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            _ccMazeCanvas = new MazeRunner.TestbedUI.Controls.CCMazeCanvas();
            tlpOptionsSidebar = new System.Windows.Forms.TableLayoutPanel();
            _lbxkEnginesToBenchmark = new System.Windows.Forms.CheckedListBox();
            lblAvailableEngines = new System.Windows.Forms.Label();
            gbOptions = new System.Windows.Forms.GroupBox();
            tlpDummyContainer = new System.Windows.Forms.TableLayoutPanel();
            tlpOptionsContainer = new System.Windows.Forms.TableLayoutPanel();
            lblIterations = new System.Windows.Forms.Label();
            nudIterations = new System.Windows.Forms.NumericUpDown();
            nudMovementDelay = new System.Windows.Forms.NumericUpDown();
            lblMovementDelay = new System.Windows.Forms.Label();
            tlpStartStop = new System.Windows.Forms.TableLayoutPanel();
            btnStart = new System.Windows.Forms.Button();
            btnStop = new System.Windows.Forms.Button();
            lblTip = new System.Windows.Forms.Label();
            tlpLogs = new System.Windows.Forms.TableLayoutPanel();
            txtLog = new System.Windows.Forms.TextBox();
            tlpFooterTitle = new System.Windows.Forms.TableLayoutPanel();
            lblLogs = new System.Windows.Forms.Label();
            _lnkClearLogs = new System.Windows.Forms.LinkLabel();
            mstripFile.SuspendLayout();
            tlpUniversal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitBoxHorizontal).BeginInit();
            splitBoxHorizontal.Panel1.SuspendLayout();
            splitBoxHorizontal.Panel2.SuspendLayout();
            splitBoxHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tlpOptionsSidebar.SuspendLayout();
            gbOptions.SuspendLayout();
            tlpDummyContainer.SuspendLayout();
            tlpOptionsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudIterations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMovementDelay).BeginInit();
            tlpStartStop.SuspendLayout();
            tlpLogs.SuspendLayout();
            tlpFooterTitle.SuspendLayout();
            SuspendLayout();
            // 
            // mstripFile
            // 
            mstripFile.ImageScalingSize = new System.Drawing.Size(19, 19);
            mstripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem });
            mstripFile.Location = new System.Drawing.Point(0, 0);
            mstripFile.Name = "mstripFile";
            mstripFile.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            mstripFile.Size = new System.Drawing.Size(2012, 30);
            mstripFile.TabIndex = 0;
            mstripFile.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveMazeToolStripMenuItem, loadMazeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            fileToolStripMenuItem.Text = "&File";
            // 
            // saveMazeToolStripMenuItem
            // 
            saveMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.SaveMaze;
            saveMazeToolStripMenuItem.Name = "saveMazeToolStripMenuItem";
            saveMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            saveMazeToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            saveMazeToolStripMenuItem.Text = "&Save Maze";
            saveMazeToolStripMenuItem.Click += saveMazeToolStripMenuItem_Click;
            // 
            // loadMazeToolStripMenuItem
            // 
            loadMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.LoadMaze;
            loadMazeToolStripMenuItem.Name = "loadMazeToolStripMenuItem";
            loadMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            loadMazeToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            loadMazeToolStripMenuItem.Text = "&Load Maze";
            loadMazeToolStripMenuItem.Click += loadMazeToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { reshuffleCurrentMazeToolStripMenuItem, generateRandomMazeToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // reshuffleCurrentMazeToolStripMenuItem
            // 
            reshuffleCurrentMazeToolStripMenuItem.Name = "reshuffleCurrentMazeToolStripMenuItem";
            reshuffleCurrentMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
            reshuffleCurrentMazeToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            reshuffleCurrentMazeToolStripMenuItem.Text = "&Reshuffle Current Maze";
            reshuffleCurrentMazeToolStripMenuItem.Click += reshuffleCurrentMazeToolStripMenuItem_Click;
            // 
            // generateRandomMazeToolStripMenuItem
            // 
            generateRandomMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.GenerateRandomMaze;
            generateRandomMazeToolStripMenuItem.Name = "generateRandomMazeToolStripMenuItem";
            generateRandomMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G));
            generateRandomMazeToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            generateRandomMazeToolStripMenuItem.Text = "&Generate Different Random Maze";
            generateRandomMazeToolStripMenuItem.Click += generateRandomMazeToolStripMenuItem_Click;
            // 
            // tlpUniversal
            // 
            tlpUniversal.ColumnCount = 1;
            tlpUniversal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpUniversal.Controls.Add(splitBoxHorizontal, 0, 0);
            tlpUniversal.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpUniversal.Location = new System.Drawing.Point(0, 30);
            tlpUniversal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            tlpUniversal.Name = "tlpUniversal";
            tlpUniversal.RowCount = 1;
            tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpUniversal.Size = new System.Drawing.Size(2012, 1202);
            tlpUniversal.TabIndex = 1;
            // 
            // splitBoxHorizontal
            // 
            splitBoxHorizontal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitBoxHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            splitBoxHorizontal.Location = new System.Drawing.Point(4, 4);
            splitBoxHorizontal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            splitBoxHorizontal.Name = "splitBoxHorizontal";
            splitBoxHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBoxHorizontal.Panel1
            // 
            splitBoxHorizontal.Panel1.Controls.Add(splitContainer1);
            splitBoxHorizontal.Panel1MinSize = 300;
            // 
            // splitBoxHorizontal.Panel2
            // 
            splitBoxHorizontal.Panel2.Controls.Add(tlpLogs);
            splitBoxHorizontal.Panel2MinSize = 200;
            splitBoxHorizontal.Size = new System.Drawing.Size(2004, 1194);
            splitBoxHorizontal.SplitterDistance = 765;
            splitBoxHorizontal.SplitterWidth = 12;
            splitBoxHorizontal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(_ccMazeCanvas);
            splitContainer1.Panel1MinSize = 450;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tlpOptionsSidebar);
            splitContainer1.Panel2MinSize = 380;
            splitContainer1.Size = new System.Drawing.Size(2004, 765);
            splitContainer1.SplitterDistance = 937;
            splitContainer1.SplitterWidth = 11;
            splitContainer1.TabIndex = 0;
            // 
            // _ccMazeCanvas
            // 
            _ccMazeCanvas.AutoSize = true;
            _ccMazeCanvas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            _ccMazeCanvas.Location = new System.Drawing.Point(0, 0);
            _ccMazeCanvas.Margin = new System.Windows.Forms.Padding(0);
            _ccMazeCanvas.Name = "_ccMazeCanvas";
            _ccMazeCanvas.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            _ccMazeCanvas.Size = new System.Drawing.Size(175, 175);
            _ccMazeCanvas.TabIndex = 0;
            // 
            // tlpOptionsSidebar
            // 
            tlpOptionsSidebar.AutoSize = true;
            tlpOptionsSidebar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tlpOptionsSidebar.ColumnCount = 1;
            tlpOptionsSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpOptionsSidebar.Controls.Add(_lbxkEnginesToBenchmark, 0, 3);
            tlpOptionsSidebar.Controls.Add(lblAvailableEngines, 0, 1);
            tlpOptionsSidebar.Controls.Add(gbOptions, 0, 5);
            tlpOptionsSidebar.Controls.Add(tlpStartStop, 0, 6);
            tlpOptionsSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpOptionsSidebar.Location = new System.Drawing.Point(0, 0);
            tlpOptionsSidebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            tlpOptionsSidebar.MinimumSize = new System.Drawing.Size(400, 438);
            tlpOptionsSidebar.Name = "tlpOptionsSidebar";
            tlpOptionsSidebar.RowCount = 7;
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpOptionsSidebar.Size = new System.Drawing.Size(1054, 763);
            tlpOptionsSidebar.TabIndex = 0;
            // 
            // _lbxkEnginesToBenchmark
            // 
            _lbxkEnginesToBenchmark.CheckOnClick = true;
            _lbxkEnginesToBenchmark.Dock = System.Windows.Forms.DockStyle.Fill;
            _lbxkEnginesToBenchmark.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            _lbxkEnginesToBenchmark.Location = new System.Drawing.Point(4, 42);
            _lbxkEnginesToBenchmark.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            _lbxkEnginesToBenchmark.Name = "_lbxkEnginesToBenchmark";
            _lbxkEnginesToBenchmark.Size = new System.Drawing.Size(1046, 238);
            _lbxkEnginesToBenchmark.TabIndex = 0;
            // 
            // lblAvailableEngines
            // 
            lblAvailableEngines.AutoSize = true;
            lblAvailableEngines.Dock = System.Windows.Forms.DockStyle.Fill;
            lblAvailableEngines.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lblAvailableEngines.Location = new System.Drawing.Point(4, 15);
            lblAvailableEngines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblAvailableEngines.Name = "lblAvailableEngines";
            lblAvailableEngines.Size = new System.Drawing.Size(1046, 17);
            lblAvailableEngines.TabIndex = 1;
            lblAvailableEngines.Text = "Available Engines:";
            // 
            // gbOptions
            // 
            gbOptions.AutoSize = true;
            gbOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbOptions.Controls.Add(tlpDummyContainer);
            gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            gbOptions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            gbOptions.Location = new System.Drawing.Point(4, 303);
            gbOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbOptions.Name = "gbOptions";
            gbOptions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbOptions.Size = new System.Drawing.Size(1046, 86);
            gbOptions.TabIndex = 1;
            gbOptions.TabStop = false;
            gbOptions.Text = "Options:";
            // 
            // tlpDummyContainer
            // 
            tlpDummyContainer.AutoSize = true;
            tlpDummyContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tlpDummyContainer.ColumnCount = 1;
            tlpDummyContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpDummyContainer.Controls.Add(tlpOptionsContainer, 0, 0);
            tlpDummyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpDummyContainer.Location = new System.Drawing.Point(4, 20);
            tlpDummyContainer.Margin = new System.Windows.Forms.Padding(0);
            tlpDummyContainer.Name = "tlpDummyContainer";
            tlpDummyContainer.RowCount = 1;
            tlpDummyContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpDummyContainer.Size = new System.Drawing.Size(1038, 62);
            tlpDummyContainer.TabIndex = 0;
            // 
            // tlpOptionsContainer
            // 
            tlpOptionsContainer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            tlpOptionsContainer.AutoSize = true;
            tlpOptionsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tlpOptionsContainer.ColumnCount = 2;
            tlpOptionsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpOptionsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpOptionsContainer.Controls.Add(lblIterations, 0, 0);
            tlpOptionsContainer.Controls.Add(nudIterations, 1, 0);
            tlpOptionsContainer.Controls.Add(nudMovementDelay, 1, 1);
            tlpOptionsContainer.Controls.Add(lblMovementDelay, 0, 1);
            tlpOptionsContainer.Location = new System.Drawing.Point(357, 0);
            tlpOptionsContainer.Margin = new System.Windows.Forms.Padding(0);
            tlpOptionsContainer.MaximumSize = new System.Drawing.Size(400, 0);
            tlpOptionsContainer.Name = "tlpOptionsContainer";
            tlpOptionsContainer.RowCount = 2;
            tlpOptionsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpOptionsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpOptionsContainer.Size = new System.Drawing.Size(323, 62);
            tlpOptionsContainer.TabIndex = 0;
            // 
            // lblIterations
            // 
            lblIterations.AutoSize = true;
            lblIterations.Dock = System.Windows.Forms.DockStyle.Fill;
            lblIterations.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lblIterations.Location = new System.Drawing.Point(4, 0);
            lblIterations.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblIterations.Name = "lblIterations";
            lblIterations.Size = new System.Drawing.Size(148, 31);
            lblIterations.TabIndex = 0;
            lblIterations.Text = "&Iterations:";
            lblIterations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudIterations
            // 
            nudIterations.Dock = System.Windows.Forms.DockStyle.Fill;
            nudIterations.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            nudIterations.Location = new System.Drawing.Point(160, 4);
            nudIterations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            nudIterations.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudIterations.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudIterations.Name = "nudIterations";
            nudIterations.Size = new System.Drawing.Size(159, 23);
            nudIterations.TabIndex = 0;
            nudIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            nudIterations.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // nudMovementDelay
            // 
            nudMovementDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            nudMovementDelay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            nudMovementDelay.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudMovementDelay.Location = new System.Drawing.Point(160, 35);
            nudMovementDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            nudMovementDelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudMovementDelay.Name = "nudMovementDelay";
            nudMovementDelay.Size = new System.Drawing.Size(159, 23);
            nudMovementDelay.TabIndex = 1;
            nudMovementDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            nudMovementDelay.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // lblMovementDelay
            // 
            lblMovementDelay.AutoSize = true;
            lblMovementDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            lblMovementDelay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lblMovementDelay.Location = new System.Drawing.Point(4, 31);
            lblMovementDelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMovementDelay.Name = "lblMovementDelay";
            lblMovementDelay.Size = new System.Drawing.Size(148, 31);
            lblMovementDelay.TabIndex = 3;
            lblMovementDelay.Text = "&Movement Delay (ms):";
            lblMovementDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpStartStop
            // 
            tlpStartStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            tlpStartStop.AutoSize = true;
            tlpStartStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tlpStartStop.ColumnCount = 3;
            tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpStartStop.Controls.Add(btnStart, 0, 0);
            tlpStartStop.Controls.Add(btnStop, 2, 0);
            tlpStartStop.Controls.Add(lblTip, 0, 1);
            tlpStartStop.Location = new System.Drawing.Point(400, 437);
            tlpStartStop.Margin = new System.Windows.Forms.Padding(4, 44, 4, 4);
            tlpStartStop.Name = "tlpStartStop";
            tlpStartStop.RowCount = 2;
            tlpStartStop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpStartStop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpStartStop.Size = new System.Drawing.Size(254, 109);
            tlpStartStop.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.AutoSize = true;
            btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            btnStart.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            btnStart.Image = global::MazeRunner.TestbedUI.Properties.Resources.StartBenchmark;
            btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnStart.Location = new System.Drawing.Point(4, 4);
            btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(111, 41);
            btnStart.TabIndex = 0;
            btnStart.Text = "&Start";
            btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.AutoSize = true;
            btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            btnStop.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            btnStop.Image = global::MazeRunner.TestbedUI.Properties.Resources.StopBenchmark;
            btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnStop.Location = new System.Drawing.Point(143, 4);
            btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(107, 41);
            btnStop.TabIndex = 1;
            btnStop.Text = "St&op";
            btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // lblTip
            // 
            lblTip.Anchor = System.Windows.Forms.AnchorStyles.None;
            lblTip.AutoSize = true;
            tlpStartStop.SetColumnSpan(lblTip, 3);
            lblTip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lblTip.Location = new System.Drawing.Point(30, 71);
            lblTip.Margin = new System.Windows.Forms.Padding(4, 22, 4, 0);
            lblTip.MaximumSize = new System.Drawing.Size(200, 0);
            lblTip.Name = "lblTip";
            lblTip.Size = new System.Drawing.Size(194, 38);
            lblTip.TabIndex = 2;
            lblTip.Text = "Note: The engines complete very fast in the background but the UI will take some time to render everything\n\nTip: Press Ctrl + R to reshuffle the current maze";
            lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpLogs
            // 
            tlpLogs.ColumnCount = 1;
            tlpLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpLogs.Controls.Add(txtLog, 0, 1);
            tlpLogs.Controls.Add(tlpFooterTitle, 0, 0);
            tlpLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpLogs.Location = new System.Drawing.Point(0, 0);
            tlpLogs.Margin = new System.Windows.Forms.Padding(0);
            tlpLogs.Name = "tlpLogs";
            tlpLogs.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            tlpLogs.RowCount = 2;
            tlpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpLogs.Size = new System.Drawing.Size(2002, 415);
            tlpLogs.TabIndex = 0;
            // 
            // txtLog
            // 
            txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            txtLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            txtLog.Location = new System.Drawing.Point(11, 47);
            txtLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            txtLog.MinimumSize = new System.Drawing.Size(332, 144);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(1980, 357);
            txtLog.TabIndex = 0;
            // 
            // tlpFooterTitle
            // 
            tlpFooterTitle.AutoSize = true;
            tlpFooterTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tlpFooterTitle.ColumnCount = 2;
            tlpFooterTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpFooterTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpFooterTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tlpFooterTitle.Controls.Add(lblLogs, 0, 0);
            tlpFooterTitle.Controls.Add(_lnkClearLogs, 1, 0);
            tlpFooterTitle.Location = new System.Drawing.Point(11, 11);
            tlpFooterTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            tlpFooterTitle.Name = "tlpFooterTitle";
            tlpFooterTitle.RowCount = 1;
            tlpFooterTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpFooterTitle.Size = new System.Drawing.Size(105, 28);
            tlpFooterTitle.TabIndex = 1;
            // 
            // lblLogs
            // 
            lblLogs.AutoSize = true;
            lblLogs.Location = new System.Drawing.Point(4, 0);
            lblLogs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblLogs.Name = "lblLogs";
            lblLogs.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            lblLogs.Size = new System.Drawing.Size(43, 28);
            lblLogs.TabIndex = 4;
            lblLogs.Text = "Logs:";
            // 
            // _lnkClearLogs
            // 
            _lnkClearLogs.AutoSize = true;
            _lnkClearLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            _lnkClearLogs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            _lnkClearLogs.Location = new System.Drawing.Point(55, 0);
            _lnkClearLogs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            _lnkClearLogs.Name = "_lnkClearLogs";
            _lnkClearLogs.Size = new System.Drawing.Size(46, 28);
            _lnkClearLogs.TabIndex = 5;
            _lnkClearLogs.TabStop = true;
            _lnkClearLogs.Text = "(clear)";
            _lnkClearLogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMazeRunnerTester
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(2012, 1232);
            Controls.Add(tlpUniversal);
            Controls.Add(mstripFile);
            Icon = global::MazeRunner.TestbedUI.Properties.Resources.Appicon;
            MainMenuStrip = mstripFile;
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            MinimumSize = new System.Drawing.Size(1194, 1075);
            Text = "Maze Runner";
            mstripFile.ResumeLayout(false);
            mstripFile.PerformLayout();
            tlpUniversal.ResumeLayout(false);
            splitBoxHorizontal.Panel1.ResumeLayout(false);
            splitBoxHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitBoxHorizontal).EndInit();
            splitBoxHorizontal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tlpOptionsSidebar.ResumeLayout(false);
            tlpOptionsSidebar.PerformLayout();
            gbOptions.ResumeLayout(false);
            gbOptions.PerformLayout();
            tlpDummyContainer.ResumeLayout(false);
            tlpDummyContainer.PerformLayout();
            tlpOptionsContainer.ResumeLayout(false);
            tlpOptionsContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudIterations).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMovementDelay).EndInit();
            tlpStartStop.ResumeLayout(false);
            tlpStartStop.PerformLayout();
            tlpLogs.ResumeLayout(false);
            tlpLogs.PerformLayout();
            tlpFooterTitle.ResumeLayout(false);
            tlpFooterTitle.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip mstripFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateRandomMazeToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpUniversal;
        private System.Windows.Forms.SplitContainer splitBoxHorizontal;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tlpOptionsSidebar;
        private System.Windows.Forms.CheckedListBox _lbxkEnginesToBenchmark;
        private System.Windows.Forms.Label lblAvailableEngines;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.TableLayoutPanel tlpStartStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TableLayoutPanel tlpLogs;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TableLayoutPanel tlpDummyContainer;
        private System.Windows.Forms.TableLayoutPanel tlpOptionsContainer;
        private System.Windows.Forms.Label lblIterations;
        private System.Windows.Forms.NumericUpDown nudIterations;
        private System.Windows.Forms.NumericUpDown nudMovementDelay;
        private System.Windows.Forms.Label lblMovementDelay;
        private CCMazeCanvas _ccMazeCanvas;
        private System.Windows.Forms.ToolStripMenuItem reshuffleCurrentMazeToolStripMenuItem;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.TableLayoutPanel tlpFooterTitle;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.LinkLabel _lnkClearLogs;
    }
}

