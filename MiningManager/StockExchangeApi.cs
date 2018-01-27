using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MiningManager
{
    public static class StockExchangeApi
    {
        private static readonly string url = "https://stocks.exchange/api2/prices";
        private static JArray marketPrices = new JArray();
        private static DateTime lastUpdate = new DateTime(1970, 1, 1);

        private static JArray getMarketPrice()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                (object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; }
            );
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebRequest request = WebRequest.Create(url);
            string responseText;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    responseText = sr.ReadToEnd();
                }
                try
                {
                    return JArray.Parse(responseText);
                }
                catch { }
            }
            catch (WebException e) { }
            return null;
        }

        public static double getCoinPrice(string coinSymbol)
        {
            string marketName = coinSymbol + "_BTC";
            if (lastUpdate < DateTime.Now.AddMinutes(-5))
            {
                JArray newMarketPrices = getMarketPrice();
                if (newMarketPrices != null)
                {
                    marketPrices = newMarketPrices;
                    lastUpdate = DateTime.Now;
                }
            }
            JObject match = marketPrices.Values<JObject>()
            .Where(p => p["market_name"].Value<string>() == marketName)
            .FirstOrDefault();
            if (match == null) return 0;
            return match["buy"].Value<double>();
        }
    }
}
