namespace VendingMachine.Domain
{
    public interface IWallet
    {
        Coin[] GetMoney(Money amount);

        void Put(params Coin[] coin);

        Money Total { get; }

        Coin[] Coins { get; }
    }
}
