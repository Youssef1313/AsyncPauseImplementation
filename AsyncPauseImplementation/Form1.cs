using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncPauseImplementation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;

            AddToLog("Program started.");

            File.WriteAllLines("D:\\urls.txt", new []{ "https://google.com" , "https://google.com" , "https://google.com" , "https://google.com" });

            var urls = File.ReadAllLines("D:\\urls.txt");
            for (var i = 0; i < urls.Length; i++)
            {
                // ToDo: Check if user requested to pause operation, change button text to resume, wait here until user click resume and back the button text to pause.
                var result = await DownloadContentAsync(urls[i]).ConfigureAwait(true); // Because next lines are interacting with UI. (Thanks for your video ^_^)
                File.WriteAllText($"D:\\{i}.txt", result);
                AddToLog($"Downloaded web contents for {urls[i]}");
            }

            AddToLog("Program finished.");

            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;

        }

        private static async Task<string> DownloadContentAsync(string url)
        {
            return await new WebClient().DownloadStringTaskAsync(url).ConfigureAwait(false);
        }

        private void AddToLog(string s)
        {
            richTextBox1.AppendText($"[{DateTime.Now.ToShortTimeString()}]: {s}{Environment.NewLine}");
        }
    }
}
