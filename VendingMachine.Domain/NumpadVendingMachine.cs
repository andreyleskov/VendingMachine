namespace VendingMachine.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using VendingMachine.Domain.Wallet;

    public class NumpadVendingMachine : IVendingMachine
    {
        #region Fields

        private readonly IDictionary<int, ProductBusket> _buskets;

        private readonly IWallet _machineWallet;

        #endregion

        #region Constructors and Destructors

        public NumpadVendingMachine(IWallet machineWallet, params ProductBusket[] buskets)
        {
            this._buskets = buskets == null
                                ? new Dictionary<int, ProductBusket>()
                                : buskets.ToDictionary(b => b.Number, b => b);
            this._machineWallet = machineWallet;
            this.Balance = Money.Zero;
        }

        #endregion

        #region Public Properties

        public Money Balance { get; private set; }

        public IReadOnlyList<IShowcaseItem> Showcase
        {
            get
            {
                return this._buskets.Values.Cast<IShowcaseItem>().ToList();
            }
        }

        #endregion

        #region Public Methods and Operators

        public Coin[] GetChange()
        {
            Coin[] change = this._machineWallet.GetMoney(this.Balance);
            this.Balance -= change.Aggregate(Money.Zero, (m, c) => m + c.Value);
            return change;
        }

        public void Insert(Coin coin)
        {
            this._machineWallet.Put(coin);
            this.Balance += coin.Value;
        }

        public IProduct Sell(int number)
        {
            ProductBusket busket;
            if (!this._buskets.TryGetValue(number, out busket))
            {
                throw new InvalidProductNumberException(number);
            }
            if (this.Balance < busket.Cost)
            {
                throw new NotAnoughMoneyException();
            }

            try
            {
                IProduct product = busket.Dispence();
                this.Balance -= busket.Cost;
                return product;
            }
            catch (BusketIsEmptyException)
            {
                throw new ProductIsOutOfStockException(busket.Product);
            }
        }

        #endregion
    }
}