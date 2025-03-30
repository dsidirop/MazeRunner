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
            this.mstripFile = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reshuffleCurrentMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateRandomMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpUniversal = new System.Windows.Forms.TableLayoutPanel();
            this.splitBoxHorizontal = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ccMazeCanvas = new MazeRunner.TestbedUI.Controls.CCMazeCanvas();
            this.tlpOptionsSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.lbxkEnginesToBenchmark = new System.Windows.Forms.CheckedListBox();
            this.lblAvailableEngines = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.tlpDummyContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOptionsContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblIterations = new System.Windows.Forms.Label();
            this.nudIterations = new System.Windows.Forms.NumericUpDown();
            this.nudMovementDelay = new System.Windows.Forms.NumericUpDown();
            this.lblMovementDelay = new System.Windows.Forms.Label();
            this.tlpStartStop = new System.Windows.Forms.TableLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            this.tlpLogs = new System.Windows.Forms.TableLayoutPanel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tlpFooterTitle = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogs = new System.Windows.Forms.Label();
            this.lnkClearLogs = new System.Windows.Forms.LinkLabel();
            this.mstripFile.SuspendLayout();
            this.tlpUniversal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBoxHorizontal)).BeginInit();
            this.splitBoxHorizontal.Panel1.SuspendLayout();
            this.splitBoxHorizontal.Panel2.SuspendLayout();
            this.splitBoxHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tlpOptionsSidebar.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.tlpDummyContainer.SuspendLayout();
            this.tlpOptionsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMovementDelay)).BeginInit();
            this.tlpStartStop.SuspendLayout();
            this.tlpLogs.SuspendLayout();
            this.tlpFooterTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // mstripFile
            // 
            this.mstripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.mstripFile.Location = new System.Drawing.Point(0, 0);
            this.mstripFile.Name = "mstripFile";
            this.mstripFile.Size = new System.Drawing.Size(1509, 24);
            this.mstripFile.TabIndex = 0;
            this.mstripFile.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMazeToolStripMenuItem,
            this.loadMazeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveMazeToolStripMenuItem
            // 
            this.saveMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.SaveMaze;
            this.saveMazeToolStripMenuItem.Name = "saveMazeToolStripMenuItem";
            this.saveMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMazeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.saveMazeToolStripMenuItem.Text = "&Save Maze";
            this.saveMazeToolStripMenuItem.Click += new System.EventHandler(this.saveMazeToolStripMenuItem_Click);
            // 
            // loadMazeToolStripMenuItem
            // 
            this.loadMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.LoadMaze;
            this.loadMazeToolStripMenuItem.Name = "loadMazeToolStripMenuItem";
            this.loadMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadMazeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.loadMazeToolStripMenuItem.Text = "&Load Maze";
            this.loadMazeToolStripMenuItem.Click += new System.EventHandler(this.loadMazeToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reshuffleCurrentMazeToolStripMenuItem,
            this.generateRandomMazeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // reshuffleCurrentMazeToolStripMenuItem
            // 
            this.reshuffleCurrentMazeToolStripMenuItem.Name = "reshuffleCurrentMazeToolStripMenuItem";
            this.reshuffleCurrentMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reshuffleCurrentMazeToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.reshuffleCurrentMazeToolStripMenuItem.Text = "&Reshuffle Current Maze";
            this.reshuffleCurrentMazeToolStripMenuItem.Click += new System.EventHandler(this.reshuffleCurrentMazeToolStripMenuItem_Click);
            // 
            // generateRandomMazeToolStripMenuItem
            // 
            this.generateRandomMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.GenerateRandomMaze;
            this.generateRandomMazeToolStripMenuItem.Name = "generateRandomMazeToolStripMenuItem";
            this.generateRandomMazeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.generateRandomMazeToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.generateRandomMazeToolStripMenuItem.Text = "&Generate Different Random Maze";
            this.generateRandomMazeToolStripMenuItem.Click += new System.EventHandler(this.generateRandomMazeToolStripMenuItem_Click);
            // 
            // tlpUniversal
            // 
            this.tlpUniversal.ColumnCount = 1;
            this.tlpUniversal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUniversal.Controls.Add(this.splitBoxHorizontal, 0, 0);
            this.tlpUniversal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUniversal.Location = new System.Drawing.Point(0, 24);
            this.tlpUniversal.Name = "tlpUniversal";
            this.tlpUniversal.RowCount = 1;
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUniversal.Size = new System.Drawing.Size(1509, 819);
            this.tlpUniversal.TabIndex = 1;
            // 
            // splitBoxHorizontal
            // 
            this.splitBoxHorizontal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitBoxHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBoxHorizontal.Location = new System.Drawing.Point(3, 3);
            this.splitBoxHorizontal.Name = "splitBoxHorizontal";
            this.splitBoxHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBoxHorizontal.Panel1
            // 
            this.splitBoxHorizontal.Panel1.Controls.Add(this.splitContainer1);
            this.splitBoxHorizontal.Panel1MinSize = 300;
            // 
            // splitBoxHorizontal.Panel2
            // 
            this.splitBoxHorizontal.Panel2.Controls.Add(this.tlpLogs);
            this.splitBoxHorizontal.Panel2MinSize = 200;
            this.splitBoxHorizontal.Size = new System.Drawing.Size(1503, 813);
            this.splitBoxHorizontal.SplitterDistance = 521;
            this.splitBoxHorizontal.SplitterWidth = 8;
            this.splitBoxHorizontal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.ccMazeCanvas);
            this.splitContainer1.Panel1MinSize = 450;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tlpOptionsSidebar);
            this.splitContainer1.Panel2MinSize = 380;
            this.splitContainer1.Size = new System.Drawing.Size(1503, 521);
            this.splitContainer1.SplitterDistance = 1115;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // ccMazeCanvas
            // 
            this.ccMazeCanvas.AutoSize = true;
            this.ccMazeCanvas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ccMazeCanvas.Location = new System.Drawing.Point(0, 0);
            this.ccMazeCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.ccMazeCanvas.Maze = null;
            this.ccMazeCanvas.Name = "ccMazeCanvas";
            this.ccMazeCanvas.Padding = new System.Windows.Forms.Padding(5);
            this.ccMazeCanvas.Size = new System.Drawing.Size(173, 173);
            this.ccMazeCanvas.TabIndex = 0;
            // 
            // tlpOptionsSidebar
            // 
            this.tlpOptionsSidebar.AutoSize = true;
            this.tlpOptionsSidebar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOptionsSidebar.ColumnCount = 1;
            this.tlpOptionsSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOptionsSidebar.Controls.Add(this.lbxkEnginesToBenchmark, 0, 3);
            this.tlpOptionsSidebar.Controls.Add(this.lblAvailableEngines, 0, 1);
            this.tlpOptionsSidebar.Controls.Add(this.gbOptions, 0, 5);
            this.tlpOptionsSidebar.Controls.Add(this.tlpStartStop, 0, 6);
            this.tlpOptionsSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOptionsSidebar.Location = new System.Drawing.Point(0, 0);
            this.tlpOptionsSidebar.MinimumSize = new System.Drawing.Size(300, 300);
            this.tlpOptionsSidebar.Name = "tlpOptionsSidebar";
            this.tlpOptionsSidebar.RowCount = 7;
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOptionsSidebar.Size = new System.Drawing.Size(378, 519);
            this.tlpOptionsSidebar.TabIndex = 0;
            // 
            // lbxkEnginesToBenchmark
            // 
            this.lbxkEnginesToBenchmark.CheckOnClick = true;
            this.lbxkEnginesToBenchmark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxkEnginesToBenchmark.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxkEnginesToBenchmark.Location = new System.Drawing.Point(3, 30);
            this.lbxkEnginesToBenchmark.Name = "lbxkEnginesToBenchmark";
            this.lbxkEnginesToBenchmark.Size = new System.Drawing.Size(377, 164);
            this.lbxkEnginesToBenchmark.TabIndex = 0;
            // 
            // lblAvailableEngines
            // 
            this.lblAvailableEngines.AutoSize = true;
            this.lblAvailableEngines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvailableEngines.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableEngines.Location = new System.Drawing.Point(3, 10);
            this.lblAvailableEngines.Name = "lblAvailableEngines";
            this.lblAvailableEngines.Size = new System.Drawing.Size(377, 13);
            this.lblAvailableEngines.TabIndex = 1;
            this.lblAvailableEngines.Text = "Available Engines:";
            // 
            // gbOptions
            // 
            this.gbOptions.AutoSize = true;
            this.gbOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbOptions.Controls.Add(this.tlpDummyContainer);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOptions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOptions.Location = new System.Drawing.Point(3, 210);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(377, 74);
            this.gbOptions.TabIndex = 1;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options:";
            // 
            // tlpDummyContainer
            // 
            this.tlpDummyContainer.AutoSize = true;
            this.tlpDummyContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpDummyContainer.ColumnCount = 1;
            this.tlpDummyContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDummyContainer.Controls.Add(this.tlpOptionsContainer, 0, 0);
            this.tlpDummyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDummyContainer.Location = new System.Drawing.Point(3, 17);
            this.tlpDummyContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDummyContainer.Name = "tlpDummyContainer";
            this.tlpDummyContainer.RowCount = 1;
            this.tlpDummyContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDummyContainer.Size = new System.Drawing.Size(371, 54);
            this.tlpDummyContainer.TabIndex = 0;
            // 
            // tlpOptionsContainer
            // 
            this.tlpOptionsContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpOptionsContainer.AutoSize = true;
            this.tlpOptionsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOptionsContainer.ColumnCount = 2;
            this.tlpOptionsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOptionsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOptionsContainer.Controls.Add(this.lblIterations, 0, 0);
            this.tlpOptionsContainer.Controls.Add(this.nudIterations, 1, 0);
            this.tlpOptionsContainer.Controls.Add(this.nudMovementDelay, 1, 1);
            this.tlpOptionsContainer.Controls.Add(this.lblMovementDelay, 0, 1);
            this.tlpOptionsContainer.Location = new System.Drawing.Point(62, 0);
            this.tlpOptionsContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpOptionsContainer.MaximumSize = new System.Drawing.Size(300, 0);
            this.tlpOptionsContainer.Name = "tlpOptionsContainer";
            this.tlpOptionsContainer.RowCount = 2;
            this.tlpOptionsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsContainer.Size = new System.Drawing.Size(246, 54);
            this.tlpOptionsContainer.TabIndex = 0;
            // 
            // lblIterations
            // 
            this.lblIterations.AutoSize = true;
            this.lblIterations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIterations.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIterations.Location = new System.Drawing.Point(3, 0);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(115, 27);
            this.lblIterations.TabIndex = 0;
            this.lblIterations.Text = "&Iterations:";
            this.lblIterations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudIterations
            // 
            this.nudIterations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudIterations.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudIterations.Location = new System.Drawing.Point(124, 3);
            this.nudIterations.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudIterations.Name = "nudIterations";
            this.nudIterations.Size = new System.Drawing.Size(119, 21);
            this.nudIterations.TabIndex = 0;
            this.nudIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudIterations.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudMovementDelay
            // 
            this.nudMovementDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudMovementDelay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMovementDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMovementDelay.Location = new System.Drawing.Point(124, 30);
            this.nudMovementDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMovementDelay.Name = "nudMovementDelay";
            this.nudMovementDelay.Size = new System.Drawing.Size(119, 21);
            this.nudMovementDelay.TabIndex = 1;
            this.nudMovementDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMovementDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblMovementDelay
            // 
            this.lblMovementDelay.AutoSize = true;
            this.lblMovementDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMovementDelay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovementDelay.Location = new System.Drawing.Point(3, 27);
            this.lblMovementDelay.Name = "lblMovementDelay";
            this.lblMovementDelay.Size = new System.Drawing.Size(115, 27);
            this.lblMovementDelay.TabIndex = 3;
            this.lblMovementDelay.Text = "&Movement Delay (ms):";
            this.lblMovementDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpStartStop
            // 
            this.tlpStartStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tlpStartStop.AutoSize = true;
            this.tlpStartStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpStartStop.ColumnCount = 3;
            this.tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpStartStop.Controls.Add(this.btnStart, 0, 0);
            this.tlpStartStop.Controls.Add(this.btnStop, 2, 0);
            this.tlpStartStop.Controls.Add(this.lblTip, 0, 1);
            this.tlpStartStop.Location = new System.Drawing.Point(80, 317);
            this.tlpStartStop.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.tlpStartStop.Name = "tlpStartStop";
            this.tlpStartStop.RowCount = 2;
            this.tlpStartStop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpStartStop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpStartStop.Size = new System.Drawing.Size(222, 107);
            this.tlpStartStop.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Image = global::MazeRunner.TestbedUI.Properties.Resources.StartBenchmark;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 38);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "&Start";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.AutoSize = true;
            this.btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Image = global::MazeRunner.TestbedUI.Properties.Resources.StopBenchmark;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(123, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 38);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "St&op";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblTip
            // 
            this.lblTip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTip.AutoSize = true;
            this.tlpStartStop.SetColumnSpan(this.lblTip, 3);
            this.lblTip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.Location = new System.Drawing.Point(46, 59);
            this.lblTip.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.lblTip.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(129, 48);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "Tip: Press Ctrl + R to reshuffle the current maze";
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpLogs
            // 
            this.tlpLogs.ColumnCount = 1;
            this.tlpLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpLogs.Controls.Add(this.txtLog, 0, 1);
            this.tlpLogs.Controls.Add(this.tlpFooterTitle, 0, 0);
            this.tlpLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLogs.Location = new System.Drawing.Point(0, 0);
            this.tlpLogs.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLogs.Name = "tlpLogs";
            this.tlpLogs.Padding = new System.Windows.Forms.Padding(5);
            this.tlpLogs.RowCount = 2;
            this.tlpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogs.Size = new System.Drawing.Size(1501, 282);
            this.tlpLogs.TabIndex = 0;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(8, 33);
            this.txtLog.MinimumSize = new System.Drawing.Size(250, 100);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1485, 241);
            this.txtLog.TabIndex = 0;
            // 
            // tlpFooterTitle
            // 
            this.tlpFooterTitle.AutoSize = true;
            this.tlpFooterTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpFooterTitle.ColumnCount = 2;
            this.tlpFooterTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFooterTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFooterTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFooterTitle.Controls.Add(this.lblLogs, 0, 0);
            this.tlpFooterTitle.Controls.Add(this.lnkClearLogs, 1, 0);
            this.tlpFooterTitle.Location = new System.Drawing.Point(8, 8);
            this.tlpFooterTitle.Name = "tlpFooterTitle";
            this.tlpFooterTitle.RowCount = 1;
            this.tlpFooterTitle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFooterTitle.Size = new System.Drawing.Size(83, 19);
            this.tlpFooterTitle.TabIndex = 1;
            // 
            // lblLogs
            // 
            this.lblLogs.AutoSize = true;
            this.lblLogs.Location = new System.Drawing.Point(3, 0);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lblLogs.Size = new System.Drawing.Size(33, 19);
            this.lblLogs.TabIndex = 4;
            this.lblLogs.Text = "Logs:";
            // 
            // lnkClearLogs
            // 
            this.lnkClearLogs.AutoSize = true;
            this.lnkClearLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lnkClearLogs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkClearLogs.Location = new System.Drawing.Point(42, 0);
            this.lnkClearLogs.Name = "lnkClearLogs";
            this.lnkClearLogs.Size = new System.Drawing.Size(38, 19);
            this.lnkClearLogs.TabIndex = 5;
            this.lnkClearLogs.TabStop = true;
            this.lnkClearLogs.Text = "(clear)";
            this.lnkClearLogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMazeRunnerTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1509, 843);
            this.Controls.Add(this.tlpUniversal);
            this.Controls.Add(this.mstripFile);
            this.Icon = global::MazeRunner.TestbedUI.Properties.Resources.Appicon;
            this.MainMenuStrip = this.mstripFile;
            this.MinimumSize = new System.Drawing.Size(900, 750);
            this.Name = "FormMazeRunnerTester";
            this.Text = "Maze Runner";
            this.mstripFile.ResumeLayout(false);
            this.mstripFile.PerformLayout();
            this.tlpUniversal.ResumeLayout(false);
            this.splitBoxHorizontal.Panel1.ResumeLayout(false);
            this.splitBoxHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBoxHorizontal)).EndInit();
            this.splitBoxHorizontal.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tlpOptionsSidebar.ResumeLayout(false);
            this.tlpOptionsSidebar.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.tlpDummyContainer.ResumeLayout(false);
            this.tlpDummyContainer.PerformLayout();
            this.tlpOptionsContainer.ResumeLayout(false);
            this.tlpOptionsContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMovementDelay)).EndInit();
            this.tlpStartStop.ResumeLayout(false);
            this.tlpStartStop.PerformLayout();
            this.tlpLogs.ResumeLayout(false);
            this.tlpLogs.PerformLayout();
            this.tlpFooterTitle.ResumeLayout(false);
            this.tlpFooterTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.CheckedListBox lbxkEnginesToBenchmark;
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
        private CCMazeCanvas ccMazeCanvas;
        private System.Windows.Forms.ToolStripMenuItem reshuffleCurrentMazeToolStripMenuItem;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.TableLayoutPanel tlpFooterTitle;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.LinkLabel lnkClearLogs;
    }
}

