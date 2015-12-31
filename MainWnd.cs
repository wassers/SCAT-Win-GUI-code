using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;

namespace SCAT_2._0
{
    public partial class MainWnd : Form
    {

        private string parms, htmlFilePath, scatDataPath;
        private cfHttpEngine listeningEngine;
        private bool httpEnabled;
        private List <int>  jobIDList;
        private List <TextBox> scat2OutBusyList, scat2OutFreeList;
        private List <Button> scat2OutBtnBusyList, scat2OutBtnFreeList;
        private List <Label> scat2OutLblBusyList, scat2OutLblFreeList;
        private int nProcs = 2;
        
        meanMedianDataGrid postProcessList;
        //private double latList=0, longList=0;         
        //[DllImport("scatLib.dll", EntryPoint = "?scat2@@YAHPAPAD@Z")]
            //public extern unsafe static int scat2(byte** argv);
        public MainWnd()
        {
            InitializeComponent();
            
            parms = "?";
            htmlFilePath = System.IO.Directory.GetCurrentDirectory() + "\\SCATPlot.html";
            scatDataPath = System.IO.Directory.GetCurrentDirectory();
            this.textBox1.Text = scatDataPath;
            initializeTreeView();
            postProcessList = new meanMedianDataGrid(/*splitContainer1.Panel2*/);
            this.splitContainer1.Panel2.Controls.Add(postProcessList.dgvHolder);
            postProcessList.dgvHolder.AllowDrop = true;
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer2.Panel2.DragDrop += new DragEventHandler(Panel2_DragDrop);
            this.splitContainer1.Panel2.AutoScroll = true;
            this.Text = "SCAT-Win";
            //listeningEngine = new cfHttpEngine();
            
            /*for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
            {
                this.dataGridView1.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }*/
            /*
            if (listeningEngine.start()) 
            {
                listeningEngine.newRequest += new newRequestHandler(listeningEngine_newRequest);
                httpEnabled = true;
            }
            else 
            {
                httpEnabled = false;
            }
             */
            try
            {
                /*
                unsafe {
                    byte** sScatParms = stackalloc byte*[3];
                    byte* sTest1 = stackalloc byte[9];
                    byte* sTest2 = stackalloc byte[10];
                    fixed (byte* sTest3 = new System.Text.UTF8Encoding().GetBytes("test.out"))
                    {
                        sTest1 = sTest3;
                    }
                    fixed (byte* sTest3 = new System.Text.UTF8Encoding().GetBytes("test1.out"))
                    {
                        sTest2 = sTest3;
                    }
                   
                    sScatParms[0] = sTest1;
                    sScatParms[1] = sTest2;
                    sScatParms[2] = sTest2;

                    //scat2(sScatParms);
                }*/
                //SysProc sysProcTest = new SysProc();
                //sysProcTest.startProc(scat2Out1, scat2Stop1, "-A 111 133 C:\\Users\\me\\Documents\\Work\\SCAT\\scat2\\Debug\\Leopard133.genotypes.txt C:\\Users\\me\\Documents\\Work\\SCAT\\scat2\\Debug\\regionfile.leopard110.txt -BBoundary.txt test.out 10 10 10", this);
                //sysProcTest.startProc(scat2Out1, scat2Stop1, "");
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("scat2 launch failed " + e.Message);
            }
            
        }

        string listeningEngine_newRequest(object sender, string e)
        {
            string outputString = "Hello";
            /*
            using (XmlReader reqReader = XmlReader.Create(new StringReader(e)))
            {
                while (reqReader.Read())
                {
                    //reqReader.
                }
            }
             */
            return outputString;
        }

