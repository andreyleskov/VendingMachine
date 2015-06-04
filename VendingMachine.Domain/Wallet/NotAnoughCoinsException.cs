namespace VendingMachine.Domain.Wallet
{
    using System;

    public class NotAnoughCoinsException : Exception
    {
        #region Constructors and Destructors

        public NotAnoughCoinsException()
            : base("Недостаточно монет для запрошенной суммы")
        {
        }

        #endregion
    }
}