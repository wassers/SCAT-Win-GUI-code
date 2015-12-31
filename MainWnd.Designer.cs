using System.Windows.Forms;
namespace SCAT_2._0
{
    public struct latLongProb 
    {
        double[] latLongProbContainer;
        public bool initialize(string[] value)
        {
            try
            {
                latLongProbContainer = new double[3];
                latLongProbContainer[0] = System.Convert.ToDouble(value[0]);
                latLongProbContainer[1] = System.Convert.ToDouble(value[1]);
                latLongProbContainer[2] = System.Convert.ToDouble(value[2]);
                return true;
            }
            catch (System.Exception e)
            {                
                return false;
            }
        }
        public double getLatitute()
        {
            return latLongProbContainer[0];
        }
        public double getLongitude()
        {
            return latLongProbContainer[1];
        }
        public double getProb()
        {
            return latLongProbContainer[2];
        }
        public double[] getLatLongProbContainer()
        {
            return latLongProbContainer;
        }
    }

    public struct outputPointCollection
    {        
        public System.Collections.ArrayList pointCollection, latCollection, longCollection, probCollection;
                
        //public double medianLat, medianLong;
        public void initialize()
        {
            pointCollection = new System.Collections.ArrayList();
            latCollection = new System.Collections.ArrayList();
            longCollection = new System.Collections.ArrayList();
            probCollection = new System.Collections.ArrayList();
        }
       
