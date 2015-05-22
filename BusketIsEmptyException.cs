namespace VendingMachine.Domain
{
    using System;

    public class BusketIsEmptyException : Exception
    {
        public BusketIsEmptyException():base("Диспенсер товаров пуст")
        {
        
        }
    }
}