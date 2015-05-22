namespace VendingMachine.Domain
{
    using System;

    public class MoneyCurrencyMismatchException : Exception
    {
        public MoneyCurrencyMismatchException():base("Виды валют не совпадают")
        {
        
        }
    }
}