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

        Dictionary<string, Coin> Coins = new Dictionary<string, Coin>();
        Dictionary<string, CoinStats> Stats = new Dictionary<string, CoinStats>();
        Dictionary<string, CoinConfig> Config = new Dictionary<string, CoinConfig>();
        string[] WorkingCoins = { };

        public MiningManager()
        {
            Application.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();
            timer1.Interval = 60000;
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
            DefineCoins();
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
            foreach (Coin coin in Coins.Values)
            {
                JObject pool = Http.GetResponce("https://" + coin.poolName + ".hashvault.pro/api/network/stats");
                try
                {
                    long diff = Int64.Parse(pool.GetValue("difficulty").ToString());
                    double rew = Int64.Parse(pool.GetValue("value").ToString()) / coin.divider;
                    double priceBTC = 0;
                    double priceUSD = 0;

                    if (coin.marketCapName != null && coin.marketCapName != "")
                    {
                        JObject market = Http.GetResponce("https://api.coinmarketcap.com/v1/ticker/" + coin.marketCapName + "/");
                        priceBTC = Double.Parse(market.GetValue("price_btc").ToString());
                        priceUSD = Double.Parse(market.GetValue("price_usd").ToString());
                    }
                    CoinStats stats = new CoinStats(diff, rew, priceBTC, priceUSD);
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
        }

        private void DefineCoins()
        {
            WorkingCoins = new string[] { };
            foreach (Coin coin in Coins.Values)
            {
                CoinConfig coinConfig = Config[coin.symbol];
                if (!coinConfig.CPU.Equals(null) && coinConfig.CPU.Length > 0 || !coinConfig.GPU.Equals(null) && coinConfig.GPU.Length > 0)
                {
                    string[] newCoin = { coin.symbol };
                    WorkingCoins = WorkingCoins.Concat(newCoin).ToArray();
                }
            }
        }

        private string getActiveCoin()
        {
            Dictionary<string, double> CoinRating = new Dictionary<string, double>();
            foreach(Coin coin in Coins.Values)
            {
                CoinPanel panel = (CoinPanel)mainPanel.Controls[coin.symbol];
                CoinConfig cfg = Config[coin.symbol];
                if (cfg.GPU != null && cfg.GPU.Length > 0 || cfg.CPU != null && cfg.CPU.Length > 0)
                {
                    double rating = panel.getRaiting();
                    if (coin.symbol == ActiveCoin) rating *= 1.1;
                    CoinRating.Add(coin.symbol, rating);
                }
            }
            if (CoinRating.Count == 0) return null;
            CoinRating = CoinRating.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return CoinRating.Last().Key;
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
        }

        private void MiningManager_Resize(object sender, EventArgs e)
        {
            mainPanel.Height = this.Height - 60;
            apply.Location = new Point(apply.Location.X, mainPanel.Height + 4);
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
