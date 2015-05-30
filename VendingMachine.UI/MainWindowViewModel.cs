using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI
{
    using System.Collections.ObjectModel;
    using System.Runtime.Remoting;

    using VendingMachine.Domain;

    public static class CoinExtensions
    {
        public static IEnumerable<CoinPile> ToPiles(this IEnumerable<Coin> coins)
        {
            return coins.GroupBy(c => c.Value).Select(g => new CoinPile() { Value = g.Key, Amount = g.Count() });
        }   
    }

    class MainWindowViewModel
    {
        private IVendingMachine _machine;

        private IWallet _customerWallet;
        private IWallet _machineWallet;

        public ObservableCollection<CoinPile> MachineCoins { get; set; }
        public ObservableCollection<CoinPile> CustomerCoins { get; set; }

        public ObservableCollection<IShowcaseItem> MachineProducts { get; set; }
        public ObservableCollection<IProduct> CustomerProducts { get; set; }

        public MainWindowViewModel(IVendingMachine machine, IWallet machineWallet, IWallet customerWallet)
        {
            _machine = machine;
            _customerWallet = customerWallet;
            _machineWallet = machineWallet;

            MachineProducts = new ObservableCollection<IShowcaseItem>(_machine.Showcase);
            CustomerProducts = new ObservableCollection<IProduct>();

            MachineCoins = new ObservableCollection<CoinPile>(machineWallet.Coins.ToPiles());
            CustomerCoins = new ObservableCollection<CoinPile>(customerWallet.Coins.ToPiles());
        }

    }
}
