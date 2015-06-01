namespace VendingMachine.UI
{
    using System.Collections.Generic;

    using VendingMachine.Domain;

    public class CoinPile
    {
        public Money Value { get; private set; }
        public int Amount { get; set; }
        public string ImagePath { get; private set; }

        public CoinPile(Money money, int amount)
        {
            Value = money;
            Amount = amount;

            string path;
            CoinImagesPaths.TryGetValue(money, out path);
            ImagePath = path;
        }

        private static readonly Dictionary<Money, string> CoinImagesPaths= new Dictionary<Money, string>();

        static CoinPile()
        {
            CoinImagesPaths[Coin.One().Value]  = "/VendingMachine.UI;component/Images/1_coin.png"; 
            CoinImagesPaths[Coin.Two().Value]  = "/VendingMachine.UI;component/Images/2_coin.png"; 
            CoinImagesPaths[Coin.Five().Value] = "/VendingMachine.UI;component/Images/5_coin.png"; 
            CoinImagesPaths[Coin.Ten().Value]  = "/VendingMachine.UI;component/Images/10_coin.png"; 
        }
    }
}