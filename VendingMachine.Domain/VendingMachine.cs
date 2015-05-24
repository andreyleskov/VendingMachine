using System;

using VendingMachine.Domain;

namespace VendingMachine.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::VendingMachine.Domain.Wallet;

    public class VendingMachine : IVendingMachine
    {
        public IReadOnlyList<IShowcaseItem> Showcase
        {
            get
            {
                return _buskets.Values.Cast<IShowcaseItem>().ToList();
            }
        }

        public Money Balance
        {
            get
            {
                return _customerWallet.Total;
            }
        }

        private readonly IWallet _customerWallet;

        private readonly IWallet _machineWallet;

        private readonly IDictionary<int, ProductBusket> _buskets;

        public VendingMachine(IEnumerable<ProductBusket> buskets,
                              IWallet machineWallet,
                              IWallet customerWallet)
        {
            this._buskets = buskets.ToDictionary(b => b.Number, b => b);
            this._machineWallet = machineWallet;
            this._customerWallet = customerWallet;
        }

        public void Insert(Coin coin)
        {
            _customerWallet.Put(coin);
        }

        public Coin[] GetChange()
        {
            return _customerWallet.GetMoney(_customerWallet.Total);
        }

        public IProduct Buy(int number)
        {
            ProductBusket busket;
            if (!_buskets.TryGetValue(number, out busket)) throw new InvalidProductNumberException(number);
            if(_customerWallet.Total < busket.Cost) throw new NotAnoughMoneyException();

            var customerTotal = _customerWallet.Total;
            _machineWallet.Put(_customerWallet.GetMoney(_customerWallet.Total));
            var change = _machineWallet.GetMoney(customerTotal - busket.Cost);
            _customerWallet.Put(change);

            try
            {
                return busket.Dispence();
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