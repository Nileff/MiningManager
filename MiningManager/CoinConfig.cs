using System;
using System.IO;
using System.Xml.Linq;

namespace MiningManager
{
    public class CoinConfig
    {
        public string configName { get; }
        public double want { get; set; }
        public string GPU { get; set; }
        public string CPU { get; set; }

        public CoinConfig(string coin)
        {
            configName = coin + "Config";
            want = 0;
            GPU = "";
            CPU = "";
        }

        public CoinConfig(XElement configXML, string coin) : this(coin)
        {
            if (configXML != null)
            {
                want = Double.Parse(configXML.Element("want").Value);
                GPU = configXML.Element("GPU.bat").Value;
                if (!File.Exists(GPU)) GPU = "";
                CPU = configXML.Element("CPU.bat").Value;
                if (!File.Exists(CPU)) CPU = "";
            }
        }
        public CoinConfig(CoinConfig config)
        {
            configName = config.configName;
            want = config.want;
            GPU = config.GPU;
            CPU = config.CPU;
        }

        public XElement toXML()
        {
            XElement root = new XElement(configName);
            root.Add(new XElement("want", want));
            root.Add(new XElement("GPU.bat", GPU));
            root.Add(new XElement("CPU.bat", CPU));
            return root;
        }

        public CoinConfig Clone()
        {
            return new CoinConfig(this);
        }
    }
}
