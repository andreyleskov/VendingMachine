namespace VendingMachine.UI
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PileViewModelOf<T> : IPileViewModelOf<T>
    {
        public T Item { get; private set; }
        public virtual string ImagePath { get; private set; }

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

        public PileViewModelOf(T product, int amount, string imagePath)
        {
            this.Item = product;
            this.Amount = amount;
            this.ImagePath = imagePath;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}