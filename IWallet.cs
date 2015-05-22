namespace VendingMachine.Domain
{
    interface IWallet
    {
        Coin[] GetMoney(Money amount);

        void Put(params Coin[] coin);

        Money Total { get; }
    }
}
