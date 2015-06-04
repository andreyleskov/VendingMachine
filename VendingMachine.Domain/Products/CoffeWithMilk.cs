namespace VendingMachine.Domain.Products
{
    public class CoffeWithMilk : IProduct
    {
        #region Public Properties

        public string Name
        {
            get
            {
                return "Кофе с молоком";
            }
        }

        #endregion
    }
}