        void Panel2_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show("here");
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'scatdbDataSetL2.JobList' table. You can move, or remove it, as needed.
            this.jobListTableAdapter.Fill(this.scatdbDataSetL2.JobList);
            // TODO: This line of code loads data into the 'scatdbDataSetL.JobList' table. You can move, or remove it, as needed.
            //this.jobListTableAdapter1.Fill(this.scatdbDataSetL.JobList);
            dataGridView1.Columns.Add(dataGridViewJobStatusTextColumn);
            dataGridView1.Columns.Add(lActionsDataGridViewLinkColumn);
            dataGridView1_AfterRefresh();
            //this.treeView1 = new TreeViewMS();
            initializeScatProc();
            checkQueue();       

        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.baseFolderChooser.ShowDialog() == DialogResult.OK)
            {                
                this.textBox1.Text = this.baseFolderChooser.SelectedPath;
                string folderPath = this.baseFolderChooser.SelectedPath;             
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file1Chooser = new OpenFileDialog();
            file1Chooser.ShowDialog();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {/*
            outputPointCollection outPoints = new outputPointCollection();
            outPoints.initialize();
            OpenFileDialog fileChooser = new OpenFileDialog();
            double[] calculatedMedian = new double[2];
            double[] calcCentroid = new double[2];
            if (fileChooser.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileChooser.FileName))
            {
                string fileLine;
                int index = 0;
                while ((fileLine = sr.ReadLine()) != null && !fileLine.Contains("Acceptance"))
                {
                    outPoints.Add(fileLine.Split(' '));
                    index++;
                }
                calculatedMedian = outPoints.computeMedian(System.Convert.ToInt32(this.textBox3.Text));             
                    
                calcCentroid = outPoints.computeCentroid(System.Convert.ToInt32(this.textBox3.Text));
                
                parms += fileChooser.FileName.Split('\\')[fileChooser.FileName.Split('\\').Length-2] + "*" + Uri.EscapeDataString(fileChooser.SafeFileName) + "*" + calculatedMedian[0] + "*" + calculatedMedian[1] + "*" + calcCentroid[0] + "*" + calcCentroid[1] + "|";
            }
            
            webBrowser1.Navigate(htmlFilePath + parms);
           */ 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parms = "?";
            webBrowser1.Navigate("maps.google.com");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1");
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            parms = "?";
            DataGridViewRow temp;
            for (int t = 0; t < postProcessList.dgvHolder.Controls.Count; t++)
            {
                temp = ((DataGridView)postProcessList.dgvHolder.Controls[t]).Rows[0];                
                parms += temp.Cells["label"].Value.ToString() + "*" + temp.Cells["sample"].Value.ToString() + "*" ;
                parms += temp.Cells["median"].Value.ToString().Split(',')[0] + "*" + temp.Cells["median"].Value.ToString().Split(' ')[1] + "*";
                parms += temp.Cells["label"].Value + "*";
                parms += " * |";
            }
            webBrowser1.Navigate(htmlFilePath + parms);
            this.tabControl1.SelectedIndex = 3;
                
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            postProcessList.reset();            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void newJobBtn_Click(object sender, EventArgs e)
        {
            string sGenFile="", sLocFile="", sBFile="";
            int nBurns = 0, nThin = 0, nIter = 0, fUnkn = 0, lUnkn = 0, vLength = 0;
            short nLoci = 0;
            if (jobNameTxtBox.Text == null || jobNameTxtBox.Text == "")
            {
                MessageBox.Show("Please enter a Name for this Job");
                jobNameTxtBox.Focus();
                return;
            }
            if (jobFldrTxtBox.Text == null || jobFldrTxtBox.Text == "")
            {
                MessageBox.Show("Please select a Folder for this Job");
                //selJobFldrLbl.Invoke(OnClick, new object[] {new LinkLabelLinkClickedEventArgs(selJobFldrLbl.Links[0])}) ;
                selJobFldrLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(selJobFldrLbl.Links[0]));
                return;
            }
            if (genFileTxtBox.Text == null || genFileTxtBox.Text == "")
            {
                MessageBox.Show("Please select a GenoType File for this Job");
                //selJobFldrLbl.Invoke(OnClick, new object[] {new LinkLabelLinkClickedEventArgs(selJobFldrLbl.Links[0])}) ;
                selGenFileLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(selGenFileLbl.Links[0]));
                return;
            }
            if (locFileTxtBox.Text == null || locFileTxtBox.Text == "")
            {
                MessageBox.Show("Please select a Location/Region File for this Job");
                //selJobFldrLbl.Invoke(OnClick, new object[] {new LinkLabelLinkClickedEventArgs(selJobFldrLbl.Links[0])}) ;
                selLocFileLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(selLocFileLbl.Links[0]));
                return;
            }

