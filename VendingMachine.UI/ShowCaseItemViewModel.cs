using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;

    public class ShowCaseItemViewModel : IShowcaseItem,INotifyPropertyChanged
    {
        public readonly IShowcaseItem Item;

        private int _amount;

        public ShowCaseItemViewModel(IShowcaseItem item)
        {
            this.Item = item;
            string imagePath;
            ImagePaths.TryGetValue(item.Product.GetType(), out imagePath);
            ImagePath = imagePath;
            Amount = item.Amount;
        }

        public int Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
                this.OnPropertyChanged();
            }
        }

        public int Number {get{return Item.Number;}}

        public IProduct Product { get{return Item.Product;} }

        public Money Cost { get{return Item.Cost;}}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string ImagePath { get; private set; }

        private static Dictionary<Type, string> ImagePaths = 
            new Dictionary<Type, string>
            {
                 {typeof(Tea),"/VendingMachine.UI;component/Images/Products/tea.png"},
                 {typeof(Coffe),"/VendingMachine.UI;component/Images/Products/coffe.png"},
                 {typeof(CoffeWithMilk),"/VendingMachine.UI;component/Images/Products/coffe_with_milk.png"},
                 {typeof(Juice),"/VendingMachine.UI;component/Images/Products/juice.png"},
            }; 

    }
}
