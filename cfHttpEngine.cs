using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SCAT_2._0
{
    public delegate string newRequestHandler(object sender, string e);
    class cfHttpEngine
    {
        private System.Net.HttpListener hListener;
        private string listenPort;

       
        public event newRequestHandler newRequest;

        public cfHttpEngine()
        {           
            listenPort = "3001";                     
        }

        public cfHttpEngine(string port)
        {            
            listenPort = port;
        }

        public bool start()
        {
            if (!System.Net.HttpListener.IsSupported) 
            {
                System.Windows.Forms.MessageBox.Show("Windows XP SP2 or later required");
                return false;
            }
            hListener = new System.Net.HttpListener();
            hListener.Prefixes.Add("http://+:" + listenPort + "/");
            try
            {
                hListener.Start();
                System.IAsyncResult listenerResult = hListener.BeginGetContext(new AsyncCallback(processData), hListener);
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Could not start httpListener. Details: " + e.Message);
                return false;
            }
        }

        public void Dispose()
        {
            hListener.Close();            
        }

        protected virtual string raiseNewRequest(string request)
        {
            if (newRequest != null)
            {
                return newRequest(this, request);
            }
            else return null;
        }
        
        void processData(IAsyncResult result)
        {
            //System.Windows.Forms.MessageBox.Show("in process data");
            string reqString;
            HttpListener thisListener = (HttpListener) result.AsyncState;
            HttpListenerContext thisContext = thisListener.EndGetContext(result);
            HttpListenerRequest thisRequest = thisContext.Request;
            HttpListenerResponse thisResponse = thisContext.Response;     
            
            try
            {
                System.IO.Stream body = thisRequest.InputStream;
                System.Text.Encoding encoding = thisRequest.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                reqString = reader.ReadToEnd();
                byte[] outBuffer = System.Text.Encoding.UTF8.GetBytes(raiseNewRequest(reqString));
                
                thisContext.Response.ContentLength64 = outBuffer.Length;
                thisContext.Response.OutputStream.Write(outBuffer, 0, outBuffer.Length);
                thisContext.Response.OutputStream.Close();
                System.IAsyncResult listenerResult = hListener.BeginGetContext(new AsyncCallback(processData), hListener);
                
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error reading http request: " + e.Message);
            }
        }
    }
}
