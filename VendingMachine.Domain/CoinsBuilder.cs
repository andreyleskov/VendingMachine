namespace VendingMachine.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoinsBuilder
    {
        private readonly List<Coin> _coins = new List<Coin>();

        private Func<Coin> _coinCreator;

        public static CoinsBuilder New()
        {
            return new CoinsBuilder();
        }
        public CoinsBuilder PileOf(Func<Coin> coinCreator)
        {
            this._coinCreator = coinCreator;
            return this;
        }

        public CoinsBuilder Size(int count)
        {
            this._coins.AddRange(Enumerable.Range(1, count).Select(c => this._coinCreator()));
            return this;
        }

        public Coin[] GetCoins()
        {
            return _coins.ToArray();
        }
    }
}