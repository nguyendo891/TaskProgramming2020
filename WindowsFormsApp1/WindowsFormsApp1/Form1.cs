using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnCalculate_Click(object sender, EventArgs e)
        {
            //var calculation = CalculateValueAsync();
            //calculation.ContinueWith(t =>
            //{
            //    LblResult.Text = t.Result.ToString();

            //}, TaskScheduler.FromCurrentSynchronizationContext());
            int value = await CalculateValueAsync();
            LblResult.Text = value.ToString();

            await Task.Delay(5000);

            using( var wc = new WebClient())
            {
                string data = await
                    wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                LblResult.Text = data.Split('\n')[0].Trim();
            }
        }
    }
}
