namespace NitroStudio2
{

    partial class StreamPlayer
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
           
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamPlayer));
            this.SuspendLayout();
            // 
            // StreamPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(737, 366);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "StreamPlayer";
            this.Text = "s";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onClose);
            this.ResumeLayout(false);

        }

        #endregion

        internal AxWMPLib.AxWindowsMediaPlayer wmp;

    }

}