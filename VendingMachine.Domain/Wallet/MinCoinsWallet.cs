namespace VendingMachine.Domain.Wallet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ������ � ������� ������ ����������� ����� ����������� ����������� �����. 
    /// </summary>
    public class MinCoinsWallet : IWallet
    {
        private readonly IDictionary<Money, List<Coin>> _coinsByValue;

        public MinCoinsWallet(params Coin[] coins)
        {
            var coinPiles = coins.GroupBy(c => c.Value)
                                 .ToDictionary(g => g.Key, g => g.ToList());
            _coinsByValue = new SortedDictionary<Money, List<Coin>>(coinPiles);

            this.CalcTotal();
        }

        public Coin[] GetMoney(Money amount)
        {
            IDictionary<Money, int> ret;
            var leftAmount = amount.Clone();
            if(amount > this.Total)
                throw new NotAnoughMoneyException();

            //�������� ����� ������ �������� ��������, ������� ����
            //��������� �� ��, ��� �������� �������� ������������ ��� ���� ����� ����� ����������� 
            //����������� �����. ���� �� ��� ��� �������� ����� �����.
            //��-�������� ������ ���� ���������� ��������, �� ����c���� �� ���������. 
            var coinsToGiveAway = this._coinsByValue.Keys.Reverse().ToDictionary(
                k => k,
                k => {
                         int coinsNum = Math.Min(leftAmount / k, this._coinsByValue[k].Count);
                         leftAmount   = leftAmount - k *  coinsNum;
                         return coinsNum;
                });


            if (leftAmount != Money.Zero) throw new NotAnoughCoinsException();

            var result = new List<Coin>();
            foreach (var pair in coinsToGiveAway)
            {
                var coins = this._coinsByValue[pair.Key];
                result.AddRange(coins.Take(pair.Value));
                coins.RemoveRange(0, pair.Value);
            }
            this.CalcTotal();
            return result.ToArray();
        }


        public void Put(params Coin[] coins)
        {
            if(coins == null) throw new ArgumentNullException("coins");
            foreach (var coin in coins)
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

        private void CalcTotal()
        {
            this.Total = this._coinsByValue.Values.SelectMany(v => v).Aggregate(Money.Zero,(a,coin) => a + coin.Value);
        }

        public Money Total { get; private set; }

        public Coin[] Coins 
        { 
            get
            {
                return _coinsByValue.Values.SelectMany(c => c).ToArray();
            }
        }
    }
}