namespace VendingMachine.UI
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    using VendingMachine.Domain;

    public class CoinPile: PileViewModelOf<Coin>
    {
        private static readonly Dictionary<Coin, string> CoinImagesPaths = 
            new Dictionary<Coin, string>()
            {
                {Domain.Coin.One(),"/VendingMachine.UI;component/Images/Coins/1_coin.png"},
                {Domain.Coin.Two(),"/VendingMachine.UI;component/Images/Coins/2_coin.png"},
                {Domain.Coin.Five(),"/VendingMachine.UI;component/Images/Coins/5_coin.png"},
                {Domain.Coin.Ten(),"/VendingMachine.UI;component/Images/Coins/10_coin.png"}
            };

        public CoinPile(Coin product, int amount)
            : base(product, amount, GetImagePath(product))
        {
        }

        public string Money
        {
            get
            {
                return Item.Value.ToUIString();
            }
        }

        private static string GetImagePath(Coin coin)
        {
            string imagePath;
            CoinImagesPaths.TryGetValue(coin, out imagePath);
            return imagePath;
        }
    }
}