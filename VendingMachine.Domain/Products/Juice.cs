namespace VendingMachine.Domain.Products
{
    public class Juice : IProduct
    {
        #region Public Properties

        public string Name
        {
            get
            {
                return "Сок";
            }
        }

        #endregion
    }
}