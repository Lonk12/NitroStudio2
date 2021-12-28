namespace NitroStudio2
{
    partial class AboutWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.Title = new System.Windows.Forms.Label();
            this.Credits = new System.Windows.Forms.TextBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoEllipsis = true;
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.Click += new System.EventHandler(this.label1_Click);
            // 
            // Credits
            // 
            resources.ApplyResources(this.Credits, "Credits");
            this.Credits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.Credits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Credits.Cursor = System.Windows.Forms.Cursors.Default;
            this.Credits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.Credits.Name = "Credits";
            this.Credits.ReadOnly = true;
            this.Credits.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Logo
            // 
            this.Logo.BackgroundImage = global::NitroStudio2.Properties.Resources.Ico;
            resources.ApplyResources(this.Logo, "Logo");
            this.Logo.Name = "Logo";
            this.Logo.TabStop = false;
            // 
            // AboutWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.Credits);
            this.Controls.Add(this.Title);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.Name = "AboutWindow";
            this.Load += new System.EventHandler(this.AboutWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TextBox Credits;
        private System.Windows.Forms.PictureBox Logo;
    }
}