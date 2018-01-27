using System;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Resources;

namespace MiningManager
{
    public partial class MiningManager : Form
    {
        static readonly string configFile = "config.xml";
        private string logFile;
        static XDocument XMLConfig;
        static readonly string[] MinersName = { "NsCpuCNMiner64", "sgminer" };

        string ActiveCoin;
        DateTime LastChangeCoin = new DateTime(1970, 1, 1);
        double bitcoinPrice = 0;

        Dictionary<string, Coin> Coins = new Dictionary<string, Coin>();
        Dictionary<string, CoinStats> Stats = new Dictionary<string, CoinStats>();
        Dictionary<string, CoinConfig> Config = new Dictionary<string, CoinConfig>();

        public MiningManager()
        {
            Application.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();
            timer1.Interval = 5 * 60 * 1000;
            logFile = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";
            writeLog("start programm");

            Coins.Add("XMR", new Coin("XMR", 1000000000000, "monero", "monero"));
            Coins.Add("KRB", new Coin("KRB", 1000000000000, "karbo", "karbowanec"));
            Coins.Add("SUMO", new Coin("SUMO", 1000000000, "sumokoin", "sumokoin"));
            Coins.Add("ETN", new Coin("ETN", 100, "electroneum", "electroneum"));
            Coins.Add("ITNS", new Coin("ITNS", 100000000, "intense", "intensecoin"));
            Coins.Add("FNO", new Coin("FNO", 1000000000000, "fonero"));

            int i = 0;
            foreach (Coin coin in Coins.Values)
            {
                Stats.Add(coin.symbol, new CoinStats());
                CoinPanel panel = new CoinPanel(coin.symbol);
                panel.Location = new Point(0, 110 * i);
                mainPanel.Controls.Add(panel);
                i++;
            }
        }

        private void MiningManager_Load(object sender, EventArgs e)
        {
            try
            {
                XMLConfig = XDocument.Load(configFile);
            }
            catch
            {
                XMLConfig = new XDocument();
                XMLConfig.Add(new XElement("MiningManagerConfig"));
            }
            XElement root = XMLConfig.Root;

            foreach (Coin coin in Coins.Values)
            {
                CoinConfig config = new CoinConfig(root.Element(coin.symbol + "Config"), coin.symbol);
                Config.Add(coin.symbol, config);
                CoinPanel panel = (CoinPanel)mainPanel.Controls[coin.symbol];
                panel.setCoinConfig(config);
            }
        }

        private void MiningManager_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            getStats();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            setStats();
        }

        private void getStats()
        {
            Application.CurrentCulture = new CultureInfo("en-US");
            JObject bitcoinMarket = Http.GetResponce("https://api.coinmarketcap.com/v1/ticker/bitcoin/");
            bitcoinPrice = bitcoinMarket["price_usd"].Value<double>();
            foreach (Coin coin in Coins.Values)
            {
                JObject pool = Http.GetResponce("https://" + coin.poolName + ".hashvault.pro/api/network/stats");
                try
                {
                    long diff = pool["difficulty"].Value<long>();
                    double rew = pool["value"].Value<long>() / coin.divider;
                    double priceBTC = 0;

                    if (coin.marketCapName != null && coin.marketCapName != "")
                    {
                        JObject market = Http.GetResponce("https://api.coinmarketcap.com/v1/ticker/" + coin.marketCapName + "/");
                        priceBTC = market["price_btc"].Value<double>();
                    } else
                    {
                        priceBTC = StockExchangeApi.getCoinPrice(coin.symbol);
                    }
                    CoinStats stats = new CoinStats(diff, rew, priceBTC, bitcoinPrice);
                    Stats[coin.symbol] = stats;
                }
                catch { }
            }
        }

        private void setStats()
        {
            foreach (Coin coin in Coins.Values)
            {
                CoinStats stats = Stats[coin.symbol];
                CoinPanel panel = (CoinPanel)mainPanel.Controls[coin.symbol];
                if (panel != null) panel.setCoinStats(stats);
            }
            setActiveCoin();
            apply.Enabled = true;
        }

