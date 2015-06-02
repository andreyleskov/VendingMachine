namespace VendingMachine.Domain
{
    using System.Linq;

    using VendingMachine.Domain.Wallet;

    public static class WalletExtensions
    {
        public static Coin GetCoin(this IWallet wallet, Money value)
        {
            var coins = wallet.GetMoney(value);
            if(coins.Length != 1) 
                throw new NotAnoughCoinsException();

            return coins.First();
        }

        public static Coin GetCoinLike(this IWallet wallet, Coin coin)
        {
            return GetCoin(wallet, coin.Value);
        }
    }
}