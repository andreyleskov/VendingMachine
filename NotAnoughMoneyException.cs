namespace VendingMachine
{
    using System;

    public class NotAnoughMoneyException : Exception
    {
        public NotAnoughMoneyException():base("Недостаточно денег")
        {
            
        }
    }
}