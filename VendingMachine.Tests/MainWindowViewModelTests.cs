﻿using System;
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


        [Test]
        public void Customer_balance_changes_after_coin_insert()
        {
            var viewModel = new DesignTimeMainWindowViewModel();
            var customerCoinPile = viewModel.CustomerCoins.First();
            viewModel.PutCoinCommand.Execute(customerCoinPile);
            viewModel.PutCoinCommand.Execute(customerCoinPile);
            viewModel.PutCoinCommand.Execute(customerCoinPile);

            viewModel.Balance.ShouldEqual(customerCoinPile.Coin.Value * 3);
        }

        [Test]
        public void Buying_Product_reducing_its_amount()
        {
            var viewModel = GetModelWithCoins();
            var productViewModel = viewModel.MachineProducts.First(p => p.Number == 1);
            var productAmount = productViewModel.Amount - 1;
            viewModel.BuyProductCommand.Execute(1);

            productAmount.ShouldEqual(productViewModel.Amount);
        }

        [Test]
        public void Buying_all_products_removing_it_from_machine()
        {
            var viewModel = GetModelWithCoins();
            var showCaseItemViewModel = viewModel.MachineProducts.First(p => p.Number == 1);
            for(int i = 0; i < showCaseItemViewModel.Amount ; i++)
                viewModel.BuyProductCommand.Execute(1);

            viewModel.MachineProducts.ShouldNotContain(showCaseItemViewModel);
        }

        private static DesignTimeMainWindowViewModel GetModelWithCoins()
        {
            var viewModel = new DesignTimeMainWindowViewModel();
            var coinPile = new CoinPile(Coin.Ten(), 10);
            for (int i = 0; i < 10; i++)
                viewModel.PutCoinCommand.Execute(coinPile);

            return viewModel;
        }

        [Test]
        public void Buying_a_product_adds_it_to_customer_product_list()
        {
            var viewModel = GetModelWithCoins();
            var showCaseItemViewModel = viewModel.MachineProducts.First(p => p.Number == 1);
            viewModel.BuyProductCommand.Execute(1);

            viewModel.CustomerProducts.ShouldContain(showCaseItemViewModel.Product);
        }
    }
}