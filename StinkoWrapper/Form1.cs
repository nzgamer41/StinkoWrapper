using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxShockwaveFlashObjects;

namespace StinkoWrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(".\\stinkogame.swf"))
            {
                AcquireAssets aa = new AcquireAssets();
                aa.ShowDialog();
            }

            string path = Directory.GetCurrentDirectory();
            axShockwaveFlash1.LoadMovie(0, path + "\\stinkogame.swf");
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void resetFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axShockwaveFlash1.Stop();
            axShockwaveFlash1 = new AxShockwaveFlash();
            File.Delete(".\\stinkogame.swf");

            AcquireAssets aa = new AcquireAssets();
            aa.ShowDialog();
            string path = Directory.GetCurrentDirectory();
            axShockwaveFlash1.LoadMovie(0, path + "\\stinkogame.swf");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("StinkoWrapper, written in C# by Alex Cheer (nzgamer41)");
        }

        private void hideToolbarrelaunchProgramForItToReappearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Visible = false;
            menuStrip1.Enabled = false;
            axShockwaveFlash1.Size = new Size(760,537);
            this.Height += 15;
        }
    }
}
