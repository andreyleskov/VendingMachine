using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Remoting;
    using System.Windows.Input;
    using VendingMachine.Domain;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IVendingMachine _machine;
        private readonly IWallet _customerWallet;

        public ObservableCollection<CoinPile> MachineCoins { get; set; }
        public ObservableCollection<CoinPile> CustomerCoins { get; set; }

        public ObservableCollection<ShowCaseItemViewModel> MachineProducts { get; set; }
        public ObservableCollection<IProduct> CustomerProducts { get; set; }

        public ICommand PutCoinCommand { get; private set; }

        public MainWindowViewModel(IVendingMachine machine, IWallet machineWallet, IWallet customerWallet)
        {
            _machine = machine;
            _customerWallet = customerWallet;

            MachineProducts = new ObservableCollection<ShowCaseItemViewModel>(_machine.Showcase.Select(i => new ShowCaseItemViewModel(i)));
            CustomerProducts = new ObservableCollection<IProduct>();

            MachineCoins = new ObservableCollection<CoinPile>(machineWallet.Coins.ToPiles());
            CustomerCoins = new ObservableCollection<CoinPile>(customerWallet.Coins.ToPiles());

            PutCoinCommand = new DelegateCommand<CoinPile>(PutCoins, CanPutCoins);
        }

        private bool CanPutCoins(CoinPile parameter)
        {
            return parameter.Amount > 0;
        }

        private void PutCoins(CoinPile pile)
        {
            var coin = _customerWallet.GetMoney(pile.Coin.Value).Single();
            pile.Amount--;
            if (pile.Amount == 0) CustomerCoins.Remove(pile);

            _machine.Insert(coin);
            OnPropertyChanged(GetName.Of(this, t => t.Balance));
            
            var machinePile = this.MachineCoins.FirstOrDefault(c => c.Coin == coin);
            if (machinePile == null)
            {
                machinePile = new CoinPile(pile.Coin);
                MachineCoins.Add(machinePile);
            }

            machinePile.Amount ++;
        }


        public Money Balance
        {
            get
            {
                return _machine.Balance;
            }
        }

        public string Title
        {
            get
            {
                return "It's alive!";
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
    }
}
