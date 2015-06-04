using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Remoting;
    using System.Windows.Input;
    using VendingMachine.Domain;
    using VendingMachine.Domain.Wallet;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IVendingMachine _machine;
        private readonly IWallet _customerWallet;

        private readonly IInteractionService _interactionService;

        public ObservableCollection<IPileViewModelOf<Coin>> MachineCoins { get; set; }
        public ObservableCollection<IPileViewModelOf<Coin>> CustomerCoins { get; set; }

        public ObservableCollection<ShowCaseItemViewModel> MachineProducts { get; set; }
        public ObservableCollection<IPileViewModelOf<IProduct>> CustomerProducts { get; set; }

        public ICommand PutCoinCommand    { get; private set; }
        public ICommand BuyProductCommand { get; private set; }
        public ICommand GetChangeCommand  { get; private set; }

        private const string BuyConfirmMessage = "Спасибо!";

        public MainWindowViewModel(IVendingMachine machine,
                                   IWallet machineWallet,
                                   IWallet customerWallet,
                                   IInteractionService interactionService)
        {
            this._interactionService = interactionService;
            _machine = machine;
            _customerWallet = customerWallet;

            MachineProducts = new ObservableCollection<ShowCaseItemViewModel>(_machine.Showcase.Select(i => new ShowCaseItemViewModel(i)));
            CustomerProducts = new ObservableCollection<IPileViewModelOf<IProduct>>();

            MachineCoins = new ObservableCollection<IPileViewModelOf<Coin>>(machineWallet.Coins.ToPiles());
            CustomerCoins = new ObservableCollection<IPileViewModelOf<Coin>>(customerWallet.Coins.ToPiles());

            PutCoinCommand = new DelegateCommand<IPileViewModelOf<Coin>>(PutCoins, CanPutCoins);
            BuyProductCommand = new DelegateCommand<int>(BuyProduct, CanBuyProduct);
            GetChangeCommand = new DelegateCommand<object>(GetChange);
        }

        private void GetChange(object obj)
        {
            var change = _machine.GetChange();
            _customerWallet.Put(change);

            foreach (var coin in change)
            {
                Coin coin1 = coin;
                Reduce<Coin, IPileViewModelOf<Coin>>(MachineCoins, c => c.Item == coin1);
                Increase<Coin, IPileViewModelOf<Coin>>(CustomerCoins, c => c.Item == coin1, () => new CoinPile(coin1, 0));
            }

            OnPropertyChanged(GetName.Of(this, t => t.Balance));
            OnPropertyChanged(GetName.Of(this, t => t.CustomerTotal));
        }

        private bool CanBuyProduct(int number)
        {
            return _machine.Showcase.Any(s => s.Number == number);
        }

        private void BuyProduct(int number)
        {
            IProduct product;
            try
            {
                product = _machine.Sell(number);
            }
            catch (NotAnoughCoinsException exCoins)
            {
                _interactionService.Notify(exCoins.Message);
                return;
            }
            catch (NotAnoughMoneyException exMoney)
            {
                _interactionService.Notify(exMoney.Message);
                return;
            }

            OnPropertyChanged(GetName.Of(this, t => t.Balance));

            this.Reduce<IProduct, ShowCaseItemViewModel>(MachineProducts,
                                                         p => p.Item == null || this.Like(p.Item, product));

            this.Increase<IProduct, IPileViewModelOf<IProduct>>(CustomerProducts,
                                                                p => this.Like(p.Item,product),
                                                                () => new ProductPile(product,0));

            _interactionService.Notify(BuyConfirmMessage);
        }

        private void Increase<T, U>(ICollection<U> collection,
                                    Func<U, bool> searchFunc,
                                    Func<U> factory) where U : class, IPileViewModelOf<T>
        {
            var existedItem = collection.FirstOrDefault(searchFunc);
            if (existedItem == null)
            {
                existedItem = factory();
                collection.Add(existedItem);
            }
            existedItem.Amount++;
        }

        private bool Like(IProduct a, IProduct b)
        {
            return a.GetType() == b.GetType();
        }

        private bool CanPutCoins(IPileViewModelOf<Coin> parameter)
        {
            return parameter.Amount > 0;
        }

        private void PutCoins(IPileViewModelOf<Coin> pile)
        {
            var coin = _customerWallet.GetCoinLike(pile.Item);
            Reduce<Coin,IPileViewModelOf<Coin>>(CustomerCoins,
                                                c => c.Item == coin );

            _machine.Insert(coin);

            OnPropertyChanged(GetName.Of(this, t => t.Balance));
            OnPropertyChanged(GetName.Of(this, t => t.CustomerTotal));

            Increase<Coin, IPileViewModelOf<Coin>>(MachineCoins, 
                                                   c => c.Item == coin, 
                                                   () => new CoinPile(coin, 0));
        }

        public string Balance
        {
            get
            {
                return  _machine.Balance.ToUIString();
            }
        }

        public string CustomerTotal
        {
            get
            {
                return _customerWallet.Total.ToUIString();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Reduce<T, U>(ICollection<U> collection,
                                  Func<U,bool> searchFunc) where U : IPileViewModelOf<T>
        {
            var showcaseProduct = collection.First(searchFunc);
            showcaseProduct.Amount--;
            if (showcaseProduct.Amount == 0)
                collection.Remove(showcaseProduct);
        }

  

    }
}