        public bool Add(string[] pointToAdd)
        {
            latLongProb llpToAdd = new latLongProb();
            if (llpToAdd.initialize(pointToAdd))
            {
                pointCollection.Add(llpToAdd);
                latCollection.Add(pointToAdd[0]);
                longCollection.Add(pointToAdd[1]);
                probCollection.Add(pointToAdd[2]);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public double[] computeMedianProb(int startIndex)
        {            
            pointCollection.Sort(startIndex,pointCollection.Count-startIndex, new latLongComparer());            
            return ((latLongProb) (pointCollection[startIndex + (pointCollection.Count-startIndex)/2])).getLatLongProbContainer();
        }

        public double[] computeMedian(int startIndex)
        {
            System.Collections.ArrayList tempLats = latCollection.GetRange(startIndex, latCollection.Count - startIndex);
            tempLats.Sort();
            System.Collections.ArrayList tempLongs = longCollection.GetRange(startIndex, longCollection.Count - startIndex);
            tempLongs.Sort();
            double[] median = new double[2];
            if (tempLats.Count % 2 == 0)
            {
                median[0] = (System.Convert.ToDouble(tempLats[tempLats.Count / 2]) + System.Convert.ToDouble(tempLats[(tempLats.Count / 2) - 1])) / 2;
                median[1] = (System.Convert.ToDouble(tempLongs[tempLongs.Count / 2]) + System.Convert.ToDouble(tempLongs[(tempLongs.Count / 2) - 1])) / 2;
            }
            else
            {
                median[0] = System.Convert.ToDouble(tempLats[tempLats.Count / 2]);
                median[1] = System.Convert.ToDouble(tempLongs[tempLongs.Count / 2]);
            }
            return median;
        }
        public double[] computeCentroid(int startIndex)
        {
            double[] centroidPoint = new double[2];
            double latCount=0, longCount = 0;
            int k=0;
            for (k = startIndex; k < latCollection.Count; k++)
            {
                latCount += System.Convert.ToDouble(latCollection[k]);
                longCount += System.Convert.ToDouble(longCollection[k]);
            }
            centroidPoint[0] = latCount / (k-startIndex) ;
            centroidPoint[1] = longCount / (k-startIndex);
            return centroidPoint;
        }
    };

    public class latLongComparer : System.Collections.IComparer 
    {
        int System.Collections.IComparer.Compare(object x, object y)
        {
            return ((latLongProb)x).getProb().CompareTo(((latLongProb)y).getProb());

        }
    }
    partial class MainWnd
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
            if (jobIDList.Count > 0)
            {
                MessageBox.Show("There are " + jobIDList.Count + " scat processes running. Please stop these runs or wait for them to complete before closing this program!"); 
            }
            else
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer10 = new System.Windows.Forms.SplitContainer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nLociLbl = new System.Windows.Forms.Label();
            this.nLociTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uEndTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uStartTxtBox = new System.Windows.Forms.TextBox();
            this.newJobBtn = new System.Windows.Forms.Button();
            this.nIterLbl = new System.Windows.Forms.Label();
            this.nIterTxtBox = new System.Windows.Forms.TextBox();
            this.nThinLbl = new System.Windows.Forms.Label();
            this.nThinTxtBox = new System.Windows.Forms.TextBox();
            this.nBurnLbl = new System.Windows.Forms.Label();
            this.nBurnTxtBox = new System.Windows.Forms.TextBox();
            this.bFileTxtBox = new System.Windows.Forms.TextBox();
            this.bFileLbl = new System.Windows.Forms.LinkLabel();
            this.locFileTxtBox = new System.Windows.Forms.TextBox();
            this.selLocFileLbl = new System.Windows.Forms.LinkLabel();
            this.genFileTxtBox = new System.Windows.Forms.TextBox();
            this.selGenFileLbl = new System.Windows.Forms.LinkLabel();
            this.jobFldrTxtBox = new System.Windows.Forms.TextBox();
            this.selJobFldrLbl = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.jobNameTxtBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.hIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtJobCreatedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sJobNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOutputFilesPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.sInputGenotypeFilePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.sInputRegionFilePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.sBoundaryFilePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.iNLoci1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iBurnIterationsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iThinningsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iNIterationsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sRestriction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sSuppArgs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iJobStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iProcIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iUnknownStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iUnknownStopDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtJobQueuedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtJobStartedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtJobEndedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scatdbDataSetL2 = new SCAT_2._0.scatdbDataSetL2();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.scat2Stop1 = new System.Windows.Forms.Button();
            this.scat2Out1 = new System.Windows.Forms.TextBox();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.label9 = new System.Windows.Forms.Label();
            this.scat2Stop3 = new System.Windows.Forms.Button();
            this.scat2Out3 = new System.Windows.Forms.TextBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.label8 = new System.Windows.Forms.Label();
            this.scat2Stop2 = new System.Windows.Forms.Button();
            this.scat2Out2 = new System.Windows.Forms.TextBox();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.scat2Stop4 = new System.Windows.Forms.Button();
            this.scat2Out4 = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.voronoiBFile = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button5 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new SCAT_2._0.TreeViewMS();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.splitContainer11 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.maxSessCtl = new System.Windows.Forms.NumericUpDown();
            this.scatdbDataSetLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scatdbDataSetL = new SCAT_2._0.scatdbDataSetL();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseFolderChooser = new System.Windows.Forms.FolderBrowserDialog();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            this.jobListTableAdapter1 = new SCAT_2._0.scatdbDataSetLTableAdapters.JobListTableAdapter();
            this.dataGridViewJobStatusTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lActionsDataGridViewLinkColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.jobListTableAdapter = new SCAT_2._0.scatdbDataSetL2TableAdapters.JobListTableAdapter();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();			
			this.label21 = new System.Windows.Forms.Label();			
			this.nGridTBox = new System.Windows.Forms.TextBox();
			this.nBurnTBox = new System.Windows.Forms.TextBox();
			this.nIterTBox = new System.Windows.Forms.TextBox();			
            this.vLengthTBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
            this.splitContainer10.Panel1.SuspendLayout();
            this.splitContainer10.Panel2.SuspendLayout();
            this.splitContainer10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scatdbDataSetL2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSessCtl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scatdbDataSetLBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scatdbDataSetL)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(-1, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1228, 610);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.splitContainer10);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1220, 584);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SCAT Job List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer10
            // 
            this.splitContainer10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer10.Location = new System.Drawing.Point(3, 3);
            this.splitContainer10.Name = "splitContainer10";
            this.splitContainer10.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer10.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer10.Panel1.Controls.Add(this.label5);
            this.splitContainer10.Panel1.Controls.Add(this.nLociLbl);
            this.splitContainer10.Panel1.Controls.Add(this.nLociTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.label4);
            this.splitContainer10.Panel1.Controls.Add(this.uEndTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.label3);
            this.splitContainer10.Panel1.Controls.Add(this.uStartTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.newJobBtn);
            this.splitContainer10.Panel1.Controls.Add(this.nIterLbl);
            this.splitContainer10.Panel1.Controls.Add(this.nIterTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.nThinLbl);
            this.splitContainer10.Panel1.Controls.Add(this.nThinTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.nBurnLbl);
            this.splitContainer10.Panel1.Controls.Add(this.nBurnTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.bFileTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.bFileLbl);
            this.splitContainer10.Panel1.Controls.Add(this.locFileTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.selLocFileLbl);
            this.splitContainer10.Panel1.Controls.Add(this.genFileTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.selGenFileLbl);
            this.splitContainer10.Panel1.Controls.Add(this.jobFldrTxtBox);
            this.splitContainer10.Panel1.Controls.Add(this.selJobFldrLbl);
            this.splitContainer10.Panel1.Controls.Add(this.label2);
            this.splitContainer10.Panel1.Controls.Add(this.jobNameTxtBox);
            // 
            // splitContainer10.Panel2
            // 
            this.splitContainer10.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer10.Size = new System.Drawing.Size(1214, 578);
            this.splitContainer10.SplitterDistance = 36;
            this.splitContainer10.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "None",
            "Savannah",
            "Forest"});
            this.comboBox1.Location = new System.Drawing.Point(970, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(73, 21);
            this.comboBox1.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(966, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Restrictions";
            this.label5.Click += new System.EventHandler(this.label5_Click_1);
            // 
            // nLociLbl
            // 
            this.nLociLbl.AutoSize = true;
            this.nLociLbl.Location = new System.Drawing.Point(597, 1);
            this.nLociLbl.Name = "nLociLbl";
            this.nLociLbl.Size = new System.Drawing.Size(40, 13);
            this.nLociLbl.TabIndex = 38;
            this.nLociLbl.Text = "# Loci:";
            // 
            // nLociTxtBox
            // 
            this.nLociTxtBox.Location = new System.Drawing.Point(600, 15);
            this.nLociTxtBox.Name = "nLociTxtBox";
            this.nLociTxtBox.Size = new System.Drawing.Size(37, 20);
            this.nLociTxtBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(889, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Unknown end";
            // 
            // uEndTxtBox
            // 
            this.uEndTxtBox.Location = new System.Drawing.Point(892, 15);
            this.uEndTxtBox.Name = "uEndTxtBox";
            this.uEndTxtBox.Size = new System.Drawing.Size(71, 20);
            this.uEndTxtBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(813, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Unknown start";
            // 
            // uStartTxtBox
            // 
            this.uStartTxtBox.Location = new System.Drawing.Point(816, 15);
            this.uStartTxtBox.Name = "uStartTxtBox";
            this.uStartTxtBox.Size = new System.Drawing.Size(68, 20);
            this.uStartTxtBox.TabIndex = 10;
            // 
            // newJobBtn
            // 
            this.newJobBtn.Location = new System.Drawing.Point(1051, 11);
            this.newJobBtn.Name = "newJobBtn";
            this.newJobBtn.Size = new System.Drawing.Size(89, 25);
            this.newJobBtn.TabIndex = 17;
            this.newJobBtn.Text = "Add Job To List";
            this.newJobBtn.UseVisualStyleBackColor = true;
            this.newJobBtn.Click += new System.EventHandler(this.newJobBtn_Click);
            // 
            // nIterLbl
            // 
            this.nIterLbl.AutoSize = true;
            this.nIterLbl.Location = new System.Drawing.Point(753, 1);
            this.nIterLbl.Name = "nIterLbl";
            this.nIterLbl.Size = new System.Drawing.Size(63, 13);
            this.nIterLbl.TabIndex = 32;
            this.nIterLbl.Text = "# Iterations:";
            // 
            // nIterTxtBox
            // 
            this.nIterTxtBox.Location = new System.Drawing.Point(756, 15);
            this.nIterTxtBox.Name = "nIterTxtBox";
            this.nIterTxtBox.Size = new System.Drawing.Size(50, 20);
            this.nIterTxtBox.TabIndex = 9;
            this.nIterTxtBox.Text = "100";
            // 
            // nThinLbl
            // 
            this.nThinLbl.AutoSize = true;
            this.nThinLbl.Location = new System.Drawing.Point(704, 1);
            this.nThinLbl.Name = "nThinLbl";
            this.nThinLbl.Size = new System.Drawing.Size(51, 13);
            this.nThinLbl.TabIndex = 30;
            this.nThinLbl.Text = "Thinning:";
            // 
            // nThinTxtBox
            // 
            this.nThinTxtBox.Location = new System.Drawing.Point(707, 15);
            this.nThinTxtBox.Name = "nThinTxtBox";
            this.nThinTxtBox.Size = new System.Drawing.Size(39, 20);
            this.nThinTxtBox.TabIndex = 8;
            this.nThinTxtBox.Text = "10";
            this.nThinTxtBox.TextChanged += new System.EventHandler(this.nThinTxtBox_TextChanged);
            // 
            // nBurnLbl
            // 
            this.nBurnLbl.AutoSize = true;
            this.nBurnLbl.Location = new System.Drawing.Point(642, 1);
            this.nBurnLbl.Name = "nBurnLbl";
            this.nBurnLbl.Size = new System.Drawing.Size(58, 13);
            this.nBurnLbl.TabIndex = 28;
            this.nBurnLbl.Text = "# Burn-ins:";
            // 
            // nBurnTxtBox
            // 
            this.nBurnTxtBox.Location = new System.Drawing.Point(645, 15);
            this.nBurnTxtBox.Name = "nBurnTxtBox";
            this.nBurnTxtBox.Size = new System.Drawing.Size(55, 20);
            this.nBurnTxtBox.TabIndex = 7;
            this.nBurnTxtBox.Text = "100";
            // 
            // bFileTxtBox
            // 
            this.bFileTxtBox.Location = new System.Drawing.Point(476, 15);
            this.bFileTxtBox.Name = "bFileTxtBox";
            this.bFileTxtBox.Size = new System.Drawing.Size(116, 20);
            this.bFileTxtBox.TabIndex = 5;
            // 
            // bFileLbl
            // 
            this.bFileLbl.AutoSize = true;
            this.bFileLbl.Location = new System.Drawing.Point(473, 1);
            this.bFileLbl.Name = "bFileLbl";
            this.bFileLbl.Size = new System.Drawing.Size(107, 13);
            this.bFileLbl.TabIndex = 25;
            this.bFileLbl.TabStop = true;
            this.bFileLbl.Text = "Select Boundary File:";
            this.bFileLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.bFileLbl_LinkClicked);
            // 
            // locFileTxtBox
            // 
            this.locFileTxtBox.Location = new System.Drawing.Point(355, 15);
            this.locFileTxtBox.Name = "locFileTxtBox";
            this.locFileTxtBox.Size = new System.Drawing.Size(112, 20);
            this.locFileTxtBox.TabIndex = 4;
            // 
            // selLocFileLbl
            // 
            this.selLocFileLbl.AutoSize = true;
            this.selLocFileLbl.Location = new System.Drawing.Point(352, 1);
            this.selLocFileLbl.Name = "selLocFileLbl";
            this.selLocFileLbl.Size = new System.Drawing.Size(103, 13);
            this.selLocFileLbl.TabIndex = 23;
            this.selLocFileLbl.TabStop = true;
            this.selLocFileLbl.Text = "Select Location File:";
            this.selLocFileLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selLocFileLbl_LinkClicked);
            // 
            // genFileTxtBox
            // 
            this.genFileTxtBox.Location = new System.Drawing.Point(226, 15);
            this.genFileTxtBox.Name = "genFileTxtBox";
            this.genFileTxtBox.Size = new System.Drawing.Size(119, 20);
            this.genFileTxtBox.TabIndex = 3;
            // 
            // selGenFileLbl
            // 
            this.selGenFileLbl.AutoSize = true;
            this.selGenFileLbl.Location = new System.Drawing.Point(223, 1);
            this.selGenFileLbl.Name = "selGenFileLbl";
            this.selGenFileLbl.Size = new System.Drawing.Size(105, 13);
            this.selGenFileLbl.TabIndex = 21;
            this.selGenFileLbl.TabStop = true;
            this.selGenFileLbl.Text = "Select Genotype File";
            this.selGenFileLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selGenFileLbl_LinkClicked);
            // 
            // jobFldrTxtBox
            // 
            this.jobFldrTxtBox.Location = new System.Drawing.Point(107, 15);
            this.jobFldrTxtBox.Name = "jobFldrTxtBox";
            this.jobFldrTxtBox.Size = new System.Drawing.Size(109, 20);
            this.jobFldrTxtBox.TabIndex = 2;
            // 
            // selJobFldrLbl
            // 
            this.selJobFldrLbl.AutoSize = true;
            this.selJobFldrLbl.Location = new System.Drawing.Point(104, 1);
            this.selJobFldrLbl.Name = "selJobFldrLbl";
            this.selJobFldrLbl.Size = new System.Drawing.Size(92, 13);
            this.selJobFldrLbl.TabIndex = 19;
            this.selJobFldrLbl.TabStop = true;
            this.selJobFldrLbl.Text = "Select Job Folder:";
            this.selJobFldrLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selJobFldrLbl_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Name:";
            // 
            // jobNameTxtBox
            // 
            this.jobNameTxtBox.Location = new System.Drawing.Point(0, 14);
            this.jobNameTxtBox.Name = "jobNameTxtBox";
            this.jobNameTxtBox.Size = new System.Drawing.Size(99, 20);
            this.jobNameTxtBox.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hIDDataGridViewTextBoxColumn,
            this.dtJobCreatedDataGridViewTextBoxColumn,
            this.sJobNameDataGridViewTextBoxColumn,
            this.sOutputFilesPathDataGridViewTextBoxColumn,
            this.sInputGenotypeFilePathDataGridViewTextBoxColumn,
            this.sInputRegionFilePathDataGridViewTextBoxColumn,
            this.sBoundaryFilePathDataGridViewTextBoxColumn,
            this.iNLoci1,
            this.iBurnIterationsDataGridViewTextBoxColumn,
            this.iThinningsDataGridViewTextBoxColumn,
            this.iNIterationsDataGridViewTextBoxColumn,
            this.sRestriction,
            this.sSuppArgs,
            this.iJobStatusDataGridViewTextBoxColumn,
            this.iProcIDDataGridViewTextBoxColumn,
            this.iUnknownStartDataGridViewTextBoxColumn,
            this.iUnknownStopDataGridViewTextBoxColumn,
            this.dtJobQueuedDataGridViewTextBoxColumn,
            this.dtJobStartedDataGridViewTextBoxColumn,
            this.dtJobEndedDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.jobListBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1214, 538);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // hIDDataGridViewTextBoxColumn
            // 
            this.hIDDataGridViewTextBoxColumn.DataPropertyName = "hID";
            this.hIDDataGridViewTextBoxColumn.HeaderText = "hID";
            this.hIDDataGridViewTextBoxColumn.Name = "hIDDataGridViewTextBoxColumn";
            this.hIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.hIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // dtJobCreatedDataGridViewTextBoxColumn
            // 
            this.dtJobCreatedDataGridViewTextBoxColumn.DataPropertyName = "dtJobCreated";
            this.dtJobCreatedDataGridViewTextBoxColumn.HeaderText = "Created At";
            this.dtJobCreatedDataGridViewTextBoxColumn.Name = "dtJobCreatedDataGridViewTextBoxColumn";
            this.dtJobCreatedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sJobNameDataGridViewTextBoxColumn
            // 
            this.sJobNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.sJobNameDataGridViewTextBoxColumn.DataPropertyName = "sJobName";
            this.sJobNameDataGridViewTextBoxColumn.HeaderText = "Job Name";
            this.sJobNameDataGridViewTextBoxColumn.Name = "sJobNameDataGridViewTextBoxColumn";
            this.sJobNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.sJobNameDataGridViewTextBoxColumn.Width = 74;
            // 
            // sOutputFilesPathDataGridViewTextBoxColumn
            // 
            this.sOutputFilesPathDataGridViewTextBoxColumn.DataPropertyName = "sOutputFilesPath";
            this.sOutputFilesPathDataGridViewTextBoxColumn.HeaderText = "Job Folder";
            this.sOutputFilesPathDataGridViewTextBoxColumn.Name = "sOutputFilesPathDataGridViewTextBoxColumn";
            this.sOutputFilesPathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sInputGenotypeFilePathDataGridViewTextBoxColumn
            // 
            this.sInputGenotypeFilePathDataGridViewTextBoxColumn.DataPropertyName = "sInputGenotypeFilePath";
            this.sInputGenotypeFilePathDataGridViewTextBoxColumn.HeaderText = "Genotype File";
            this.sInputGenotypeFilePathDataGridViewTextBoxColumn.Name = "sInputGenotypeFilePathDataGridViewTextBoxColumn";
            this.sInputGenotypeFilePathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sInputRegionFilePathDataGridViewTextBoxColumn
            // 
            this.sInputRegionFilePathDataGridViewTextBoxColumn.DataPropertyName = "sInputRegionFilePath";
            this.sInputRegionFilePathDataGridViewTextBoxColumn.HeaderText = "Location/Region File";
            this.sInputRegionFilePathDataGridViewTextBoxColumn.Name = "sInputRegionFilePathDataGridViewTextBoxColumn";
            this.sInputRegionFilePathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sBoundaryFilePathDataGridViewTextBoxColumn
            // 
            this.sBoundaryFilePathDataGridViewTextBoxColumn.DataPropertyName = "sBoundaryFilePath";
            this.sBoundaryFilePathDataGridViewTextBoxColumn.HeaderText = "Boundary File";
            this.sBoundaryFilePathDataGridViewTextBoxColumn.Name = "sBoundaryFilePathDataGridViewTextBoxColumn";
            this.sBoundaryFilePathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iNLoci1
            // 
            this.iNLoci1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.iNLoci1.DataPropertyName = "iNLoci1";
            this.iNLoci1.HeaderText = "# Loci";
            this.iNLoci1.Name = "iNLoci1";
            this.iNLoci1.ReadOnly = true;
            this.iNLoci1.Width = 58;
            // 
            // iBurnIterationsDataGridViewTextBoxColumn
            // 
            this.iBurnIterationsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.iBurnIterationsDataGridViewTextBoxColumn.DataPropertyName = "iBurnIterations";
            this.iBurnIterationsDataGridViewTextBoxColumn.HeaderText = "Burn-ins";
            this.iBurnIterationsDataGridViewTextBoxColumn.Name = "iBurnIterationsDataGridViewTextBoxColumn";
            this.iBurnIterationsDataGridViewTextBoxColumn.ReadOnly = true;
            this.iBurnIterationsDataGridViewTextBoxColumn.Width = 70;
            // 
            // iThinningsDataGridViewTextBoxColumn
            // 
            this.iThinningsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.iThinningsDataGridViewTextBoxColumn.DataPropertyName = "iThinnings";
            this.iThinningsDataGridViewTextBoxColumn.HeaderText = "Thinning";
            this.iThinningsDataGridViewTextBoxColumn.Name = "iThinningsDataGridViewTextBoxColumn";
            this.iThinningsDataGridViewTextBoxColumn.ReadOnly = true;
            this.iThinningsDataGridViewTextBoxColumn.Width = 73;
            // 
            // iNIterationsDataGridViewTextBoxColumn
            // 
            this.iNIterationsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.iNIterationsDataGridViewTextBoxColumn.DataPropertyName = "iNIterations";
            this.iNIterationsDataGridViewTextBoxColumn.HeaderText = "Iterations";
            this.iNIterationsDataGridViewTextBoxColumn.Name = "iNIterationsDataGridViewTextBoxColumn";
            this.iNIterationsDataGridViewTextBoxColumn.ReadOnly = true;
            this.iNIterationsDataGridViewTextBoxColumn.Width = 75;
            // 
            // sRestriction
            // 
            this.sRestriction.DataPropertyName = "sRestriction";
            this.sRestriction.HeaderText = "Restriction";
            this.sRestriction.Name = "sRestriction";
            this.sRestriction.ReadOnly = true;
            // 
            // sSuppArgs
            // 
            this.sSuppArgs.DataPropertyName = "sSuppArgs";
            this.sSuppArgs.HeaderText = "Switches";
            this.sSuppArgs.Name = "sSuppArgs";
            this.sSuppArgs.ReadOnly = true;
            this.sSuppArgs.Visible = false;
            // 
            // iJobStatusDataGridViewTextBoxColumn
            // 
            this.iJobStatusDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.iJobStatusDataGridViewTextBoxColumn.DataPropertyName = "iJobStatus";
            this.iJobStatusDataGridViewTextBoxColumn.HeaderText = "iJobStatus";
            this.iJobStatusDataGridViewTextBoxColumn.Name = "iJobStatusDataGridViewTextBoxColumn";
            this.iJobStatusDataGridViewTextBoxColumn.ReadOnly = true;
            this.iJobStatusDataGridViewTextBoxColumn.Visible = false;
            // 
            // iProcIDDataGridViewTextBoxColumn
            // 
            this.iProcIDDataGridViewTextBoxColumn.DataPropertyName = "iProcID";
            this.iProcIDDataGridViewTextBoxColumn.HeaderText = "iProcID";
            this.iProcIDDataGridViewTextBoxColumn.Name = "iProcIDDataGridViewTextBoxColumn";
            this.iProcIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iProcIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // iUnknownStartDataGridViewTextBoxColumn
            // 
            this.iUnknownStartDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.iUnknownStartDataGridViewTextBoxColumn.DataPropertyName = "iUnknownStart";
            this.iUnknownStartDataGridViewTextBoxColumn.HeaderText = "First Unknown";
            this.iUnknownStartDataGridViewTextBoxColumn.Name = "iUnknownStartDataGridViewTextBoxColumn";
            this.iUnknownStartDataGridViewTextBoxColumn.ReadOnly = true;
            this.iUnknownStartDataGridViewTextBoxColumn.Width = 92;
            // 
            // iUnknownStopDataGridViewTextBoxColumn
            // 
            this.iUnknownStopDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.iUnknownStopDataGridViewTextBoxColumn.DataPropertyName = "iUnknownStop";
            this.iUnknownStopDataGridViewTextBoxColumn.HeaderText = "Last Unknown";
            this.iUnknownStopDataGridViewTextBoxColumn.Name = "iUnknownStopDataGridViewTextBoxColumn";
            this.iUnknownStopDataGridViewTextBoxColumn.ReadOnly = true;
            this.iUnknownStopDataGridViewTextBoxColumn.Width = 93;
            // 
            // dtJobQueuedDataGridViewTextBoxColumn
            // 
            this.dtJobQueuedDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtJobQueuedDataGridViewTextBoxColumn.DataPropertyName = "dtJobQueued";
            this.dtJobQueuedDataGridViewTextBoxColumn.HeaderText = "Queued At";
            this.dtJobQueuedDataGridViewTextBoxColumn.Name = "dtJobQueuedDataGridViewTextBoxColumn";
            this.dtJobQueuedDataGridViewTextBoxColumn.ReadOnly = true;
            this.dtJobQueuedDataGridViewTextBoxColumn.Width = 77;
            // 
            // dtJobStartedDataGridViewTextBoxColumn
            // 
            this.dtJobStartedDataGridViewTextBoxColumn.DataPropertyName = "dtJobStarted";
            this.dtJobStartedDataGridViewTextBoxColumn.HeaderText = "Started At";
            this.dtJobStartedDataGridViewTextBoxColumn.Name = "dtJobStartedDataGridViewTextBoxColumn";
            this.dtJobStartedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dtJobEndedDataGridViewTextBoxColumn
            // 
            this.dtJobEndedDataGridViewTextBoxColumn.DataPropertyName = "dtJobEnded";
            this.dtJobEndedDataGridViewTextBoxColumn.HeaderText = "Ended At";
            this.dtJobEndedDataGridViewTextBoxColumn.Name = "dtJobEndedDataGridViewTextBoxColumn";
            this.dtJobEndedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // jobListBindingSource
            // 
            this.jobListBindingSource.DataMember = "JobList";
            this.jobListBindingSource.DataSource = this.scatdbDataSetL2;
            // 
            // scatdbDataSetL2
            // 
            this.scatdbDataSetL2.DataSetName = "scatdbDataSetL2";
            this.scatdbDataSetL2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1220, 584);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "SCAT Running Jobs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(1138, 578);
            this.splitContainer3.SplitterDistance = 532;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer4.Size = new System.Drawing.Size(532, 578);
            this.splitContainer4.SplitterDistance = 329;
            this.splitContainer4.TabIndex = 0;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.label7);
            this.splitContainer6.Panel1.Controls.Add(this.scat2Stop1);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.scat2Out1);
            this.splitContainer6.Size = new System.Drawing.Size(532, 329);
            this.splitContainer6.SplitterDistance = 25;
            this.splitContainer6.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 1;
            // 
            // scat2Stop1
            // 
            this.scat2Stop1.Dock = System.Windows.Forms.DockStyle.Right;
            this.scat2Stop1.Location = new System.Drawing.Point(439, 0);
            this.scat2Stop1.Name = "scat2Stop1";
            this.scat2Stop1.Size = new System.Drawing.Size(93, 25);
            this.scat2Stop1.TabIndex = 0;
            this.scat2Stop1.Text = "Stop";
            this.scat2Stop1.UseVisualStyleBackColor = true;
            // 
            // scat2Out1
            // 
            this.scat2Out1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scat2Out1.Location = new System.Drawing.Point(0, 0);
            this.scat2Out1.Multiline = true;
            this.scat2Out1.Name = "scat2Out1";
            this.scat2Out1.ReadOnly = true;
            this.scat2Out1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scat2Out1.Size = new System.Drawing.Size(532, 300);
            this.scat2Out1.TabIndex = 0;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.label9);
            this.splitContainer8.Panel1.Controls.Add(this.scat2Stop3);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.scat2Out3);
            this.splitContainer8.Size = new System.Drawing.Size(532, 245);
            this.splitContainer8.SplitterDistance = 25;
            this.splitContainer8.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 3;
            // 
            // scat2Stop3
            // 
            this.scat2Stop3.Dock = System.Windows.Forms.DockStyle.Right;
            this.scat2Stop3.Location = new System.Drawing.Point(439, 0);
            this.scat2Stop3.Name = "scat2Stop3";
            this.scat2Stop3.Size = new System.Drawing.Size(93, 25);
            this.scat2Stop3.TabIndex = 0;
            this.scat2Stop3.Text = "Stop";
            this.scat2Stop3.UseVisualStyleBackColor = true;
            // 
            // scat2Out3
            // 
            this.scat2Out3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scat2Out3.Location = new System.Drawing.Point(0, 0);
            this.scat2Out3.Multiline = true;
            this.scat2Out3.Name = "scat2Out3";
            this.scat2Out3.ReadOnly = true;
            this.scat2Out3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scat2Out3.Size = new System.Drawing.Size(532, 216);
            this.scat2Out3.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer9);
            this.splitContainer5.Size = new System.Drawing.Size(602, 578);
            this.splitContainer5.SplitterDistance = 329;
            this.splitContainer5.TabIndex = 1;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.label8);
            this.splitContainer7.Panel1.Controls.Add(this.scat2Stop2);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.scat2Out2);
            this.splitContainer7.Size = new System.Drawing.Size(602, 329);
            this.splitContainer7.SplitterDistance = 25;
            this.splitContainer7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 2;
            // 
            // scat2Stop2
            // 
            this.scat2Stop2.Dock = System.Windows.Forms.DockStyle.Right;
            this.scat2Stop2.Location = new System.Drawing.Point(509, 0);
            this.scat2Stop2.Name = "scat2Stop2";
            this.scat2Stop2.Size = new System.Drawing.Size(93, 25);
            this.scat2Stop2.TabIndex = 0;
            this.scat2Stop2.Text = "Stop";
            this.scat2Stop2.UseVisualStyleBackColor = true;
            // 
            // scat2Out2
            // 
            this.scat2Out2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scat2Out2.Location = new System.Drawing.Point(0, 0);
            this.scat2Out2.Multiline = true;
            this.scat2Out2.Name = "scat2Out2";
            this.scat2Out2.ReadOnly = true;
            this.scat2Out2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scat2Out2.Size = new System.Drawing.Size(602, 300);
            this.scat2Out2.TabIndex = 0;
            // 
            // splitContainer9
            // 
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.label10);
            this.splitContainer9.Panel1.Controls.Add(this.scat2Stop4);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.scat2Out4);
            this.splitContainer9.Size = new System.Drawing.Size(602, 245);
            this.splitContainer9.SplitterDistance = 25;
            this.splitContainer9.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(2, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 2;
            // 
            // scat2Stop4
            // 
            this.scat2Stop4.Dock = System.Windows.Forms.DockStyle.Right;
            this.scat2Stop4.Location = new System.Drawing.Point(509, 0);
            this.scat2Stop4.Name = "scat2Stop4";
            this.scat2Stop4.Size = new System.Drawing.Size(93, 25);
            this.scat2Stop4.TabIndex = 0;
            this.scat2Stop4.Text = "Stop";
            this.scat2Stop4.UseVisualStyleBackColor = true;
            // 
            // scat2Out4
            // 
            this.scat2Out4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scat2Out4.Location = new System.Drawing.Point(0, 0);
            this.scat2Out4.Multiline = true;
            this.scat2Out4.Name = "scat2Out4";
            this.scat2Out4.ReadOnly = true;
            this.scat2Out4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scat2Out4.Size = new System.Drawing.Size(602, 216);
            this.scat2Out4.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1220, 584);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Output";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.button3);
            this.splitContainer2.Panel1.Controls.Add(this.resetButton);
            this.splitContainer2.Panel1.Controls.Add(this.button2);
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1214, 578);
            this.splitContainer2.SplitterDistance = 52;
            this.splitContainer2.TabIndex = 4;
            // 
            // groupBox1
            // 
			this.groupBox1.Controls.Add(this.label19);			//// new
			this.groupBox1.Controls.Add(this.label20);			//// new
            this.groupBox1.Controls.Add(this.label21);			//// new
			this.groupBox1.Controls.Add(this.nGridTBox);		//// new
            this.groupBox1.Controls.Add(this.nBurnTBox);		//// new
			this.groupBox1.Controls.Add(this.nIterTBox);		//// new

			
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.vLengthTBox);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.voronoiBFile);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(645, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 47);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voronoi Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(222, 6);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(89, 17);
            this.radioButton3.TabIndex = 33;
            this.radioButton3.Text = "Boundary File";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // voronoiBFile
            // 
            this.voronoiBFile.Location = new System.Drawing.Point(223, 23);
            this.voronoiBFile.Name = "voronoiBFile";
            this.voronoiBFile.Size = new System.Drawing.Size(126, 20);
            this.voronoiBFile.TabIndex = 26;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(308, 8);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 27;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Select";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_3);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "Voronoi";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(167, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "Forest";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(93, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(74, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Savannah";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(511, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(325, 16);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(421, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Plot";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Not Set";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SCAT Data Root Folder:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1214, 522);
            this.splitContainer1.SplitterDistance = 269;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("treeView1.SelectedNodes")));
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(269, 522);
            this.treeView1.TabIndex = 8;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1220, 584);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Map";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1138, 578);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.splitContainer11);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1220, 584);
            this.tabPage6.TabIndex = 7;
            this.tabPage6.Text = "Voronoi";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // splitContainer11
            // 
            this.splitContainer11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer11.Location = new System.Drawing.Point(3, 3);
            this.splitContainer11.Name = "splitContainer11";
            this.splitContainer11.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer11.Panel1
            // 
            this.splitContainer11.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer11.Panel2
            // 
            this.splitContainer11.Panel2.Controls.Add(this.textBox2);
            this.splitContainer11.Size = new System.Drawing.Size(1214, 578);
            this.splitContainer11.SplitterDistance = 33;
            this.splitContainer11.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(491, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Please click on the \'Output\' tab and select files for Voronoi processing";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(1214, 541);
            this.textBox2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBox6);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.textBox5);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.textBox4);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.button4);
            this.tabPage4.Controls.Add(this.textBox3);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.maxSessCtl);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1220, 584);
            this.tabPage4.TabIndex = 8;
            this.tabPage4.Text = "Settings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(206, 260);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(43, 20);
            this.textBox6.TabIndex = 12;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(9, 264);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Default # Iterations:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(206, 223);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(43, 20);
            this.textBox5.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(9, 227);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Default # Thinnings:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(206, 187);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(43, 20);
            this.textBox4.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 191);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Default # Burn-ins:";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Terrain",
            "Satellite",
            "Roadmap",
            "Hybrid"});
            this.listBox1.Location = new System.Drawing.Point(206, 115);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(114, 56);
            this.listBox1.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 115);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(161, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Google Maps Default View:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(330, 78);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 21);
            this.button4.TabIndex = 4;
            this.button4.Text = "Change";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(206, 79);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(114, 20);
            this.textBox3.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "SCAT Data Default Root Folder:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(9, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(191, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Maximum concurrent SCAT runs:";
            // 
            // maxSessCtl
            // 
            this.maxSessCtl.Location = new System.Drawing.Point(206, 50);
            this.maxSessCtl.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.maxSessCtl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxSessCtl.Name = "maxSessCtl";
            this.maxSessCtl.Size = new System.Drawing.Size(43, 20);
            this.maxSessCtl.TabIndex = 0;
            this.maxSessCtl.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxSessCtl.ValueChanged += new System.EventHandler(this.maxSessCtl_ValueChanged);
            // 
            // scatdbDataSetLBindingSource
            // 
            this.scatdbDataSetLBindingSource.DataSource = this.scatdbDataSetL;
            this.scatdbDataSetLBindingSource.Position = 0;
            // 
            // scatdbDataSetL
            // 
            this.scatdbDataSetL.DataSetName = "scatdbDataSetL";
            this.scatdbDataSetL.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1227, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newToolStripMenuItem.Text = "Clear Job List";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // jobListTableAdapter1
            // 
            this.jobListTableAdapter1.ClearBeforeFill = true;
            // 
            // dataGridViewJobStatusTextColumn
            // 
            this.dataGridViewJobStatusTextColumn.HeaderText = "Job Status";
            this.dataGridViewJobStatusTextColumn.Name = "dataGridViewJobStatusTextColumn";
            // 
            // lActionsDataGridViewLinkColumn
            // 
            this.lActionsDataGridViewLinkColumn.HeaderText = "Actions";
            this.lActionsDataGridViewLinkColumn.Name = "lActionsDataGridViewLinkColumn";
            // 
            // jobListTableAdapter
            // 
            this.jobListTableAdapter.ClearBeforeFill = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(360, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Voronoi Cells:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(440, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(50, 13);
            this.label19.TabIndex = 45;
            this.label19.Text = "Grid Size:";
            // 
            // label20
            // 

            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(500, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(50, 13);
            this.label20.TabIndex = 45;
            this.label20.Text = "SCAT Burn-In:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(583, 8);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 13);
            this.label21.TabIndex = 45;
            this.label21.Text = "SCAT Main Iterations:";			
            // 
            // vLengthTBox
            // 
            this.vLengthTBox.Location = new System.Drawing.Point(360, 22);
            this.vLengthTBox.Name = "vLengthTBox";
            this.vLengthTBox.Size = new System.Drawing.Size(39, 20);
            this.vLengthTBox.TabIndex = 44;
            this.vLengthTBox.Text = "100";
            // 
            // nGridTBox
            // 
            this.nGridTBox.Location = new System.Drawing.Point(440, 22);
            this.nGridTBox.Name = "nGridTBox";
            this.nGridTBox.Size = new System.Drawing.Size(39, 20);
            this.nGridTBox.TabIndex = 44;
            this.nGridTBox.Text = "67";
            // 
            // nBurnTBox
            // 
            this.nBurnTBox.Location = new System.Drawing.Point(500, 22);
            this.nBurnTBox.Name = "nBurnTBox";
            this.nBurnTBox.Size = new System.Drawing.Size(39, 20);
            this.nBurnTBox.TabIndex = 44;
            this.nBurnTBox.Text = "100";			
            // 
            // nIterTBox
            // 
            this.nIterTBox.Location = new System.Drawing.Point(583, 22);
            this.nIterTBox.Name = "nIterTBox";
            this.nIterTBox.Size = new System.Drawing.Size(39, 20);
            this.nIterTBox.TabIndex = 44;
            this.nIterTBox.Text = "100";				
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1227, 638);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCAT 2.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainWnd_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer10.Panel1.ResumeLayout(false);
            this.splitContainer10.Panel1.PerformLayout();
            this.splitContainer10.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
            this.splitContainer10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scatdbDataSetL2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel1.PerformLayout();
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel1.PerformLayout();
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel1.PerformLayout();
            this.splitContainer9.Panel2.ResumeLayout(false);
            this.splitContainer9.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel1.PerformLayout();
            this.splitContainer11.Panel2.ResumeLayout(false);
            this.splitContainer11.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSessCtl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scatdbDataSetLBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scatdbDataSetL)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
 //           MessageBox.Show("Exception");
            anError.ThrowException = false;
            return;
/*            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is System.Data.ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                MessageBox.Show(anError.RowIndex.ToString());
                //view.Rows[anError.RowIndex].ErrorText = "an error";
                //view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
            MessageBox.Show(anError.Exception.Message);
            MessageBox.Show(anError.Exception.Source);
            MessageBox.Show(anError.Exception.TargetSite.ToString());
            MessageBox.Show(anError.Exception.StackTrace);
            MessageBox.Show(anError.Exception.InnerException.Message);
            anError.ThrowException = false;
            
            //refresh_DGV1();
  */      }
        
        
        void dataGridView1_AfterRefresh()
        {
            try
            {

                for (int i =0; i<dataGridView1.Rows.Count; i++) {
                    string temp = dataGridView1.Rows[i].Cells["iJobStatusDataGridViewTextBoxColumn"].Value.ToString();              

                    switch (temp)
                    {
                        case "1":
                            dataGridView1.Rows[i].Cells["dataGridViewJobStatusTextColumn"].Value = "Queued";
                            dataGridView1.Rows[i].Cells["lActionsDataGridViewLinkColumn"].Value = "Cancel";
                            break;
                        case "0":
                            dataGridView1.Rows[i].Cells["dataGridViewJobStatusTextColumn"].Value = "Running";
                            dataGridView1.Rows[i].Cells["lActionsDataGridViewLinkColumn"].Value = "Abort";
                            break;
                        case "2":
                            dataGridView1.Rows[i].Cells["dataGridViewJobStatusTextColumn"].Value = "Finished";
                            dataGridView1.Rows[i].Cells["lActionsDataGridViewLinkColumn"].Value = "";
                            break;
                        case "3":
                            dataGridView1.Rows[i].Cells["dataGridViewJobStatusTextColumn"].Value = "Aborted";
                            dataGridView1.Rows[i].Cells["lActionsDataGridViewLinkColumn"].Value = "Add to Queue";
                            break;
                    }
                    string temp2 = dataGridView1.Rows[i].Cells["sRestriction"].Value.ToString();
                    switch (temp2)
                    {
                        case " -D ":
                            dataGridView1.Rows[i].Cells["sRestriction"].Value = "Forest";
                            break;
                        case " -d ":
                            dataGridView1.Rows[i].Cells["sRestriction"].Value = "Savannah";
                            break;
                        default :
                            dataGridView1.Rows[i].Cells["sRestriction"].Value = "None";
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void treeView1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                treeView1_NodeMouseDoubleClick(sender, new System.Windows.Forms.TreeNodeMouseClickEventArgs(treeView1.SelectedNode, System.Windows.Forms.MouseButtons.None, 1,1,1));
            }
        }

        void treeView1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("dragging");
        }
       
        /*
        void dataGridView2_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedCells.Count == 0 && dataGridView2.SelectedCells[0].OwningColumn.Name == "ignore")
            {
                dataGridView2.CurrentCell = dataGridView2.SelectedCells[0];
                dataGridView2.BeginEdit(false);
                System.Windows.Forms.SendKeys.Send("{F2}");

            }
        }
        
        void dataGridView2_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (this.dataGridView2.SelectedCells.Count == 1 && this.dataGridView2.SelectedCells[0].OwningColumn.Name == "fileDataGridViewColumn")
            {
                dataGridView2.Rows.Remove(this.dataGridView2.SelectedCells[0].OwningRow);
                //initializeTreeView();
                //call sample recalculate;
            }
        }
        */
       

        void treeView1_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show(treeView1.SelectedNodes.Count.ToString());
            int i = 0;
            while (i < treeView1.SelectedNodes.Count && treeView1.SelectedNodes[i] != null && ((TreeNode) treeView1.SelectedNodes[i]).Tag != null)
            {
                if (System.IO.File.Exists(((System.IO.FileInfo) ((TreeNode)treeView1.SelectedNodes[i]).Tag).FullName))
                {                                       
                   postProcessList.addRow(((TreeNode)treeView1.SelectedNodes[i]).Parent.Text, ((System.IO.FileInfo)((TreeNode)treeView1.SelectedNodes[i]).Tag), 100);                   
                }                
                i++;
            }
        }
        void treeViewMS1_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show(treeView1.SelectedNodes.Count.ToString());
            /*
            int i = 0;
            while (i < treeViewMS1.SelectedNodes.Count && treeView1.SelectedNodes[i] != null && ((TreeNode)treeView1.SelectedNodes[i]).Tag != null)
            {
                if (System.IO.File.Exists(((System.IO.FileInfo)((TreeNode)treeView1.SelectedNodes[i]).Tag).FullName))
                {
                    postProcessList.addRow(((TreeNode)treeView1.SelectedNodes[i]).Parent.Text, ((System.IO.FileInfo)((TreeNode)treeView1.SelectedNodes[i]).Tag), 100);
                }
                i++;
            }*/
        }
        void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            initializeTreeView();
        }

        void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("before expand");
            if (e.Node.Tag != null)
            {
               addTreeViewContent(e.Node, (string)e.Node.Tag);
            }
        }

        void treeViewMS1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("before expand");
            if (e.Node.Tag != null)
            {
                addTreeViewContent(e.Node, (string)e.Node.Tag);
            }
        }

        void addTreeViewContent(System.Windows.Forms.TreeNode tNode, string treeNodePath)
        {
            tNode.Nodes.Clear(); // clear dummy node if exists

            try
            {
                System.IO.DirectoryInfo currentDir = new System.IO.DirectoryInfo(treeNodePath);
                System.IO.DirectoryInfo[] subdirs = currentDir.GetDirectories();

                foreach (System.IO.DirectoryInfo subdir in subdirs)
                {
                    System.Windows.Forms.TreeNode child = new System.Windows.Forms.TreeNode(subdir.Name);
                    child.Tag = subdir.FullName; // save full path in tag
                    // TODO: Use some image for the node to show its a music file

                    child.Nodes.Add(new System.Windows.Forms.TreeNode()); // add dummy node to allow expansion
                    tNode.Nodes.Add(child);
                }

                System.Collections.Generic.List<System.IO.FileInfo> files = new System.Collections.Generic.List<System.IO.FileInfo>();
                files.AddRange(currentDir.GetFiles("*.*"));

                foreach (System.IO.FileInfo file in files)
                {
                    System.Windows.Forms.TreeNode child = new System.Windows.Forms.TreeNode(file.Name);
                    // TODO: Use some image for the node to show its a music file

                    child.Tag = file; // save full path for later use  
                    child.ToolTipText = "Double-click to add a single file. To select multiple files, use the SHIFT or CTRL button and then press ENTER.";
                    tNode.Nodes.Add(child);
                }

            }
            catch
            { // try to handle use each exception separately
            }
            finally
            {
                tNode.Tag = null; // clear tag
            }
        }
        void initializeTreeView()
        {
            this.treeView1.ShowPlusMinus = true;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Nodes.Clear();
            //this.treeView1.Nodes.
            /*
            foreach (System.IO.DriveInfo d in System.IO.DriveInfo.GetDrives())
            {
                System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode(d.Name);
                root.Tag = d.Name; // for later reference
                // TODO: Use Drive image for node

                root.Nodes.Add(new System.Windows.Forms.TreeNode()); // add dummy node to allow expansion
                treeView1.Nodes.Add(root);
            }*/
            System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode(textBox1.Text);
            root.Tag = textBox1.Text; // for later reference
            // TODO: Use Drive image for node

            root.Nodes.Add(new System.Windows.Forms.TreeNode()); // add dummy node to allow expansion
            treeView1.Nodes.Add(root);
            //MessageBox.Show("tb changed");

        }

        

        void dataGridView1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
           /* if (dataGridView1.SelectedCells.GetType() == System.Windows.Forms.DataGridViewLinkCell)
            {

            }
            */
            //System.Windows.Forms.MessageBox.Show(dataGridView1.SelectedCells[0].GetType().ToString());
            if (dataGridView1.SelectedCells.Count == 1 && dataGridView1.SelectedCells[0].GetType().ToString() == "System.Windows.Forms.DataGridViewLinkCell" )
            {
                if (dataGridView1.SelectedCells[0].OwningColumn.HeaderText == "Actions")
                {
                    switch (dataGridView1.SelectedCells[0].Value.ToString())
                    {
                        case "Cancel":
                        case "Abort":
                            //MessageBox.Show((jobIDList.IndexOf(System.Convert.ToInt16(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["hIDDataGridViewTextBoxColumn"].Value.ToString())).ToString()));
                            //MessageBox.Show(((int)jobIDList[0]).ToString());
                            if (jobIDList.IndexOf(System.Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["hIDDataGridViewTextBoxColumn"].Value.ToString())) >= 0)
                            {
                                this.tabControl1.SelectedIndex = 1;
                                //this.tabPage3.Focus();
                                ((Button)scat2OutBtnBusyList[jobIDList.IndexOf(System.Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["hIDDataGridViewTextBoxColumn"].Value.ToString()))]).PerformClick();
                            }
                            else
                            {
                                updateJobEnd(System.Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["hIDDataGridViewTextBoxColumn"].Value.ToString()), -1);
                            }
                            break;
                        case "Remove From Queue":
                            break;
                        case "Add to Queue":
                            updateJobToQueue(System.Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["hIDDataGridViewTextBoxColumn"].Value.ToString()));
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    System.Diagnostics.Process.Start(@dataGridView1.SelectedCells[0].Value.ToString());
                }
            }
        }
        
        void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //bdryInputForm = new BoundaryInput();
            //bdryInputForm.Show();            
            if (MessageBox.Show("Are you sure you want to clear all Jobs from the list?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clearJobList();
                refresh_DGV1();
            }
        }

        void optionsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //MessageBox.Show("The Options tab will be available in a later release");
            this.tabControl1.SelectedTab = tabPage4;
            //optionsForm1 = new OptionsForm();
            //optionsForm1.Show();
        }

        void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AboutSCAT aboutScatForm = new AboutSCAT();
            aboutScatForm.Show();
            //this.Close();
            //aboutScatForm
        }
        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private OptionsForm optionsForm1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        
        private BoundaryInput bdryInputForm;
        private System.Windows.Forms.TabPage tabPage5;
        //private System.Windows.Forms.DataGridViewTextBoxColumn sOutputFilesPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.FolderBrowserDialog baseFolderChooser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeViewMS treeView1;
        
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button2;
        private Button resetButton;
        //private ConsoleRedirector.Form1 oWindow;
        private TabPage tabPage3;
        private SplitContainer splitContainer3;
        private SplitContainer splitContainer4;
        private SplitContainer splitContainer5;
        private SplitContainer splitContainer6;
        private TextBox scat2Out1;
        private Button scat2Stop1;
        private SplitContainer splitContainer7;
        private Button scat2Stop2;
        private TextBox scat2Out2;
        private SplitContainer splitContainer8;
        private Button scat2Stop3;
        private TextBox scat2Out3;
        private SplitContainer splitContainer9;
        private Button scat2Stop4;
        private TextBox scat2Out4;
        private SplitContainer splitContainer10;
        private Button newJobBtn;
        private Label nIterLbl;
        private TextBox nIterTxtBox;
        private Label nThinLbl;
        private TextBox nThinTxtBox;
        private Label nBurnLbl;
        private TextBox nBurnTxtBox;
        private TextBox bFileTxtBox;
        private LinkLabel bFileLbl;
        private TextBox locFileTxtBox;
        private LinkLabel selLocFileLbl;
        private TextBox genFileTxtBox;
        private LinkLabel selGenFileLbl;
        private TextBox jobFldrTxtBox;
        private LinkLabel selJobFldrLbl;
        private Label label2;
        private TextBox jobNameTxtBox;
      
        private OpenFileDialog fileChooser;
        private Label label3;
        private TextBox uStartTxtBox;
        private Label label4;
        private TextBox uEndTxtBox;
        private DataGridView dataGridView1;
        
        private scatdbDataSetL scatdbDataSetL;
       
        private scatdbDataSetLTableAdapters.JobListTableAdapter jobListTableAdapter1;

        private DataGridViewTextBoxColumn dataGridViewJobStatusTextColumn;
        private DataGridViewLinkColumn lActionsDataGridViewLinkColumn;
        private scatdbDataSetL2 scatdbDataSetL2;
        private BindingSource jobListBindingSource;
        private scatdbDataSetL2TableAdapters.JobListTableAdapter jobListTableAdapter;
        private BindingSource scatdbDataSetLBindingSource;
        private Label nLociLbl;
        private TextBox nLociTxtBox;
        private Button button3;
        private Button button5;
        private TabPage tabPage6;
        private Label label6;
        private SplitContainer splitContainer11;
        private TextBox textBox2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label5;
        private ComboBox comboBox1;
        private Label label7;
        private Label label9;
        private Label label8;
        private Label label10;
        private TabPage tabPage4;
        private Button button4;
        private TextBox textBox3;
        private Label label12;
        private Label label11;
        private NumericUpDown maxSessCtl;
        private ListBox listBox1;
        private Label label13;
        private Label label14;
        private TextBox textBox6;
        private Label label16;
        private TextBox textBox5;
        private Label label15;
        private TextBox textBox4;
        private GroupBox groupBox1;
        private TextBox voronoiBFile;
        private LinkLabel linkLabel1;
        private RadioButton radioButton3;
        private DataGridViewTextBoxColumn hIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dtJobCreatedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sJobNameDataGridViewTextBoxColumn;
        private DataGridViewLinkColumn sOutputFilesPathDataGridViewTextBoxColumn;
        private DataGridViewLinkColumn sInputGenotypeFilePathDataGridViewTextBoxColumn;
        private DataGridViewLinkColumn sInputRegionFilePathDataGridViewTextBoxColumn;
        private DataGridViewLinkColumn sBoundaryFilePathDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iNLoci1;
        private DataGridViewTextBoxColumn iBurnIterationsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iThinningsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iNIterationsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sRestriction;
        private DataGridViewTextBoxColumn sSuppArgs;
        private DataGridViewTextBoxColumn iJobStatusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iProcIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iUnknownStartDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iUnknownStopDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dtJobQueuedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dtJobStartedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dtJobEndedDataGridViewTextBoxColumn;
        private Label label18;
        private TextBox vLengthTBox;
		private TextBox nBurnTBox;
		private TextBox nIterTBox;
		private TextBox nGridTBox;
        private Label label19;
        private Label label20;
		private Label label21;		

    }

    
}