        private string getActiveCoin()
        {
            Dictionary<string, double> CoinRating = new Dictionary<string, double>();
            Dictionary<string, double> WorkCoinRating = new Dictionary<string, double>();
            foreach (Coin coin in Coins.Values)
            {
                CoinPanel panel = (CoinPanel)mainPanel.Controls[coin.symbol];
                CoinConfig cfg = Config[coin.symbol];
                CoinRating.Add(coin.symbol, panel.getRaiting());
            }
            foreach (string coinSymbol in CoinRating.Keys)
            {
                CoinConfig cfg = Config[coinSymbol];
                if (cfg.GPU != null && cfg.GPU.Length > 0 || cfg.CPU != null && cfg.CPU.Length > 0)
                {
                    double rating = CoinRating[coinSymbol];
                    if (coinSymbol == ActiveCoin) rating *= 1.1;
                    WorkCoinRating.Add(coinSymbol, rating);
                }
            }
            CoinRating = CoinRating.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            WorkCoinRating = WorkCoinRating.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            string newActiveCoin = null;
            if (WorkCoinRating.Count > 0) newActiveCoin = WorkCoinRating.Last().Key;
            fillCoinStatusPanel(CoinRating.Keys.ToArray<string>(), newActiveCoin);
            return newActiveCoin;
        }

        private void fillCoinStatusPanel(string[] Coins, string ActiveCoin)
        {
            coinStatusPanel.Controls.Clear();
            foreach(string Coin in Coins.Reverse())
            {
                PictureBox pic = new PictureBox();
                pic.Margin = new Padding(0, 0, 5, 0);
                pic.Height = 24;
                pic.Width = 24;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Image = GetImage(Coin, Coin == ActiveCoin || ActiveCoin == null);
                coinStatusPanel.Controls.Add(pic);
            }
        }

        private Image GetImage(string coin, bool active = false)
        {
            ResourceManager rm = Properties.Resources.ResourceManager;
            Bitmap img = (Bitmap)rm.GetObject(coin);
            if (img != null && !active) return ToolStripRenderer.CreateDisabledImage(img);
            return img;
        }

        private void setActiveCoin()
        {
            string newActiveCoin = getActiveCoin();
            if (newActiveCoin == null && ActiveCoin != null)
            {
                killMiners();

                CoinPanel panel = (CoinPanel)mainPanel.Controls[ActiveCoin];
                panel.setActive(false);
                writeLog("End mining " + ActiveCoin);

                ActiveCoin = newActiveCoin;
            }
            else if (newActiveCoin != null && !newActiveCoin.Equals(ActiveCoin) && LastChangeCoin < DateTime.Now.AddMinutes(-30))
            {
                killMiners();

                CoinPanel panel = (CoinPanel)mainPanel.Controls[newActiveCoin];
                panel.setActive(true);

                if (ActiveCoin != null)
                {
                    panel = (CoinPanel)mainPanel.Controls[ActiveCoin];
                    panel.setActive(false);
                    writeLog("End mining " + ActiveCoin);
                }
                writeLog("Start mining " + newActiveCoin);

                CoinConfig cfg = Config[newActiveCoin];
                if (cfg.GPU != null && cfg.GPU.Length > 0) startProcess(cfg.GPU);
                if (cfg.CPU != null && cfg.CPU.Length > 0) startProcess(cfg.CPU);
                LastChangeCoin = DateTime.Now;

                ActiveCoin = newActiveCoin;
            }
        }

        private void killMiners()
        {
            Process[] procArr = Process.GetProcessesByName("cmd");
            foreach (Process proc in procArr)
            {
                proc.Kill();
                proc.WaitForExit();
            }
            foreach (Process proc in Process.GetProcesses())
            {
                if (Array.IndexOf(MinersName, proc.ProcessName) >= 0)
                {
                    proc.Kill();
                    proc.WaitForExit();
                }
            }
        }

        private void startProcess(string file)
        {
            int dirIndex = file.LastIndexOf('\\');
            var processInfo = new ProcessStartInfo(file);
            processInfo.WorkingDirectory = file.Substring(0, dirIndex);
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            Process.Start(processInfo);
        }

        private void apply_Click(object sender, EventArgs e)
        {
            XMLConfig.Root.RemoveAll();
            foreach (Coin coin in Coins.Values)
            {
                CoinPanel panel = (CoinPanel)mainPanel.Controls[coin.symbol];
                Config[coin.symbol] = panel.getCoinConfig();
                XMLConfig.Root.Add(Config[coin.symbol].toXML());
            }
            XMLConfig.Save(configFile);
            setActiveCoin();
        }

        private void MiningManager_Resize(object sender, EventArgs e)
        {
            mainPanel.Height = this.Height - 60;
            apply.Location = new Point(apply.Location.X, mainPanel.Height + 4);
            coinStatusPanel.Location = new Point(0, mainPanel.Height + 2);
        }

        private void writeLog(string msg)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (!File.Exists(logFile))
            {
                using (StreamWriter sw = File.CreateText(logFile))
                {
                    sw.WriteLine(date + " - " + msg);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(logFile))
                {
                    sw.WriteLine(date + " - " + msg);
                }
            }
        }
    }
}