            if (!short.TryParse(nLociTxtBox.Text, out nLoci))
            {
                MessageBox.Show("Please enter a valid number for # of Loci");
                nLociTxtBox.Focus();
                return;
            }

            if (!int.TryParse(nBurnTxtBox.Text, out nBurns))
            {
                MessageBox.Show("Please enter a valid number for Burn-in iterations");
                nBurnTxtBox.Focus();
                return;
            }
            if (!int.TryParse(nThinTxtBox.Text, out nThin))
            {
                MessageBox.Show("Please enter a valid number for Thinnings");
                nThinTxtBox.Focus();
                return;
            }
            if (!int.TryParse(nIterTxtBox.Text, out nIter))
            {
                MessageBox.Show("Please enter a valid number for # of iterations");
                nIterTxtBox.Focus();
                return;
            }
            if (!int.TryParse(uStartTxtBox.Text, out fUnkn))
            {
                MessageBox.Show("Please enter a valid number for First Unknown Sample");
                uStartTxtBox.Focus();
                return;
            }
            if (!int.TryParse(uEndTxtBox.Text, out lUnkn))
            {
                MessageBox.Show("Please enter a valid number for Last Unknown Sample");
                uEndTxtBox.Focus();
                return;
            }
            
            if (Directory.Exists(jobFldrTxtBox.Text)) 
            {
                if (File.Exists(genFileTxtBox.Text))
                {
                    string sFileName = genFileTxtBox.Text.Substring(genFileTxtBox.Text.LastIndexOf('\\') + 1);
                    string sFldr = genFileTxtBox.Text.Substring(0, genFileTxtBox.Text.LastIndexOf('\\') + 1);
                    if (!sFldr.Equals(jobFldrTxtBox.Text))
                    {
                        sGenFile = jobFldrTxtBox.Text + "\\" + sFileName;
                        File.Copy(genFileTxtBox.Text, sGenFile, true);                        
                    }
                }
                else
                {
                    MessageBox.Show("Please Verify the GenoType File");
                    selGenFileLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(selGenFileLbl.Links[0]));
                    return;
                }
                if (File.Exists(locFileTxtBox.Text))
                {
                    string sFileName = locFileTxtBox.Text.Substring(locFileTxtBox.Text.LastIndexOf('\\') + 1);
                    string sFldr = locFileTxtBox.Text.Substring(0, locFileTxtBox.Text.LastIndexOf('\\') + 1);
                    if (!sFldr.Equals(jobFldrTxtBox.Text))
                    {
                        sLocFile = jobFldrTxtBox.Text + "\\" + sFileName;
                        File.Copy(locFileTxtBox.Text, sLocFile, true);
                    }
                }
                else
                {
                    MessageBox.Show("Please Verify the Location/Region File");
                    selLocFileLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(selLocFileLbl.Links[0]));
                    return;
                }
                
                if (File.Exists(bFileTxtBox.Text))
                {
                    string sFileName = bFileTxtBox.Text.Substring(bFileTxtBox.Text.LastIndexOf('\\') + 1);
                    string sFldr = bFileTxtBox.Text.Substring(0, bFileTxtBox.Text.LastIndexOf('\\') + 1);
                    if (!sFldr.Equals(jobFldrTxtBox.Text))
                    {
                        sBFile = jobFldrTxtBox.Text + "\\" + sFileName;
                        File.Copy(bFileTxtBox.Text, sBFile, true);
                    }
                }
                else if (comboBox1.SelectedIndex < 1)
                {
                    MessageBox.Show("Please Verify the Boundary File");
                    bFileLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(bFileLbl.Links[0]));
                    return;
                }
                
                //jobListTableAdapter.InsertJob(jobNameTxtBox.Text, jobFldrTxtBox.Text, sGenFile, sLocFile, sBFile, nLoci, nBurns, nThin, nIter, fUnkn, lUnkn);
                string[] sRestr = {"", " -d ", " -D " };
                if (comboBox1.SelectedIndex < 0) comboBox1.SelectedIndex = 0;
                //add spaces to suppArgs field when you add it to the code
                jobListTableAdapter.InsertJob(jobNameTxtBox.Text, jobFldrTxtBox.Text, sGenFile, sLocFile, sBFile, nLoci, nBurns, nThin, nIter, fUnkn, lUnkn, sRestr[comboBox1.SelectedIndex],"");
                refresh_DGV1();
                jobFldrTxtBox.Clear();
                jobNameTxtBox.Clear();
                checkQueue();
            } else {
                MessageBox.Show("Please verify Job Folder");
                selJobFldrLbl_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(selJobFldrLbl.Links[0]));
                return;
            }
        }

        private void selJobFldrLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.baseFolderChooser.Description = "Select the folder where Job data and results will be stored";
            if (this.baseFolderChooser.ShowDialog() == DialogResult.OK)
            {
                this.jobFldrTxtBox.Text = this.baseFolderChooser.SelectedPath;
            }
        }
        private void selGenFileLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.fileChooser.Title = "Select the GenoType File for this job:";
            if (this.fileChooser.ShowDialog() == DialogResult.OK)
            {
                this.genFileTxtBox.Text = this.fileChooser.FileName;
            }
        }

        private void selLocFileLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.fileChooser.Title = "Select the Location/Region File for this job:";
            if (this.fileChooser.ShowDialog() == DialogResult.OK)
            {
                this.locFileTxtBox.Text = this.fileChooser.FileName;
            }
        }

        private void bFileLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.fileChooser.Title = "Select the Boundary File for this job:";
            if (this.fileChooser.ShowDialog() == DialogResult.OK)
            {
                this.bFileTxtBox.Text = this.fileChooser.FileName;
            }
        }

        private void initializeScatProc()
        {

            scat2OutBusyList = new List<TextBox>();
            scat2OutFreeList = new List<TextBox>();
            scat2OutBtnBusyList = new List<Button>();
            scat2OutBtnFreeList = new List<Button>();
            scat2OutLblBusyList = new List<Label>();
            scat2OutLblFreeList = new List<Label>();
            jobIDList = new List<int>();

            scat2OutFreeList.Add(scat2Out1); 
            scat2OutFreeList.Add(scat2Out2);
            scat2OutFreeList.Add(scat2Out3);
            scat2OutFreeList.Add(scat2Out4);
            
            scat2OutBtnFreeList.Add(scat2Stop1);
            scat2OutBtnFreeList.Add(scat2Stop2);
            scat2OutBtnFreeList.Add(scat2Stop3);
            scat2OutBtnFreeList.Add(scat2Stop4);

            scat2OutLblFreeList.Add(label7);
            scat2OutLblFreeList.Add(label8);
            scat2OutLblFreeList.Add(label9);
            scat2OutLblFreeList.Add(label10);
        }

        private void checkQueue()
        {
            string sJName, sJFolder, sysProcArgs = "";
            int jobID;
            while (jobIDList.Count < nProcs)
            {
                if ((sysProcArgs=getNextJob(out jobID, out sJName, out sJFolder))!="") {
                    scat2OutFreeList[0].Text = "";
                    new SysProc().startProc(scat2OutFreeList[0], scat2OutBtnFreeList[0], "scat2.exe", sysProcArgs,this,jobID);
                    scat2OutBtnFreeList[0].Show();
                    jobIDList.Add(jobID);
                    scat2OutLblFreeList[0].Text = "Job: " + sJName + " Job Folder: " + sJFolder;
                    scat2OutBusyList.Add(scat2OutFreeList[0]);
                    scat2OutBtnBusyList.Add(scat2OutBtnFreeList[0]);
                    scat2OutLblBusyList.Add(scat2OutLblFreeList[0]);

                    scat2OutFreeList.RemoveAt(0);
                    scat2OutBtnFreeList.RemoveAt(0);
                    scat2OutLblFreeList.RemoveAt(0);
                    //((TextBox)scat2OutBusyList[0]).AppendText(sysProcArgs);
                    updateJobStart(jobID);
                    
                } 
                else 
                {
                    break;
                }
            }
        }

        private void clearJobList()
        {
            jobListTableAdapter.DeleteAllJobs();
        }

        private string getNextJob(out int jID, out string jName, out string jFolder)
        {
            string argStr = "";
            scatdbDataSetL2.JobListDataTable tempTable;
            tempTable = jobListTableAdapter.GetMinDtQueuedRow(jobListTableAdapter.getMinDtQueued());
            jID = 0;
            jName = "";
            jFolder = "";
            foreach (DataRow dR in tempTable.Rows)
            {
                jID = Convert.ToInt16(dR[tempTable.hIDColumn].ToString());
                jName = dR[tempTable.sJobNameColumn].ToString();
                jFolder = dR[tempTable.sOutputFilesPathColumn].ToString();

                argStr += dR[tempTable.sRestrictionColumn].ToString();
                argStr += dR[tempTable.sSuppArgsColumn].ToString();
                argStr += " -A " + dR[tempTable.iUnknownStartColumn].ToString() + " " + dR[tempTable.iUnknownStopColumn].ToString() + " ";
                if (dR[tempTable.sRestrictionColumn].ToString() =="") argStr += "\"-B" + dR[tempTable.sBoundaryFilePathColumn].ToString() + "\" ";
                argStr += "-S " + jID.ToString() + " ";
                argStr += "\"" + dR[tempTable.sInputGenotypeFilePathColumn].ToString() + "\" ";
                argStr += "\"" + dR[tempTable.sInputRegionFilePathColumn].ToString() + "\" ";               
                argStr += "\"" + dR[tempTable.sOutputFilesPathColumn].ToString() + "\" ";
                argStr += dR[tempTable.iNLoci1Column].ToString() + " ";
                argStr += dR[tempTable.iBurnIterationsColumn].ToString() + " ";
                argStr += dR[tempTable.iThinningsColumn].ToString() + " ";
                argStr += dR[tempTable.iNIterationsColumn].ToString() + " ";
                //MessageBox.Show(dR[tempTable.hIDColumn].ToString());                
                
                break;
            }
            //jID = 0;
            //MessageBox.Show(argStr);
            //argStr = "";
            return argStr;
        }

        public void onJobClose(int eCode, int pJobID) 
        {
            if (pJobID != -1)
            {
                System.IO.StreamWriter strWriter;
                int lIndex = jobIDList.IndexOf(pJobID);
                jobIDList.RemoveAt(lIndex);
                try
                {
                    strWriter = new System.IO.StreamWriter(jobListTableAdapter.getOutputFolderByJobID(pJobID) + "\\SCAT2_0.output.window.log.txt");
                    strWriter.Write(scat2OutBusyList[lIndex].Text);
                    strWriter.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Could not open file for export: " + ex.Message);
                    //return 0;
                }
                
                scat2OutFreeList.Add(scat2OutBusyList[lIndex]);
                scat2OutBusyList.RemoveAt(lIndex);
                scat2OutBtnFreeList.Add(scat2OutBtnBusyList[lIndex]);
                scat2OutBtnBusyList.RemoveAt(lIndex);
                scat2OutLblFreeList.Add(scat2OutLblBusyList[lIndex]);
                scat2OutLblBusyList.RemoveAt(lIndex);
                updateJobEnd(pJobID, eCode);
                checkQueue();
            }
        }

        private void updateJobEnd(int jobID, int eCode)
        {
            switch (eCode)
            {
                case 0:
                    //jobListTableAdapter.UpdateJobStatus(2, jobID);
                    jobListTableAdapter.setJobEnd(jobID);
                    break;
                case -1:
                case 1:
                    jobListTableAdapter.UpdateJobStatus(3, jobID);
                    break;               
            }
            refresh_DGV1();
        }

        private void updateJobStart(int jobID)
        {
            //jobListTableAdapter.UpdateJobStatus(0, jobID);            
            jobListTableAdapter.setJobStart(jobID);
            refresh_DGV1();
        }

        private void updateJobToQueue(int jID)
        {
            jobListTableAdapter.UpdateJobStatus(1, jID);
            refresh_DGV1();
            checkQueue();
        }
        private void refresh_DGV1()
        {
            jobListTableAdapter.Fill(scatdbDataSetL2.JobList);            
            dataGridView1_AfterRefresh();
        }

        private void nThinTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        

        private void button4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int vLength = 100;
			int nGrid = 67;
			int nBurn = 100;
			int nIter = 100;
            if (radioButton3.Checked && !File.Exists(voronoiBFile.Text))
            {
                MessageBox.Show("You have selected the boundary file option. Please click on 'Select' and choose a valid boundary file!");
                return;
            }
            if (!int.TryParse(vLengthTBox.Text, out vLength))
            {
                MessageBox.Show("Please enter a valid number for Voronoi Cells");
                vLengthTBox.Focus();
                return;
            }
			if (!int.TryParse(nGridTBox.Text, out nGrid))
            {
                MessageBox.Show("Please enter a valid number for Grid Size");
                nGridTBox.Focus();
                return;
            }			
			if (!int.TryParse(nBurnTBox.Text, out nBurn))
            {
                MessageBox.Show("Please enter a valid number for SCAT Burn-In");
                nBurnTBox.Focus();
                return;
            }
			if (!int.TryParse(nIterTBox.Text, out nIter))
            {
                MessageBox.Show("Please enter a valid number for SCAT Main Iterations");
                nIterTBox.Focus();
                return;
            }
            string[] fileList = new string[postProcessList.dgvHolder.Controls.Count*9];
            string vOutPath = "";
            string sampleListString = "";
            string[] savanahParm = { "", "-d" };
            string[] forestParm =  { "", "-D" };
            string[] bFileParm = { "", "-B\"" + this.voronoiBFile.Text + "\"" };
            DataGridView tempDG;
            DataGridViewRow temp;
            if (postProcessList.dgvHolder.Controls.Count < 1)
            {
                MessageBox.Show("Please select 9 scat2 output files for each sample for Voronoi processing");
                return;
            }
            sampleListString += postProcessList.dgvHolder.Controls.Count.ToString();
            for (int t = 0; t < postProcessList.dgvHolder.Controls.Count; t++)
            {
                tempDG = (DataGridView)postProcessList.dgvHolder.Controls[t];
                temp = tempDG.Rows[0];
                if (tempDG.Rows.Count != 10)
                {
                    MessageBox.Show(temp.Cells["sample"].Value.ToString() + " has " + (tempDG.Rows.Count - 1) + " output files selected. Please select 9 output files for this sample!");
                    return;
                }
                //sampleListString += " " + tempDG.Rows[1].Cells["sample"].Value.ToString().Substring(0, tempDG.Rows[1].Cells["sample"].Value.ToString().Length - 1);
                sampleListString += " " + tempDG.Rows[1].Cells["sample"].Value.ToString();
                for (int k = 1; k < tempDG.Rows.Count; k++)
                {
                    fileList[(t * 9) + k-1] = tempDG.Rows[k].Cells["path"].Value.ToString();                    
                }
            }
            sampleListString.Replace((char)(48), (char)(0));
            this.baseFolderChooser.Description = "Select the Folder for the Voronoi output:";
            if (this.baseFolderChooser.ShowDialog() == DialogResult.OK)
            {
                vOutPath = this.baseFolderChooser.SelectedPath;
            }
            for (int m = 0; m < fileList.Length; m++)
            {
                try
                {
                    if (File.Exists(fileList[m]))
                    {
                        //File.Copy(fileList[m], vOutPath + "\\" + Path.GetFileName(fileList[m]).Substring(0, Path.GetFileName(fileList[m]).Length - 1) + (char)(114 + (m % 9)),true);
                        File.Copy(fileList[m], vOutPath + "\\" + Path.GetFileName(fileList[m]) + (char)(114 + (m % 9)), true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error copying " + (fileList[m]) + ex.ToString());
                    return;
                }
                
            }
            System.IO.File.WriteAllText(vOutPath + "\\" + "sampleList.txt", sampleListString);
            //new SysProc().startProc(textBox2, null, "voronoi.exe", SFOptions[Convert.ToInt16(radioButton1.Checked)] + " " + vOutPath + "\\sampleList.txt " + vOutPath + "\\VoronoiOutput -B" + this.voronoiBFile.Text + " -G " + gridSizeComboBox.Text, this, -1);
            //new SysProc().startProc(textBox2, null, "voronoi.exe", savanahParm[Convert.ToInt16(radioButton1.Checked)] + forestParm[Convert.ToInt16(radioButton2.Checked)] + bFileParm[Convert.ToInt16(radioButton3.Checked)] + " -G " + gridSizeComboBox.Text + " \"" + vOutPath + "\\sampleList.txt\" \"" + vOutPath + "\\VoronoiOutput\"" , this, -1);
            new SysProc().startProc(textBox2, null, "voronoi.exe", "-V " + vLength + " -G " + nGrid + " -b " + nBurn + " -n " + nIter + " " + savanahParm[Convert.ToInt16(radioButton1.Checked)] + forestParm[Convert.ToInt16(radioButton2.Checked)] + bFileParm[Convert.ToInt16(radioButton3.Checked)] + " \"" + vOutPath + "\\sampleList.txt\" \"" + vOutPath + "\\VoronoiOutput\"", this, -1);
            this.tabControl1.SelectedIndex = 4;
            this.label6.Text = "Processing Folder: " + vOutPath + " (" + (radioButton1.Checked ? radioButton1.Text : (radioButton2.Checked ? radioButton2.Text : radioButton3.Text) ) + ")";

        }

        private void treeViewMS1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                voronoiBFile.Text = "";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                voronoiBFile.Text = "";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (postProcessList.dgvHolder.Controls.Count < 1)
            {
                MessageBox.Show("Please select output samples to export");
                return;
            }
            SaveFileDialog sFDialog = new SaveFileDialog();
            sFDialog.Title = "Select a folder and filename to export to:";
            
            if (sFDialog.ShowDialog() == DialogResult.OK && sFDialog.FileName != "")
            {
                if (this.postProcessList.exportData(sFDialog.FileName) ==1)
                {
                    MessageBox.Show("Export Successful");
                    System.Diagnostics.Process.Start(@sFDialog.FileName);
                }
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void maxSessCtl_ValueChanged(object sender, EventArgs e)
        {
            nProcs = Convert.ToInt32(maxSessCtl.Value);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_3(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.fileChooser.Title = "Select the Boundary File for this job:";
            if (this.fileChooser.ShowDialog() == DialogResult.OK)
            {
                this.voronoiBFile.Text = this.fileChooser.FileName;
                this.radioButton1.Checked = false;
                this.radioButton2.Checked = false;
                this.radioButton3.Checked = true;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

       
    }
}
