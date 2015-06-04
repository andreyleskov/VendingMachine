namespace VendingMachine.Domain.Wallet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Кошелёк с логикой выдачи запрошенной суммы минимальным количеством монет.
    /// </summary>
    public class MinCoinsWallet : IWallet
    {
        #region Fields

        private readonly IDictionary<Money, List<Coin>> _coinsByValue;

        #endregion

        #region Constructors and Destructors

        public MinCoinsWallet(params Coin[] coins)
        {
            Dictionary<Money, List<Coin>> coinPiles = coins.GroupBy(c => c.Value)
                .ToDictionary(g => g.Key, g => g.ToList());
            this._coinsByValue = new SortedDictionary<Money, List<Coin>>(coinPiles);

            this.CalcTotal();
        }

        #endregion

        #region Public Properties

        public Coin[] Coins
        {
            get
            {
                return this._coinsByValue.Values.SelectMany(c => c).ToArray();
            }
        }

        public Money Total { get; private set; }

        #endregion

        #region Public Methods and Operators

        public Coin[] GetMoney(Money amount)
        {
            IDictionary<Money, int> ret;
            Money leftAmount = amount.Clone();
            if (amount > this.Total)
            {
                throw new NotAnoughMoneyException();
            }

            //Набираем суммы самыми дорогими монетами, которые есть
            //Опираемся на то, что заданные номиналы обеспечивают при этом набор суммы минимальным 
            //количеством монет. Плюс то что все значения монет целые.
            //По-хорошему должен быть обобщённый алгоритм, не завиcящий от номиналов. 
            Dictionary<Money, int> coinsToGiveAway = this._coinsByValue.Keys.Reverse().ToDictionary(
                k => k,
                k =>
                    {
                        int coinsNum = Math.Min(leftAmount / k, this._coinsByValue[k].Count);
                        leftAmount = leftAmount - k * coinsNum;
                        return coinsNum;
                    });

            if (leftAmount != Money.Zero)
            {
                throw new NotAnoughCoinsException();
            }

            var result = new List<Coin>();
            foreach (var pair in coinsToGiveAway)
            {
                List<Coin> coins = this._coinsByValue[pair.Key];
                result.AddRange(coins.Take(pair.Value));
                coins.RemoveRange(0, pair.Value);
            }
            this.CalcTotal();
            return result.ToArray();
        }

        public void Put(params Coin[] coins)
        {
            if (coins == null)
            {
                throw new ArgumentNullException("coins");
            }
            foreach (Coin coin in coins)
            {
                List<Coin> list;
                if (!this._coinsByValue.TryGetValue(coin.Value, out list))
                {
                    list = new List<Coin>();
                    this._coinsByValue[coin.Value] = list;
                }
                list.Add(coin);
            }

            this.CalcTotal();
        }

        #endregion

        #region Methods

        private void CalcTotal()
        {
            this.Total = this._coinsByValue.Values.SelectMany(v => v).Aggregate(Money.Zero, (a, coin) => a + coin.Value);
        }

        #endregion
    }
}