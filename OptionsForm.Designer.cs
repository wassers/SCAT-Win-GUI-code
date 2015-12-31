namespace SCAT_2._0
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.optGenLabel = new System.Windows.Forms.LinkLabel();
            this.optNetLabel = new System.Windows.Forms.LinkLabel();
            this.optRepLabel = new System.Windows.Forms.LinkLabel();
            this.optStatLabel = new System.Windows.Forms.LinkLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.optMapLabel = new System.Windows.Forms.LinkLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.optMapLabel);
            this.splitContainer1.Panel1.Controls.Add(this.optStatLabel);
            this.splitContainer1.Panel1.Controls.Add(this.optRepLabel);
            this.splitContainer1.Panel1.Controls.Add(this.optNetLabel);
            this.splitContainer1.Panel1.Controls.Add(this.optGenLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Size = new System.Drawing.Size(528, 401);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 0;
            // 
            // optGenLabel
            // 
            this.optGenLabel.AutoSize = true;
            this.optGenLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.optGenLabel.Location = new System.Drawing.Point(37, 87);
            this.optGenLabel.Name = "optGenLabel";
            this.optGenLabel.Size = new System.Drawing.Size(44, 13);
            this.optGenLabel.TabIndex = 0;
            this.optGenLabel.TabStop = true;
            this.optGenLabel.Text = "General";
            this.optGenLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // optNetLabel
            // 
            this.optNetLabel.AutoSize = true;
            this.optNetLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.optNetLabel.Location = new System.Drawing.Point(37, 134);
            this.optNetLabel.Name = "optNetLabel";
            this.optNetLabel.Size = new System.Drawing.Size(47, 13);
            this.optNetLabel.TabIndex = 1;
            this.optNetLabel.TabStop = true;
            this.optNetLabel.Text = "Network";
            // 
            // optRepLabel
            // 
            this.optRepLabel.AutoSize = true;
            this.optRepLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.optRepLabel.Location = new System.Drawing.Point(37, 177);
            this.optRepLabel.Name = "optRepLabel";
            this.optRepLabel.Size = new System.Drawing.Size(53, 13);
            this.optRepLabel.TabIndex = 2;
            this.optRepLabel.TabStop = true;
            this.optRepLabel.Text = "Reporting";
            // 
            // optStatLabel
            // 
            this.optStatLabel.AutoSize = true;
            this.optStatLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.optStatLabel.Location = new System.Drawing.Point(37, 226);
            this.optStatLabel.Name = "optStatLabel";
            this.optStatLabel.Size = new System.Drawing.Size(49, 13);
            this.optStatLabel.TabIndex = 3;
            this.optStatLabel.TabStop = true;
            this.optStatLabel.Text = "Statistics";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 401);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // optMapLabel
            // 
            this.optMapLabel.AutoSize = true;
            this.optMapLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.optMapLabel.Location = new System.Drawing.Point(37, 271);
            this.optMapLabel.Name = "optMapLabel";
            this.optMapLabel.Size = new System.Drawing.Size(33, 13);
            this.optMapLabel.TabIndex = 4;
            this.optMapLabel.TabStop = true;
            this.optMapLabel.Text = "Maps";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1 (Default)",
            "2",
            "3 (Max)"});
            this.comboBox1.Location = new System.Drawing.Point(117, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(76, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "# of concurrent jobs";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 401);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCAT Options";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel optGenLabel;
        private System.Windows.Forms.LinkLabel optStatLabel;
        private System.Windows.Forms.LinkLabel optRepLabel;
        private System.Windows.Forms.LinkLabel optNetLabel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.LinkLabel optMapLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}