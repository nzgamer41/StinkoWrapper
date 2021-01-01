using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StinkoWrapper
{
    public partial class AcquireAssets : Form
    {
        private bool isDone = false;
        private string[] levelNums = new[]
        {
            "1.1.1", "1.1.2", "1.1.3", "1.1.4", "1.2.1", "1.2.2", "1.2.3", "1.2.4", "1.3.3", "1.4.1", "1.4.2", "1.4.3",
            "1.5.1", "1.5.2", "1.5.3", "1.6.1", "1.6.2", "1.6.3", "1.7.1", "1.7.2", "1.7.3", "1.8.1", "1.8.2", "1.8.3",
            "1.9.1", "1.9.2", "1.9.3", "1.10.1", "1.10.2", "1.10.3", "1.10.4", "1.10.5", "1.10.6", "1.11.1"
        };
        public AcquireAssets()
        {
            InitializeComponent();
        }

        private void AcquireAssets_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() => getSmAssets()));
            thread.Start();

        }

        /// <summary>
        /// Will acquire all assets required to run stinkoman from homestarrunner.com
        /// </summary>
        private void getSmAssets()
        {
            if (!Directory.Exists(".\\levelData"))
            {
                Directory.CreateDirectory(".\\levelData");
            }

            if (!Directory.Exists(".\\externalSwf"))
            {
                Directory.CreateDirectory(".\\externalSwf");
            }

            downloadFile("https://old.homestarrunner.com/stinkogame/v7/stinkogame.swf", ".\\stinkogame.swf");
            while (!isDone)
            {

            }
            downloadFile("https://old.homestarrunner.com/stinkogame/v7/objectLibrary.swf", ".\\objectLibrary.swf");
            while (!isDone)
            {

            }

            foreach (string s in levelNums)
            {
                downloadFile("https://old.homestarrunner.com/stinkogame/v7/levelData/level"+s+".xml", ".\\levelData\\level"+s+".xml");
                while (!isDone)
                {

                }
            }

            foreach (string s in levelNums)
            {
                downloadFile("https://old.homestarrunner.com/stinkogame/v7/externalSwf/cutscene" + s + ".swf", ".\\externalSwf\\cutscene" + s + ".swf");
                while (!isDone)
                {

                }
            }

            this.Invoke((MethodInvoker) delegate { this.Close(); });
        }

        private void downloadFile(string url, string destination)
        {
            listBoxConsole.Invoke((MethodInvoker) delegate
            {
                // Running on the UI thread
                listBoxConsole.Items.Add("Downloading " + url + "...");
                listBoxConsole.SelectedIndex = listBoxConsole.Items.Count - 1;
            });

            isDone = false;
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += WcOnDownloadFileCompleted;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(url),
                    // Param2 = Path to save
                    destination
                );
            }
        }

        private void WcOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
            isDone = true;
        }

        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                progressBar1.Value = e.ProgressPercentage;
            });
        }
    }
}
