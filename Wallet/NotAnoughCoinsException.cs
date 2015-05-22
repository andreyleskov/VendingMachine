namespace VendingMachine.Domain.Wallet
{
    using System;

    public class NotAnoughCoinsException : Exception
    {
        public NotAnoughCoinsException()
            : base("Недостаточно монет для запрошенной суммы")
        {
        }
    }
}