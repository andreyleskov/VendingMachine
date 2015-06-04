namespace VendingMachine.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoinsBuilder
    {
        #region Fields

        private readonly List<Coin> _coins = new List<Coin>();

        private Func<Coin> _coinCreator;

        #endregion

        #region Public Methods and Operators

        public static CoinsBuilder New()
        {
            return new CoinsBuilder();
        }

        public Coin[] GetCoins()
        {
            return this._coins.ToArray();
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

        #endregion
    }
}