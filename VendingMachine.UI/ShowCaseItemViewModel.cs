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

    public class ShowCaseItemViewModel : IPileViewModelOf<IProduct>, IShowcaseItem
    {
        private readonly PileViewModelOf<IProduct> _pile;

        private readonly IShowcaseItem _item;

        public ShowCaseItemViewModel(IShowcaseItem item)
        {
            this._item = item;
            _pile = new ProductPile(item.Product, item.Amount);
        }

        public int Amount
        {
            get { return _pile.Amount; }
            set { _pile.Amount = value; }
        }

        public int Number { get { return _item.Number; } }

        public IProduct Product { get { return _item.Product; } }

        public Money Cost { get { return _item.Cost; } }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _pile.PropertyChanged += value;
            }
            remove
            {
                _pile.PropertyChanged -= value;
            }
        }

        public IProduct Item { get {return Product;} }

        public string ImagePath { get{ return _pile.ImagePath;} }
    }
}
