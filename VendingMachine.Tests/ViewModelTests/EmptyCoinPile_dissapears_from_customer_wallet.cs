using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Tests.ViewModelTests
{
    using NUnit.Framework;

    using Should;

    using VendingMachine.Domain;
    using VendingMachine.Domain.Wallet;
    using VendingMachine.UI;

    [TestFixture]
    public class MainWindowViewModelTests
    {
        [Test]
        public void EmptyCoinPile_dissapears_from_customer_wallet()
        {
            var viewModel = new DesignTimeMainWindowViewModel();
            var customerCoinPile = viewModel.CustomerCoins.First();
            var machineCoins = viewModel.MachineCoins.Single(c => c.Coin == customerCoinPile.Coin);
            int amount = customerCoinPile.Amount;
            for(int i=0; i < amount; i++)
                viewModel.PutCoinCommand.Execute(customerCoinPile);

            viewModel.CustomerCoins.ShouldNotContain(customerCoinPile);
        }

        [Test]
        public void NewCoinPile_appears_in_machine_wallet()
        {
            var customerWallet = new MinCoinsWallet(Coin.One());
            var machineWallet = new MinCoinsWallet();
            var machine = new NumpadVendingMachine(machineWallet, null);
            var viewModel = new MainWindowViewModel(machine,machineWallet,customerWallet);

            var customerCoinPile = viewModel.CustomerCoins.First();
            int amount = customerCoinPile.Amount;
            for (int i = 0; i < amount; i++)
                viewModel.PutCoinCommand.Execute(customerCoinPile);

            viewModel.MachineCoins.Any(c => c.Coin == customerCoinPile.Coin)
                                  .ShouldBeTrue();
        }
    }
}
