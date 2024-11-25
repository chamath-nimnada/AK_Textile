namespace AK_Textile
{
    partial class MainForm
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
            this.MainPanal = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // MainPanal
            // 
            this.MainPanal.Location = new System.Drawing.Point(-2, -1);
            this.MainPanal.Name = "MainPanal";
            this.MainPanal.Size = new System.Drawing.Size(1308, 740);
            this.MainPanal.TabIndex = 0;
            this.MainPanal.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanal_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 738);
            this.Controls.Add(this.MainPanal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "AK Textile PVT(LTD)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanal;
    }
}

