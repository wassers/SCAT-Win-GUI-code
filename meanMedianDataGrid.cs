using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace SCAT_2._0
{
    class meanMedianDataGrid
    {
        private List<DataGridView> inputDGVList;
        public Panel dgvHolder;
        private int latLongPrecision;
        private int dgvHolderInitialHeight;

        public meanMedianDataGrid(/*Panel parentPanel*/)
        {
            inputDGVList = new List<DataGridView>();
            dgvHolder = new Panel(); //parentPanel;
            dgvHolder.AutoScroll = true;
            dgvHolder.VerticalScroll.Enabled = true;
            dgvHolder.VerticalScroll.Visible = true;
            dgvHolder.Dock = DockStyle.Fill;
            latLongPrecision = 4;
            dgvHolderInitialHeight = dgvHolder.Height;
            
        }

        public void reset() 
        {
            dgvHolder.Controls.Clear();
            for (int j = 0; j < inputDGVList.Count; j++)
            {
                ((DataGridView)inputDGVList[j]).Dispose();
            }
            inputDGVList.Clear();            
        }

        public void setLatLongPrecision(int precToSet)
        {
            latLongPrecision = precToSet;
        }

        public int getLatLongPrecision()
        {
            return latLongPrecision;
        }

        string formatDouble(double toFormat)
        {
            return toFormat.ToString().Substring(0, toFormat.ToString().Length > (toFormat.ToString().IndexOf(".") + latLongPrecision + 1) ? (toFormat.ToString().IndexOf(".") + latLongPrecision + 1) : toFormat.ToString().Length);
        }

        void initializeDGV(DataGridView dgvToInit, string sName)
        {
            DataGridViewCellStyle sampleSummaryStyleMiddle = new DataGridViewCellStyle();
            DataGridViewCellStyle sampleSummaryStyleLeft;
            DataGridViewCellStyle sampleSummaryStyleRight;
            sampleSummaryStyleMiddle.Font = new System.Drawing.Font(dgvToInit.Font, System.Drawing.FontStyle.Bold);
            sampleSummaryStyleMiddle.BackColor = System.Drawing.Color.LightYellow;
            
            sampleSummaryStyleLeft = sampleSummaryStyleMiddle.Clone();
            sampleSummaryStyleLeft.Alignment = DataGridViewContentAlignment.MiddleLeft;
            sampleSummaryStyleRight = sampleSummaryStyleMiddle.Clone();
            sampleSummaryStyleRight.Alignment = DataGridViewContentAlignment.MiddleRight;
            //sampleSummaryStyleRight.
            dgvToInit.Columns.Add("run", "Run");
            dgvToInit.Columns.Add("sample", "Sample");
            dgvToInit.Columns.Add("ignore", "Ignore");
            dgvToInit.Columns.Add("median", "Median");
            dgvToInit.Columns.Add("mean", "Mean");
            dgvToInit.Columns.Add("label", "Label");
            dgvToInit.Columns.Add("path", "FullPath");
            dgvToInit.Columns["median"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvToInit.Columns["mean"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvToInit.Columns["run"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvToInit.Columns["sample"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvToInit.Columns["label"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvToInit.Columns["ignore"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvToInit.ReadOnly = true;
            dgvToInit.Columns["run"].ReadOnly = true;
            dgvToInit.Columns["sample"].ReadOnly = true;
            dgvToInit.Columns["median"].ReadOnly = true;
            dgvToInit.Columns["mean"].ReadOnly = true;
            dgvToInit.Columns["ignore"].ReadOnly = false;
            dgvToInit.Columns["label"].ReadOnly = false;
            dgvToInit.Columns["path"].Visible = false;

			dgvToInit.Columns["run"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvToInit.Columns["sample"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvToInit.Columns["median"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvToInit.Columns["mean"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvToInit.Columns["ignore"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvToInit.Columns["label"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvToInit.Columns["path"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvToInit.CellValueChanged += new DataGridViewCellEventHandler(dgvToInit_CellValueChanged);
            //dgvToInit.Dock = DockStyle.Top;
            dgvToInit.AllowUserToAddRows = false;
            dgvToInit.AllowUserToDeleteRows = false;            
            dgvToInit.Rows.Add(new string[] {"[# of Runs:", "[Sample: " + sName +"]", "[ Mean:", "","Map Label:",sName });
            
            dgvToInit.Rows[0].DefaultCellStyle = sampleSummaryStyleMiddle;
            dgvToInit.Rows[0].Cells["ignore"].Style = sampleSummaryStyleRight;
            dgvToInit.Rows[0].Cells["median"].Style = sampleSummaryStyleLeft;
            dgvToInit.Rows[0].Cells["mean"].Style = sampleSummaryStyleRight;
            dgvToInit.Rows[0].Cells["sample"].ToolTipText = "Double-click to Expand/Collapse";
            //dgvToInit.Rows[0].AdjustRowHeaderBorderStyle(new DataGridViewAdvancedBorderStyle()
            dgvToInit.CellDoubleClick += new DataGridViewCellEventHandler(dgv_CellDoubleClick);
            dgvHolder.Controls.Add(dgvToInit);                
            inputDGVList.Add(dgvToInit);

            //if (inputDGVList.Count == 1) 


        }

        void dgvToInit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).SelectedCells.Count == 1)                 
            {
                if (((DataGridView)sender).SelectedCells[0].OwningColumn.Name == "ignore")
                {
                    updateDGVRow((DataGridView)sender, ((DataGridView)sender).SelectedCells[0].OwningRow.Index, System.Convert.ToInt16(((DataGridView)sender).SelectedCells[0].OwningRow.Cells["ignore"].Value));
                }
            }
        }

        private bool calcMedianMeanFromFile(ref double[] median, ref double[] mean, string fullPath, int iIgnore)
        {
            outputPointCollection outPoints = new outputPointCollection();
            outPoints.initialize();

            using (System.IO.StreamReader sr = new System.IO.StreamReader(fullPath))
            {
                string fileLine;
                int index = 0;
                while ((fileLine = sr.ReadLine()) != null && !fileLine.Contains("Acceptance"))
                {
                    if (!outPoints.Add(fileLine.Split(' ')))
                    {
                        System.Windows.Forms.MessageBox.Show("Error reading lat/long at line: " + (index + 1).ToString() + " in file: " + fullPath + "! Check File Format");
                        return false;
                    }
                    index++;
                }
                if (median != null) median = outPoints.computeMedian(iIgnore);
                if (mean != null) mean = outPoints.computeCentroid(iIgnore);
            }
            return true;
        }


        void dgv_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null)
            {
                throw new ArgumentException();
            }
            if (dgv.SelectedCells.Count == 1 && (dgv).SelectedCells[0].OwningColumn.Name == "sample")
            {
                if (dgv.SelectedCells[0].OwningRow.Index > 0)
                {
                    (dgv).Rows.Remove((dgv).SelectedCells[0].OwningRow);
                    if ((dgv).Rows.Count <= 1)
                    {
                        dgvHolder.Controls.Remove(dgv);
                        inputDGVList.Remove(dgv);
                        (dgv).Dispose();
                        updateDGVHeights();
                    }
                    else updateDataGridView((dgv));
                }
                else toggleDGVRows(dgv);
            }
        }

        void toggleDGVRows(DataGridView dgvToToggle)
        {
            bool isCollapsed = (dgvToToggle.Rows.GetRowCount(DataGridViewElementStates.Visible) < dgvToToggle.Rows.Count);
            //MessageBox.Show(isCollapsed.ToString());
            for (int i = 1; i < dgvToToggle.Rows.Count; i++)
            {
                if (isCollapsed)
                {
                    dgvToToggle.Rows[i].Visible = true;
                }
                else
                {
                    dgvToToggle.Rows[i].Visible = false;
                }
            }
            //dgvToToggle.Size = new System.Drawing.Size(dgvToToggle.Size.Width, dgvToToggle.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 2 * dgvToToggle.Rows[0].Height);
            updateDGVHeights();
        }

        public void addRow(string runName, System.IO.FileInfo fileI, int iIgnore)
        {           
            int m = getSampleDataGridView(fileI.Name);
            addDataGridViewRow(m, runName, fileI, iIgnore);            
        }

        void updateDataGridView(DataGridView dgvToUpdate)
        {
            double sumLat = 0, sumLong = 0;
            int j = 1;
            for (j = 1; j < dgvToUpdate.Rows.Count; j++)
            {
                sumLat += System.Convert.ToDouble(dgvToUpdate.Rows[j].Cells["median"].Value.ToString().Split(',')[0]);
                sumLong += System.Convert.ToDouble(dgvToUpdate.Rows[j].Cells["median"].Value.ToString().Split(' ')[1]);
                if (inputDGVList.Count>1 && dgvToUpdate.Rows.Count > 5 && dgvHolder.Height > dgvHolder.Parent.Height) dgvToUpdate.Rows[j].Visible = false;
            }
            dgvToUpdate.Rows[0].Cells["median"].Value = formatDouble(sumLat/(j-1)) + ", " + formatDouble(sumLong/(j-1)) + " ]";
            dgvToUpdate.Rows[0].Cells["run"].Value = "[# of Runs: " + (j-1) + "]";
            //dgvToUpdate.Size  = new System.Drawing.Size(dgvToUpdate.Size.Width, dgvToUpdate.Rows.GetRowsHeight(DataGridViewElementStates.Displayed) + 2 * dgvToUpdate.Rows[0].Height);

            updateDGVHeights();
            //dgvToUpdate.AutoSize = true;
        }

        void updateDGVHeights()
        {
            int dgvTotalHeight = 0, iLocationY=0;
            for (int i = 0; i < inputDGVList.Count; i++)
            {
                ((DataGridView)inputDGVList[i]).Size = new System.Drawing.Size(((DataGridView)inputDGVList[i]).Parent.Parent.Width, ((DataGridView)inputDGVList[i]).Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 2 * ((DataGridView)inputDGVList[i]).Rows[0].Height);
                dgvTotalHeight += ((DataGridView)inputDGVList[i]).Height;
                ((DataGridView)inputDGVList[i]).Location = new System.Drawing.Point(0, iLocationY);
                iLocationY += ((DataGridView)inputDGVList[i]).Height;
            }
            if (dgvTotalHeight > dgvHolderInitialHeight)
            {
                dgvHolder.Height = dgvTotalHeight;
            }
            else dgvHolder.Height = dgvHolderInitialHeight;
            dgvHolder.AutoScrollMinSize = new System.Drawing.Size(0, dgvHolder.Height * 2);
        }

        void addDataGridViewRow(int index, string rName, FileInfo fileI, int iIgnore)
        {
            //MessageBox.Show(index.ToString() + " " + rName + " " + sName);
            DataGridView temp;
            int tempRowCount = 0;
            if (index < 0)
            {
                DataGridView newDGV = new DataGridView();
                initializeDGV(newDGV, fileI.Name);
                //newDGV.Rows.Add();
                temp = newDGV;                
            }
            else
            {
                if (sampleAlreadyExists(rName, index)) return;                
                temp = (DataGridView) inputDGVList[index];
            }
            double[] mean, median;
            mean = new double[2];
            median = new double[2];
            temp.Rows.Add();
            calcMedianMeanFromFile(ref median, ref mean, fileI.FullName, iIgnore);
            tempRowCount = temp.Rows.Count-1;
            temp.Rows[tempRowCount].Cells["run"].Value = rName;
            temp.Rows[tempRowCount].Cells["sample"].Value = fileI.Name;
            temp.Rows[tempRowCount].Cells["sample"].ToolTipText = "Double-click to Remove";
            temp.Rows[tempRowCount].Cells["ignore"].Value = "100";
            temp.Rows[tempRowCount].Cells["median"].Value = formatDouble(median[0]) + ", " + formatDouble(median[1]);
            temp.Rows[tempRowCount].Cells["mean"].Value =  formatDouble(mean[0]) + ", " + formatDouble(mean[1]);
            temp.Rows[tempRowCount].Cells["label"].ReadOnly = true;
            temp.Rows[tempRowCount].Cells["path"].Value = fileI.FullName;
            updateDataGridView(temp);
        }

        void updateDGVRow(DataGridView dgvToUpdate, int rowIndex, int iIgnore) 
        {
            double[] mean, median;
            median = new double[2];
            mean = new double[2];
            calcMedianMeanFromFile(ref median, ref mean, dgvToUpdate.Rows[rowIndex].Cells["path"].Value.ToString(), iIgnore);
            dgvToUpdate.Rows[rowIndex].Cells["median"].Value = formatDouble(median[0]) + ", " + formatDouble(median[1]);
            dgvToUpdate.Rows[rowIndex].Cells["mean"].Value = formatDouble(mean[0]) + ", " + formatDouble(mean[1]);
            updateDataGridView(dgvToUpdate);
        }

        public void removeRow(int remDGVIndex, int remIndex)
        {
            updateDataGridView((DataGridView)inputDGVList[remDGVIndex]);
        }

        bool sampleAlreadyExists(string rName, int index)
        {
            for (int k = 0; k < ((DataGridView)inputDGVList[index]).Rows.Count; k++)
            {
                if (((DataGridView)inputDGVList[index]).Rows[k].Cells["run"].Value.ToString() == rName) return true;
            }
            return false;
        }

        int getSampleDataGridView(string sName)
        {
            for (int k = 0; k < inputDGVList.Count; k++ )
            {
                for (int j = 0; j < ((DataGridView)inputDGVList[k]).Rows.Count; j++)
                {
                    if ( ((DataGridView)inputDGVList[k]).Rows[j].Cells["sample"].Value.ToString() == sName)
                    {
                        return k;
                    }
                    if (j > 0) break;
                }
            }
            return -1;
        }

        
        public int exportData(string fPath)
        {
            System.IO.StreamWriter streamWriter;
            try
            {
                streamWriter = new System.IO.StreamWriter(fPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open file for export: " + ex.Message);
                return 0;
            }
            string strHeader = "", strRowValue ="";
            for (int k = 0; k < inputDGVList.Count; k++ )
            {
                strHeader = "";
                for (int i = 0; i < inputDGVList[k].Columns.Count; i++)
                {
                    if ((i==0) || (i==1) || (i==3) || (i==4)) strHeader += inputDGVList[k].Columns[i].HeaderText + "\t";
                }
                streamWriter.WriteLine(strHeader);
                for (int m = 1; m < inputDGVList[k].Rows.Count; m++)
                {
                    strRowValue = "";
                    for (int n = 0; n < inputDGVList[k].Columns.Count; n++)
                    {
                        if ((n == 0) || (n == 1) || (n == 3) || (n == 4)) strRowValue += inputDGVList[k].Rows[m].Cells[n].Value + "\t ";                        
                    }
                    streamWriter.WriteLine(strRowValue);
                }
                streamWriter.WriteLine("Sample Mean:\t" + inputDGVList[k].Rows[0].Cells[3].Value.ToString().Substring(0, inputDGVList[k].Rows[0].Cells[3].Value.ToString().Length - 1));
                streamWriter.WriteLine("");
            }
            streamWriter.Close();
            return 1;
        }
    }
}
