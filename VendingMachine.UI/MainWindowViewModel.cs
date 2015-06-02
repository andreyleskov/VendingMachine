using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Remoting;
    using System.Windows.Input;
    using VendingMachine.Domain;

    public class MainWindowViewModel
    {
        private IVendingMachine _machine;
        private IWallet _customerWallet;
        private IWallet _machineWallet;

        public ObservableCollection<CoinPile> MachineCoins { get; set; }
        public ObservableCollection<CoinPile> CustomerCoins { get; set; }

        public ObservableCollection<IShowcaseItem> MachineProducts { get; set; }
        public ObservableCollection<IProduct> CustomerProducts { get; set; }

        public ICommand PutCoinCommand { get; private set; }

        public MainWindowViewModel(IVendingMachine machine, IWallet machineWallet, IWallet customerWallet)
        {
            _machine = machine;
            _customerWallet = customerWallet;
            _machineWallet = machineWallet;

            MachineProducts = new ObservableCollection<IShowcaseItem>(_machine.Showcase);
            CustomerProducts = new ObservableCollection<IProduct>();

            MachineCoins = new ObservableCollection<CoinPile>(machineWallet.Coins.ToPiles());
            CustomerCoins = new ObservableCollection<CoinPile>(customerWallet.Coins.ToPiles());

            PutCoinCommand = new DelegateCommand<CoinPile>(PutCoins, CanPutCoins);
        }

        public bool CanPutCoins(CoinPile parameter)
        {
            return parameter.Amount > 0;
        }

        public void PutCoins(CoinPile pile)
        {
            var coin = _customerWallet.GetMoney(pile.Value).Single();
            pile.Amount--;
            if (pile.Amount == 0) CustomerCoins.Remove(pile);

            _machine.Insert(coin);

            var machinePile = MachineCoins.FirstOrDefault(c => c.Value == coin.Value);
            machinePile.Amount ++;
        }

        public string Title
        {
            get
            {
                return "It's alive!";
            }
        }
    }
}
