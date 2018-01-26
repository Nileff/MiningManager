using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace MiningManager
{
    public partial class CoinPanel : UserControl
    {
        private static readonly string[] Mnemonic = { "", "K", "M", "G", "T", "P" };

        private bool active;
        private CoinConfig config;
        private CoinStats stats;

        public CoinPanel(string coinSymbol)
        {
            InitializeComponent();
            this.Name = coinSymbol;
            this.config = new CoinConfig(coinSymbol);
            this.stats = new CoinStats();

            openFileDialog1.Filter = "bat files|*.bat";
            coinIcon.Image = GetImage(coinSymbol);
            coinSymbol1.Text = this.Name;
            coinSymbol2.Text = this.Name;
        }

        private Image GetImage(string coin)
        {
            ResourceManager rm = Properties.Resources.ResourceManager;
            Bitmap img = (Bitmap)rm.GetObject(coin);
            if (img != null && !active) return ToolStripRenderer.CreateDisabledImage(img);
            return img;
        }

        public void setActive(bool active)
        {
            this.active = active;
            coinIcon.Image = GetImage(this.Name);
        }

        public void setCoinConfig(CoinConfig config)
        {
            this.config = config.Clone();
            wantReward.Text = this.config.want.ToString();
            if (this.config.GPU == null || this.config.GPU == "")
            {
                GPUbat.Text = "GPU.bat empty";
                setGPU.Text = "Set GPU.bat";
            } else
            {
                GPUbat.Text = Regex.Match(this.config.GPU, @"[^\\]*\\[^\\]*\.bat").Value;
                setGPU.Text = "Clear GPU.bat";
            }
            if (this.config.CPU == null || this.config.CPU == "")
            {
                CPUbat.Text = "CPU.bat empty";
                setCPU.Text = "Set CPU.bat";
            }
            else
            {
                CPUbat.Text = Regex.Match(this.config.CPU, @"[^\\]*\\[^\\]*\.bat").Value;
                setCPU.Text = "Clear CPU.bat";
            }
        }

        public void setCoinStats(CoinStats stats)
        {
            this.stats = stats;
            rewInCoins.Text = this.stats.getDayRew(1000).ToString();
            rewInUsd.Text = this.stats.getDayRewUSD(1000).ToString();
            setDiff();
        }

        public CoinConfig getCoinConfig()
        {
            return this.config.Clone();
        }

        public double getRaiting()
        {
            return this.stats.getDayRew(1000) * 100 / this.config.want;
        }

        private void setDiff()
        {
            double roundDiff = this.stats.diff;
            int i = 0;
            string label = Mnemonic[i];
            while (roundDiff > 1000)
            {
                roundDiff /= 1000;
                i++;
                label = Mnemonic[i];
            }
            difficulty.Text = Math.Round(roundDiff, 2).ToString();
            diffMnemonic.Text = label;
        }

        private void wantReward_TextChanged(object sender, EventArgs e)
        {
            string valueStr = ((TextBox)sender).Text;
            config.want = Double.Parse(valueStr);
        }

        private void wantReward_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            string value = ((TextBox)sender).Text;
            if (!Char.IsDigit(number) && number != 8 && (number != 46 || value.IndexOf('.') >= 0))
            {
                e.Handled = true;
            }
        }

        private void setGPU_Click(object sender, EventArgs e)
        {
            if (config.GPU != null && config.GPU.Length > 0)
            {
                config.GPU = "";
                GPUbat.Text = "GPU.bat empty";
                setGPU.Text = "Set GPU.bat";
            }
            else
            {
                openFileDialog1.Title = "GPU.bat";
                openFileDialog1.ShowDialog();
            }
        }

        private void setCPU_Click(object sender, EventArgs e)
        {
            if (config.CPU != null && config.CPU.Length > 0)
            {
                config.CPU = "";
                CPUbat.Text = "CPU.bat empty";
                setCPU.Text = "Set CPU.bat";
            }
            else
            {
                openFileDialog1.Title = "CPU.bat";
                openFileDialog1.ShowDialog();
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string title = ((OpenFileDialog)sender).Title;
            string file = ((OpenFileDialog)sender).FileName;
            string hard = title.Replace(".bat", "");
            if (hard.Equals("GPU"))
            {
                config.GPU = file;
                setGPU.Text = "Clear GPU.bat";
                GPUbat.Text = Regex.Match(config.GPU, @"[^\\]*\\[^\\]*\.bat").Value;
            }
            else
            {
                config.CPU = file;
                setCPU.Text = "Clear CPU.bat";
                CPUbat.Text = Regex.Match(config.CPU, @"[^\\]*\\[^\\]*\.bat").Value;
            }
        }
    }
}
