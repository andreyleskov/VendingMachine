namespace VendingMachine.Domain.Wallet
{
    using System;

    public class NotAnoughMoneyException : Exception
    {
        #region Constructors and Destructors

        public NotAnoughMoneyException()
            : base("Недостаточно средств")
        {
        }

        #endregion
    }
}