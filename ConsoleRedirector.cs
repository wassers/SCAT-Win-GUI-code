using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace ConsoleRedirector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ConsoleRedirector.attach(null, false);
            Console.WriteLine("Still goes to the console");
            ConsoleRedirector.detatch();
            Console.WriteLine("So does this");
            ConsoleRedirector.attach(null, true);
            Console.WriteLine("But not this");
            ConsoleRedirector.detatch();
        }

        void WriteStdOutputToTextBox(object sender, ProgressChangedEventArgs e)
        {
            txtFromStandardOut.AppendText((string)e.UserState);
        }

        private void btnSendToConsole_Click(object sender, EventArgs e)
        {
            Console.WriteLine(txtToStdOut.Text);
        }

        private void btnDetach_Click(object sender, EventArgs e)
        {
            ConsoleRedirector.detatch();
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {

            ConsoleRedirector.attach(WriteStdOutputToTextBox, true);
        }

    }

    public class ConsoleRedirector : IDisposable
    {
        private static ConsoleRedirector _instance;

        public static void attach(ProgressChangedEventHandler handler, bool forceConsoleRedirection)
        {
            Debug.Assert(null == _instance);
            _instance = new ConsoleRedirector(handler, forceConsoleRedirection);

        }

        public static void detatch()
        {
            _instance.Dispose();
            _instance = null;
        }

        public static bool isAttached
        {
            get
            {
                return null != _instance;
            }
        }

        private static void ResetConsoleOutStream()
        {
            //Force console to recreate its output stream the next time Write/WriteLine is called
            typeof(Console).GetField("_out", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).SetValue(null, null);
        }

        private const int PERIOD = 500;
        private const int BUFFER_SIZE = 4096;
        private volatile bool _isDisposed;
        private BackgroundWorker _worker;
        private readonly IntPtr _stdout;
        private readonly Mutex _sync;
        private readonly System.Threading.Timer _timer;
        private readonly char[] _buffer;
        private readonly AnonymousPipeServerStream _outServer;
        private readonly TextReader _outClient;
        private readonly bool _forceConsoleRedirection;

        private StreamWriter _consoleStandardOut;

        private ConsoleRedirector(ProgressChangedEventHandler handler, bool forceConsoleRedirection)
        {
            bool ret;
            _forceConsoleRedirection = forceConsoleRedirection;

            if (!_forceConsoleRedirection)
            {
                //Make sure Console._out is initialized before we redirect stdout, so the redirection won't affect it
                TextWriter temp = Console.Out;
            }

            AnonymousPipeClientStream client;

            _worker = new BackgroundWorker();
            _worker.ProgressChanged += handler;
            _worker.DoWork += _worker_DoWork;
            _worker.WorkerReportsProgress = true;

            _stdout = GetStdHandle(STD_OUTPUT_HANDLE);

            _sync = new Mutex();
            _buffer = new char[BUFFER_SIZE];

            _outServer = new AnonymousPipeServerStream(PipeDirection.Out);
            client = new AnonymousPipeClientStream(PipeDirection.In, _outServer.ClientSafePipeHandle);
            Debug.Assert(_outServer.IsConnected);
            _outClient = new StreamReader(client, Encoding.Default);
            ret = SetStdHandle(STD_OUTPUT_HANDLE, _outServer.SafePipeHandle.DangerousGetHandle());
            Debug.Assert(ret);

            if (_forceConsoleRedirection)
            {
                ResetConsoleOutStream(); //calls to Console.Write/WriteLine will now get made against the redirected stream
            }

            _worker.RunWorkerAsync(_outClient);

            _timer = new System.Threading.Timer(flush, null, PERIOD, PERIOD);

        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            TextReader client = (TextReader)e.Argument;
            try
            {
                while (true)
                {
                    int read = client.Read(_buffer, 0, BUFFER_SIZE);
                    if (read > 0)
                        worker.ReportProgress(0, new string(_buffer, 0, read));
                }
            }
            catch (ObjectDisposedException)
            {
                // Pipe was closed... terminate

            }
            catch (Exception ex)
            {

            }
        }

        private void flush(object state)
        {
            _outServer.Flush();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~ConsoleRedirector()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                lock (_sync)
                {
                    if (!_isDisposed)
                    {
                        _isDisposed = true;
                        _timer.Change(Timeout.Infinite, Timeout.Infinite);
                        _timer.Dispose();
                        flush(null);

                        try { SetStdHandle(STD_OUTPUT_HANDLE, _stdout); }
                        catch (Exception) { }
                        _outClient.Dispose();
                        _outServer.Dispose();

                        if (_forceConsoleRedirection)
                        {
                            ResetConsoleOutStream(); //Calls to Console.Write/WriteLine will now get redirected to the original stdout stream
                        }

                    }
                }
            }
        }

        private const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(int nStdHandle);
    }

    partial class Form1
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
            this.txtFromStandardOut = new System.Windows.Forms.TextBox();
            this.txtToStdOut = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendToConsole = new System.Windows.Forms.Button();
            this.btnDetach = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFromStandardOut
            // 
            this.txtFromStandardOut.Location = new System.Drawing.Point(9, 25);
            this.txtFromStandardOut.Multiline = true;
            this.txtFromStandardOut.Name = "txtFromStandardOut";
            this.txtFromStandardOut.Size = new System.Drawing.Size(193, 181);
            this.txtFromStandardOut.TabIndex = 0;
            // 
            // txtToStdOut
            // 
            this.txtToStdOut.Location = new System.Drawing.Point(290, 25);
            this.txtToStdOut.Name = "txtToStdOut";
            this.txtToStdOut.Size = new System.Drawing.Size(116, 22);
            this.txtToStdOut.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "To Stdout:";
            // 
            // btnSendToConsole
            // 
            this.btnSendToConsole.Location = new System.Drawing.Point(215, 53);
            this.btnSendToConsole.Name = "btnSendToConsole";
            this.btnSendToConsole.Size = new System.Drawing.Size(191, 23);
            this.btnSendToConsole.TabIndex = 3;
            this.btnSendToConsole.Text = "Call Console.WriteLine";
            this.btnSendToConsole.UseVisualStyleBackColor = true;
            this.btnSendToConsole.Click += new System.EventHandler(this.btnSendToConsole_Click);
            // 
            // btnDetach
            // 
            this.btnDetach.Location = new System.Drawing.Point(331, 183);
            this.btnDetach.Name = "btnDetach";
            this.btnDetach.Size = new System.Drawing.Size(75, 23);
            this.btnDetach.TabIndex = 4;
            this.btnDetach.Text = "Detach";
            this.btnDetach.UseVisualStyleBackColor = true;
            this.btnDetach.Click += new System.EventHandler(this.btnDetach_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(209, 183);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(75, 23);
            this.btnAttach.TabIndex = 5;
            this.btnAttach.Text = "Attach";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Redirected StdOut shows here";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 219);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAttach);
            this.Controls.Add(this.btnDetach);
            this.Controls.Add(this.btnSendToConsole);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtToStdOut);
            this.Controls.Add(this.txtFromStandardOut);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFromStandardOut;
        private System.Windows.Forms.TextBox txtToStdOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendToConsole;
        private System.Windows.Forms.Button btnDetach;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Label label2;
    }
}