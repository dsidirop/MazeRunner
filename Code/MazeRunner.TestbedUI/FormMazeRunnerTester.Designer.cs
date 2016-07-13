namespace MazeRunner.TestbedUI
{
    partial class FormMazeRunnerTester
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mstripFile = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpUniversal = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOptionsSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.lbxkAvailableEngines = new System.Windows.Forms.CheckedListBox();
            this.lblAvailableEngines = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.tlpOptionsContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblIterations = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.lblDelay = new System.Windows.Forms.Label();
            this.tlpStartStop = new System.Windows.Forms.TableLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.mstripFile.SuspendLayout();
            this.tlpUniversal.SuspendLayout();
            this.tlpOptionsSidebar.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.tlpOptionsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.tlpStartStop.SuspendLayout();
            this.SuspendLayout();
            // 
            // mstripFile
            // 
            this.mstripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.mstripFile.Location = new System.Drawing.Point(0, 0);
            this.mstripFile.Name = "mstripFile";
            this.mstripFile.Size = new System.Drawing.Size(944, 24);
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
            // loadMazeToolStripMenuItem
            // 
            this.loadMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.LoadMaze;
            this.loadMazeToolStripMenuItem.Name = "loadMazeToolStripMenuItem";
            this.loadMazeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadMazeToolStripMenuItem.Text = "&Load Maze";
            // 
            // saveMazeToolStripMenuItem
            // 
            this.saveMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.SaveMaze;
            this.saveMazeToolStripMenuItem.Name = "saveMazeToolStripMenuItem";
            this.saveMazeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveMazeToolStripMenuItem.Text = "&Save Maze";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomMazeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // randomMazeToolStripMenuItem
            // 
            this.randomMazeToolStripMenuItem.Image = global::MazeRunner.TestbedUI.Properties.Resources.GenerateRandomMaze;
            this.randomMazeToolStripMenuItem.Name = "randomMazeToolStripMenuItem";
            this.randomMazeToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.randomMazeToolStripMenuItem.Text = "&Generate Random Maze";
            // 
            // tlpUniversal
            // 
            this.tlpUniversal.ColumnCount = 2;
            this.tlpUniversal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUniversal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUniversal.Controls.Add(this.tlpOptionsSidebar, 1, 0);
            this.tlpUniversal.Controls.Add(this.txtLog, 0, 1);
            this.tlpUniversal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUniversal.Location = new System.Drawing.Point(0, 24);
            this.tlpUniversal.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUniversal.Name = "tlpUniversal";
            this.tlpUniversal.RowCount = 2;
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpUniversal.Size = new System.Drawing.Size(944, 713);
            this.tlpUniversal.TabIndex = 1;
            // 
            // tlpOptionsSidebar
            // 
            this.tlpOptionsSidebar.AutoSize = true;
            this.tlpOptionsSidebar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOptionsSidebar.ColumnCount = 1;
            this.tlpOptionsSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOptionsSidebar.Controls.Add(this.lbxkAvailableEngines, 0, 3);
            this.tlpOptionsSidebar.Controls.Add(this.lblAvailableEngines, 0, 1);
            this.tlpOptionsSidebar.Controls.Add(this.gbOptions, 0, 5);
            this.tlpOptionsSidebar.Controls.Add(this.tlpStartStop, 0, 6);
            this.tlpOptionsSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOptionsSidebar.Location = new System.Drawing.Point(663, 3);
            this.tlpOptionsSidebar.Name = "tlpOptionsSidebar";
            this.tlpOptionsSidebar.RowCount = 7;
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOptionsSidebar.Size = new System.Drawing.Size(278, 407);
            this.tlpOptionsSidebar.TabIndex = 0;
            // 
            // lbxkAvailableEngines
            // 
            this.lbxkAvailableEngines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxkAvailableEngines.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxkAvailableEngines.FormattingEnabled = true;
            this.lbxkAvailableEngines.Location = new System.Drawing.Point(3, 30);
            this.lbxkAvailableEngines.Name = "lbxkAvailableEngines";
            this.lbxkAvailableEngines.Size = new System.Drawing.Size(272, 96);
            this.lbxkAvailableEngines.TabIndex = 0;
            // 
            // lblAvailableEngines
            // 
            this.lblAvailableEngines.AutoSize = true;
            this.lblAvailableEngines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvailableEngines.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableEngines.Location = new System.Drawing.Point(3, 10);
            this.lblAvailableEngines.Name = "lblAvailableEngines";
            this.lblAvailableEngines.Size = new System.Drawing.Size(272, 13);
            this.lblAvailableEngines.TabIndex = 1;
            this.lblAvailableEngines.Text = "Available Engines:";
            // 
            // gbOptions
            // 
            this.gbOptions.AutoSize = true;
            this.gbOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbOptions.Controls.Add(this.tlpOptionsContainer);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOptions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOptions.Location = new System.Drawing.Point(3, 142);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(272, 74);
            this.gbOptions.TabIndex = 2;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options:";
            // 
            // tlpOptionsContainer
            // 
            this.tlpOptionsContainer.AutoSize = true;
            this.tlpOptionsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOptionsContainer.ColumnCount = 2;
            this.tlpOptionsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOptionsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOptionsContainer.Controls.Add(this.lblIterations, 0, 0);
            this.tlpOptionsContainer.Controls.Add(this.numericUpDown1, 1, 0);
            this.tlpOptionsContainer.Controls.Add(this.numericUpDown2, 1, 1);
            this.tlpOptionsContainer.Controls.Add(this.lblDelay, 0, 1);
            this.tlpOptionsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOptionsContainer.Location = new System.Drawing.Point(3, 17);
            this.tlpOptionsContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpOptionsContainer.Name = "tlpOptionsContainer";
            this.tlpOptionsContainer.RowCount = 2;
            this.tlpOptionsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOptionsContainer.Size = new System.Drawing.Size(266, 54);
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
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(124, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(139, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(124, 30);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(139, 21);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDelay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelay.Location = new System.Drawing.Point(3, 27);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(115, 27);
            this.lblDelay.TabIndex = 3;
            this.lblDelay.Text = "&Movement Delay (ms):";
            this.lblDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpStartStop
            // 
            this.tlpStartStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpStartStop.AutoSize = true;
            this.tlpStartStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpStartStop.ColumnCount = 3;
            this.tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpStartStop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpStartStop.Controls.Add(this.btnStart, 0, 0);
            this.tlpStartStop.Controls.Add(this.btnStop, 2, 0);
            this.tlpStartStop.Location = new System.Drawing.Point(28, 291);
            this.tlpStartStop.Name = "tlpStartStop";
            this.tlpStartStop.RowCount = 1;
            this.tlpStartStop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpStartStop.Size = new System.Drawing.Size(222, 44);
            this.tlpStartStop.TabIndex = 3;
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
            // 
            // txtLog
            // 
            this.tlpUniversal.SetColumnSpan(this.txtLog, 2);
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 416);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(938, 294);
            this.txtLog.TabIndex = 1;
            // 
            // FormMazeRunnerTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 737);
            this.Controls.Add(this.tlpUniversal);
            this.Controls.Add(this.mstripFile);
            this.Icon = global::MazeRunner.TestbedUI.Properties.Resources.Appicon;
            this.MainMenuStrip = this.mstripFile;
            this.Name = "FormMazeRunnerTester";
            this.Text = "Maze Runner";
            this.mstripFile.ResumeLayout(false);
            this.mstripFile.PerformLayout();
            this.tlpUniversal.ResumeLayout(false);
            this.tlpUniversal.PerformLayout();
            this.tlpOptionsSidebar.ResumeLayout(false);
            this.tlpOptionsSidebar.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.tlpOptionsContainer.ResumeLayout(false);
            this.tlpOptionsContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.tlpStartStop.ResumeLayout(false);
            this.tlpStartStop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mstripFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMazeToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpUniversal;
        private System.Windows.Forms.TableLayoutPanel tlpOptionsSidebar;
        private System.Windows.Forms.CheckedListBox lbxkAvailableEngines;
        private System.Windows.Forms.Label lblAvailableEngines;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.TableLayoutPanel tlpOptionsContainer;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblIterations;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomMazeToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpStartStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
    }
}

