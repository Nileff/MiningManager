using System;

namespace MiningManager
{
    public class CoinStats
    {
        public long diff { get; }
        public double rew { get; }
        public double priceBTC { get; }
        public double priceUSD { get; }
        public CoinStats()
        {
            this.diff = 1;
            this.rew = 0;
            this.priceBTC = 0;
            this.priceUSD = 0;
        }
        public CoinStats(long diff, double rew, double priceBTC, double priceUSD)
        {
            this.diff = diff;
            this.rew = rew;
            this.priceBTC = priceBTC;
            this.priceUSD = priceUSD;
        }
        public double getDayRew(long hash)
        {
            long Hash24 = hash * 60 * 60 * 24;
            return Math.Round(Hash24 * this.rew / this.diff, 6);
        }
        public double getDayRewUSD(long hash)
        {
            double rew = getDayRew(hash);
            return Math.Round(rew * this.priceUSD, 6);
        }
    }
}
