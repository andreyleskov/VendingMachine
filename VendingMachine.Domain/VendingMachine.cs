using System;

using VendingMachine.Domain;

namespace VendingMachine.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::VendingMachine.Domain.Wallet;

    public class NumpadVendingMachine : IVendingMachine
    {
        public IReadOnlyList<IShowcaseItem> Showcase
        {
            get
            {
                return _buskets.Values.Cast<IShowcaseItem>().ToList();
            }
        }

        public Money Balance { get; private set; }

        private readonly IWallet _machineWallet;

        private readonly IDictionary<int, ProductBusket> _buskets;

        public NumpadVendingMachine(IWallet machineWallet,
                                    params ProductBusket[] buskets)
        {
            this._buskets = buskets == null ? new Dictionary<int, ProductBusket>() : buskets.ToDictionary(b => b.Number, b => b);
            this._machineWallet = machineWallet;
            Balance = Money.Zero;
        }

        public void Insert(Coin coin)
        {
            _machineWallet.Put(coin);
            Balance += coin.Value;
        }

        public Coin[] GetChange()
        {
            return _machineWallet.GetMoney(Balance);
        }

        public IProduct Sell(int number)
        {
            ProductBusket busket;
            if (!_buskets.TryGetValue(number, out busket)) throw new InvalidProductNumberException(number);
            if(Balance < busket.Cost) throw new NotAnoughMoneyException();
            
            try
            {
               var product = busket.Dispence();
               Balance -= busket.Cost;
               return product;
            }
            catch (BusketIsEmptyException)
            {
                 throw new ProductIsOutOfStockException(busket.Product);
            }
        }
    }
}

public class ProductIsOutOfStockException : Exception
{
    public ProductIsOutOfStockException(IProduct product) :
        base(product.Name + " нет в наличии")
    {

    }
}

public class InvalidProductNumberException : Exception
{
    public InvalidProductNumberException(int number)
        : base("Выбран неверный номер продукта: " + number)
    {

    }
}