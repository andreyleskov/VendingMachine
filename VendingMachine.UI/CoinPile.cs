namespace VendingMachine.UI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using VendingMachine.Domain;

    public class CoinPile:INotifyPropertyChanged
    {
        public Money Value { get; private set; }

        private int _amount;
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

        public string ImagePath { get; private set; }

        public CoinPile(Coin coin, int amount)
        {
            Value = coin.Value;
            Amount = amount;

            string path;
            CoinImagesPaths.TryGetValue(Value, out path);
            ImagePath = path;
        }

        private static readonly Dictionary<Money, string> CoinImagesPaths= new Dictionary<Money, string>();


        static CoinPile()
        {
            CoinImagesPaths[Coin.One().Value]  = "/VendingMachine.UI;component/Images/1_coin.png"; 
            CoinImagesPaths[Coin.Two().Value]  = "/VendingMachine.UI;component/Images/2_coin.png"; 
            CoinImagesPaths[Coin.Five().Value] = "/VendingMachine.UI;component/Images/5_coin.png"; 
            CoinImagesPaths[Coin.Ten().Value]  = "/VendingMachine.UI;component/Images/10_coin.png"; 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}