namespace VendingMachine.Domain.Wallet
{
    using System;

    public class NotAnoughMoneyException : Exception
    {
        public NotAnoughMoneyException():base("Недостаточно денег")
        {
            
        }
    }
}