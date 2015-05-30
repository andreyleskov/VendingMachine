namespace VendingMachine.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoinBuilder
    {
        private readonly List<Coin> _coins = new List<Coin>();

        private Func<Coin> _coinCreator;

        public static CoinBuilder New()
        {
            return new CoinBuilder();
        }
        public CoinBuilder PileOf(Func<Coin> coinCreator)
        {
            this._coinCreator = coinCreator;
            return this;
        }

        public CoinBuilder Take(int count)
        {
            this._coins.AddRange(Enumerable.Range(1, count).Select(c => this._coinCreator()));
            return this;
        }

        public Coin[] Create()
        {
            return _coins.ToArray();
        }
    }
}