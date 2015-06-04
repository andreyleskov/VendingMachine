using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace  VendingMachine.UI
{
    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;
    using VendingMachine.Domain.Wallet;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = this.ComposeApp();
            mainWindow.ShowDialog();
        }

        private MainWindow ComposeApp()
        {
            var machineWallet = new MinCoinsWallet(
               CoinsBuilder.New().PileOf(Coin.One).Size(100)
                    .PileOf(Coin.Two).Size(100)
                    .PileOf(Coin.Five).Size(100)
                    .PileOf(Coin.Ten).Size(100)
               .GetCoins());

            var productBuskets = new[]
                                     {
                                         ProductBusket.Of<Tea>(1, Money.Rub(13), 10),
                                         ProductBusket.Of<Coffe>(2, Money.Rub(18), 20),
                                         ProductBusket.Of<CoffeWithMilk>(3, Money.Rub(21), 20),
                                         ProductBusket.Of<Juice>(4, Money.Rub(35), 15)
                                     };

            var machine = new NumpadVendingMachine(machineWallet, productBuskets);

            var customerWallet = new MinCoinsWallet(
                CoinsBuilder.New().PileOf(Coin.One).Size(10)
                                  .PileOf(Coin.Two).Size(30)
                                  .PileOf(Coin.Five).Size(20)
                                  .PileOf(Coin.Ten).Size(15)
                            .GetCoins());

            var mainViewModel = new MainWindowViewModel(machine, machineWallet, customerWallet, new SimpleInteractionService());

            return new MainWindow(){DataContext = mainViewModel};
        }
    }
}
