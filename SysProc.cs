using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SCAT_2._0
{
    class SysProc
    {
        //[STAThread]
        private Process proc;
        private int jID;
        public SysProc()
        {
            ;
        }
        //private System.EventArgs sysProcEventArgs;
        public Process startProc(TextBox textBox,  Button button, string procName, string args, MainWnd parent, int thisJobID)
        {
            /*
            Button button = new Button
            {
                Text = "Click me",
                Dock = DockStyle.Top
            };
            TextBox textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Location = new Point(5, 50),
                Dock = DockStyle.Fill
            };
            Form form = new Form
            {
                Text = "Pinger",
                Size = new Size(500, 300),
                Controls = { textBox, button }
            };
            */
            jID = thisJobID;
            Action<string> appendLine = line =>
            {
                MethodInvoker invoker = () => textBox.AppendText(line + "\r\n");
                textBox.BeginInvoke(invoker);
            };

            textBox.AppendText(procName + " " + args + "\r\n");
            ProcessStartInfo psi = new ProcessStartInfo(procName)
            {
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Arguments = args,
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            proc = Process.Start(psi);
            proc.EnableRaisingEvents = true;
            proc.OutputDataReceived += (s, e) => appendLine(e.Data);
            proc.BeginOutputReadLine();
            proc.ErrorDataReceived += (s, e) => appendLine(e.Data);
            proc.BeginErrorReadLine();

            proc.Exited += delegate
            {
                int eCode = 0;
                if (proc.ExitCode == 0)
                {
                    appendLine("Job completed!");       
                    eCode = 0;
                }
                else if (proc.ExitCode < 0)
                {
                    appendLine("Job Aborted! Exit Code: " + proc.ExitCode);
                    eCode = -1;
                }
                else
                {
                    appendLine("Job Stopped! Exit Code: " + proc.ExitCode);
                    eCode = 1;
                }
                if (button != null) button.Hide();
                parent.onJobClose(eCode,jID);
            };

            if (button != null)
            {
                
                button.Click += delegate
                {
                    if (MessageBox.Show("Are you sure you want to terminate this run?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }                    
                };
            }

            //Application.Run(form);
            return proc;
        }
        public Process getProcess() 
        {
            return proc;
        }
    }
}




