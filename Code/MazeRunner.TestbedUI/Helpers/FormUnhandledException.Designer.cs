namespace MazeRunner.TestbedUI.Helpers
{
    partial class FormUnhandledException
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
            this.lblOopsThatWasUnexpected = new System.Windows.Forms.Label();
            this.txtExceptionData = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTheApplicationWillNowTerminate = new System.Windows.Forms.Label();
            this.lblTechnicalDetails = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOopsThatWasUnexpected
            // 
            this.lblOopsThatWasUnexpected.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblOopsThatWasUnexpected, 2);
            this.lblOopsThatWasUnexpected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOopsThatWasUnexpected.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblOopsThatWasUnexpected.Location = new System.Drawing.Point(13, 10);
            this.lblOopsThatWasUnexpected.Name = "lblOopsThatWasUnexpected";
            this.lblOopsThatWasUnexpected.Size = new System.Drawing.Size(935, 23);
            this.lblOopsThatWasUnexpected.TabIndex = 0;
            this.lblOopsThatWasUnexpected.Text = "Oops!!! That was unexpected!";
            // 
            // txtExceptionData
            // 
            this.txtExceptionData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtExceptionData, 2);
            this.txtExceptionData.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExceptionData.Location = new System.Drawing.Point(13, 87);
            this.txtExceptionData.Multiline = true;
            this.txtExceptionData.Name = "txtExceptionData";
            this.txtExceptionData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExceptionData.Size = new System.Drawing.Size(935, 287);
            this.txtExceptionData.TabIndex = 1;
            this.txtExceptionData.WordWrap = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(878, 387);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 10, 0, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnClose.Size = new System.Drawing.Size(73, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblTheApplicationWillNowTerminate
            // 
            this.lblTheApplicationWillNowTerminate.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTheApplicationWillNowTerminate, 2);
            this.lblTheApplicationWillNowTerminate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTheApplicationWillNowTerminate.Location = new System.Drawing.Point(13, 38);
            this.lblTheApplicationWillNowTerminate.Name = "lblTheApplicationWillNowTerminate";
            this.lblTheApplicationWillNowTerminate.Size = new System.Drawing.Size(935, 13);
            this.lblTheApplicationWillNowTerminate.TabIndex = 3;
            this.lblTheApplicationWillNowTerminate.Text = "An unexpected exception occurred. The application could now terminate. Report thi" +
    "s bug at github.com/dsidirop.";
            // 
            // lblTechnicalDetails
            // 
            this.lblTechnicalDetails.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTechnicalDetails, 2);
            this.lblTechnicalDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTechnicalDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTechnicalDetails.Location = new System.Drawing.Point(13, 71);
            this.lblTechnicalDetails.Name = "lblTechnicalDetails";
            this.lblTechnicalDetails.Size = new System.Drawing.Size(935, 13);
            this.lblTechnicalDetails.TabIndex = 4;
            this.lblTechnicalDetails.Text = "Technical Details:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblOopsThatWasUnexpected, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTechnicalDetails, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtExceptionData, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblTheApplicationWillNowTerminate, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(961, 420);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // FormUnhandledException
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(961, 420);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = global::MazeRunner.TestbedUI.Properties.Resources.Appicon;
            this.Name = "FormUnhandledException";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unhandled Exception";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOopsThatWasUnexpected;
        private System.Windows.Forms.TextBox txtExceptionData;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTheApplicationWillNowTerminate;
        private System.Windows.Forms.Label lblTechnicalDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}