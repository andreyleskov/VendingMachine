namespace VendingMachine.Domain
{
    using System.Linq;

    using VendingMachine.Domain.Wallet;

    public static class WalletExtensions
    {
        #region Public Methods and Operators

        public static Coin GetCoin(this IWallet wallet, Money value)
        {
            Coin[] coins = wallet.GetMoney(value);
            if (coins.Length != 1)
            {
                throw new NotAnoughCoinsException();
            }

            return coins.First();
        }

        public static Coin GetCoinLike(this IWallet wallet, Coin coin)
        {
            return GetCoin(wallet, coin.Value);
        }

        #endregion
    }
}