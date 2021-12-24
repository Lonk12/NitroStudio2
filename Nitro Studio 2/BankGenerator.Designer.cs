namespace NitroStudio2 {
    partial class BankGenerator {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankGenerator));
            this.instruments = new System.Windows.Forms.DataGridView();
            this.play = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bank = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.instrument = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.newId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waveArchiveMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.createBnk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.instruments)).BeginInit();
            this.SuspendLayout();
            // 
            // instruments
            // 
            this.instruments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instruments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.instruments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instruments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.play,
            this.bank,
            this.instrument,
            this.newId,
            this.waveArchiveMode});
            this.instruments.Location = new System.Drawing.Point(18, 39);
            this.instruments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.instruments.Name = "instruments";
            this.instruments.Size = new System.Drawing.Size(610, 316);
            this.instruments.TabIndex = 3;
            // 
            // play
            // 
            this.play.HeaderText = "Play";
            this.play.Name = "play";
            this.play.Text = "Play";
            this.play.ToolTipText = "Play the instrument sample.";
            this.play.UseColumnTextForButtonValue = true;
            this.play.Width = 35;
            // 
            // bank
            // 
            this.bank.HeaderText = "Bank";
            this.bank.Name = "bank";
            this.bank.ToolTipText = "Bank to copy the instrument from.";
            this.bank.Width = 200;
            // 
            // instrument
            // 
            this.instrument.HeaderText = "Instrument";
            this.instrument.Name = "instrument";
            this.instrument.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.instrument.ToolTipText = "Instrument to copy.";
            // 
            // newId
            // 
            this.newId.HeaderText = "New Id";
            this.newId.MaxInputLength = 5;
            this.newId.Name = "newId";
            this.newId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.newId.ToolTipText = "New Id for the instrument.";
            this.newId.Width = 50;
            // 
            // waveArchiveMode
            // 
            this.waveArchiveMode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.waveArchiveMode.HeaderText = "Wave Archive Mode";
            this.waveArchiveMode.Items.AddRange(new object[] {
            "Use Generated Wave Archive",
            "Reference Original Wave Archive"});
            this.waveArchiveMode.Name = "waveArchiveMode";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(14, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(610, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Instruments:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createBnk
            // 
            this.createBnk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createBnk.BackColor = System.Drawing.Color.Navy;
            this.createBnk.Location = new System.Drawing.Point(18, 362);
            this.createBnk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.createBnk.Name = "createBnk";
            this.createBnk.Size = new System.Drawing.Size(610, 27);
            this.createBnk.TabIndex = 4;
            this.createBnk.Text = "Create Bank And Wave Archive";
            this.createBnk.UseVisualStyleBackColor = false;
            this.createBnk.Click += new System.EventHandler(this.CreateBnk_Click);
            // 
            // BankGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(644, 396);
            this.Controls.Add(this.createBnk);
            this.Controls.Add(this.instruments);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "BankGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Generator";
            ((System.ComponentModel.ISupportInitialize)(this.instruments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DataGridView instruments;
        public System.Windows.Forms.Button createBnk;
        private System.Windows.Forms.DataGridViewButtonColumn play;
        private System.Windows.Forms.DataGridViewComboBoxColumn bank;
        private System.Windows.Forms.DataGridViewComboBoxColumn instrument;
        private System.Windows.Forms.DataGridViewTextBoxColumn newId;
        private System.Windows.Forms.DataGridViewComboBoxColumn waveArchiveMode;
    }
}