using System;

namespace VendingMachine.Domain
{
    public class ProductIsOutOfStockException : Exception
    {
        #region Constructors and Destructors

        public ProductIsOutOfStockException(IProduct product)
            : base(product.Name + " нет в наличии")
        {
        }

        #endregion
    }
}