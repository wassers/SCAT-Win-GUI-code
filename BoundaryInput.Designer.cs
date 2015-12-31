namespace SCAT_2._0
{
    partial class BoundaryInput
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

        private WebKit.WebKitBrowser boundarySelectBrowser;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoundaryInput));
            this.boundarySelectBrowser = new WebKit.WebKitBrowser();
            this.SuspendLayout();
            // 
            // boundarySelectBrowser
            // 
            this.boundarySelectBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.boundarySelectBrowser.BackColor = System.Drawing.Color.White;
            this.boundarySelectBrowser.Location = new System.Drawing.Point(1, 1);
            this.boundarySelectBrowser.Name = "boundarySelectBrowser";
            this.boundarySelectBrowser.Size = new System.Drawing.Size(1003, 584);
            this.boundarySelectBrowser.TabIndex = 0;
            this.boundarySelectBrowser.Url = new System.Uri("file:///C:/Users/me/Documents/Work/SCAT/SCAT%202.0/SCAT%202.0/bin/Debug/SCATboundary" +
                    ".html", System.UriKind.Absolute);
            // 
            // BoundaryInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 516);
            this.Controls.Add(this.boundarySelectBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "BoundaryInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Boundary Points";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
    }
}