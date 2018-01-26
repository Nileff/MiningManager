namespace MiningManager
{
    public class Coin
    {
        public string symbol { get; }
        public long divider { get; }
        public string poolName { get; }
        public string marketCapName { get; }

        public Coin(string symbol, long divider, string poolName, string marketCapName = null)
        {
            this.symbol = symbol;
            this.divider = divider;
            this.poolName = poolName;
            this.marketCapName = marketCapName;
        }
    }
}
