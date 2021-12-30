namespace NitroStudio2 {
    partial class SequenceRecorder {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceRecorder));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loopsBox = new System.Windows.Forms.NumericUpDown();
            this.fadeBox = new System.Windows.Forms.CheckBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.loopsBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.fadeBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 14);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(351, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(249, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fade Out:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number Of Loops:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // loopsBox
            // 
            this.loopsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loopsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loopsBox.ForeColor = System.Drawing.SystemColors.Control;
            this.loopsBox.Location = new System.Drawing.Point(4, 25);
            this.loopsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loopsBox.Name = "loopsBox";
            this.loopsBox.Size = new System.Drawing.Size(237, 23);
            this.loopsBox.TabIndex = 2;
            this.loopsBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fadeBox
            // 
            this.fadeBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fadeBox.Checked = true;
            this.fadeBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fadeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fadeBox.Location = new System.Drawing.Point(249, 25);
            this.fadeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fadeBox.Name = "fadeBox";
            this.fadeBox.Size = new System.Drawing.Size(98, 27);
            this.fadeBox.TabIndex = 3;
            this.fadeBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fadeBox.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.Navy;
            this.exportButton.Location = new System.Drawing.Point(9, 75);
            this.exportButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(351, 27);
            this.exportButton.TabIndex = 1;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // SequenceRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(368, 112);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SequenceRecorder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sequence Recorder";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loopsBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown loopsBox;
        private System.Windows.Forms.CheckBox fadeBox;
        private System.Windows.Forms.Button exportButton;
    }
}