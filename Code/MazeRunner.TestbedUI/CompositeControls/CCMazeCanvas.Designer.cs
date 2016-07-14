namespace MazeRunner.TestbedUI.CompositeControls
{
    partial class CCMazeCanvas
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMesh = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMesh.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMesh
            // 
            this.tlpMesh.AutoSize = true;
            this.tlpMesh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMesh.BackColor = System.Drawing.Color.Transparent;
            this.tlpMesh.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpMesh.ColumnCount = 2;
            this.tlpMesh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMesh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMesh.Location = new System.Drawing.Point(0, 0);
            this.tlpMesh.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMesh.Name = "tlpMesh";
            this.tlpMesh.RowCount = 2;
            this.tlpMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMesh.Size = new System.Drawing.Size(58, 43);
            this.tlpMesh.TabIndex = 0;
            // 
            // CCMazeCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tlpMesh);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CCMazeCanvas";
            this.Size = new System.Drawing.Size(58, 43);
            this.tlpMesh.ResumeLayout(false);
            this.tlpMesh.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMesh;
    }
}
