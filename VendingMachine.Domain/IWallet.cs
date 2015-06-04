namespace VendingMachine.Domain
{
    public interface IWallet
    {
        #region Public Properties

        Coin[] Coins { get; }

        Money Total { get; }

        #endregion

        #region Public Methods and Operators

        Coin[] GetMoney(Money amount);

        void Put(params Coin[] coin);

        #endregion
    }
}