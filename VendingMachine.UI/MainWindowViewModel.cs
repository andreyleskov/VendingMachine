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
        private readonly IVendingMachine _machine;
        private readonly IWallet _customerWallet;

        public ObservableCollection<CoinPile> MachineCoins { get; set; }
        public ObservableCollection<CoinPile> CustomerCoins { get; set; }

        public ObservableCollection<IShowcaseItem> MachineProducts { get; set; }
        public ObservableCollection<IProduct> CustomerProducts { get; set; }

        public ICommand PutCoinCommand { get; private set; }

        public MainWindowViewModel(IVendingMachine machine, IWallet machineWallet, IWallet customerWallet)
        {
            _machine = machine;
            _customerWallet = customerWallet;

            MachineProducts = new ObservableCollection<IShowcaseItem>(_machine.Showcase);
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

            
            var machinePile = this.MachineCoins.FirstOrDefault(c => c.Coin == coin);
            if (machinePile == null)
            {
                machinePile = new CoinPile(pile.Coin);
                MachineCoins.Add(machinePile);
            }

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
