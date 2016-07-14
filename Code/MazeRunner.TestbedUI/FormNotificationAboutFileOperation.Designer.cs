namespace MazeRunner.TestbedUI
{
    partial class FormNotificationAboutFileOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotificationAboutFileOperation));
            this.tlpMessages = new System.Windows.Forms.TableLayoutPanel();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.lblFileGeneratedSuccessfullyMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileLocation = new System.Windows.Forms.Label();
            this.lnkShowInFolder = new System.Windows.Forms.LinkLabel();
            this.tlpUniversal = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.tlpMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpUniversal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMessages
            // 
            this.tlpMessages.AutoSize = true;
            this.tlpMessages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMessages.BackColor = System.Drawing.SystemColors.Window;
            this.tlpMessages.ColumnCount = 4;
            this.tlpMessages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tlpMessages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMessages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tlpMessages.Controls.Add(this.pbInfo, 1, 1);
            this.tlpMessages.Controls.Add(this.lblFileGeneratedSuccessfullyMessage, 2, 1);
            this.tlpMessages.Controls.Add(this.tableLayoutPanel1, 2, 3);
            this.tlpMessages.Location = new System.Drawing.Point(0, 0);
            this.tlpMessages.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMessages.MinimumSize = new System.Drawing.Size(400, 0);
            this.tlpMessages.Name = "tlpMessages";
            this.tlpMessages.RowCount = 4;
            this.tlpMessages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tlpMessages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMessages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMessages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMessages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMessages.Size = new System.Drawing.Size(400, 76);
            this.tlpMessages.TabIndex = 0;
            // 
            // pbInfo
            // 
            this.pbInfo.Image = global::MazeRunner.TestbedUI.Properties.Resources.FileNotification;
            this.pbInfo.Location = new System.Drawing.Point(21, 11);
            this.pbInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pbInfo.Name = "pbInfo";
            this.tlpMessages.SetRowSpan(this.pbInfo, 4);
            this.pbInfo.Size = new System.Drawing.Size(32, 32);
            this.pbInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInfo.TabIndex = 1;
            this.pbInfo.TabStop = false;
            // 
            // lblFileGeneratedSuccessfullyMessage
            // 
            this.lblFileGeneratedSuccessfullyMessage.AutoSize = true;
            this.lblFileGeneratedSuccessfullyMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileGeneratedSuccessfullyMessage.Location = new System.Drawing.Point(56, 11);
            this.lblFileGeneratedSuccessfullyMessage.Name = "lblFileGeneratedSuccessfullyMessage";
            this.lblFileGeneratedSuccessfullyMessage.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblFileGeneratedSuccessfullyMessage.Size = new System.Drawing.Size(320, 16);
            this.lblFileGeneratedSuccessfullyMessage.TabIndex = 2;
            this.lblFileGeneratedSuccessfullyMessage.Text = "File generated successfully at:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblFileLocation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lnkShowInFolder, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(53, 37);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 29);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lblFileLocation
            // 
            this.lblFileLocation.AutoSize = true;
            this.lblFileLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileLocation.Location = new System.Drawing.Point(30, 0);
            this.lblFileLocation.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.lblFileLocation.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblFileLocation.Name = "lblFileLocation";
            this.lblFileLocation.Size = new System.Drawing.Size(266, 16);
            this.lblFileLocation.TabIndex = 5;
            this.lblFileLocation.Text = "C:\\some\\path\\somefile.exe";
            this.lblFileLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnkShowInFolder
            // 
            this.lnkShowInFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkShowInFolder.AutoSize = true;
            this.lnkShowInFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkShowInFolder.Location = new System.Drawing.Point(251, 16);
            this.lnkShowInFolder.Margin = new System.Windows.Forms.Padding(0);
            this.lnkShowInFolder.Name = "lnkShowInFolder";
            this.lnkShowInFolder.Size = new System.Drawing.Size(75, 13);
            this.lnkShowInFolder.TabIndex = 4;
            this.lnkShowInFolder.TabStop = true;
            this.lnkShowInFolder.Text = "&Show in folder";
            this.lnkShowInFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkShowInFolder_LinkClicked);
            // 
            // tlpUniversal
            // 
            this.tlpUniversal.AutoSize = true;
            this.tlpUniversal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpUniversal.ColumnCount = 1;
            this.tlpUniversal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUniversal.Controls.Add(this.tlpMessages, 0, 0);
            this.tlpUniversal.Controls.Add(this.btnOK, 0, 1);
            this.tlpUniversal.Location = new System.Drawing.Point(0, 0);
            this.tlpUniversal.Name = "tlpUniversal";
            this.tlpUniversal.RowCount = 2;
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUniversal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUniversal.Size = new System.Drawing.Size(400, 112);
            this.tlpUniversal.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(175, 83);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.btnOK.Size = new System.Drawing.Size(49, 22);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            // 
            // FormNotificationAboutFileOperation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(476, 193);
            this.Controls.Add(this.tlpUniversal);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNotificationAboutFileOperation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.tlpMessages.ResumeLayout(false);
            this.tlpMessages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpUniversal.ResumeLayout(false);
            this.tlpUniversal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMessages;
        private System.Windows.Forms.Label lblFileGeneratedSuccessfullyMessage;
        private System.Windows.Forms.TableLayoutPanel tlpUniversal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblFileLocation;
        public System.Windows.Forms.PictureBox pbInfo;
        public System.Windows.Forms.LinkLabel lnkShowInFolder;
        private System.Windows.Forms.Button btnOK;
    }
}