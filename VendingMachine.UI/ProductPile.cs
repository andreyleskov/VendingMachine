namespace VendingMachine.UI
{
    using System;
    using System.Collections.Generic;

    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;

    public class ProductPile: PileViewModelOf<IProduct>
    {
        private static readonly Dictionary<Type, string> ProductImagePaths =
            new Dictionary<Type, string>
                {
                    {typeof(Tea),"/VendingMachine.UI;component/Images/Products/tea.png"},
                    {typeof(Coffe),"/VendingMachine.UI;component/Images/Products/coffe.png"},
                    {typeof(CoffeWithMilk),"/VendingMachine.UI;component/Images/Products/coffe_with_milk.png"},
                    {typeof(Juice),"/VendingMachine.UI;component/Images/Products/juice.png"},
                };

        public ProductPile(IProduct product, int amount)
            : base(product, amount, GetImagePath(product))
        {
        }

        public IProduct Product { get { return Item; } }

        private static string GetImagePath(IProduct product)
        {
            string imagePath;
            ProductImagePaths.TryGetValue(product.GetType(), out imagePath);
            return imagePath;
        }
    }
}