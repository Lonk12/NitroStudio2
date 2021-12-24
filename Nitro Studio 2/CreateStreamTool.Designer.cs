namespace NitroStudio2 {
    partial class CreateStreamTool {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateStreamTool));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.impFileBox = new System.Windows.Forms.TextBox();
            this.impFileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.outFileBox = new System.Windows.Forms.TextBox();
            this.outFileButton = new System.Windows.Forms.Button();
            this.outputFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.7971F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.2029F));
            this.tableLayoutPanel1.Controls.Add(this.impFileBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.impFileButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 33);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 33);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // impFileBox
            // 
            this.impFileBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.impFileBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.impFileBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.impFileBox.Location = new System.Drawing.Point(4, 3);
            this.impFileBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.impFileBox.Name = "impFileBox";
            this.impFileBox.ReadOnly = true;
            this.impFileBox.Size = new System.Drawing.Size(336, 23);
            this.impFileBox.TabIndex = 0;
            // 
            // impFileButton
            // 
            this.impFileButton.BackColor = System.Drawing.Color.Navy;
            this.impFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.impFileButton.Location = new System.Drawing.Point(348, 3);
            this.impFileButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.impFileButton.Name = "impFileButton";
            this.impFileButton.Size = new System.Drawing.Size(50, 27);
            this.impFileButton.TabIndex = 1;
            this.impFileButton.Text = "...";
            this.impFileButton.UseVisualStyleBackColor = false;
            this.impFileButton.Click += new System.EventHandler(this.impFileButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(402, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input File:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(402, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.7971F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.2029F));
            this.tableLayoutPanel2.Controls.Add(this.outFileBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.outFileButton, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(14, 92);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(402, 33);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // outFileBox
            // 
            this.outFileBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.outFileBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outFileBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.outFileBox.Location = new System.Drawing.Point(4, 3);
            this.outFileBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.outFileBox.Name = "outFileBox";
            this.outFileBox.ReadOnly = true;
            this.outFileBox.Size = new System.Drawing.Size(336, 23);
            this.outFileBox.TabIndex = 0;
            // 
            // outFileButton
            // 
            this.outFileButton.BackColor = System.Drawing.Color.Navy;
            this.outFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outFileButton.Location = new System.Drawing.Point(348, 3);
            this.outFileButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.outFileButton.Name = "outFileButton";
            this.outFileButton.Size = new System.Drawing.Size(50, 27);
            this.outFileButton.TabIndex = 1;
            this.outFileButton.Text = "...";
            this.outFileButton.UseVisualStyleBackColor = false;
            this.outFileButton.Click += new System.EventHandler(this.outFileButton_Click);
            // 
            // outputFormat
            // 
            this.outputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.outputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.outputFormat.FormattingEnabled = true;
            this.outputFormat.Items.AddRange(new object[] {
            "PCM8",
            "PCM16",
            "IMA-ADPCM"});
            this.outputFormat.Location = new System.Drawing.Point(14, 155);
            this.outputFormat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.outputFormat.Name = "outputFormat";
            this.outputFormat.Size = new System.Drawing.Size(402, 23);
            this.outputFormat.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(14, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(402, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Format:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.BackColor = System.Drawing.Color.Navy;
            this.exportButton.Location = new System.Drawing.Point(14, 186);
            this.exportButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(402, 27);
            this.exportButton.TabIndex = 6;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // CreateStreamTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(430, 235);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "CreateStreamTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Stream";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox impFileBox;
        private System.Windows.Forms.Button impFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox outFileBox;
        private System.Windows.Forms.Button outFileButton;
        private System.Windows.Forms.ComboBox outputFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exportButton;
    }
}