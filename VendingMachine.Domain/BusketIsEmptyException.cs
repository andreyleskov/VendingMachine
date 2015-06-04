namespace VendingMachine.Domain
{
    using System;

    public class BusketIsEmptyException : Exception
    {
        #region Constructors and Destructors

        public BusketIsEmptyException()
            : base("Диспенсер товаров пуст")
        {
        }

        #endregion
    }
}