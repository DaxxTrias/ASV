﻿
namespace ARKViewer
{
    partial class frmAddLocalARK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddLocalARK));
            grpWrapper = new System.Windows.Forms.GroupBox();
            btnTestRcon = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            udRconPort = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            txtRconPassword = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtRconAddress = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btnClusterFolder = new System.Windows.Forms.Button();
            txtClusterFolder = new System.Windows.Forms.TextBox();
            lblOfflineName = new System.Windows.Forms.Label();
            lblFilename = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            btnAddARK = new System.Windows.Forms.Button();
            txtFilename = new System.Windows.Forms.TextBox();
            lblHeader = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            grpWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)udRconPort).BeginInit();
            SuspendLayout();
            // 
            // grpWrapper
            // 
            grpWrapper.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpWrapper.Controls.Add(btnTestRcon);
            grpWrapper.Controls.Add(label4);
            grpWrapper.Controls.Add(udRconPort);
            grpWrapper.Controls.Add(label3);
            grpWrapper.Controls.Add(txtRconPassword);
            grpWrapper.Controls.Add(label2);
            grpWrapper.Controls.Add(txtRconAddress);
            grpWrapper.Controls.Add(label1);
            grpWrapper.Controls.Add(btnClusterFolder);
            grpWrapper.Controls.Add(txtClusterFolder);
            grpWrapper.Controls.Add(lblOfflineName);
            grpWrapper.Controls.Add(lblFilename);
            grpWrapper.Controls.Add(txtName);
            grpWrapper.Controls.Add(btnAddARK);
            grpWrapper.Controls.Add(txtFilename);
            grpWrapper.Controls.Add(lblHeader);
            grpWrapper.Location = new System.Drawing.Point(14, 10);
            grpWrapper.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpWrapper.Name = "grpWrapper";
            grpWrapper.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            grpWrapper.Size = new System.Drawing.Size(478, 314);
            grpWrapper.TabIndex = 0;
            grpWrapper.TabStop = false;
            // 
            // btnTestRcon
            // 
            btnTestRcon.Location = new System.Drawing.Point(323, 278);
            btnTestRcon.Name = "btnTestRcon";
            btnTestRcon.Size = new System.Drawing.Size(132, 23);
            btnTestRcon.TabIndex = 15;
            btnTestRcon.Text = "Test Connection";
            btnTestRcon.UseVisualStyleBackColor = true;
            btnTestRcon.Click += btnTestRcon_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(369, 213);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(65, 15);
            label4.TabIndex = 13;
            label4.Text = "RCON Port";
            // 
            // udRconPort
            // 
            udRconPort.Location = new System.Drawing.Point(372, 233);
            udRconPort.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            udRconPort.Name = "udRconPort";
            udRconPort.Size = new System.Drawing.Size(83, 23);
            udRconPort.TabIndex = 14;
            udRconPort.Value = new decimal(new int[] { 27020, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(23, 259);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(93, 15);
            label3.TabIndex = 11;
            label3.Text = "RCON Password";
            // 
            // txtRconPassword
            // 
            txtRconPassword.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtRconPassword.Location = new System.Drawing.Point(25, 279);
            txtRconPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtRconPassword.Name = "txtRconPassword";
            txtRconPassword.PasswordChar = '●';
            txtRconPassword.Size = new System.Drawing.Size(174, 23);
            txtRconPassword.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(23, 213);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(98, 15);
            label2.TabIndex = 9;
            label2.Text = "RCON IP Address";
            // 
            // txtRconAddress
            // 
            txtRconAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtRconAddress.Location = new System.Drawing.Point(25, 233);
            txtRconAddress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtRconAddress.Name = "txtRconAddress";
            txtRconAddress.Size = new System.Drawing.Size(239, 23);
            txtRconAddress.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(21, 82);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 15);
            label1.TabIndex = 4;
            label1.Text = "Cluster Folder";
            // 
            // btnClusterFolder
            // 
            btnClusterFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClusterFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClusterFolder.Image = (System.Drawing.Image)resources.GetObject("btnClusterFolder.Image");
            btnClusterFolder.Location = new System.Drawing.Point(415, 93);
            btnClusterFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClusterFolder.Name = "btnClusterFolder";
            btnClusterFolder.Size = new System.Drawing.Size(41, 40);
            btnClusterFolder.TabIndex = 6;
            btnClusterFolder.UseVisualStyleBackColor = true;
            btnClusterFolder.Click += btnClusterFolder_Click;
            // 
            // txtClusterFolder
            // 
            txtClusterFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtClusterFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtClusterFolder.Location = new System.Drawing.Point(23, 100);
            txtClusterFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtClusterFolder.Name = "txtClusterFolder";
            txtClusterFolder.ReadOnly = true;
            txtClusterFolder.Size = new System.Drawing.Size(384, 22);
            txtClusterFolder.TabIndex = 5;
            // 
            // lblOfflineName
            // 
            lblOfflineName.AutoSize = true;
            lblOfflineName.Location = new System.Drawing.Point(21, 141);
            lblOfflineName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblOfflineName.Name = "lblOfflineName";
            lblOfflineName.Size = new System.Drawing.Size(78, 15);
            lblOfflineName.TabIndex = 7;
            lblOfflineName.Text = "Offline Name";
            // 
            // lblFilename
            // 
            lblFilename.AutoSize = true;
            lblFilename.Location = new System.Drawing.Point(21, 19);
            lblFilename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFilename.Name = "lblFilename";
            lblFilename.Size = new System.Drawing.Size(55, 15);
            lblFilename.TabIndex = 1;
            lblFilename.Text = "Filename";
            // 
            // txtName
            // 
            txtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtName.Location = new System.Drawing.Point(23, 161);
            txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(425, 23);
            txtName.TabIndex = 8;
            // 
            // btnAddARK
            // 
            btnAddARK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddARK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddARK.Image = (System.Drawing.Image)resources.GetObject("btnAddARK.Image");
            btnAddARK.Location = new System.Drawing.Point(413, 34);
            btnAddARK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAddARK.Name = "btnAddARK";
            btnAddARK.Size = new System.Drawing.Size(41, 40);
            btnAddARK.TabIndex = 3;
            btnAddARK.UseVisualStyleBackColor = true;
            btnAddARK.Click += btnAddARK_Click;
            // 
            // txtFilename
            // 
            txtFilename.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtFilename.Location = new System.Drawing.Point(21, 41);
            txtFilename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtFilename.Name = "txtFilename";
            txtFilename.ReadOnly = true;
            txtFilename.Size = new System.Drawing.Size(384, 22);
            txtFilename.TabIndex = 2;
            // 
            // lblHeader
            // 
            lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblHeader.BackColor = System.Drawing.Color.Aqua;
            lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblHeader.Location = new System.Drawing.Point(-2, 0);
            lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(482, 7);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "   ";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Location = new System.Drawing.Point(406, 330);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(88, 27);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnSave.Enabled = false;
            btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnSave.Location = new System.Drawing.Point(312, 330);
            btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(88, 27);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // frmAddLocalARK
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(506, 371);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(grpWrapper);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximumSize = new System.Drawing.Size(522, 410);
            MinimumSize = new System.Drawing.Size(522, 410);
            Name = "frmAddLocalARK";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Add ARK Save File";
            FormClosed += frmAddLocalARK_FormClosed;
            grpWrapper.ResumeLayout(false);
            grpWrapper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)udRconPort).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpWrapper;
        private System.Windows.Forms.Button btnAddARK;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblOfflineName;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClusterFolder;
        private System.Windows.Forms.TextBox txtClusterFolder;
        private System.Windows.Forms.NumericUpDown udRconPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRconPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRconAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTestRcon;
    }
}