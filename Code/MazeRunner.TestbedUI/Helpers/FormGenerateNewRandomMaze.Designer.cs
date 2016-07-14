using System.Windows.Forms;
using MazeRunner.TestbedUI.Controls;

namespace MazeRunner.TestbedUI.Helpers
{
    partial class FormGenerateNewRandomMaze
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
            this.tlpUniversal = new System.Windows.Forms.TableLayoutPanel();
            this.cclblEnterMazeSpecs = new MazeRunner.TestbedUI.Controls.CCLabelWithBottomBorderOnly();
            this.tlpFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlpValue = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudDensity = new System.Windows.Forms.NumericUpDown();
            this.lblDensity = new System.Windows.Forms.Label();
            this.tlpUniversal.SuspendLayout();
            this.tlpFooter.SuspendLayout();
            this.tlpValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDensity)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpUniversal
            // 
            this.tlpUniversal.AutoSize = true;
            this.tlpUniversal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpUniversal.ColumnCount = 1;
            this.tlpUniversal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUniversal.Controls.Add(this.cclblEnterMazeSpecs, 0, 0);
            this.tlpUniversal.Controls.Add(this.tlpFooter, 0, 2);
            this.tlpUniversal.Controls.Add(this.tlpValue, 0, 1);
            this.tlpUniversal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tlpUniversal.Location = new System.Drawing.Point(0, 0);
            this.tlpUniversal.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUniversal.Name = "tlpUniversal";
            this.tlpUniversal.RowCount = 2;
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUniversal.Size = new System.Drawing.Size(444, 122);
            this.tlpUniversal.TabIndex = 0;
            // 
            // cclblEnterMazeSpecs
            // 
            this.cclblEnterMazeSpecs.BackColor = System.Drawing.Color.White;
            this.cclblEnterMazeSpecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cclblEnterMazeSpecs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cclblEnterMazeSpecs.Location = new System.Drawing.Point(0, 0);
            this.cclblEnterMazeSpecs.Margin = new System.Windows.Forms.Padding(0);
            this.cclblEnterMazeSpecs.Name = "cclblEnterMazeSpecs";
            this.cclblEnterMazeSpecs.Padding = new System.Windows.Forms.Padding(10, 13, 13, 13);
            this.cclblEnterMazeSpecs.Size = new System.Drawing.Size(444, 42);
            this.cclblEnterMazeSpecs.TabIndex = 0;
            this.cclblEnterMazeSpecs.Text = "Enter Maze Specs";
            this.cclblEnterMazeSpecs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpFooter
            // 
            this.tlpFooter.AutoSize = true;
            this.tlpFooter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpFooter.BackColor = System.Drawing.Color.DarkGray;
            this.tlpFooter.ColumnCount = 4;
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpFooter.Controls.Add(this.btnOK, 1, 1);
            this.tlpFooter.Controls.Add(this.btnCancel, 2, 1);
            this.tlpFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFooter.Location = new System.Drawing.Point(0, 89);
            this.tlpFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFooter.Name = "tlpFooter";
            this.tlpFooter.RowCount = 3;
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpFooter.Size = new System.Drawing.Size(444, 33);
            this.tlpFooter.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(306, 5);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnOK.Size = new System.Drawing.Size(61, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(373, 5);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnCancel.Size = new System.Drawing.Size(61, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tlpValue
            // 
            this.tlpValue.AutoSize = true;
            this.tlpValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpValue.ColumnCount = 6;
            this.tlpValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpValue.Controls.Add(this.lblHeight, 2, 0);
            this.tlpValue.Controls.Add(this.lblWidth, 0, 0);
            this.tlpValue.Controls.Add(this.nudWidth, 1, 0);
            this.tlpValue.Controls.Add(this.nudHeight, 3, 0);
            this.tlpValue.Controls.Add(this.nudDensity, 5, 0);
            this.tlpValue.Controls.Add(this.lblDensity, 4, 0);
            this.tlpValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpValue.Location = new System.Drawing.Point(10, 52);
            this.tlpValue.Margin = new System.Windows.Forms.Padding(10);
            this.tlpValue.Name = "tlpValue";
            this.tlpValue.RowCount = 1;
            this.tlpValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpValue.Size = new System.Drawing.Size(424, 27);
            this.tlpValue.TabIndex = 0;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeight.Location = new System.Drawing.Point(134, 0);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblHeight.Size = new System.Drawing.Size(52, 27);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "&Height:";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWidth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidth.Location = new System.Drawing.Point(3, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(39, 27);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "&Width:";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(48, 3);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(80, 21);
            this.nudWidth.TabIndex = 1;
            this.nudWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudWidth.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(192, 3);
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(80, 21);
            this.nudHeight.TabIndex = 2;
            this.nudHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudHeight.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // nudDensity
            // 
            this.nudDensity.DecimalPlaces = 2;
            this.nudDensity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDensity.Location = new System.Drawing.Point(341, 3);
            this.nudDensity.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDensity.Name = "nudDensity";
            this.nudDensity.Size = new System.Drawing.Size(80, 21);
            this.nudDensity.TabIndex = 4;
            this.nudDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDensity.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lblDensity
            // 
            this.lblDensity.AutoSize = true;
            this.lblDensity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDensity.Location = new System.Drawing.Point(278, 0);
            this.lblDensity.Name = "lblDensity";
            this.lblDensity.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblDensity.Size = new System.Drawing.Size(57, 27);
            this.lblDensity.TabIndex = 5;
            this.lblDensity.Text = "&Density:";
            this.lblDensity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormGenerateNewRandomMaze
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(924, 226);
            this.Controls.Add(this.tlpUniversal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::MazeRunner.TestbedUI.Properties.Resources.Appicon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGenerateNewRandomMaze";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Maze";
            this.tlpUniversal.ResumeLayout(false);
            this.tlpUniversal.PerformLayout();
            this.tlpFooter.ResumeLayout(false);
            this.tlpFooter.PerformLayout();
            this.tlpValue.ResumeLayout(false);
            this.tlpValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpUniversal;
        private CCLabelWithBottomBorderOnly cclblEnterMazeSpecs;
        private System.Windows.Forms.TableLayoutPanel tlpFooter;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private TableLayoutPanel tlpValue;
        private Label lblWidth;
        private Label lblHeight;
        private NumericUpDown nudWidth;
        private NumericUpDown nudHeight;
        private NumericUpDown nudDensity;
        private Label lblDensity;
    }
}