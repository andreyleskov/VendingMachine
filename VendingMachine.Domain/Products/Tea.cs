namespace VendingMachine.Domain.Products
{
    public class Tea : IProduct
    {
        #region Public Properties

        public string Name
        {
            get
            {
                return "Чай";
            }
        }

        #endregion
    }
}